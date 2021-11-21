using System;
using System.Collections.Generic;
using System.Text;
using static StudyDemo1.MyEvent;

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

        /// <summary>
        /// 使用事件
        /// </summary>
        public static void Use()
        {
            // 1. 创建目标对象
            MyEvent my = new MyEvent();

            // 2. 获取事件属性（获取委托类型）
            my.showEvent += new ShowEvent(my.Show);    // 订阅事件
            my.Use();                                  // 触发事件
        }

        #region 多病人 多医生监控系统场景
        public static void UseDemo()
        {
            // 1.创建病人对象
            Patient p = new Patient(1,38,0);
            Patient p2 = new Patient(2, 36, 0);

            // 2.创建医生类
            Doctor d = new Doctor();

            // 3.病人事件发布类
            PatientPublish publish = new PatientPublish();
            publish.PublishPatient(p);
            publish.PublishPatient(p2);

            // 4.设置医生监控病人
            publish.PatientHandler += new PatientPublish.PatientDelegate(d.Treatment);

            // 5.开始出发
            publish.OnMonitor();
        }
        #endregion

    }

    public class MyEvent
    {
        /// <summary>
        /// 委托
        /// </summary>
        public delegate void ShowEvent();

        /// <summary>
        /// 发布时间
        /// </summary>
        public event ShowEvent showEvent;

        public void Show()
        {
            Console.WriteLine("show 方法");
        }

        public void Use()
        {
            showEvent();
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

    /// <summary>
    /// 病人类
    /// </summary>
    public class Patient
    {
        /// <summary>
        /// 病人病号
        /// </summary>
        private int Id { get; set; }
        /// <summary>
        /// 病人体温
        /// </summary>
        private int animalHeat;
        /// <summary>
        /// 病人病情
        /// </summary>
        private int condition;
       

        public Patient(int id,int head, int con)
        {
            Id = id;
            animalHeat = head;
            condition = con;
        }
        /// <summary>
        /// 获取体温
        /// </summary>
        public int GetHead()
        {
            return animalHeat;
        }
        /// <summary>
        /// 获取病人情况
        /// </summary>
        public int GetCondition()
        {
            return condition;
        }
    }
    /// <summary>
    /// 医生类
    /// </summary>
    public class Doctor
    {
        /// <summary>
        /// 治疗方法
        /// </summary>
        public void Treatment()
        {
            Console.WriteLine("医生开始治疗！");
        }
    }

    /// <summary>
    /// 病人事件布类
    /// </summary>
    public class PatientPublish
    {
        private List<Patient> list = null;

        /// <summary>
        /// 病人委托
        /// </summary>
        public delegate void PatientDelegate();
        /// <summary>
        /// 病人处理事件
        /// </summary>
        public event PatientDelegate PatientHandler;

        /// <summary>
        /// 发布病人方法
        /// </summary>
        public void PublishPatient(Patient p)
        {
            list.Add(p);
        }

        /// <summary>
        /// 监控中心(病人、医生多对多关系)
        /// </summary>
        public void OnMonitor()
        {
            foreach(var item in list)
            {
                if (item.GetHead() > 37)
                {
                    // 触发医生去操作（处理事件）
                    PatientHandler();
                }
            }
            
        }
    }
}
