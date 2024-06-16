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

namespace BusTicketBooking.Controllers
{
    public class ScheduleController : Controller
    {
        DBOperations d=new DBOperations();
        // GET: Schedule
        HttpClient client=new HttpClient();
        [HttpGet]
        public ActionResult AddNewSchedule()
        {
            
            ScheduleDetails S = new ScheduleDetails();
            Random random = new Random();
            String Id = "S"+DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString("d2") + random.Next(0000, 9999);
            S.ScheduleID = Id;
            ViewBag.busids = d.getbusid();
            ViewBag.routeids = d.getrouteid();
            
            return View(S);
        }
        [HttpPost]
        public ActionResult AddNewSchedule(ScheduleDetails sd,string sch)
        {
            sd.ScheduleID = sch;
            if (ModelState.IsValid)
            {
                UriBuilder builder = new UriBuilder("https://localhost:44337/api/BusBooking/addSchedule/");
                var json = JsonConvert.SerializeObject(sd);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var task = client.PostAsync(builder.Uri, data);
                task.Wait();
                var output = task.Result;
                if (output.IsSuccessStatusCode)
                {
                    ViewBag.msg1 = "Schedule Added Successfully";
                }
                else
                {
                    var display = output.Content.ReadAsStringAsync();
                    display.Wait();
                    string j = display.Result;
                    dynamic data1 = JsonConvert.DeserializeObject<dynamic>(j);
                    ViewBag.msg = data1.Message;
                }
            }
            return View(sd);
            
        }

    }
}