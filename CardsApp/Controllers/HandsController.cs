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
    public class HandsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HandsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Hands
        public async Task<IActionResult> Index()
        {
            return View(await _context.Hands.ToListAsync());
        }

        // GET: Hands/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hand = await _context.Hands
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hand == null)
            {
                return NotFound();
            }

            return View(hand);
        }

        // GET: Hands/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Hands/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DateTime")] Hand hand)
        {
            if (ModelState.IsValid)
            {
                hand.Id = Guid.NewGuid();
                _context.Add(hand);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hand);
        }

        // GET: Hands/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hand = await _context.Hands.FindAsync(id);
            if (hand == null)
            {
                return NotFound();
            }
            return View(hand);
        }

        // POST: Hands/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,DateTime")] Hand hand)
        {
            if (id != hand.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hand);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HandExists(hand.Id))
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
            return View(hand);
        }

        // GET: Hands/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hand = await _context.Hands
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hand == null)
            {
                return NotFound();
            }

            return View(hand);
        }

        // POST: Hands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var hand = await _context.Hands.FindAsync(id);
            _context.Hands.Remove(hand);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HandExists(Guid id)
        {
            return _context.Hands.Any(e => e.Id == id);
        }
    }
}
