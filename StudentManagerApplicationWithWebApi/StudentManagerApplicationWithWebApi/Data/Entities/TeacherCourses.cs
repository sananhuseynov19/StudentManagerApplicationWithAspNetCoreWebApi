using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagerApplicationWithWebApi.Data.Entities
{
    public class TeacherCourses
    {
        public int TeacherId { get; set; }
        public int CourseId { get; set; }

        [ForeignKey(nameof(TeacherId))]
        public Teacher? Teachers { get; set; }
        [ForeignKey(nameof(CourseId))]
        public Course? Courses { get; set; }
    }
}
