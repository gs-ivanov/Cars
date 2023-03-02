﻿namespace Cars.Controllers
{
    using System.Linq;
    using Cars.Data;
    using Cars.Data.Models;
    using Cars.Infrastructure;
    using Cars.Models.Dealers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class DealersController : Controller
    {
        private readonly CarRentDbContext data;

        public DealersController(CarRentDbContext data)
            => this.data = data;

        [Authorize]
        public IActionResult Become()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Become(BecomeDealerFormModel dealer)
        {
            var userId = this.User.GetId();
            var userIsAlreadyDealer = this.data
                .Dealers
                .Any(d => d.UserId == userId);

            if (userIsAlreadyDealer)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(dealer);
            }

            var dealerData = new Dealer
            {
                Name=dealer.Name,
                PhoneNumber=dealer.PhoneNumber,
                UserId=userId
            };

            this.data.Dealers.Add(dealerData);
            this.data.SaveChanges();

            return RedirectToAction("All", "Cars");
        }


    }
}