using System;
using System.Collections.Generic;
using System.Text;

namespace StudyDemo2_ORM
{
    /// <summary>
    /// 属性映射特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class MColumnAttribute:MappingAttribute
    {
        //private string ColumnName = null;

        public MColumnAttribute(string columnName):base(columnName)
        {
            //ColumnName = columnName;
        }

        /// <summary>
        /// 获取映射表名称
        /// </summary>
        /// <returns></returns>
        //public string GetMappingName()
        //{
        //    return ColumnName;
        //}
    }
}
