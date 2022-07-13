using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControleFinanceiro.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ControleFinanceiro.DAL.Repository
{
   public class RepositoryGenerics<Tentity>: IRepositoryGenerics<Tentity> where Tentity :class
   {
       private readonly FinanceiroContext _context;

       public RepositoryGenerics(FinanceiroContext context)
       {
           _context = context;
       }
       public async Task Update(Tentity entity)
       {
           try
           {
               var registro = _context.Set<Tentity>().Update(entity);
               registro.State = EntityState.Modified;
               await _context.SaveChangesAsync();
           }
           catch (Exception ex)
           {
               Console.WriteLine(ex);
               throw;
           }
       }

        public  IQueryable<Tentity> GetAll()
        {
            try
            {
                return  _context.Set<Tentity>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<Tentity> GetById(int id)
        {
            try
            {
                return await _context.Set<Tentity>().FindAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<Tentity> GetById(string id)
        {
            try
            {
                return await _context.Set<Tentity>().FindAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task Insert(Tentity entity)
        {
            try
            {
                await _context.AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task Insert(List<Tentity> entity)
        {
            try
            {
                await _context.AddRangeAsync(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

      
        public async Task Excluir(string id)
        {
            try
            {
                var entity = await GetById(id);
                 _context.Set<Tentity>().Remove(entity);
                 await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task Excluir(int id)
        {
            try
            {
                var entity = await GetById(id);
                _context.Set<Tentity>().Remove(entity);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task Excluir(Tentity entity)
        {
            try
            {
               
                _context.Set<Tentity>().Remove(entity);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}
