using System.Collections.Generic;

namespace hzerpdemo.Models
{
    public class FATOSchemeId
    {
        public string FNUMBER { get; set; }
    }

    public class FAuxPropertyId
    {
        public string FNumber { get; set; }
    }

    public class FAuxUnitID
    {
        public string FNumber { get; set; }
    }

    //条形码
    public class FBarCodeEntityCMK
    {
        public int FEntryID { get; set; }
        public string FCodeType_CMK { get; set; }
        public FUnitIdCMK FUnitId_CMK { get; set; }
    }

    public class FBaseUnitId
    {
        public string FNumber { get; set; }
    }

    public class FBatchRuleID
    {
        public string FNumber { get; set; }
    }

    public class FBOMUnitId
    {
        public string FNumber { get; set; }
    }

    public class FCategoryID
    {
        public string FNumber { get; set; }
    }

    public class FChargeID
    {
        public string FNumber { get; set; }
    }

    public class FCurrencyId
    {
        public string FNumber { get; set; }
    }

    public class FDefaultLineId
    {
        public string FNUMBER { get; set; }
    }

    public class FDefaultLineIdSub
    {
        public string FNUMBER { get; set; }
    }

    public class FDefaultRouting
    {
        public string FNumber { get; set; }
    }

    public class FDefaultVendor
    {
        public string FNumber { get; set; }
    }

    public class FDefBarCodeRuleId
    {
        public string FNUMBER { get; set; }
    }

    //辅助属性
    public class FEntityAuxPty
    {
        public int FEntryID { get; set; }
        public FAuxPropertyId FAuxPropertyId { get; set; }
        public string FIsEnable1 { get; set; }
        public string FIsComControl { get; set; }
        public string FIsAffectPrice1 { get; set; }
        public string FIsAffectPlan1 { get; set; }
        public string FIsAffectCost1 { get; set; }
        public string FIsMustInput { get; set; }
        public string FValueType { get; set; }
    }

    //库存属性
    public class FEntityInvPty
    {
        public int FEntryID { get; set; }
        public FInvPtyId FInvPtyId { get; set; }
        public string FIsEnable { get; set; }
        public string FIsAffectPrice { get; set; }
        public string FIsAffectPlan { get; set; }
        public string FIsAffectCost { get; set; }
    }

    public class FIncQcSchemeId
    {
        public string FNUMBER { get; set; }
    }

    public class FIncSampSchemeId
    {
        public string FNUMBER { get; set; }
    }

    public class FInspectGroupId
    {
        public string FNUMBER { get; set; }
    }

    public class FInspectorId
    {
        public string FNUMBER { get; set; }
    }

    public class FInvPtyId
    {
        public string FNumber { get; set; }
    }

    public class FMaterialGroup
    {
        public string FNumber { get; set; }
    }

    public class FMdlId
    {
        public string FNUMBER { get; set; }
    }

    public class FMdlMaterialId
    {
        public string FNUMBER { get; set; }
    }

    public class FMfgPolicyId
    {
        public string FNumber { get; set; }
    }

    public class FMinIssueUnitId
    {
        public string FNUMBER { get; set; }
    }

    public class FOrgTrustBillType
    {
        public string FNUMBER { get; set; }
    }

    public class FPickBinId
    {
    }

    public class FPickStockId
    {
        public string FNumber { get; set; }
    }

    public class FPlanerID
    {
        public string FNumber { get; set; }
    }

    public class FPlanGroupId
    {
        public string FNumber { get; set; }
    }

    public class FPlanIdent
    {
        public string FNumber { get; set; }
    }

    public class FPlanWorkshop
    {
        public string FNumber { get; set; }
    }

    public class FPOBillTypeId
    {
        public string FNUMBER { get; set; }
    }

    public class FProduceBillType
    {
        public string FNUMBER { get; set; }
    }

    public class FProduceUnitId
    {
        public string FNumber { get; set; }
    }

    public class FProductLine
    {
        public string FNUMBER { get; set; }
    }

    public class FProScheTrackId
    {
        public string FNumber { get; set; }
    }

    public class FPurchaseGroupId
    {
        public string FNumber { get; set; }
    }

    public class FPurchasePriceUnitId
    {
        public string FNumber { get; set; }
    }

