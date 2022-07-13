using ControleFinanceiro.Bll.Models;
using ControleFinanceiro.DAL.Interfaces;

namespace ControleFinanceiro.DAL.Repository
{
    public class TipoRepository:RepositoryGenerics<Tipo>,ITipoRepository
    {
        public TipoRepository(FinanceiroContext context) : base(context)
        {
        }
    }
}
