using System;
using System.Collections.Generic;
using System.Text;
using static StudyDemo1.Ticket12306;


delegate int AddDelegate(int n);
delegate T GenericDelegate<T>(T num);

namespace StudyDemo1
{
    /// <summary>
    /// 委托
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

        public static T ShowT<T>(T num)
        {
            Console.WriteLine("{0}", num);
            return num;
        }

        #region 委托使用

        /// <summary>
        /// 使用委托
        /// </summary>
        public static void UseDelegate()
        {
            AddDelegate ad = new AddDelegate(AddNum);  // 委托对象
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

        #endregion

        #region 泛型委托
        public static void UseGeneric()
        {
            // 
            GenericDelegate<int> obj = new GenericDelegate<int>(ShowT);
            obj(123456);
        }

        #endregion

        #region 12306 卖票场景

        public static void Use()
        {
            // 1. 创建12306
            Ticket12306 t = new Ticket12306();
            //t.SellTicket();

            // 2. 创建委托对象
            SellTicketDelegate action = new SellTicketDelegate(t.SellTicket);

            // 3. 创建第三方网站（飞猪）
            TicketPig pig = new TicketPig();
            pig.SellTicket(action);
        }

        #endregion
    }


    public class Ticket12306
    {
        /// <summary>
        /// 卖票委托方法
        /// </summary>
        public delegate void SellTicketDelegate();

        /// <summary>
        /// 卖票方法
        /// </summary>
        public void SellTicket()
        {
            Console.WriteLine("12306 开始买票");
        }
    }

    /// <summary>
    /// 飞猪卖票网站
    /// </summary>
    public class TicketPig
    {
        

        /// <summary>
        /// 卖票（12306委托类）
        /// </summary>
        public void SellTicket(SellTicketDelegate action)
        {
            action();
        }
    }

    
}
