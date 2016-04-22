using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HuddersfieldSportCentre.DataAccessLayer;
using HuddersfieldSportCentre.Models;
using PagedList;

namespace HuddersfieldSportCentre.Controllers
{
    public class EnrollmentController : Controller
    {
        private SportContext db = new SportContext();

        // GET: Enrollment
        public ActionResult Index(string SortOrder, string currentPage, string SearchbarString, int? page)
        {
            ViewBag.CurrentSort = SortOrder;
            ViewBag.TitleSortParm = String.IsNullOrEmpty(SortOrder) ? "TitleDescending" : "";
            ViewBag.NameSortParm = String.IsNullOrEmpty(SortOrder) ? "NameDescending" : "Name";

            if (SearchbarString != null)
            {
                page = 1;
            }
            else
            {
                SearchbarString = currentPage;
            }

            ViewBag.CurrentPage = SearchbarString;

            var enrollments = from e in db.Enrollments.Include(e => e.Course).Include(e => e.Customer)

                               select e;

        if (!String.IsNullOrEmpty(SearchbarString))
    {
        enrollments = enrollments.Where(e => e.Course.Title.Contains(SearchbarString)
                               || e.Customer.LastName.Contains(SearchbarString));
    }
        switch (SortOrder)
        {

            case "TitleDescending":
                enrollments = enrollments.OrderByDescending(e => e.Course.Title);
                break;
            case "Name":
                enrollments = enrollments.OrderBy(e => e.Customer.LastName);
                break;
            case "NameDescending":
                enrollments = enrollments.OrderByDescending(e => e.Customer.LastName);
                break;
            default:
                enrollments = enrollments.OrderBy(e => e.Course.Title);
                break;
        }
        int pageSize = 5;
        int pageNumber = (page ?? 1);
        return View(enrollments.ToPagedList(pageNumber, pageSize));
        }

        // GET: Enrollment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = db.Enrollments.Find(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            return View(enrollment);
        }

        // GET: Enrollment/Create
        public ActionResult Create()
        {
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Title");
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "Details");
            return View();
        }

        // POST: Enrollment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EnrollmentID,CourseID,CustomerID,")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                db.Enrollments.Add(enrollment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Title", enrollment.CourseID);
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "Details", enrollment.CustomerID);
            return View(enrollment);
        }

        // GET: Enrollment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = db.Enrollments.Find(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Title", enrollment.CourseID);
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "LastName", enrollment.CustomerID);
            return View(enrollment);
        }

        // POST: Enrollment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EnrollmentID,CourseID,CustomerID")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(enrollment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Title", enrollment.CourseID);
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "LastName", enrollment.CustomerID);
            return View(enrollment);
        }

        // GET: Enrollment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = db.Enrollments.Find(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            return View(enrollment);
        }

        // POST: Enrollment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Enrollment enrollment = db.Enrollments.Find(id);
            db.Enrollments.Remove(enrollment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
