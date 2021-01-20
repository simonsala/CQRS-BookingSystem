using CQRS_BookingSystem.Aggregates;
using CQRS_BookingSystem.Queries;
using Edument.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRS_BookingSystem.Models
{
    public class Domain
    {
        public  MessageDispatcher Dispatcher;
        public  BookingQueries BookingQuerier;

        public Domain()
        {
            Setup();
        }

        public void Setup()
        {
            Dispatcher = new MessageDispatcher(new InMemoryEventStore());

            Dispatcher.ScanInstance(new BookingAggregate());


            BookingQuerier = new BookingQueries();
            Dispatcher.ScanInstance(BookingQuerier);

        }
    }
}
