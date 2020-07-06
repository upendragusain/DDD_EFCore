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
                Console.WriteLine($"w/o include loading: {student?.FavoriteCourse?.Name}");

                // once the below line is executed
                //var student2 = context.Students
                //    .Include(s => s.FavoriteCourse)
                //    .SingleOrDefault(x => x.Id == 1);

                //Console.WriteLine(student2.FavoriteCourse.Name);
                Console.WriteLine($"After include loading, context is updated: {student?.FavoriteCourse?.Name}");
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
