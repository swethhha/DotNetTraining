using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SupportDeskProj2.Models;

public partial class SupportDeskProj2Context : DbContext
{
    public SupportDeskProj2Context()
    {
    }

    public SupportDeskProj2Context(DbContextOptions<SupportDeskProj2Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<CustomerProfile> CustomerProfiles { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<SupportAgent> SupportAgents { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<TicketHistory> TicketHistories { get; set; }

    public virtual DbSet<TicketSupportAgent> TicketSupportAgents { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=SupportDeskProj2;User Id=sa;Password=Swetha@06112004;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Categori__19093A0BF4BDC82F");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<CustomerProfile>(entity =>
        {
            entity.HasKey(e => e.ProfileId).HasName("PK__Customer__290C88E4CDB91AF7");

            entity.HasIndex(e => e.UserId, "UQ__Customer__1788CC4DE61582FC").IsUnique();

            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.Phone).HasMaxLength(20);

            entity.HasOne(d => d.User).WithOne(p => p.CustomerProfile)
                .HasForeignKey<CustomerProfile>(d => d.UserId)
                .HasConstraintName("FK__CustomerP__UserI__3A81B327");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PK__Departme__B2079BED141B700D");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<SupportAgent>(entity =>
        {
            entity.HasKey(e => e.AgentId).HasName("PK__SupportA__9AC3BFF121F5C3F6");

            entity.HasIndex(e => e.UserId, "UQ__SupportA__1788CC4D5F3E1CC5").IsUnique();

            entity.HasOne(d => d.Department).WithMany(p => p.SupportAgents)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__SupportAg__Depar__412EB0B6");

            entity.HasOne(d => d.User).WithOne(p => p.SupportAgent)
                .HasForeignKey<SupportAgent>(d => d.UserId)
                .HasConstraintName("FK__SupportAg__UserI__403A8C7D");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.TicketId).HasName("PK__Tickets__712CC607268C0E2D");

            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.Title).HasMaxLength(200);

            entity.HasOne(d => d.Agent).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.AgentId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Tickets__AgentId__5070F446");

            entity.HasOne(d => d.Category).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Tickets__Categor__5165187F");

            entity.HasOne(d => d.Customer).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tickets__Custome__4F7CD00D");
        });

        modelBuilder.Entity<TicketHistory>(entity =>
        {
            entity.HasKey(e => e.HistoryId).HasName("PK__TicketHi__4D7B4ABD07BBD7E8");

            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Ticket).WithMany(p => p.TicketHistories)
                .HasForeignKey(d => d.TicketId)
                .HasConstraintName("FK__TicketHis__Ticke__5535A963");
        });

        modelBuilder.Entity<TicketSupportAgent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TicketSu__3214EC07DA9F8D19");

            entity.ToTable("TicketSupportAgent");

            entity.HasIndex(e => new { e.TicketId, e.AgentId }, "UQ__TicketSu__7880FDF926B247A5").IsUnique();

            entity.HasOne(d => d.Agent).WithMany(p => p.TicketSupportAgents)
                .HasForeignKey(d => d.AgentId)
                .HasConstraintName("FK__TicketSup__Agent__5DCAEF64");

            entity.HasOne(d => d.Ticket).WithMany(p => p.TicketSupportAgents)
                .HasForeignKey(d => d.TicketId)
                .HasConstraintName("FK__TicketSup__Ticke__5CD6CB2B");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C827B885D");

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Role).HasMaxLength(50);
            entity.Property(e => e.UserName).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
