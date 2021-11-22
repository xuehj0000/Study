using System;
using System.Collections.Generic;
using System.Text;

namespace StudyDemo2_ORM
{
    /// <summary>
    /// 属性忽略特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PropIgnoreAttribute:Attribute
    {
    }
}
