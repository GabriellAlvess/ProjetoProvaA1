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

namespace ProjetoProvaA1
{
    
    public class GameController : Controller
    {
        private readonly AppDbContext db = new AppDbContext();

        // GET: Game
        public ActionResult Index()
        {
            var games = db.Games.Include(g => g.Developer).Include(g => g.Genres).ToList();
            return View(games);
        }

        // GET: Game/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Include(g => g.Developer).Include(g => g.Genres).Include(g => g.Reviews.Select(r => r.User)).FirstOrDefault(g => g.Id == id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // GET: Game/Create
        public ActionResult Create()
        {
            ViewBag.DeveloperId = new SelectList(db.Developers, "Id", "Name");
            ViewBag.Genres = new MultiSelectList(db.Genres, "Id", "Name");
            return View();
        }

        // POST: Game/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Game game)
        {
            if (ModelState.IsValid)
            {
                db.Games.Add(game);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DeveloperId = new SelectList(db.Developers, "Id", "Name", game.DeveloperId);
            ViewBag.Genres = new MultiSelectList(db.Genres, "Id", "Name", game.SelectedGenreIds);
            return View(game);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Include(g => g.Genres).FirstOrDefault(g => g.Id == id);
            if (game == null)
            {
                return HttpNotFound();
            }
            ViewBag.DeveloperId = new SelectList(db.Developers, "Id", "Name", game.DeveloperId);
            ViewBag.Genres = new MultiSelectList(db.Genres, "Id", "Name", game.Genres.Select(g => g.Id));
            return View(game);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Game game)
        {
            if (ModelState.IsValid)
            {
                var existingGame = db.Games.Include(g => g.Genres).FirstOrDefault(g => g.Id == game.Id);
                if (existingGame != null)
                {
                    existingGame.Title = game.Title;
                    existingGame.Description = game.Description;
                    existingGame.ReleaseDate = game.ReleaseDate;
                    existingGame.DeveloperId = game.DeveloperId;
                    existingGame.Genres.Clear();
                    foreach (var genreId in game.SelectedGenreIds)
                    {
                        var genre = db.Genres.Find(genreId);
                        if (genre != null)
                        {
                            existingGame.Genres.Add(genre);
                        }
                    }
                    db.Entry(existingGame).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.DeveloperId = new SelectList(db.Developers, "Id", "Name", game.DeveloperId);
            ViewBag.Genres = new MultiSelectList(db.Genres, "Id", "Name", game.SelectedGenreIds);
            return View(game);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Include(g => g.Developer).Include(g => g.Genres).FirstOrDefault(g => g.Id == id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Game game = db.Games.Include(g => g.Genres).FirstOrDefault(g => g.Id == id);
            if (game != null)
            {
                game.Genres.Clear();
                db.Games.Remove(game);
                db.SaveChanges();
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
