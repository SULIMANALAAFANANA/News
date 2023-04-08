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
    public class PoliticalNewsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PoliticalNewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PoliticalNews
        public async Task<IActionResult> Index()
        {
              return _context.PoliticalNews != null ? 
                          View(await _context.PoliticalNews.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.PoliticalNews'  is null.");
        }

        // GET: PoliticalNews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PoliticalNews == null)
            {
                return NotFound();
            }

            var politicalNews = await _context.PoliticalNews
                .FirstOrDefaultAsync(m => m.Id == id);
            if (politicalNews == null)
            {
                return NotFound();
            }

            return View(politicalNews);
        }

        // GET: PoliticalNews/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PoliticalNews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Email")] PoliticalNews politicalNews)
        {
            if (ModelState.IsValid)
            {
                _context.Add(politicalNews);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(politicalNews);
        }

        // GET: PoliticalNews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PoliticalNews == null)
            {
                return NotFound();
            }

            var politicalNews = await _context.PoliticalNews.FindAsync(id);
            if (politicalNews == null)
            {
                return NotFound();
            }
            return View(politicalNews);
        }

        // POST: PoliticalNews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Email")] PoliticalNews politicalNews)
        {
            if (id != politicalNews.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(politicalNews);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PoliticalNewsExists(politicalNews.Id))
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
            return View(politicalNews);
        }

        // GET: PoliticalNews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PoliticalNews == null)
            {
                return NotFound();
            }

            var politicalNews = await _context.PoliticalNews
                .FirstOrDefaultAsync(m => m.Id == id);
            if (politicalNews == null)
            {
                return NotFound();
            }

            return View(politicalNews);
        }

        // POST: PoliticalNews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PoliticalNews == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PoliticalNews'  is null.");
            }
            var politicalNews = await _context.PoliticalNews.FindAsync(id);
            if (politicalNews != null)
            {
                _context.PoliticalNews.Remove(politicalNews);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PoliticalNewsExists(int id)
        {
          return (_context.PoliticalNews?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
