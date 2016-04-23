using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HuddersfieldSportCentre.Controllers;
using HuddersfieldSportCentre.DataAccessLayer;
using HuddersfieldSportCentre.Models;


namespace HuddersfieldSportCentre.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Login()
        {
            return View();
        }
        //Login Admin
        [HttpPost]
        public ActionResult Login(Admin user)
        {
            using (SportContext db = new SportContext())
            {
                var usr = db.Admin.Where(u => u.Username == user.Username && u.Password == user.Password).FirstOrDefault();
                if (usr != null)
                {
                    Session["AdminID"] = usr.AdminID.ToString();
                    Session["Username"] = usr.Username.ToString();
                    return RedirectToAction("LoggedIn");
                }
                else
                {
                    ModelState.AddModelError("", "Username or Password is incorrect.");
                }
            }
            return View();
        }

        public ActionResult LoggedIn()
        {
            if (Session["AdminID"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("LogIn");
            }
        }
    }
}