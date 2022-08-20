using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleFinanceiro.Bll.Models;

namespace ControleFinanceiro.DAL.Interfaces
{
    public  interface IDespesasRepository : IRepositoryGenerics<Despesa>
    {
        IQueryable<Despesa> PegarDespesasPorUsuarioId(string usuarioId);
        void ExcluirDespesas(IEnumerable<Despesa> despesas);
        Task<IEnumerable<Despesa>> PegarDespesasPeloCartaoId(int cartaoId);
        IQueryable<Despesa> FiltrarDespesas(string nomeCategoria);

        Task<double> PegarDespesaTotalPorUsuarioId(string usuarioId);
    }
}
