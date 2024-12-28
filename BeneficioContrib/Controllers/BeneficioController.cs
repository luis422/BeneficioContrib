using BeneficioContrib.Models;
using Microsoft.AspNetCore.Mvc;
using BeneficioContrib.Cn.Beneficio;
using BeneficioContrib.Cn.Database;

namespace BeneficioContrib.Controllers
{
    public class BeneficioController : Controller
    {
        private readonly CnBeneficio Cn;

        public BeneficioController(Contexto contexto)
        {
            Cn = new CnBeneficio(contexto);
        }

        public IActionResult Index()
        {
            return View(Cn.ListarTodosComQtdContrib().Select(dd => new MvBeneficio(dd.Key, dd.Value)));
        }

        public IActionResult Detalhes(int id)
        {
            var dd = Cn.ObterPorIdComQtdContrib(id);
            if (dd is not null)
            {
                return PartialView(new MvBeneficio(dd.Value.Beneficio, dd.Value.Count));
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Salvar(int? id = null)
        {
            if (id is null)
            {
                return View(new MvBeneficio());
            }

            var dd = Cn.ObterPorIdComQtdContrib(id.Value);
            if (dd is null)
            {
                return View(new MvBeneficio());
            }
            
            return View(new MvBeneficio(dd.Value.Beneficio,dd.Value.Count));
        }

        [HttpPost]
        public IActionResult Salvar(MvBeneficio model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                if (Cn.Salvar(model.ToDD()) == 0)
                {
                    ModelState.AddModelError(nameof(MvBeneficio), "Não foi possível salvar o benefício");
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(nameof(MvBeneficio), "Não foi possível salvar o benefício. " + ex.Message);
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Excluir(int id)
        {
            var dd = Cn.ObterPorIdComQtdContrib(id);
            if (dd is null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(new MvBeneficio(dd.Value.Beneficio, dd.Value.Count));
        }

        [HttpPost]
        public IActionResult Excluir(MvBeneficio model)
        {
            try
            {
                ModelState.Clear();
                var dd = Cn.ObterPorIdComQtdContrib(model.IdCodigo);
                if (dd is null)
                {
                    ModelState.AddModelError(nameof(MvBeneficio), $"O benefício \"{model.IdCodigo}\" não existe");
                    return View(model);
                }

                if (!Cn.Excluir(dd.Value.Beneficio))
                {
                    ModelState.AddModelError(nameof(MvBeneficio), "Não foi possível excluir");
                    return View(new MvBeneficio(dd.Value.Beneficio, dd.Value.Count));
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError(nameof(MvBeneficio), "Houve um erro ao tentar excluir. Tente novamente mais tarde.");
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
