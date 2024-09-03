using System.Collections.Generic;
using System;

namespace hzerpdemo.Models
{

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class ApproverId
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserAccount { get; set; }
    }

    public class BaseUnitId
    {
        public int Id { get; set; }
        public int msterID { get; set; }
        public string Number { get; set; }
        public List<MultiLanguageText> MultiLanguageText { get; set; }
        public List<Name> Name { get; set; }
        public int UnitGroupId_Id { get; set; }
        public UnitGroupId UnitGroupId { get; set; }
        public int Precision { get; set; }
        public string RoundType { get; set; }
        public List<BomUNITCONVERTRATE> UNITCONVERTRATE { get; set; }
    }

    public class BILLTYPE
    {
        public string Id { get; set; }
        public List<MultiLanguageText> MultiLanguageText { get; set; }
        public List<Name> Name { get; set; }
        public string Number { get; set; }
    }

    public class BOMUnitId
    {
        public int Id { get; set; }
        public int msterID { get; set; }
        public string Number { get; set; }
        public List<MultiLanguageText> MultiLanguageText { get; set; }
        public List<Name> Name { get; set; }
    }

    public class ChildBaseUnitID
    {
        public int Id { get; set; }
        public int msterID { get; set; }
        public List<MultiLanguageText> MultiLanguageText { get; set; }
        public List<Name> Name { get; set; }
        public string Number { get; set; }
        public int UnitGroupId_Id { get; set; }
        public UnitGroupId UnitGroupId { get; set; }
        public int Precision { get; set; }
        public string RoundType { get; set; }
        public List<BomUNITCONVERTRATE> UNITCONVERTRATE { get; set; }
    }

    public class ChildSupplyOrgId
    {
        public int Id { get; set; }
        public List<MultiLanguageText> MultiLanguageText { get; set; }
        public List<Name> Name { get; set; }
        public string Number { get; set; }
    }

    public class CHILDUNITID
    {
        public int Id { get; set; }
        public int msterID { get; set; }
        public List<MultiLanguageText> MultiLanguageText { get; set; }
        public List<Name> Name { get; set; }
        public string Number { get; set; }
        public int UnitGroupId_Id { get; set; }
        public UnitGroupId UnitGroupId { get; set; }
        public int Precision { get; set; }
        public string RoundType { get; set; }
        public List<BomUNITCONVERTRATE> UNITCONVERTRATE { get; set; }
    }

    public class CreateOrgId
    {
        public int Id { get; set; }
        public List<MultiLanguageText> MultiLanguageText { get; set; }
        public List<Name> Name { get; set; }
        public string Number { get; set; }
    }

    public class CreatorId
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserAccount { get; set; }
    }

    public class Description
    {
        public int Key { get; set; }
        public string Value { get; set; }
    }

    public class ForbidReson
    {
        public int Key { get; set; }
        public string Value { get; set; }
    }

    public class BomFUNITID
    {
        public int Id { get; set; }
        public int msterID { get; set; }
        public List<MultiLanguageText> MultiLanguageText { get; set; }
        public List<Name> Name { get; set; }
        public string Number { get; set; }
        public int UnitGroupId_Id { get; set; }
        public UnitGroupId UnitGroupId { get; set; }
        public int Precision { get; set; }
        public string RoundType { get; set; }
        public List<BomUNITCONVERTRATE> UNITCONVERTRATE { get; set; }
    }

    public class InvPtyId
    {
        public int Id { get; set; }
        public int msterID { get; set; }
        public string Number { get; set; }
        public List<MultiLanguageText> MultiLanguageText { get; set; }
        public List<Name> Name { get; set; }
    }

    public class MaterialBase
    {
        public int Id { get; set; }
        public string ErpClsID { get; set; }
        public bool IsInventory { get; set; }
        public bool IsProduce { get; set; }
        public int BaseUnitId_Id { get; set; }
        public BaseUnitId BaseUnitId { get; set; }
        public string Suite { get; set; }
    }

    public class MaterialGroup
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public List<MultiLanguageText> MultiLanguageText { get; set; }
        public List<Name> Name { get; set; }
    }

    public class MATERIALID
    {
        public int Id { get; set; }
        public int msterID { get; set; }
        public string DocumentStatus { get; set; }
        public string ForbidStatus { get; set; }
        public List<MultiLanguageText> MultiLanguageText { get; set; }
        public List<Name> Name { get; set; }
        public string Number { get; set; }
        public int CreateOrgId_Id { get; set; }
        public CreateOrgId CreateOrgId { get; set; }
        public int UseOrgId_Id { get; set; }
        public UseOrgId UseOrgId { get; set; }
        public List<Specification> Specification { get; set; }
        public int MaterialGroup_Id { get; set; }
        public MaterialGroup MaterialGroup { get; set; }
        public List<MaterialBase> MaterialBase { get; set; }
        public List<MaterialStock> MaterialStock { get; set; }
        public List<MaterialPurchase> MaterialPurchase { get; set; }
        public List<MaterialPlan> MaterialPlan { get; set; }
        public List<MaterialProduce> MaterialProduce { get; set; }
        public List<object> MaterialAuxPty { get; set; }
        public List<MaterialInvPty> MaterialInvPty { get; set; }
        public List<MaterialQM> MaterialQM { get; set; }
    }

    public class MATERIALIDCHILD
    {
        public int Id { get; set; }
        public int msterID { get; set; }
        public string DocumentStatus { get; set; }
        public string ForbidStatus { get; set; }
        public List<MultiLanguageText> MultiLanguageText { get; set; }
        public List<Name> Name { get; set; }
        public string Number { get; set; }
        public int CreateOrgId_Id { get; set; }
        public CreateOrgId CreateOrgId { get; set; }
        public int UseOrgId_Id { get; set; }
        public UseOrgId UseOrgId { get; set; }
        public List<Specification> Specification { get; set; }
        public int MaterialGroup_Id { get; set; }
        public MaterialGroup MaterialGroup { get; set; }
        public List<MaterialBase> MaterialBase { get; set; }
        public List<MaterialStock> MaterialStock { get; set; }
        public List<MaterialPurchase> MaterialPurchase { get; set; }
        public List<MaterialPlan> MaterialPlan { get; set; }
        public List<MaterialProduce> MaterialProduce { get; set; }
        public List<object> MaterialAuxPty { get; set; }
        public List<MaterialInvPty> MaterialInvPty { get; set; }
        public List<MaterialQM> MaterialQM { get; set; }
    }

    public class MaterialInvPty
    {
        public int Id { get; set; }
        public bool IsEnable { get; set; }
        public bool IsAffectPlan { get; set; }
        public int InvPtyId_Id { get; set; }
        public InvPtyId InvPtyId { get; set; }
    }

    public class MaterialPlan
    {
        public int Id { get; set; }
        public double EOQ { get; set; }
        public string PlanningStrategy { get; set; }
        public string OrderPolicy { get; set; }
        public string FixLeadTimeType { get; set; }
        public int FixLeadTime { get; set; }
        public string VarLeadTimeType { get; set; }
        public int VarLeadTime { get; set; }
        public string CheckLeadTimeType { get; set; }
        public int CheckLeadTime { get; set; }
        public string OrderIntervalTimeType { get; set; }
        public int OrderIntervalTime { get; set; }
        public string PlanOffsetTimeType { get; set; }
        public int PlanOffsetTime { get; set; }
        public double MinPOQty { get; set; }
        public double IncreaseQty { get; set; }
        public double MaxPOQty { get; set; }
        public double VarLeadTimeLotSize { get; set; }
        public int MfgPolicyId_Id { get; set; }
        public PartMfgPolicyId MfgPolicyId { get; set; }
        public int SupplySourceId_Id { get; set; }
        public object SupplySourceId { get; set; }
    }

    public class MaterialProduce
    {
        public int Id { get; set; }
        public int PickStockId_Id { get; set; }
        public PickStockId PickStockId { get; set; }
        public int BOMUnitId_Id { get; set; }
        public BOMUnitId BOMUnitId { get; set; }
        public int WorkShopId_Id { get; set; }
        public object WorkShopId { get; set; }
        public string IssueType { get; set; }
        public int ProduceUnitId_Id { get; set; }
        public ProduceUnitId ProduceUnitId { get; set; }
        public bool IsKitting { get; set; }
        public bool IsCoby { get; set; }
        public string BKFLTime { get; set; }
        public int PickBinId_Id { get; set; }
        public object PickBinId { get; set; }
        public bool IsMainPrd { get; set; }
        public string OverControlMode { get; set; }
        public double MinIssueQty { get; set; }
        public bool IsProductLine { get; set; }
        public bool ISMinIssueQty { get; set; }
        public bool IsECN { get; set; }
        public int MinIssueUnitId_Id { get; set; }
        public MinIssueUnitId MinIssueUnitId { get; set; }
        public double LossPercent { get; set; }
        public double FIXLOSS { get; set; }
    }

    public class MaterialPurchase
    {
        public int Id { get; set; }
        public bool IsVmiBusiness { get; set; }
    }

    public class MaterialQM
    {
        public int Id { get; set; }
        public bool CheckProduct { get; set; }
        public bool CheckIncoming { get; set; }
    }

    public class MaterialStock
    {
        public int Id { get; set; }
        public int StoreUnitID_Id { get; set; }
        public StoreUnitID StoreUnitID { get; set; }
        public bool IsExpParToFlot { get; set; }
    }

    public class MfgPolicyId
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public List<MultiLanguageText> MultiLanguageText { get; set; }
        public List<Name> Name { get; set; }
    }

    public class MinIssueUnitId
    {
        public int Id { get; set; }
        public int msterID { get; set; }
        public string Number { get; set; }
        public List<MultiLanguageText> MultiLanguageText { get; set; }
        public List<Name> Name { get; set; }
    }

    public class ModifierId
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserAccount { get; set; }
    }

    public class MultiLanguageText
    {
        public int PkId { get; set; }
        public int LocaleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ForbidReson { get; set; }
        public string Specification { get; set; }
    }

    public class Name
    {
        public int Key { get; set; }
        public string Value { get; set; }
    }

    public class PickStockId
    {
        public int Id { get; set; }
        public int msterID { get; set; }
        public string Number { get; set; }
        public List<MultiLanguageText> MultiLanguageText { get; set; }
        public List<Name> Name { get; set; }
    }

    public class ProduceUnitId
    {
        public int Id { get; set; }
        public int msterID { get; set; }
        public string Number { get; set; }
        public List<MultiLanguageText> MultiLanguageText { get; set; }
        public List<Name> Name { get; set; }
    }

    public class BomResponseStatus
    {
        public bool IsSuccess { get; set; }
    }

    public class BomResult
    {
        public BomResponseStatus ResponseStatus { get; set; }
        public Result Result { get; set; }
        public int Id { get; set; }
        public int msterID { get; set; }
        public string DocumentStatus { get; set; }
        public string ForbidStatus { get; set; }
        public List<MultiLanguageText> MultiLanguageText { get; set; }
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
        public int ForbidderId_Id { get; set; }
        public object ForbidderId { get; set; }
        public int ApproverId_Id { get; set; }
        public ApproverId ApproverId { get; set; }
        public object ForbidDate { get; set; }
        public DateTime? ApproveDate { get; set; }
        public string BOMCATEGORY { get; set; }
        public string BOMUSE { get; set; }
        public int MATERIALID_Id { get; set; }
        public MATERIALID MATERIALID { get; set; }
        public double YIELDRATE { get; set; }
        public string BILLTYPE_Id { get; set; }
        public BILLTYPE BILLTYPE { get; set; }
        public int FUNITID_Id { get; set; }
        public BomFUNITID FUNITID { get; set; }
        public int BaseUnitId_Id { get; set; }
        public BaseUnitId BaseUnitId { get; set; }
        public string ISDEFAULT { get; set; }
        public int CfgBomId { get; set; }
        public double Qty { get; set; }
        public double BaseQty { get; set; }
        public int GROUP_Id { get; set; }
        public object GROUP { get; set; }
        public string PLMBOMId { get; set; }
        public string BOMSRC { get; set; }
        public bool IsValidate { get; set; }
        public int ParentAuxPropId_Id { get; set; }
        public object ParentAuxPropId { get; set; }
        public int MDLID_Id { get; set; }
        public object MDLID { get; set; }
        public object ExtVar { get; set; }
        public List<ForbidReson> ForbidReson { get; set; }
        public bool IsChange { get; set; }
        public List<TreeEntity> TreeEntity { get; set; }
        public List<object> EntryBOMCOBY { get; set; }
        public List<object> BopEntity { get; set; }
    }

    public class ERPBomViewResponse
    {
        public BomResult Result { get; set; }
    }

    public class Specification
    {
        public int Key { get; set; }
        public string Value { get; set; }
    }

    public class StoreUnitID
    {
        public int Id { get; set; }
        public int msterID { get; set; }
        public string Number { get; set; }
        public List<MultiLanguageText> MultiLanguageText { get; set; }
        public List<Name> Name { get; set; }
    }

    public class SUPPLYORG
    {
        public int Id { get; set; }
        public List<MultiLanguageText> MultiLanguageText { get; set; }
        public List<Name> Name { get; set; }
        public string Number { get; set; }
    }

    public class TreeEntity
    {
        public int Id { get; set; }
        public int Seq { get; set; }
        public string ParentRowId { get; set; }
        public int RowExpandType { get; set; }
        public string RowId { get; set; }
        public string MATERIALTYPE { get; set; }
        public string DOSAGETYPE { get; set; }
        public int PROCESSID_Id { get; set; }
        public object PROCESSID { get; set; }
        public double FIXSCRAPQTY { get; set; }
        public DateTime? EFFECTDATE { get; set; }
        public DateTime? EXPIREDATE { get; set; }
        public string ISSUETYPE { get; set; }
        public int SUPPLYORG_Id { get; set; }
        public SUPPLYORG SUPPLYORG { get; set; }
        public int STOCKID_Id { get; set; }
        public object STOCKID { get; set; }
        public int STOCKLOCID_Id { get; set; }
        public object STOCKLOCID { get; set; }
        public bool ALLOWOVER { get; set; }
        public string BACKFLUSHTYPE { get; set; }
        public string FTIMEUNIT { get; set; }
        public bool ISKEYCOMPONENT { get; set; }
        public int MATERIALIDCHILD_Id { get; set; }
        public MATERIALIDCHILD MATERIALIDCHILD { get; set; }
        public string POSITIONNO { get; set; }
        public double FSCRAPRATE { get; set; }
        public double DISASSMBLERATE { get; set; }
        public int OFFSETTIME { get; set; }
        public bool FISGETSCRAP { get; set; }
        public int BOMID_Id { get; set; }
        public object BOMID { get; set; }
        public double NUMERATOR { get; set; }
        public double DENOMINATOR { get; set; }
        public string OWNERTYPEID { get; set; }
        public int OWNERID_Id { get; set; }
        public object OWNERID { get; set; }
        public int OPERID { get; set; }
        public int AuxPropId_Id { get; set; }
        public object AuxPropId { get; set; }
        public List<object> MultiLanguageText { get; set; }
        public List<object> MEMO { get; set; }
        public int CHILDUNITID_Id { get; set; }
        public CHILDUNITID CHILDUNITID { get; set; }
        public string EntryRowId { get; set; }
        public int ChildBaseUnitID_Id { get; set; }
        public ChildBaseUnitID ChildBaseUnitID { get; set; }
        public double BaseNumerator { get; set; }
        public double BaseFixscrapQty { get; set; }
        public double BaseDenominator { get; set; }
        public double Qty { get; set; }
        public double ActualQty { get; set; }
        public string OverControlMode { get; set; }
        public int ReplaceGroup { get; set; }
        public string ReplacePolicy { get; set; }
        public string ReplaceType { get; set; }
        public int ReplacePriority { get; set; }
        public int MRPPriority { get; set; }
        public bool IskeyItem { get; set; }
        public bool IsCanChoose { get; set; }
        public bool IsCanEdit { get; set; }
        public bool IsCanReplace { get; set; }
        public int CfgBomEntryId { get; set; }
        public int CfgFeatureEntryId { get; set; }
        public int ChildSupplyOrgId_Id { get; set; }
        public ChildSupplyOrgId ChildSupplyOrgId { get; set; }
        public string OptQueue { get; set; }
        public string PLMBOMEntryId { get; set; }
        public string BOMEntrySRC { get; set; }
        public bool ISSkip { get; set; }
        public int TreeEntryIdBak { get; set; }
        public bool ISMinIssueQty { get; set; }
        public string ChangeType { get; set; }
        public DateTime ChangeTime { get; set; }
        public string ECNBillNo { get; set; }
        public string ECNChgType { get; set; }
        public object ECNChgDate { get; set; }
        public string SupplyMode { get; set; }
        public string ECNRowType { get; set; }
        public string EntrySource { get; set; }
        public string RecordData { get; set; }
        public string FPLMBOMEntryRowId { get; set; }
        public bool IsMrpRun { get; set; }
        public object ModifiedField { get; set; }
        public int SubstitutionId_Id { get; set; }
        public object SubstitutionId { get; set; }
        public int STEntryId { get; set; }
        public string SupplyType { get; set; }
        public bool IsMulCsd { get; set; }
        public string F_TBIB_Text { get; set; }
        public List<object> BOMCHILDLOTBASEDQTY { get; set; }
    }

    public class BomUNITCONVERTRATE
    {
        public int Id { get; set; }
        public string ConvertType { get; set; }
    }

    public class UnitGroupId
    {
        public int Id { get; set; }
        public int msterID { get; set; }
        public string Number { get; set; }
        public List<MultiLanguageText> MultiLanguageText { get; set; }
        public List<Name> Name { get; set; }
    }

    public class UseOrgId
    {
        public int Id { get; set; }
        public List<MultiLanguageText> MultiLanguageText { get; set; }
        public List<Name> Name { get; set; }
        public string Number { get; set; }
    }


}
