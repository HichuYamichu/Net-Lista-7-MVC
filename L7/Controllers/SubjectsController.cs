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
    public class SubjectsController : Controller {
        private readonly SchoolContext _context;

        public SubjectsController(SchoolContext context) {
            _context = context;
        }

        // GET: Subjects
        public async Task<IActionResult> Index() {
            return View(await _context.Subjects.ToListAsync());
        }

        // GET: Subjects/Details/5
        public async Task<IActionResult> Details(int? id) {
            if (id == null || _context.Subjects == null) {
                return NotFound();
            }

            var subject = await _context.Subjects
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subject == null) {
                return NotFound();
            }

            return View(subject);
        }

        // GET: Subjects/Create
        public IActionResult Create() {
            return View();
        }

        // POST: Subjects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title")] Subject subject) {
            try {
                if (ModelState.IsValid) {
                    _context.Add(subject);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            } catch (DbUpdateException /* ex */) {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            return View(subject);
        }

        // GET: Subjects/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            if (id == null || _context.Subjects == null) {
                return NotFound();
            }

            var subject = await _context.Subjects.FindAsync(id);
            if (subject == null) {
                return NotFound();
            }
            return View(subject);
        }

        // POST: Subjects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title")] Subject subject) {
            if (id != subject.Id) {
                return NotFound();
            }

            try {
                if (ModelState.IsValid) {
                    _context.Update(subject);
                    await _context.SaveChangesAsync();
                }
            } catch (DbUpdateException /* ex */) {
                if (!SubjectExists(subject.Id)) {
                    return NotFound();
                }
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }


            return View(subject);
        }

        // GET: Subjects/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            if (id == null || _context.Subjects == null) {
                return NotFound();
            }

            var subject = await _context.Subjects
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subject == null) {
                return NotFound();
            }

            return View(subject);
        }

        // POST: Subjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            if (_context.Subjects == null) {
                return Problem("Entity set 'SchoolContext.Subjects'  is null.");
            }

            var subject = await _context.Subjects.FindAsync(id);
            try {
                if (subject != null) {
                    _context.Subjects.Remove(subject);
                }
                await _context.SaveChangesAsync();
            } catch (DbUpdateException /* ex */) {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
                return View(subject);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool SubjectExists(int id) {
            return (_context.Subjects?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
