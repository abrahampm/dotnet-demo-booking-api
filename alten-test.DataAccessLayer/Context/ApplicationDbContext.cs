using alten_test.Core.Models;
using alten_test.Core.Models.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace alten_test.DataAccessLayer.Context
{
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser>
    {
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Room> Rooms { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reservation>()
                .HasOne(p => p.Room)
                .WithMany(b => b.Reservations)
                .HasForeignKey(p => p.RoomId);
            modelBuilder.Entity<Room>()
                .Property(r => r.Status)
                .HasConversion<int>();
            modelBuilder.Entity<Room>()
                .Property(r => r.Type)
                .HasConversion<int>();
            
            // Fix maxlength issues with Identity and MySQL
            modelBuilder.Entity<ApplicationUser>(e => e.Property(m => m.Id).HasMaxLength(85));
            modelBuilder.Entity<IdentityRole>(e => e.Property(m => m.Id).HasMaxLength(85));
            
            modelBuilder.Entity<IdentityUserClaim<string>>(e => e.Property(m => m.Id).HasMaxLength(85));
            modelBuilder.Entity<IdentityRoleClaim<string>>(e => e.Property(m => m.Id).HasMaxLength(85));

            modelBuilder.Entity<IdentityUserLogin<string>>(e => e.Property(m => m.LoginProvider).HasMaxLength(85));
            modelBuilder.Entity<IdentityUserLogin<string>>(e => e.Property(m => m.ProviderKey).HasMaxLength(85));

            modelBuilder.Entity<IdentityUserToken<string>>(e => e.Property(m => m.LoginProvider).HasMaxLength(85));
            modelBuilder.Entity<IdentityUserToken<string>>(e => e.Property(m => m.Name).HasMaxLength(85));
            
            base.OnModelCreating(modelBuilder);
        }
    }
}