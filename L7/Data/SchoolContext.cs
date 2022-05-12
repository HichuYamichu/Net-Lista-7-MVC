using L7.Models;
using Microsoft.EntityFrameworkCore;

namespace L7.Data {
    public class SchoolContext : DbContext {
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options) {
        }

        public DbSet<Course> Courses => Set<Course>();
        public DbSet<Subject> Subjects => Set<Subject>();
        public DbSet<GradeOption> GradeOptions => Set<GradeOption>();
        public DbSet<Grade> Grades => Set<Grade>();
        public DbSet<Enrollment> Enrollments => Set<Enrollment>();
        public DbSet<Student> Students => Set<Student>();
        public DbSet<Instructor> Instructors => Set<Instructor>();
        
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Course>().ToTable("Course");
            modelBuilder.Entity<Subject>().ToTable("Subject");
            modelBuilder.Entity<GradeOption>().ToTable("GradeOption");
            modelBuilder.Entity<Grade>().ToTable("Grade");
            modelBuilder.Entity<Enrollment>().ToTable("Enrollment");
            modelBuilder.Entity<Student>().ToTable("Student");
            modelBuilder.Entity<Instructor>().ToTable("Instructor");
        }
    }
}
