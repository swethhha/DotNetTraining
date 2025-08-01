using System;
using Microsoft.EntityFrameworkCore;
using SupportDeskAssesment.Models;

namespace SupportDeskAssesment.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Ticket> Tickets => Set<Ticket>();
        public DbSet<Tag> Tags => Set<Tag>();
        public DbSet<TicketTag> TicketTags => Set<TicketTag>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=BugTrackerLiteDB;User Id=sa;Password=Swetha@06112004;TrustServerCertificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TicketTag>()
                .HasKey(tt => new { tt.TicketId, tt.TagId });

            modelBuilder.Entity<TicketTag>()
                .HasOne(tt => tt.Ticket)
                .WithMany(t => t.TicketTags)
                .HasForeignKey(tt => tt.TicketId);

            modelBuilder.Entity<TicketTag>()
                .HasOne(tt => tt.Tag)
                .WithMany(t => t.TicketTags)
                .HasForeignKey(tt => tt.TagId);
        }
    }

}
