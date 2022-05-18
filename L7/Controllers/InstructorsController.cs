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
    [Authorize(Roles = "Admin")]
    public class InstructorsController : Controller {
        private readonly SchoolContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public InstructorsController(SchoolContext context, UserManager<ApplicationUser> userManager) {
            _context = context;
            _userManager = userManager;
        }

        // GET: Instructors
        public async Task<IActionResult> Index(int? id, int? courseID) {
            var viewModel = new InstructorIndexData();
            viewModel.Instructors = await _context.Instructors
                .Include(i => i.Courses!)
                    .ThenInclude(i => i.Enrollments!)
                    .ThenInclude(i => i.Student!)
                .Include(i => i.Courses!)
                    .ThenInclude(i => i.Enrollments!)
                .Include(i => i.Courses!)
                    .ThenInclude(i => i.Subject)
                .AsNoTracking()
                .OrderBy(i => i.LastName)
                .ToListAsync();

            if (id != null) {
                ViewData["InstructorID"] = id.Value;
                Instructor instructor = viewModel.Instructors.Where(
                    i => i.Id == id.Value).Single();
                viewModel.Courses = instructor.Courses!;
            }

            if (courseID != null) {
                ViewData["CourseID"] = courseID.Value;
                viewModel.Enrollments = viewModel.Courses.Where(
                    x => x.Id == courseID).Single().Enrollments!;
            }

            return View(viewModel);
        }

        // GET: Instructors/Details/5
        public async Task<IActionResult> Details(int? id) {
            if (id == null || _context.Instructors == null) {
                return NotFound();
            }

            var instructor = await _context.Instructors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (instructor == null) {
                return NotFound();
            }

            return View(instructor);
        }

        // GET: Instructors/Create
        public IActionResult Create() {
            return View();
        }

        // POST: Instructors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName")] Instructor instructor) {
            try {
                if (ModelState.IsValid) {
                    _context.Add(instructor);
                    await _context.SaveChangesAsync();
                    var u = new ApplicationUser() {
                        UserName = $"{instructor.LastName}{instructor.FirstName}",
                        Email = $"{instructor.LastName}{instructor.FirstName}@school.com",
                        Instructor = instructor,
                    };
                    await _userManager.CreateAsync(u, instructor.LastName);
                    await _userManager.AddToRoleAsync(u, "Instructor");

                    return RedirectToAction(nameof(Index));
                }

            } catch (DbUpdateException /* ex */) {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            return View(instructor);
        }

        // GET: Instructors/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            if (id == null || _context.Instructors == null) {
                return NotFound();
            }

            var instructor = await _context.Instructors.FindAsync(id);
            if (instructor == null) {
                return NotFound();
            }
            return View(instructor);
        }

        // POST: Instructors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName")] Instructor instructor) {
            if (id != instructor.Id) {
                return NotFound();
            }

            try {
                if (ModelState.IsValid) {
                    _context.Update(instructor);
                    await _context.SaveChangesAsync();
                    if (!InstructorExists(instructor.Id)) {
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
            return View(instructor);
        }

        // GET: Instructors/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            if (id == null || _context.Instructors == null) {
                return NotFound();
            }

            var instructor = await _context.Instructors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (instructor == null) {
                return NotFound();
            }

            return View(instructor);
        }

        // POST: Instructors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            if (_context.Instructors == null) {
                return Problem("Entity set 'SchoolContext.Instructors'  is null.");
            }

            var instructor = await _context.Instructors.FindAsync(id);
            try {
                if (instructor != null) {
                    _context.Instructors.Remove(instructor);
                }

                await _context.SaveChangesAsync();
            } catch (DbUpdateException /* ex */) {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
                return View(instructor);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool InstructorExists(int id) {
            return (_context.Instructors?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
