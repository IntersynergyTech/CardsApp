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
    public class GamesPlayedController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GamesPlayedController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: GamesPlayed
        public async Task<IActionResult> Index()
        {
            return View(await _context.GamesPlayed.ToListAsync());
        }

        // GET: GamesPlayed/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gamePlayed = await _context.GamesPlayed
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gamePlayed == null)
            {
                return NotFound();
            }

            return View(gamePlayed);
        }

        // GET: GamesPlayed/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GamesPlayed/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DateTime,GameComplete,IsDraw,Stake")] GamePlayed gamePlayed)
        {
            if (ModelState.IsValid)
            {
                gamePlayed.Id = Guid.NewGuid();
                _context.Add(gamePlayed);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gamePlayed);
        }

        // GET: GamesPlayed/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gamePlayed = await _context.GamesPlayed.FindAsync(id);
            if (gamePlayed == null)
            {
                return NotFound();
            }
            return View(gamePlayed);
        }

        // POST: GamesPlayed/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,DateTime,GameComplete,IsDraw,Stake")] GamePlayed gamePlayed)
        {
            if (id != gamePlayed.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gamePlayed);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GamePlayedExists(gamePlayed.Id))
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
            return View(gamePlayed);
        }

        // GET: GamesPlayed/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gamePlayed = await _context.GamesPlayed
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gamePlayed == null)
            {
                return NotFound();
            }

            return View(gamePlayed);
        }

        // POST: GamesPlayed/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var gamePlayed = await _context.GamesPlayed.FindAsync(id);
            _context.GamesPlayed.Remove(gamePlayed);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GamePlayedExists(Guid id)
        {
            return _context.GamesPlayed.Any(e => e.Id == id);
        }
    }
}
