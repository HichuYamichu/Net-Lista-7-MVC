namespace L7.Models.SchoolViewModels {
    public class GradeCreateData {
        public Grade Grade { get; set; } = null!;
        public List<Course> Courses { get; set; } = null!;
    }
}
