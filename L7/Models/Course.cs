using System.ComponentModel.DataAnnotations;

namespace L7.Models {
    public class Course {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        public int SubjectId { get; set; }
        public int InstructorId { get; set; }

        public string Title {
            get {
                if (Subject == null) {
                    return string.Empty;
                }
                return $"{Subject!.Title} {StartDate} {EndDate}";
            }
        }

        public Subject? Subject { get; set; }
        public Instructor? Instructor { get; set; }
        public ICollection<Enrollment>? Enrollments { get; set; }
    }
}
