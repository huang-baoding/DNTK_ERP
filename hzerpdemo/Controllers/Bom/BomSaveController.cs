using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Text;
using Kingdee.CDP.WebApi.SDK;
using hzerpdemo.Models;
using System.Xml.Linq;
using System.Collections.Generic;
using hzerpdemo.helper;

namespace hzerpdemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BomSaveController : ControllerBase
    {
        [HttpPost]
        public JsonResult BomTest()
        {
            var jr = new JsonResult("");
            //初始化
            var clienter = new K3CloudApi();
            //测试连接
            RepoResult reporesult = clienter.CheckAuthInfo();
            if (reporesult.ResponseStatus.IsSuccess)
            {
                var helper = new DataHelper();
                var data = GetBomData();
                var resultJson = clienter.Save("ENG_BOM", JsonConvert.SerializeObject(data));
                var res = JsonConvert.DeserializeObject<ApiSaveResponseModel>(resultJson);
                if (res != null && res.Result != null && res.Result.ResponseStatus != null && res.Result.ResponseStatus.IsSuccess)
                {
                    jr.Value = "物料清单保存接口成功：" + $"名称：{data.Model.FName}，id={res.Result.Id}";
                }
            }

            return jr;
        }

        public BomDataModel GetBomData()
        {
            var bomDataModel = new BomDataModel();
            bomDataModel.NeedUpDateFields = new List<string>();
            bomDataModel.NeedReturnFields = new List<string>();
            bomDataModel.IsDeleteEntry = "true";
            bomDataModel.IsVerifyBaseDataField = "false";
            bomDataModel.ValidateFlag = "true";
            bomDataModel.IsAutoAdjustField = "false";
            bomDataModel.NumberSearch = "true";


            var m = new BomModel();
            m.FBOMCATEGORY = "1";//BOM分类
            m.FBOMUSE = "99";//BOM用途
            m.FMATERIALID = new FMATERIALID { FNumber = "1785455352" };//父项物料编码
            //m.FBILLTYPE = new FBILLTYPE { FNUMBER = "FNumber" };
            m.FBILLTYPE = new FBILLTYPE { FNUMBER = "WLQD01_SYS" };// 单据类型 （1，ENG_BOM，物料清单都不对）
            m.FUNITID = new FUNITID { FNumber = "02" };//父项物料单位
            m.FNumber = DateTime.UtcNow.Ticks.ToString();
            //m.FNumber = "638379420389453763";
            m.FYIELDRATE = 50;

            m.FName = "Bom12.20----" + Guid.NewGuid().ToString();
            //m.FName = "Bom简称2131c43b-c10d-496c-8791-909400ee5798";
            //m.F_TBIB_BaseProperty = " BOM 分组编码1111";
            //m.FITEMNAME = "物料名称";
            //m.FITEMMODEL = "规格型号";
            //m.FGroup = new FGroup { FNumber = "Bom组1" };
            //m.FCreateOrgId = new FCreateOrgId { FNumber = "" };

            #region FTreeEntity
            m.FTreeEntity = new List<FTreeEntity>();
            var list = new List<string> { "0251290202", "0167629184", "0207114184" };//子级物料编码
            for (var i = 0; i < list.Count; i++)
            {
                var fTreeEntity = new FTreeEntity();
                fTreeEntity.FMATERIALTYPE = "1";//子项类型
                fTreeEntity.FDOSAGETYPE = "2";//用量类型
                fTreeEntity.FEFFECTDATE = "2023-12-19 00:00:00";
                fTreeEntity.FEXPIREDATE = "9999-12-31 00:00:00";
                fTreeEntity.FISSUETYPE = "1";
                //fTreeEntity.FCHILDITEMNAME = new FCHILDITEMNAME { FNumber = "子项物料名称" + i } ;
                //fTreeEntity.FCHILDITEMMODEL = "子项规格型号" + i;

                fTreeEntity.FTIMEUNIT = "1";
                fTreeEntity.FMATERIALIDCHILD = new FMATERIALIDCHILD { FNumber = list[i] };//子项物料编码
                fTreeEntity.FOWNERTYPEID = "BD_OwnerOrg";//货主类型
                fTreeEntity.FCHILDUNITID = new FCHILDUNITID { FNumber = "02" };//子项单位  
                fTreeEntity.FOverControlMode = "3";
                fTreeEntity.FEntrySource = "1";
                fTreeEntity.FPROCESSID = new FPROCESSID { FNumber = "FNumber" };
                fTreeEntity.FBaseNumerator = 1;// 基本单位分子
                fTreeEntity.FBaseDenominator = 100;//  基本单位分母
                fTreeEntity.FNUMERATOR = 1;//用量:分子
                fTreeEntity.FDENOMINATOR = 1;// 用量:分母

                //fTreeEntity.FBOMCHILDLOTBASEDQTY = new List<FBOMCHILDLOTBASEDQTY>();

                //var fBOMCHILDLOTBASEDQTY = new FBOMCHILDLOTBASEDQTY();
                //fBOMCHILDLOTBASEDQTY.FMATERIALIDLOTBASED = new FMATERIALIDLOTBASED { FNumber = list[i] };//子项物料编码
                //fBOMCHILDLOTBASEDQTY.FUNITIDLOT = new FUNITIDLOT { FNumber = "个" };//子项单位
                ////fBOMCHILDLOTBASEDQTY.FCHILDITEMNAMELOT =  "物料名称" + i ;
                ////fBOMCHILDLOTBASEDQTY.FCHILDITEMNAMELOT = "FCHILDITEMNAMELOT"+i;


                //fTreeEntity.FBOMCHILDLOTBASEDQTY.Add(fBOMCHILDLOTBASEDQTY);

                //var fBOMCHILDLOTBASEDQTY1 = new FBOMCHILDLOTBASEDQTY();
                //fBOMCHILDLOTBASEDQTY1.FMATERIALIDLOTBASED = new FMATERIALIDLOTBASED { FNumber = "FNumber" };
                //fBOMCHILDLOTBASEDQTY1.FUNITIDLOT = new FUNITIDLOT { FNumber = "FNumber" };
                //fTreeEntity.FBOMCHILDLOTBASEDQTY.Add(fBOMCHILDLOTBASEDQTY1);
                m.FTreeEntity.Add(fTreeEntity);
            }
            #endregion

            #region FEntryBOMCOBY  
            //m.FEntryBOMCOBY = new List<FEntryBOMCOBY>();
            //var fEntryBOMCOBY = new FEntryBOMCOBY();
            //fEntryBOMCOBY.FCOBYTYPE = "FCOBYTYPE";//联副产品类型
            //fEntryBOMCOBY.FMATERIALIDCOBY = new FMATERIALIDCOBY { FNumber = "FNumber" };//联副产品物料编码
            //fEntryBOMCOBY.FUNITIDCOBY = new FUNITIDCOBY { FNumber = "个" };//单位
            //m.FEntryBOMCOBY.Add(fEntryBOMCOBY);
            #endregion

            #region FBopEntity   
            m.FBopEntity = new List<FBopEntity>();
            var fBopEntity = new FBopEntity();
            m.FBopEntity.Add(fBopEntity);
            #endregion
            bomDataModel.Model = m;
            return bomDataModel;
        }
    }
}
