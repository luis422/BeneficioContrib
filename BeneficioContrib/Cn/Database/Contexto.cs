using BeneficioContrib.Cn.Beneficio;
using BeneficioContrib.Cn.Contribuinte;
using BeneficioContrib.Cn.Contribuinte.VinculoContribuinteBeneficio;
using Microsoft.EntityFrameworkCore;

namespace BeneficioContrib.Cn.Database
{
    public class Contexto : DbContext
    {
        public DbSet<DdBeneficio> Beneficios { get; set; }
        public DbSet<DdContribuinte> Contribuintes { get; set; }
        public DbSet<DdVinculoContribuinteBeneficio> VinculoContribuinteBeneficios { get; set; }

        public Contexto(DbContextOptions options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
    }
}
