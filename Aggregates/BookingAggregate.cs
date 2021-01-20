using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRS_BookingSystem.Commands;
using CQRS_BookingSystem.Events;
using CQRS_BookingSystem.Exceptions;
using Edument.CQRS;

namespace CQRS_BookingSystem.Aggregates
{
    public class BookingAggregate : Aggregate,
        IHandleCommand<MakeBooking>,
        IHandleCommand<CheckOutBooking>,
        IHandleCommand<CancelBooking>,
        IApplyEvent<BookingMade>,
        IApplyEvent<BookingCheckedOut>,
        IApplyEvent<BookingCancelled>
    {
        private List<BookingMade> bookingsMade = new List<BookingMade>();
        private List<BookingCheckedOut> bookingsCheckedOut = new List<BookingCheckedOut>();
        private List<BookingCancelled> bookingsCancelled = new List<BookingCancelled>();
     

        public void Apply(BookingMade e)
        {
            bookingsMade.Add(e);
        }

        public void Apply(BookingCancelled e)
        {
            var bookingToRemove = bookingsMade.Single(b => b.Id == e.Id);
            bookingsMade.Remove(bookingToRemove);
            bookingsCancelled.Add(e);
        }

        public void Apply(BookingCheckedOut e)
        {
            bookingsCheckedOut.Add(e);
        }

        public IEnumerable Handle(MakeBooking c)
        {
            yield return new BookingMade
            {
                Id = c.Id,
                BookingId = c.BookingId,
                Name = c.Name,
                Phone = c.Phone,
                Email = c.Email,
                CheckInDate = c.CheckInDate,
                CheckOutDate = c.CheckOutDate
            };
        }

        public IEnumerable Handle(CheckOutBooking c)
        {
            if (!DoesBookingExist(c.BookingId))
            {
                throw new UnexistingBooking();
            }
            else if (( from booking in bookingsCheckedOut
                      where booking.BookingId == c.BookingId
                      select booking).Count() == 1)
            {
                throw new CanNotCheckOutTwice();
            }

            yield return new BookingCheckedOut()
            {
                Id = c.Id,
                BookingId = c.BookingId,
                CheckOutDate = c.CheckOutDate
            };
        }

        public IEnumerable Handle(CancelBooking c)
        {

            if (!DoesBookingExist(c.BookingId))
            {
                throw new UnexistingBooking();
            }
            foreach (BookingCheckedOut b in bookingsCheckedOut)
            {
                if (c.BookingId == b.BookingId)
                    throw new CanNotCancelUponCheckOut();
            }

            yield return new BookingCancelled
            {
                Id = c.Id,
                BookingId = c.BookingId               
            };
        }


        public bool DoesBookingExist(Guid id)
        {
            return ((from booking in bookingsMade
                   where booking.BookingId == id
                   select booking).Count() == 1);
        }
    }
}