    public class FPurchaserId
    {
        public string FNumber { get; set; }
    }

    public class FPurchaseUnitId
    {
        public string FNumber { get; set; }
    }

    public class FQtyFactorId
    {
        public string FNumber { get; set; }
    }

    public class FRetailUnitID
    {
        public string FNUMBER { get; set; }
    }

    public class FSalePriceUnitId
    {
        public string FNumber { get; set; }
    }

    public class FSaleUnitId
    {
        public string FNumber { get; set; }
    }

    public class FSalGroup
    {
        public string FNumber { get; set; }
    }

    public class FSNCodeRule
    {
        public string FNumber { get; set; }
    }

    public class FSNUnit
    {
        public string FNumber { get; set; }
    }

    //规格属性列表
    public class FSpecialAttributeEntity
    {
        public int FEntryID { get; set; }
    }

    public class FStockId
    {
        public string FNumber { get; set; }
    }

    public class FStockPlaceId
    {
    }

    public class FStoreUnitID
    {
        public string FNumber { get; set; }
    }

    public class FSubBillType
    {
        public string FNUMBER { get; set; }
    }

    public class FSubconPriceUnitId
    {
        public string FNumber { get; set; }
    }

    public class FSubconUnitId
    {
        public string FNumber { get; set; }
    }


    //零售特性
    public class FSubHeadEntity
    {
        public int FEntryId { get; set; }
        public string FIsControlSal { get; set; }
        public int FLowerPercent { get; set; }
        public int FUpPercent { get; set; }
        public string FCalculateBase { get; set; }
        public int FMaxSalPrice_CMK { get; set; }
        public int FMinSalPrice_CMK { get; set; }
        public string FIsAutoRemove { get; set; }
        public string FIsMailVirtual { get; set; }
        public string FIsFreeSend { get; set; }
        public string FTimeUnit { get; set; }
        public int FRentFreeDura { get; set; }
        public int FPricingStep { get; set; }
        public int FMinRentDura { get; set; }
        public int FRentBeginPrice { get; set; }
        public string FPriceType { get; set; }
        public int FRentStepPrice { get; set; }
        public int FDepositAmount { get; set; }
        public int FLogisticsCount { get; set; }
        public int FRequestMinPackQty { get; set; }
        public int FMinRequestQty { get; set; }
        public FRetailUnitID FRetailUnitID { get; set; }
        public string FIsPrinttAg { get; set; }
        public string FIsAccessory { get; set; }
    }

    public class FSupplySourceId
    {
        public string FNumber { get; set; }
    }

    public class FTaxCategoryCodeId
    {
        public string FNUMBER { get; set; }
    }

    public class FTaxRateId
    {
        public string FNUMBER { get; set; }
    }

    public class FTaxType
    {
        public string FNumber { get; set; }
    }

    public class FTimeFactorId
    {
        public string FNumber { get; set; }
    }

    public class FUnitIdCMK
    {
        public string FNUMBER { get; set; }
    }

    public class FVOLUMEUNITID
    {
        public string FNUMBER { get; set; }
    }

    public class FWEIGHTUNITID
    {
        public string FNUMBER { get; set; }
    }

    public class FWorkShopId
    {
        public string FNumber { get; set; }
    }

    //物料
    public class Model
    {
        public int FMATERIALID { get; set; }
        public string FNumber { get; set; }
        public string FName { get; set; }
        public string FSpecification { get; set; }
        public string FMnemonicCode { get; set; }
        public string FOldNumber { get; set; }
        public string FDescription { get; set; }
        public FMaterialGroup FMaterialGroup { get; set; }
        public string FDSMatchByLot { get; set; }
        public string FImgStorageType { get; set; }
        public string FIsSalseByNet { get; set; }
        public string FForbidReson { get; set; }
        public string FExtVar { get; set; }
        public string F_TBIB_Combo { get; set; }
        //public F_TBIB_Combo F_TBIB_Combo { get; set; }
        public string F_TBIB_Remark { get; set; }
        public string F_TBIB_Attachment { get; set; }
        public string F_TBIB_Text { get; set; }
        public FSubHeadEntity FSubHeadEntity { get; set; }
        public SubHeadEntity SubHeadEntity { get; set; }
        public SubHeadEntity1 SubHeadEntity1 { get; set; }
        public SubHeadEntity2 SubHeadEntity2 { get; set; }
        public SubHeadEntity3 SubHeadEntity3 { get; set; }
        public SubHeadEntity4 SubHeadEntity4 { get; set; }
        public SubHeadEntity5 SubHeadEntity5 { get; set; }
        public SubHeadEntity7 SubHeadEntity7 { get; set; }
        public SubHeadEntity6 SubHeadEntity6 { get; set; }
        public List<FBarCodeEntityCMK> FBarCodeEntity_CMK { get; set; }
        public List<FSpecialAttributeEntity> FSpecialAttributeEntity { get; set; }
        public List<FEntityAuxPty> FEntityAuxPty { get; set; }
        public List<FEntityInvPty> FEntityInvPty { get; set; }
    }

