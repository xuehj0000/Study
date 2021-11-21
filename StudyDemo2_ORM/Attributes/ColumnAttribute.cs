using System;
using System.Collections.Generic;
using System.Text;

namespace StudyDemo2_ORM
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnAttribute:MappingAttribute
    {
        //private string ColumnName = null;

        public ColumnAttribute(string columnName):base(columnName)
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
