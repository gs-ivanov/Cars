namespace Cars.Controllers
{
    using Cars.Data;
    using Cars.Models.Api;
    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;

    [ApiController]
    [Route("api/statistics")]
    public class StatisticsApiController : ControllerBase
    {
        private readonly CarRentDbContext data;

        public StatisticsApiController(CarRentDbContext data)
            => this.data = data;

        [HttpGet]
        [EnableCors]
        public StatisticsResponseModel GetStatistics()
        {
            var totalCars = this.data.Cars.Count();
            var totalUsers = this.data.Users.Count();

            return new StatisticsResponseModel
            {
                TotalCars = totalCars,
                TotalUsers = totalUsers,
                TotalRents = 0
            };
        }
    }
}
