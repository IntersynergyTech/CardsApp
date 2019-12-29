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
    public class GamePlayersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GamePlayersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: GamePlayers
        public async Task<IActionResult> Index()
        {
            return View(await _context.GamesPlayedByPlayers.ToListAsync());
        }

        // GET: GamePlayers/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gamePlayers = await _context.GamesPlayedByPlayers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gamePlayers == null)
            {
                return NotFound();
            }

            return View(gamePlayers);
        }

        // GET: GamePlayers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GamePlayers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id")] GamePlayers gamePlayers)
        {
            if (ModelState.IsValid)
            {
                gamePlayers.Id = Guid.NewGuid();
                _context.Add(gamePlayers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gamePlayers);
        }

        // GET: GamePlayers/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gamePlayers = await _context.GamesPlayedByPlayers.FindAsync(id);
            if (gamePlayers == null)
            {
                return NotFound();
            }
            return View(gamePlayers);
        }

        // POST: GamePlayers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id")] GamePlayers gamePlayers)
        {
            if (id != gamePlayers.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gamePlayers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GamePlayersExists(gamePlayers.Id))
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
            return View(gamePlayers);
        }

        // GET: GamePlayers/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gamePlayers = await _context.GamesPlayedByPlayers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gamePlayers == null)
            {
                return NotFound();
            }

            return View(gamePlayers);
        }

        // POST: GamePlayers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var gamePlayers = await _context.GamesPlayedByPlayers.FindAsync(id);
            _context.GamesPlayedByPlayers.Remove(gamePlayers);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GamePlayersExists(Guid id)
        {
            return _context.GamesPlayedByPlayers.Any(e => e.Id == id);
        }
    }
}
