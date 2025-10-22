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

        public DbSet<User> Users { get; set; }
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
                entity.Property(r => r.Creation_user_id)
                    .HasColumnName("Creation_user_id")
                    .HasConversion(
                      v => v.ToString(),     // de Guid a string para C#
                      v => Guid.Parse(v)     // de string a Guid para la BD
                    );
                entity.HasMany(r => r.Donations)
                      .WithOne(d => d.Request)
                      .HasForeignKey(d => d.RequestId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(x => x.User)               // cada DonationRequest tiene 1 User
                  .WithMany()                   // no necesitamos colección de Requests en User
                  .HasForeignKey(r => r.Creation_user_id) // FK en DonationRequest
                  .HasPrincipalKey(u => u.Id)   // PK en User
                  .OnDelete(DeleteBehavior.Restrict);
            });


            // ===== DonationOffer =====
            modelBuilder.Entity<DonationOffer>(entity =>
            {
                entity.HasKey(o => o.OfferId);
                entity.Property(r => r.Creation_user_id)
                    .HasColumnName("Creation_user_id")
                    .HasConversion(
                          v => v.ToString(),     // de Guid a string para C#
                          v => Guid.Parse(v)     // de string a Guid para la BD
                      );
                entity.Property(o => o.DonationOrganizationId).IsRequired();
                entity.HasMany(o => o.Donations)
                      .WithOne(d => d.Offer)
                      .HasForeignKey(d => d.OfferId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(x=> x.User )
                  .WithMany()
                  .HasForeignKey(o => o.Creation_user_id)
                  .HasPrincipalKey(u => u.Id)
                  .OnDelete(DeleteBehavior.Restrict);
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

            modelBuilder.Entity<User>(entity =>
            {
                // Tabla y esquema
                entity.ToTable("users", "public");

                // Clave primaria
                entity.HasKey(u => u.Id)
                      .HasName("users_pkey");
                entity.Property(u => u.Id)
                      .HasColumnName("id")
                      .HasConversion(
                          v => v.ToString(),      // EF Core convierte Guid a string en memoria
                          v => Guid.Parse(v)      // EF Core convierte string a Guid para consultas
                      );
                // Columnas
                entity.Property(u => u.Id)
                      .HasColumnName("id")
                      .HasMaxLength(255)
                      .IsRequired();

                entity.Property(u => u.Active)
                      .HasColumnName("active")
                      .IsRequired();

                entity.Property(u => u.Email)
                      .HasColumnName("email")
                      .HasMaxLength(255)
                      .IsRequired();

                entity.Property(u => u.LastName)
                      .HasColumnName("lastname")
                      .HasMaxLength(255)
                      .IsRequired();

                entity.Property(u => u.Name)
                      .HasColumnName("name")
                      .HasMaxLength(255)
                      .IsRequired();

                entity.Property(u => u.Password)
                      .HasColumnName("password")
                      .HasMaxLength(255)
                      .IsRequired();

                entity.Property(u => u.Phone)
                      .HasColumnName("phone")
                      .HasMaxLength(255)
                      .IsRequired(false);

                entity.Property(u => u.Role)
                      .HasColumnName("role")
                      .HasMaxLength(255)
                      .IsRequired();

                entity.Property(u => u.Username)
                      .HasColumnName("username")
                      .HasMaxLength(255)
                      .IsRequired();

                // Índices únicos
                entity.HasIndex(u => u.Email)
                      .IsUnique()
                      .HasDatabaseName("uk6dotkott2kjsp8vw4d0m25fb7");

                entity.HasIndex(u => u.Username)
                      .IsUnique()
                      .HasDatabaseName("ukr43af9ap4edm43mmtq01oddj6");
            });

            modelBuilder.ApplyConfiguration(new UserSavedFilterConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}