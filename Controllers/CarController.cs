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

        [HttpGet]
        public ActionResult Rent(int? id)
        {
            if(Session["user"] == null)
            {
                return RedirectToAction("VMLogin", "User");
            } else
            {
                if (id == null)
                {
                    return RedirectToAction("Index", "Car");
                }
                else
                {
                    var db = new UserEntities();
                    Car car = db.Cars.Find(id);
                    //Cheating here... Will "fix" later
                    Session["car"] = id;
                    return View(car);
                }
            }
            
            
        }

        [HttpPost]
        public ActionResult Rent(Car model)
        {
            var db = new UserEntities();
            //Cheating here... Will "fix" later
            Car car = db.Cars.Find(Session["car"]);
            var id = (int)Session["uid"];


            db.Bookings.Add(new Booking
            {
               CarID = car.Id,
               UserID = id
            });
            db.SaveChanges();
            return RedirectToAction("Booked", "Car");
        }

        public ActionResult Booked()
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("Index", "Car");
            }
            else
            {
                var db = new UserEntities();
                Car car = db.Cars.Find((int)Session["car"]);
                //Cheating here... Will "fix" later
                Session["car"] = null;
                return View(car);
            }
           
        }
    }
}