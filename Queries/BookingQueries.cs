using CQRS_BookingSystem.Events;
using Edument.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRS_BookingSystem.Queries
{
    public class BookingQueries : ISubscribeTo<BookingMade>, ISubscribeTo<BookingCancelled>, ISubscribeTo<BookingCheckedOut>
    {
        private List<BookingMade> bookingsMade= new List<BookingMade>();
        private List<BookingCheckedOut> bookingsCheckedOut = new List<BookingCheckedOut>();
        private List<BookingCancelled> bookingsCancelled = new List<BookingCancelled>();
        public void Handle(BookingMade e)
        {
            lock (bookingsMade)
            {
                bookingsMade.Add(e);
            }
           
        }

        public List<BookingMade> GetTodoList()
        {
            lock (bookingsMade)
            {
                return bookingsMade;
            }
                
        }

        public void Handle(BookingCancelled e)
        {
            lock (bookingsCancelled)
            {
                var bookingToRemove = bookingsMade.Single(b => b.Id == e.Id);
                bookingsMade.Remove(bookingToRemove);
                bookingsCancelled.Add(e);
            }
        }

        public void Handle(BookingCheckedOut e)
        {
            lock (bookingsCheckedOut)
            {
                bookingsCheckedOut.Add(e);
            }
        }
    }
}
