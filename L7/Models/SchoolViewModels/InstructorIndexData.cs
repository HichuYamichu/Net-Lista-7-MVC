using L7.Models;

namespace L7.Models.SchoolViewModels {
    public class InstructorIndexData {
        public IEnumerable<Instructor> Instructors { get; set; } = null!;
        public IEnumerable<Course> Courses { get; set; } = null!;
        public IEnumerable<Enrollment> Enrollments { get; set; } = null!;
    }
}
