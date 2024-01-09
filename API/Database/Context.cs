using API.Models;
using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations.Schema;

namespace API.Database
{
	public class Context : DbContext
	{
        public DbSet<AudienceType> AudienceType { get; set; }
        public DbSet<Audience> Audience { get; set; }

        public DbSet<Subject> Subject { get; set; }

        public DbSet<Speciality> Speciality { get; set; }
        public DbSet<Group> Group { get; set; }

        public DbSet<Teacher> Teacher { get; set; }
        public DbSet<TeacherSubject> TeacherSubject { get; set; }

        public DbSet<LessonPlan> LessonPlan { get; set; }
        public DbSet<LessonTeacher> LessonTeacher { get; set; }

        
        public Context(DbContextOptions<Context> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Speciality>().HasData(
            //    new Speciality[]
            //    {
            //        new Speciality{Id = 1, Name="09.02.07"},
            //        new Speciality{Id = 2, Name="09.02.01"},
            //        new Speciality{Id = 3, Name="09.02.06"},
            //        new Speciality{Id = 4, Name="09.02.07"},
            //        new Speciality{Id = 5, Name="10.02.05"},
            //        new Speciality{Id = 6, Name="13.02.11"},
            //        new Speciality{Id = 7, Name="15.02.14"},
            //        new Speciality{Id = 8, Name="15.01.17"},
            //        new Speciality{Id = 9, Name="08.01.18"},
            //        new Speciality{Id = 10, Name="15.01.31"},
            //    });
        }
    }
}
