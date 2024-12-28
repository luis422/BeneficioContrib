using BeneficioContrib.Cn.Contribuinte.VinculoContribuinteBeneficio;
using BeneficioContrib.Cn.Database;
using Microsoft.EntityFrameworkCore;

namespace BeneficioContrib.Cn.Contribuinte
{
    public class DaoContribuinte
    {
        private readonly Contexto Db;

        public DaoContribuinte(Contexto contexto)
        {
            Db = contexto;
        }

        internal DdContribuinte? ObterPorId(int id)
        {
            return Db.Contribuintes
                .Include(nameof(DdContribuinte.Beneficios) + "." + nameof(DdVinculoContribuinteBeneficio.Beneficio))
                .FirstOrDefault(c => c.IdCodigo == id);
        }

        internal int Incluir(DdContribuinte contribuinte)
        {
            Db.Contribuintes.Add(contribuinte);
            Db.SaveChanges();
            return contribuinte.IdCodigo;
        }

        internal bool Atualizar(DdContribuinte contribuinte)
        {
            Db.Entry(contribuinte).Collection(c => c.Beneficios).EntityEntry.State = EntityState.Unchanged;
            Db.Contribuintes.Update(contribuinte);
            return Db.SaveChanges() > 0;
        }

        internal bool Existe(DdContribuinte contribuinte)
        {
            return Db.Contribuintes.Any(c => c.IdCodigo == contribuinte.IdCodigo || c.Cnpj == contribuinte.Cnpj);
        }

        internal bool Excluir(DdContribuinte contribuinte)
        {
            if (!Existe(contribuinte))
            {
                return false;
            }

            Db.Contribuintes.Attach(contribuinte);
            Db.Contribuintes.Remove(contribuinte);
            return Db.SaveChanges() > 0;
        }

        internal List<DdContribuinte> ListarTodos()
        {
            return Db.Contribuintes
                .Include(nameof(DdContribuinte.Beneficios) + "." + nameof(DdVinculoContribuinteBeneficio.Beneficio))
                .ToList();
        }

        internal bool CnpjExiste(string cnpj)
        {
            return Db.Contribuintes.Any(c => c.Cnpj == cnpj);
        }

        internal void AtualizarVinculoBeneficios(DdContribuinte contribuinte)
        {
            var dbBeneficios = Db.VinculoContribuinteBeneficios.AsNoTracking()
                .Where(v => v.EsContribuinte == contribuinte.IdCodigo).ToList();

            var dbIdsBeneficios = dbBeneficios.Select(b => b.EsBeneficio).ToList();



            var excluir = dbBeneficios
                .Where(v =>
                    !contribuinte.Beneficios.Select(b => b.EsBeneficio).Contains(v.EsBeneficio))
                .ToList();

            //var atualizar = contribuinte.Beneficios
            //    .Where(v => dbIdsBeneficios.Contains(v.EsBeneficio))
            //    .ToList();

            var incluir = contribuinte.Beneficios
                .Where(v => !dbIdsBeneficios.Contains(v.EsBeneficio))
                .Select(v => new DdVinculoContribuinteBeneficio()
                {
                    EsBeneficio = v.EsBeneficio,
                    EsContribuinte = contribuinte.IdCodigo
                })
                .ToList();


            Db.VinculoContribuinteBeneficios.RemoveRange(excluir);
            Db.VinculoContribuinteBeneficios.AddRange(incluir);
            //Db.VinculoContribuinteBeneficios.UpdateRange(atualizar);
            Db.SaveChanges();
        }

        internal void ExcluirVinculoBeneficioPorIdContribuinte(int idContribuinte)
        {
            Db.VinculoContribuinteBeneficios.RemoveRange(Db.VinculoContribuinteBeneficios.Where(v => v.EsContribuinte == idContribuinte));
            Db.SaveChanges();
        }
    }
}
