using CQRS_BookingSystem.Aggregates;
using Edument.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using CQRS_BookingSystem.Commands;
using CQRS_BookingSystem.Events;
using CQRS_BookingSystem.Exceptions;

namespace CQRS_BookingSystem.Tests
{
    [TestFixture]
    public class BookingTests : BDDTest<BookingAggregate>
    {
        private MakeBooking bookingOne;
        private MakeBooking bookingTwo;

        [SetUp]
        public void Setup()
        {
            var bookingOneId = Guid.NewGuid();
            var bookingTwoId = Guid.NewGuid();
            bookingOne = new MakeBooking()
            {
                Id = bookingOneId,
                BookingId = bookingOneId,
                Name = "Simon",
                Phone = "456464646",
                Email = "cardoso@gmail.com",
                CheckInDate = new DateTime(),
                CheckOutDate = new DateTime(),
            };

            bookingTwo = new MakeBooking()
            {
                Id = bookingTwoId,
                BookingId = bookingTwoId,
                Name = "Alejandro",
                Phone = "Cristian",
                Email = "carmolino@gmail.com",
                CheckInDate = new DateTime(),
                CheckOutDate = new DateTime()
            };


        }

        [Test]
        public void CanMakeABooking()
        {
            Test(
            Given(),
            When( bookingOne), 
            Then(new BookingMade()
            {

                Id = bookingOne.Id,
                BookingId = bookingOne.BookingId,
                Name = bookingOne.Name,
                Email = bookingOne.Email,
                Phone = bookingOne.Phone,
                CheckInDate = bookingOne.CheckInDate,
                CheckOutDate = bookingOne.CheckOutDate

            }));
        }

        [Test]
        public void CanCancelBooking()
        {
            Test(
            Given(new BookingMade()
            {
                Id = bookingTwo.Id,
                BookingId = bookingTwo.BookingId,
                Name = bookingTwo.Name,
                Email = bookingTwo.Email,
                Phone = bookingTwo.Phone,
                CheckInDate = bookingTwo.CheckInDate,
                CheckOutDate = bookingTwo.CheckOutDate
            }),
            When(new CancelBooking()
            {
                Id = bookingTwo.BookingId,
                BookingId = bookingTwo.BookingId
            }),
            Then(new BookingCancelled()
            {
                Id = bookingTwo.BookingId,
                BookingId = bookingTwo.BookingId

            }));
        }

        [Test]
        public void CanNotCancelUnexistingBooking()
        {
            var nonExistingBookingId = Guid.NewGuid();
            Test(
            Given(new BookingMade()
            {
                Id = bookingTwo.Id,
                BookingId = bookingTwo.BookingId,
                Name = bookingTwo.Name,
                Email = bookingTwo.Email,
                Phone = bookingTwo.Phone,
                CheckInDate = bookingTwo.CheckInDate,
                CheckOutDate = bookingTwo.CheckOutDate
            }),
            When(new CancelBooking()
            {
                Id = nonExistingBookingId,
                BookingId = nonExistingBookingId
            }),
            ThenFailWith<UnexistingBooking>());
        }

        [Test]
        public void CanNotCancelUponCheckOut()
        {
            Test(
            Given(new BookingMade()
            {
                Id = bookingTwo.Id,
                BookingId = bookingTwo.BookingId,
                Name = bookingTwo.Name,
                Email = bookingTwo.Email,
                Phone = bookingTwo.Phone,
                CheckInDate = bookingTwo.CheckInDate,
                CheckOutDate = bookingTwo.CheckOutDate
            }, new BookingCheckedOut()
            {
                Id = bookingTwo.Id,
                BookingId = bookingTwo.BookingId,
                CheckOutDate = new DateTime()
            }),
            When(new CancelBooking()
            {
                Id = bookingTwo.Id,
                BookingId = bookingTwo.Id
            }),
            ThenFailWith<CanNotCancelUponCheckOut>()); ;
        }


        [Test]
        public void CanCheckOutBooking()
        {
            Test(
            Given(new BookingMade()
            {
                Id = bookingTwo.Id,
                BookingId = bookingTwo.BookingId,
                Name = bookingTwo.Name,
                Email = bookingTwo.Email,
                Phone = bookingTwo.Phone,
                CheckInDate = bookingTwo.CheckInDate,
                CheckOutDate = bookingTwo.CheckOutDate
            }),
            When(new CheckOutBooking()
            {
                Id = bookingTwo.BookingId,
                BookingId = bookingTwo.BookingId,
                CheckOutDate = bookingTwo.CheckOutDate
            }),
            Then(new BookingCheckedOut()
            {
                Id = bookingTwo.BookingId,
                BookingId = bookingTwo.BookingId,
                CheckOutDate = bookingTwo.CheckOutDate

            })); ;
        }

        [Test]
        public void CanNotCheckOutTwice()
        {
           Test(
           Given(new BookingMade()
           {
               Id = bookingTwo.Id,
               BookingId = bookingTwo.BookingId,
               Name = bookingTwo.Name,
               Email = bookingTwo.Email,
               Phone = bookingTwo.Phone,
               CheckInDate = bookingTwo.CheckInDate,
               CheckOutDate = bookingTwo.CheckOutDate
           }, new BookingCheckedOut()
           {
               Id = bookingTwo.BookingId,
               BookingId = bookingTwo.BookingId,
               CheckOutDate = bookingTwo.CheckOutDate

           }),
           When(new CheckOutBooking()
           {
               Id = bookingTwo.BookingId,
               BookingId = bookingTwo.BookingId,
               CheckOutDate = bookingTwo.CheckOutDate
           }),
           ThenFailWith<CanNotCheckOutTwice>()); ;
        }

        [Test]
        public void CanNotCheckOutlUnexistingBooking()
        {
            var nonExistingBookingId = Guid.NewGuid();
            Test(
            Given(new BookingMade()
            {
                Id = bookingTwo.Id,
                BookingId = bookingTwo.BookingId,
                Name = bookingTwo.Name,
                Email = bookingTwo.Email,
                Phone = bookingTwo.Phone,
                CheckInDate = bookingTwo.CheckInDate,
                CheckOutDate = bookingTwo.CheckOutDate
            }),
            When(new CheckOutBooking()
            {
                Id = nonExistingBookingId,
                BookingId = nonExistingBookingId,
                CheckOutDate = bookingTwo.CheckOutDate
            }),
            ThenFailWith<UnexistingBooking>());
        }

    }
}
