using BeneficioContrib.Cn.Database;

namespace BeneficioContrib.Cn.Beneficio
{
    public class CnBeneficio
    {
        private readonly Contexto Db;
        private readonly DaoBeneficio Dao;

        public CnBeneficio(Contexto contexto)
        {
            Db = contexto;
            Dao = new DaoBeneficio(contexto);
        }

        public decimal SimularPagamento(int idBeneficio, decimal valor, out DdBeneficio? beneficio)
        {
            beneficio = ObterPorId(idBeneficio);
            if (beneficio is null)
            {
                return valor;
            }

            return valor - (valor * ((beneficio.PorcentagemDesconto > 0 ? beneficio.PorcentagemDesconto : 1) / 100));
        }


        public List<DdBeneficio> ListarTodos()
        {
            return Dao.ListarTodos();
        }

        public Dictionary<DdBeneficio, int> ListarTodosComQtdContrib()
        {
            return Dao.ListarTodosComQtdContrib();
        }

        internal List<DdBeneficio> ListarTodosExcetoIds(List<int> idsNaoBuscar)
        {
            return Dao.ListarTodosExcetoIds(idsNaoBuscar);
        }

        public DdBeneficio? ObterPorId(int id)
        {
            return Dao.ObterPorId(id);
        }

        public (DdBeneficio Beneficio, int Count)? ObterPorIdComQtdContrib(int id)
        {
            return Dao.ObterPorIdComQtdContrib(id);
        }

        public int Salvar(DdBeneficio beneficio)
        {
            try
            {
                Db.Database.BeginTransaction();

                var r = Dao.Salvar(beneficio);

                if (r > 0)
                    Db.Database.CommitTransaction();
                else
                    Db.Database.RollbackTransaction();

                return beneficio.IdCodigo = r;
            }
            catch (Exception)
            {
                Db.Database.RollbackTransaction();
                throw;
            }
        }

        public bool Excluir(DdBeneficio beneficio)
        {
            try
            {
                Db.Database.BeginTransaction();

                Dao.ExcluirVinculoContribuintePorIdBeneficio(beneficio.IdCodigo);

                var r = Dao.Excluir(beneficio);

                if (r)
                    Db.Database.CommitTransaction();
                else
                    Db.Database.RollbackTransaction();

                return r;
            }
            catch (Exception)
            {
                Db.Database.RollbackTransaction();
                throw;
            }
        }
    }
}
