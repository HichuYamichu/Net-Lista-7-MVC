namespace L7.Models.SchoolViewModels {
    public class StudentIndexData {
        public IEnumerable<Student> Students { get; set; } = null!;
        public IEnumerable<Enrollment> Enrollments { get; set; } = null!;
        public IEnumerable<Grade> Grades { get; set; } = null!;
    }
}
