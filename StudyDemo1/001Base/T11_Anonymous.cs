using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace StudyDemo1
{
    /// <summary>
    /// 匿名函数：没有名称的函数
    /// </summary>
    public class T11_Anonymous
    {
        delegate void AnonyDelegate(string s);
        delegate int LambdaDelegate(int i);
        delegate T Func<TArgument, T>(TArgument args);

        static void Method(string s)
        {
            Console.WriteLine(s);
        }


        /// <summary>
        /// 委托在C# 中发展的历史
        /// </summary>
        public static void Use()
        {
            //AnonyDelegate ad = new AnonyDelegate(Method);

            // C#2.0 进阶
            //AnonyDelegate ad = delegate (string s) { };  // 匿名方法

            //C#3.0 再进阶 lambda expression
            AnonyDelegate ad = (x) => { };                 // Lambda 表达式写法
        }

        /// <summary>
        /// 使用匿名方法，不能使用ref 与out 关键字参数的，不能使用 is 
        /// </summary>
        public static void UseAnonymous()
        {
            Thread t = new Thread(delegate ()
            {
                Console.Write("开启多线程！");
            });
            t.Start();            // 开启多线程
        }

        /// <summary>
        /// lambda 表达式   组成 ()=> expression
        /// </summary>
        public static void UseLambda()
        {
            LambdaDelegate lam = r => r * r;
            var ret = lam(5);
            Console.WriteLine(ret);
        }
        /// <summary>
        /// lambda 与 泛型 结合使用
        /// </summary>
        public static void UseLambdaGeneric()
        {
            Func<int, bool> myFunc = x => x == 5;
            var ret = myFunc(4);
        }

    }
}
