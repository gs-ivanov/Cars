namespace CarsRentingSystem.Data.Models
{
    using CarsRentingSystemData.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Category;
    public class Category
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        public IEnumerable<Car> Cars { get; init; } = new List<Car>();


    }
}
