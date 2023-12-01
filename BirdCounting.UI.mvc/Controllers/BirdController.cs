using Microsoft.AspNetCore.Mvc;
using BirdCounting.Model;
using BirdCounting.Services;
using System;

namespace BirdCounting.UI.mvc.Controllers
{
    public class BirdController : Controller
    {
        private readonly BirdService _birdService;

        public BirdController(BirdService birdService)
        {
            _birdService = birdService;
        }

        public IActionResult Index()
        {
            var birds = _birdService.Find();
            return View(birds);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Bird bird)
        {
            if (!ModelState.IsValid)
            {
                return View(bird);
            }

            _birdService.Create(bird);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit([FromRoute] int id)
        {
            var bird = _birdService.Get(id);
            if (bird == null)
            {
                return NotFound();
            }

            return View(bird);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, Bird bird)
        {
            if (!ModelState.IsValid)
            {
                return View(bird);
            }

            var updatedBird = _birdService.Update(id, bird);
            if (updatedBird == null)
            {
                return NotFound();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete([FromRoute] int id)
        {
            var bird = _birdService.Get(id);
            if (bird == null)
            {
                return NotFound();
            }

            return View(bird);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed([FromRoute] int id)
        {
            var bird = _birdService.Get(id);
            if (bird == null)
            {
                return NotFound();
            }

            _birdService.Delete(id);

            return RedirectToAction("Index");
        }


        public IActionResult Counting()
        {
            // Retrieve the list of birds sorted by frequency (implement this in your service)
            var sessionId = _birdService.GetCurrentSessionId();
            var birds = _birdService.GetBirdsSortedByFrequency();

             ViewData["SessionId"] = sessionId;
            return View(birds);
        }

        [HttpPost]
        public IActionResult Count(int birdId)
        {
            // Implement logic to increase the count for the bird with birdId
            _birdService.CountBird(birdId, 1);

            // Redirect back to the counting page
            return RedirectToAction("Counting");
        }

        [HttpPost]
        public IActionResult CorrectCount(int birdId)
        {
            // Implement logic to decrease the count for the bird with birdId
            _birdService.CountBird(birdId, -1);

            // Redirect back to the counting page
            return RedirectToAction("Counting");
        }
        [HttpPost]
        public IActionResult StopSession(int sessionId)
        {
            _birdService.StopSession(sessionId);

            // Redirect back to the counting page
            return RedirectToAction("Index", "Home");
        }
    }
}
