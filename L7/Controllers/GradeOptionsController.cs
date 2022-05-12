using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using L7.Data;
using L7.Models;

namespace L7.Controllers
{
    public class GradeOptionsController : Controller
    {
        private readonly SchoolContext _context;

        public GradeOptionsController(SchoolContext context)
        {
            _context = context;
        }

        // GET: GradeOptions
        public async Task<IActionResult> Index()
        {
            return View(await _context.GradeOptions.ToListAsync());
        }

        // GET: GradeOptions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.GradeOptions == null)
            {
                return NotFound();
            }

            var gradeOption = await _context.GradeOptions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gradeOption == null)
            {
                return NotFound();
            }

            return View(gradeOption);
        }

        // GET: GradeOptions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GradeOptions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Value")] GradeOption gradeOption)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gradeOption);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gradeOption);
        }

        // GET: GradeOptions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.GradeOptions == null)
            {
                return NotFound();
            }

            var gradeOption = await _context.GradeOptions.FindAsync(id);
            if (gradeOption == null)
            {
                return NotFound();
            }
            return View(gradeOption);
        }

        // POST: GradeOptions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Value")] GradeOption gradeOption)
        {
            if (id != gradeOption.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gradeOption);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GradeOptionExists(gradeOption.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(gradeOption);
        }

        // GET: GradeOptions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.GradeOptions == null)
            {
                return NotFound();
            }

            var gradeOption = await _context.GradeOptions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gradeOption == null)
            {
                return NotFound();
            }

            return View(gradeOption);
        }

        // POST: GradeOptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.GradeOptions == null)
            {
                return Problem("Entity set 'SchoolContext.GradeOptions'  is null.");
            }
            var gradeOption = await _context.GradeOptions.FindAsync(id);
            if (gradeOption != null)
            {
                _context.GradeOptions.Remove(gradeOption);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GradeOptionExists(int id)
        {
          return (_context.GradeOptions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
