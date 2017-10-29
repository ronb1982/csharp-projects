using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using DYRMock.Models;

namespace DYRMock.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            PetViewModel viewModel = new PetViewModel();
            return View(viewModel);
        }
    }
}
