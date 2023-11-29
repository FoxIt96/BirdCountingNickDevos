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
            var person = _birdService.Get(id);
            return View(person);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, Bird bird)
        {
            if (!ModelState.IsValid)
            {
                return View(bird);
            }

            _birdService.Update(id, bird);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete([FromRoute] int id)
        {
            var bird = _birdService.Get(id);
            return View(bird);
        }

        [HttpPost("/bird/delete/{id:int}")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed([FromRoute] int id)
        {
            _birdService.Delete(id);

            return RedirectToAction("Index");
        }
    }
}
