using System.Reflection;
using ControleFinanceiro.Bll.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ControleFinanceiro.DAL
{
    public class FinanceiroContext:IdentityDbContext<Usuario,Funcao, string>
    {
        public FinanceiroContext(DbContextOptions<FinanceiroContext> options):base(options)
        {
            
        }
        public DbSet<Cartao> Cartoes { get; set; }

        public DbSet<Categoria> Categorias { get; set; }

        public DbSet<Mes> Meses { get; set; }


        public DbSet<Despesa> Despesas { get; set; }

        public DbSet<Ganho> Ganhos { get; set; }

        public DbSet<Funcao> Funcoes { get; set; }

        public DbSet<Tipo> Tipos { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
