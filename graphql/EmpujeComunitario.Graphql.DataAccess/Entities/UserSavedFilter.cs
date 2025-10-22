using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpujeComunitario.Graphql.DataAccess.Entities
{
    public class UserSavedFilter
    {
        public int Id{ get; set; }
        public string Name {get;set;}
        public string Filter {get;set;}
        public string Type { get;set;}
        public Guid UserId {get;set;}
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    public class UserSavedFilterConfiguration : IEntityTypeConfiguration<UserSavedFilter>
    {
        public void Configure(EntityTypeBuilder<UserSavedFilter> builder)
        {
            builder.ToTable("UserSavedFilters");

            builder.HasKey(f => f.Id);

            builder.Property(f => f.Id)
                .ValueGeneratedOnAdd(); 

            builder.Property(f => f.UserId)
                .IsRequired()
                .HasColumnType("text");

            builder.Property(f => f.Name)
                .IsRequired()
                .HasColumnType("text");

            builder.Property(f => f.Filter)
                .IsRequired()
                .HasColumnType("text");

            builder.Property(f => f.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("NOW()");

            builder.Property(f => f.Type)
                .IsRequired()
                .HasColumnType("text");

            builder.HasIndex(f => f.UserId)
                   .HasDatabaseName("IX_UserSavedFilters_UserId");
        }
    }
}
