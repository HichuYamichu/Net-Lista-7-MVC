using L7.Models;

namespace L7.Models {
    public class Grade {
        public int Id { get; set; }
        public int EnrollmentId { get; set; }
        public int GradeOptionId { get; set; }
        public int ClassificationId { get; set; }

        public Enrollment? Enrollment { get; set; }
        public GradeOption? GradeOption { get; set; }
        public Classification? Classification { get; set; }
    }
}
