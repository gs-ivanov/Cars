namespace Cars.Controllers
{
    using Cars.Data;
    using Cars.Models;
    using Cars.Models.Home;
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;
    using System.Linq;


    public class HomeController : Controller
    {
        private readonly CarRentDbContext data;

        public HomeController(CarRentDbContext data)
            => this.data = data;

        public IActionResult Index()
        {
            var totalCars = this.data.Cars.Count();
            var totalUsers = this.data.Users.Count();

            var cars = this.data
                .Cars
                .OrderByDescending(c => c.Id)
                .Select(c => new CarIndexViewModel
                {
                    Id = c.Id,
                    Brand = c.Brand,
                    Model = c.Model,
                    Year = c.Year,
                    ImageUrl = c.ImageUrl
                })
                .Take(3)
               .ToList();

            return View(new IndexViewModel
            {
                TotalCars = totalCars,
                TotalUsers = totalUsers,
                Cars = cars
            }); ;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
