using GStore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace GStore.Context
{
    public class GoogleStoreContext : IdentityDbContext<User>
    {
        public GoogleStoreContext() { }

        public GoogleStoreContext(DbContextOptions<GoogleStoreContext> options)
           : base(options) 
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Store;Username=postgres;Password=admin");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });
            });

            modelBuilder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });
            });

            modelBuilder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("users_pkey");

                entity.ToTable("users");

                entity.HasIndex(e => e.Email, "users_email_key").IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");
                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email");
                entity.Property(e => e.Password)
                    .HasMaxLength(30)
                    .HasColumnName("password");
                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .HasColumnName("name");
                entity.Property(e => e.Phone)
                    .HasMaxLength(30)
                    .HasColumnName("phone");
                entity.Property(e => e.Country)
                    .HasMaxLength(30)
                    .HasColumnName("country");
                entity.Property(e => e.City)
                    .HasMaxLength(30)
                    .HasColumnName("city");
                entity.Property(e => e.Address)
                    .HasMaxLength(30)
                    .HasColumnName("adress");
            });
        }
    }
}
