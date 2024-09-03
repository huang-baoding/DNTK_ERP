using System.Collections.Generic;

namespace hzerpdemo.Models
{
    public class FAuxPropId
    {
    }

    public class FAuxPropIdCoby
    {
    }

    public class FBILLTYPE
    {
        public string FNUMBER { get; set; }
    }

    public class FBOMCHILDLOTBASEDQTY
    {
        public int FDETAILID { get; set; }
        public FMATERIALIDLOTBASED FMATERIALIDLOTBASED { get; set; }
        public int FSTARTQTY { get; set; }
        public int FENDQTY { get; set; }
        public FUNITIDLOT FUNITIDLOT { get; set; }
        public int FFIXSCRAPQTYLOT { get; set; }
        public int FSCRAPRATELOT { get; set; }
        public int FNUMERATORLOT { get; set; }
        public int FDENOMINATORLOT { get; set; }
        public string FNOTELOT { get; set; }
        //public FCHILDITEMNAMELOT FCHILDITEMNAMELOT { get; set; }
        public string FCHILDITEMNAMELOT { get; set; }
    }

    public class FCHILDITEMNAMELOT{
        public string FNumber { get; set; }
    }
    public class FBOMID
    {
        public string FNumber { get; set; }
    }

    public class FBOMIDCoby
    {
        public string FNumber { get; set; }
    }

    public class FBopBaseUnitID
    {
        public string FNumber { get; set; }
    }

    public class FBopEntity
    {
        public int FEntryID { get; set; }
        public int FReplaceGroupBop { get; set; }
        public FProductLineId FProductLineId { get; set; }
        public FPrdLineLocId FPrdLineLocId { get; set; }
        public FBopMaterialId FBopMaterialId { get; set; }
        public FBopUnitId FBopUnitId { get; set; }
        public string FBopDosageType { get; set; }
        public int FBopNumerator { get; set; }
        public int FBopDenominator { get; set; }
        public FBopBaseUnitID FBopBaseUnitID { get; set; }
        public int FBaseBopNumerator { get; set; }
        public int FBaseBopDenominator { get; set; }
        public int FTreeEntryId { get; set; }
    }

    public class FBopMaterialId
    {
        public string FNUMBER { get; set; }
    }

    public class FBopUnitId
    {
        public string FNUMBER { get; set; }
    }

    public class FCHILDUNITID
    {
        public string FNumber { get; set; }
    }

    public class FEntryBOMCOBY
    {
        public int FENTRYID { get; set; }
        public string FCOBYTYPE { get; set; }
        public FMATERIALIDCOBY FMATERIALIDCOBY { get; set; }
        public FBOMIDCoby FBOMIDCoby { get; set; }
        public FUNITIDCOBY FUNITIDCOBY { get; set; }
        public FAuxPropIdCoby FAuxPropIdCoby { get; set; }
        public int FQTYCOBY { get; set; }
        public string FISBACKFLUSH { get; set; }
        public int FCOSTRATECOBY { get; set; }
        public string FOutPutOptQueue { get; set; }
        public string FPPROCESSID { get; set; }
        public FTASKIDCOBY FTASKIDCOBY { get; set; }
        public string FEFFECTDATECOBY { get; set; }
        public string FEXPIREDATECOBY { get; set; }
        public string FNOTECOBY { get; set; }
    }

    public class FGroup
    {
        public string FNumber { get; set; }
    }

    public class FMATERIALID
    {
        public string FNumber { get; set; }
    }

    public class FMATERIALIDCHILD
    {
        public string FNumber { get; set; }
    }

    public class FMATERIALIDCOBY
    {
        public string FNumber { get; set; }
    }

    public class FMATERIALIDLOTBASED
    {
        public string FNumber { get; set; }
    }

    public class FMDLID
    {
        public string FNUMBER { get; set; }
    }

    public class FOWNERID
    {
        public string FNumber { get; set; }
    }

    public class FParentAuxPropId
    {
    }

    public class FPrdLineLocId
    {
        public string FLOCATIONCODE { get; set; }
    }

    public class FPROCESSID
    {
        public string FNumber { get; set; }
    }

    public class FProductLineId
    {
        public string FNumber { get; set; }
    }

    public class FSTOCKID
    {
        public string FNumber { get; set; }
    }

    public class FSTOCKLOCID
    {
    }

    public class FSubstitutionId
    {
        public string FNUMBER { get; set; }
    }

    public class FTASKIDCOBY
    {
        public string FNumber { get; set; }
    }

