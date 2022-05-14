namespace L7.Models.SchoolViewModels {
    public class CourseIndexData {
        public IEnumerable<Enrollment> Enrollments { get; set; }
        public IEnumerable<Course> Courses { get; set; }
        public IEnumerable<Grade> Grades { get; set; }
    }
}
