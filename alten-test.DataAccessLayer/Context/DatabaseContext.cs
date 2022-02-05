using alten_test.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace alten_test.DataAccessLayer.Context
{
    public class DatabaseContext: DbContext
    {
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Room> Rooms { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reservation>()
                .HasOne(p => p.Contact)
                .WithMany(b => b.Reservations)
                .HasForeignKey(p => p.ContactId);
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
        }
    }
}