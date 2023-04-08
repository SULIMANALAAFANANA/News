using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using News.Data;
using News.Models;

namespace News.Controllers
{
    public class SportNewsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SportNewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SportNews
        public async Task<IActionResult> Index()
        {
              return _context.SportNews != null ? 
                          View(await _context.SportNews.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.SportNews'  is null.");
        }

        // GET: SportNews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SportNews == null)
            {
                return NotFound();
            }

            var sportNews = await _context.SportNews
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sportNews == null)
            {
                return NotFound();
            }

            return View(sportNews);
        }

        // GET: SportNews/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SportNews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Email")] SportNews sportNews)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sportNews);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sportNews);
        }

        // GET: SportNews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SportNews == null)
            {
                return NotFound();
            }

            var sportNews = await _context.SportNews.FindAsync(id);
            if (sportNews == null)
            {
                return NotFound();
            }
            return View(sportNews);
        }

        // POST: SportNews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Email")] SportNews sportNews)
        {
            if (id != sportNews.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sportNews);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SportNewsExists(sportNews.Id))
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
            return View(sportNews);
        }

        // GET: SportNews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SportNews == null)
            {
                return NotFound();
            }

            var sportNews = await _context.SportNews
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sportNews == null)
            {
                return NotFound();
            }

            return View(sportNews);
        }

        // POST: SportNews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SportNews == null)
            {
                return Problem("Entity set 'ApplicationDbContext.SportNews'  is null.");
            }
            var sportNews = await _context.SportNews.FindAsync(id);
            if (sportNews != null)
            {
                _context.SportNews.Remove(sportNews);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SportNewsExists(int id)
        {
          return (_context.SportNews?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
