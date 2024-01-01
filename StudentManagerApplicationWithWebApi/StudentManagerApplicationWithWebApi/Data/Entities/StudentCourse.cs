using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagerApplicationWithWebApi.Data.Entities
{
    public class StudentCourse
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }

        [ForeignKey(nameof(StudentId))]
        public Student Students { get; set; }
        [ForeignKey(nameof(CourseId))]
          public Course  Courses { get; set; }
    }
}
