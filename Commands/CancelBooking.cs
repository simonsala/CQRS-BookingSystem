using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRS_BookingSystem.Commands
{
    public class CancelBooking
    {
        public Guid Id { get; set; }
        public Guid BookingId { get; set; }

    }
}
