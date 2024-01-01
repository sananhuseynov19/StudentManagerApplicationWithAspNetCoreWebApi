using StudentManagerApplicationWithWebApi.Data;
using StudentManagerApplicationWithWebApi.Data.Entities;
using StudentManagerApplicationWithWebApi.Repository;

namespace StudentManagerApplicationWithWebApi.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {

        public AppDbContext dbContext { get; set; }

        public UnitOfWork(AppDbContext dbContext, IRepository<Teacher> TeacherRepository)
        {
            this.dbContext=dbContext;
            StudentRepository = new EFRepository<Student>(dbContext);
            this.TeacherRepository = TeacherRepository;
            CourseRepository=new EFRepository<Course>(dbContext);   
        }
       
        public IRepository<Student> StudentRepository { get; set; }
        public IRepository<Teacher> TeacherRepository { get; set; }
        public IRepository<Course> CourseRepository { get; set; }
        public async Task Commit()
        {
           await  dbContext.SaveChangesAsync();
        }
    }
}
