using EmpujeComunitario.MessageFlow.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace EmpujeComunitario.MessageFlow.DataAccess.Context
{
    public class MessageFlowDbContext : DbContext
    {
        public MessageFlowDbContext(DbContextOptions<MessageFlowDbContext> options) : base(options) { }

        public DbSet<DonationRequest> DonationRequests { get; set; }
        public DbSet<DonationOffer> DonationOffers { get; set; }
        public DbSet<DonationItem> DonationItems { get; set; }
        public DbSet<DonationTransfer> DonationTransfers { get; set; }
        public DbSet<SolidaryEvent> SolidaryEvents { get; set; }
        public DbSet<VolunteerAdhesion> VolunteerAdhesions { get; set; }
        public DbSet<CancelledDonation> CancelledDonation{ get; set; }
        public DbSet<CancelledEvent> CancelledEvents { get; set; }

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

            // ===== SolidaryEvent =====
            modelBuilder.Entity<SolidaryEvent>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.OrgId).IsRequired();
                entity.Property(e => e.EventId).IsRequired();
            });

            // ===== VolunteerAdhesion =====
            modelBuilder.Entity<VolunteerAdhesion>(entity =>
            {
                entity.HasKey(v => v.Id);
                entity.Property(v => v.EventId).IsRequired();
                entity.Property(v => v.OrgId).IsRequired();
            });

            // ===== TransferDonation =====
            modelBuilder.Entity<DonationTransfer>(entity =>
            {
                entity.HasKey(t => t.Id);
                entity.Property(t => t.RequestId).IsRequired();
                entity.Property(t => t.DonationOrgId).IsRequired();
            });

            // ===== CancelledEvent =====
            modelBuilder.Entity<CancelledEvent>(entity =>
            {
                entity.HasKey(c => new { c.OrgId, c.EventId});
                entity.Property(c => c.OrgId).IsRequired();
                entity.Property(c => c.EventId).IsRequired();
            });

            // ===== CancelledRequest =====
            modelBuilder.Entity<CancelledDonation>(entity =>
            {
                entity.HasKey(c => new { c.OrgId, c.RequestId });
                entity.Property(c => c.OrgId).IsRequired();
                entity.Property(c => c.RequestId).IsRequired();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
