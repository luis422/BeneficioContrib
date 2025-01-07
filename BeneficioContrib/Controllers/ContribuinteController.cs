using BeneficioContrib.Cn.Beneficio;
using BeneficioContrib.Cn.Contribuinte;
using BeneficioContrib.Cn.Database;
using BeneficioContrib.Models;
using Microsoft.AspNetCore.Mvc;

namespace BeneficioContrib.Controllers
{
    public class ContribuinteController : Controller
    {
        private readonly CnContribuinte Cn;
        private readonly CnBeneficio CnBeneficio;

        public ContribuinteController(Contexto contexto)
        {
            Cn = new CnContribuinte(contexto);
            CnBeneficio = new CnBeneficio(contexto);
        }

        public IActionResult Index()
        {
            return View(Cn.ListarTodos().Select(dd => new MvContribuinte(dd)));
        }

        public IActionResult Detalhes(int id)
        {
            var dd = Cn.ObterPorId(id);
            if (dd is null)
            {
                return RedirectToAction(nameof(Index));
            }

            var idsVinculados = dd.Beneficios.Select(b => b.EsBeneficio).ToList();
            return PartialView(new MvContribuinte(dd, CnBeneficio.ListarTodosExcetoIds(idsVinculados)));
        }


        public IActionResult Salvar(int? id = null)
        {
            DdContribuinte? dd;

            if (id is null || (dd = Cn.ObterPorId(id.Value)) is null)
            {
                var model = new MvContribuinte();
                model.AddBeneficios(CnBeneficio.ListarTodos(), false);
                return View(model);
            }

            var idsVinculados = dd.Beneficios.Select(b => b.EsBeneficio).ToList();
            return View(new MvContribuinte(dd, CnBeneficio.ListarTodosExcetoIds(idsVinculados)));
        }

        [HttpPost]
        public IActionResult Salvar(MvContribuinte model)
        {
            if (!ModelState.IsValid)
            {
                var dd = Cn.ObterPorId(model.IdCodigo)!;
                if (dd is null)
                {
                    model.Beneficios.Clear();
                    model.AddBeneficios(CnBeneficio.ListarTodos(), false);
                    return View(model);
                }
                var idsVinculados = dd.Beneficios.Select(b => b.EsBeneficio).ToList();
                return View(new MvContribuinte(dd, CnBeneficio.ListarTodosExcetoIds(idsVinculados)));
            }

            try
            {
                if (Cn.Salvar(model.ToDD()) == 0)
                {
                    ModelState.AddModelError(nameof(MvContribuinte), "Não foi possível salvar");
                    return View(model);
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(nameof(MvContribuinte), "Não foi possível salvar. " + ex.Message);
                return View(model);
            }
        }

        public IActionResult Excluir(int id)
        {
            var dd = Cn.ObterPorId(id);
            if (dd is null)
            {
                return RedirectToAction(nameof(Index));
            }

            var idsVinculados = dd.Beneficios.Select(b => b.EsBeneficio).ToList();
            return View(new MvContribuinte(dd, CnBeneficio.ListarTodosExcetoIds(idsVinculados)));
        }

        [HttpPost]
        public IActionResult Excluir(MvContribuinte model)
        {
            try
            {
                if (!Cn.Excluir(model.ToDD()))
                {
                    ModelState.AddModelError(nameof(MvContribuinte), "Não foi possível excluir");
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(nameof(MvContribuinte), "Não foi possível excluir. " + ex.Message);
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
