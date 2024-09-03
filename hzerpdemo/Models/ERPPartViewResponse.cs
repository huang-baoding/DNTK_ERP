
using System.Collections.Generic;
using System;
using System.Text.Json.Nodes;


namespace hzerpdemo.Models
{
    public class ERPPartResponse
    {
        public ResultModel Result { get; set; }
    }

    public class ResultModel
    {
        public ResponseStatusmodel ResponseStatus { get; set; }
        public PartViewResModel Result { get; set; }
    }

    public class ResponseStatusmodel
    {
        public bool IsSuccess { get; set; }
    }

    public class PartApproverId
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserAccount { get; set; }
    }

    public class PartBaseUnitId
    {
        public int Id { get; set; }
        public int msterID { get; set; }
        public List<PartMultiLanguageText> MultiLanguageText { get; set; }
        public List<Name> Name { get; set; }
        public string Number { get; set; }
        public bool IsBaseUnit { get; set; }
        public int UnitGroupId_Id { get; set; }
        public UnitGroupId UnitGroupId { get; set; }
        public int Precision { get; set; }
        public string RoundType { get; set; }
        public List<UNITCONVERTRATE> UNITCONVERTRATE { get; set; }
    }

    public class BatchRuleID
    {
        public int Id { get; set; }
        public int msterID { get; set; }
        public List<PartMultiLanguageText> MultiLanguageText { get; set; }
        public List<Name> Name { get; set; }
        public string Number { get; set; }
    }

    public class BOMViewUnitId
    {
        public int Id { get; set; }
        public int msterID { get; set; }
        public List<PartMultiLanguageText> MultiLanguageText { get; set; }
        public List<Name> Name { get; set; }
        public string Number { get; set; }
        public bool IsBaseUnit { get; set; }
        public int UnitGroupId_Id { get; set; }
        public UnitGroupId UnitGroupId { get; set; }
        public int Precision { get; set; }
        public string RoundType { get; set; }
        public List<UNITCONVERTRATE> UNITCONVERTRATE { get; set; }
    }

    public class CategoryID
    {
        public int Id { get; set; }
        public int msterID { get; set; }
        public List<PartMultiLanguageText> MultiLanguageText { get; set; }
        public List<Name> Name { get; set; }
        public string Number { get; set; }
    }

    //public class CreateOrgId
    //{
    //    public int Id { get; set; }
    //    public List<MultiLanguageText> MultiLanguageText { get; set; }
    //    public List<Name> Name { get; set; }
    //    public string Number { get; set; }
    //}

    //public class CreatorId
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //    public string UserAccount { get; set; }
    //}

    public class CurrencyId
    {
        public int Id { get; set; }
        public int msterID { get; set; }
        public List<PartMultiLanguageText> MultiLanguageText { get; set; }
        public List<Name> Name { get; set; }
        public string Number { get; set; }
        public string Sysmbol { get; set; }
        public int PriceDigits { get; set; }
        public int AmountDigits { get; set; }
        public bool IsShowCSymbol { get; set; }
        public string FormatOrder { get; set; }
        public string RoundType { get; set; }
    }

    public class DefStockStatusId
    {
        public int Id { get; set; }
        public int msterID { get; set; }
        public string Number { get; set; }
        public List<PartMultiLanguageText> MultiLanguageText { get; set; }
        public List<Name> Name { get; set; }
    }

    //public class Description
    //{
    //    public int Key { get; set; }
    //    public string Value { get; set; }
    //}

    public class DestUnitId
    {
        public int Id { get; set; }
        public int msterID { get; set; }
        public string Number { get; set; }
        public List<PartMultiLanguageText> MultiLanguageText { get; set; }
        public List<Name> Name { get; set; }
    }

    public class FBusinessTypeCMK
    {
        public string Id { get; set; }
        public string FNumber { get; set; }
        public List<PartMultiLanguageText> MultiLanguageText { get; set; }
        public List<FDataValue> FDataValue { get; set; }
    }

    public class FComTypeIdCMK
    {
        public int Id { get; set; }
        public List<PartMultiLanguageText> MultiLanguageText { get; set; }
        public List<Name> Name { get; set; }
        public string Number { get; set; }
        public string FBarCodeHeader { get; set; }
    }

    public class FDataValue
    {
        public int Key { get; set; }
        public string Value { get; set; }
    }

    public class FORBIDREASON
    {
        public int Key { get; set; }
        public string Value { get; set; }
    }

    public class FSpecialAttributeEntityModel
    {
        public int Id { get; set; }
        public int Seq { get; set; }
        public int FSpecAttrCategoryID_Id { get; set; }
        public object FSpecAttrCategoryID { get; set; }
        public int FSpecialAttributeID_Id { get; set; }
        public object FSpecialAttributeID { get; set; }
    }

    //public class InvPtyId
    //{
    //    public int Id { get; set; }
    //    public int msterID { get; set; }
    //    public string Number { get; set; }
    //    public List<PartMultiLanguageText> MultiLanguageText { get; set; }
    //    public List<Name> Name { get; set; }
    //}

    public class PartMaterialBase
    {
        public int Id { get; set; }
        public string ErpClsID { get; set; }
        public bool IsInventory { get; set; }
        public bool IsSale { get; set; }
        public bool IsAsset { get; set; }
        public bool IsSubContract { get; set; }
        public bool IsProduce { get; set; }
        public bool IsPurchase { get; set; }
        public bool IsRealTimeAccout { get; set; }
        public int BaseUnitId_Id { get; set; }
        public PartBaseUnitId BaseUnitId { get; set; }
        public string TaxType_Id { get; set; }
        public TaxType TaxType { get; set; }
        public int TypeID_Id { get; set; }
        public object TypeID { get; set; }
        public int CategoryID_Id { get; set; }
        public CategoryID CategoryID { get; set; }
        public int TaxRateId_Id { get; set; }
        public TaxRateId TaxRateId { get; set; }
        public object BARCODE { get; set; }
        public object WEIGHTUNITID { get; set; }
        public int VOLUMEUNITID_Id { get; set; }
        public object VOLUMEUNITID { get; set; }
        public double GROSSWEIGHT { get; set; }
        public double NETWEIGHT { get; set; }
        public double LENGTH { get; set; }
        public double VOLUME { get; set; }
        public double WIDTH { get; set; }
        public double HEIGHT { get; set; }
        public string CONFIGTYPE { get; set; }
        public string Suite { get; set; }
        public double CostPriceRate { get; set; }
        public string FNameEn { get; set; }
        public string FSysModel { get; set; }
        public string FColor { get; set; }
        public string FSpreadName { get; set; }
        public string FMAKEINVOICEPARTY { get; set; }
        public string FeatureItem { get; set; }
        public int UseOrgId1_Id { get; set; }
        public UseOrgId1 UseOrgId1 { get; set; }
        public bool IsChange { get; set; }
    }

    public class MaterialCMK
    {
        public int Id { get; set; }
        public int FComTypeId_CMK_Id { get; set; }
        public FComTypeIdCMK FComTypeId_CMK { get; set; }
        public string FBarCodeHeader_CMK { get; set; }
        public string FGoodBarCode_CMK { get; set; }
        public int FComBrandId_CMK_Id { get; set; }
        public object FComBrandId_CMK { get; set; }
        public string FBusinessType_CMK_Id { get; set; }
        public FBusinessTypeCMK FBusinessType_CMK { get; set; }
        public string FSellMethod_CMK_Id { get; set; }
        public object FSellMethod_CMK { get; set; }
        public int FCurrencyId_CMK_Id { get; set; }
        public object FCurrencyId_CMK { get; set; }
        public string FSaleStatus_CMK { get; set; }
        public string FPurStatus_CMK { get; set; }
        public double FPurPrice_CMK { get; set; }
        public double FSalePrice_CMK { get; set; }
        public double FVIPPrice_CMK { get; set; }
        public double FPointsRate_CMK { get; set; }
        public string FImgFile_CMK { get; set; }
        public int FShoppeID_CMK_Id { get; set; }
        public object FShoppeID_CMK { get; set; }
        public double FProPrice { get; set; }
        public int FMaterialSource_Id { get; set; }
        public object FMaterialSource { get; set; }
        public bool IsControlSal { get; set; }
        public double UpPercent { get; set; }
        public double LowerPercent { get; set; }
        public string CalculateBase { get; set; }
        public double MaxSalPrice_CMK { get; set; }
        public double MinSalPrice_CMK { get; set; }
        public bool IsAutoRemove { get; set; }
        public bool IsMailVirtual { get; set; }
        public string IsFreeSend { get; set; }
        public object PackageMail { get; set; }
        public object FreightTem { get; set; }
        public string PriceType { get; set; }
        public object PerUnit { get; set; }
        public object ByVolume { get; set; }
        public object ByWeight { get; set; }
        public double LogisticsCount { get; set; }
        public double RequestMinPackQty { get; set; }
        public double MinRequestQty { get; set; }
        public int RetailUnitID_Id { get; set; }
        public object RetailUnitID { get; set; }
        public bool IsPrinttAg { get; set; }
        public bool FIsAutoRemove1 { get; set; }
        public string FTimeUnit { get; set; }
        public double FMinRentDura { get; set; }
        public double FRentBeginPrice { get; set; }
        public double FPricingStep { get; set; }
        public double FRentStepPrice { get; set; }
        public double FRentFreeDura { get; set; }
        public double FDepositAmount { get; set; }
    }

    //public class MaterialGroup
    //{
    //    public int Id { get; set; }
    //    public string Number { get; set; }
    //    public List<PartMultiLanguageText> MultiLanguageText { get; set; }
    //    public List<Name> Name { get; set; }
    //}

    public class PartMaterialInvPty
    {
        public int Id { get; set; }
        public bool IsEnable { get; set; }
        public bool IsAffectPrice { get; set; }
        public bool IsAffectPlan { get; set; }
        public bool IsAffectCost { get; set; }
        public int InvPtyId_Id { get; set; }
        public InvPtyId InvPtyId { get; set; }
        public int UseOrgId10_Id { get; set; }
        public UseOrgId10 UseOrgId10 { get; set; }
    }

    public class PartMaterialPlan
    {
        public int Id { get; set; }
        public int PlanerID_Id { get; set; }
        public object PlanerID { get; set; }
        public double EOQ { get; set; }
        public string PlanningStrategy { get; set; }
        public string OrderPolicy { get; set; }
        public int PlanWorkshop_Id { get; set; }
        public object PlanWorkshop { get; set; }
        public string FixLeadTimeType { get; set; }
        public int FixLeadTime { get; set; }
        public string VarLeadTimeType { get; set; }
        public int VarLeadTime { get; set; }
        public string CheckLeadTimeType { get; set; }
        public int CheckLeadTime { get; set; }
        public string OrderIntervalTimeType { get; set; }
        public int OrderIntervalTime { get; set; }
        public int PlanIntervalsDays { get; set; }
        public double PlanBatchSplitQty { get; set; }
        public int PlanTimeZone { get; set; }
        public int RequestTimeZone { get; set; }
        public bool IsMrpComReq { get; set; }
        public string ReserveType { get; set; }
        public int CanLeadDays { get; set; }
        public int LeadExtendDay { get; set; }
        public int DelayExtendDay { get; set; }
        public int CanDelayDays { get; set; }
        public string PlanOffsetTimeType { get; set; }
        public int PlanOffsetTime { get; set; }
        public double MinPOQty { get; set; }
        public double IncreaseQty { get; set; }
        public double MaxPOQty { get; set; }
        public double VarLeadTimeLotSize { get; set; }
        public double BaseVarLeadTimeLotSize { get; set; }
        public int PlanGroupId_Id { get; set; }
        public object PlanGroupId { get; set; }
        public int MfgPolicyId_Id { get; set; }
        public MfgPolicyId MfgPolicyId { get; set; }
        public int SupplySourceId_Id { get; set; }
        public object SupplySourceId { get; set; }
        public int TimeFactorId_Id { get; set; }
        public object TimeFactorId { get; set; }
        public int QtyFactorId_Id { get; set; }
        public object QtyFactorId { get; set; }
        public string PlanMode { get; set; }
        public bool AllowPartDelay { get; set; }
        public bool AllowPartAhead { get; set; }
        public double PLANSAFESTOCKQTY { get; set; }
        public int ATOSchemeId_Id { get; set; }
        public object ATOSchemeId { get; set; }
        public int AccuLeadTime { get; set; }
        public int ProductLine_Id { get; set; }
        public object ProductLine { get; set; }
        public double WriteOffQty { get; set; }
        public string PlanIdent_Id { get; set; }
        public object PlanIdent { get; set; }
        public string ProScheTrackId_Id { get; set; }
        public object ProScheTrackId { get; set; }
        public double DailyOutQty { get; set; }
        public bool IsMrpComBill { get; set; }
        public int UseOrgId7_Id { get; set; }
        public UseOrgId7 UseOrgId7 { get; set; }
    }

    public class PartMaterialProduce
    {
        public int Id { get; set; }
        public int PickStockId_Id { get; set; }
        public PartPickStockId PickStockId { get; set; }
        public int BOMUnitId_Id { get; set; }
        public BOMUnitId BOMUnitId { get; set; }
        public int WorkShopId_Id { get; set; }
        public object WorkShopId { get; set; }
        public string IssueType { get; set; }
        public int ProduceUnitId_Id { get; set; }
        public PartProduceUnitId ProduceUnitId { get; set; }
        public bool IsKitting { get; set; }
        public int DefaultRouting_Id { get; set; }
        public object DefaultRouting { get; set; }
        public bool IsCoby { get; set; }
        public double PerUnitStandHour { get; set; }
        public string BKFLTime { get; set; }
        public double FinishReceiptOverRate { get; set; }
        public double FinishReceiptShortRate { get; set; }
        public int PickBinId_Id { get; set; }
        public object PickBinId { get; set; }
        public double PrdURNum { get; set; }
        public double PrdURNom { get; set; }
        public double BOMURNum { get; set; }
        public double BOMURNom { get; set; }
        public bool IsMainPrd { get; set; }
        public bool IsCompleteSet { get; set; }
        public string OverControlMode { get; set; }
        public double MinIssueQty { get; set; }
        public double StdLaborPrePareTime { get; set; }
        public double StdLaborProcessTime { get; set; }
        public double StdMachinePrepareTime { get; set; }
        public double StdMachineProcessTime { get; set; }
        public double ConsumVolatility { get; set; }
        public bool IsProductLine { get; set; }
        public string ProduceBillType_Id { get; set; }
        public ProduceBillType ProduceBillType { get; set; }
        public string OrgTrustBillType_Id { get; set; }
        public object OrgTrustBillType { get; set; }
        public bool ISMinIssueQty { get; set; }
        public bool IsECN { get; set; }
        public int MinIssueUnitId_Id { get; set; }
        public PartMinIssueUnitId MinIssueUnitId { get; set; }
        public int MDLID_Id { get; set; }
        public object MDLID { get; set; }
        public int MdlMaterialId_Id { get; set; }
        public object MdlMaterialId { get; set; }
        public double LossPercent { get; set; }
        public bool IsSNCarryToParent { get; set; }
        public string StandHourUnitId { get; set; }
        public string BackFlushType { get; set; }
        public double FIXLOSS { get; set; }
        public int UseOrgId6_Id { get; set; }
        public UseOrgId6 UseOrgId6 { get; set; }
        public bool IsEnableSchedule { get; set; }
        public int DefaultLineId_Id { get; set; }
        public object DefaultLineId { get; set; }
    }

    public class PartMaterialPurchase
    {
        public int Id { get; set; }
        public int PurchaseUnitID_Id { get; set; }
        public PurchaseUnitID PurchaseUnitID { get; set; }
        public int PurchaserId_Id { get; set; }
        public object PurchaserId { get; set; }
        public int DefaultVendor_Id { get; set; }
        public object DefaultVendor { get; set; }
        public bool IsSourceControl { get; set; }
        public bool IsPR { get; set; }
        public double ReceiveMinScale { get; set; }
        public int PurchaseGroupId_Id { get; set; }
        public object PurchaseGroupId { get; set; }
        public double ReceiveMaxScale { get; set; }
        public int PurchasePriceUnitId_Id { get; set; }
        public PurchasePriceUnitId PurchasePriceUnitId { get; set; }
        public bool IsVendorQualification { get; set; }
        public int ReceiveAdvanceDays { get; set; }
        public int ReceiveDelayDays { get; set; }
        public double PurURNum { get; set; }
        public double PurPriceURNum { get; set; }
        public double PurURNom { get; set; }
        public double PurPriceURNom { get; set; }
        public bool IsQuota { get; set; }
        public string QuotaType { get; set; }
        public double AgentPurPlusRate { get; set; }
        public int ChargeID_Id { get; set; }
        public object ChargeID { get; set; }
        public double MinSplitQty { get; set; }
        public double BaseMinSplitQty { get; set; }
        public bool IsVmiBusiness { get; set; }
        public bool IsReturnMaterial { get; set; }
        public bool EnableSL { get; set; }
        public int PurchaseOrgId_Id { get; set; }
        public PurchaseOrgId PurchaseOrgId { get; set; }
        public int DefBarCodeRuleId_Id { get; set; }
        public object DefBarCodeRuleId { get; set; }
        public int PrintCount { get; set; }
        public string POBillTypeId_Id { get; set; }
        public POBillTypeId POBillTypeId { get; set; }
        public double MinPackCount { get; set; }
        public int UseOrgId4_Id { get; set; }
        public UseOrgId4 UseOrgId4 { get; set; }
        public double DailyOutQtySub { get; set; }
        public int DefaultLineIdSub_Id { get; set; }
        public object DefaultLineIdSub { get; set; }
        public bool IsEnableScheduleSub { get; set; }
    }

    public class PartMaterialQM
    {
        public int Id { get; set; }
        public bool CheckProduct { get; set; }
        public bool CheckIncoming { get; set; }
        public int IncSampSchemeId_Id { get; set; }
        public object IncSampSchemeId { get; set; }
        public int IncQcSchemeId_Id { get; set; }
        public object IncQcSchemeId { get; set; }
        public bool CheckStock { get; set; }
        public bool EnableCyclistQCSTK { get; set; }
        public bool EnableCyclistQCSTKEW { get; set; }
        public int EWLeadDay { get; set; }
        public int StockCycle { get; set; }
        public bool CheckDelivery { get; set; }
        public bool CheckReturn { get; set; }
        public int InspectGroupId_Id { get; set; }
        public object InspectGroupId { get; set; }
        public int InspectorId_Id { get; set; }
        public object InspectorId { get; set; }
        public bool CheckEntrusted { get; set; }
        public bool CheckOther { get; set; }
        public bool IsFirstInspect { get; set; }
        public int UseOrgId5_Id { get; set; }
        public UseOrgId5 UseOrgId5 { get; set; }
        public bool CheckReturnMtrl { get; set; }
        public bool CheckSubRtnMtrl { get; set; }
    }

    public class MaterialSale
    {
        public int Id { get; set; }
        public bool IsATPCheck { get; set; }
        public int SalePriceUnitId_Id { get; set; }
        public SalePriceUnitId SalePriceUnitId { get; set; }
        public int SaleUnitId_Id { get; set; }
        public SaleUnitId SaleUnitId { get; set; }
        public bool IsInvoice { get; set; }
        public double MaxQty { get; set; }
        public bool IsReturn { get; set; }
        public double MinQty { get; set; }
        public bool IsReturnPart { get; set; }
        public double OrderQty { get; set; }
        public double SalePriceURNom { get; set; }
        public double SaleURNum { get; set; }
        public double SalePriceURNum { get; set; }
        public double SaleURNom { get; set; }
        public double OutStockLmtH { get; set; }
        public double OutStockLmtL { get; set; }
        public double AgentSalReduceRate { get; set; }
        public bool AllowPublish { get; set; }
        public bool ISAFTERSALE { get; set; }
        public bool ISPRODUCTFILES { get; set; }
        public bool ISWARRANTED { get; set; }
        public int FWARRANTY { get; set; }
        public string WARRANTYUNITID { get; set; }
        public string OutLmtUnit { get; set; }
        public int TaxCategoryCodeId_Id { get; set; }
        public object TaxCategoryCodeId { get; set; }
        public int SalGroup_Id { get; set; }
        public object SalGroup { get; set; }
        public string TaxDiscountsType { get; set; }
        public bool IsTaxEnjoy { get; set; }
        public int UseOrgId3_Id { get; set; }
        public UseOrgId3 UseOrgId3 { get; set; }
        public bool UnValidateExpQty { get; set; }
    }

    public class PartMaterialStock
    {
        public int Id { get; set; }
        public int StoreUnitID_Id { get; set; }
        public PartStoreUnitID StoreUnitID { get; set; }
        public int AuxUnitID_Id { get; set; }
        public object AuxUnitID { get; set; }
        public int StockId_Id { get; set; }
        public StockId StockId { get; set; }
        public bool IsLockStock { get; set; }
        public int BatchRuleID_Id { get; set; }
        public BatchRuleID BatchRuleID { get; set; }
        public string ExpUnit { get; set; }
        public int StockPlaceId_Id { get; set; }
        public object StockPlaceId { get; set; }
        public int OnlineLife { get; set; }
        public int ExpPeriod { get; set; }
        public double StoreURNum { get; set; }
        public double StoreURNom { get; set; }
        public bool IsBatchManage { get; set; }
        public bool IsKFPeriod { get; set; }
        public bool IsExpParToFlot { get; set; }
        public bool IsCycleCounting { get; set; }
        public bool IsMustCounting { get; set; }
        public int CurrencyId_Id { get; set; }
        public CurrencyId CurrencyId { get; set; }
        public double RefCost { get; set; }
        public string CountCycle { get; set; }
        public int CountDay { get; set; }
        public bool IsSNManage { get; set; }
        public int SNCodeRule_Id { get; set; }
        public object SNCodeRule { get; set; }
        public int SNUnit_Id { get; set; }
        public SNUnit SNUnit { get; set; }
        public double SafeStock { get; set; }
        public double ReOrderGood { get; set; }
        public double MinStock { get; set; }
        public double MaxStock { get; set; }
        public string UnitConvertDir { get; set; }
        public bool IsEnableMinStock { get; set; }
        public bool IsEnableSafeStock { get; set; }
        public bool IsEnableMaxStock { get; set; }
        public bool IsEnableReOrder { get; set; }
        public double EconReOrderQty { get; set; }
        public bool IsSNPRDTracy { get; set; }
        public string SNGenerateTime { get; set; }
        public string SNManageType { get; set; }
        public double BoxStandardQty { get; set; }
        public int UseOrgId2_Id { get; set; }
        public UseOrgId2 UseOrgId2 { get; set; }
    }

    public class MaterialSubcon
    {
        public int Id { get; set; }
        public int SubconUnitId_Id { get; set; }
        public SubconUnitId SubconUnitId { get; set; }
        public int SubconPriceUnitId_Id { get; set; }
        public SubconPriceUnitId SubconPriceUnitId { get; set; }
        public double SUBCONURNUM { get; set; }
        public double SUBCONURNOM { get; set; }
        public double SUBCONPRICEURNUM { get; set; }
        public double SUBCONPRICEURNOM { get; set; }
        public string SUBBILLTYPE_Id { get; set; }
        public SUBBILLTYPE SUBBILLTYPE { get; set; }
        public int UseOrgId8_Id { get; set; }
        public UseOrgId8 UseOrgId8 { get; set; }
    }

    public class PartMfgPolicyId
    {
        public int Id { get; set; }
        public List<PartMultiLanguageText> MultiLanguageText { get; set; }
        public List<Name> Name { get; set; }
        public string Number { get; set; }
        public string PlanMode { get; set; }
        public bool Ato { get; set; }
    }

    public class PartMinIssueUnitId
    {
        public int Id { get; set; }
        public int msterID { get; set; }
        public List<PartMultiLanguageText> MultiLanguageText { get; set; }
        public List<Name> Name { get; set; }
        public string Number { get; set; }
        public bool IsBaseUnit { get; set; }
        public int UnitGroupId_Id { get; set; }
        public UnitGroupId UnitGroupId { get; set; }
        public int Precision { get; set; }
        public string RoundType { get; set; }
        public List<UNITCONVERTRATE> UNITCONVERTRATE { get; set; }
    }

    //public class ModifierId
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //    public string UserAccount { get; set; }
    //}

    public class PartMultiLanguageText
    {
        public string PkId { get; set; }
        public int LocaleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Specification { get; set; }
        public string FORBIDREASON { get; set; }
        public string FDataValue { get; set; }
    }

    //public class Name
    //{
    //    public int Key { get; set; }
    //    public string Value { get; set; }
    //}

    public class PartPickStockId
    {
        public int Id { get; set; }
        public int msterID { get; set; }
        public List<PartMultiLanguageText> MultiLanguageText { get; set; }
        public List<Name> Name { get; set; }
        public string Number { get; set; }
        public bool IsOpenLocation { get; set; }
        public int DefStockStatusId_Id { get; set; }
        public DefStockStatusId DefStockStatusId { get; set; }
        public string LocListFormatter { get; set; }
        public List<object> StockFlexItem { get; set; }
    }

    public class POBillTypeId
    {
        public string Id { get; set; }
        public List<PartMultiLanguageText> MultiLanguageText { get; set; }
        public List<Name> Name { get; set; }
        public string Number { get; set; }
        public bool IsDefault { get; set; }
    }

    public class ProduceBillType
    {
        public string Id { get; set; }
        public List<PartMultiLanguageText> MultiLanguageText { get; set; }
        public List<Name> Name { get; set; }
        public string Number { get; set; }
        public bool IsDefault { get; set; }
    }

    public class PartProduceUnitId
    {
        public int Id { get; set; }
        public int msterID { get; set; }
        public List<PartMultiLanguageText> MultiLanguageText { get; set; }
        public List<Name> Name { get; set; }
        public string Number { get; set; }
        public bool IsBaseUnit { get; set; }
        public int UnitGroupId_Id { get; set; }
        public UnitGroupId UnitGroupId { get; set; }
        public int Precision { get; set; }
        public string RoundType { get; set; }
        public List<UNITCONVERTRATE> UNITCONVERTRATE { get; set; }
    }

    public class PurchaseOrgId
    {
        public int Id { get; set; }
        public List<PartMultiLanguageText> MultiLanguageText { get; set; }
        public List<Name> Name { get; set; }
        public string Number { get; set; }
    }

    public class PurchasePriceUnitId
    {
        public int Id { get; set; }
        public int msterID { get; set; }
        public List<PartMultiLanguageText> MultiLanguageText { get; set; }
        public List<Name> Name { get; set; }
        public string Number { get; set; }
        public bool IsBaseUnit { get; set; }
        public int UnitGroupId_Id { get; set; }
        public UnitGroupId UnitGroupId { get; set; }
        public int Precision { get; set; }
        public string RoundType { get; set; }
        public List<UNITCONVERTRATE> UNITCONVERTRATE { get; set; }
    }

    public class PurchaseUnitID
    {
        public int Id { get; set; }
        public int msterID { get; set; }
        public List<PartMultiLanguageText> MultiLanguageText { get; set; }
        public List<Name> Name { get; set; }
        public string Number { get; set; }
        public bool IsBaseUnit { get; set; }
        public int UnitGroupId_Id { get; set; }
        public UnitGroupId UnitGroupId { get; set; }
        public int Precision { get; set; }
        public string RoundType { get; set; }
        public List<UNITCONVERTRATE> UNITCONVERTRATE { get; set; }
    }

    public class PartViewResModel
    {
        public int Id { get; set; }
        public int msterID { get; set; }
        public string DocumentStatus { get; set; }
        public string ForbidStatus { get; set; }
        public List<PartMultiLanguageText> MultiLanguageText { get; set; }
        public List<Name> Name { get; set; }
        public string Number { get; set; }
        public List<Description> Description { get; set; }
        public int CreateOrgId_Id { get; set; }
        public CreateOrgId CreateOrgId { get; set; }
        public int UseOrgId_Id { get; set; }
        public UseOrgId UseOrgId { get; set; }
        public int CreatorId_Id { get; set; }
        public CreatorId CreatorId { get; set; }
        public int ModifierId_Id { get; set; }
        public ModifierId ModifierId { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string MnemonicCode { get; set; }
        public List<Specification> Specification { get; set; }
        public int ForbidderId_Id { get; set; }
        public object ForbidderId { get; set; }
        public object ForbidDate { get; set; }
        public DateTime? ApproveDate { get; set; }
        public int ApproverId_Id { get; set; }
        public PartApproverId ApproverId { get; set; }
        public object Image { get; set; }
        public string OldNumber { get; set; }
        public int MaterialGroup_Id { get; set; }
        public MaterialGroup MaterialGroup { get; set; }
        public object PLMMaterialId { get; set; }
        public string MaterialSRC { get; set; }
        public bool IsValidate { get; set; }
        public string ImageFileServer { get; set; }
        public string ImgStorageType { get; set; }
        public object IsImgDataBase { get; set; }
        public object IsImgFileServer { get; set; }
        public bool IsSalseByNet { get; set; }
        public bool FIsAutoAllocate { get; set; }
        public int FSPUID_Id { get; set; }
        public object FSPUID { get; set; }
        public string FPinYin { get; set; }
        public bool DSMatchByLot { get; set; }
        public List<FORBIDREASON> FORBIDREASON { get; set; }
        public string RefStatus { get; set; }
        public object ExtVar { get; set; }
        public string F_TBIB_Combo { get; set; }
        public string F_TBIB_Remark { get; set; }
        public string F_TBIB_Attachment { get; set; }
        public List<object> F_TBIB_Attachment_Files { get; set; }
        public string F_TBIB_Text { get; set; }
        public List<MaterialCMK> MaterialCMK { get; set; }
        public List<object> FBarCodeEntity_CMK { get; set; }
        public List<FSpecialAttributeEntityModel> FSpecialAttributeEntity { get; set; }
        public List<PartMaterialBase> MaterialBase { get; set; }
        public List<PartMaterialStock> MaterialStock { get; set; }
        public List<MaterialSale> MaterialSale { get; set; }
        public List<PartMaterialPurchase> MaterialPurchase { get; set; }
        public List<PartMaterialPlan> MaterialPlan { get; set; }
        public List<PartMaterialProduce> MaterialProduce { get; set; }
        public List<object> MaterialAuxPty { get; set; }
        public List<PartMaterialInvPty> MaterialInvPty { get; set; }
        public List<MaterialSubcon> MaterialSubcon { get; set; }
        public List<PartMaterialQM> MaterialQM { get; set; }
    }

    public class SalePriceUnitId
    {
        public int Id { get; set; }
        public int msterID { get; set; }
        public List<PartMultiLanguageText> MultiLanguageText { get; set; }
        public List<Name> Name { get; set; }
        public string Number { get; set; }
        public bool IsBaseUnit { get; set; }
        public int UnitGroupId_Id { get; set; }
        public UnitGroupId UnitGroupId { get; set; }
        public int Precision { get; set; }
        public string RoundType { get; set; }
        public List<UNITCONVERTRATE> UNITCONVERTRATE { get; set; }
    }

    public class SaleUnitId
    {
        public int Id { get; set; }
        public int msterID { get; set; }
        public List<PartMultiLanguageText> MultiLanguageText { get; set; }
        public List<Name> Name { get; set; }
        public string Number { get; set; }
        public bool IsBaseUnit { get; set; }
        public int UnitGroupId_Id { get; set; }
        public UnitGroupId UnitGroupId { get; set; }
        public int Precision { get; set; }
        public string RoundType { get; set; }
        public List<UNITCONVERTRATE> UNITCONVERTRATE { get; set; }
    }

    public class SNUnit
    {
        public int Id { get; set; }
        public int msterID { get; set; }
        public List<PartMultiLanguageText> MultiLanguageText { get; set; }
        public List<Name> Name { get; set; }
        public string Number { get; set; }
        public bool IsBaseUnit { get; set; }
        public int UnitGroupId_Id { get; set; }
        public UnitGroupId UnitGroupId { get; set; }
        public int Precision { get; set; }
        public string RoundType { get; set; }
        public List<UNITCONVERTRATE> UNITCONVERTRATE { get; set; }
    }

    //public class Specification
    //{
    //    public int Key { get; set; }
    //    public string Value { get; set; }
    //}

    public class StockId
    {
        public int Id { get; set; }
        public int msterID { get; set; }
        public List<PartMultiLanguageText> MultiLanguageText { get; set; }
        public List<Name> Name { get; set; }
        public string Number { get; set; }
        public bool IsOpenLocation { get; set; }
        public int DefStockStatusId_Id { get; set; }
        public DefStockStatusId DefStockStatusId { get; set; }
        public string LocListFormatter { get; set; }
        public List<object> StockFlexItem { get; set; }
    }

    public class PartStoreUnitID
    {
        public int Id { get; set; }
        public int msterID { get; set; }
        public List<PartMultiLanguageText> MultiLanguageText { get; set; }
        public List<Name> Name { get; set; }
        public string Number { get; set; }
        public bool IsBaseUnit { get; set; }
        public int UnitGroupId_Id { get; set; }
        public UnitGroupId UnitGroupId { get; set; }
        public int Precision { get; set; }
        public string RoundType { get; set; }
        public List<UNITCONVERTRATE> UNITCONVERTRATE { get; set; }
    }

    public class SUBBILLTYPE
    {
        public string Id { get; set; }
        public List<PartMultiLanguageText> MultiLanguageText { get; set; }
        public List<Name> Name { get; set; }
        public string Number { get; set; }
        public bool IsDefault { get; set; }
    }

    public class SubconPriceUnitId
    {
        public int Id { get; set; }
        public int msterID { get; set; }
        public List<PartMultiLanguageText> MultiLanguageText { get; set; }
        public List<Name> Name { get; set; }
        public string Number { get; set; }
        public bool IsBaseUnit { get; set; }
        public int UnitGroupId_Id { get; set; }
        public UnitGroupId UnitGroupId { get; set; }
        public int Precision { get; set; }
        public string RoundType { get; set; }
        public List<UNITCONVERTRATE> UNITCONVERTRATE { get; set; }
    }

    public class SubconUnitId
    {
        public int Id { get; set; }
        public int msterID { get; set; }
        public List<PartMultiLanguageText> MultiLanguageText { get; set; }
        public List<Name> Name { get; set; }
        public string Number { get; set; }
        public bool IsBaseUnit { get; set; }
        public int UnitGroupId_Id { get; set; }
        public UnitGroupId UnitGroupId { get; set; }
        public int Precision { get; set; }
        public string RoundType { get; set; }
        public List<UNITCONVERTRATE> UNITCONVERTRATE { get; set; }
    }

    public class TaxRateId
    {
        public int Id { get; set; }
        public int msterID { get; set; }
        public List<PartMultiLanguageText> MultiLanguageText { get; set; }
        public List<Name> Name { get; set; }
        public string Number { get; set; }
    }

    public class TaxType
    {
        public string Id { get; set; }
        public string FNumber { get; set; }
        public List<PartMultiLanguageText> MultiLanguageText { get; set; }
        public List<FDataValue> FDataValue { get; set; }
    }

    public class UNITCONVERTRATE
    {
        public int Id { get; set; }
        public string ConvertType { get; set; }
        public int DestUnitId_Id { get; set; }
        public DestUnitId DestUnitId { get; set; }
    }

    //public class UnitGroupId
    //{
    //    public int Id { get; set; }
    //    public int msterID { get; set; }
    //    public string Number { get; set; }
    //    public List<MultiLanguageText> MultiLanguageText { get; set; }
    //    public List<Name> Name { get; set; }
    //}

    //public class UseOrgId
    //{
    //    public int Id { get; set; }
    //    public List<MultiLanguageText> MultiLanguageText { get; set; }
    //    public List<Name> Name { get; set; }
    //    public string Number { get; set; }
    //}

    public class UseOrgId1
    {
        public int Id { get; set; }
        public List<PartMultiLanguageText> MultiLanguageText { get; set; }
        public List<Name> Name { get; set; }
        public string Number { get; set; }
    }

    public class UseOrgId10
    {
        public int Id { get; set; }
        public List<PartMultiLanguageText> MultiLanguageText { get; set; }
        public List<Name> Name { get; set; }
        public string Number { get; set; }
    }

    public class UseOrgId2
    {
        public int Id { get; set; }
        public List<PartMultiLanguageText> MultiLanguageText { get; set; }
        public List<Name> Name { get; set; }
        public string Number { get; set; }
    }

    public class UseOrgId3
    {
        public int Id { get; set; }
        public List<PartMultiLanguageText> MultiLanguageText { get; set; }
        public List<Name> Name { get; set; }
        public string Number { get; set; }
    }

    public class UseOrgId4
    {
        public int Id { get; set; }
        public List<PartMultiLanguageText> MultiLanguageText { get; set; }
        public List<Name> Name { get; set; }
        public string Number { get; set; }
    }

    public class UseOrgId5
    {
        public int Id { get; set; }
        public List<PartMultiLanguageText> MultiLanguageText { get; set; }
        public List<Name> Name { get; set; }
        public string Number { get; set; }
    }

    public class UseOrgId6
    {
        public int Id { get; set; }
        public List<PartMultiLanguageText> MultiLanguageText { get; set; }
        public List<Name> Name { get; set; }
        public string Number { get; set; }
    }

    public class UseOrgId7
    {
        public int Id { get; set; }
        public List<PartMultiLanguageText> MultiLanguageText { get; set; }
        public List<Name> Name { get; set; }
        public string Number { get; set; }
    }

    public class UseOrgId8
    {
        public int Id { get; set; }
        public List<PartMultiLanguageText> MultiLanguageText { get; set; }
        public List<Name> Name { get; set; }
        public string Number { get; set; }
    }


}
