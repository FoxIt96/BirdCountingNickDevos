﻿using BirdCounting.Services;
using BirdCounting.Model;
using Microsoft.AspNetCore.Mvc;

namespace BirdCounting.UI.mvc.Controllers
{
    public class SessionController : Controller
    {
        private readonly BirdService _birdService;

        public SessionController(BirdService birdService)
        {
            _birdService = birdService;
        }


        [HttpGet]
        public IActionResult CreateSession()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateSession(Session session)
        {
            if (!ModelState.IsValid)
            {
                return View(session);
            }

            _birdService.CreateSession(session);

            return RedirectToAction("AllSessions");
        }

        [HttpGet]
        public IActionResult AllSessions()
        {
            return RedirectToAction("Index", "Home");
        }
    }

}