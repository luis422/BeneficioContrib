using BeneficioContrib.Cn.Beneficio;
using BeneficioContrib.Cn.Contribuinte;
using BeneficioContrib.Cn.Database;
using BeneficioContrib.Helpers;
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

        public IActionResult Index()
        {
            ModelState.Clear(); // Limpa o erro de validação de cnpj vazio
            return View(nameof(SelecionarContribuinte), new MvSimulacaoPagamento());
        }

        [HttpPost]
        public IActionResult SelecionarContribuinte(MvSimulacaoPagamento model)
        {
            return View(model);
        }

        [HttpPost]
        public IActionResult SelecionarBeneficio(MvSimulacaoPagamento model)
        {
            var contribuinte = CnContribuinte.ObterPorCnpj(Formatacao.RemoverMascara(model.CnpjContribuinte)!);
            if (contribuinte is null)
            {
                ModelState.AddModelError(nameof(MvSimulacaoPagamento.CnpjContribuinte), "CNPJ não cadastrado!");
                return View(nameof(SelecionarContribuinte), model);
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
                model.Total = CnBeneficio.SimularPagamento(model.IdBeneficio, model.Valor ?? 0, out var beneficio);
                model.PorcentagemDesconto = beneficio?.PorcentagemDesconto ?? 0;
            }
            else
            {
                model.Valor = 0;
                model.PorcentagemDesconto = 0;
            }

            return View(model);
        }
    }
}
