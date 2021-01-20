using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRS_BookingSystem.Models
{
    public class BookingViewModel
    {
        public string Name { get; set; }

        public int Phone { get; set; }

        public string Email { get; set; }

        public DateTime CheckInDate { get; set; }

        public DateTime CheckOutDate { get; set; } 

    }
}
