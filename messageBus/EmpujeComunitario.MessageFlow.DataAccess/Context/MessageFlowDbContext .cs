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
        public DbSet<CancelledDonation> CancelledDonation{ get; set; }
        public DbSet<SolidaryEvent> SolidaryEvents { get; set; }
        public DbSet<VolunteerAdhesion> VolunteerAdhesions { get; set; }
        public DbSet<CancelledEvent> CancelledEvents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ===== SolidaryEvent =====
            modelBuilder.Entity<SolidaryEvent>(entity =>
            {
                entity.HasKey(e => e.EventId);
                entity.Property(e => e.OrgId).IsRequired();
                entity.HasMany(e => e.Adhesions)
                      .WithOne(a => a.Event)
                      .HasForeignKey(a => a.EventId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // ===== VolunteerAdhesion =====
            modelBuilder.Entity<VolunteerAdhesion>(entity =>
            {
                entity.HasKey(a => new { a.EventId, a.VolunteerId });
                entity.Property(a => a.OrgId).IsRequired();
                entity.Property(a => a.Name).IsRequired();
                entity.Property(a => a.LastName).IsRequired();
            });

            // ===== CancelledEvent =====
            modelBuilder.Entity<CancelledEvent>(entity =>
            {
                entity.HasKey(c => new { c.EventId, c.OrgId });
                entity.Property(c => c.CancelledAt).IsRequired();
            });

            // ===== DonationRequest =====
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
                entity.Property(o => o.Create_user_id).HasColumnName("Creation_user_id");
                entity.HasMany(o => o.Donations)
                      .WithOne(d => d.Offer)
                      .HasForeignKey(d => d.OfferId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

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

            // ===== DonationTransfer =====
            modelBuilder.Entity<DonationTransfer>(entity =>
            {
                entity.HasKey(t => t.TransferId);
                entity.Property(t => t.TransferId).IsRequired();
                entity.Property(t => t.DonationOrgId).IsRequired();
                entity.HasMany(t => t.Donations)
                      .WithOne(d => d.Transfer)
                      .HasForeignKey(d => d.TransferId);
            });

            // ===== CancelledDonation =====
            modelBuilder.Entity<CancelledDonation>(entity =>
            {
                entity.HasKey(c => new { c.RequestId, c.OrgId });
                entity.Property(c => c.CancelledAt).IsRequired();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
