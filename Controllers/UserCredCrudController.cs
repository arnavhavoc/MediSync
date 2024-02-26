using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MediSync.Models;

namespace MediSync.Controllers
{
    public class UserCredCrudController : Controller
    {
        private MediSyncEntities db = new MediSyncEntities();

        // GET: UserCredCrud
        public ActionResult Index()
        {
            return View(db.UserCreds.ToList());
        }

        // GET: UserCredCrud/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserCred userCred = db.UserCreds.Find(id);
            if (userCred == null)
            {
                return HttpNotFound();
            }
            return View(userCred);
        }

        // GET: UserCredCrud/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserCredCrud/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "userID,userName,userPassword,userRole,userReferenceToID")] UserCred userCred)
        {
            if (ModelState.IsValid)
            {
                db.UserCreds.Add(userCred);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userCred);
        }

        // GET: UserCredCrud/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserCred userCred = db.UserCreds.Find(id);
            if (userCred == null)
            {
                return HttpNotFound();
            }
            return View(userCred);
        }

        // POST: UserCredCrud/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "userID,userName,userPassword,userRole,userReferenceToID")] UserCred userCred)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userCred).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userCred);
        }

        // GET: UserCredCrud/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserCred userCred = db.UserCreds.Find(id);
            if (userCred == null)
            {
                return HttpNotFound();
            }
            return View(userCred);
        }

        // POST: UserCredCrud/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserCred userCred = db.UserCreds.Find(id);
            db.UserCreds.Remove(userCred);
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
