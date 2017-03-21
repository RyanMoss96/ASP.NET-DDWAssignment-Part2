using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DDWAssignment.Models;
using DDWAssignment.ViewModels;

namespace DDWAssignment.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var entities = new UserEntities();
            return View(entities.Cars.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        public ActionResult UserArea()
        {
            if (Session["loggedIn"] != null)
            {
                ViewData["Message"] = "Welcome " + Session["user"];
                return View();
            }
            else
            {
                return Redirect("../User/VMLogin"); //making sure you substitute “Login” for whatever your login method is actually called if different
            }         
        }

    }
}