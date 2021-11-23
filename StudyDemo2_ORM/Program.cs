using System;
using System.Linq.Expressions;

namespace StudyDemo2_ORM
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                SqlHelper sqlHelper = new SqlHelper();
                var user = sqlHelper.Find<CompanyModel>(r => r.Id == 2);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}



    #region 通用ORM 封装

        // 封装ORM 增删改查方法
        // 泛型，通用方法封装
        // 动态 sql 生成，sql语句泛型缓存
        // 特殊字符导致sql问题，sql 注入问题
        // 表达式目录树生成动态生成 sql 查询条件
        // 数据库读写分离

    #endregion
