using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;

namespace StudyDemo2_ORM
{
    /// <summary>
    /// 验证扩展类
    /// </summary>
    public static class ValidateExtensions
    {
        public static bool Validate<T>(this T t)
        {
            Type type = typeof(T);
            foreach(var prop in type.GetProperties())
            {
                if(prop.IsDefined(typeof(ValidateBaseAttribute), true))
                {
                    object value = prop.GetValue(t);
                    var attributes = prop.GetCustomAttributes<ValidateBaseAttribute>();
                    foreach(var attribute in attributes)
                    {
                        if (!attribute.Validate(value))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

    }
}
