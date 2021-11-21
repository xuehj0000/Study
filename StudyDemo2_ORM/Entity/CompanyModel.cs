using System;

namespace StudyDemo2_ORM
{
    /// <summary>
    /// 一律在实体后加model
    /// 改了数据库表，不想改代码
    /// 实体跟数据库的表&字段，不完全一致
    /// </summary>
    [Table("Company")]
    public class CompanyModel:BaseEntity
    {
        public string Name { get; set; }

        public DateTime CreateTime { get; set; }
        
        public int CreatorId { get; set; }
        
        public Nullable<int> LastModifierId { get; set; }
        
        public DateTime? LastModifyTime { get; set; }
    }
}
