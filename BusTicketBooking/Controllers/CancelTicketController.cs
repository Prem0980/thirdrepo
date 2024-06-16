using BusTicketBooking.Models.DAO;
using BusTicketBooking.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace BusTicketBooking.Controllers
{
    public class CancelTicketController : Controller
    {
        // GET: CancelTicket
        DBOperations d = new DBOperations();
        // GET: CancelTicket
        HttpClient client = new HttpClient();
        [HttpGet]
        public ActionResult Cancel()
        {
            Passenger p = new Passenger();
            return View(p);
        }
        [HttpPost]
        public ActionResult Cancel(List<string> chb)
        {
            //string sc = null;
            //int not = 0;
            ScheduleDetails schedule = new ScheduleDetails();
            Passenger p = new Passenger();
            BTicket ticket = new BTicket();

            int count = chb.Count();

            string[] s = chb[0].Split(' ');
            ticket = d.cancelbookticket(s[1], count);

            UriBuilder builder1 = new UriBuilder("https://localhost:44337/api/BusBooking/updatePassenger/{tid}/{pid}/");//i have to change the link
            builder1.Query = $"tid=" + s[1] + "&pid=" + s[0];
            var json3 = JsonConvert.SerializeObject(s);
            var data5 = new StringContent(json3, Encoding.UTF8, "application/json");
            var task3 = client.PutAsync(builder1.Uri, data5);
            task3.Wait();

            var output = task3.Result;
            if (output.IsSuccessStatusCode)
            {
                var display = output.Content.ReadAsAsync<Passenger>();
                display.Wait();


            }
            if (ModelState.IsValid || ticket.CustomerDetail == null && ticket.Schedule == null && ticket.Passengers == null)
            {
                UriBuilder builder3 = new UriBuilder("https://localhost:44337/api/BusBooking/updateseatminus/{id}/{not}");//i have to change the link
                builder3.Query = $"id=" + ticket.ScheduleId + "&not=" + ticket.NumberOfTickets;
                var json2 = JsonConvert.SerializeObject(ticket);
                var data = new StringContent(json2, Encoding.UTF8, "application/json");
                var task2 = client.PutAsync(builder3.Uri, data);
                task2.Wait();

                var schedule20 = task2.Result;

                // i have to copy from this line to
                UriBuilder builder4 = new UriBuilder("https://localhost:44337/api/BusBooking/updatetickets/{id}/{count}");//i have to change the link//yes
                builder4.Query = $"id=" + ticket.TicketID + "&count=" + count;
                var json4 = JsonConvert.SerializeObject(ticket);
                var data55 = new StringContent(json4, Encoding.UTF8, "application/json");
                var task21 = client.PutAsync(builder4.Uri, data55);
                task21.Wait();

                var btkt = task21.Result;
                if (btkt.IsSuccessStatusCode)
                {
                    var display = btkt.Content.ReadAsAsync<BTicket>();
                    display.Wait();
                    ViewBag.msg = "Ticket cancelled sucessfully";
                }
                //this line i need to copy it 

                if (schedule20.IsSuccessStatusCode)
                {
                    var display1 = schedule20.Content.ReadAsAsync<BTicket>();
                    display1.Wait();

                    ViewBag.msg = "Ticket cancelled sucessfully";
                }
                
                else
                {
                    var display = schedule20.Content.ReadAsStringAsync();
                    display.Wait();
                    ViewBag.msg = display.Result;
                }
            }
            return View();
        }
    }
}