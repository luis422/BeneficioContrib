using BeneficioContrib.Cn.Database;

namespace BeneficioContrib.Cn.Beneficio
{
    public class DaoBeneficio
    {
        private readonly Contexto Db;

        public DaoBeneficio(Contexto contexto)
        {
            Db = contexto;
        }

        internal List<DdBeneficio> ListarTodos()
        {
            return Db.Beneficios.ToList();
        }

        internal Dictionary<DdBeneficio, int> ListarTodosComQtdContrib()
        {
            return Db.Beneficios.Select(b => new
            {
                beneficio = b,
                count = Db.VinculoContribuinteBeneficios.Count(v => v.EsBeneficio == b.IdCodigo)
            }).ToDictionary(
                result => result.beneficio,
                result => result.count
            );
        }

        internal List<DdBeneficio> ListarTodosExcetoIds(List<int> idsNaoBuscar)
        {
            return Db.Beneficios.Where(b => !idsNaoBuscar.Contains(b.IdCodigo)).ToList();
        }

        internal DdBeneficio? ObterPorId(int id)
        {
            return Db.Beneficios.Find(id);
        }

        internal (DdBeneficio Beneficio, int Count)? ObterPorIdComQtdContrib(int id)
        {
            var b = Db.Beneficios.Find(id);
            if (b == null)
            {
                return null;
            }

            return (b, Db.VinculoContribuinteBeneficios.Count(v => v.EsBeneficio == b.IdCodigo));
        }

        internal int Salvar(DdBeneficio beneficio)
        {
            if (Db.Beneficios.Any(b => b.IdCodigo == beneficio.IdCodigo))
            {
                return Atualizar(beneficio) ? beneficio.IdCodigo : 0;
            }

            return Incluir(beneficio);
        }

        internal bool Atualizar(DdBeneficio beneficio)
        {
            Db.Beneficios.Attach(beneficio);
            Db.Beneficios.Update(beneficio);
            return Db.SaveChanges() > 0;
        }

        internal int Incluir(DdBeneficio beneficio)
        {
            Db.Beneficios.Add(beneficio);
            Db.SaveChanges();
            return beneficio.IdCodigo;
        }

        internal bool Excluir(DdBeneficio beneficio)
        {
            if (!Db.Beneficios.Any(b => b.IdCodigo == beneficio.IdCodigo))
            {
                return false;
            }

            Db.Beneficios.Attach(beneficio);
            Db.Beneficios.Remove(beneficio);
            return Db.SaveChanges() > 0;
        }

        internal void ExcluirVinculoContribuintePorIdBeneficio(int idBeneficio)
        {
            Db.VinculoContribuinteBeneficios.RemoveRange(Db.VinculoContribuinteBeneficios.Where(v => v.EsBeneficio == idBeneficio));
            Db.SaveChanges();
        }
    }
}