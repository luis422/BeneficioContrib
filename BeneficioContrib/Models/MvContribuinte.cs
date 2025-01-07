using BeneficioContrib.Cn.Beneficio;
using BeneficioContrib.Cn.Contribuinte;
using BeneficioContrib.Cn.Contribuinte.VinculoContribuinteBeneficio;
using BeneficioContrib.Helpers;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BeneficioContrib.Models
{
    [DisplayName("Contribuinte")]
    public class MvContribuinte
    {
        [Key]
        public int IdCodigo { get; set; }

        [Display(Name = "Razão Social")]
        [Required(ErrorMessage = MensagemValidacao.Required)]
        [MaxLength(200, ErrorMessage = MensagemValidacao.MaxLength)]
        public string RazaoSocial { get; set; }

        [Display(Name = "CNPJ")]
        [Required(ErrorMessage = MensagemValidacao.Required)]
        [MaxLength(14 + 4, ErrorMessage = MensagemValidacao.MaxLength)]
        [MinLength(14 + 4, ErrorMessage = MensagemValidacao.MinLength)]
        public string Cnpj { get; set; }

        [Display(Name = "Data de Abertura")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = MensagemValidacao.Required)]
        public DateTime DataAbertura { get; set; }

        [Display(Name = "Regime de Tributação")]
        [Required(ErrorMessage = MensagemValidacao.Required)]
        public EDdContribuinteRegimeTributacao RegimeTributacao { get; set; }

        [Display(Name = "Benefícios")]
        public List<KeyValuePair<MvBeneficio, bool>> Beneficios { get; set; }

        public MvContribuinte()
        {
            Cnpj = "";
            RazaoSocial = "";
            Beneficios = [];
            DataAbertura = DateTime.Now;
        }

        public MvContribuinte(DdContribuinte dd) : this(dd, [])
        {
        }

        public MvContribuinte(DdContribuinte dd, List<DdBeneficio> beneficiosAdicionais)
        {
            IdCodigo = dd.IdCodigo;
            RazaoSocial = dd.RazaoSocial;
            Cnpj = dd.Cnpj;
            DataAbertura = dd.DataAbertura;
            RegimeTributacao = dd.RegimeTributacao;
            Beneficios = [];

            AddBeneficios(dd.Beneficios.Select(v => v.Beneficio!).ToList(), true);

            beneficiosAdicionais = beneficiosAdicionais.Where(b =>
                !dd.Beneficios.Select(v => v.EsBeneficio).Contains(b.IdCodigo))
            .ToList();

            AddBeneficios(beneficiosAdicionais, false);

            Beneficios = Beneficios.OrderBy(b => b.Key.Nome).ToList();
        }

        public void AddBeneficios(List<DdBeneficio> beneficios, bool marcado)
        {
            foreach (var b in beneficios)
            {
                Beneficios.Add(new KeyValuePair<MvBeneficio, bool>(new MvBeneficio(b, 0), marcado));
            }
        }

        public DdContribuinte ToDD()
        {
            return new DdContribuinte()
            {
                IdCodigo = IdCodigo,
                RazaoSocial = RazaoSocial,
                Cnpj = Cnpj.Replace(".", "").Replace("-", "").Replace("/", ""),
                DataAbertura = DataAbertura,
                RegimeTributacao = RegimeTributacao,
                Beneficios = Beneficios.Where(b => b.Value)
                    .Select(b => new DdVinculoContribuinteBeneficio()
                    {
                        EsBeneficio = b.Key.IdCodigo,
                        EsContribuinte = IdCodigo
                    }).ToList()
            };
        }
    }
}
