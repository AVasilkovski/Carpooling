using Carpooling.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Carpooling.Data
{
    public class CarpoolingContext : DbContext
    {
        public CarpoolingContext(DbContextOptions<CarpoolingContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            modelbuilder.Entity<Travel>().HasOne(x => x.StartPointCity)
                                         .WithMany(x => x.TravelStartPoints)
                                         .OnDelete(DeleteBehavior.Restrict);

            modelbuilder.Entity<Travel>().HasOne(x => x.EndPointCity)
                                         .WithMany(x => x.TravelEndPoints)
                                         .OnDelete(DeleteBehavior.Restrict);

            modelbuilder.Entity<User>().HasMany(x => x.TravelsAsDriver)
                                       .WithOne(x => x.Driver)
                                       .OnDelete(DeleteBehavior.Restrict);

            modelbuilder.Entity<User>().HasMany(x => x.AppliedTravels)
                                       .WithMany(x => x.ApplyingPassengers);

            modelbuilder.Entity<User>().HasMany(x => x.TravelsAsPassenger)
                                       .WithMany(x => x.Passengers);

            modelbuilder.Entity<Feedback>().HasOne(x => x.UserFrom)
                                           .WithMany(x => x.GivenFeedbacks)
                                           .OnDelete(DeleteBehavior.Restrict);

            modelbuilder.Entity<Feedback>().HasOne(x => x.UserTo)
                                           .WithMany(x => x.RecievedFeedbacks)
                                           .OnDelete(DeleteBehavior.Restrict);
            modelbuilder.Seed();
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Travel> Travels { get; set; }

        public DbSet<Feedback> Feedbacks { get; set; }

        public DbSet<TravelTag> TravelTags { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<City> Cities { get; set; }
    }
}
