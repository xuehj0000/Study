using System;
using System.Collections.Generic;
using System.Linq;

namespace StudyDemo2_ORM
{
    /// <summary>
    /// 泛型缓存，生成sql语句
    /// 缺点：只能跟类型相关 如Company，不适合更新
    /// 优点：性能好，读取的功能。没有线程安全问题
    /// </summary>
    public class SqlCacheBuilder<T> where T: BaseEntity, new()
    {
        #region 泛型缓存

        // 泛型缓存
        // 每次都要动态瓶装sql，尤其是通过特性去映射，能不能缓存一下，重用一下？
        //不同的类型需要缓存一个不同的sql，就推荐用泛型缓存

        // 第一次使用时，静态初始化：第一个加载静态字段，第二个初始化静态构造函数，而且只执行一次
        // 第二次使用时，可以重用第一次生成的sql
        // 泛型类的类型参数T不同的时候，其实会产生一个全新的类。所以，静态字段，静态构造函数都会重新执行一次。

        #endregion

        
        private static string _insertSql = null;
        private static string _updateSql = null;
        private static string _findOneSql = null;
        private static string _deleteSql = null;

        /// <summary>
        /// 静态构造函数，会在程序第一次使用该函数之前完成初始化
        /// </summary>
        static SqlCacheBuilder()
        {
            {
                // 查询
                Type type = typeof(T);
                var columnString = type.GetProperties().Select(r => $"[{r.GetMappingName()}]").ToJoin();
                _findOneSql = $@"select {columnString} from [{type.GetMappingName()}] where Id=@Id";                    
            }

            {
                // 编辑
                Type type = typeof(T);
                var columnString = type.GetPropsWithOutIgnore().Select(r => $"{r.GetMappingName()}=@{r.GetMappingName()}").ToJoin();
                _updateSql = $@"update [{type.GetMappingName()}] set { columnString } where Id=@Id";

            }

            {
                // 插入
                Type type = typeof(T);
                var columnStrings = type.GetPropsWithOutIgnore().Select(r => $"[{r.GetMappingName()}]").ToJoin();
                var valueStrings = type.GetPropsWithOutIgnore().Select(r => $"@{r.GetMappingName()}").ToJoin();
                _insertSql = $@"insert into [{type.GetMappingName()}] ({columnStrings}) values ({valueStrings})";
            }

            {
                // 删除
                Type type = typeof(T);
                _deleteSql = $@"delete from [{type.GetMappingName()}] where Id=@Id";
            }
        }

        public static string GetSql(SqlCacheType cacheType)
        {
            switch (cacheType)
            {
                case SqlCacheType.FindOne:
                    return _findOneSql;
                case SqlCacheType.Insert:
                    return _insertSql;
                case SqlCacheType.Update:
                    return _updateSql;
                case SqlCacheType.Delete:
                    return _deleteSql;
                default:
                    throw new Exception("unkown type");
            }
        }

    }

    public enum SqlCacheType
    {
        FindOne,
        Insert,
        Update,
        Delete
    }


    #region 扩展知识，字典缓存

    /// <summary>
    /// 字典缓存
    /// 优势：方便，数据的保存以key为准
    /// 劣势：性能问题，性能主要是数据超过1w（大概）以上会有下降
    ///       hash 存储的，增删改查性能都差不多，但是最怕数据太多，多了有损耗
    /// </summary>
    internal class CustomCache
    {
        private static Dictionary<string, string> dic = new Dictionary<string, string>();
        
        public static void Add(string key, string value)
        {
            dic.Add(key, value);
        }

        public static string Get(string key)
        {
            return dic[key];
        }
    }


    #endregion


}
