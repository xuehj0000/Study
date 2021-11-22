using System;
using System.Collections.Generic;
using System.Text;

namespace StudyDemo2_ORM
{
    /// <summary>
    /// 非空特性
    /// </summary>
    public class VRequiredAttribute : ValidateBaseAttribute
    {
        public override bool Validate(object value)
        {
            return value != null && !string.IsNullOrWhiteSpace(value.ToString());
        }
    }
}
