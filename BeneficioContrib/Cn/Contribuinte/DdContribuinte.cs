using BeneficioContrib.Cn.Contribuinte.VinculoContribuinteBeneficio;
using BeneficioContrib.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeneficioContrib.Cn.Contribuinte
{
    [Table("contribuinte")]
    public class DdContribuinte
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_codigo")]
        public int IdCodigo { get; set; }

        [Required(ErrorMessage = MensagemValidacao.Required)]
        [MaxLength(200, ErrorMessage = MensagemValidacao.MaxLength)]
        [Column("razao_social")]
        public string RazaoSocial { get; set; }

        [Required(ErrorMessage = MensagemValidacao.Required)]
        [MaxLength(14, ErrorMessage = MensagemValidacao.MaxLength)]
        [MinLength(14, ErrorMessage = MensagemValidacao.MinLength)]
        [Column("cnpj")]
        public string Cnpj { get; set; }

        [Required(ErrorMessage = MensagemValidacao.Required)]
        [Column("data_abertura", TypeName = "timestamp")]
        public DateTime DataAbertura { get; set; }

        [Required(ErrorMessage = MensagemValidacao.Required)]
        [Column("es_regime_tributacao", TypeName = "INT")]
        public EDdContribuinteRegimeTributacao RegimeTributacao { get; set; }

        public List<DdVinculoContribuinteBeneficio> Beneficios { get; set; }

        public DdContribuinte()
        {
            Cnpj = "";
            RazaoSocial = "";
            Beneficios = new List<DdVinculoContribuinteBeneficio>();
        }
    }

    public enum EDdContribuinteRegimeTributacao
    {
        [Display(Name = "MEI - Micro Empresário Individual")]
        MEI = 5,

        [Display(Name = "MEEPP - Micro Empresa ou Empresa de Pequeno Porte")]
        MEEPP = 6,

        [Display(Name = "Variável - Tributação por Faturamento Variável")]
        Variavel = 7,
    }
}
