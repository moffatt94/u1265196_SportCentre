﻿using System;
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
    public class CustomerController : Controller
    {
        private SportContext db = new SportContext();

        // GET: Customer
        public ViewResult Index(string SortOrder, string currentPage, string SearchbarString, int? page)
        {
            ViewBag.CurrentSort = SortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(SortOrder) ? "NameDescending" : "";
            ViewBag.DateSortParm = SortOrder == "Date" ? "DateDescending" : "Date";

            if (SearchbarString != null)
            {
                page = 1;
            }
            else
            {
                SearchbarString = currentPage;
            }

            ViewBag.CurrentPage = SearchbarString;

            var customers = from c in db.Customers
                           select c;
        if (!String.IsNullOrEmpty(SearchbarString))
    {
        customers = customers.Where(c => c.LastName.Contains(SearchbarString)
                               || c.FirstName.Contains(SearchbarString));
    }
            switch (SortOrder)
            {
                case "NameDescending":
                    customers = customers.OrderByDescending(c => c.LastName);
                    break;
                case "Date":
                    customers = customers.OrderBy(c => c.EnrollmentDate);
                    break;
                case "DateDescending":
                    customers = customers.OrderByDescending(c => c.EnrollmentDate);
                    break;
                default:
                    customers = customers.OrderBy(c => c.LastName);
                    break;
            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(customers.ToPagedList(pageNumber, pageSize));
        }

        // GET: Customer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LastName,FirstName,EnrollmentDate,Email")] Customer customer)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    db.Customers.Add(customer);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return View(customer);
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customer/Edit/5
        
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var customerToUpdate = db.Customers.Find(id);
            if (TryUpdateModel(customerToUpdate, "",
                new string[] { "LastName", "FirstName", "EnrollmentDate" }))
            {
                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DataException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(customerToUpdate);
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int? id, bool? saveChangesError=false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
               ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customer/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                Customer customer = db.Customers.Find(id);
                db.Customers.Remove(customer);
                db.SaveChanges();
            }
            catch (DataException)
            {

                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
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
