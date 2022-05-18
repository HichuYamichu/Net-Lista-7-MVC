using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using L7.Data;
using L7.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace L7.Controllers {
    [Authorize(Roles = "Admin,Instructor")]
    public class GradesController : Controller {
        private readonly SchoolContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public GradesController(SchoolContext context, UserManager<ApplicationUser> userManager) {
            _context = context;
            _userManager = userManager;
        }

        // GET: Grades
        public async Task<IActionResult> Index() {
            var schoolContext = _context.Grades
                .Include(g => g.Enrollment!)
                    .ThenInclude(i => i.Student)
                .Include(g => g.Enrollment!)
                    .ThenInclude(i => i.Course!)
                    .ThenInclude(i => i.Subject)
                .Include(g => g.GradeOption)
                .Include(g => g.Classification);
            return View(await schoolContext.ToListAsync());
        }

        // GET: Grades/Details/5
        public async Task<IActionResult> Details(int? id) {
            if (id == null || _context.Grades == null) {
                return NotFound();
            }

            var grade = await _context.Grades
                .Include(g => g.Enrollment)
                .Include(g => g.GradeOption)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (grade == null) {
                return NotFound();
            }

            return View(grade);
        }

        // GET: Grades/Create
        public async Task<IActionResult> Create(int? id, int? enrollmentId) {
            var applicationUser = await _userManager.GetUserAsync(User);

            if (User.IsInRole("Admin")) {
                var courses = await _context.Courses.Include(i => i.Subject).ToListAsync();
                ViewData["CoursesId"] = new SelectList(courses, "Id", "Subject.Title", id != null ? id.Value : "");
            } else {
                await _context.Entry(applicationUser).Reference(x => x.Instructor).LoadAsync();
                var courses = await _context.Courses.Include(i => i.Subject)
                    .Where(c => c.InstructorId == applicationUser.Instructor!.Id).ToListAsync();
                ViewData["CoursesId"] = new SelectList(courses, "Id", "Subject.Title", id != null ? id.Value : "");
            }


            if (id != null) {
                var enrolledStudents = await _context.Enrollments
                    .Include(i => i.Student)
                    .Where(e => e.CourseId == id)
                    .ToListAsync();
                ViewData["EnrollmentId"] = new SelectList(enrolledStudents, "Id", "Student.FullName", enrollmentId == null ? "" : enrollmentId.Value);
            } else {
                var anyCourse = await _context.Courses.FirstOrDefaultAsync();
                if (anyCourse != null) {
                    var enrolledStudents = await _context.Enrollments
                        .Include(i => i.Student)
                        .Where(e => e.CourseId == anyCourse.Id)
                        .ToListAsync();
                    ViewData["EnrollmentId"] = new SelectList(enrolledStudents, "Id", "Student.FullName");
                }
            }

            ViewData["GradeOptionId"] = new SelectList(await _context.GradeOptions.ToListAsync(), "Id", "Value");
            ViewData["ClassificationId"] = new SelectList(await _context.Classifications.ToListAsync(), "Id", "Name");
            return View();
        }

        // POST: Grades/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EnrollmentId,GradeOptionId,ClassificationId")] Grade grade) {
            try {
                if (ModelState.IsValid) {
                    _context.Add(grade);
                    await _context.SaveChangesAsync();
                    _context.Entry(grade).Reference(p => p.Enrollment).Load();
                    return RedirectToAction("Index", "Courses", new { id = grade.Enrollment!.CourseId, enrollmentId = grade.EnrollmentId });
                }
            } catch (DbUpdateException /* ex */) {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }

            var courses = await _context.Courses.Include(i => i.Subject).ToListAsync();
            ViewData["CoursesId"] = new SelectList(courses, "Id", "Subject.Title");

            ViewData["EnrollmentId"] = new SelectList(_context.Enrollments, "Id", "Id", grade.EnrollmentId);
            ViewData["GradeOptionId"] = new SelectList(_context.GradeOptions, "Id", "Id", grade.GradeOptionId);
            return View(grade);
        }

        // GET: Grades/Edit/5
        public async Task<IActionResult> Edit(int? id, int? enrollmentId) {
            if (id == null || _context.Grades == null) {
                return NotFound();
            }

            var grade = await _context.Grades.FindAsync(id);
            if (grade == null) {
                return NotFound();
            }

            var applicationUser = await _userManager.GetUserAsync(User);

            if (User.IsInRole("Admin")) {
                var courses = await _context.Courses.Include(i => i.Subject).ToListAsync();
                ViewData["CoursesId"] = new SelectList(courses, "Id", "Subject.Title", id != null ? id.Value : "");
            } else {
                await _context.Entry(applicationUser).Reference(x => x.Instructor).LoadAsync();
                var courses = await _context.Courses.Include(i => i.Subject)
                    .Where(c => c.InstructorId == applicationUser.Instructor!.Id).ToListAsync();
                ViewData["CoursesId"] = new SelectList(courses, "Id", "Subject.Title", id != null ? id.Value : "");
            }

            if (id != null) {
                var enrolledStudents = await _context.Enrollments
                    .Include(i => i.Student)
                    .Where(e => e.CourseId == id)
                    .ToListAsync();
                ViewData["EnrollmentId"] = new SelectList(enrolledStudents, "Id", "Student.FullName", enrollmentId == null ? "" : enrollmentId.Value);
            } else {
                var anyCourse = await _context.Courses.FirstOrDefaultAsync();
                if (anyCourse != null) {
                    var enrolledStudents = await _context.Enrollments
                        .Include(i => i.Student)
                        .Where(e => e.CourseId == anyCourse.Id)
                        .ToListAsync();
                    ViewData["EnrollmentId"] = new SelectList(enrolledStudents, "Id", "Student.FullName");
                }
            }

            ViewData["GradeOptionId"] = new SelectList(await _context.GradeOptions.ToListAsync(), "Id", "Value");
            ViewData["ClassificationId"] = new SelectList(await _context.Classifications.ToListAsync(), "Id", "Name");

            return View(grade);
        }

        // POST: Grades/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EnrollmentId,GradeOptionId,ClassificationId")] Grade grade) {
            if (id != grade.Id) {
                return NotFound();
            }

            try {
                if (ModelState.IsValid) {
                    _context.Update(grade);
                    await _context.SaveChangesAsync();
                    if (!GradeExists(grade.Id)) {
                        return NotFound();

                    }
                    return RedirectToAction("Index", "Courses");
                }
            } catch (DbUpdateException /* ex */) {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }

            ViewData["EnrollmentId"] = new SelectList(_context.Enrollments, "Id", "Id", grade.EnrollmentId);
            ViewData["GradeOptionId"] = new SelectList(await _context.GradeOptions.ToListAsync(), "Id", "Value");
            ViewData["ClassificationId"] = new SelectList(await _context.Classifications.ToListAsync(), "Id", "Name");


            //return View(grade);
            return RedirectToAction("Index", "Courses");

        }

        // GET: Grades/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            if (id == null || _context.Grades == null) {
                return NotFound();
            }

            var grade = await _context.Grades
                .Include(g => g.Enrollment)
                .Include(g => g.GradeOption)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (grade == null) {
                return NotFound();
            }

            return View(grade);
        }

        // POST: Grades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            if (_context.Grades == null) {
                return Problem("Entity set 'SchoolContext.Grades'  is null.");
            }

            var grade = await _context.Grades.FindAsync(id);
            try {
                if (grade != null) {
                    _context.Entry(grade).Reference(p => p.Enrollment).Load();
                    _context.Grades.Remove(grade);
                }
                await _context.SaveChangesAsync();
            } catch (DbUpdateException /* ex */) {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }

            return RedirectToAction("Index", "Courses", new { id = grade.Enrollment!.CourseId, enrollmentId = grade.EnrollmentId });
        }

        private bool GradeExists(int id) {
            return (_context.Grades?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
