using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusTicketBooking.Controllers
{
    public class PassengerController : Controller
    {
        // GET: Passenger
        public ActionResult Passenger()
        {
            return View();
        }
    }
}