using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagerApplicationWithWebApi.Data;
using StudentManagerApplicationWithWebApi.Data.Entities;
using StudentManagerApplicationWithWebApi.DTO;
using StudentManagerApplicationWithWebApi.Repository;
using StudentManagerApplicationWithWebApi.UnitOfWork;

namespace StudentManagerApplicationWithWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
       
      //  public AppDbContext dbContext;
        public IRepository<Course> _repository;
        private readonly IUnitOfWork _unitOfWork;
       public  ILogger<CourseController> _logger;

        public CourseController(IRepository<Course> repository, IUnitOfWork unitOfWork, ILogger<CourseController> logger)
        {
          
            _repository = repository;
            _unitOfWork = unitOfWork;
            _logger = logger;

        }


        [HttpGet("Get")]
        public async Task<object> GetCourse()
        {
            _logger.LogInformation("Request accepted for getting all courses at {date}", DateTime.Now);
          var listOfCourse= await _repository.Include(s=>s.StudentCourses).Include(tc=>tc.TeacherCourses).Select(c =>new 
            {
              c.CourseId,
               c.Name,
              std=c.StudentCourses.Select(sc=>sc.Students.Name),
              tch = c.TeacherCourses.Select(tc => tc.Teachers.Name),
              c.StartDate,
              c.EndDate,


          }).ToListAsync();

            return listOfCourse;
        }
       

        [HttpPost("Create")]
        public  async Task<Course> CreateCourse(CourseDto courseDto)
        {
            var course = new Course()
            {
                Name = courseDto.Name,
                StartDate=courseDto.StartingDate,
                EndDate=courseDto.EndingDate
            };
            await  _unitOfWork.CourseRepository.Create(course);
           await  _unitOfWork.Commit();
            return course;

        }

        [HttpPut("Update")]
        public async Task<Course> UpdateCourse(int id ,string Name)
        {
            try {
                if (_logger.IsEnabled(logLevel: LogLevel.Debug))
                {
                    _logger.LogDebug("Request is accepted for updating Course with id:{id}", id);
                }
                var course = await _repository.GetById(id);

                _logger.LogInformation("Course is fetcehed with id {id}", id);
                course.Name = Name;

                await _repository.Update(course);
                if (_logger.IsEnabled(logLevel: LogLevel.Debug))
                {
                    _logger.LogDebug("Course is updated succesfuly at {date}", DateTime.Now);
                }
                _repository.Commit();
                _logger.LogWarning("Request is completed and transaction commited succesfuly");

                return course;
            }
            catch(Exception exc)
            {
                _logger.LogError(exc,"Error happen when updating course with id:{id} at {date}", id, DateTime.Now);
                throw;
            
            }
        }

        [HttpDelete("Delete")]

        public async Task DeleteCourse(int id)
        {
             await _repository.Delete(id);
           
             _repository.Commit();

        }
    }
}
