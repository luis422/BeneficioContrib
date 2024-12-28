using BeneficioContrib.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.ComponentModel.DataAnnotations;

namespace BeneficioContrib.Models
{
    [Display(Name = "Simular Pagamento")]
    public class MvSimulacaoPagamento
    {
        [Display(Name = "Selecione um Contribuinte")]
        public int IdContribuinte { get; set; }

        [Display(Name = "Selecione um Benefício")]
        public int IdBeneficio { get; set; }

        [Display(Name = "Digite um valor numérico")]
        [Required(ErrorMessage = MensagemValidacao.Required)]
        public decimal? Valor { get; set; } = 0;

        [Display(Name = "Total a Pagar")]
        public decimal Total { get; set; }

        public List<MvContribuinte> Contribuintes;
        public List<MvBeneficio> Beneficios;
        public MvSimulacaoPagamento()
        {
            Contribuintes = [];
            Beneficios = [];
        }

        public List<MvContribuinte> ListarTodosContribuintes() => Contribuintes;

        public List<SelectListItem> SelectListItemFromContribuintes(bool addSelecione = false)
        {
            var listItem = Contribuintes.Select(c => new SelectListItem(
                Formatacao.CpfCnpj(c.Cnpj) + " " + c.RazaoSocial,
                c.IdCodigo.ToString(),
                c.IdCodigo == IdContribuinte
            )).ToList();

            if (addSelecione)
            {
                listItem = listItem.Prepend(new SelectListItem("Nenhum Contribuinte", 0.ToString())).ToList();
            }

            return listItem;
        }

        public List<SelectListItem> SelectListItemFromBeneficios(bool addSelecione = false)
        {
            var listItem = Beneficios.Select(b => new SelectListItem(
                b.Nome + " " + b.PorcentagemDesconto?.ToString("F2") + "%",
                b.IdCodigo.ToString(),
                b.IdCodigo == IdBeneficio
            )).ToList();

            if (addSelecione)
            {
                listItem = listItem.Prepend(new SelectListItem("Nenhum Benefício", 0.ToString())).ToList();
            }

            return listItem;
        }
    }
}
