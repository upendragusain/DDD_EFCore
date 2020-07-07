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
                Student student = context.Students.Find(1L);
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
