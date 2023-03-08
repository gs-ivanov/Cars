﻿namespace CarsRentingSystem.Models.Cars
{
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;
    using global::CarsRentingSystem.Services.Cars;

    public class AllCarsQueryModel
    {
        public static int CarsPerPage = 3;

        public string Brand { get; init; }


        [Display(Name = "Search by text")]
        public string SearchTerm { get; init; }

        public CarSorting Sorting { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int TotalCars { get; set; }

        public IEnumerable<string> Brands { get; set; }

        public IEnumerable<CarServiceModel> Cars { get; set; }
    }
}
