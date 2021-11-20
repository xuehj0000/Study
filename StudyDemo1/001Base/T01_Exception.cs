using System;
using System.IO;

namespace StudyDemo1
{
    /// <summary>
    /// 异常
    /// </summary>
    public class _01Exception
    {
        public static void ShowException() 
        {
            int x = 0;
            try
            {
                int y = 100 / x;

            }catch(NullReferenceException e) //System.Exception
            {
                Console.WriteLine(e.Message);
                throw;
            }
            finally
            {
                Console.WriteLine("总是执行");
            }

            var ae = new ArgumentException();                // 参数异常
            var ane = new ArgumentNullException();           // 参数为空异常
            var aore = new ArgumentOutOfRangeException();    // 参数超出范围异常

            var done = new DirectoryNotFoundException();     // IO 路径没有找到异常
            var fne = new FileNotFoundException();           // IO 文件没有找到异常
            var ioe = new InvalidOperationException();       // 非法运算符异常

            var nie = new NotImplementedException();        // 未实现异常

        }


    }
}
