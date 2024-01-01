using StudentManagerApplicationWithWebApi.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace StudentManagerApplicationWithWebApi.DTO
{
    public class StudentDto
    {
      
        public string Name { get; set; }
        public string Surname { get; set; }    
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; } 
        public int Phone { get; set; }
        public int GenderId { get; set; }
        public int Age { get; set; }
    }
}
