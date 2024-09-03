using Aras.IOM;
using hzerpdemo.Controllers;
using hzerpdemo.helper;
using hzerpdemo.Models;
using hzerpdemo.Util;
using Kingdee.CDP.WebApi.SDK;
using log4net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace hzerp.Controllers.Bom
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoBomDraftController : ControllerBase
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(AutoBomDraftController));
        private readonly IWebHostEnvironment _hostingEnvironment;
        public AutoBomDraftController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpPost]
        public JsonResult Bom()
        {
            string contentRootPath = _hostingEnvironment.ContentRootPath;
            var unitFilePath = contentRootPath + "\\data\\Unit.json";
            var jr = new JsonResult("");
            var formId = HttpContext.Request.Form["formId"].ToString();
            ThreadPool.SetMinThreads(10, 10);
            Task.Factory.StartNew(() =>
            {
                try
                {
                    //初始化
                    var clienter = new K3CloudApi();
                    //测试连接
                    RepoResult reporesult = clienter.CheckAuthInfo();
                    if (reporesult.ResponseStatus.IsSuccess)
                    {
                        var inn = InnovatorFactory.GetInnovator();
                        var sql =
                            $@"select hs_item  from [innovator].[HS_REL_ENGINEERING_ITEM]  where [SOURCE_ID]='{formId}' and HS_CATEGORY='物料'"; //查询送审单的送审对象中的物料id
                        var tempItem = inn.applySQL(sql);
                        var count = tempItem.getItemCount();
                        if (count == 0)
                        {
                            jr.Value = "没有送审的物料。";
                        }

                        for (int i = 0; i < count; i++)
                        {
                            List<Item> childPartList = new List<Item>();
                            var idItem = tempItem.getItemByIndex(i);
                            var partId = idItem.getProperty("hs_item"); //获取送审单的送审对象中的物料id
                            var queryParentPartItemSQL =
                                $@"select source_id from [innovator].[PART_BOM] where related_id='{partId}'"; //查询当前物料的父级物料id
                            Item queryParentPartItem = inn.applySQL(queryParentPartItemSQL);
                            var parentCount = queryParentPartItem.getItemCount();
                            if (parentCount <= 0)
                            {
                                jr.Value = "没有要暂存的BOM。";
                            }
                            else
                            {
                                var parentId = queryParentPartItem.getItemByIndex(0).getProperty("source_id");
                                var parentItem = inn.getItemById("Part", parentId); //获取当前物料的父级物料
                                var parentItemKeyedName = parentItem.getProperty("keyed_name");
                                if (parentItem == null)
                                {
                                    jr.Value = "没有送审的物料。";
                                }
                                else
                                {
                                    var childPartIdSQL =
                                        $@"select related_id from [innovator].[PART_BOM] where SOURCE_ID='{parentId}'"; //查询父级物料下的所有子级物料的id
                                    Item tempChildPartItem = inn.applySQL(childPartIdSQL);
                                    var ChildPartItemCount = tempChildPartItem.getItemCount();
                                    for (int j = 0; j < ChildPartItemCount; j++)
                                    {
                                        var childIdItem = tempChildPartItem.getItemByIndex(j);
                                        var childId = childIdItem.getProperty("related_id");
                                        Item childItem = inn.getItemById("Part", childId);
                                        if (childItem != null)
                                        {
                                            var state = childItem.getProperty("state");
                                            if (state.Equals("Released", StringComparison.InvariantCultureIgnoreCase))
                                            {
                                                childPartList.Add(childItem);
                                            }
                                        }
                                    }

                                    var viewdata = new ViewDataModel();
                                    viewdata.Number = parentItemKeyedName;

                                    var viewresultJson =
                                        clienter.View("ENG_BOM", JsonConvert.SerializeObject(viewdata));
                                    var viewres = JsonConvert.DeserializeObject<ERPPartResponse>(viewresultJson);
                                    if (viewres != null && viewres.Result != null &&
                                        viewres.Result.ResponseStatus != null &&
                                        viewres.Result.ResponseStatus.IsSuccess)
                                    {
                                        // jr.Value = "Bom已存在不需要同步。";

                                        var deleteresultJson = clienter.Delete("ENG_BOM",
                                            JsonConvert.SerializeObject(viewdata));
                                        var deleteres = JsonConvert.DeserializeObject<ERPPartResponse>(viewresultJson);
                                    }

                                    //else
                                    //{
                                    var helper = new DataHelper();
                                    var data = helper.GetBomData(unitFilePath, childPartList, parentItem);
                                    var resultJson = clienter.Draft("ENG_BOM", JsonConvert.SerializeObject(data));

                                    var res = JsonConvert.DeserializeObject<ApiSaveResponseModel>(resultJson);
                                    if (res != null && res.Result != null && res.Result.ResponseStatus != null &&
                                        res.Result.ResponseStatus.IsSuccess)
                                    {
                                        parentItem.setProperty("hs_erp_bom_id", res.Result.Id.ToString());
                                        parentItem.apply();
                                        jr.Value = "物料清单暂存成功";
                                    }
                                    else
                                    {
                                        parentItem.setProperty("hs_failed_info", "物料清单暂存失败");
                                        parentItem.apply();
                                    }
                                    //}
                                }

                            }

                        }

                        InnovatorFactory.Logout();
                    }
                }
                catch (Exception ex)
                {
                    _logger.Error("BOM传递ERP发生异常：" + ex);
                }
            });

            return jr;
        }
    }
}
