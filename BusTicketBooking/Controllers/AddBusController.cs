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
    public class AddBusController : Controller
    {
        // GET: AddBus
        DBOperations db = new DBOperations();
        HttpClient client = new HttpClient();
        [HttpGet]
        public ActionResult AddBus()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddBus(Bus B)
        {
            
            if(ModelState.IsValid) 
            {
                UriBuilder builder = new UriBuilder("https://localhost:44337/api/BusBooking/addBus/");
                var json = JsonConvert.SerializeObject(B);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var task = client.PostAsync(builder.Uri, data);
                task.Wait();
                var output = task.Result;
                if (output.IsSuccessStatusCode)
                {
                    ViewBag.msg1 = "Bus Added SucessFully";
                }
                else
                {
                    var display = output.Content.ReadAsStringAsync();
                    display.Wait();
                    string j = display.Result;
                    dynamic data1=JsonConvert.DeserializeObject<dynamic>(j);
                    ViewBag.msg= data1.Message;
                }
            }
            return View();
        }

    }
}