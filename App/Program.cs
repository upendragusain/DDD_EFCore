using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;

namespace App
{
    public class Program
    {
        static void Main(string[] args)
        {
            string connectionString = GetConnectionString();
            ILoggerFactory loggerFactory = CreateLoggerFactory();

            var optionsBuilder = new DbContextOptionsBuilder<SchoolContext>();
            optionsBuilder
                .UseSqlServer(connectionString)
                .UseLoggerFactory(loggerFactory)
                .EnableSensitiveDataLogging();

            using (var context = new SchoolContext(optionsBuilder.Options))
            {
                Student student = context.Students.Find(1L);
            }
        }

        private static ILoggerFactory CreateLoggerFactory()
        {
            return LoggerFactory.Create(builder =>
            {
                builder
                    .AddFilter((category, level) =>
                        category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information)
                    .AddConsole();
            });
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
