using System;
using System.Collections.Generic;
using System.Text;

namespace StudyDemo2_ORM
{
    /// <summary>
    /// 长度限制特性
    /// </summary>
    public class VLengthAttribute: ValidateBaseAttribute
    {
        private int _min = 0;
        private int _max = 0;

        /// <summary>
        /// 左边闭区间，右边开区间
        /// </summary>
        /// <param name="min">包含</param>
        /// <param name="max">不包含</param>
        public VLengthAttribute(int min, int max)
        {
            this._min = min;
            this._max = max;
        }

        public override bool Validate(object value)
        {
            return value != null && !string.IsNullOrWhiteSpace(value.ToString()) && value.ToString().Length >= _min && value.ToString().Length < _max;
        }
    }
}
