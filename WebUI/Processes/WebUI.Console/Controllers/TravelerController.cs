using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TraPa.DataAccess.Repositories.Public.Interfaces;
using TraPa.Entities.Impl.Classes;

namespace WebUI.Console.Controllers
{
    public class TravelerController : Controller
    {
        private readonly ITravelerRepository _travelerRepository;

        public TravelerController(IDatabaseRepositoryFactory repositoryFactory)
        {
            _travelerRepository = repositoryFactory.GetNewTravelerRepository();
        }

        public IActionResult Index()
        {
            var travelers = _travelerRepository.GetAll().ToList();
            return View(travelers);
        }

        public IActionResult NewTraveler()
        {
            return View("Save", new Traveler());
        }

        public IActionResult Save([Required]Traveler traveler)
        {
            if (ModelState.IsValid)
            {
                _travelerRepository.InnerAdd(traveler);
            }

            return RedirectToAction("Index", "Traveler");
        }

        public IActionResult Edit([Required]Traveler traveler)
        {
            if (ModelState.IsValid)
            {
                return View(traveler);
            }

            return View();
        }

        public IActionResult Update([Required]Traveler traveler)
        {
            if (ModelState.IsValid)
            {
                _travelerRepository.Update(traveler);
            }

            return RedirectToAction("Index", "Traveler");
        }

        public IActionResult Remove([Required]Traveler traveler)
        {
            if (ModelState.IsValid)
            {
                _travelerRepository.Remove(traveler);
            }

            return RedirectToAction("Index", "Traveler");
        }
    }
}