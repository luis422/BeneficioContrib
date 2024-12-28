using BeneficioContrib.Cn.Beneficio;
using BeneficioContrib.Cn.Contribuinte;
using BeneficioContrib.Cn.Database;
using BeneficioContrib.Models;
using Microsoft.AspNetCore.Mvc;

namespace BeneficioContrib.Controllers
{
    public class SimulacaoPagamentoController : Controller
    {
        private readonly CnContribuinte CnContribuinte;
        private readonly CnBeneficio CnBeneficio;

        public SimulacaoPagamentoController(Contexto contexto)
        {
            CnContribuinte = new CnContribuinte(contexto);
            CnBeneficio = new CnBeneficio(contexto);
        }

        public IActionResult Index() => RedirectToAction(nameof(SelecionarContribuinte));

        public IActionResult SelecionarContribuinte(MvSimulacaoPagamento model)
        {
            model.Contribuintes = CnContribuinte.ListarTodos()
                .Select(dd => new MvContribuinte(dd, []))
                .ToList();

            return View(model);
        }

        [HttpPost]
        public IActionResult SelecionarBeneficio(MvSimulacaoPagamento model)
        {
            var contribuinte = CnContribuinte.ObterPorId(model.IdContribuinte);
            if (contribuinte is null)
            {
                ModelState.AddModelError("msg", "Contribuinte não encontrado");
                return View(model);
            }

            model.Beneficios = contribuinte.Beneficios
                .Select(dd => new MvBeneficio(dd.Beneficio!, 0))
                .ToList();

            return View(model);
        }

        [HttpPost]
        public IActionResult Calcular(MvSimulacaoPagamento model)
        {
            if (ModelState.IsValid)
            {
                model.Total = CnBeneficio.SimularPagamento(model.IdBeneficio, model.Valor ?? 0);
            }
            else
            {
                model.Valor = 0;
            }

            return View(model);
        }
    }
}
