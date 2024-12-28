using BeneficioContrib.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace BeneficioContrib.Cn.Beneficio
{
    [Table("beneficio")]
    public class DdBeneficio
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_codigo")]
        public int IdCodigo { get; set; }

        [Required(ErrorMessage = MensagemValidacao.Required)]
        [MaxLength(200, ErrorMessage = MensagemValidacao.MaxLength)]
        [Column("nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = MensagemValidacao.Required)]
        [Range(0.01, 100, ErrorMessage = MensagemValidacao.Range)]
        [Column("porcentagem_desconto")]
        public decimal PorcentagemDesconto { get; set; }

        public DdBeneficio()
        {
            Nome = "";
        }
    }
}
