using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagerApplicationWithWebApi.Data.Entities
{
    public class TeacherStudent
    {
        
        public int StudentId { get; set; }

        public int TeacherId { get; set; }
        [ForeignKey(nameof(StudentId))]
        public Student? Students { get; set; }
        [ForeignKey(nameof(TeacherId))]
        public Teacher? Teachers { get; set; }   
    }
}
