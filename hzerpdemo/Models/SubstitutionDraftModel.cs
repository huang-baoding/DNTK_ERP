using System.Collections.Generic;

namespace hzerp.Models
{
    public class SubstitutionDraftModel
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
        public SubstitutionModel Model { get; set; }
    }
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class FApproverId
    {
        public string FUserID { get; set; }
    }

    public class FAuxPropID
    {
    }

    public class FBillType
    {
        public string FNUMBER { get; set; }
    }

    public class FBomId
    {
        public string FNumber { get; set; }
    }

    public class FCreatorId
    {
        public string FUserID { get; set; }
    }

    public class FEntity
    {
        public int FEntryID { get; set; }
        public int FPriority { get; set; }
        public FSubMaterialID FSubMaterialID { get; set; }//替代物料编码
        public FSubAuxPropID FSubAuxPropID { get; set; }
        public FSubBomId FSubBomId { get; set; }
        public bool FSubIsKeyItem { get; set; }
        public FSubUnitID FSubUnitID { get; set; }//单位
        public int FSubNumerator { get; set; }
        public int FSubDenominator { get; set; }
        public string FEffectDate { get; set; }//生效日期
        public string FExpireDate { get; set; }//失效日期
        public string FMemo { get; set; }
        public int FSubBomEntryId { get; set; }
        public FSUBTemMtrlId FSUBTemMtrlId { get; set; }
    }

    public class FEntityMainItem
    {
        public int FEntryID { get; set; }
        public int FMainPriority { get; set; }
        public FMaterialID FMaterialID { get; set; }//物料编码
        public FAuxPropID FAuxPropID { get; set; }
        public FBomId FBomId { get; set; }
        public bool FIsKeyItem { get; set; }
        public FUnitID FUnitID { get; set; }//单位
        public int FNumerator { get; set; }
        public int FDenominator { get; set; }
        public int FBomEntryId { get; set; }
        public FTemplateMtrlId FTemplateMtrlId { get; set; }
    }

    public class FForbidderId
    {
        public string FUserID { get; set; }
    }

    public class FMaterialID
    {
        public string FNUMBER { get; set; }
    }

    public class FModifierId
    {
        public string FUserID { get; set; }
    }

    public class FSubAuxPropID
    {
    }

    public class FSubBomId
    {
        public string FNumber { get; set; }
    }

    public class FSubMaterialID
    {
        public string FNUMBER { get; set; }
    }

    public class FSUBTemMtrlId
    {
        public string FNUMBER { get; set; }
    }

    public class FSubUnitID
    {
        public string FNumber { get; set; }
    }

    public class FTemplateMtrlId
    {
        public string FNUMBER { get; set; }
    }

    public class FUnitID
    {
        public string FNumber { get; set; }
    }

    public class SubstitutionModel
    {
        public int FID { get; set; }//实体主键
        public string FNumber { get; set; }//替代编码
        public string FDescription { get; set; }
        public string FName { get; set; }//替代名称
        public string FReplacePolicy { get; set; }//替代策略
        public string FReplaceType { get; set; }//替代方式
        public FBillType FBillType { get; set; }//单据类型
        public FCreatorId FCreatorId { get; set; }
        public string FCreateDate { get; set; }
        public FModifierId FModifierId { get; set; }
        public string FModifyDate { get; set; }
        public FApproverId FApproverId { get; set; }
        public string FApproveDate { get; set; }
        public FForbidderId FForbidderId { get; set; }
        public string FForbidDate { get; set; }
        public string FReplaceSource { get; set; }//替代方案来源
        public string FRepType { get; set; }
        public List<FEntity> FEntity { get; set; }//替代物料
        public List<FEntityMainItem> FEntityMainItems { get; set; }//主物料
        public string FReplaceNO { get; set; }
    }

}
