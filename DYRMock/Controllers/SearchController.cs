using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DYRMock.Models;

namespace DYRMock.Controllers
{
    public class SearchController : Controller
    {
        public ActionResult Results()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Results(PetViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.Pet.Facility.SanitizeLocation(model.Pet.Facility.FullLocation);
                model.SetPetsByKeyword(model.Pet.Facility.FullLocation, model.SelectedPetId, model.Pet.Breed.BreedType);                   
            }

            return View(model);
        }
    }
}
