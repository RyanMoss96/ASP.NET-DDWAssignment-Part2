using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DDWAssignment.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            if (Session["loggedIn"] != null)
            {
                if ((bool)Session["admin"] == true)
                {
                    return View();
                }
                
            }
            
            
                return Redirect("/Home/Index");
            

        }
    }
}