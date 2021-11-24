using System;
using System.Linq.Expressions;
using System.Threading;

namespace StudyDemo2_ORM
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                SqlHelper sqlHelper = new SqlHelper();

                #region 验证 主--从 库 同步时间大约是多少
                var company = sqlHelper.Find<CompanyModel>(r => r.Id == 2);
                company.Name += "你大爷";
                bool result = sqlHelper.Update(company);
                Console.WriteLine($"主库已更新!{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff")}");
                while (true)
                {
                    var model = sqlHelper.Find<CompanyModel>(2);
                    if (company.Name.Equals(model.Name))
                    {
                        Console.WriteLine($"同步成功！{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff")}");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("从库尚未同步成功...");
                        Thread.Sleep(100);
                    }
                }
                #endregion
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
