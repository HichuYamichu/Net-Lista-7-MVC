using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace L7.Models {
    public class Enrollment {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }

        public ICollection<Grade> Grades { get; set; } = null!;
        public Student Student { get; set; } = null!;
        public Course Course { get; set; } = null!;
    }
}
