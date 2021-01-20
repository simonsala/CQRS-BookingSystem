using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRS_BookingSystem.Models
{
    public class OperationViewModel
    {
        public bool Result { get; set; }

        public bool InProcess { get; set;}

        public string ErrorMessage { get; set; }
    }
}
