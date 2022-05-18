using L7.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace L7.Data {
    public class SchoolContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int> {
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options) {

        }

        public DbSet<Course> Courses => Set<Course>();
        public DbSet<Subject> Subjects => Set<Subject>();
        public DbSet<GradeOption> GradeOptions => Set<GradeOption>();
        public DbSet<Grade> Grades => Set<Grade>();
        public DbSet<Enrollment> Enrollments => Set<Enrollment>();
        public DbSet<Student> Students => Set<Student>();
        public DbSet<Instructor> Instructors => Set<Instructor>();
        public DbSet<Admin> Admins => Set<Admin>();
        public DbSet<Classification> Classifications => Set<Classification>();

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("Identity");
            modelBuilder.Entity<ApplicationUser>().ToTable("ApplicationUser");
            modelBuilder.Entity<IdentityRole<int>>().ToTable("Role");
            modelBuilder.Entity<IdentityUserRole<int>>().ToTable("UserRole");
            modelBuilder.Entity<IdentityUserClaim<int>>().ToTable("UserClaim");
            modelBuilder.Entity<IdentityUserLogin<int>>().ToTable("UserLogin");
            modelBuilder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaim");
            modelBuilder.Entity<IdentityUserToken<int>>().ToTable("UserToken");

            modelBuilder.Entity<Course>().ToTable("Course");
            modelBuilder.Entity<Subject>().ToTable("Subject");
            modelBuilder.Entity<GradeOption>().ToTable("GradeOption")
                .HasIndex(u => u.Value)
                .IsUnique();
            modelBuilder.Entity<Grade>().ToTable("Grade");
            modelBuilder.Entity<Enrollment>().ToTable("Enrollment");
            modelBuilder.Entity<Student>().ToTable("Student");
            modelBuilder.Entity<Instructor>().ToTable("Instructor");
            modelBuilder.Entity<Admin>().ToTable("Admin");
            modelBuilder.Entity<Classification>().ToTable("Classification")
                .HasIndex(u => u.Name)
                .IsUnique();

            modelBuilder.Entity<Grade>()
                .HasOne(b => b.GradeOption)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Grade>()
                .HasOne(b => b.Classification)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Course>()
                .HasOne(b => b.Subject)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Enrollment>()
                .HasIndex(a => new { a.StudentId, a.CourseId }).IsUnique();
        }
    }
}
