using BeneficioContrib.Cn.Beneficio;
using BeneficioContrib.Helpers;
using System.ComponentModel.DataAnnotations;

namespace BeneficioContrib.Models
{
    [Display(Name = "Benefício")]
    public class MvBeneficio
    {
        [Key]
        public int IdCodigo { get; set; }

        [Display(Name = "Nome Benefício")]
        [Required(ErrorMessage = MensagemValidacao.Required)]
        [MaxLength(200, ErrorMessage = MensagemValidacao.MaxLength)]
        public string Nome { get; set; }

        [Display(Name = "Percentual de Desconto")]
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = MensagemValidacao.Required)]
        [Range(0.01, 100, ErrorMessage = MensagemValidacao.Range)]
        public decimal? PorcentagemDesconto { get; set; }

        [Display(Name = "Quantidade de Contribuintes")]
        public int QtdContribs { get; set; }

        public MvBeneficio()
        {
            Nome = "";
        }

        public MvBeneficio(DdBeneficio beneficio, int qtdContribs)
        {
            IdCodigo = beneficio.IdCodigo;
            Nome = beneficio.Nome;
            PorcentagemDesconto = Convert.ToDecimal(beneficio.PorcentagemDesconto);
            QtdContribs = qtdContribs;
        }

        public DdBeneficio ToDD()
        {
            return new DdBeneficio()
            {
                IdCodigo = IdCodigo,
                Nome = Nome,
                PorcentagemDesconto = Convert.ToDecimal(PorcentagemDesconto)
            };
        }
    }
}
