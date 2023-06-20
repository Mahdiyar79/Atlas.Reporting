using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Reporting.DAL.Domain.InternalSendingModel;

public partial class InternalSendingContext : DbContext
{
    public InternalSendingContext()
    {
    }

    public InternalSendingContext(DbContextOptions<InternalSendingContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BranchConnection> BranchConnections { get; set; }

    public virtual DbSet<ChatGroup> ChatGroups { get; set; }

    public virtual DbSet<GroupChatMessage> GroupChatMessages { get; set; }

    public virtual DbSet<PrivateChatMessage> PrivateChatMessages { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BranchConnection>(entity =>
        {
            entity.Property(e => e.BranchId).ValueGeneratedNever();
            entity.Property(e => e.BrandId).HasDefaultValueSql("((1))");
            entity.Property(e => e.IsDisable).HasDefaultValueSql("(CONVERT([bit],(0)))");
            entity.Property(e => e.LastPing).HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");
        });

        modelBuilder.Entity<ChatGroup>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasMany(d => d.Users).WithMany(p => p.Groups)
                .UsingEntity<Dictionary<string, object>>(
                    "UserGroup",
                    r => r.HasOne<User>().WithMany().HasForeignKey("UserId"),
                    l => l.HasOne<ChatGroup>().WithMany().HasForeignKey("GroupId"),
                    j =>
                    {
                        j.HasKey("GroupId", "UserId");
                        j.ToTable("UserGroups");
                        j.HasIndex(new[] { "UserId" }, "IX_UserGroups_UserId");
                    });
        });

        modelBuilder.Entity<GroupChatMessage>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<PrivateChatMessage>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Receiver).WithMany(p => p.PrivateChatMessageReceivers).OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Sender).WithMany(p => p.PrivateChatMessageSenders).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.UserId).ValueGeneratedNever();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
