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
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Donations
            modelBuilder.Entity<Donation>(entity =>
            {
                // Tabla y esquema
                entity.ToTable("donations", "public");

                // Clave primaria
                entity.HasKey(d => d.Id)
                      .HasName("donations_pkey"); // nombre del constraint

                // Columnas
                entity.Property(d => d.Id)
                      .HasColumnName("id")
                      .HasMaxLength(255)
                      .IsRequired();

                entity.Property(d => d.Category)
                      .HasColumnName("category")
                      .HasMaxLength(255)
                      .IsRequired();

                entity.Property(d => d.CreationDate)
                      .HasColumnName("creation_date")
                      .IsRequired();

                entity.Property(d => d.Description)
                      .HasColumnName("description")
                      .HasMaxLength(255)
                      .IsRequired();

                entity.Property(d => d.IsDeleted)
                      .HasColumnName("is_deleted")
                      .IsRequired();

                entity.Property(d => d.ModificationDate)
                      .HasColumnName("modification_date")
                      .IsRequired(false);

                entity.Property(d => d.Quantity)
                      .HasColumnName("quantity")
                      .IsRequired();

                entity.Property(d => d.CreationUserId)
                      .HasColumnName("creation_user_id")
                      .HasMaxLength(255)
                      .IsRequired();

                entity.Property(d => d.ModificationUserId)
                      .HasColumnName("modification_user_id")
                      .HasMaxLength(255)
                      .IsRequired(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                // Tabla y esquema
                entity.ToTable("users", "public");

                // Clave primaria
                entity.HasKey(u => u.Id)
                      .HasName("users_pkey");

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

        }
    }
}
