using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DDWAssignment.Models;
using DDWAssignment.ViewModels;
using System.Net;

namespace DDWAssignment.Controllers
{
    public class CarController : Controller
    {
        // GET: Car
        public ActionResult Index()
        {
            var entities = new UserEntities();
            return View(entities.Cars.ToList());
        }

        public ActionResult Rent(int id)
        {
            return View(id);
        }
    }
}