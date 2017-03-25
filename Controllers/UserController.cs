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
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult VMLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult VMLogin(UserLoginVM model) //notice we’re using the ViewModel
        {
            if (ModelState.IsValid)
            {
                var db = new UserEntities();

                var password = model.Password;
                password = Helpers.SHA1.Encode(password);

                var v = db.Users.Where(u => u.Username.Equals(model.Username) && u.Password.Equals(password)).FirstOrDefault();

                if (v != null)
                {
                    ViewData["Message"] = "Login Successful";
                    Session["loggedIn"] = true;
                    Session["user"] = v.Username;
                    Session["uid"] = v.Id;
                    Session["admin"] = v.Admin;
                    
                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    ViewData["Message"] = "Login Unsuccessful";
                }
            }
            return View(model);
        }



        public ActionResult DisplayUsers()
        {
            var entities = new UserEntities();
            return View(entities.Users.ToList());

        }

        [HttpGet]
        public ActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateUser(UserRegistrationVM model)
        {

            var password  = model.Password;
            password = Helpers.SHA1.Encode(password);
            if (ModelState.IsValid)
            {
                var db = new UserEntities();
                db.Users.Add(new User
                {
                    Username = model.Username,
                    EmailAddress = model.EmailAddress,
                    Password = password
                });
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(model);

        }

        [HttpPost]
        public JsonResult doesUserNameExist(string Username)
        {
            var db = new UserEntities(); //Where Entities is replaced by the name of YOUR entities
            return Json(!db.Users.Any(u => u.Username == Username), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult doesEmailExist(string EmailAddress)
        {
            var db = new UserEntities(); //Where Entities is replaced by the name of YOUR entities
            return Json(!db.Users.Any(u => u.EmailAddress == EmailAddress), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Update()
        {
            if (Session["loggedIn"] != null)
            {
                var db = new UserEntities(); //where Entities matches the name of YOUR entities
                var userToUpdate = db.Users.Find(Session["uid"]);
                return View(userToUpdate);
            }
            else
            {
                return Redirect("../User/VMLogin"); //making sure you substitute “Login” for whatever your login method is actually called if different
            } 
        }

        [HttpPost]
        public ActionResult Update(User model)
        {
            int? id = (int)Session["uid"];
            var db = new UserEntities(); //where Entities matches the name of YOUR entities
            var userToUpdate = db.Users.Find(id);

            var password = model.Password;
            password = Helpers.SHA1.Encode(password);

            if (ModelState.IsValid)
            {
                if (userToUpdate != null)
                {
                    userToUpdate.Id = id ?? default(int);
                    userToUpdate.Username = model.Username;
                    userToUpdate.Password = password;
                    userToUpdate.EmailAddress = model.EmailAddress;
                    db.SaveChanges();
                }
            }
            ViewData["Message"] = "Record updated";
            return View(model);
        }

        [HttpGet]
        public ActionResult Cars()
        {
            if (Session["loggedIn"] != null)
            {
                var db = new UserEntities();
                var user = (int)Session["uid"];

                //Cheating here... Will "fix" later

                return View(db.Bookings.Where(b => b.UserID.Equals(user)).ToList());
            }
            else
            {
                return Redirect("../User/VMLogin"); //making sure you substitute “Login” for whatever your login method is actually called if different
            }

        }
    }
}