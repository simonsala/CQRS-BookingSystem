using CQRS_BookingSystem.Commands;
using CQRS_BookingSystem.Events;
using CQRS_BookingSystem.Exceptions;
using CQRS_BookingSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CQRS_BookingSystem.Controllers
{
    public class PortalController : Controller
    {
        private readonly ILogger<PortalController> _logger;
        private Domain _domain;

        public PortalController(ILogger<PortalController> logger, Domain domain)
        {
            _logger = logger;
            _domain = domain;
    
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Book(MakeBooking makeBooking)
        {
            if (ModelState.IsValid)
            {
                makeBooking.Id = Guid.NewGuid();
                makeBooking.BookingId = makeBooking.Id;
                _domain.Dispatcher.SendCommand(makeBooking);
              
                return RedirectToAction("Booking", new { result = true });
            }
            return RedirectToAction("Booking", new { result = false});
        }

        public IActionResult Booking(bool result)
        {
           
            OperationViewModel model = new OperationViewModel{
                Result = result
            };

            return View(model);
        }

        public IActionResult BookingCancellation(bool result, string errorMessage, bool inProcess)
        {
            OperationViewModel model = new OperationViewModel
            {
                Result = result,
                InProcess = inProcess,
                ErrorMessage = errorMessage
            };

            return View(model);
        }

        public IActionResult CancelBooking(CancelBooking cancelBooking)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    cancelBooking.Id = cancelBooking.BookingId;
                    _domain.Dispatcher.SendCommand(cancelBooking);
                    return RedirectToAction("BookingCancellation", new { result = true, inProcess = true, errorMessage = "" });
                }
                catch (Exception e)
                {
                    _logger.LogInformation(ExceptionTranslator.TranslateException(e));
                    return RedirectToAction("BookingCancellation", new { result = false, inProcess = true, errorMessage = ExceptionTranslator.TranslateException(e) });
                }
            }
            return RedirectToAction("BookingCancellation", new { result = false, inProcess = true, errorMessage = "Invalid data"  });
        }

        public IActionResult BookingCheckOut(bool result, string errorMessage, bool inProcess)
        {
            OperationViewModel model = new OperationViewModel
            {
                Result = result,
                InProcess = inProcess,
                ErrorMessage = errorMessage
            };

            return View(model);
        }

        public IActionResult CheckOutBooking(CheckOutBooking checkOutBooking)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    checkOutBooking.Id = checkOutBooking.BookingId;
                    _domain.Dispatcher.SendCommand(checkOutBooking);
                    return RedirectToAction("BookingCheckOut", new { result = true, inProcess = true, errorMessage = "" });
                }
                catch (Exception e)
                {
                    _logger.LogInformation(ExceptionTranslator.TranslateException(e));
                    return RedirectToAction("BookingCheckOut", new { result = false, inProcess = true, errorMessage = ExceptionTranslator.TranslateException(e) });
                }
            }
            return RedirectToAction("BookingCheckOut", new { result = false, inProcess = true, errorMessage = "Invalid data" });
        }


        public IActionResult BookingList()
        {
            return View(_domain.BookingQuerier.GetTodoList());
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
