﻿namespace CarsRentingSystem.Controllers
{
    using CarsRentingSystem.Data;
    using CarsRentingSystem.Infrastructure;
    using CarsRentingSystem.Models.Cars;
    using CarsRentingSystem.Services.Cars;
    using CarsRentingSystemData.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

    using System.Linq;

    public class CarsController : Controller
    {
        private readonly ICarService cars;
        private readonly CarRentDbContext data;
        public CarsController(CarRentDbContext data, ICarService cars)
        {
            this.cars = cars;
            this.data = data;
        }

        public IActionResult All([FromQuery] AllCarsQueryModel query)
        {
            var queryResult = this.cars.All(
                query.Brand,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                AllCarsQueryModel.CarsPerPage);

            var carBrands = this.cars.AllCarBrands();

            query.Brands = carBrands;
            query.TotalCars = queryResult.TotalCars;
            query.Cars = queryResult.Cars;

            return View(query);
        }

        [Authorize]
        public IActionResult Add()
        {
            if (!this.UserIsDealer())
            {
                return RedirectToAction(nameof(DealersController.Become), "Dealers");
            }

            return View(new CarFormModel
            {
                Categories = this.GetCarCategories()
            });

        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(CarFormModel car)
        {
            var dealerId = this.data
                .Dealers
                .Where(d => d.UserId == this.User.GetId())
                .Select(d => d.Id)
                .FirstOrDefault();

            if (dealerId == 0)
            {
                return RedirectToAction(nameof(DealersController.Become), "Dealers");
            }



            if (!this.data.Categories.Any(c => c.Id == car.CategoryId))
            {
                this.ModelState.AddModelError(nameof(car.CategoryId), $"Category {car.CategoryId} does not exist.");
            }

            if (!ModelState.IsValid)
            {
                car.Categories = GetCarCategories();

                return View(car);
            }

            var carData = new Car
            {
                Brand = car.Brand,
                Model = car.Model,
                Description = car.Description,
                ImageUrl = car.ImageUrl,
                Year = car.Year,
                CategoryId = car.CategoryId,
                DealerId = dealerId
            };

            this.data.Cars.Add(carData);

            this.data.SaveChanges();

            return RedirectToAction(nameof(All));
        }

        private bool UserIsDealer()
            => this.data
                .Dealers
                .Any(d => d.UserId == this.User.GetId());

        private IEnumerable<CarCategoryViewModel> GetCarCategories()
                => this.data
                .Categories
            .Select(c => new CarCategoryViewModel
            {
                Id = c.Id,
                Name = c.Name
            })
            .ToList();
    }

}

