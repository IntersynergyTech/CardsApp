using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using CardsApp.PokerModels;
using CardsApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace CardsApp.Controllers
{
    public class ChineseController : Controller
    {
        public ChineseScoringService _chineseScoringService;

        public ChineseController()
        {
            _chineseScoringService = new ChineseScoringService();
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Calculate(ChineseFormModel model)
        {
            var player1 = GetBoardFromForm(model.Player1Top, model.Player1Middle, model.Player1Bottom);
            var player2 = GetBoardFromForm(model.Player2Top, model.Player2Middle, model.Player2Bottom);
            var score = _chineseScoringService.Points(player1, player2);
            var p1Fantasy = player1.IsFantasyLand;
            var p2Fantasy = player2.IsFantasyLand;
            var returnable = new ChineseCalcViewModel{TotalPoints = score, Player1Fantasy = p1Fantasy, Player2Fantasy = p2Fantasy};
            return await Index();
        }

        private ChinesePokerBoard GetBoardFromForm(string top, string middle, string bottom)
        {
            var topCards = PokerCardStringParser.ParseCards(top);
            var middleCards = PokerCardStringParser.ParseCards(middle);
            var bottomCards = PokerCardStringParser.ParseCards(bottom);
            var topHand = new ChinesePokerHand(ChineseHandPosition.Top, topCards);
            var middleHand = new ChinesePokerHand(ChineseHandPosition.Middle, middleCards);
            var bottomHand = new ChinesePokerHand(ChineseHandPosition.Bottom, bottomCards);
            var returnable = new ChinesePokerBoard(new List<ChinesePokerHand> {topHand, middleHand, bottomHand});
            return returnable;
        }
    }

    public class ChineseFormModel
    {
        [Display(Name = "Top Row (3 Cards)")] public string Player1Top { get; set; }
        [Display(Name = "Top Row (3 Cards)")] public string Player2Top { get; set; }

        [Display(Name = "Middle Row (5 Cards)")]
        public string Player1Middle { get; set; }

        [Display(Name = "Middle Row (5 Cards)")]
        public string Player2Middle { get; set; }

        [Display(Name = "Bottom Row (5 Cards)")]
        public string Player1Bottom { get; set; }

        [Display(Name = "Bottom Row (5 Cards)")]
        public string Player2Bottom { get; set; }
    }

    public class ChineseCalcViewModel
    {
        public int TotalPoints { get; set; }
        public bool Player1Fantasy { get; set; }
        public bool Player2Fantasy { get; set; }
    }
}