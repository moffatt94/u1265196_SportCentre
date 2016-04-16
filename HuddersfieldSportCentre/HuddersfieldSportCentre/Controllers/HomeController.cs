using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HuddersfieldSportCentre.DataAccessLayer;
using HuddersfieldSportCentre.ViewModels;

namespace HuddersfieldSportCentre.Controllers
{
    public class HomeController : Controller
    {
        private SportContext db = new SportContext();

        public ActionResult Index()
        {

            return View();
        }

        public ActionResult About()
        {
            IQueryable<EnrollmentDateGroup> data = from customer in db.Customers
                                                   group customer by customer.EnrollmentDate into dateGroup
                                                   select new EnrollmentDateGroup()
                                                   {
                                                       EnrollmentDate = dateGroup.Key,
                                                       CustomerCount = dateGroup.Count()
                                                   };

            return View(data.ToList());
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}