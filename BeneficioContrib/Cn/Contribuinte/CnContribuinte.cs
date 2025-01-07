using BeneficioContrib.Cn.Database;

namespace BeneficioContrib.Cn.Contribuinte
{
    public class CnContribuinte
    {
        private readonly Contexto Db;
        private readonly DaoContribuinte Dao;

        public CnContribuinte(Contexto contexto)
        {
            Db = contexto;
            Dao = new DaoContribuinte(contexto);
        }

        public bool CnpjExiste(string cnpj)
        {
            return Dao.CnpjExiste(cnpj);
        }

        public DdContribuinte? ObterPorId(int id)
        {
            return Dao.ObterPorId(id);
        }

        public List<DdContribuinte> ListarTodos()
        {
            return Dao.ListarTodos();
        }

        public int Salvar(DdContribuinte contribuinte)
        {
            try
            {
                Db.Database.BeginTransaction();

                int id;
                if (contribuinte.IdCodigo > 0)
                {
                    if (contribuinte.Cnpj != Dao.ObterPorId(contribuinte.IdCodigo)!.Cnpj)
                    {
                        throw new Exception("Não é possível alterar o CNPJ!");
                    }

                    id = Dao.Atualizar(contribuinte) ? contribuinte.IdCodigo : 0;

                    Dao.AtualizarVinculoBeneficios(contribuinte);
                }
                else
                {
                    if (Dao.CnpjExiste(contribuinte.Cnpj))
                    {
                        throw new Exception("CNPJ já cadastrado!");
                    }

                    id = Dao.Incluir(contribuinte);
                }

                if (id > 0)
                    Db.Database.CommitTransaction();
                else
                    Db.Database.RollbackTransaction();
                return id;
            }
            catch (Exception)
            {
                Db.Database.RollbackTransaction();
                throw;
            }
        }

        public bool Excluir(DdContribuinte contribuinte)
        {
            try
            {
                Db.Database.BeginTransaction();

                Dao.ExcluirVinculoBeneficioPorIdContribuinte(contribuinte.IdCodigo);

                var r = Dao.Excluir(contribuinte);

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

        public DdContribuinte? ObterPorCnpj(string cnpj)
        {
            return Dao.ObterPorCnpj(cnpj);
        }
    }
}
