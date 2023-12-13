using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Practice1;

namespace Practice1.Controllers
{
    
    public class TrainingController : Controller
    {
        private readonly PracticeCrossfitContext _context;

        public TrainingController(PracticeCrossfitContext context)
        {
            _context = context;
        }

        // GET: Training
        public async Task<IActionResult> Index()
        {
            var practiceCrossfitContext = _context.Training
                .Include(t => t.TrainingFormat);
            ViewData["TrainingFormatId"] = new SelectList(_context.TrainingFormats, "Id", "Name");
            return View(await practiceCrossfitContext.ToListAsync());
        }

        // GET: Training/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Training == null)
            {
                return NotFound();
            }

            var training = await _context.Training
                .Include(t => t.TrainingFormat)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (training == null)
            {
                return NotFound();
            }
            ViewData["TrainingFormatId"] = new SelectList(_context.TrainingFormats, "Id", "Name");
            return View(training);
        }

        // GET: Training/Create
        public IActionResult Create()
        {
            ViewData["TrainingFormatId"] = new SelectList(_context.TrainingFormats, "Id", "Name");
            return View();
        }

        // POST: Training/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,TrainingFormatId,Duration,Author")] Training training)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(training);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TrainingFormatId"] = new SelectList(_context.TrainingFormats, "Id", "Name", training.TrainingFormatId);
            return View(training);
        }

        // GET: Training/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Training == null)
            {
                return NotFound();
            }

            var training = await _context.Training.FindAsync(id);
            if (training == null)
            {
                return NotFound();
            }
            ViewData["TrainingFormatId"] = new SelectList(_context.TrainingFormats, "Id", "Name", training.TrainingFormatId);
            return View(training);
        }

        // POST: Training/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,TrainingFormatId,Duration,Author")] Training training)
        {
            if (id != training.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(training);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainingExists(training.Id))
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
            ViewData["TrainingFormatId"] = new SelectList(_context.TrainingFormats, "Id", "Name", training.TrainingFormatId);
            return View(training);
        }

        // GET: Training/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Training == null)
            {
                return NotFound();
            }

            var training = await _context.Training
                .Include(t => t.TrainingFormat)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (training == null)
            {
                return NotFound();
            }

            return View(training);
        }

        // POST: Training/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Training == null)
            {
                return Problem("Entity set 'PracticeCrossfitContext.Training'  is null.");
            }
            var training = await _context.Training.FindAsync(id);
            if (training != null)
            {
                _context.Training.Remove(training);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrainingExists(int id)
        {
          return (_context.Training?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
