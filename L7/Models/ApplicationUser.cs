using Microsoft.AspNetCore.Identity;

namespace L7.Models {
    public class ApplicationUser : IdentityUser<int> {
        public override int Id { get; set; }
        //public int AdminId { get; set; }
        //public int InstructorId { get; set; }

        public virtual Admin? Admin { get; set; }
        public virtual Instructor? Instructor { get; set; }
    }
}
