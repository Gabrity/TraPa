using Microsoft.EntityFrameworkCore;
using TraPa.DataAccess.EFCore.Public.Interfaces;
using TraPa.Entities.Impl.Classes;

namespace TraPa.DataAccess.EFCore.Impl.Data
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        public DatabaseContext(DbContextOptions<DbContext> options)
            : base(options)
        {
        }

        public DbSet<Traveler> Travelers { get; set; }
        public DbSet<TravelDate> TravelDates { get; set; }
        public DbSet<TravelerTravelDateReference> TravelerTravelDateReferences { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            modelBuilder.Entity<TravelDate>(entity =>
            {
                entity.ToTable("TravelDate", "dbo");

                entity
                    .HasMany(x => x.TravelerTravelDateReferences)
                    .WithOne(x => x.TravelDate)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasForeignKey(x => x.TravelDateId);
            });

            modelBuilder.Entity<Traveler>(entity =>
            {
                entity.ToTable("Traveler", "dbo");

                entity
                    .HasMany(x => x.TravelerTravelDateReferences)
                    .WithOne(x => x.Traveler)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasForeignKey(x => x.TravelerId);
            });
            
            modelBuilder.Entity<TravelerTravelDateReference>(entity =>
            {
                entity.ToTable("TravelerTravelDateReference", "dbo");
                
                entity.HasKey(e => new { e.TravelerId, e.TravelDateId });

                entity
                    .HasOne(x => x.TravelDate)
                    .WithMany(x => x.TravelerTravelDateReferences)
                    .HasForeignKey(x => x.TravelDateId);

                entity
                    .HasOne(x => x.Traveler)
                    .WithMany(x => x.TravelerTravelDateReferences)
                    .HasForeignKey(x => x.TravelerId);
            });
        }
    }
}