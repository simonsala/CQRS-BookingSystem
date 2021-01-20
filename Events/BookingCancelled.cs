using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRS_BookingSystem.Events
{
    public class BookingCancelled
    {

        public Guid Id { get; set; }
        public Guid BookingId { get; set; }

    }
}
