using System;
using System.Collections.Generic;
using System.Text;

namespace StudyDemo2_ORM
{
    /// <summary>
    /// 表/类映射特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class MTableAttribute : MappingAttribute
    {
        //private string TableName = null;

        public MTableAttribute(string tableName):base(tableName)
        {
            //TableName = tableName;
        }

        /// <summary>
        /// 获取映射表名称
        /// </summary>
        /// <returns></returns>
        //public string GetMappingName()
        //{
        //    return TableName;
        //}
    }
}
