using ControleFinanceiro.Bll.Models;
using ControleFinanceiro.DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFinanceiro.DAL.Repository
{
    public class FuncaoRepository: RepositoryGenerics<Funcao>, IFuncaoRepository
    {
        private readonly FinanceiroContext _context;
        private readonly RoleManager<Funcao> _roleManager;
        public FuncaoRepository(FinanceiroContext context, RoleManager<Funcao> roleManager) : base(context)
        {
            _context = context;
            _roleManager = roleManager;
        }

        public async Task AdicionarFuncao(Funcao funcao)
        {
            try
            {
                await _roleManager.CreateAsync(funcao);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task AtualizarFuncao(Funcao funcao)
        {
            try
            {
                Funcao f = await GetById(funcao.Id);
                f.Name = funcao.Name;
                f.NormalizedName = funcao.NormalizedName;
                f.Descricao = funcao.Descricao;

                await _roleManager.UpdateAsync(f);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public IQueryable<Funcao> FiltrarCategorias(string nome)
        {
          
                try
                {
                    return _context.Funcoes.Where(x => x.Name.Contains(nome));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            
        }
    }
}
