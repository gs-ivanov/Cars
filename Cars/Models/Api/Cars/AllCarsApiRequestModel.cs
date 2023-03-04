﻿using Cars.Models.Cars;

namespace Cars.Models.Api.Cars
{
    // /api/cars?carsPerPage=5
    public class AllCarsApiRequestModel
    {
        public string Brand { get; init; }

        public string SearchTerm { get; init; }

        public CarSorting Sorting { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int CarsPerPage { get; init; } = 10;
    }
}
