using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;
using ProjetoAPI.Models;

namespace ProjetoAPI.DataBase;

public partial class ProdutosDbContext : DbContext
{

    public DbSet<Produto> Produtos { get; set; }

    public ProdutosDbContext()
    {
    }

    public ProdutosDbContext(DbContextOptions<ProdutosDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
