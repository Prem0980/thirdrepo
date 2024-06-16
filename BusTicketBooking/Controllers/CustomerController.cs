using BusTicketBooking.Models.DAO;
using BusTicketBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace BusTicketBooking.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        Admin ad = new Admin();
        DBOperations db = new DBOperations();
        HttpClient client = new HttpClient();
        public ActionResult Entryy()
        {
            return View();
        }
        public ActionResult SignUp()
        {
            CDetails C = new CDetails();
            Random random = new Random();
            String Id = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString("d2") + random.Next(0000, 9999);
            C.CustomerID = Int32.Parse(Id);
            ViewBag.states = db.getstates();
            C.Country = "India";
            return View(C);
        }
        public ActionResult SignUp1(CDetails c, string Customerpassword, string ConfirmPassword, String custId, String country, string state, string Address, string Name)
        {
            c.CustomerID = long.Parse(custId);
            List<string> list = db.getcitites(state);
            ViewBag.states = db.getstates();
            ViewBag.cities = list;
            c.Country = country;
            ad.Role = "customer";
            ad.UserID = custId;
            ad.UserPassword = Customerpassword;
            c.Address = Address;
            c.Name = Name;
            if (ModelState.IsValid)
            {
                if (Customerpassword == ConfirmPassword)
                {
                    CDetails k = db.insert(c);
                    db.addAdmin(ad);
                    return RedirectToAction("Login","Customer");
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

        public ActionResult getCities(string state, string custId, string country, string Address, string Name)
        {
            CDetails C = new CDetails();
            C.CustomerID = long.Parse(custId);
            C.Country = country;
            C.Address = Address;
            C.Name = Name;
            List<string> list = db.getcitites(state);
            ViewBag.states = db.getstates();
            ViewBag.cities = list;
            return View("SignUp", C);
        }
        [HttpGet]
        public ActionResult Login()
        {

            return View(ad);
        }
        [HttpPost]
        public ActionResult Login(string userID, string UserPassword)
        {
            String result = db.Admin(userID, UserPassword);
            if (result == null)
            {
                ViewBag.msg = "Invalid credentails";
                return View(result);
            }
            else
            {
                Session["user"] = userID;
                Session["utype"] = result;
                if (result.Equals("admin"))
                {
                    return RedirectToAction("SignUp", "Customer");
                }
                else
                {
                    return RedirectToAction("ViewHistory", "ViewHistory");

                }
            }
            
        }
        public ActionResult UpdateCustomer()
        {
            CDetails c = null;
            string id = Session["user"].ToString();
            if (ModelState.IsValid)
            {
                UriBuilder uriBuilder = new UriBuilder("https://localhost:44337/api/BusBooking/CustomerExtract/{id}");
                uriBuilder.Query = "id=" + id;
                var task = client.GetAsync(uriBuilder.Uri);

                task.Wait();
                var output = task.Result;
                if (output.IsSuccessStatusCode)
                {
                    var display = output.Content.ReadAsAsync<CDetails>();
                    display.Wait();
                    c = display.Result;

                }
                else
                {
                    var display = output.Content.ReadAsStringAsync();
                    display.Wait();
                    string json = display.Result;
                    dynamic data = JsonConvert.DeserializeObject<dynamic>(json);
                    string msg = data.Message;

                    ViewBag.msg = msg;

                }
            }
            else
            {
                return View();

            }

            return View(c);
        }
        [HttpPost]
        public ActionResult UpdateCustomer(CDetails c)
        {
            if (ModelState.IsValid || c.Customerpassword == null && c.ConfirmPassword == null && c.State2 == null)
            {
                UriBuilder uriBuilder = new UriBuilder("https://localhost:44337/api/BusBooking/UpdateCustomer/" + c.CustomerID);
                //uriBuilder.Query = "id=" + id;
                var json = JsonConvert.SerializeObject(c);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var task = client.PutAsync(uriBuilder.Uri, data);

                task.Wait();
                var output = task.Result;
                if (output.IsSuccessStatusCode)
                {
                    var display = output.Content.ReadAsAsync<CDetails>();
                    display.Wait();
                    c = display.Result;
                    ViewBag.msg = "Updated Sucessfully";
                }
                else
                {
                    var display = output.Content.ReadAsAsync<CDetails>();
                    c = display.Result;
                }
            }
            else
            {
                ViewBag.msg1 = "Please Enter The valid Customer ID";
            }
            return View(c);
        }
    }
}