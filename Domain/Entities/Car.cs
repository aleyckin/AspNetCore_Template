using Domain.Entities.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Car : IId
    {
        public string Make { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int Year { get; set; }
        public long Price { get; set; }
        public string Color { get; set; } = string.Empty;
        public DateTime DateAdded { get; set; } = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
        public DateTime? DateSold { get; set; }
        public bool IsSold { get; set; } = false;
        public Guid? UserId { get; set; }
        public User? Owner { get; set; }
    }
}
