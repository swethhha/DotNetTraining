    using Microsoft.EntityFrameworkCore;
    using SupportDesk.Core.Entities;

    namespace SupportDesk.Infrastructure.Data
    {
        public class AppDbContext : DbContext
        {
            public AppDbContext(DbContextOptions<AppDbContext> options)
                : base(options)
            {
            }

            public DbSet<User> Users { get; set; }
            public DbSet<Ticket> Tickets { get; set; }
            public DbSet<Tag> Tags { get; set; }
            public DbSet<TicketTag> TicketTags { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);

                // Many-to-Many between Ticket and Tag using TicketTag
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
