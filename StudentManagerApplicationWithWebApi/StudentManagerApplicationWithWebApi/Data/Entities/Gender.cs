using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace StudentManagerApplicationWithWebApi.Data.Entities
{
    public class Gender
    {
        [Key]
        public int GenderId { get; set; }
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }

        public ICollection<Student> Students { get; set; }
        public ICollection<Teacher> Teachers { get; set; }
    }
}
