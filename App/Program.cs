using Microsoft.Extensions.Configuration;
using System;

namespace App
{
    public class Program
    {
        static void Main(string[] args)
        {
            string connectionString = GetConnectionString();

            using (var context = new SchoolContext(connectionString, true))
            {
                Student student = context.Students.Find(1L);
                Console.WriteLine(student.Email);
            }

            Console.ReadLine();
        }

        private static string GetConnectionString()
        {
            IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();

            return config["ConnectionString"];
        }
    }
}
