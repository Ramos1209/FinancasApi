using ControleFinanceiro.Bll.Models;
using ControleFinanceiro.DAL.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFinanceiro.DAL.Repository
{
    public class CartaoRepository: RepositoryGenerics<Cartao>, ICartaoRepository
    {
        private readonly FinanceiroContext _context;
        public CartaoRepository(FinanceiroContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<Cartao> PegarCartoesPeloUsuarioId(string usuarioId)
        {
            try
            {
                var lista = _context.Cartoes.Where(x => x.UsuarioId == usuarioId);
                return lista;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public IQueryable<Cartao> FiltrarCartoes(string numeroCartao)
        {
            return _context.Cartoes.Where(c => c.Numero.Contains(numeroCartao));
        }

        public Task<int> PegarQuantidadeCartoesPeloUsuarioId(string usuarioId)
        {
            throw new NotImplementedException();
        }
    }
}
