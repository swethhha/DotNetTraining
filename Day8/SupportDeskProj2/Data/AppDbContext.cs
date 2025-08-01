using Microsoft.EntityFrameworkCore;
using SupportDeskProj2.Models;

namespace SupportDeskProj2
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Ticket> Tickets => Set<Ticket>();
        public DbSet<SupportAgent> SupportAgents => Set<SupportAgent>();
        public DbSet<CustomerProfile> CustomerProfiles => Set<CustomerProfile>();
        public DbSet<TicketSupportAgent> TicketSupportAgents => Set<TicketSupportAgent>();
        public DbSet<TicketHistory> TicketHistories => Set<TicketHistory>();
        public DbSet<Department> Departments => Set<Department>();
        public DbSet<Category> Categories => Set<Category>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=SupportDeskProj2;User Id=sa;Password=Swetha@06112004;TrustServerCertificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Many-to-Many between Ticket and SupportAgent
            modelBuilder.Entity<TicketSupportAgent>()
                .HasKey(tsa => new { tsa.TicketId, tsa.AgentId });

            modelBuilder.Entity<TicketSupportAgent>()
                .HasOne(tsa => tsa.Ticket)
                .WithMany(t => t.TicketSupportAgents)
                .HasForeignKey(tsa => tsa.TicketId);

            modelBuilder.Entity<TicketSupportAgent>()
                .HasOne(tsa => tsa.Agent)
                .WithMany(a => a.TicketSupportAgents)
                .HasForeignKey(tsa => tsa.AgentId);

            // One-to-One: User ↔ CustomerProfile
            modelBuilder.Entity<User>()
                .HasOne(u => u.CustomerProfile)
                .WithOne(cp => cp.User)
                .HasForeignKey<CustomerProfile>(cp => cp.UserId);

            // One-to-Many: Department → Agents
            modelBuilder.Entity<Department>()
                .HasMany(d => d.SupportAgents)
                .WithOne(a => a.Department)
                .HasForeignKey(a => a.DepartmentId);

            // One-to-Many: Ticket → TicketHistories
            modelBuilder.Entity<Ticket>()
                .HasMany(t => t.TicketHistories)
                .WithOne(th => th.Ticket)
                .HasForeignKey(th => th.TicketId);
        }
    }
}
