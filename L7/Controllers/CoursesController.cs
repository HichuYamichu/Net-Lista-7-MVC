using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using L7.Data;
using L7.Models;
using L7.Models.SchoolViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace L7.Controllers {
    public class CoursesController : Controller {
        private readonly SchoolContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CoursesController(SchoolContext context, UserManager<ApplicationUser> userManager) {
            _context = context;
            _userManager = userManager;
        }

        // GET: Courses
        public async Task<IActionResult> Index(int? id, int? enrollmentId) {
            var viewModel = new CourseIndexData();
            var applicationUser = await _userManager.GetUserAsync(User);

            var q = _context.Courses
                .Include(c => c.Subject)
                .Include(c => c.Instructor)
                .Include(c => c.Enrollments!)
                    .ThenInclude(c => c.Student)
                .Include(c => c.Enrollments!)
                    .ThenInclude(c => c.Grades!)
                    .ThenInclude(c => c.GradeOption)
                .Include(c => c.Enrollments!)
                    .ThenInclude(c => c.Grades!)
                    .ThenInclude(c => c.Classification);

            if (User.IsInRole("Admin")) {
                viewModel.Courses = await q.ToListAsync();
            } else {
                await _context.Entry(applicationUser).Reference(x => x.Instructor).LoadAsync();
                viewModel.Courses = await q
                    .Where(c => c.InstructorId == applicationUser.Instructor!.Id).ToListAsync();
            }

            if (id != null) {
                ViewData["CourseId"] = id.Value;
                var course = viewModel.Courses.Where(i => i.Id == id.Value).Single();
                viewModel.Enrollments = course.Enrollments!;
            }

            if (enrollmentId != null) {
                ViewData["EnrollmentId"] = enrollmentId.Value;
                viewModel.Grades = viewModel.Enrollments.Where(x => x.Id == enrollmentId).Single().Grades!;
            }

            return View(viewModel);
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id) {
            if (id == null || _context.Courses == null) {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.Subject)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null) {
                return NotFound();
            }

            return View(course);
        }

        // GET: Courses/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create() {
            PopulateSubjectDropDownList();
            PopulateInstructorDropDownList();
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("SubjectId, InstructorId, StartDate, EndDate")] Course course) {

            try {
                if (ModelState.IsValid) {
                    _context.Add(course);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            } catch (DbUpdateException /* ex */) {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }

            PopulateSubjectDropDownList(course.SubjectId);
            PopulateInstructorDropDownList(course.InstructorId);

            return View(course);
        }

        // GET: Courses/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id) {
            if (id == null || _context.Courses == null) {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(i => i.Instructor)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (course == null) {
                return NotFound();
            }

            PopulateSubjectDropDownList(course.SubjectId);
            PopulateInstructorDropDownList(course.Instructor!);
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SubjectId,InstructorId,StartDate,EndDate")] Course course) {
            if (id != course.Id) {
                return NotFound();
            }

            try {
                if (ModelState.IsValid) {
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                    if (!CourseExists(course.Id)) {
                        return NotFound();
                    }
                    return RedirectToAction(nameof(Index));
                }
            } catch (DbUpdateException /* ex */) {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }

            PopulateSubjectDropDownList(course.SubjectId);
            PopulateSubjectDropDownList(course.InstructorId);
            return View(course);
        }

        private void PopulateSubjectDropDownList(object selectedSubject = null!) {
            var subjectsQuery = from s in _context.Subjects
                                orderby s.Title
                                select s;
            ViewBag.SubjectId = new SelectList(subjectsQuery.AsNoTracking(), "Id", "Title", selectedSubject);
        }

        private void PopulateInstructorDropDownList(object selectedInstructor = null!) {
            var instructorQuery = from i in _context.Instructors
                                  orderby i.LastName
                                  select i;
            ViewBag.InstructorId = new SelectList(instructorQuery.AsNoTracking(), "Id", "FullName", selectedInstructor);
        }

        // GET: Courses/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id) {
            if (id == null || _context.Courses == null) {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.Subject)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null) {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            if (_context.Courses == null) {
                return Problem("Entity set 'SchoolContext.Courses'  is null.");
            }

            var course = await _context.Courses.FindAsync(id);
            try {
                if (course != null) {
                    _context.Courses.Remove(course);
                }

                await _context.SaveChangesAsync();

            } catch (DbUpdateException /* ex */) {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
                return View(course);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id) {
            return (_context.Courses?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
