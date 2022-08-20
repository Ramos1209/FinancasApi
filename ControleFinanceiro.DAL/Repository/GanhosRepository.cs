using ControleFinanceiro.Bll.Models;
using ControleFinanceiro.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
    }
}
