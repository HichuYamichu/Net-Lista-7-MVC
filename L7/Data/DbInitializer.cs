using L7.Models;

namespace L7.Data {
    public class DbInitializer {
        public static void Initialize(SchoolContext context) {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Students.Any()) {
                return;   // DB has been seeded
            }

            var students = new Student[]
            {
            new Student{FirstName="Carson",LastName="Alexander"},
            new Student{FirstName="Meredith",LastName="Alonso"},
            new Student{FirstName="Arturo",LastName="Anand"},
            new Student{FirstName="Gytis",LastName="Barzdukas"},
            };
            foreach (Student s in students) {
                context.Students.Add(s);
            }
            context.SaveChanges();

            var instructors = new Instructor[]
            {
                new Instructor { FirstName = "Kim",     LastName = "Abercrombie", },
                new Instructor { FirstName = "Fadi",    LastName = "Fakhouri", },
                new Instructor { FirstName = "Roger",   LastName = "Harui", },
                new Instructor { FirstName = "Candace", LastName = "Kapoor", },
            };

            foreach (Instructor i in instructors) {
                context.Instructors.Add(i);
            }
            context.SaveChanges();

            var gradeOptions = new GradeOption[]
{
            new GradeOption{Value = 2.0},
            new GradeOption{Value = 3.0},
            new GradeOption{Value = 4.0},
            new GradeOption{Value = 5.0},
};
            foreach (var c in gradeOptions) {
                context.GradeOptions.Add(c);
            }
            context.SaveChanges();

            var subjects = new Subject[]
            {
            new Subject{Title = "Math"},
            new Subject{Title = "IT"},
            new Subject{Title = "PE"},
            new Subject{Title = "Net"},
            };
            foreach (var c in subjects) {
                context.Subjects.Add(c);
            }
            context.SaveChanges();

            var courses = new Course[]
            {
                new Course{
                    StartDate = DateTime.Parse("2010-09-01"),
                    SubjectId = subjects.Single(s => s.Title == "Math").Id,
                    InstructorId = instructors.Single(i => i.LastName == "Abercrombie").Id
                },
                new Course{
                    StartDate = DateTime.Parse("2010-09-02"),
                    SubjectId = subjects.Single(s => s.Title == "IT").Id,
                    InstructorId = instructors.Single(i => i.LastName == "Fakhouri").Id
                },
                new Course{
                    StartDate = DateTime.Parse("2010-09-03"),
                    SubjectId = subjects.Single(s => s.Title == "PE").Id,
                    InstructorId = instructors.Single(i => i.LastName == "Harui").Id
                },
                new Course{
                    StartDate = DateTime.Parse("2010-09-04"),
                    SubjectId = subjects.Single(s => s.Title == "Net").Id,
                    InstructorId = instructors.Single(i => i.LastName == "Kapoor").Id
                },
            };
            foreach (Course c in courses) {
                context.Courses.Add(c);
            }
            context.SaveChanges();

            var enrollments = new Enrollment[]
            {
                new Enrollment {
                    StudentId = students.Single(s => s.LastName == "Alexander").Id,
                    CourseId = courses.Single(c => c.Subject.Title == "Math" ).Id,
                },
                new Enrollment {
                    StudentId = students.Single(s => s.LastName == "Alonso").Id,
                    CourseId = courses.Single(c => c.Subject.Title == "IT" ).Id,
                },
                new Enrollment {
                    StudentId = students.Single(s => s.LastName == "Anand").Id,
                    CourseId = courses.Single(c => c.Subject.Title == "PE" ).Id,
                },
                new Enrollment {
                    StudentId = students.Single(s => s.LastName == "Barzdukas").Id,
                    CourseId = courses.Single(c => c.Subject.Title == "Net" ).Id,
                },
            };

            foreach (Enrollment e in enrollments) {
                var enrollmentInDataBase = context.Enrollments.Where(
                    s =>
                            s.Student.Id == e.StudentId &&
                            s.Course.Id == e.CourseId).SingleOrDefault();
                if (enrollmentInDataBase == null) {
                    context.Enrollments.Add(e);
                }
            }
            context.SaveChanges();

            var grades = new Grade[]
            {
                new Grade (){
                    GradeOptionId = gradeOptions.Single(g => g.Value == 3.0).Id,
                    EnrollmentId = enrollments.Single(s => s.Student.LastName == "Alexander").Id
                },
                new Grade (){
                    GradeOptionId = gradeOptions.Single(g => g.Value == 2.0).Id,
                    EnrollmentId = enrollments.Single(s => s.Student.LastName == "Alexander").Id
                },
                new Grade (){
                    GradeOptionId = gradeOptions.Single(g => g.Value == 4.0).Id,
                    EnrollmentId = enrollments.Single(s => s.Student.LastName == "Alexander").Id
                },
                new Grade (){
                    GradeOptionId = gradeOptions.Single(g => g.Value == 2.0).Id,
                    EnrollmentId = enrollments.Single(s => s.Student.LastName == "Alonso").Id
                },
                new Grade (){
                    GradeOptionId = gradeOptions.Single(g => g.Value == 2.0).Id,
                    EnrollmentId = enrollments.Single(s => s.Student.LastName == "Alonso").Id
                },
                new Grade (){
                    GradeOptionId = gradeOptions.Single(g => g.Value == 2.0).Id,
                    EnrollmentId = enrollments.Single(s => s.Student.LastName == "Alonso").Id
                },
                 new Grade (){
                    GradeOptionId = gradeOptions.Single(g => g.Value == 2.0).Id,
                    EnrollmentId = enrollments.Single(s => s.Student.LastName == "Anand").Id
                }
            };

            foreach (var g in grades) {
                context.Grades.Add(g);
            }
            context.SaveChanges();

        }
    }
}
