using BirdCounting.UI.mvc.Models;
using BirdCounting.Services;
using Microsoft.AspNetCore.Mvc;
using System;
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
