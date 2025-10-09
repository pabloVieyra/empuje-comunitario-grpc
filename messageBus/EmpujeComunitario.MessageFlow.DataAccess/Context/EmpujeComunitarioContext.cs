using EmpujeComunitario.MessageFlow.DataAccess.EntitiesEmpujeComunitario;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpujeComunitario.MessageFlow.DataAccess.Context
{
    public class EmpujeComunitarioContext : DbContext
    {
        public EmpujeComunitarioContext(DbContextOptions<EmpujeComunitarioContext> options) : base(options)
        {
        }
        public DbSet<Donation> Donations { get; set; }
        public DbSet<EventDonation> EventDonations { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<UserEvent> UserEvents { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Donations
            modelBuilder.Entity<Donation>(entity =>
            {
                entity.ToTable("donations", "public");
                entity.HasKey(d => d.Id);
                entity.Property(d => d.Id).HasMaxLength(255);
                entity.Property(d => d.Category).HasMaxLength(255).IsRequired();
                entity.Property(d => d.Description).HasMaxLength(255).IsRequired();
                entity.Property(d => d.CreationDate).IsRequired();
                entity.Property(d => d.ModificationDate).IsRequired(false);

                entity.HasOne(d => d.CreationUser)
                      .WithMany(u => u.DonationsCreated)
                      .HasForeignKey(d => d.CreationUserId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.ModificationUser)
                      .WithMany(u => u.DonationsModified)
                      .HasForeignKey(d => d.ModificationUserId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // EventDonations
            modelBuilder.Entity<EventDonation>(entity =>
            {
                entity.ToTable("event_donations", "public");
                entity.HasKey(ed => new { ed.DonationId, ed.EventId });

                entity.HasOne(ed => ed.Donation)
                      .WithMany(d => d.EventDonations)
                      .HasForeignKey(ed => ed.DonationId);

                entity.HasOne(ed => ed.Event)
                      .WithMany(e => e.EventDonations)
                      .HasForeignKey(ed => ed.EventId);
            });

            // Events
            modelBuilder.Entity<Event>(entity =>
            {
                entity.ToTable("events", "public");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Description).HasMaxLength(255).IsRequired();
                entity.Property(e => e.EventName).HasMaxLength(255).IsRequired();
                entity.Property(e => e.EventDateTime).IsRequired();
                entity.Property(e => e.ModificationDate).IsRequired(false);

                entity.HasOne(e => e.ModificationUser)
                      .WithMany(u => u.EventsModified)
                      .HasForeignKey(e => e.ModificationUserId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // UserEvents
            modelBuilder.Entity<UserEvent>(entity =>
            {
                entity.ToTable("user_events", "public");
                entity.HasKey(ue => new { ue.EventId, ue.UserId });

                entity.HasOne(ue => ue.User)
                      .WithMany(u => u.UserEvents)
                      .HasForeignKey(ue => ue.UserId);

                entity.HasOne(ue => ue.Event)
                      .WithMany(e => e.UserEvents)
                      .HasForeignKey(ue => ue.EventId);
            });

            // Users
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users", "public");
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Id).HasMaxLength(255);
                entity.Property(u => u.Email).HasMaxLength(255).IsRequired();
                entity.Property(u => u.LastName).HasMaxLength(255).IsRequired();
                entity.Property(u => u.Name).HasMaxLength(255).IsRequired();
                entity.Property(u => u.Password).HasMaxLength(255).IsRequired();
                entity.Property(u => u.Username).HasMaxLength(255).IsRequired();
                entity.Property(u => u.Phone).HasMaxLength(255).IsRequired(false);
                entity.Property(u => u.Role).HasMaxLength(255).IsRequired();
            });
        }
    }
}
