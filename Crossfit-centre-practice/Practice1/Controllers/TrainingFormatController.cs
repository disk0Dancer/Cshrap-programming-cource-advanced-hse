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
    public class TrainingFormatController : Controller
    {
        private readonly PracticeCrossfitContext _context;

        public TrainingFormatController(PracticeCrossfitContext context)
        {
            _context = context;
        }

        public  void Access()
        {
            if (!Practice1.User.IsAdmin())
            {
                var p = new HomeController( _context);
                p.Denied();
                Problem("Требуется авторизация в профиль тренера");
            }
            
        }

        // GET: TrainingFormatController
        public async Task<IActionResult> Index()
        {
            Access();
              return _context.TrainingFormats != null? 
                          View(await _context.TrainingFormats.ToListAsync()) :
                          Problem("Entity set 'PracticeCrossfitContext.TrainingFormats'  is null.");
        }

        // GET: TrainingFormatController/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            Access();
            if (id == null || _context.TrainingFormats == null)
            {
                return NotFound();
            }

            var trainingFormat = await _context.TrainingFormats
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trainingFormat == null)
            {
                return NotFound();
            }

            return View(trainingFormat);
        }

        // GET: TrainingFormatController/Create
        public IActionResult Create()
        {
            Access();
            return View();
        }

        // POST: TrainingFormatController/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,MaxClients")] TrainingFormat trainingFormat)
        {
            Access();
            if (ModelState.IsValid)
            {
                _context.Add(trainingFormat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trainingFormat);
        }

        // GET: TrainingFormatController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            Access();
            if (id == null || _context.TrainingFormats == null)
            {
                return NotFound();
            }

            var trainingFormat = await _context.TrainingFormats.FindAsync(id);
            if (trainingFormat == null)
            {
                return NotFound();
            }
            return View(trainingFormat);
        }

        // POST: TrainingFormatController/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,MaxClients")] TrainingFormat trainingFormat)
        {
            Access();
            if (id != trainingFormat.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trainingFormat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainingFormatExists(trainingFormat.Id))
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
            return View(trainingFormat);
        }

        // GET: TrainingFormatController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            Access();
            if (id == null || _context.TrainingFormats == null)
            {
                return NotFound();
            }

            var trainingFormat = await _context.TrainingFormats
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trainingFormat == null)
            {
                return NotFound();
            }

            return View(trainingFormat);
        }

        // POST: TrainingFormatController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Access();
            if (_context.TrainingFormats == null)
            {
                return Problem("Entity set 'PracticeCrossfitContext.TrainingFormats'  is null.");
            }
            var trainingFormat = await _context.TrainingFormats.FindAsync(id);
            if (trainingFormat != null)
            {
                _context.TrainingFormats.Remove(trainingFormat);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrainingFormatExists(int id)
        { 
            Access();
            return (_context.TrainingFormats?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
