using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudyDemo1
{
    /// <summary>
    /// linq 语句：语言整合查询语句，对数据进行查询与操作的语句
    /// </summary>
    public class T12_Linq
    {
        /// <summary>
        /// 实现了IEnumerable 的，都能使用linq语句进行查询
        /// linq to sql
        /// linq to xml
        /// linq to DateSet
        /// linq to object
        /// </summary>
        public static void UseLinqQuery()    // 查询方式一, query syntax 像sql 写法
        {
            // 数据源
            int[] nums = { 5, 10, 8, 3, 6, 12 };
            // 创建query 语句
            var query = from num in nums
                        where num % 2 == 0 && num % 3 == 1
                        orderby num descending
                        select num;
            //在此，查询并未立即执行，当数据用到时才会执行。好处：条件筛选后再执行，不会被放到内存中，提高效率

            // 执行
            foreach(var item in query)
            {
                Console.Write(item + " ");
            }

            // query 查询：分三步，定义数据源，创建query 语句，执行
        }


        public static void UseLinqMethod()   // 查询方式二, method syntax
        {
            int[] nums = { 5, 10, 8, 3, 6, 12 };
            var ret = nums.Where(r => r % 2 == 0).OrderBy(n => n);   // 并未立即执行，用到时才会执行

            foreach(var item in ret)
            {
                Console.WriteLine(item + " ");
            }
        }
    
    
        /// <summary>
        /// query 分组
        /// </summary>
        public static void QueryGroup()
        {
            // 数据源
            var list = new List<Customer>();
            // query 语句
            var query = from item in list
                        group item by item.City;
            foreach(var g in query)
            {
                Console.Write(g.Key);
                foreach(var item in g)
                {
                    Console.Write(" {0}", item.Name);
                }
            }
        }

        /// <summary>
        /// query Join
        /// </summary>
        public static void QueryJoin()
        {
            var listCus = new List<Customer>();
            var listEmp = new List<Employee>();

            var query = from c in listCus
                        join e in listEmp on c.Name equals e.Name
                        select new { PersonName = c.Name, PersonId = e.Id, PersionCity = c.City };

            foreach(var item in query)
            {
                Console.WriteLine("{0} {1} {2}", item.PersonName, item.PersonId, item.PersionCity);
            }
        }

        /// <summary>
        /// into 关键字， 将group 的数据打包成一个临时变量，对每个子group 进行处理过滤等
        /// </summary>
        public static void QueryInto()
        {
            var list = new List<Customer>();

            var query = from c in list
                        group c by c.City into cusGroup     
                        where cusGroup.Count() >= 2
                        select new { City = cusGroup.Key, Number = cusGroup.Count()};

            foreach(var item in query)
            {
                Console.Write("{0} Count {1}", item.City, item.Number);
            }
        }

        /// <summary>
        /// let 关键字，中间变量，存储处理后的数据
        /// </summary>
        public static void QueryLet()
        {
            string[] strs = { "Hello jikexueyuan", "This is Friday!", "Are you happy?" };
            var query = from s in strs
                        let words = s.Split(' ')
                        from word in words
                        let w = word.ToLower()
                        select w;
            foreach(var s in query)
            {
                Console.Write(s);
            }

        }



    }

    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
    }

    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
