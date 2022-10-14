using System;
using ControleFinanceiro.Bll.Models;
using ControleFinanceiro.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ControleFinanceiro.DAL.Repository
{
    public class DespesasRepository: RepositoryGenerics<Despesa>, IDespesasRepository
    {
        private readonly FinanceiroContext _context;

        public DespesasRepository(FinanceiroContext context) : base(context)
        {
            _context = context;
        }
        public void ExcluirDespesas(IEnumerable<Despesa> despesas)
        {
            try
            {
                _context.Despesas.RemoveRange(despesas);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<IEnumerable<Despesa>> PegarDespesasPeloCartaoId(int cartaoId)
        {
            try
            {
                return await _context.Despesas.Where(c => c.CartaoId == cartaoId).ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

       
        public IQueryable<Despesa> PegarDespesasPorUsuarioId(string usuarioId)
        {
            try
            {
                return _context.Despesas.Include(d => d.Cartao)
                                        .Include(d => d.Categoria)
                                        .Include(m=>m.Mes)
                    .Where(d => d.UsuarioId == usuarioId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public IQueryable<Despesa> FiltrarDespesas(string nomeCategoria)
        {
            try
            {
                return _context.Despesas.Include(x => x.Cartao)
                    .Include(x => x.Categoria).ThenInclude(x => x.Tipo)
                    .Include(x => x.Mes)
                    .Where(x => x.Categoria.Nome.Contains(nomeCategoria) && x.Categoria.Tipo.Nome =="Despesa");


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<double> PegarDespesaTotalPorUsuarioId(string usuarioId)
        {
            try
            {
                return await _context.Despesas.Where(d => d.UsuarioId == usuarioId).SumAsync(d => d.Valor);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



    }
}