    public class PartDataModel:ERPDataModel
    {
        public Model Model { get; set; }
    }

    //基本
    public class SubHeadEntity
    {
        public int FEntryId { get; set; }
        public string FBARCODE { get; set; }
        public string FErpClsID { get; set; }
        public string FFeatureItem { get; set; }
        public string FCONFIGTYPE { get; set; }
        public FCategoryID FCategoryID { get; set; }
        public FTaxType FTaxType { get; set; }
        public FTaxRateId FTaxRateId { get; set; }
        public FBaseUnitId FBaseUnitId { get; set; }
        public string FIsPurchase { get; set; }
        public string FIsInventory { get; set; }
        public string FIsSubContract { get; set; }
        public string FIsSale { get; set; }
        public string FIsProduce { get; set; }
        public string FIsAsset { get; set; }
        public int FGROSSWEIGHT { get; set; }
        public int FNETWEIGHT { get; set; }
        public FWEIGHTUNITID FWEIGHTUNITID { get; set; }
        public int FLENGTH { get; set; }
        public int FWIDTH { get; set; }
        public int FHEIGHT { get; set; }
        public int FVOLUME { get; set; }
        public FVOLUMEUNITID FVOLUMEUNITID { get; set; }
        public string FSuite { get; set; }
        public int FCostPriceRate { get; set; }
    }

    public class F_TBIB_Combo {
        public string FNumber { get; set; }
    }

    //库存
    public class SubHeadEntity1
    {
        public int FEntryId { get; set; }
        public FStoreUnitID FStoreUnitID { get; set; }
        public FAuxUnitID FAuxUnitID { get; set; }
        public string FUnitConvertDir { get; set; }
        public FStockId FStockId { get; set; }
        public FStockPlaceId FStockPlaceId { get; set; }
        public string FIsLockStock { get; set; }
        public string FIsCycleCounting { get; set; }
        public string FCountCycle { get; set; }
        public int FCountDay { get; set; }
        public string FIsMustCounting { get; set; }
        public string FIsBatchManage { get; set; }
        public FBatchRuleID FBatchRuleID { get; set; }
        public string FIsKFPeriod { get; set; }
        public string FIsExpParToFlot { get; set; }
        public string FExpUnit { get; set; }
        public int FExpPeriod { get; set; }
        public int FOnlineLife { get; set; }
        public int FRefCost { get; set; }
        public FCurrencyId FCurrencyId { get; set; }
        public string FIsEnableMinStock { get; set; }
        public string FIsEnableMaxStock { get; set; }
        public string FIsEnableSafeStock { get; set; }
        public string FIsEnableReOrder { get; set; }
        public int FMinStock { get; set; }
        public int FSafeStock { get; set; }
        public int FReOrderGood { get; set; }
        public int FEconReOrderQty { get; set; }
        public int FMaxStock { get; set; }
        public string FIsSNManage { get; set; }
        public string FIsSNPRDTracy { get; set; }
        public FSNCodeRule FSNCodeRule { get; set; }
        public FSNUnit FSNUnit { get; set; }
        public string FSNManageType { get; set; }
        public string FSNGenerateTime { get; set; }
        public int FBoxStandardQty { get; set; }
    }

