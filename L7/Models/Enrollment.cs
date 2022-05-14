using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace L7.Models {
    public class Enrollment {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }

        public ICollection<Grade>? Grades { get; set; }
        public Student? Student { get; set; }
        public Course? Course { get; set; }
    }
}
