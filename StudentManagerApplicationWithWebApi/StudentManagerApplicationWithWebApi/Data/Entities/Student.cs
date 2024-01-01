using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagerApplicationWithWebApi.Data.Entities
{
    public class Student
    {
        [Key]
      //  [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudentId { get; set; }
        [Required]
      //  [MaxLength(25)]
        public string Name { get; set; }
       // [Required]
       // [MaxLength(25)]
        public string Surname { get; set; }
      //  [Required]
      //  [MaxLength(10)]
        public int Age { get; set; }
     //   [MaxLength(10)]
        public DateTime DateOfBirth { get; set; }
        public int Phone { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public int GenderId { get; set; }
        [ForeignKey(nameof(GenderId))]
        public Gender Gender { get; set; }
        public ICollection<StudentCourse> StudentCourse { get; set; }
        public ICollection<TeacherStudent> TeacherStudents { get; set; }

    }
}
