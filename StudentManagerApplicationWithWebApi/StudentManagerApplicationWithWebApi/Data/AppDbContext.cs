using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StudentManagerApplicationWithWebApi.Data.Entities;

namespace StudentManagerApplicationWithWebApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<TeacherStudent> TeacherStudents { get; set; }
        public DbSet<TeacherCourses> TeacherCourses { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
       
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>().ToTable("Students"); 
            modelBuilder.Entity<Gender>().ToTable("Genders");
            modelBuilder.Entity<StudentCourse>().ToTable("StudentCourse");
            modelBuilder.Entity<TeacherStudent>().ToTable("TeacherStudent");
            modelBuilder.Entity<TeacherCourses>().ToTable("TeacherCourse");
            

            modelBuilder.Entity<StudentCourse>().HasKey(x => new { x.StudentId, x.CourseId });
            modelBuilder.Entity<TeacherCourses>().HasKey(x => new { x.TeacherId, x.CourseId });
            modelBuilder.Entity<TeacherStudent>().HasKey(x => new { x.TeacherId, x.StudentId });

            modelBuilder.Entity<Student>().HasOne(x => x.Gender).WithMany(x => x.Students);
            modelBuilder.Entity<Student>().HasMany(x => x.StudentCourse).WithOne(x => x.Students);
          //  modelBuilder.Entity<Student>().HasMany(x => x.TeacherStudents).WithOne(x => x.Students).HasForeignKey(x => x.StudentId);
            

            modelBuilder.Entity<Teacher>(x =>
            {
                x.ToTable("Teachers");
                x.HasOne(x => x.Gender).WithMany(x => x.Teachers);
              //  x.HasMany(x => x.TeacherStudents).WithOne(x => x.Teachers).HasForeignKey(x=>x.TeacherId).OnDelete(DeleteBehavior.NoAction); ;
                x.HasMany(x => x.TeacherCourses).WithOne(x => x.Teachers).OnDelete(DeleteBehavior.NoAction);


            });
            modelBuilder.Entity<TeacherStudent>(x =>
            {
                x.HasOne(x => x.Teachers).WithMany(x => x.TeacherStudents).HasForeignKey(x=>x.TeacherId).OnDelete(DeleteBehavior.NoAction);
                x.HasOne(x => x.Students).WithMany(x => x.TeacherStudents).HasForeignKey(x => x.StudentId).OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Course>(x =>
            {
                x.ToTable("Courses");
                x.HasMany(x => x.StudentCourses).WithOne(x => x.Courses).OnDelete(DeleteBehavior.NoAction);
                x.HasMany(x => x.TeacherCourses).WithOne(x => x.Courses).OnDelete(DeleteBehavior.NoAction);
            });

        }
    }
}
