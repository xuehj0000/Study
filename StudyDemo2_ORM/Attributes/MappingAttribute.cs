using System;
using System.Collections.Generic;
using System.Text;

namespace StudyDemo2_ORM
{
    public class MappingAttribute:Attribute
    {
        private string MappingName = null;

        public MappingAttribute(string mappingName)
        {
            MappingName = mappingName;
        }

        /// <summary>
        /// 获取映射表名称
        /// </summary>
        /// <returns></returns>
        public string GetMappingName()
        {
            return MappingName;
        }
    }
}
