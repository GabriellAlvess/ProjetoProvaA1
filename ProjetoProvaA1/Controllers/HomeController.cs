using GameLibraryApp.Models;
using System.Linq;
using System.Web.Mvc;

namespace ProjetoProvaA1.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // Simula a definição do tipo de usuário
        public ActionResult SetUserType(string role)
        {
            Session["UserRole"] = role;
            return RedirectToAction("Index");
        }

        // Redireciona o usuário para a página inicial adequada com base no tipo de usuário
        public ActionResult Index()
        {
            var role = Session["UserRole"] as string;

            if (role == "Admin")
            {
                return RedirectToAction("AdminHome");
            }
            else
            {
                return RedirectToAction("UserHome");
            }
        }

        // Página inicial para Admin
        public ActionResult AdminHome()
        {
            return View();
        }

        // Página inicial para Usuário Comum
        public ActionResult UserHome()
        {
            var games = db.Games.ToList();
            return View(games);
        }
    }
}
