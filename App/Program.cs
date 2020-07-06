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
                var course = student.FavoriteCourse;

                var course2 = context.Courses.SingleOrDefault(_ => _.Id == course.Id);

                bool result = course == course2;
                bool result2 = ReferenceEquals(course, course2);
                bool result3 = course.Equals(course2);
                
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
