using System;

namespace StudyDemo1
{
    class Program
    {
        static void Main(string[] args)
        {
            QuartZManager.Init().GetAwaiter().GetResult() ;
            Console.ReadLine();
        }
    }
}
