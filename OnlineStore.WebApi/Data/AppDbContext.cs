using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OnlineStore.WebApi.Models;

namespace OnlineStore.WebApi.Data
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity => entity.Property(c => c.Category)
                .HasConversion(new EnumToStringConverter<ProductCategory>()));

            modelBuilder.Entity<AppUser>(entity => entity.Property(m => m.Id));
            modelBuilder.Entity<AppUser>(entity => entity.Property(m => m.NormalizedEmail).HasMaxLength(85));
            modelBuilder.Entity<AppUser>(entity => entity.Property(m => m.NormalizedUserName).HasMaxLength(85));

            modelBuilder.Entity<AppRole>(entity => entity.Property(m => m.Id));
            modelBuilder.Entity<AppRole>(entity => entity.Property(m => m.NormalizedName).HasMaxLength(85));

            modelBuilder.Entity<IdentityUserLogin<Guid>>(entity => entity.Property(m => m.LoginProvider).HasMaxLength(85));
            modelBuilder.Entity<IdentityUserLogin<Guid>>(entity => entity.Property(m => m.ProviderKey).HasMaxLength(85));
            modelBuilder.Entity<IdentityUserLogin<Guid>>(entity => entity.Property(m => m.UserId).HasMaxLength(85));
            modelBuilder.Entity<IdentityUserRole<Guid>>(entity => entity.HasNoKey());
            modelBuilder.Entity<IdentityUserRole<Guid>>(entity => entity.Property(m => m.RoleId).HasMaxLength(85));

            modelBuilder.Entity<IdentityUserToken<Guid>>(entity => entity.Property(m => m.UserId).HasMaxLength(85));
            modelBuilder.Entity<IdentityUserToken<Guid>>(entity => entity.Property(m => m.LoginProvider).HasMaxLength(85));
            modelBuilder.Entity<IdentityUserToken<Guid>>(entity => entity.Property(m => m.Name).HasMaxLength(85));
            modelBuilder.Entity<IdentityUserToken<Guid>>(entity => entity.HasNoKey());

            modelBuilder.Entity<IdentityUserClaim<Guid>>(entity => entity.Property(m => m.Id).HasMaxLength(85));
            modelBuilder.Entity<IdentityUserClaim<Guid>>(entity => entity.Property(m => m.UserId).HasMaxLength(85));
            modelBuilder.Entity<IdentityRoleClaim<Guid>>(entity => entity.HasNoKey());
            modelBuilder.Entity<IdentityRoleClaim<Guid>>(entity => entity.Property(m => m.RoleId).HasMaxLength(85));

            base.OnModelCreating(modelBuilder);
        }
    }
}
