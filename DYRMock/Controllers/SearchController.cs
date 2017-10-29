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
                model.Pet.Facility.SanitizeLocation(model.Pet.Facility.Address.FullLocation);

                // Temporarily set the user location to the location being searched as this is the most likely location where the user
                // lives or wants to adopt a pet from.
                model.User.SetUserLocation(model.Pet.Facility.Address.FullLocation,
                                           model.Pet.Facility.Address.LocationCity,
                                           model.Pet.Facility.Address.LocationState);
                
                model.SetPetsByKeyword(model.SelectedPetId, model.Pet.Breed.BreedType);                   
            }

            return View(model);
        }
    }
}
