using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using L7.Data;
using L7.Models;
using Microsoft.AspNetCore.Authorization;

namespace L7.Controllers {
    [Authorize(Roles = "Admin")]
    public class EnrollmentsController : Controller {
        private readonly SchoolContext _context;

        public EnrollmentsController(SchoolContext context) {
            _context = context;
        }

        // GET: Enrollments
        public async Task<IActionResult> Index() {
            var schoolContext = _context.Enrollments.Include(e => e.Course).Include(e => e.Student);
            return View(await schoolContext.ToListAsync());
        }

        // GET: Enrollments/Details/5
        public async Task<IActionResult> Details(int? id) {
            if (id == null || _context.Enrollments == null) {
                return NotFound();
            }

            var enrollment = await _context.Enrollments
                .Include(e => e.Course)
                .Include(e => e.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (enrollment == null) {
                return NotFound();
            }

            return View(enrollment);
        }

        // GET: Enrollments/Create
        public async Task<IActionResult> Create(int? id) {
            var c = await _context.Courses.Include(i => i.Subject).ToListAsync();

            ViewData["CourseId"] = new SelectList(c, "Id", "Title");
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "FullName", id != null ? id.Value : "");
            return View();
        }

        // POST: Enrollments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentId,CourseId")] Enrollment enrollment) {

            try {
                if (ModelState.IsValid) {
                    _context.Add(enrollment);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Students", new { id = enrollment.StudentId });
                }
            } catch (DbUpdateException /* ex */) {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            var c = await _context.Courses.Include(i => i.Subject).ToListAsync();
            ViewData["CourseId"] = new SelectList(c, "Id", "Title");
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "FullName");
            return View(enrollment);
        }

        // GET: Enrollments/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            if (id == null || _context.Enrollments == null) {
                return NotFound();
            }

            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment == null) {
                return NotFound();
            }

            var c = await _context.Courses.Include(i => i.Subject).ToListAsync();
            ViewData["CourseId"] = new SelectList(c, "Id", "Title");
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "FullName", enrollment.StudentId);
            return View(enrollment);
        }

        // POST: Enrollments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StudentId,CourseId")] Enrollment enrollment) {
            if (id != enrollment.Id) {
                return NotFound();
            }

            try {
                if (ModelState.IsValid) {
                    _context.Update(enrollment);
                    await _context.SaveChangesAsync();
                    if (!EnrollmentExists(enrollment.Id)) {
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
            var c = await _context.Courses.Include(i => i.Subject).ToListAsync();
            ViewData["CourseId"] = new SelectList(c, "Id", "Title");
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "FullName", enrollment.StudentId);
            return View(enrollment);
        }

        // GET: Enrollments/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            if (id == null || _context.Enrollments == null) {
                return NotFound();
            }

            var enrollment = await _context.Enrollments
                .Include(e => e.Course)
                .Include(e => e.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (enrollment == null) {
                return NotFound();
            }

            return View(enrollment);
        }

        // POST: Enrollments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            if (_context.Enrollments == null) {
                return Problem("Entity set 'SchoolContext.Enrollments'  is null.");
            }
            var enrollment = await _context.Enrollments.FindAsync(id);
            try {
                if (enrollment != null) {
                    _context.Enrollments.Remove(enrollment);
                }
                await _context.SaveChangesAsync();
            } catch (DbUpdateException /* ex */) {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
                return View(enrollment);
            }

            return RedirectToAction("Index", "Students", new { id = enrollment!.StudentId });
        }

        private bool EnrollmentExists(int id) {
            return (_context.Enrollments?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
