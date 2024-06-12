using API.Models;
using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations.Schema;

namespace API.Database
{
	public class Context : DbContext
	{
        public DbSet<ScheduleStatus> ScheduleStatuses { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Change> Changes { get; set; }
        public DbSet<Classroom> Classrooms { get; set; }
        public DbSet<ClassroomType> ClassroomTypes { get; set; }
        public DbSet<LessonGroup> LessonGroups { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<TeacherSubject> TeacherSubjects { get; set; }
        public DbSet<LessonGroupTeacher> LessonGroupTeachers { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Group> Groups { get; set; }



        public DbSet<YearBegin> YearBegin { get; set; }

        //Auth
        public DbSet<UserAuthData> UserAuthData { get; set; }
        public DbSet<User> Users { get; set; }


        public Context(DbContextOptions<Context> options) : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LessonGroupTeacher>()
            .HasKey(lgt => new { lgt.LessonGroupId, lgt.TeacherId });

            modelBuilder.Entity<TeacherSubject>()
                .HasKey(ts => new { ts.SubjectId, ts.TeacherId });

            base.OnModelCreating(modelBuilder);
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
