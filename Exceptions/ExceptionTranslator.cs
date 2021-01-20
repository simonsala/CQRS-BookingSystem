using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRS_BookingSystem.Exceptions
{
    public class ExceptionTranslator
    {

        public static string TranslateException(Exception e)
        {
            switch(e.GetType().Name)
            {
                case "UnexistingBooking" : return "Opps! Booking Id does not exist!";
                case "CanNotCheckOutTwice": return "Opps! This Booking Id has been checked out before!";
                default : return "Opps! Booking cannot be cancelled as it has been checked out!";
            }
        }

    }


}
