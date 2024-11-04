using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GameLibraryApp.Models;
using ProjetoProvaA1.Models;

namespace ProjetoProvaA1.Controllers
{
    public class UserGameLibraryController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: UserGameLibrary
        public ActionResult Index()
        {
            var userGameLibraries = db.UserGameLibraries.Include(u => u.Game).Include(u => u.User);
            return View(userGameLibraries.ToList());
        }

        // GET: UserGameLibrary/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserGameLibrary userGameLibrary = db.UserGameLibraries.Find(id);
            if (userGameLibrary == null)
            {
                return HttpNotFound();
            }
            return View(userGameLibrary);
        }

        // GET: UserGameLibrary/Create
        public ActionResult Create()
        {
            ViewBag.GameId = new SelectList(db.Games, "Id", "Title");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name");
            return View();
        }

        // POST: UserGameLibrary/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,GameId,AddedDate")] UserGameLibrary userGameLibrary)
        {
            if (ModelState.IsValid)
            {
                db.UserGameLibraries.Add(userGameLibrary);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GameId = new SelectList(db.Games, "Id", "Title", userGameLibrary.GameId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", userGameLibrary.UserId);
            return View(userGameLibrary);
        }

        // GET: UserGameLibrary/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserGameLibrary userGameLibrary = db.UserGameLibraries.Find(id);
            if (userGameLibrary == null)
            {
                return HttpNotFound();
            }
            ViewBag.GameId = new SelectList(db.Games, "Id", "Title", userGameLibrary.GameId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", userGameLibrary.UserId);
            return View(userGameLibrary);
        }

        // POST: UserGameLibrary/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,GameId,AddedDate")] UserGameLibrary userGameLibrary)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userGameLibrary).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GameId = new SelectList(db.Games, "Id", "Title", userGameLibrary.GameId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", userGameLibrary.UserId);
            return View(userGameLibrary);
        }

        // GET: UserGameLibrary/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserGameLibrary userGameLibrary = db.UserGameLibraries.Find(id);
            if (userGameLibrary == null)
            {
                return HttpNotFound();
            }
            return View(userGameLibrary);
        }

        // POST: UserGameLibrary/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserGameLibrary userGameLibrary = db.UserGameLibraries.Find(id);
            db.UserGameLibraries.Remove(userGameLibrary);
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
