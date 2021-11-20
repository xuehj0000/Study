using System;
using System.Collections.Generic;
using System.Text;

namespace StudyDemo1
{
    /// <summary>
    /// 事件，通知程序发生了一些事情，最常用在图形化交互的界面中，winform项目中
    /// 事件声明
    /// 事件触发
    /// 事件可被继承
    /// </summary>
    public class T05_Event
    {
        private int value;
        public delegate void EventDelegate();  
        public event EventDelegate ChangeNum;   // 事件


        public T05_Event(int n)
        {
            SetValue(n);
        }

        /// <summary>
        /// 数据改变时，触发的事件
        /// </summary>
        public virtual void OnNumChanged()     // virtual 虚方法，可再派生类中被重写
        {
            if(ChangeNum != null)
            {
                ChangeNum();
            }
            else
            {
                Console.WriteLine("Event fired!");
            }
        }

        public void SetValue(int n)
        {
            if(value != n)
            {
                value = n;
                OnNumChanged();
            }
        }

        public static void ChangeMethod()
        {
            Console.WriteLine("binded event fire!");
        }

    }



    public delegate void MyDelegate();

    public interface I
    {
        event MyDelegate MyEvent;
        event EventHandler MyGuidlineEvent;   // 推荐格式
        void FireAway();
    }


    public class MyClass : I
    {
        public event MyDelegate MyEvent;
        public event EventHandler MyGuidlineEvent;

        public void FireAway()
        {
            if (MyEvent != null)
            {
                MyEvent();
            }
        }
    }

    // .net 推荐使用下方这种方式,即 EventHandler，绑定委托，最好使用自带的 EventHandler
    public delegate void ChangedEventHandler(object sender, EventArgs e);

    // EventHandler
}
