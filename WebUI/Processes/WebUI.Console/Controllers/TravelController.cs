using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TraPa.DataAccess.Repositories.Public.Interfaces;
using TraPa.Entities.Impl.Classes;

namespace WebUI.Console.Controllers
{
    public class TravelController : Controller
    {
        private readonly ITravelDateRepository _travelDateRepository;
        //private readonly ITravelerTravelDateReferenceRepository _var;

        public TravelController(IDatabaseRepositoryFactory repositoryFactory)
        {
            _travelDateRepository = repositoryFactory.GetNewTravelDateRepository();
            //_var = repositoryFactory.GetNewTravelerTravelDateReferenceRepository();
        }

        public IActionResult Index()
        {
            var travelers = _travelDateRepository.GetAll().ToList();
            return View(travelers);
        }

        public IActionResult NewTravel()
        {
            return View("Save", new TravelDate() {DateOfTravel = DateTime.Today, TravelerTravelDateReferences = new List<TravelerTravelDateReference>()});
        }

        //[ValidateAntiForgeryToken]
        public IActionResult Save([Required] TravelDate travelDate)
        {
            if (ModelState.IsValid)
            {
                _travelDateRepository.InnerAdd(travelDate);
                return RedirectToAction("Index", "Travel");
            }
            
            return View();
        }

        //[ValidateAntiForgeryToken]
        public IActionResult Edit([Required]TravelDate travelDate)
        {
            if (ModelState.IsValid)
            {
                //_var.Add(24, travelDate.Id);
                return View(travelDate);
            }
            else
                return View();
        }

        //[ValidateAntiForgeryToken]
        public IActionResult Update([Required]TravelDate travelDate)
        {
            if (ModelState.IsValid)
            {
                _travelDateRepository.Update(travelDate);
            }
            return RedirectToAction("Index", "Travel");
        }
        
        //[ValidateAntiForgeryToken]
        public IActionResult Remove([Required]TravelDate travelDate)
        {
            if (ModelState.IsValid)
            {
                _travelDateRepository.Remove(travelDate);
            }

            return RedirectToAction("Index", "Travel");
        }

        public IActionResult Export()
        {
            throw new NotImplementedException();
        }
    }
}