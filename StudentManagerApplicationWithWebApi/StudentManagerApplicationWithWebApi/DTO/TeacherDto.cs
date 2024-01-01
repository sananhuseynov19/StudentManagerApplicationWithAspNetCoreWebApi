using System.ComponentModel.DataAnnotations;

namespace StudentManagerApplicationWithWebApi.DTO
{
    public class TeacherDto
    {
        public string Name { get; set; }
       
        public string Surname { get; set; }
        
        public int Age { get; set; }
       
        public int Phone { get; set; }
     
        public string Email { get; set; }
        public int GenderId { get; set; }
    }
}
