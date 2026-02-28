using DecisionHelper.Data;
using DecisionHelper.Models;
using DecisionHelper.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DecisionHelper.Controllers
{
    //Handles decision inputs, calculation, flair and personalised recommendation
    public class DecisionController : Controller
    {
        private readonly DecisionService _service = new();
        private readonly AppDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;


        public DecisionController(AppDbContext db, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        
        //Shows the main decision page with available flairs and login
        public async Task<IActionResult> Index()
        {
            var flairs = await _db.Flairs.ToListAsync();
            ViewBag.Flairs = flairs;
            ViewBag.IsLoggedIn = User.Identity?.IsAuthenticated ?? false;
            return View();
        }

        //Returns top 5 most used options for a selected flair
        [HttpGet]
        public async Task<IActionResult> Recommendations(int flairId)
        {
            var top5 = await _db.DecisionRecords
                .Where(r => r.FlairId == flairId)
                .GroupBy(r => r.WinningOption)
                .Select(g => new { Option = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .Take(5)
                .ToListAsync();

            return Json(top5);
        }


        //Returns top 5 most used options for a selected flair by name
        [HttpGet]
        public async Task<IActionResult> RecommendationsByName(string flairName)
        {
            var flair = await _db.Flairs
                .FirstOrDefaultAsync(f => f.Name.ToLower() == flairName.ToLower());

            if (flair == null) return Json(new List<object>());

            var top5 = await _db.DecisionRecords
                .Where(r => r.FlairId == flair.Id)
                .GroupBy(r => r.WinningOption)
                .Select(g => new { Option = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .Take(5)
                .ToListAsync();

            return Json(top5);
        }

        // Returns top 5 most used options by all users for a selected flair
        [HttpGet]
        public async Task<IActionResult> TopOptions(int flairId)
        {
            var top5 = await _db.DecisionOptions
                .Where(o => o.FlairId == flairId)
                .GroupBy(o => o.OptionName)
                .Select(g => new { Option = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .Take(5)
                .ToListAsync();

            return Json(top5);
        }

        //Returns the top 5 options of the current user for a selected flair
        //Returns empty if user is not authenticated
        [HttpGet]
        public async Task<IActionResult> MyTopOptions(int flairId)
        {
            if (!(User.Identity?.IsAuthenticated ?? false))
                return Json(new List<object>());

            var userId = _userManager.GetUserId(User)!;

            var top5 = await _db.UserDecisionOptions
                .Where(o => o.UserId == userId && o.FlairId == flairId)
                .GroupBy(o => o.OptionName)
                .Select(g => new { Option = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .Take(5)
                .ToListAsync();

            return Json(top5);
        }

        //Returns the top5 criteria of the current user for a selected flair
        //Returns empty if the user is not authenticated
        [HttpGet]
        public async Task<IActionResult> MyTopCriteria(int flairId)
        {
            if (!(User.Identity?.IsAuthenticated ?? false))
                return Json(new List<object>());

            var userId = _userManager.GetUserId(User)!;

            var top5 = await _db.UserCriteria
                .Where(c => c.UserId == userId && c.FlairId == flairId)
                .GroupBy(c => c.CriterionName)
                .Select(g => new
                {
                    Option = g.Key,
                    Count = g.Count(),
                    IsHigher = g.Sum(x => x.IsHigher ? 1 : -1) >= 0
                })
                .OrderByDescending(x => x.Count)
                .Take(5)
                .ToListAsync();

            return Json(top5);
        }

        //Weighted calculation
        [HttpPost]
        public async Task<IActionResult> Calculate(IFormCollection form)
        {
            if (!int.TryParse(form["optionCount"], out int optionCount))
                return Content("Score table was not generated. Please go back and click 'Generate Score Table'.");

            if (!int.TryParse(form["criterionCount"], out int criterionCount))
                return Content("Score table was not generated. Please go back and click 'Generate Score Table'.");

            var options = new List<Options>();
            var criteria = new List<Criterion>();
            var scores = new List<ScoreEntry>();
            var choices = new List<ChooseFromTwo>();

            int flairId = 0;
            string flairName = form["flairName"].ToString().Trim();
            if (!string.IsNullOrEmpty(flairName))
            {
                var flair = await _db.Flairs
                    .FirstOrDefaultAsync(f => f.Name.ToLower() == flairName.ToLower());

                //Creates flair if it doesnt exist
                if (flair == null)
                {
                    flair = new Flair { Name = flairName };
                    _db.Flairs.Add(flair);
                    await _db.SaveChangesAsync();
                }
                flairId = flair.Id;
            }

            for (int o = 1; o <= optionCount; o++)
            {
                options.Add(new Options { Name = form[$"option{o}"].ToString() });
            }

            for (int c = 1; c <= criterionCount; c++)
            {
                bool isHigher = form[$"higher{c}"].ToString() == "true";
                criteria.Add(new Criterion
                {
                    Name = form[$"criterion{c}"].ToString(),
                    IsHigher = isHigher,
                    Weight = 0
                });
            }

            for (int o = 1; o <= optionCount; o++)
            {
                for (int c = 1; c <= criterionCount; c++)
                {
                    if (!double.TryParse(form[$"score_{o}_{c}"].ToString(), out double value))
                        return Content($"Invalid score for Option {o}, Criterion {c}");

                    scores.Add(new ScoreEntry
                    {
                        OptionName = options[o - 1].Name,
                        CriterionName = criteria[c - 1].Name,
                        Value = value
                    });
                }
            }

            //Pairwise comparison
            int pairCount = int.TryParse(form["pairCount"].FirstOrDefault(), out int pc) ? pc : 0;
            for (int p = 1; p <= pairCount; p++)
            {
                choices.Add(new ChooseFromTwo
                {
                    CriteriaA = form[$"pairA_{p}"].ToString(),
                    CriteriaB = form[$"pairB_{p}"].ToString(),
                    Winner = form[$"winner_{p}"].ToString()
                });
            }

            var input = new DecisionInput
            {
                Options = options,
                Criteria = criteria,
                Scores = scores,
                PairwiseChoices = choices
            };

            var results = _service.Calculate(input);

            if (flairId > 0)
            {
                if (results.Any())
                {
                    _db.DecisionRecords.Add(new DecisionRecord
                    {
                        FlairId = flairId,
                        WinningOption = results.First().Key,
                        CreatedAt = DateTime.UtcNow
                    });
                }

                foreach (var option in options)
                {
                    _db.DecisionOptions.Add(new DecisionOption
                    {
                        FlairId = flairId,
                        OptionName = option.Name,
                        CreatedAt = DateTime.UtcNow
                    });
                }
                if (User.Identity?.IsAuthenticated ?? false)
                {
                    var userId = _userManager.GetUserId(User)!;

                    foreach (var option in options)
                    {
                        _db.UserDecisionOptions.Add(new UserDecisionOption
                        {
                            UserId = userId,
                            FlairId = flairId,
                            OptionName = option.Name,
                            CreatedAt = DateTime.UtcNow
                        });
                    }

                    foreach (var criterion in criteria)
                    {
                        _db.UserCriteria.Add(new UserCriterion
                        {
                            UserId = userId,
                            FlairId = flairId,
                            CriterionName = criterion.Name,
                            IsHigher = criterion.IsHigher,
                            CreatedAt = DateTime.UtcNow
                        });
                    }
                }

                await _db.SaveChangesAsync();
            }

            ViewBag.Results = results;
            ViewBag.FlairName = flairName;
            return View("Result");
        }
    }
}