using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CardsApp.Data;
using CardsApp.Models;

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
            var model = new ViewBoardModel();
            model.Players = _context.Players.ToList();
            model.PlayerSums = new Dictionary<Player, decimal>();
            foreach (var player in model.Players)
            {
                model.PlayerSums.Add(player, _context.Board.Where(be => be.Player.Id == player.Id).Sum(be => be.Difference)); 
            }
            model.Board = new Dictionary<GamePlayed, IEnumerable<BoardEntry>>();
            foreach (var gamePlayed in _context.GamesPlayed.Include(gp => gp.Game).OrderByDescending(gp => gp.DateTime))
            {

                model.Board.Add(gamePlayed, _context.Board.Include(be => be.Player).Where(entry => entry.Game.Id == gamePlayed.Id));
            }

            return View(model);
        }

    }
}
