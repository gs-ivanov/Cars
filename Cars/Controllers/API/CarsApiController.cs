namespace Cars.Controllers.API
{
    using Cars.Data;
    using Cars.Data.Models;
    using Cars.Models;
    using Cars.Models.Api.Cars;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    //using System.Collections.Generic;

    [ApiController]
    [Route("api/cars")]
    public class CarsApiController : ControllerBase
    {
        private readonly CarRentDbContext data;

        public CarsApiController(CarRentDbContext data)
            => this.data = data;

        [HttpGet]
        public ActionResult<AllCarsApiResponseModel> All([FromQuery] AllCarsApiRequestModel query)
        {
            var carsQuery = this.data.Cars.AsQueryable();


            if (!string.IsNullOrWhiteSpace(query.Brand))
            {
                carsQuery = carsQuery.Where(c => c.Brand == query.Brand);
            }

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                carsQuery = carsQuery.Where(
                    c => (c.Brand + " " + c.Model).ToLower().Contains(query.SearchTerm.ToLower()) ||
                    c.Description.ToLower().Contains(query.SearchTerm.ToLower()));
            }

            carsQuery = query.Sorting switch
            {
                CarSorting.Year => carsQuery.OrderByDescending(c => c.Year),
                CarSorting.BrandAndModel => carsQuery.OrderBy(c => c.Brand).ThenBy(c => c.Model),
                CarSorting.DateCreated or _ => carsQuery.OrderByDescending(c => c.Id)
            };

            var totalCars = carsQuery.Count();

            var cars = carsQuery
                .Skip((query.CurrentPage - 1) * query.CarsPerPage)
                .Take(query.CarsPerPage)
                .Select(c => new CarResponseModel
                {
                    Id = c.Id,
                    Brand = c.Brand,
                    Model = c.Model,
                    Year = c.Year,
                    ImageUrl = c.ImageUrl,
                    Category = c.Category.Name
                })
                .ToList();

            return new AllCarsApiResponseModel
            {
                CurrentPage = query.CurrentPage,
                CarsPerPage = query.CarsPerPage,
                TotalCars = totalCars,
                Cars = cars
            };

            return Ok(query);

        }




        // Variant A
        //[HttpGet]
        //public IEnumerable GerCar()
        //{
        //    return this.data.Cars.ToList();
        //}

        // Variant B
        //[HttpGet]
        //public ActionResult<IEnumerable<Car>> GerCar()
        //{
        //    var cars = this.data.Cars.ToList();

        //    if (!cars.Any())
        //    {
        //        return NotFound();
        //    }
        //    return cars;
        //}

        // Variant  C
        //[HttpGet]
        //public ActionResult<Car> GerCar()
        //{
        //    var car = this.data.Cars.FirstOrDefault();

        //    if (car == null)
        //    {
        //        return NotFound();
        //    }
        //    return car;
        //}

        // Variant  D
        //[HttpGet]
        public IActionResult GerCar()
        {
            var car = this.data.Cars.FirstOrDefault();

            if (car == null)
            {
                return NotFound();
            }
            return Ok(car);
        }

        [HttpPost]
        public IActionResult SaveCar(Car car)
        {
            return Ok($"OK! {car.Brand}");
        }

        //[HttpGet]
        //[Route("{id}")]
        //public object GetDetails(int id)
        //{
        //    var car = this.data.Cars.Find(id);
        //    if (car == null)
        //    {
        //        return Ok($"Not Found car Id == {id}");
        //    }

        //    return car;
        //}
    }
}
