using Aras.IOM;
using hzerp.Models;
using hzerpdemo.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace hzerpdemo.helper
{
    public class DataHelper
    {
        public PartDataModel GetPartData()
        {
            var dataModel = new PartDataModel();
            dataModel.NeedReturnFields = new List<string>();
            dataModel.NeedUpDateFields = new List<string>();
            //dataModel.NeedUpDateFields.Add("SubHeadEntity");
            //dataModel.NeedUpDateFields.Add("FGROSSWEIGHT");
            //dataModel.NeedUpDateFields.Add("FErpClsID");

            var m = new Model();
            var tick = DateTime.UtcNow.Ticks.ToString();
            //m.FNumber = "0167629184";//编码
            //m.FName = "测试物料12.20--"+Guid.NewGuid().ToString();
            m.FName = "";
            //m.FMATERIALID = 393188;//实体主键，需要用view返回的id
            m.FMaterialGroup = new FMaterialGroup { FNumber = "2.1" };
            //m.F_TBIB_Combo = "F_TBIB_Combo";
            m.F_TBIB_Combo = "2";//编码属性，1是正式编码，2是临时编码

            m.FSubHeadEntity = new FSubHeadEntity();

            #region SubHeadEntity
            m.SubHeadEntity = new SubHeadEntity
            {
                FErpClsID = "1",//物料属性，原来是“自制”
                FBaseUnitId = new FBaseUnitId//基本单位
                {
                    FNumber = "02"
                },
                FCategoryID = new FCategoryID//存货类别
                {
                    FNumber = "CHLB03_SYS"
                },
                FSuite = "否",
                //FGROSSWEIGHT = 222222,
                FFeatureItem = "FFeatureItem"//特征件子项
            };
            #endregion

            #region SubHeadEntity1
            m.SubHeadEntity1 = new SubHeadEntity1
            {
                FStoreUnitID = new FStoreUnitID { FNumber = "02" },//库存单位,原来是01
                FCurrencyId = new FCurrencyId { FNumber = "PRE001" },//币别
                FUnitConvertDir = "库存单位-->辅助单位",//换算方向
                FSNGenerateTime = "自动生成",//序列号生成时机
                //FSNManageType = "管理每个事物的序列号",//业务范围
                FSNManageType = "1",//业务范围
                FStockId = new FStockId { FNumber = "102" },//仓库
                FBatchRuleID = new FBatchRuleID { FNumber = "PHBM002" },//批号编码规则
                //FAuxUnitID = new FAuxUnitID {  FNumber="02" },//辅助单位
                FIsLockStock = "true"//可锁库
            };
            #endregion

            #region SubHeadEntity2
            m.SubHeadEntity2 = new SubHeadEntity2
            {
                FSalePriceUnitId = new FSalePriceUnitId { FNumber = "02" },//销售计价单位
                FSaleUnitId = new FSaleUnitId { FNumber = "02" }//销售单位
            };
            #endregion

            #region SubHeadEntity3
            m.SubHeadEntity3 = new SubHeadEntity3
            {
                FPurchaseUnitId = new FPurchaseUnitId { FNumber = "02" },//采购单位
                FPurchasePriceUnitId = new FPurchasePriceUnitId { FNumber = "02" },//采购计价单位
                FQuotaType = "顺序优先"// 配额方式
            };
            #endregion


            #region SubHeadEntity4
            m.SubHeadEntity4 = new SubHeadEntity4
            {
                //FPlanningStrategy = "MRP",//计划策略
                //FOrderPolicy = "LFL(批对批) ",//订货策略
                //FFixLeadTimeType = "天",//固定提前期单位
                //FVarLeadTimeType = "个",//变动提前期单位
                //FCheckLeadTimeType = "天",//检验提前期单位
                //FOrderIntervalTimeType = "月",//订货间隔期单位
                //FReserveType = "FReserveType",//预留类型
                //FPlanOffsetTimeType = "分",//时间单位
                //FWriteOffQty = 1000000,//冲销数量
                FMinPOQty = 1,//最小订货量
                FIncreaseQty = 1,//最小包装量
                FEOQ = 1, //固定/经济批量
                FVarLeadTimeLotSize = 1,//变动提前期批量
                FMaxPOQty = 100000//最大订货量
            };
            #endregion

            #region SubHeadEntity5
            m.SubHeadEntity5 = new SubHeadEntity5
            {
                FIssueType = "直接领料",//发料方式
                FOverControlMode = "不允许超发",//超发控制方式
                FMinIssueUnitId = new FMinIssueUnitId { FNUMBER = "02" },//最小发料批量单位
                FStandHourUnitId = "分",//工时单位
                FBackFlushType = "主业务单位数量",//倒冲数量
                FMinIssueQty = 1,//最小发料批量
                FIsMainPrd = "true",//可为主产品，如果不加这个属性，bom结构选不到物料
            };
            #endregion

            #region SubHeadEntity7
            m.SubHeadEntity7 = new SubHeadEntity7();
            #endregion

            #region SubHeadEntity6
            m.SubHeadEntity6 = new SubHeadEntity6();
            #endregion

            #region FEntityAuxPty
            m.FEntityAuxPty = new List<FEntityAuxPty>();
            m.FEntityAuxPty.Add(new FEntityAuxPty());
            #endregion


            #region FEntityInvPty
            //m.FEntityInvPty = new List<FEntityInvPty>();
            //var fEntityInvPty1 = new FEntityInvPty();
            //fEntityInvPty1.FInvPtyId = new FInvPtyId { FNumber = "02" };//库存属性
            //fEntityInvPty1.FIsEnable = "true";
            //m.FEntityInvPty.Add(fEntityInvPty1);
            //var fEntityInvPty = new FEntityInvPty();
            //fEntityInvPty.FInvPtyId = new FInvPtyId { FNumber = "01" };//库存属性
            //fEntityInvPty.FIsEnable = "true";
            //m.FEntityInvPty.Add(fEntityInvPty);

            #endregion

            #region FBarCodeEntity_CMK
            m.FBarCodeEntity_CMK = new List<FBarCodeEntityCMK>();
            var fBarCodeEntityCMK = new FBarCodeEntityCMK
            {
                FCodeType_CMK = "FCodeType_CMK",
                FUnitId_CMK = new FUnitIdCMK { FNUMBER = "FNUMBER" }
            };
            m.FBarCodeEntity_CMK.Add(fBarCodeEntityCMK);
            #endregion

            dataModel.Model = m;
            return dataModel;
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

        public SubstitutionDraftModel GetSubstitutionModel(string unitFilePath, SubstitutionToSendedDataModel data)
        {
            var substitutionDraftModel = new SubstitutionDraftModel();
            substitutionDraftModel.NeedUpDateFields = new List<string>();
            substitutionDraftModel.NeedReturnFields = new List<string>();
            substitutionDraftModel.IsDeleteEntry = "true";
            substitutionDraftModel.IsVerifyBaseDataField = "false";
            substitutionDraftModel.ValidateFlag = "true";
            substitutionDraftModel.NumberSearch = "true";
            substitutionDraftModel.IsEntryBatchFill = "true";
            substitutionDraftModel.IsAutoAdjustField = "false";
            var m = new SubstitutionModel();
            m.FName = data.ParentData.PartNumber + "的替代名称";
            m.FBillType = new FBillType { FNUMBER = "TDFA01_SYS" };
            m.FReplacePolicy = "1";
            m.FReplaceType = "1";
            m.FReplaceSource = "01";
            m.FID = string.IsNullOrEmpty(data.ParentData.ERPSubstitutionId) ? 0 : int.Parse(data.ParentData.ERPSubstitutionId);

            m.FEntity = new List<FEntity>();
            var fPriority = 1;
            foreach (var childData in data.ChildrenDatas)
            {
                var fEntity = new FEntity
                {
                    FEntryID = 0,
                    FPriority = fPriority++,
                    FSubMaterialID = new FSubMaterialID { FNUMBER = childData.PartNumber },
                    FSubUnitID = new FSubUnitID { FNumber = GetUint(unitFilePath, childData.Unit) },
                    FSubIsKeyItem = true,
                    FSubNumerator = 1,
                    FSubDenominator = 1,
                    FEffectDate = "2024-01-31 00:00:00",
                    FExpireDate = "9999-12-31 00:00:00"
                };
                m.FEntity.Add(fEntity);
            }

            m.FEntityMainItems = new List<FEntityMainItem>();
            var fEntityMainItem = new FEntityMainItem
            {
                FEntryID = 0,
                FMaterialID = new FMaterialID { FNUMBER = data.ParentData.PartNumber },
                FUnitID = new FUnitID { FNumber = GetUint(unitFilePath, data.ParentData.Unit) },
                FNumerator = 1,
                FDenominator = 1,
                FIsKeyItem = true
            };
            m.FEntityMainItems.Add(fEntityMainItem);


            substitutionDraftModel.Model = m;
            return substitutionDraftModel;
        }

        /// <summary>
        /// 组装要发送的BOM数据
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="childItemList">子级part</param>
        /// <param name="parentItem">父级part</param>
        /// <returns></returns>
        public BomDataModel GetBomData(string filePath, IList<Item> childItemList, Item parentItem)
        {
            var bomDataModel = new BomDataModel();
            bomDataModel.NeedUpDateFields = new List<string>();
            bomDataModel.NeedReturnFields = new List<string>();
            bomDataModel.IsDeleteEntry = "true";
            bomDataModel.IsVerifyBaseDataField = "false";
            bomDataModel.ValidateFlag = "true";
            bomDataModel.IsAutoAdjustField = "false";
            bomDataModel.NumberSearch = "true";

            var parentKeyedName = parentItem.getProperty("keyed_name");
            var parentItemNumber = parentItem.getProperty("item_number");
            var parentHSUnit = parentItem.getProperty("hs_unit");
            var parentERPUnit = string.Empty;
            if (!string.IsNullOrEmpty(parentHSUnit))
            {
                parentERPUnit = GetUint(filePath, parentHSUnit);
            }
            else
            {
                parentERPUnit = "02";
            }

            var m = new BomModel();
            m.FBOMCATEGORY = "1";//BOM分类
            m.FBOMUSE = "99";//BOM用途
            m.FMATERIALID = new FMATERIALID { FNumber = parentItemNumber };//父项物料编码
            m.FBILLTYPE = new FBILLTYPE { FNUMBER = "WLQD01_SYS" };// 单据类型 （WLQD01_SYS在erp中代表物料清单）
            m.FUNITID = new FUNITID { FNumber = parentERPUnit };//父项物料单位
            m.FNumber = parentKeyedName;//bom编号
            m.FYIELDRATE = 50;

            m.FName = parentKeyedName + "--Bom";//bom名称


            #region FTreeEntity
            m.FTreeEntity = new List<FTreeEntity>();
            //var list = new List<string> { "0251290202", "0167629184", "0207114184" };//子级物料编码
            //for (var i = 0; i < list.Count; i++)
            foreach (var item in childItemList)
            {
                var itemNumber = item.getProperty("item_number");
                var hSUnit = item.getProperty("hs_unit");
                var eRPUnit = string.Empty;
                if (!string.IsNullOrEmpty(hSUnit))
                {
                    eRPUnit = GetUint(filePath, hSUnit);
                }
                else
                {
                    eRPUnit = "02";
                }


                var fTreeEntity = new FTreeEntity();
                fTreeEntity.FMATERIALTYPE = "1";//子项类型
                fTreeEntity.FDOSAGETYPE = "2";//用量类型
                fTreeEntity.FEFFECTDATE = "2023-12-19 00:00:00";
                fTreeEntity.FEXPIREDATE = "9999-12-31 00:00:00";
                fTreeEntity.FISSUETYPE = "1";
                //fTreeEntity.FCHILDITEMNAME = new FCHILDITEMNAME { FNumber = "子项物料名称" + i } ;
                //fTreeEntity.FCHILDITEMMODEL = "子项规格型号" + i;

                fTreeEntity.FTIMEUNIT = "1";
                fTreeEntity.FMATERIALIDCHILD = new FMATERIALIDCHILD { FNumber = itemNumber };//子项物料编码
                fTreeEntity.FOWNERTYPEID = "BD_OwnerOrg";//货主类型
                fTreeEntity.FCHILDUNITID = new FCHILDUNITID { FNumber = eRPUnit };//子项单位  
                fTreeEntity.FOverControlMode = "3";
                fTreeEntity.FEntrySource = "1";
                fTreeEntity.FPROCESSID = new FPROCESSID { FNumber = "FNumber" };
                fTreeEntity.FBaseNumerator = 1;// 基本单位分子
                fTreeEntity.FBaseDenominator = 100;//  基本单位分母
                fTreeEntity.FNUMERATOR = 1;//用量:分子
                fTreeEntity.FDENOMINATOR = 1;// 用量:分母

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
        /// <summary>
        /// 获取页面上物料信息
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static PartFormModel GetPartFormModel(Item item)
        {
            var m = new PartFormModel();
            m.item_number = item.getProperty("item_number");
            m.hs_part_type = item.getProperty("hs_part_type");
            m.hs_main_category = item.getProperty("hs_main_category");
            m.hs_middle_category = item.getProperty("hs_middle_category");
            m.hs_sub_category = item.getProperty("hs_sub_category");
            m.hs_subdivision_category = item.getProperty("hs_subdivision_category");
            m.name = item.getProperty("name");
            m.hs_specification = item.getProperty("hs_specification");
            m.hs_unit = item.getProperty("hs_unit");
            m.hs_stage = item.getProperty("hs_stage");
            m.hs_part_status = item.getProperty("hs_part_status");
            m.hs_store = item.getProperty("hs_store");
            m.hs_device_letter = item.getProperty("hs_device_letter");
            m.hs_bom_number = item.getProperty("hs_bom_number");
            m.hs_note = item.getProperty("hs_note");
            return m;
        }

        /// <summary>
        /// 
        /// </summary>
        ///  <param name="groupFilePath">物料分组文件路径</param>
        /// <param name="unitFilePath">物料基本单位文件路径</param>
        /// <param name="partFormModel">页面form数据</param>
        /// <param name="partId">在erp中生成的id</param>
        /// <param name="isUpdate">判断是暂存还是保存</param>
        /// <returns></returns>
        public static PartDataModel ConvertPartFormDataToERPData(string groupFilePath, string unitFilePath, PartFormModel partFormModel, int partId, bool isUpdate,string erpFileID)
        {
            var partDataModel = new PartDataModel();
            var m = new Model();
            string unitNumber = "02";
            #region 暂存
            if (!isUpdate)
            {
                unitNumber = GetUint(unitFilePath, partFormModel.hs_unit);
                m.FNumber = partFormModel.item_number;//编码
                m.FName = partFormModel.name;
                //m.FMATERIALID = 393188;//实体主键，需要用view返回的id
                m.FMaterialGroup = new FMaterialGroup { FNumber = GetPartGroup(groupFilePath, partFormModel.hs_sub_category) };// 物料分组
                //m.F_TBIB_Combo = "F_TBIB_Combo";
                m.F_TBIB_Combo = GetNumberProperty(partFormModel.hs_part_status);//编码属性，1是正式编码，2是临时编码
                m.FSpecification = partFormModel.hs_specification;
                m.F_TBIB_Attachment = erpFileID;
                m.FSubHeadEntity = new FSubHeadEntity();

                #region SubHeadEntity 基本
                m.SubHeadEntity = new SubHeadEntity
                {
                    FErpClsID = "1",//物料属性，原来是“自制”
                    FBaseUnitId = new FBaseUnitId//基本单位
                    {
                        FNumber = unitNumber
                    },
                    FCategoryID = new FCategoryID//存货类别
                    {
                        FNumber = "CHLB03_SYS"
                    },
                    FSuite = "否",
                    //FGROSSWEIGHT = 222222,
                    FFeatureItem = "FFeatureItem"//特征件子项
                };
                #endregion

                #region SubHeadEntity1 库存
                m.SubHeadEntity1 = new SubHeadEntity1
                {
                    FStoreUnitID = new FStoreUnitID { FNumber = unitNumber },//库存单位,原来是01
                    FCurrencyId = new FCurrencyId { FNumber = "PRE001" },//币别
                    FUnitConvertDir = "库存单位-->辅助单位",//换算方向
                    FSNGenerateTime = "自动生成",//序列号生成时机
                                             //FSNManageType = "管理每个事物的序列号",//业务范围
                    FSNManageType = "1",//业务范围
                    FStockId = new FStockId { FNumber = "102" },//仓库
                    FBatchRuleID = new FBatchRuleID { FNumber = "PHBM002" },//批号编码规则
                                                                            //FAuxUnitID = new FAuxUnitID {  FNumber="02" },//辅助单位
                    FIsLockStock = "true"//可锁库
                };
                #endregion

                #region SubHeadEntity2 销售
                m.SubHeadEntity2 = new SubHeadEntity2
                {
                    FSalePriceUnitId = new FSalePriceUnitId { FNumber = unitNumber },//销售计价单位
                    FSaleUnitId = new FSaleUnitId { FNumber = unitNumber }//销售单位
                };
                #endregion

                #region SubHeadEntity3 采购
                m.SubHeadEntity3 = new SubHeadEntity3
                {
                    FPurchaseUnitId = new FPurchaseUnitId { FNumber = unitNumber },//采购单位
                    FPurchasePriceUnitId = new FPurchasePriceUnitId { FNumber = unitNumber },//采购计价单位
                    FQuotaType = "顺序优先"// 配额方式
                };
                #endregion


                #region SubHeadEntity4 计划
                m.SubHeadEntity4 = new SubHeadEntity4
                {
                    //FPlanningStrategy = "MRP",//计划策略
                    //FOrderPolicy = "LFL(批对批) ",//订货策略
                    //FFixLeadTimeType = "天",//固定提前期单位
                    //FVarLeadTimeType = "个",//变动提前期单位
                    //FCheckLeadTimeType = "天",//检验提前期单位
                    //FOrderIntervalTimeType = "月",//订货间隔期单位
                    //FReserveType = "FReserveType",//预留类型
                    //FPlanOffsetTimeType = "分",//时间单位
                    //FWriteOffQty = 1000000,//冲销数量
                    FMinPOQty = 1,//最小订货量
                    FIncreaseQty = 1,//最小包装量
                    FEOQ = 1, //固定/经济批量
                    FVarLeadTimeLotSize = 1,//变动提前期批量
                    FMaxPOQty = 100000//最大订货量
                };
                #endregion

                #region SubHeadEntity5 生产
                m.SubHeadEntity5 = new SubHeadEntity5
                {
                    FIssueType = "直接领料",//发料方式
                    FOverControlMode = "不允许超发",//超发控制方式
                    FMinIssueUnitId = new FMinIssueUnitId { FNUMBER = unitNumber },//最小发料批量单位
                    FStandHourUnitId = "分",//工时单位
                    FBackFlushType = "主业务单位数量",//倒冲数量
                    FMinIssueQty = 1,//最小发料批量
                    FIsMainPrd = "true",//可为主产品，如果不加这个属性，bom结构选不到物料
                };
                #endregion

                #region SubHeadEntity7 委外
                m.SubHeadEntity7 = new SubHeadEntity7();
                #endregion

                #region SubHeadEntity6 质量
                m.SubHeadEntity6 = new SubHeadEntity6();
                #endregion

                #region FEntityAuxPty 辅助属性
                m.FEntityAuxPty = new List<FEntityAuxPty>();
                m.FEntityAuxPty.Add(new FEntityAuxPty());
                #endregion


                #region FEntityInvPty 库存属性
                #endregion

                #region FBarCodeEntity_CMK 条形码
                m.FBarCodeEntity_CMK = new List<FBarCodeEntityCMK>();
                var fBarCodeEntityCMK = new FBarCodeEntityCMK
                {
                    FCodeType_CMK = "FCodeType_CMK",
                    FUnitId_CMK = new FUnitIdCMK { FNUMBER = "FNUMBER" }
                };
                m.FBarCodeEntity_CMK.Add(fBarCodeEntityCMK);
                #endregion
            }
            #endregion
            #region 保存
            else
            {
                m.FNumber = partFormModel.item_number;//编码
                m.FName = partFormModel.name;
                m.FMATERIALID = partId;//实体主键，需要用view返回的id
                m.FMaterialGroup = new FMaterialGroup { FNumber = GetPartGroup(groupFilePath, partFormModel.hs_sub_category) };// 物料分组
                m.F_TBIB_Combo = GetNumberProperty(partFormModel.hs_part_status);//编码属性，1是正式编码，2是临时编码
                m.FSpecification = partFormModel.hs_specification;
                m.F_TBIB_Attachment = erpFileID;
                m.SubHeadEntity = new SubHeadEntity
                {
                    FBaseUnitId = new FBaseUnitId//基本单位
                    {
                        FNumber = GetUint(unitFilePath, partFormModel.hs_unit)
                    },
                };
                m.SubHeadEntity5 = new SubHeadEntity5 { FIsMainPrd = "true",FMinIssueQty=1 };
                if (partId != 0)
                {
                    //dataModel.NeedUpDateFields.Add("SubHeadEntity");
                    //dataModel.NeedUpDateFields.Add("FGROSSWEIGHT");
                    //dataModel.NeedUpDateFields.Add("FErpClsID");

                    partDataModel.NeedReturnFields.Add("FBaseUnitId");//基本单位
                    partDataModel.NeedReturnFields.Add("FNumber");

                    partDataModel.NeedReturnFields.Add("FName ");//名称

                    partDataModel.NeedReturnFields.Add("FNumber");//编码

                    partDataModel.NeedReturnFields.Add("FSpecification");//规格型号

                    partDataModel.NeedReturnFields.Add("FMaterialGroup");//物料分组
                    partDataModel.NeedReturnFields.Add("FNumber");

                    partDataModel.NeedReturnFields.Add("F_TBIB_Combo");//编码属性
                    partDataModel.NeedReturnFields.Add("FIsMainPrd");//可为主产品，如果不加这个属性，bom结构选不到物料
                }
            }
            #endregion
            partDataModel.Model = m;
            return partDataModel;
        }


        /// <summary>
        /// 获取物料基本单位
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetUint(string filePath, string value)
        {
            var unitContent = System.IO.File.ReadAllText(filePath);
            var unitData = JsonConvert.DeserializeObject<List<ERPUnit>>(unitContent);
            if (unitData != null)
            {
                var temp = unitData.Where(i => i.value == value).FirstOrDefault();
                if (temp != null)
                {
                    return temp.key;
                }
            }
            return "01";
        }

        /// <summary>
        /// 获取物料的分组
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetPartGroup(string filePath, string key)
        {
            var groupContent = System.IO.File.ReadAllText(filePath);
            var groupData = JsonConvert.DeserializeObject<List<ERPPartGroupKeyValuePair>>(groupContent);
            if (groupData != null)
            {
                var temp = groupData.Where(i => i.key == key).FirstOrDefault();
                if (temp != null)
                {
                    return temp.value;
                }
            }
            return "";
        }


        public static string GetNumberProperty(string status)
        {
            return status == "临时物料" ? "2" : "1";
        }
    }
}
