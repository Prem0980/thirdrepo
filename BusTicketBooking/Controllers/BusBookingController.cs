using BusTicketBooking.Models;
using BusTicketBooking.Models.DAO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace BusTicketBooking.Controllers
{
    public class BusBookingController : Controller
    {
        // GET: BusBooking
        HttpClient client = new HttpClient();
        BTicket b = new BTicket();
        // GET: BusBooking
        DBOperations db = new DBOperations();

        public ActionResult BusBooking()
        {

            ViewBag.FromLocation = db.getFromLocation();
            ViewBag.ToLocation = db.getToLocation();
            ViewBag.Time = db.getTime();
            ViewBag.BusName = db.getBus();
            return View();
        }
        //[HttpGet]
        public ActionResult BusBooking1(string busRadio,string costValue)
        {
            ScheduleDetails s=new ScheduleDetails();
            b = new BTicket();
            Random random = new Random();
            String Id = "T" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString("d2") + random.Next(0000, 9999);

            b.TicketID = Id;
            b.ScheduleId = busRadio;
            b.DateOfBooking=DateTime.Now;
            s.Cost= int.Parse(costValue);
            ViewBag.CostValue = s.Cost;
            return View(b);
        }

        //[HttpPost]
        public ActionResult BusBooking2(BTicket b)
        {
            //ScheduleDetails s = new ScheduleDetails();
            string costValue = Request.Form["costValue"];
            ViewBag.cost= int.Parse(costValue);
            List<Passenger> plist = new List<Passenger>();
            Passenger p = new Passenger();
            b.Passengers.Status = "Booked";
                p.PassengerID = b.Passengers.PassengerID;
                p.Name = b.Passengers.Name;
                p.Age = b.Passengers.Age;
                p.Gender = b.Passengers.Gender;
                p.Status = b.Passengers.Status;
                p.TicketId = b.TicketID;
            if (ModelState.IsValid || b.CustomerDetail == null && b.Schedule == null)
            {
                UriBuilder builder = new UriBuilder("https://localhost:44337/api/BusBooking/TicketBooking/");
                var json = JsonConvert.SerializeObject(b);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var task = client.PostAsync(builder.Uri, data);
                task.Wait();
                var output = task.Result;
                UriBuilder builder1 = new UriBuilder("https://localhost:44337/api/BusBooking/PassengerBooking/");
                var json1 = JsonConvert.SerializeObject(p);
                var data1 = new StringContent(json1, Encoding.UTF8, "application/json");
                var task1 = client.PostAsync(builder1.Uri, data1);
                task1.Wait();

                var Passenger1 = task1.Result;
                if (output.IsSuccessStatusCode)
                {
                    ViewBag.msg1 = "Your Ticket is Booked SucessFully";

                }
                if (Passenger1.IsSuccessStatusCode)
                {
                    var display = output.Content.ReadAsAsync<Passenger>();
                    display.Wait();
                }


                else
                {
                    var display = output.Content.ReadAsStringAsync();
                    display.Wait();
                    string j = display.Result;
                    dynamic data2 = JsonConvert.DeserializeObject<dynamic>(j);
                    ViewBag.msg = data2.Message;
                }
            }
            return View(b);
        }

    }
}