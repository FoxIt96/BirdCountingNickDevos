using BirdCounting.Services;
using BirdCounting.UI.mvc.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BirdCounting.UI.mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly BirdService _birdService;

        public HomeController(BirdService birdService)
        {
            _birdService = birdService;
        }

        public IActionResult Index()
        {
            var sessions = _birdService.GetSessions();
            return View(sessions);
        }

        public IActionResult RedirectToHomeIndex()
        {
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult SessionDetails(int id)
        {
            var session = _birdService.GetSessionDetails(id);
            if (session == null)
            {
                return NotFound(); // Of een andere actie afhankelijk van je behoeften
            }

            return View(session);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
