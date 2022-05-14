using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using L7.Data;
using L7.Models;

namespace L7.Controllers {
    public class GradesController : Controller {
        private readonly SchoolContext _context;

        public GradesController(SchoolContext context) {
            _context = context;
        }

        // GET: Grades
        public async Task<IActionResult> Index() {
            var schoolContext = _context.Grades
                .Include(g => g.Enrollment!)
                    .ThenInclude(i => i.Student)
                .Include(g => g.GradeOption);
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
        public async Task<IActionResult> Create(int? id) {
            var courses = await _context.Courses.Include(i => i.Subject).ToListAsync();
            ViewData["CoursesId"] = new SelectList(courses, "Id", "Subject.Title", id != null ? id.Value : "");

            if (id != null) {
                var enrolledStudents = await _context.Enrollments
                    .Include(i => i.Student)
                    .Where(e => e.CourseId == id)
                    .ToListAsync();
                ViewData["EnrollmentId"] = new SelectList(enrolledStudents, "Id", "Student.FullName");
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
            return View();
        }

        // POST: Grades/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EnrollmentId,GradeOptionId")] Grade grade) {
            if (ModelState.IsValid) {
                _context.Add(grade);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var courses = await _context.Courses.Include(i => i.Subject).ToListAsync();
            ViewData["CoursesId"] = new SelectList(courses, "Id", "Subject.Title");

            ViewData["EnrollmentId"] = new SelectList(_context.Enrollments, "Id", "Id", grade.EnrollmentId);
            ViewData["GradeOptionId"] = new SelectList(_context.GradeOptions, "Id", "Id", grade.GradeOptionId);
            return View(grade);
        }

        // GET: Grades/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            if (id == null || _context.Grades == null) {
                return NotFound();
            }

            var grade = await _context.Grades.FindAsync(id);
            if (grade == null) {
                return NotFound();
            }
            ViewData["EnrollmentId"] = new SelectList(_context.Enrollments, "Id", "Id", grade.EnrollmentId);
            ViewData["GradeOptionId"] = new SelectList(_context.GradeOptions, "Id", "Id", grade.GradeOptionId);
            return View(grade);
        }

        // POST: Grades/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EnrollmentId,GradeOptionId")] Grade grade) {
            if (id != grade.Id) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(grade);
                    await _context.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException) {
                    if (!GradeExists(grade.Id)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["EnrollmentId"] = new SelectList(_context.Enrollments, "Id", "Id", grade.EnrollmentId);
            ViewData["GradeOptionId"] = new SelectList(_context.GradeOptions, "Id", "Id", grade.GradeOptionId);
            return View(grade);
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
            if (grade != null) {
                _context.Grades.Remove(grade);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GradeExists(int id) {
            return (_context.Grades?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
