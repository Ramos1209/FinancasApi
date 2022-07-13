using System;
using System.Collections.Generic;
using System.Text;
using ControleFinanceiro.Bll.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleFinanceiro.DAL.Mapping
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.Property(u => u.Id).ValueGeneratedOnAdd();

            builder.Property(u => u.Cpf).IsRequired().HasMaxLength(20);
            builder.HasIndex(u => u.Cpf).IsUnique();

            builder.Property(u => u.Profissao).IsRequired().HasMaxLength(30);

            builder.HasMany(u => u.Cartoes).WithOne(u => u.Usuario).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(u => u.Despesas).WithOne(u => u.Usuario).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(u => u.Ganhos).WithOne(u => u.Usuario).OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("Usuarios");
        }
    }
}
