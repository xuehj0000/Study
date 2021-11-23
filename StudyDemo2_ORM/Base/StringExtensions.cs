using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace StudyDemo2_ORM
{
    public static class StringExtensions
    {
        /// <summary>
        /// 拼接
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static string ToJoin(this IEnumerable arr,char split=',')
        {
            return string.Join(split, arr);
        }
    }
}
