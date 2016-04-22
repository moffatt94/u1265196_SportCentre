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
using System.Data.Entity.Infrastructure;
using PagedList;

namespace HuddersfieldSportCentre.Controllers
{
    public class CourseController : Controller
    {
        private SportContext db = new SportContext();

        // GET: Course
        public ActionResult Index(string SortOrder, string currentPage, string SearchbarString, int? page)
        
             {
            ViewBag.CurrentSort = SortOrder;
            ViewBag.CourseNoSortParm = SortOrder == "ID" ? "CourseNoDescending" : "";
            ViewBag.NameSortParm = String.IsNullOrEmpty(SortOrder) ? "CourseNameDescending" : "CourseName";
            ViewBag.DepartmentSortParm = String.IsNullOrEmpty(SortOrder) ? "DepartmentDescending" : "Department";

            if (SearchbarString != null)
            {
                page = 1;
            }
            else
            {
                SearchbarString = currentPage;
            }

            ViewBag.CurrentPage = SearchbarString;

            var courses = from c in db.Courses.Include(c => c.Department)
                           select c;
        if (!String.IsNullOrEmpty(SearchbarString))
    {
        courses = courses.Where(c => c.CourseID.ToString().Contains(SearchbarString)
                               || c.Title.Contains(SearchbarString)
                               || c.Department.Name.Contains(SearchbarString));
           
            
    }
            switch (SortOrder)
            {
                case "CourseNoDescending":
                    courses = courses.OrderByDescending(c => c.CourseID);
                    break;
                case "CourseName":
                    courses = courses.OrderBy(c => c.Title);
                    break;
                case "CourseNameDescending":
                    courses = courses.OrderByDescending(c => c.Title);
                    break;
                case "Department":
                    courses = courses.OrderBy(c => c.Department.Name);
                    break;
                case "DepartmentDescending":
                    courses = courses.OrderByDescending(c => c.Department.Name);
                    break;
                default:
                    courses = courses.OrderBy(c => c.CourseID);
                    break;
            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(courses.ToPagedList(pageNumber, pageSize));
        }


        // GET: Course/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // GET: Course/Create
        public ActionResult Create()
        {
            PopulateDepartmentsDropDownList();
            return View();
        }

        // POST: Course/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CourseID,Title,DepartmentID")] Course course)
        {
            try
            {
                   if (ModelState.IsValid)
                {
                    db.Courses.Add(course);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }

            catch (RetryLimitExceededException)
            {
                
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }

            PopulateDepartmentsDropDownList(course.DepartmentID);

            return View(course);
        }

        // GET: Course/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            PopulateDepartmentsDropDownList(course.DepartmentID);
            return View(course);
        }

        // POST: Course/Edit/5
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var courseToUpdate = db.Courses.Find(id);
            if (TryUpdateModel(courseToUpdate, "",
               new string[] { "Title", "DepartmentID" }))
            {
                try
                {
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            PopulateDepartmentsDropDownList(courseToUpdate.DepartmentID);
            return View(courseToUpdate);
        }

        private void PopulateDepartmentsDropDownList(object selectedDepartment = null)
        {
            var departmentsQuery = from d in db.Departments
                                   orderby d.Name
                                   select d;
            ViewBag.DepartmentID = new SelectList(departmentsQuery, "DepartmentID", "Name", selectedDepartment);
        } 

        // GET: Course/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Course/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = db.Courses.Find(id);
            db.Courses.Remove(course);
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
