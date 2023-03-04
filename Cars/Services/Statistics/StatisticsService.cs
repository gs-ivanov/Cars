namespace Cars.Services.Statistics
{
    using System.Linq;
    using Cars.Data;

    public class StatisticsService : IStatisticsService
    {
        private readonly CarRentDbContext data;

        public StatisticsService(CarRentDbContext data)
            =>this.data = data;

        public StatisticsServiceModel Total()
        {
            var totalCars = this.data.Cars.Count();
            var totalUsers = this.data.Users.Count();

            return new StatisticsServiceModel
            {
                TotalCars = totalCars,
                TotalUsers = totalUsers
            };
        }
    }
}
