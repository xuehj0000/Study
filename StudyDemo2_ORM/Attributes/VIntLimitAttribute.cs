using System;
using System.Linq;

namespace StudyDemo2_ORM
{
    /// <summary>
    /// int 值限制特性
    /// </summary>
    public class VIntLimitAttribute : ValidateBaseAttribute
    {
        private int[] _values;

        public VIntLimitAttribute(params int[] values)
        {
            _values = values;
        }

        public override bool Validate(object value)
        {
            return value != null
                && !string.IsNullOrWhiteSpace(value.ToString())
                && int.TryParse(value.ToString(), out int ivalue)
                && this._values != null
                && this._values.Contains(ivalue);
        }
    }
}
