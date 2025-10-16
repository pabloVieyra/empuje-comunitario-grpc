using EmpujeComunitario.Graphql.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;


namespace EmpujeComunitario.Graphql.DataAccess.Context
{
    public class MessageFlowDbContext : DbContext
    {
        public MessageFlowDbContext(DbContextOptions<MessageFlowDbContext> options) : base(options) { }

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

                // Relación con DonationRequest
                entity.HasOne<DonationRequest>()
                      .WithMany(r => r.Donations)
                      .HasForeignKey(d => d.RequestId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .IsRequired(false);

                // Relación con DonationOffer
                entity.HasOne<DonationOffer>()
                      .WithMany(o => o.Donations)
                      .HasForeignKey(d => d.OfferId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .IsRequired(false);

                // Relación con TransferDonation
                entity.HasOne<DonationTransfer>()
                      .WithMany(t => t.Donations)
                      .HasForeignKey(d => d.TransferId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .IsRequired(false);
            });

            // ===== DonationRequest =====
            modelBuilder.Entity<DonationRequest>(entity =>
            {
                entity.HasKey(r => r.Id);
                entity.Property(r => r.RequesterOrgId).IsRequired();
                entity.Property(r => r.RequestId).IsRequired();
            });

            // ===== DonationOffer =====
            modelBuilder.Entity<DonationOffer>(entity =>
            {
                entity.HasKey(o => o.Id);
                entity.Property(o => o.OfferId).IsRequired();
                entity.Property(o => o.DonationOrganizationId).IsRequired();
            });

            // ===== TransferDonation =====
            modelBuilder.Entity<DonationTransfer>(entity =>
            {
                entity.HasKey(t => t.Id);
                entity.Property(t => t.RequestId).IsRequired();
                entity.Property(t => t.DonationOrgId).IsRequired();
            });

           

            base.OnModelCreating(modelBuilder);
        }
    }
}