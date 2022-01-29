using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using mvc.Repository.Models;

namespace mvc.Repository
{
    public partial class gcasppDbContext : DbContext
    {
        public gcasppDbContext()
        {
        }

        public gcasppDbContext(DbContextOptions<gcasppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Filho> Filhos { get; set; } = null!;
        public virtual DbSet<Funcionario> Funcionarios { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Filho>(entity =>
            {
                entity.HasOne(d => d.FilhoFuncionarioMaeNavigation)
                    .WithMany(p => p.FilhoFilhoFuncionarioMaeNavigations)
                    .HasForeignKey(d => d.FilhoFuncionarioMae)
                    .HasConstraintName("FK_Filho_FuncionarioMae");

                entity.HasOne(d => d.FilhoFuncionarioPaiNavigation)
                    .WithMany(p => p.FilhoFilhoFuncionarioPaiNavigations)
                    .HasForeignKey(d => d.FilhoFuncionarioPai)
                    .HasConstraintName("FK_Filho_FuncionarioPai");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
