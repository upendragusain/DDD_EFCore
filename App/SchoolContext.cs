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

                // has one course which has many students
                // many to one (many studenst may have the same favourite course)
                // .WithMany(c => c.Students) will set the navigation property on course entity back to student entity
                // getting all the students who have that curse as thier fav course
                // but we don't want that (only student to course)
                x.HasOne(p => p.FavoriteCourse).WithMany();
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
