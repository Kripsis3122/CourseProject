using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FactsApp.Models;

namespace FactsApp.Controllers
{
    public class FactsController : Controller
    {
        private readonly FactsContext _context;

        public FactsController(FactsContext context)
        {
            _context = context;
        }

        // GET: Facts
        public async Task<IActionResult> Index()
        {
              return _context.Facts != null ? 
                          View(await _context.Facts.ToListAsync()) :
                          Problem("Entity set 'FactsContext.Facts'  is null.");
        }

        // GET: Facts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Facts == null)
            {
                return NotFound();
            }

            var fact = await _context.Facts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fact == null)
            {
                return NotFound();
            }

            return View(fact);
        }

        // GET: Facts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Facts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Author,FactTitle,FactBody")] Fact fact)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fact);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fact);
        }

        // GET: Facts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Facts == null)
            {
                return NotFound();
            }

            var fact = await _context.Facts.FindAsync(id);
            if (fact == null)
            {
                return NotFound();
            }
            return View(fact);
        }

        // POST: Facts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Author,FactTitle,FactBody")] Fact fact)
        {
            if (id != fact.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fact);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FactExists(fact.Id))
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
            return View(fact);
        }

        // GET: Facts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Facts == null)
            {
                return NotFound();
            }

            var fact = await _context.Facts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fact == null)
            {
                return NotFound();
            }

            return View(fact);
        }

        // POST: Facts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Facts == null)
            {
                return Problem("Entity set 'FactsContext.Facts'  is null.");
            }
            var fact = await _context.Facts.FindAsync(id);
            if (fact != null)
            {
                _context.Facts.Remove(fact);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FactExists(int id)
        {
          return (_context.Facts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        public async Task<IActionResult> SortById()
        {
            var facts = await _context.Facts.OrderByDescending(c => c.Id).ToListAsync();

            return View("Index", facts);
        }

        public async Task<IActionResult> SortByTitle()
        {
            var facts = await _context.Facts.OrderByDescending(c => c.FactTitle).ToListAsync();

            return View("Index", facts);
        }

        public async Task<IActionResult> SortByAuthor()
        {
            var facts = await _context.Facts.OrderByDescending(c => c.Author).ToListAsync();

            return View("Index", facts);
        }
    }
}
