using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRS_BookingSystem.Events
{
    public class BookingCheckedOut
    {

        public Guid Id { get; set; }
        public Guid BookingId { get; set; }

        public DateTime CheckOutDate { get; set; }
    }
}
