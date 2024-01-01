using Microsoft.AspNetCore.Mvc;
using StudentManagerApplicationWithWebApi.Data.Entities;
using StudentManagerApplicationWithWebApi.Data;
using Microsoft.EntityFrameworkCore;
using StudentManagerApplicationWithWebApi.DTO;
using StudentManagerApplicationWithWebApi.Repository;
using StudentManagerApplicationWithWebApi.UnitOfWork;

namespace StudentManagerApplicationWithWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeacherController : ControllerBase
    {
        public AppDbContext dbContext;
        private readonly IRepository<Teacher> _teacherRepository;
        private readonly IUnitOfWork unitOfWork;

        public TeacherController(AppDbContext dbContext, IRepository<Teacher> teacherRepository, IUnitOfWork unitOfWork)
        {
            this.dbContext=dbContext;
            _teacherRepository = teacherRepository;
            this.unitOfWork=unitOfWork;
        }


        [HttpGet("Get")]
        public async Task<object> GetTeachers()
        {


            var query = from t in dbContext.Teachers
                        join g in dbContext.Genders on t.GenderId equals g.GenderId into genderGroup
                        from g in genderGroup.DefaultIfEmpty()
                        join ts in dbContext.TeacherStudents on t.TeacherId equals ts.TeacherId into teacherStudentGroup
                        from ts in teacherStudentGroup.DefaultIfEmpty()
                        join s in dbContext.Students on ts.StudentId equals s.StudentId into studentGroup
                        from s in studentGroup.DefaultIfEmpty()
                        join tc in dbContext.TeacherCourses on t.TeacherId equals tc.TeacherId into  teacherCourseGroup
                        from tc in teacherCourseGroup.DefaultIfEmpty()
                        join c in dbContext.Courses on tc.CourseId equals c.CourseId into courseGroup
                        from c in courseGroup.DefaultIfEmpty()
                        select new
                        {
                            t.TeacherId,
                            t.Name,
                            t.Surname,
                            t.Age,
                            t.Phone,
                            gn=g.Name,
                            cr = c.Name,
                           st = s.Name
                         
                        };

            return await query.ToListAsync();
        }

        [HttpPost("Create")]
        public async Task<Teacher> CreateTeacher(TeacherDto teacherDto)
        {
            var teacher = new Teacher()
            {
                Name = teacherDto.Name,
                Surname = teacherDto.Surname,
                Email = teacherDto.Email,
                GenderId = teacherDto.GenderId,
                Phone = teacherDto.Phone,
            };
            await unitOfWork.TeacherRepository.Create(teacher);
          await unitOfWork.Commit();
            return teacher;

        }
        [HttpPut("Update")]
        public async Task<Teacher> UpdateTeacher(int id, string Name, string Surname)
        {
            var teacher = await _teacherRepository.GetById(id);
            teacher.Name = Name;
            teacher.Surname = Surname;
           await  _teacherRepository.Update(teacher);
            _teacherRepository.Commit();

            return teacher;
        }

        [HttpDelete("Delete")]

        public async Task DeleteTeacher(int id)
        {
          await _teacherRepository.Delete(id);
            _teacherRepository.Commit();

        }
    }
}
