using API.CRUD.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.CRUD.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Fornecedor> Fornecedores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Fornecedor>(
                f =>
                {
                    f.HasKey(f => f.Id);
                    f.Property(f => f.Id).ValueGeneratedOnAdd().IsRequired();
                    f.Property(f => f.Nome).HasMaxLength(100).IsRequired();
                    f.Property(f => f.Documento).HasMaxLength(14).IsRequired();
                    f.Property(f => f.Ativo).IsRequired();
                }
            );
        }
    }
}
