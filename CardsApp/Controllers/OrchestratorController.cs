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
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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
            //Add the first hand too.
            var newhand = new Hand();
            newhand.Game = newGamePlayed;
            var scores = new List<PlayerHand>();
            foreach (var player in players)
            {
                var plhand = new PlayerHand();
                plhand.Player = player.Player;
                plhand.Hand = newhand;
                plhand.Done = false;
                scores.Add(plhand);
            }
            newhand.Scores = scores;
            newGamePlayed.Hands = new Hand[]{newhand};
            _context.SaveChanges();
            return RedirectToAction("Manage", new { gameId = newGamePlayed.Id});
        }

        public IActionResult Manage(Guid gameId)
        {
            var game = GetGame(gameId);
            return GameManagerView(game);
        }

        public IActionResult View(Guid gameId)
        {
            var game = GetGame(gameId);
            return View("ViewGame", game);
        }

        private GamePlayed GetGame(Guid gameId)
        {
            return _context.GamesPlayed
                .Include(gp => gp.Game)
                .Include(gp => gp.Winner)
                .Include(gp => gp.Players).ThenInclude(gp => gp.Player)
                .Include(gp => gp.Hands).ThenInclude(h => h.Scores)
                .First(g => g.Id == gameId);
        }

        public JsonResult NewHand(Guid gameId)
        {
            var game = GetGame(gameId);
            //Check if the last hand was completed first.
            if (!game.Hands.OrderByDescending(hand => hand.DateTime).First().Scores.All(ph => ph.Done))
            {
                return Json(false); //Previous hand not completed.
            }
            //Start building a new hand
            var eligibleplayers = _context.Hands.OrderByDescending(hand => hand.DateTime).First().Scores
                .Where(ph => !ph.KnockedOut);

            var newhand = new Hand();
            newhand.Game = game;
            var scores = new List<PlayerHand>();
            foreach (var player in eligibleplayers)
            {
                var plhand = new PlayerHand();
                plhand.Player = player.Player;
                plhand.Hand = newhand;
                plhand.Done = false;
                scores.Add(plhand);
            }
            newhand.Scores = scores;
            _context.Hands.Add(newhand);
            _context.SaveChanges();
            return Json(true);
        }

        public JsonResult SubmitPlayerHandAccrual(Guid handId, int score)
        {
            var hand = _context.PlayerHands
                .Include(ph => ph.Player)
                .Include(ph => ph.Hand)
                .ThenInclude(h => h.Game)
                .ThenInclude(gp => gp.Game)
                .Include(ph => ph.Hand.Scores)
                .ThenInclude(ph => ph.Player)
                .Single(ph => ph.Id == handId);
            hand.Score = score;
            hand.Done = true;
            //Check if we're out
            var totalscore = _context.PlayerHands.Where(ph =>
                ph.Player.Id == hand.Player.Id && ph.Hand.Game.Id == hand.Hand.Game.Id).Sum(x => x.Score) + score;
            _context.SaveChanges();
            if (totalscore >= hand.Hand.Game.Game.MaximumScore)
            {
                hand.KnockedOut = true;
            }
            _context.SaveChanges();
            //Progress the game by adding another hand if this one is done and there's more than 1 player still in or everyone is drawn and the current hand is complete
            if (_context.Hands.Include(h => h.Scores).Single(h => h.Id == hand.Hand.Id).Scores.All(ph => ph.Done))
            {
                bool addhand = true;
                if (hand.Hand.Scores.Count(ph => !ph.KnockedOut) < 2)
                {
                    //Game over, somebody is winrar
                    var winner = hand.Hand.Scores.Single(ph => !ph.KnockedOut).Player;
                    hand.Hand.Game.Winner = winner;
                    hand.Hand.Game.GameComplete = true;
                    hand.Hand.Game.IsDraw = false;
                    addhand = false;
                    Payout(hand.Hand.Game, winner);
                }
                //Check for a draw
                if (addhand && hand.Hand.Scores.All(ph => _context.PlayerHands.Where(x => x.Player.Id == ph.Player.Id && x.Hand.Game.Id == ph.Hand.Game.Id).Sum(ph2 => ph2.Score) >= hand.Hand.Game.Game.DrawPosition))
                {
                    //We draw now
                    hand.Hand.Game.IsDraw = true;
                    hand.Hand.Game.GameComplete = true;
                    addhand = false;
                }
                _context.SaveChanges();
                //Looks good, continue game
                if (addhand) //Add a new hand.
                {
                    NewHand(hand.Hand.Game.Id);
                }
            }
            return Json(hand.KnockedOut);
        }

        private void Payout(GamePlayed game, Player winner)
        {
            if (game.GameComplete)
            {
                _context.SaveChanges();
                var gamebetter = _context.GamesPlayed.Include(gp => gp.Winner).Include(gp => gp.Players).ThenInclude(gpp => gpp.Player).Single(g => g.Id == game.Id);
                var stake = gamebetter.Stake;
                foreach (var player in gamebetter.Players)
                {
                    var be = new BoardEntry();
                    be.Game = gamebetter;
                    be.Player = player.Player;
                    if (gamebetter.Winner.Id == player.Player.Id)
                    {
                        var winnings = gamebetter.Players.Count() * stake;
                        be.Difference = winnings-stake;
                    }
                    else
                    {
                        be.Difference = -stake;
                    }

                    _context.Board.Add(be);
                }

                _context.SaveChanges();
            }
            
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