    //销售
    public class SubHeadEntity2
    {
        public int FEntryId { get; set; }
        public FSaleUnitId FSaleUnitId { get; set; }
        public FSalePriceUnitId FSalePriceUnitId { get; set; }
        public int FOrderQty { get; set; }
        public int FMinQty { get; set; }
        public int FMaxQty { get; set; }
        public int FOutStockLmtH { get; set; }
        public int FOutStockLmtL { get; set; }
        public int FAgentSalReduceRate { get; set; }
        public string FIsATPCheck { get; set; }
        public string FIsReturnPart { get; set; }
        public string FIsInvoice { get; set; }
        public string FIsReturn { get; set; }
        public string FAllowPublish { get; set; }
        public string FISAFTERSALE { get; set; }
        public string FISPRODUCTFILES { get; set; }
        public string FISWARRANTED { get; set; }
        public int FWARRANTY { get; set; }
        public string FWARRANTYUNITID { get; set; }
        public string FOutLmtUnit { get; set; }
        public FTaxCategoryCodeId FTaxCategoryCodeId { get; set; }
        public FSalGroup FSalGroup { get; set; }
        public string FIsTaxEnjoy { get; set; }
        public string FTaxDiscountsType { get; set; }
        public string FUnValidateExpQty { get; set; }
    }

    //采购
    public class SubHeadEntity3
    {
        public int FEntryId { get; set; }
        public int FBaseMinSplitQty { get; set; }
        public FPurchaseUnitId FPurchaseUnitId { get; set; }
        public FPurchasePriceUnitId FPurchasePriceUnitId { get; set; }
        public FPurchaseGroupId FPurchaseGroupId { get; set; }
        public FPurchaserId FPurchaserId { get; set; }
        public FDefaultVendor FDefaultVendor { get; set; }
        public FChargeID FChargeID { get; set; }
        public string FIsQuota { get; set; }
        public string FQuotaType { get; set; }
        public int FMinSplitQty { get; set; }
        public string FIsVmiBusiness { get; set; }
        public string FEnableSL { get; set; }
        public string FIsPR { get; set; }
        public string FIsReturnMaterial { get; set; }
        public string FIsSourceControl { get; set; }
        public int FReceiveMaxScale { get; set; }
        public int FReceiveMinScale { get; set; }
        public int FReceiveAdvanceDays { get; set; }
        public int FReceiveDelayDays { get; set; }
        public FPOBillTypeId FPOBillTypeId { get; set; }
        public int FAgentPurPlusRate { get; set; }
        public FDefBarCodeRuleId FDefBarCodeRuleId { get; set; }
        public int FPrintCount { get; set; }
        public int FMinPackCount { get; set; }
        public int FDailyOutQtySub { get; set; }
        public FDefaultLineIdSub FDefaultLineIdSub { get; set; }
        public string FIsEnableScheduleSub { get; set; }
    }

    //计划
    public class SubHeadEntity4
    {
        public int FEntryId { get; set; }
        public string FPlanMode { get; set; }
        public int FBaseVarLeadTimeLotSize { get; set; }
        public string FPlanningStrategy { get; set; }
        public FMfgPolicyId FMfgPolicyId { get; set; }
        public string FOrderPolicy { get; set; }
        public FPlanWorkshop FPlanWorkshop { get; set; }
        public int FFixLeadTime { get; set; }
        public string FFixLeadTimeType { get; set; }
        public int FVarLeadTime { get; set; }
        public string FVarLeadTimeType { get; set; }
        public int FCheckLeadTime { get; set; }
        public string FCheckLeadTimeType { get; set; }
        public string FOrderIntervalTimeType { get; set; }
        public int FOrderIntervalTime { get; set; }
        public int FMaxPOQty { get; set; }
        public int FMinPOQty { get; set; }
        public int FIncreaseQty { get; set; }
        public int FEOQ { get; set; }
        public int FVarLeadTimeLotSize { get; set; }
        public int FPlanIntervalsDays { get; set; }
        public int FPlanBatchSplitQty { get; set; }
        public int FRequestTimeZone { get; set; }
        public int FPlanTimeZone { get; set; }
        public FPlanGroupId FPlanGroupId { get; set; }
        public FATOSchemeId FATOSchemeId { get; set; }
        public FPlanerID FPlanerID { get; set; }
        public string FIsMrpComBill { get; set; }
        public int FCanLeadDays { get; set; }
        public string FIsMrpComReq { get; set; }
        public int FLeadExtendDay { get; set; }
        public string FReserveType { get; set; }
        public int FPlanSafeStockQty { get; set; }
        public string FAllowPartAhead { get; set; }
        public int FCanDelayDays { get; set; }
        public int FDelayExtendDay { get; set; }
        public string FAllowPartDelay { get; set; }
        public string FPlanOffsetTimeType { get; set; }
        public int FPlanOffsetTime { get; set; }
        public FSupplySourceId FSupplySourceId { get; set; }
        public FTimeFactorId FTimeFactorId { get; set; }
        public FQtyFactorId FQtyFactorId { get; set; }
        public FProductLine FProductLine { get; set; }
        public int FWriteOffQty { get; set; }
        public FPlanIdent FPlanIdent { get; set; }
        public FProScheTrackId FProScheTrackId { get; set; }
        public int FDailyOutQty { get; set; }
    }

