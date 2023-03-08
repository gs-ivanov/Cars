namespace CarsRentingSystem.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using CarsRentingSystem.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using CarsRentingSystemData.Models;

    public class CarRentDbContext : IdentityDbContext
    {
        public CarRentDbContext(DbContextOptions<CarRentDbContext> options)
            : base(options)
        {
        }

        public DbSet<Car> Cars { get; init; }

        public DbSet<Category> Categories { get; init; }

        public DbSet<Dealer> Dealers { get; init; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Car>()
                .HasOne(c => c.Category)
                .WithMany(c => c.Cars)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Car>()
                .HasOne(c => c.Dealer)   // Една кола има много на брой дилъри
                .WithMany(d => d.Cars)               //Дилърите имат много на брой коли
                .HasForeignKey(c => c.DealerId)       //ForeignKey  е в колата и се казва DealerId
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Dealer>()
                .HasOne<IdentityUser>()
                .WithOne()                   //.WithOne<Dealer>()   
                .HasForeignKey<Dealer>(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict);


            base.OnModelCreating(builder);
        }
    }
}
