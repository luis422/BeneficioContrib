using BeneficioContrib.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BeneficioContrib.Models
{
    [Display(Name = "Simular Pagamento")]
    public class MvSimulacaoPagamento
    {
        [Display(Name = "CNPJ")]
        [Required(ErrorMessage = MensagemValidacao.Required)]
        [MaxLength(14 + 4, ErrorMessage = MensagemValidacao.MaxLength)]
        [MinLength(14 + 4, ErrorMessage = MensagemValidacao.MinLength)]
        public string CnpjContribuinte { get; set; } = "";

        [Display(Name = "Selecione um Benefício")]
        public int IdBeneficio { get; set; }

        [Display(Name = "Digite um valor numérico")]
        [Required(ErrorMessage = MensagemValidacao.Required)]
        public decimal? Valor { get; set; } = 0;

        [Display(Name = "Total a Pagar")]
        public decimal Total { get; set; }

        [Display(Name = "Porcentagem Desconto")]
        public decimal PorcentagemDesconto { get; set; }

        public MvContribuinte Contribuinte;
        public List<MvBeneficio> Beneficios;

        public MvSimulacaoPagamento()
        {
            Beneficios = [];
            Contribuinte = new MvContribuinte();
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
