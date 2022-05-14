using System.ComponentModel.DataAnnotations;

namespace L7.Models {
    public class Instructor {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;

        [Display(Name = "Full Name")]
        public string FullName {
            get {
                return LastName + ", " + FirstName;
            }
        }

        public ICollection<Course>? Courses { get; set; }
    }
}
