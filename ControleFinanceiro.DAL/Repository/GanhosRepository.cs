using ControleFinanceiro.Bll.Models;
using ControleFinanceiro.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFinanceiro.DAL.Repository
{
    public class GanhosRepository : RepositoryGenerics<Ganho>, IGanhosRepository
    {
        private readonly FinanceiroContext _context;

        public GanhosRepository(FinanceiroContext context) : base(context)
        {
            _context = context;
        }


      
        public IQueryable<Ganho> PegarGanhosUsuarioId(string usuarioId)
        {
            return _context.Ganhos.Include(g=>g.Mes).Include(g=>g.Categoria).Where(x => x.UsuarioId == usuarioId);
        }

        public async Task<double> PegarGanhoTotalPeloUsuarioId(string usuarioId)
        {
            return await _context.Ganhos.Where(g => g.UsuarioId == usuarioId).SumAsync(g => g.Valor);
        }
    }
}
