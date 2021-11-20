using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace StudyDemo1
{
    /// <summary>
    /// 正则表达式：匹配字符串
    /// </summary>
    public class T10_Regex
    {

        // 使用方式：静态方法
        //           实例化对象使用
        public static void Use()
        {
            string pattern = @"^\d{3}-\d{2}-\d{4}$";

            string[] values = { "111-22-3333", "111-2-3333"};
            foreach(var item in values)
            {
                if(Regex.IsMatch(item, pattern))
                {
                    Console.WriteLine("{0} is valid", item);
                }
                else
                {
                    Console.WriteLine("{0} is not valid", item);
                }
            }
        }

        /// <summary>
        /// 获取匹配--方法一
        /// </summary>
        public static void RegexMatch()
        {
            var input = "This is jikexueyuan jikexueyuan !";
            var patter = @"\b(\w+)\W(\1)\b";
            Match match = Regex.Match(input, patter);
            while (match.Success)
            {
                Console.WriteLine("Duplication {0} found", match.Groups[1].Value);
                match = match.NextMatch();
            }
        }

        /// <summary>
        /// 利用正则替换字符串效果
        /// </summary>
        public static void RegexReplace()
        {
            var pattern = @"\b\d+\.\d{2}\b";
            var replacement = "$$$&";

            var str = "Total cost:103.64";
            var ret = Regex.Replace(str, pattern, replacement);

            Console.WriteLine(ret);
        }

        /// <summary>
        /// 利用正则进行分割
        /// </summary>
        public static void RegexSplit()
        {
            var str = "1.Egg 2.Bread 3.Mike 4.Coffee";
            var patter = @"\b\d{1,2}\.\s";
            foreach(var item in Regex.Split(str, patter))
            {
                if (!string.IsNullOrEmpty(item))
                {
                    Console.WriteLine(item);
                }
            }
        }

        /// <summary>
        /// 获取匹配--方法二，集合信息
        /// </summary>
        public static void Matches()
        {
            Regex reg = new Regex("abc");
            MatchCollection matches = reg.Matches("123abc4abcd");
            foreach(Match item in matches)
            {
                Console.WriteLine("{0} found at position {1}", item.Value, item.Index);

                Console.WriteLine("{0}", item.Result("$&, hello jikexueyuan"));
            }
        }

        /// <summary>
        /// 获取匹配的 Groups 值
        /// </summary>
        public static void Groups()
        {
            var str = "Born: July 28, 1989";
            string patter = @"\b(\w)\s(\d{1,2}),\s(\d{4})\b";
            Match match = Regex.Match(str, patter);
            if (match.Success)
            {
                for (int i = 0; i < match.Groups.Count; i++)
                {
                    Console.WriteLine("Group {0}:{1}", i, match.Groups[i].Value);
                }
            }
        }
    }
}
