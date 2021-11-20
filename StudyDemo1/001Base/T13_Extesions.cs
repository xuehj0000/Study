using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace StudyDemo1
{
    /// <summary>
    /// 扩展方法
    /// </summary>
    public class T13_Extesions
    {
        /// <summary>
        /// 使用扩展方法
        /// </summary>
        public static void UseEntensions()
        {
            int[] arr = { 10, 45, 15, 39, 21, 26 };

            var ret = arr.OrderBy(r => r);
            foreach(var item in ret)
            {
                Console.Write(item);
            }
        }

        /// <summary>
        /// 使用枚举扩展方法
        /// </summary>
        public static void UseEnumExtensions()
        {
            SExtensions.minPassing = Grades.C;
            var g1 = Grades.D;
            var ret = g1.Passing();
            Console.Write(ret);
        }

    }


    /// <summary>
    /// 定义扩展方法
    /// 必须写在静态类里，静态方法，this 开头，对哪个类型进行扩展
    /// </summary>
    public static class SExtensions
    {
        public static int WordCount(this string str)
        {
            return str.Split(new char[] { ' ', '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries).Length;
        }

        public static Grades minPassing = Grades.D;

        /// <summary>
        /// 枚举类型扩展方法
        /// </summary>
        public static bool Passing(this Grades grade)
        {
            return grade >= minPassing;
        }

    }

    public enum Grades { F=0, D=1, C=2,B=3,A=4};

}
