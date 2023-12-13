using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Practice1;

namespace Practice1.Controllers
{
    public class TimetableController : Controller
    {
        private readonly PracticeCrossfitContext _context;

        public TimetableController(PracticeCrossfitContext context)
        {
            _context = context;
        }

        // GET: Timetable
        public async Task<IActionResult> Index()
        {
            var practiceCrossfitContext = _context.Timetables.Include(t => t.Training).Include(t => t.User);
            return View(await practiceCrossfitContext.ToListAsync());
        }

        // GET: Timetable/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Timetables == null)
            {
                return NotFound();
            }

            var timetable = await _context.Timetables
                .Include(t => t.Training)
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (timetable == null)
            {
                return NotFound();
            }

            return View(timetable);
        }

        // GET: Timetable/Create
        public IActionResult Create()
        {
            ViewData["TariffId"] = new SelectList(_context.Training, "Id", "Name");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Fio");
            return View();
        }

        // POST: Timetable/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Start,End,TrainingId,UserId")] Timetable timetable)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(timetable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TrainingId"] = new SelectList(_context.Training, "Id", "Name");
            ViewData["UserId"] = new SelectList(_context.Users.Where(u=>u.RoleId==1), "Id", "Fio");
            return View(timetable);
        }

        // GET: Timetable/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Timetables == null)
            {
                return NotFound();
            }

            var timetable = await _context.Timetables.FindAsync(id);
            if (timetable == null)
            {
                return NotFound();
            }
            ViewData["TrainingId"] = new SelectList(_context.Training, "Id", "Name");
            ViewData["UserId"] = new SelectList(_context.Users.Where(u=>u.RoleId==1), "Id", "Fio");
            return View(timetable);
        }

        // POST: Timetable/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Start,End,TrainingId,UserId")] Timetable timetable)
        {
            if (id != timetable.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(timetable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TimetableExists(timetable.Id))
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
            ViewData["TrainingId"] = new SelectList(_context.Training, "Id", "Name");
            ViewData["UserId"] = new SelectList(_context.Users.Where(u=>u.RoleId==1), "Id", "Fio");
            return View(timetable);
        }

        // GET: Timetable/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Timetables == null)
            {
                return NotFound();
            }

            var timetable = await _context.Timetables
                .Include(t => t.Training)
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (timetable == null)
            {
                return NotFound();
            }

            return View(timetable);
        }

        // POST: Timetable/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Timetables == null)
            {
                return Problem("Entity set 'PracticeCrossfitContext.Timetables'  is null.");
            }
            var timetable = await _context.Timetables.FindAsync(id);
            if (timetable != null)
            {
                _context.Timetables.Remove(timetable);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TimetableExists(int id)
        {
          return (_context.Timetables?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
