using EmpujeComunitario.Graphql.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;


namespace EmpujeComunitario.Graphql.DataAccess.Context
{
    public class MessageFlowDbContext : DbContext
    {
        public MessageFlowDbContext(DbContextOptions<MessageFlowDbContext> options) : base(options) { }
        public DbSet<UserSavedFilter> UserSavedFilters { get; set; }
        public DbSet<DonationRequest> DonationRequests { get; set; }
        public DbSet<DonationOffer> DonationOffers { get; set; }
        public DbSet<DonationItem> DonationItems { get; set; }
        public DbSet<DonationTransfer> DonationTransfers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ===== DonationItem =====
            modelBuilder.Entity<DonationItem>(entity =>
            {
                entity.HasKey(d => d.Id);

                entity.HasOne(d => d.Request)
                      .WithMany(r => r.Donations)
                      .HasForeignKey(d => d.RequestId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .IsRequired(false);

                entity.HasOne(d => d.Offer)
                      .WithMany(o => o.Donations)
                      .HasForeignKey(d => d.OfferId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .IsRequired(false);

                entity.HasOne(d => d.Transfer)
                      .WithMany(t => t.Donations)
                      .HasForeignKey(d => d.TransferId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .IsRequired(false);
            });


            modelBuilder.Entity<DonationRequest>(entity =>
            {
                entity.HasKey(r => r.RequestId);
                entity.Property(r => r.RequesterOrgId).IsRequired();
                entity.HasMany(r => r.Donations)
                      .WithOne(d => d.Request)
                      .HasForeignKey(d => d.RequestId)
                      .OnDelete(DeleteBehavior.Cascade);
            });


            // ===== DonationOffer =====
            modelBuilder.Entity<DonationOffer>(entity =>
            {
                entity.HasKey(o => o.OfferId);
                entity.Property(o => o.DonationOrganizationId).IsRequired();
                entity.HasMany(o => o.Donations)
                      .WithOne(d => d.Offer)
                      .HasForeignKey(d => d.OfferId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<DonationTransfer>(entity =>
            {
                entity.HasKey(t => t.TransferId);
                entity.Property(t => t.TransferId).IsRequired();
                entity.Property(t => t.DonationOrgId).IsRequired();
                entity.HasMany(t => t.Donations)
                      .WithOne(d => d.Transfer)
                      .HasForeignKey(d => d.TransferId);
            });


            modelBuilder.ApplyConfiguration(new UserSavedFilterConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}