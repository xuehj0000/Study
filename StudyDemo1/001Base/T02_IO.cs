using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace StudyDemo1
{
    public class T02_IO
    {

        public static void M()
        {
            // 
            var path = @"C:\hello.txt";
            var path2 = @"C:\";
            File.Exists(path);               // 文件是否存在
            Directory.Exists(path2);         // 文件夹是否存在
        }

        /// <summary>
        /// 获取程序当前路径的可执行文件
        /// </summary>
        /// <param name="args">程序的外部参数</param>
        /// <returns></returns>
        public static void GetFiles(string[] args)
        {
            var path = ".";
            if (args.Length > 0)
            {
                if (Directory.Exists(args[0]))
                {
                    path = args[0];
                }
                else
                {
                    Console.WriteLine("{0}not found; using current directory;", args[0]);
                }
            }

            DirectoryInfo dir = new DirectoryInfo(path);
            foreach (FileInfo f in dir.GetFiles("*.exe"))
            {
                var name = f.Name;
                var size = f.Length;
                DateTime createTime = f.CreationTime;
                Console.WriteLine("{0, -12:NO} {1, -20:g} {2}", size, createTime, name);
            }
            Console.ReadLine();
        }

        /// <summary>
        /// 文件存在，输出存在
        /// 文件不存在，创建文件，写入内容
        /// </summary>
        public static void Write(string path)
        {
            if (File.Exists(path))                                    // 如果文件存在，
            {
                Console.WriteLine("{0} 以存在", path);
                Console.ReadLine();
                return;
            }

            // 文件流
            FileStream fs = new FileStream(path, FileMode.Create);    // 创建文件
            // 字节写入
            BinaryWriter bw = new BinaryWriter(fs);                   // 将二进制的基元类型写入流，并支持以特定编码写入字符串。

            for(int i = 0; i < 11; i++)
            {
                bw.Write("a");    
            }
            bw.Close();
            fs.Close();
        }

        /// <summary>
        /// StreamWriter，写入IO文件 
        /// </summary>
        public static void WriteBySW(string path)
        {
            using (StreamWriter sw = File.AppendText(path))  //打开文件
            {
                Log("呵呵", sw);
                Log("hello jissdf", sw);

                sw.Close();
                // 放入using 后，不用dispose 释放；
                // 区别：close 后还可再open，dispose 就必须再create一次
            }

        }

        /// <summary>
        /// 写入信息
        /// </summary>
        /// <param name="logMsg"></param>
        /// <param name="w"></param>
        public static void Log(string logMsg, TextWriter w)
        {
            w.Write("\r\nLog Entry:");
            w.WriteLine(" :{0}", logMsg);
            w.Flush();                      // 清除当前写入程序的所有缓冲区，并将所有缓冲数据写入基础设备。
        } 


        /// <summary>
        /// 文件读操作
        /// </summary>
        public static void Read(string path)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("{0} does not exist!", path);
                Console.ReadLine();
                return;
            }
            FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            for (int i = 0; i < 11; i++)
            {
                Console.WriteLine(br.ReadString());
            }
            br.Close();
            fs.Close();
            Console.ReadLine();
        }

        /// <summary>
        /// StreamReader, 读取IO文件
        /// </summary>
        public static void ReadBySR(string path)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("{0} does not exist!", path);
                Console.ReadLine();
                return;
            }

            using (StreamReader sr = File.OpenText(path))
            {
                string input;
                while((input = sr.ReadLine())!= null)
                {
                    Console.WriteLine(input);
                }
                Console.WriteLine("The end of the stream");
                sr.Close();
            }
            Console.ReadLine();
        }
    }
}
