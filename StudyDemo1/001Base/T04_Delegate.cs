using System;
using System.Collections.Generic;
using System.Text;

namespace StudyDemo1
{
    /// <summary>
    /// 委托，类似C++中的函数指针。有时候我们想调用一个方法，在编译时不知道哪个方法被调用，在被执行时确定哪个方法被调用
    /// 采用匿名的方式，参数和返回值确定，是实现事件和回调函数的基础
    /// 委托本质上是一个类，继承自multicastDelegate
    /// </summary>
    public class T04_Delegate
    {
        static int num = 10;



        public static int AddNum(int p)
        {
            num += p;
            return num;
        }

        public static int MultiNum(int p)
        {
            num *= p;
            return num;
        }

        public static int DelNum(int p)
        {
            return num;
        }

        /// <summary>
        /// 使用委托
        /// </summary>
        public static void UseDelegate()
        {
            AddDelegate ad = new AddDelegate(AddNum);
            ad(12);

            AddDelegate am = new AddDelegate(MultiNum);
            am(12);

            Console.WriteLine("value of Num:{0}", num);
            Console.ReadLine();
        }

        //多重委托，委托调用是一个调用列表，可以同时调用多个方法，这就是多重委托。
        //+= 与-=有顺序说法，最下方的。空列表，-= 不存在的委托，没有问题
        public static void UseMultiDelegate()
        {
            AddDelegate ad1 = new AddDelegate(AddNum);
            AddDelegate ad2 = new AddDelegate(MultiNum);

            AddDelegate ad = ad1 + ad2;
            ad += new AddDelegate(DelNum);
            ad += new AddDelegate(ad1);
            ad -= new AddDelegate(ad1);
            ad(12);
            Console.WriteLine("value of Num:{0}", num);
            Console.ReadLine();
        }

    }


    delegate int AddDelegate(int n);

}
