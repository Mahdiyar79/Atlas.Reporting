using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Reporting.DAL.Domain.OrderModel;

public partial class OrderContext : DbContext
{
    public OrderContext()
    {
    }

    public OrderContext(DbContextOptions<OrderContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Branch> Branches { get; set; }

    public virtual DbSet<Canceltype> Canceltypes { get; set; }

    public virtual DbSet<Deliverymethod> Deliverymethods { get; set; }

    public virtual DbSet<Driver> Drivers { get; set; }

    public virtual DbSet<Driverstatus> Driverstatuses { get; set; }

    public virtual DbSet<Driverstatuslog> Driverstatuslogs { get; set; }

    public virtual DbSet<IntegrationEventLog> IntegrationEventLogs { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<Orderstatus> Orderstatuses { get; set; }

    public virtual DbSet<Paymentmethod> Paymentmethods { get; set; }

    public virtual DbSet<Productitem> Productitems { get; set; }

    public virtual DbSet<Request> Requests { get; set; }

    public virtual DbSet<Sourcetype> Sourcetypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=188.121.112.160;Database=OrderingDb;user id=sa;password=5q8Ms#dbBte6tI;MultipleActiveResultSets=true;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Branch>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.BrandId).HasDefaultValueSql("((1))");
            entity.Property(e => e.IsActive).HasDefaultValueSql("(CONVERT([bit],(1)))");
        });

        modelBuilder.Entity<Canceltype>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Deliverymethod>(entity =>
        {
            entity.Property(e => e.Id).HasDefaultValueSql("((1))");
        });

        modelBuilder.Entity<Driver>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.IsEnabled).HasDefaultValueSql("(CONVERT([bit],(1)))");
        });

        modelBuilder.Entity<Driverstatus>(entity =>
        {
            entity.Property(e => e.Id).HasDefaultValueSql("((1))");
        });

        modelBuilder.Entity<Driverstatuslog>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<IntegrationEventLog>(entity =>
        {
            entity.Property(e => e.EventId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasIndex(e => e.ExternalInvoiceNo, "IX_orders_ExternalInvoiceNo")
                .IsUnique()
                .HasFilter("([ExternalInvoiceNo] IS NOT NULL)");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Branch).WithMany(p => p.Orders).OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.DeliveryMethod).WithMany(p => p.Orders).OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.PaymentMethod).WithMany(p => p.Orders).OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.SourceType).WithMany(p => p.Orders).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Orderstatus>(entity =>
        {
            entity.Property(e => e.Id).HasDefaultValueSql("((1))");
        });

        modelBuilder.Entity<Paymentmethod>(entity =>
        {
            entity.Property(e => e.Id).HasDefaultValueSql("((1))");
        });

        modelBuilder.Entity<Request>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Sourcetype>(entity =>
        {
            entity.Property(e => e.Id).HasDefaultValueSql("((1))");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
