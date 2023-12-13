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
    public class TariffController : Controller
    {
        private readonly PracticeCrossfitContext _context;
        
        private bool _isAdmin =  Practice1.User.IsAdmin();
        

        public TariffController(PracticeCrossfitContext context)
        {
            _context = context;
        }

        // GET: Tariff
        public async Task<IActionResult> Index()
        {
            
            ViewData["TrainingFormatId"] = new SelectList(_context.TrainingFormats, "Id", "Name");
            var practiceCrossfitContext = _context.Tariffs.Include(t => t.TrainingFormat);
            return View(await practiceCrossfitContext.ToListAsync());
        }

        // GET: Tariff/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tariffs == null)
            {
                return NotFound();
            }

            var tariff = await _context.Tariffs
                .Include(t => t.TrainingFormat)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tariff == null)
            {
                return NotFound();
            }

            return View(tariff);
        }

        // GET: Tariff/Create
        public IActionResult Create()
        {
            ViewData["TrainingFormatId"] = new SelectList(_context.TrainingFormats, "Id", "Name");
            return View();
        }

        // POST: Tariff/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,TrainingFormatId,Price")] Tariff tariff)
        {
            if (!ModelState.IsValid)//TODO
            {
                _context.Add(tariff);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TrainingFormatId"] = new SelectList(_context.TrainingFormats, "Id", "Name", tariff.TrainingFormatId);
            return View(tariff);
        }

        // GET: Tariff/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tariffs == null)
            {
                return NotFound();
            }

            var tariff = await _context.Tariffs.FindAsync(id);
            if (tariff == null)
            {
                return NotFound();
            }
            ViewData["TrainingFormatId"] = new SelectList(_context.TrainingFormats, "Id", "Name", tariff.TrainingFormatId);
            return View(tariff);
        }

        // POST: Tariff/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,TrainingFormatId,Price")] Tariff tariff)
        {
            if (id != tariff.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)//TODO
            {
                try
                {
                    _context.Update(tariff);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TariffExists(tariff.Id))
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
            ViewData["TrainingFormatId"] = new SelectList(_context.TrainingFormats, "Id", "Name", tariff.TrainingFormatId);
            return View(tariff);
        }

        // GET: Tariff/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tariffs == null)
            {
                return NotFound();
            }

            var tariff = await _context.Tariffs
                .Include(t => t.TrainingFormat)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tariff == null)
            {
                return NotFound();
            }

            return View(tariff);
        }

        // POST: Tariff/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tariffs == null)
            {
                return Problem("Entity set 'PracticeCrossfitContext.Tariffs'  is null.");
            }
            var tariff = await _context.Tariffs.FindAsync(id);
            if (tariff != null)
            {
                _context.Tariffs.Remove(tariff);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TariffExists(int id)
        {
          return (_context.Tariffs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
