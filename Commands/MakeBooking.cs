using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRS_BookingSystem.Commands
{
    public class MakeBooking
    {
        public Guid Id { get; set; }
        public Guid BookingId { get; set; }

        public string Name { get; set; } = "Pedro";

        public string Phone { get; set; } = "0410636466";

        public string Email { get; set; } = "example@hotmail.com";

        public DateTime CheckInDate { get; set; }

        public DateTime CheckOutDate { get; set; }
    }
}
