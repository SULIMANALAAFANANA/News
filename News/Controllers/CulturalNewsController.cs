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
    public class CulturalNewsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CulturalNewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CulturalNews
        public async Task<IActionResult> Index()
        {
              return _context.CulturalNews != null ? 
                          View(await _context.CulturalNews.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.CulturalNews'  is null.");
        }

        // GET: CulturalNews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CulturalNews == null)
            {
                return NotFound();
            }

            var culturalNews = await _context.CulturalNews
                .FirstOrDefaultAsync(m => m.Id == id);
            if (culturalNews == null)
            {
                return NotFound();
            }

            return View(culturalNews);
        }

        // GET: CulturalNews/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CulturalNews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Email")] CulturalNews culturalNews)
        {
            if (ModelState.IsValid)
            {
                _context.Add(culturalNews);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(culturalNews);
        }

        // GET: CulturalNews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CulturalNews == null)
            {
                return NotFound();
            }

            var culturalNews = await _context.CulturalNews.FindAsync(id);
            if (culturalNews == null)
            {
                return NotFound();
            }
            return View(culturalNews);
        }

        // POST: CulturalNews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Email")] CulturalNews culturalNews)
        {
            if (id != culturalNews.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(culturalNews);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CulturalNewsExists(culturalNews.Id))
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
            return View(culturalNews);
        }

        // GET: CulturalNews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CulturalNews == null)
            {
                return NotFound();
            }

            var culturalNews = await _context.CulturalNews
                .FirstOrDefaultAsync(m => m.Id == id);
            if (culturalNews == null)
            {
                return NotFound();
            }

            return View(culturalNews);
        }

        // POST: CulturalNews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CulturalNews == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CulturalNews'  is null.");
            }
            var culturalNews = await _context.CulturalNews.FindAsync(id);
            if (culturalNews != null)
            {
                _context.CulturalNews.Remove(culturalNews);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CulturalNewsExists(int id)
        {
          return (_context.CulturalNews?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
