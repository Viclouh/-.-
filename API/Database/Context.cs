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
            //Database.EnsureDeleted();
            Database.EnsureCreated();

            ScheduleStatus[] statuses = new ScheduleStatus[]
            {
                new ScheduleStatus{Id=1, Name="Завершенное"},
                new ScheduleStatus{Id=2, Name="Черновик"},
                new ScheduleStatus{Id=3, Name="Активное"},
                new ScheduleStatus{Id=4, Name="Сгенерированное"},
                new ScheduleStatus{Id=5, Name="Экспортированное"},
            };
            foreach (var status in statuses)
            {
                var existingStatus = ScheduleStatuses.Find(status.Id);
                if (existingStatus == null)
                {
                    ScheduleStatuses.Add(status);
                }
                else
                {
                    existingStatus.Name = status.Name;
                }
            }
            SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LessonGroupTeacher>()
            .HasKey(lgt => new { lgt.LessonGroupId, lgt.TeacherId });

            modelBuilder.Entity<TeacherSubject>()
                .HasKey(ts => new { ts.SubjectId, ts.TeacherId });

            
            modelBuilder.Entity<ScheduleStatus>().HasData(
                new ScheduleStatus[]
                    {
                        new ScheduleStatus{Id=1, Name="Завершенное"},
                        new ScheduleStatus{Id=2, Name="Черновик"},
                        new ScheduleStatus{Id=3, Name="Активное"},
                        new ScheduleStatus{Id=4, Name="Сгенерированное"},
                        new ScheduleStatus{Id=5, Name="Экспортированное"},
                    }
                );
            base.OnModelCreating(modelBuilder);
        }
    }
}
