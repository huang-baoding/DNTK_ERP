namespace hzerpdemo.Models
{
    public class PartFormModel
    {
        public string item_number { get; set; }//物料编码
        public string hs_part_type { get; set; }//物料类别
        //public string PartCategory { get; set; }//
        //public string hs_large { get; set; }//
        public string hs_main_category { get; set; }//大类
        public string hs_middle_category { get; set; }//中类
        public string hs_sub_category { get; set; }//小类
        public string hs_subdivision_category { get; set; }//细分类
        public string name { get; set; }//物料名称
        public string hs_specification { get; set; }//规格型号
        public string hs_unit { get; set; }//计量单位
        public string hs_stage { get; set; }//阶段
        public string hs_part_status { get; set; }//物料状态
        public string hs_store { get; set; }//物料入库
        public string hs_device_letter { get; set; }//器件识别首字母
        public string hs_bom_number { get; set; }//客户BOM号
        public string hs_rala_prodect { get; set; }//关联的产品
        public string hs_note { get; set; }//备注
    }
}
