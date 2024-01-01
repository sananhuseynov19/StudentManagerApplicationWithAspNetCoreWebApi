using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Tokens;
using StudentManagerApplicationWithWebApi.Data.Entities;
using StudentManagerApplicationWithWebApi.DTO;
using StudentManagerApplicationWithWebApi.Model;
using StudentManagerApplicationWithWebApi.Repository;
using StudentManagerApplicationWithWebApi.UnitOfWork;
using System.IdentityModel.Tokens.Jwt;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text;

namespace StudentManagerApplicationWithWebApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        IConfiguration _configuration;
        IRepository<Student> _repository;
        private readonly IUnitOfWork unitOfWork;
        public AuthController(IConfiguration configuration, IRepository<Student> repository, IUnitOfWork _unitOfWork)
        {
            _configuration = configuration;
            _repository = repository;
            unitOfWork = _unitOfWork;
        }

        [HttpPost("Create Token")]
        public async Task<ActionResult<ResultLoginModel>> Login(LoginModel  model)
        {
           
           // var st1 = await unitOfWork.StudentRepository.GetByName(model.Name.ToString());
            var stu = await unitOfWork.StudentRepository.GetById(model.Id);
            
            


            if (stu == null)
            {
                return NotFound(new
                {
                    message = "it is not found"
                });
            }
            var token = GenerateTokenStudent(stu);
            return Ok(new ResultLoginModel
            {
                Id = stu.StudentId,
                token = token
            });

        }




        private string GenerateTokenStudent(Student student)
        {
            var tokenhandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["jwttoken:jwtkey"]);
            var claims = new List<Claim>
                {
                    //new Claim(ClaimTypes.NameIdentifier, student.StudentId.ToString()),
                    new Claim(ClaimTypes.Name, student.Name),
                    new Claim(ClaimTypes.Email, student.Email),
                    new Claim("Age", student.Age.ToString()),
                    new Claim(ClaimTypes.Role,student.Name),
                   
            };

           
            var tokendescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims
                ),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenhandler.CreateToken(tokendescriptor);
            return tokenhandler.WriteToken(token);
        }
    }
}
