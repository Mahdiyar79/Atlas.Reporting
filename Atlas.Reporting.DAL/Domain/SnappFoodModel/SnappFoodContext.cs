using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Reporting.DAL.Domain.SnappFoodModel;

public partial class SnappFoodContext : DbContext
{
    public SnappFoodContext()
    {
    }

    public SnappFoodContext(DbContextOptions<SnappFoodContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Brand> Brands { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Brand>(entity =>
        {
            entity.Property(e => e.BrandId).ValueGeneratedNever();
            entity.Property(e => e.IsEnable).HasDefaultValueSql("(CONVERT([bit],(0)))");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
