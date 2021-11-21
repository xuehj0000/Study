using System;
using System.Collections.Generic;
using System.Text;

namespace StudyDemo2_ORM
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TableAttribute : MappingAttribute
    {
        //private string TableName = null;

        public TableAttribute(string tableName):base(tableName)
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
