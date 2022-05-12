using System.ComponentModel.DataAnnotations;

namespace L7.Models {
    public class Course {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Date")]
        public DateTime StartDate;
        public int SubjectId { get; set; }
        public int InstructorId { get; set; }

        public Subject Subject { get; set; } = null!;
        public Instructor Instructor { get; set; } = null!;
        public ICollection<Enrollment> Enrollments { get; set; } = null!;
    }
}
