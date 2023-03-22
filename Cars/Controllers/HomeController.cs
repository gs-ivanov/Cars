namespace CarRentingSystem.Controllers
{
    using System.Linq;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using CarRentingSystem.Data;
    using CarRentingSystem.Models;
    using CarRentingSystem.Models.Home;
    using CarRentingSystem.Services.Statistics;
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;


    public class HomeController : Controller
    {
        private readonly IStatisticsService statistics;
        private readonly IConfigurationProvider mapper;
        private readonly CarRentingDbContext data;


        public HomeController(
            CarRentingDbContext data,
            IMapper mapper,
            IStatisticsService statistics
            )
        {
            this.data = data;
            this.mapper = mapper.ConfigurationProvider;
            this.statistics = statistics;
        }

        public IActionResult Index()
        {
            var cars = this.data
                .Cars
                .OrderByDescending(c => c.Id)
                .ProjectTo<CarIndexViewModel>(this.mapper)
                //.Select(c => new CarIndexViewModel
                //{
                //    Id = c.Id,
                //    Brand = c.Brand,
                //    Model = c.Model,
                //    Year = c.Year,
                //    ImageUrl = c.ImageUrl
                //})
                .Take(3)
               .ToList();

            var totalStatistics = this.statistics.Total();

            return View(new IndexViewModel
            {
                TotalCars = totalStatistics.TotalCars,
                TotalUsers = totalStatistics.TotalUsers,
                Cars = cars
            }); 
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
