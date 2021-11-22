using System;

namespace StudyDemo2_ORM
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                SqlHelper sqlHelper = new SqlHelper();
                var user = sqlHelper.Find<User>(1);
                user.Name = "111111";
                user.Status = 3;
                sqlHelper.Update(user);


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
