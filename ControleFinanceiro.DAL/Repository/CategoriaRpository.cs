using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControleFinanceiro.Bll.Models;
using ControleFinanceiro.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ControleFinanceiro.DAL.Repository
{
   public class CategoriaRpository:RepositoryGenerics<Categoria>,ICategoriaRpository
   {
       private readonly FinanceiroContext _context;
        public CategoriaRpository(FinanceiroContext context) : base(context)
        {
            _context = context;
        }

        public new  IQueryable<Categoria> GetAll()
        {
            try
            {
                return _context.Categorias.Include(c => c.Tipo);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public new async Task<Categoria> GetById(int id)
        {
            try
            {
                return await _context.Categorias.Include(c => c.Tipo).FirstOrDefaultAsync(x => x.CategoriaId == id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public IQueryable<Categoria> FiltrarCategorias(string nome)
        {
            try
            {
                return  _context.Categorias.Include(c => c.Tipo).Where(x => x.Nome.Contains(nome));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public IQueryable<Categoria> PegarCategoriaPeloTipo(string tipo)
        {
            try
            {
                return _context.Categorias.Include(c => c.Tipo).Where(c => c.Tipo.Nome == tipo);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}
