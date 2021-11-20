using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace StudyDemo1
{
    /// <summary>
    /// 特性：添加额外的信息
    /// </summary>
    public class T07_Attribute
    {
        [Conditional("DEBUG")]                 // 开发测试时使用
        public static void Message(string msg)
        {
            Console.WriteLine(msg);
        }

        [Obsolete("don't use old method")]     //老旧过时了
        public static void Method()
        {
            Console.WriteLine("方法二！");
        }



        #region 特性信息获取,利用反射

        public static void GetAttribute()
        {
            HelpAttribute help;
            foreach(var attr in typeof(AnyClass).GetCustomAttributes(true))
            {
                help = attr as HelpAttribute;
                if(help != null)
                {
                    Console.WriteLine("anyClass description:{0}", help.Description);
                }
            }
        }

        #endregion
    }

    #region 特性声明

    /// <summary>
    /// 自定义特性类,名称 + Attribute
    /// 系统定义好的特性类：AttributeUsage，适用范围
    /// AllowMultiple 为 true 时，可以放多个
    /// Inherited 为 true 时， 类被继承时，特性也被继承
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple =false, Inherited =false)]
    public class HelpAttribute : Attribute
    {
        protected string _description;
        public HelpAttribute(string description)
        {
            _description = description;
        }

        public string Description
        {
            get
            {
                return this._description;
            }
        }
        /// <summary>
        /// 有名词的属性，使用时，可以直接用name赋值，可有可无
        /// </summary>
        public string Name { get; set; }
    }

    [Help("this is a do-nothing class", Name = "名字信息")]
    public class AnyClass
    {

    }

    #endregion

}
