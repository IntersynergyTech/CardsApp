using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CardsApp.Data;

namespace CardsApp.Controllers
{
    public class BoardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BoardController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Board
        public async Task<IActionResult> Index()
        {
            return View(await _context.Board.ToListAsync());
        }

        // GET: Board/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boardEntry = await _context.Board
                .FirstOrDefaultAsync(m => m.Id == id);
            if (boardEntry == null)
            {
                return NotFound();
            }

            return View(boardEntry);
        }

        // GET: Board/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Board/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Difference")] BoardEntry boardEntry)
        {
            if (ModelState.IsValid)
            {
                boardEntry.Id = Guid.NewGuid();
                _context.Add(boardEntry);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(boardEntry);
        }

        // GET: Board/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boardEntry = await _context.Board.FindAsync(id);
            if (boardEntry == null)
            {
                return NotFound();
            }
            return View(boardEntry);
        }

        // POST: Board/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Difference")] BoardEntry boardEntry)
        {
            if (id != boardEntry.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(boardEntry);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BoardEntryExists(boardEntry.Id))
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
            return View(boardEntry);
        }

        // GET: Board/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boardEntry = await _context.Board
                .FirstOrDefaultAsync(m => m.Id == id);
            if (boardEntry == null)
            {
                return NotFound();
            }

            return View(boardEntry);
        }

        // POST: Board/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var boardEntry = await _context.Board.FindAsync(id);
            _context.Board.Remove(boardEntry);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BoardEntryExists(Guid id)
        {
            return _context.Board.Any(e => e.Id == id);
        }
    }
}
