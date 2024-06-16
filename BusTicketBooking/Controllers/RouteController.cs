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
    public class RouteController : Controller
    {
        // GET: Route
        HttpClient client=new HttpClient();
        public ActionResult AddNewRoute()
        {
            RouteDetails rd = new RouteDetails();
            return View(rd);
        }
        [HttpPost]
        public ActionResult AddNewRoute(RouteDetails rd)
        {
            if (ModelState.IsValid)
            {
                UriBuilder builder = new UriBuilder("https://localhost:44337/api/BusBooking/addRoute/");
                var json = JsonConvert.SerializeObject(rd);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var task = client.PostAsync(builder.Uri, data);
                task.Wait();
                var output = task.Result;
                if (output.IsSuccessStatusCode)
                {
                    ViewBag.msg1 = "Route Added Successfully";
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
            return View();
        }
    }
}