using BeneficioContrib.Cn.Beneficio;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeneficioContrib.Cn.Contribuinte.VinculoContribuinteBeneficio
{
    [Table("vinculo_contribuinte_beneficio")]
    public class DdVinculoContribuinteBeneficio
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_codigo")]
        public int ID { get; set; }

        [ForeignKey(nameof(Contribuinte))]
        [Column("es_contribuinte")]
        public int EsContribuinte { get; set; }

        public DdContribuinte? Contribuinte { get; set; }

        [ForeignKey(nameof(Beneficio))]
        [Column("es_beneficio")]
        public int EsBeneficio { get; set; }

        public DdBeneficio? Beneficio { get; set; }

    }
}
