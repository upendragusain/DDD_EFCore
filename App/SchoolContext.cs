using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace App
{
    public sealed class SchoolContext : DbContext
    {
        private readonly string _conectionString;
        private readonly bool _useConsoleLogger;

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }

        public SchoolContext()
        {

        }

        public SchoolContext(string conectionString, bool useConsoleLogger)
        {
            this._conectionString = conectionString;
            this._useConsoleLogger = useConsoleLogger;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddFilter((category, loglevel) =>
                    category == DbLoggerCategory.Database.Command.Name && loglevel == LogLevel.Information)
                .AddConsole();
            });

            optionsBuilder
                .UseSqlServer(_conectionString);

            if (_useConsoleLogger)
            {
                optionsBuilder
                    .UseLoggerFactory(loggerFactory)
                    .EnableSensitiveDataLogging();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>(x =>
            {
                x.ToTable("Student").HasKey(k => k.Id);
                x.Property(p => p.Id).HasColumnName("StudentID");
                x.Property(p => p.Email);
                x.Property(p => p.Name);
                x.Property(p => p.FavoriteCourseId);
            });

            modelBuilder.Entity<Course>(x =>
            {
                x.ToTable("Course").HasKey(k => k.Id);
                x.Property(p => p.Id).HasColumnName("CourseID");
                x.Property(p => p.Name);
            });
        }
    }
}
