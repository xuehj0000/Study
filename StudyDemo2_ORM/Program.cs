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
                var model = sqlHelper.Find<CompanyModel>(1);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