    public class FTreeEntity
    {
        public int FENTRYID { get; set; }
        public int FReplaceGroup { get; set; }
        public FMATERIALIDCHILD FMATERIALIDCHILD { get; set; }
        public string FSupplyType { get; set; }
        public string FMATERIALTYPE { get; set; }
        public FPROCESSID FPROCESSID { get; set; }
        public FCHILDUNITID FCHILDUNITID { get; set; }
        public string FDOSAGETYPE { get; set; }
        public int FNUMERATOR { get; set; }
        public int FDENOMINATOR { get; set; }
        public int FFIXSCRAPQTY { get; set; }
        public int FSCRAPRATE { get; set; }
        public FBOMID FBOMID { get; set; }
        public string FMEMO { get; set; }
        public string FOverControlMode { get; set; }
        public string FOptQueue { get; set; }
        public FSTOCKID FSTOCKID { get; set; }
        public FSTOCKLOCID FSTOCKLOCID { get; set; }
        public string FIsCanChoose { get; set; }
        public string FIsCanEdit { get; set; }
        public string FReplacePolicy { get; set; }
        public string FIsCanReplace { get; set; }
        public string FReplaceType { get; set; }
        public int FReplacePriority { get; set; }
        public int FMRPPriority { get; set; }
        public string FIskeyItem { get; set; }
        public string FALLOWOVER { get; set; }
        public string FISSkip { get; set; }
        public string FIsMulCsd { get; set; }
        public string FISMinIssueQty { get; set; }
        public int FTreeEntryIdBak { get; set; }
        public string FSupplyMode { get; set; }
        public string FEntrySource { get; set; }
        public string FRecordData { get; set; }
        public string FEFFECTDATE { get; set; }
        public string FEXPIREDATE { get; set; }
        public string FISSUETYPE { get; set; }
        public string FBACKFLUSHTYPE { get; set; }
        public string FISGETSCRAP { get; set; }
        public int FOFFSETTIME { get; set; }
        public string FTIMEUNIT { get; set; }
        public string FISKEYCOMPONENT { get; set; }
        public int FOPERID { get; set; }
        public string FPOSITIONNO { get; set; }
        public string FOWNERTYPEID { get; set; }
        public FOWNERID FOWNERID { get; set; }
        public int FDISASSMBLERATE { get; set; }
        public string FPLMBOMENTRYROWID { get; set; }
        public string FIsMrpRun { get; set; }
        public string FModifiedField { get; set; }
        public string F_TBIB_Text { get; set; }
        public FSubstitutionId FSubstitutionId { get; set; }
        public int FSTEntryId { get; set; }
        public FAuxPropId FAuxPropId { get; set; }
        public List<FBOMCHILDLOTBASEDQTY> FBOMCHILDLOTBASEDQTY { get; set; }
        //public FCHILDITEMNAME FCHILDITEMNAME { get; set; }
        //public string FCHILDITEMMODEL { get; set; }
        public int FBaseNumerator { get; set; }
        public int FBaseDenominator { get; set; }
    }
    public class FCHILDITEMNAME
    {
        public string FNumber { get; set; }
    }
    public class FUNITID
    {
        public string FNumber { get; set; }
    }

    public class FUNITIDCOBY
    {
        public string FNumber { get; set; }
    }

    public class FUNITIDLOT
    {
        public string FNumber { get; set; }
    }

    public class BomModel
    {
        public int FID { get; set; }
        public string FNumber { get; set; }
        public string FName { get; set; }
        public FBILLTYPE FBILLTYPE { get; set; }
        public string FBOMCATEGORY { get; set; }
        public string FBOMUSE { get; set; }
        public FGroup FGroup { get; set; }
        public int FCfgBomId { get; set; }
        public int FYIELDRATE { get; set; }
        public FMATERIALID FMATERIALID { get; set; }
        public FUNITID FUNITID { get; set; }
        public string FDescription { get; set; }
        public string FIsValidate { get; set; }
        public FParentAuxPropId FParentAuxPropId { get; set; }
        public FMDLID FMDLID { get; set; }
        public string FExtVar { get; set; }
        public string FForbidReson { get; set; }
        public List<FTreeEntity> FTreeEntity { get; set; }
        public List<FEntryBOMCOBY> FEntryBOMCOBY { get; set; }
        public List<FBopEntity> FBopEntity { get; set; }
        //public string F_TBIB_BaseProperty { get; set; }

        //public string FITEMNAME { get; set; }
        //public string FITEMMODEL { get; set; }
        public FCreateOrgId FCreateOrgId{ get; set; }
    }
    public class FCreateOrgId
    {
        public string FNumber { get; set; }
    }
    public class BomDataModel
    {
        public List<string> NeedUpDateFields { get; set; }
        public List<string> NeedReturnFields { get; set; }
        public string IsDeleteEntry { get; set; }
        public string SubSystemId { get; set; }
        public string IsVerifyBaseDataField { get; set; }
        public string IsEntryBatchFill { get; set; }
        public string ValidateFlag { get; set; }
        public string NumberSearch { get; set; }
        public string IsAutoAdjustField { get; set; }
        public string InterationFlags { get; set; }
        public string IgnoreInterationFlag { get; set; }
        public string IsControlPrecision { get; set; }
        public BomModel Model { get; set; }
    }
}
