using Assignment10.Models;
using Assignment10.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
//this is the cutie pie home controller.  important stuff happens here
namespace Assignment10.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private BowlingLeagueContext context { get; set; }

        public HomeController(ILogger<HomeController> logger, BowlingLeagueContext ctx)
        {
            _logger = logger;
            context = ctx;
        }

        
        public IActionResult Index(long? teamid, string team/* = ""*/, int pageNum = 1)
        {
            int pageSize = 5;
            //int pageNum = 2;
            ViewBag.SelectedTeam = team;
            return View(new IndexViewModel
            {
                Bowlers = (context.Bowlers
                .Where(m => m.TeamId == teamid || teamid == null)
                .OrderBy(m => m.Team)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize)
                .ToList()),
                //.FromSqlInterpolated($"SELECT * FROM Bowlers WHERE TeamID = {teamid} OR {teamid} IS NULL")
                //.FromSqlRaw("SELECT * FROM Bowler WHERE )
                //.FromSqlInterpolated()
                //.ToList());

                PageNumberingInfo = new PageNumberingInfo
                {
                    NumItemsPerPage = pageSize,
                    CurrentPage = pageNum,
                    TotalNumItems = (teamid == null ? context.Bowlers.Count() :
                    context.Bowlers.Where(x => x.TeamId == teamid).Count())
                },
                Team = team 
            });

    }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
