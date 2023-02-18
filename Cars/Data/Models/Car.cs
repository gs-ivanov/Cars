namespace Cars.Data.Models
{
    using System.Collections.Generic;

    public class Car
    {
        public int Id { get; init; }

        public string Brand { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; init; }
    }
}
