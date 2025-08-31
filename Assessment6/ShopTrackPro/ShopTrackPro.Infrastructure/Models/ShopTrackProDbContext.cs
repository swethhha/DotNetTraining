using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ShopTrackPro.Core.Entities;

namespace ShopTrackPro.Infrastructure.Models;

public partial class ShopTrackProDbContext : DbContext
{
    public ShopTrackProDbContext()
    {
    }

    public ShopTrackProDbContext(DbContextOptions<ShopTrackProDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=ShopTrackProDB;User ID=sa;Password=Swetha@06112004;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07F7684B62");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D105342B092F51").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.PasswordHash).HasMaxLength(200);
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .HasDefaultValue("User");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
