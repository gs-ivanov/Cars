namespace Cars.Controllers.API
{
    using Cars.Data;
    using Cars.Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    [ApiController]
    [Route("api/cars")]
    public class CarsApiController : ControllerBase
    {
        private readonly CarRentDbContext data;

        public CarsApiController(CarRentDbContext data)
            => this.data = data;
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
        [HttpGet]
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

        [HttpGet]
        [Route("{id}")]
        public object GetDetails(int id)
        {
            var car= this.data.Cars.Find(id);
            if (car==null)
            {
                return Ok($"Not Found car Id == {id}");
            }

            return car;
        }
    }
}
