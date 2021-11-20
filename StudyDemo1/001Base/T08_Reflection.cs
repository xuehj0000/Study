using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace StudyDemo1
{
    /// <summary>
    /// 反射：在程序运行时，程序能够获取到一些程序集、class、method、property信息的机制
    ///       
    /// abstract 抽象类，
    /// </summary>
    public class T08_Reflection
    {

        /// <summary>
        /// 获取 Type 类型的反射
        /// </summary>
        public static void GetType()
        {
            // 方式一
            string s = "aaa";
            Type t = s.GetType();
            Console.WriteLine(t.FullName);

            // 方式二
            Type t2 = Type.GetType("System.String", false, true);
            Console.WriteLine(t2.FullName);

            // 方式三
            Type t3 = typeof(string);
            Console.WriteLine(t3.FullName);

        }
        /// <summary>
        /// 获取方法的反射
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

        /// <summary>
        /// 根据 GetFields, GetProperties 获取字段或者属性
        /// </summary>
        public static void GetFields()
        {

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




        #region 抽象类

        //特性：不能实例化
        //      可以包含抽象方法和抽象访问器
        //      不能用 seald 修饰符修饰抽象类，因为这两个修饰符的含义是相反的。采用sealed修饰符的类无法继承，而abstract修饰符要求对类进行继承
        //      从抽象类派生的非抽象类必须包括继承的所有抽象方法和抽象访问器的实际实现

        //应用场景：一些方法并且想让他们中的一些有默认实现


        #endregion




    }

    public class Car
    {
        public bool IsMoving()
        {
            return true;
        }
    }

}
