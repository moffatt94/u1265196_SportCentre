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
using HuddersfieldSportCentre.ViewModels;
using System.Data.Entity.Infrastructure;

namespace HuddersfieldSportCentre.Controllers
{
    public class TrainerController : Controller
    {
        private SportContext db = new SportContext();

        // GET: Trainer
        public ActionResult Index(int? id, int? courseID)
        {
            var viewModel = new TrainerIndexData();
            viewModel.Trainers = db.Trainers
                .Include(i => i.Courses.Select(c => c.Department))
                .OrderBy(i => i.LastName);

            if (id != null)
            {
                ViewBag.TrainerID = id.Value;
                viewModel.Courses = viewModel.Trainers.Where(
                    i => i.ID == id.Value).Single().Courses;
            }

            if (courseID != null)
            {
                ViewBag.CourseID = courseID.Value;
                viewModel.Enrollments = viewModel.Courses.Where(
                    x => x.CourseID == courseID).Single().Enrollments;
            }
            return View(viewModel);
        }

        // GET: Trainer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trainer trainer = db.Trainers.Find(id);
            if (trainer == null)
            {
                return HttpNotFound();
            }
            return View(trainer);
        }

        // GET: Trainer/Create
        public ActionResult Create()
        {
            var trainer = new Trainer();
            trainer.Courses = new List<Course>();
            PopulateAssignCourseData(trainer);
            return View();
        }

        // POST: Trainer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,LastName,FirstName,StartedDate")] Trainer trainer, string[] selectedCourses)
        {
            if (selectedCourses != null)
            {
                trainer.Courses = new List<Course>();
                foreach (var course in selectedCourses)
                {
                    var courseToAdd = db.Courses.Find(int.Parse(course));
                    trainer.Courses.Add(courseToAdd);
                }
            }
            if (ModelState.IsValid)
            {
                db.Trainers.Add(trainer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            PopulateAssignCourseData(trainer);
            return View(trainer);
        }

        // GET: Trainer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trainer trainer = db.Trainers
            .Include(i => i.Courses)
            .Where(i => i.ID == id)
            .Single();
            PopulateAssignCourseData(trainer);
            if (trainer == null)
            {
                return HttpNotFound();
            }
          
            return View(trainer);
        }

        private void PopulateAssignCourseData(Trainer trainer)
        {
            var allCourses = db.Courses;
            var trainerCourses = new HashSet<int>(trainer.Courses.Select(c => c.CourseID));
            var viewModel = new List<AssignCourseData>();
            foreach (var course in allCourses)
            {
                viewModel.Add(new AssignCourseData
                {
                    CourseID = course.CourseID,
                    Title = course.Title,
                    Assigned = trainerCourses.Contains(course.CourseID)
                });
            }
            ViewBag.Courses = viewModel;
        }

        // POST: Trainer/Edit/5
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id, string[] selectedCourses)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var trainerToUpdate = db.Trainers
               .Include(i => i.Courses)
               .Where(i => i.ID == id)
               .Single();

            if (TryUpdateModel(trainerToUpdate, "",
               new string[] { "LastName", "FirstName", "HireDate" }))
            {
                try
                {

                    UpdateTrainerCourses(selectedCourses, trainerToUpdate);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException)
                {
                    
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            PopulateAssignCourseData(trainerToUpdate);
            return View(trainerToUpdate);
        }
        private void UpdateTrainerCourses(string[] selectedCourses, Trainer trainerToUpdate)
        {
            if (selectedCourses == null)
            {
                trainerToUpdate.Courses = new List<Course>();
                return;
            }

            var selectedCoursesHS = new HashSet<string>(selectedCourses);
            var trainerCourses = new HashSet<int>
                (trainerToUpdate.Courses.Select(c => c.CourseID));
            foreach (var course in db.Courses)
            {
                if (selectedCoursesHS.Contains(course.CourseID.ToString()))
                {
                    if (!trainerCourses.Contains(course.CourseID))
                    {
                        trainerToUpdate.Courses.Add(course);
                    }
                }
                else
                {
                    if (trainerCourses.Contains(course.CourseID))
                    {
                        trainerToUpdate.Courses.Remove(course);
                    }
                }
            }
        }



        // GET: Trainer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trainer trainer = db.Trainers.Find(id);
            if (trainer == null)
            {
                return HttpNotFound();
            }
            return View(trainer);
        }

        // POST: Trainer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Trainer trainer = db.Trainers
           .Where(i => i.ID == id)
           .Single();

            db.Trainers.Remove(trainer);
            var department = db.Departments
                    .Where(d => d.TrainerID == id)
                    .SingleOrDefault();
            if (department != null)
            {
                department.TrainerID = null;
            }



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
