using System.Collections.Generic;

namespace StudyDemo1
{
    /// <summary>
    /// 初始化器
    /// 分为 构造函数初始化器 和 对象初始化器
    /// </summary>
    public class T14_Initializer
    {
        public static void Use()
        {
            var student = new Student("first", "last");                            // 构造函数初始化器
            var student2 = new Student { FirstName = "first", LastName = "last" }; // 对象初始化器
            var student3 = new Student("Li", "LI") { Id = 101 };                   // 两者同
        }

        /// <summary>
        /// 匿名类对象初始化器
        /// </summary>
        public static void UseAnonymous()
        {
            var ret = new { Age = 10, Name = "name" };
        }

        /// <summary>
        /// 集合初始化器
        /// </summary>
        public static void CollectionInitialazer()
        {
            var list = new List<Student>
            {
                new Student{FirstName = "first", LastName = "last"}
            };

            Dictionary<int, Student> stuDic = new Dictionary<int, Student>()
            {
                {111, new Student{FirstName = "first", LastName = "last"} }
            };
        }


    }




    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Student() { }

        public Student(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
