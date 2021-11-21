using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace StudyDemo1
{
    /// <summary>
    /// 反射：在程序运行时，程序能够获取到一些程序集、class、method、property信息的机制
    /// </summary>
    public class T08_Reflection
    {
        /// <summary>
        /// 获取 Type 
        /// </summary>
        public static void GetType()
        {
            // 方式一: 通过反射加载dll文件
            Assembly assembly = Assembly.Load("Ant.DB.SqlServer");
            Type[] types = assembly.GetTypes();         // 根据程序集对象获取type

            // 方式二: 通过实例对象获取 type
            DbHelper obj1 = new MySqlHelper();
            Type t1 = obj1.GetType();
            MethodInfo m1 = t1.GetMethod("Query");

            // 方式三: 通过引用类型获取 type
            Type t3 = typeof(MySqlHelper);
        }
        /// <summary>
        /// 获取 Type 的方法
        /// </summary>
        public static void GetMethods(Type t)
        {
            // 情况一：获取所有方法
            MethodInfo[] mi = t.GetMethods();
            foreach(var item in mi)
            {
                Console.WriteLine("{0}", item.Name);
            }

            // 情况二：根据方法名获取方法
            MethodInfo info = t.GetMethod("GetModel");

            // 情况三：根据类型标签获取方法
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance;
            MethodInfo[] infos = t.GetMethods(flag);
        }

        // 动态加载及推迟绑定

        /// <summary>
        /// 通过 Assembly 反射
        /// </summary>
        public static void GetByAssembly()
        {
            //Assembly obj = Assembly.Load("");   //名字

            //Type[] types = obj.GetTypes();
            //foreach(var item in types)
            //{
            //    Console.WriteLine(item.Name);
            //}

            Assembly obj = Assembly.GetExecutingAssembly();
            Type t = obj.GetType("StudyDemo1.Car", false, true);
            object o = Activator.CreateInstance(t);
            MethodInfo mi = t.GetMethod("IsMoving");
            var isMoving = (bool)mi.Invoke(o, null);
            if (isMoving)
            {
                Console.WriteLine("IsMoving");
            }
            else
            {
                Console.WriteLine("Not is Moving");
            }
        }



        #region 反射应用场景：动态创建数据库

        public static void CreateDatebase()
        {
            DbHelper helper = DbHelperFactory.CreateInstance();
            helper.Query();
        }

        #endregion




    }

    public class Car
    {
        public bool IsMoving()
        {
            return true;
        }
    }

    #region 数据库类

    /// <summary>
    /// 数据库
    /// </summary>
    public interface DbHelper
    {
        void Query();
    }
    public class MySqlHelper : DbHelper
    {
        public void Query()
        {
            Console.WriteLine("mysql 链接");
        }
    }
    public class SqlServerHelper : DbHelper
    {
        public void Query()
        {
            Console.WriteLine("sqlServer 链接");
        }
    }
    /// <summary>
    /// 工厂类：封装数据库，提供实例
    /// </summary>
    public class DbHelperFactory
    {
        private static string GetDbString()
        {
            // 1.加载配置
            Type type = typeof(DBHelperConfig);
            DBHelperAttribute attribute = (DBHelperAttribute)type.GetCustomAttribute(typeof(DBHelperAttribute));
            return attribute.DBString;
        }

        public static DbHelper CreateInstance()
        {
            // 1.反射的加载
            Assembly assembly = Assembly.LoadFrom("StudyDemo1.dll");

            // 2.创建反射类型
            Type type = assembly.GetType(GetDbString());

            // 3.创建对象实例（如 DbHelper）
            //object helper = Activator.CreateInstance(type);
            //helper.Query();       // 报错原因：因为编译器不允许,可以使用动态类型       情况一

            //dynamic dy = Activator.CreateInstance(type);                               情况二：动态类型
            //dy.Query();             // 可以原因：编译器允许通过

            //DbHelper helper = (DbHelper)Activator.CreateInstance(type);                //情况三：强转

            DbHelper helper = Activator.CreateInstance(type) as DbHelper;                // 情况四：类型转换(as 转换不报错，类型不对返回null)。创建这个接口为了通用

            return helper;
        }
    }

    /// <summary>
    /// DBHelper 特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple =true)]
    public class DBHelperAttribute : Attribute
    {
        public string DBString { get; set; }
        public DBHelperAttribute(string dbString)
        {
            DBString = dbString;
        }
    }
    /// <summary>
    /// 数据库切换配置类
    /// </summary>
    [DBHelperAttribute("StudyDemo1.MySqlHelper")]
    public class DBHelperConfig
    {
        
    }

    #endregion
}
