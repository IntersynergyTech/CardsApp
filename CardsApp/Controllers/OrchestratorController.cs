using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CardsApp.Data;
using CardsApp.Misc;
using CardsApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language.CodeGeneration;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CardsApp.Controllers
{
    public class OrchestratorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrchestratorController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var model = new StartGameModel();
            model.Games = _context.Games.ToList();
            model.Players = _context.Players.OrderByDescending(player => player.LastPlayed).ToList();

            var asd = model.Games.ToList().GetDropdownItems<Game>("Name");
            Console.WriteLine(JsonConvert.SerializeObject(asd));

            return View("StartGame", model);
        }

        public ActionResult CreateGame(StartGameModel model)
        {
            var newGamePlayed = new GamePlayed();
            var players = new List<GamePlayers>();
            players.AddRange(_context.Players.Where(pl => model.SelectedPlayers.Contains(pl.Id)).Select(x => new GamePlayers(){Player = x}));
            var game = _context.Games.First(ga => ga.Id == model.SelectedGame);
            newGamePlayed.Players = players;
            newGamePlayed.Game = game;
            if (model.SelectedGameStake != null)
            {
                newGamePlayed.Stake = model.SelectedGameStake.Value;
            }
            else
            {
                newGamePlayed.Stake = game.DefaultStake;
            }

            _context.GamesPlayed.Add(newGamePlayed);
            _context.SaveChanges();
            return RedirectToAction("Manage", new { gameId = newGamePlayed.Id});
        }

        public IActionResult Manage(Guid gameId)
        {
            var game = _context.GamesPlayed
                .Include(gp => gp.Game)
                .Include(gp => gp.Players).ThenInclude(gp => gp.Player)
                .Include(gp => gp.Hands)
                .First(g => g.Id == gameId);
            return GameManagerView(game);
        }

        private IActionResult GameManagerView(GamePlayed game)
        {
            var GameTypeAttributes = game.Game.Type.GetAttributes();
            if (GameTypeAttributes.Any(attr => attr.GetType() == typeof(AccrualHandsAttribute)))
            {
                return View("ManageAccrual", game);
            }
            else if(GameTypeAttributes.Any(attr => attr.GetType() == typeof(FixedHandsAttribute)))
            {
                return View("ManageFixedHands", game);
            }else
            {
                return View("ManageSimple", game);
            }
        }
    }
}