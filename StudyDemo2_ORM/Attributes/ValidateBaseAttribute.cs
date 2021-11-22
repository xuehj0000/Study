using System;
using System.Collections.Generic;
using System.Text;

namespace StudyDemo2_ORM
{
    /// <summary>
    /// 校验基类
    /// </summary>
    [AttributeUsage(AttributeTargets.Property,AllowMultiple =true,Inherited =true)]
    public abstract class ValidateBaseAttribute:Attribute
    {
        public abstract bool Validate(object value);
    }
}
