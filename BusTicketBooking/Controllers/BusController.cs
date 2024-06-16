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
    public class BusController : Controller
    {
        // GET: Bus
        HttpClient client=new HttpClient();
        public ActionResult HomePage()
        {
            return View();
        }
        public ActionResult insertcustomerdetails() 
        { 
            return View(); 
        
        }
        public ActionResult insertCD() 
        {
            UriBuilder uriBuilder = new UriBuilder("https://localhost:44337/api/BusBooking");
            var task = client.GetAsync(uriBuilder.Uri);
            task.Wait();
            var output = task.Result;
            if (output.IsSuccessStatusCode)
            {
                var display = output.Content.ReadAsAsync<List<CDetails>>();
                display.Wait();
                ViewBag.list = display.Result;
            }
            else
            {
                var display = output.Content.ReadAsStringAsync();
                display.Wait();
                ViewBag.msg = display.Result;
            }
            return View("insertcustomerdetails");
        
        }
        public ActionResult SignUp()
        {

            CDetails C = new CDetails();
            Random random = new Random();
            String Id = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + random.Next(0000, 9999);

            C.Country = "India";
            C.CustomerID = Int32.Parse(Id);
            return View(C);
        }
        public ActionResult SignUp1(CDetails c, string Customerpassword, string ConfirmPassword, String custId)
        {
         
            c.CustomerID = long.Parse(custId);
            if (ModelState.IsValid)
            {
                if (Customerpassword == ConfirmPassword)
                {
                    //var json = JsonConvert.SerializeObject(data);
                    UriBuilder uriBuilder = new UriBuilder("https://localhost:44337/api/BusBooking");
                    
                    var json = JsonConvert.SerializeObject(c);
                    var content = new StringContent(json);
                    var response = client.PostAsync(uriBuilder.Uri,content);
                    response.Wait();
                    var output = response.Result;
                    
                    if (output.IsSuccessStatusCode)
                    {
                        var display = output.Content.ReadAsAsync<CDetails>();
                        display.Wait();
                        ViewBag.list = display.Result;
                    }
                    else
                    {
                        var display = output.Content.ReadAsStringAsync();
                        display.Wait();
                        ViewBag.msg = display.Result;
                    }
                    return View("SignUp", c);
                }
                else
                {
                    ViewBag.msg = "Please Enter The password and confirm password as same";
                    return View("SignUp", c);
                }
            }
            else
            {
                return View("SignUp", c);
            }
        }


    }
}