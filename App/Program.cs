using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace App
{
    public class Program
    {
        static void Main(string[] args)
        {
            string connectionString = GetConnectionString();

            using (var context = new SchoolContext(connectionString, true))
            {
                Student student1 = context.Students.Find(1L);
                Console.WriteLine(student1.Email);

                var s1 = context.Students.Single(_ => _.Id == 1L);// TOP(2)
                var s2 = context.Students.SingleOrDefault(_ => _.Id == 1L);// TOP(2)
                var s3 = context.Students.First(_ => _.Id == 1L);// TOP(1)
                var s4 = context.Students.FirstOrDefault(_ => _.Id == 1L);// TOP(1)

                // from cache
                Student student2 = context.Students.Find(1L);
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
