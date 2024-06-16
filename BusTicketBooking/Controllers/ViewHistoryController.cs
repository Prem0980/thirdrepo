using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusTicketBooking.Controllers
{
    public class ViewHistoryController : Controller
    {
        // GET: ViewHistory
        public ActionResult ViewHistory()
        {
            ViewBag.custId = long.Parse(Session["user"].ToString());
            return View();
        }
    }
}