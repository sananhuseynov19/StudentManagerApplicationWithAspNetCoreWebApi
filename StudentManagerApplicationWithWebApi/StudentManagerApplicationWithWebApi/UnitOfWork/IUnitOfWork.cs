using StudentManagerApplicationWithWebApi.Data.Entities;
using StudentManagerApplicationWithWebApi.Repository;

namespace StudentManagerApplicationWithWebApi.UnitOfWork
{
    public interface IUnitOfWork
    {

       public  Task Commit();

        IRepository<Student> StudentRepository { get; set; }
        IRepository<Teacher> TeacherRepository { get; set;}
        IRepository<Course> CourseRepository { get; set; }
    }
}
