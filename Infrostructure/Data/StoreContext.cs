using System;
using System.Data.Common;
using Core.Entities;
using Infrostructure.Config;
using Microsoft.EntityFrameworkCore;

namespace Infrostructure.Data;

public class StoreContext(DbContextOptions options) : DbContext(options)
{

    public DbSet<Product> Products {get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfiguration).Assembly);
        
    }


}
