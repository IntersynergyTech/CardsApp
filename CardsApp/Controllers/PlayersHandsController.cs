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
    public class PlayersHandsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlayersHandsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PlayersHands
        public async Task<IActionResult> Index()
        {
            return View(await _context.PlayerHands.ToListAsync());
        }

        // GET: PlayersHands/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playerHand = await _context.PlayerHands
                .FirstOrDefaultAsync(m => m.Id == id);
            if (playerHand == null)
            {
                return NotFound();
            }

            return View(playerHand);
        }

        // GET: PlayersHands/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PlayersHands/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,KnockedOut,Score")] PlayerHand playerHand)
        {
            if (ModelState.IsValid)
            {
                playerHand.Id = Guid.NewGuid();
                _context.Add(playerHand);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(playerHand);
        }

        // GET: PlayersHands/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playerHand = await _context.PlayerHands.FindAsync(id);
            if (playerHand == null)
            {
                return NotFound();
            }
            return View(playerHand);
        }

        // POST: PlayersHands/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,KnockedOut,Score")] PlayerHand playerHand)
        {
            if (id != playerHand.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(playerHand);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlayerHandExists(playerHand.Id))
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
            return View(playerHand);
        }

        // GET: PlayersHands/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playerHand = await _context.PlayerHands
                .FirstOrDefaultAsync(m => m.Id == id);
            if (playerHand == null)
            {
                return NotFound();
            }

            return View(playerHand);
        }

        // POST: PlayersHands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var playerHand = await _context.PlayerHands.FindAsync(id);
            _context.PlayerHands.Remove(playerHand);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlayerHandExists(Guid id)
        {
            return _context.PlayerHands.Any(e => e.Id == id);
        }
    }
}
