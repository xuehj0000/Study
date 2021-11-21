using System;
using System.Reflection;

namespace StudyDemo2_ORM
{
    /// <summary>
    /// 映射扩展方法
    /// </summary>
    public static class DbMappingExtensions
    {
        /// <summary>
        /// 获取映射类，映射表名
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        //public static string GetMappingTableName(this Type type)
        //{
        //    if(type.IsDefined(typeof(TableAttribute), true))
        //    {
        //        var attribute = type.GetCustomAttribute<TableAttribute>();
        //        return attribute.GetMappingName();
        //    }
        //    else
        //    {
        //        return type.Name;
        //    }
        //}


        //public static string GetMappingPropertyName(this PropertyInfo prop)
        //{
        //    if (prop.IsDefined(typeof(ColumnAttribute), true))
        //    {
        //        var attribute = prop.GetCustomAttribute<ColumnAttribute>();
        //        return attribute.GetMappingName();
        //    }
        //    else
        //    {
        //        return prop.Name;
        //    }
        //}

        public static string GetMappingName<T>(this T t) where T:MemberInfo
        {
            if (t.IsDefined(typeof(MappingAttribute), true))
            {
                var attribute = t.GetCustomAttribute<MappingAttribute>();
                return attribute.GetMappingName();
            }
            else
            {
                return t.Name;
            }
        }

    }
}