    //生产
    public class SubHeadEntity5
    {
        public int FEntryId { get; set; }
        public FWorkShopId FWorkShopId { get; set; }
        public FProduceUnitId FProduceUnitId { get; set; }
        public int FFinishReceiptOverRate { get; set; }
        public int FFinishReceiptShortRate { get; set; }
        public FProduceBillType FProduceBillType { get; set; }
        public FOrgTrustBillType FOrgTrustBillType { get; set; }
        public string FIsSNCarryToParent { get; set; }
        public string FIsProductLine { get; set; }
        public FBOMUnitId FBOMUnitId { get; set; }
        public int FLOSSPERCENT { get; set; }
        public int FConsumVolatility { get; set; }
        public string FIsMainPrd { get; set; }
        public string FIsCoby { get; set; }
        public string FIsECN { get; set; }
        public string FIssueType { get; set; }
        public string FBKFLTime { get; set; }
        public FPickStockId FPickStockId { get; set; }
        public FPickBinId FPickBinId { get; set; }
        public string FOverControlMode { get; set; }
        public int FMinIssueQty { get; set; }
        public int FISMinIssueQty { get; set; }
        public string FIsKitting { get; set; }
        public string FIsCompleteSet { get; set; }
        public FDefaultRouting FDefaultRouting { get; set; }
        public int FStdLaborPrePareTime { get; set; }
        public int FStdLaborProcessTime { get; set; }
        public int FStdMachinePrepareTime { get; set; }
        public int FStdMachineProcessTime { get; set; }
        public FMinIssueUnitId FMinIssueUnitId { get; set; }
        public FMdlId FMdlId { get; set; }
        public FMdlMaterialId FMdlMaterialId { get; set; }
        public string FStandHourUnitId { get; set; }
        public string FBackFlushType { get; set; }
        public int FFIXLOSS { get; set; }
        public string FIsEnableSchedule { get; set; }
        public FDefaultLineId FDefaultLineId { get; set; }
    }

    //质量
    public class SubHeadEntity6
    {
        public int FEntryId { get; set; }
        public string FCheckIncoming { get; set; }
        public string FCheckProduct { get; set; }
        public string FCheckStock { get; set; }
        public string FCheckReturn { get; set; }
        public string FCheckDelivery { get; set; }
        public string FEnableCyclistQCSTK { get; set; }
        public int FStockCycle { get; set; }
        public string FEnableCyclistQCSTKEW { get; set; }
        public int FEWLeadDay { get; set; }
        public FIncSampSchemeId FIncSampSchemeId { get; set; }
        public FIncQcSchemeId FIncQcSchemeId { get; set; }
        public FInspectGroupId FInspectGroupId { get; set; }
        public FInspectorId FInspectorId { get; set; }
        public string FCheckEntrusted { get; set; }
        public string FCheckOther { get; set; }
        public string FIsFirstInspect { get; set; }
        public string FCheckReturnMtrl { get; set; }
        public string FCheckSubRtnMtrl { get; set; }
    }

    //委外
    public class SubHeadEntity7
    {
        public int FEntryId { get; set; }
        public FSubconUnitId FSubconUnitId { get; set; }
        public FSubconPriceUnitId FSubconPriceUnitId { get; set; }
        public FSubBillType FSubBillType { get; set; }
    }


}
