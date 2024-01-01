using StudentManagerApplicationWithWebApi.Repository;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagerApplicationWithWebApi.Data.Entities
{
    public class Course
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
       public int CourseId { get; set; }
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public ICollection<StudentCourse>? StudentCourses { get; set; }
        public ICollection<TeacherCourses>? TeacherCourses { get; set; }
    }
}
