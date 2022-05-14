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

namespace L7.Controllers
{
    public class CoursesController : Controller
    {
        private readonly SchoolContext _context;

        public CoursesController(SchoolContext context)
        {
            _context = context;
        }

        // GET: Courses
        public async Task<IActionResult> Index(int? id, int? enrollmentId)
        {
            var viewModel = new CourseIndexData();

            viewModel.Courses = await _context.Courses
                .Include(c => c.Subject)
                .Include(c => c.Instructor)
                .Include(c => c.Enrollments!)
                    .ThenInclude(c => c.Student)
                .Include(c => c.Enrollments!)
                    .ThenInclude(c => c.Grades!)
                    .ThenInclude(c => c.GradeOption)
                .Include(c => c.Enrollments!)
                    .ThenInclude(c => c.Grades!)
                    .ThenInclude(c => c.Classification)
                .ToListAsync();

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
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Courses == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.Subject)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            PopulateSubjectDropDownList();
            PopulateInstructorDropDownList();
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SubjectId, InstructorId")] Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateSubjectDropDownList(course.SubjectId);
            PopulateInstructorDropDownList(course.InstructorId);

            return View(course);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Courses == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(i => i.Instructor)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (course == null)
            {
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,SubjectId")] Course course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.Id))
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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Courses == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.Subject)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Courses == null)
            {
                return Problem("Entity set 'SchoolContext.Courses'  is null.");
            }
            var course = await _context.Courses.FindAsync(id);
            if (course != null)
            {
                _context.Courses.Remove(course);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
          return (_context.Courses?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
