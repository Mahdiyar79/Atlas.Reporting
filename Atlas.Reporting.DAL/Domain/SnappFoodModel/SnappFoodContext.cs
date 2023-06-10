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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=188.121.112.160;Database=SnappFoodDb;user id=sa;password=5q8Ms#dbBte6tI;MultipleActiveResultSets=true;TrustServerCertificate=True;");

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
