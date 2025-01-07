using BeneficioContrib.Cn.Database;
using Microsoft.AspNetCore.Mvc;

namespace BeneficioContrib.Controllers
{
    public class HomeController : Controller
    {
        private Contexto Db;

        public HomeController(Contexto contexto)
        {
            Db = contexto;
        }

        public IActionResult Index()
        {
            ViewData["banco"] = Db.Database.CanConnect();
            return View();
        }
    }
}
