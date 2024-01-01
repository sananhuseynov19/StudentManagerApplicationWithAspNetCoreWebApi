using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagerApplicationWithWebApi.Data.Entities
{
    public class Teacher
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TeacherId { get; set; }
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }
        [Required]
        [MaxLength(25)]
        public string Surname { get; set; }
        [MaxLength(15)]
        public int Age { get; set; }
        [MaxLength(15)]
        public int Phone { get; set; }
        [Required]
        public string Email { get; set; }
        public int GenderId { get; set; }
        [ForeignKey(nameof(GenderId))]
        public Gender Gender { get; set; }
        public ICollection<TeacherStudent> TeacherStudents { get; set; }
        public ICollection<TeacherCourses> TeacherCourses { get; set; }

    }

}
