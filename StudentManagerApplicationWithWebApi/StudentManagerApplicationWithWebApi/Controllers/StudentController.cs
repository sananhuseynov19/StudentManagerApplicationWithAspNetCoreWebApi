using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagerApplicationWithWebApi.Data;
using StudentManagerApplicationWithWebApi.Data.Entities;
using StudentManagerApplicationWithWebApi.DTO;
using StudentManagerApplicationWithWebApi.Helpers;
using StudentManagerApplicationWithWebApi.Repository;
using StudentManagerApplicationWithWebApi.UnitOfWork;

namespace StudentManagerApplicationWithWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {

      
        private readonly IRepository<Student> studentRepository;
        private readonly IUnitOfWork unitOfWork;

        public StudentController(IRepository<Student> studentRepository,IUnitOfWork unitOfWork)
        {
          
            this.studentRepository=studentRepository;
            this.unitOfWork=unitOfWork;
        }


        [HttpGet("Get")]
        // [MyAuthorizeAttribute]
       // [Authorize("SananOnly")]

        public async Task<object> GetStudents()
        {

            var user = HttpContext.User;

            var st = await studentRepository.Include(s =>s.Gender).Select(s => new
            {
                s.StudentId,
                s.Name,
                s.Surname,
                s.DateOfBirth,
                genderName = s.Gender.Name,
                courseName=s.StudentCourse.Select(c=>c.Courses.Name),
                teacherName=s.TeacherStudents.Select(t=>t.Teachers.Name)
            }).ToListAsync();

            return st;
            
        }

        [HttpPost("Create")]
        public async Task<Student> CreateStudent(StudentDto studentDto)
        {
            var student = new Student()
            {
                Name = studentDto.Name,
                Surname = studentDto.Surname,
                DateOfBirth = studentDto.DateOfBirth,
                Email = studentDto.Email,
                GenderId = studentDto.GenderId,
                Phone = studentDto.Phone,
                Age=studentDto.Age,
               
            };
            await studentRepository.Create(student);
            studentRepository.Commit();
            return student;

        }

        [HttpPut("Update")]
        [Authorize(Policy = "CanUpdate")]
        public async Task<Student> UpdateStudent(int id, string Name,string Surname)
        {
            var student = await unitOfWork.StudentRepository.GetById(id);
            student.Name = Name;
            student.Surname = Surname;
            await unitOfWork.StudentRepository.Update(student);
           await  unitOfWork.Commit();

            return student;
        }

        [HttpDelete("Delete")]

        public async Task DeleteCourse(int id)
        {
            await studentRepository.Delete(id);
            studentRepository.Commit();
           
        }
    }
}
