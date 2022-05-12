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

namespace L7.Controllers {
    public class StudentsController : Controller {
        private readonly SchoolContext _context;

        public StudentsController(SchoolContext context) {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index(int? id, int? enrollmentId) {
            var viewModel = new StudentIndexData();
            viewModel.Students = await _context.Students
                .Include(i => i.Enrollments)
                    .ThenInclude(i => i.Course)
                    .ThenInclude(i => i.Subject)
                .Include(i => i.Enrollments)
                    .ThenInclude(i => i.Grades)
                    .ThenInclude(i => i.GradeOption)
                .AsNoTracking()
                .OrderBy(i => i.LastName)
                .ToListAsync();

            if (id != null) {
                ViewData["StudentId"] = id.Value;
                var student = viewModel.Students.Where(i => i.Id == id.Value).Single();
                viewModel.Enrollments = student.Enrollments;
            }

            if (enrollmentId != null) {
                ViewData["EnrollmentId"] = enrollmentId.Value;
                viewModel.Grades = viewModel.Enrollments.Where(x => x.Id == enrollmentId).Single().Grades;
            }

            return View(viewModel);
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id) {
            if (id == null || _context.Students == null) {
                return NotFound();
            }

            var student = await _context.Students
                .Include(i => i.Enrollments)
                    .ThenInclude(i => i.Grades)
                    .ThenInclude(i => i.GradeOption)
                .Include(i => i.Enrollments)
                    .ThenInclude(i => i.Course)
                    .ThenInclude(i => i.Subject)

                .FirstOrDefaultAsync(m => m.Id == id);

            if (student == null) {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create() {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName")] Student student) {
            if (ModelState.IsValid) {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            if (id == null || _context.Students == null) {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null) {
                return NotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName")] Student student) {
            if (id != student.Id) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException) {
                    if (!StudentExists(student.Id)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            if (id == null || _context.Students == null) {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null) {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            if (_context.Students == null) {
                return Problem("Entity set 'SchoolContext.Students'  is null.");
            }
            var student = await _context.Students.FindAsync(id);
            if (student != null) {
                _context.Students.Remove(student);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id) {
            return (_context.Students?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
