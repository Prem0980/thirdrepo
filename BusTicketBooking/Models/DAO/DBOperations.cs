using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Routing;

namespace BusTicketBooking.Models.DAO
{
    public class DBOperations
    {
        HttpClient client=new HttpClient();
        public Bus insert(Bus c)
        {
            UriBuilder builder = new UriBuilder("https://localhost:44337/api/BusBooking/addBus/");
            var json = JsonConvert.SerializeObject(c);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var task = client.PostAsync(builder.Uri, data);
            task.Wait();
            var output = task.Result;
            if (output.IsSuccessStatusCode)
            {
                var display = output.Content.ReadAsAsync<Bus>();
                return display.Result;
            }
            else
            {
                var display = output.Content.ReadAsAsync<Bus>();
                return display.Result;
            }
        }
        public List<string> getFromLocation()
        {

            List<string>list = null;
            UriBuilder uriBuilder = new UriBuilder("https://localhost:44337/api/BusBooking/getFromLocation/");
            var task = client.GetAsync(uriBuilder.Uri);
            task.Wait();
            var output = task.Result;
            if (output.IsSuccessStatusCode)
            {
                var display = output.Content.ReadAsAsync<List<string>>();
            
                display.Wait();
               list=display.Result;
                
            }

            return list;
        }
        public List<string> getToLocation()
        {

            List<string> list = null;
            UriBuilder uriBuilder = new UriBuilder("https://localhost:44337/api/BusBooking/getToLocation/");
            var task = client.GetAsync(uriBuilder.Uri);
            task.Wait();
            var output = task.Result;
            if (output.IsSuccessStatusCode)
            {
                var display = output.Content.ReadAsAsync<List<string>>();
                display.Wait();
               list = display.Result;
            }

            return list;
        }
        public List<string> getcitites(string state)
        {

            List<string> list = null;
            UriBuilder uriBuilder = new UriBuilder("https://localhost:44337/api/BusBooking/CityDrop/{state}");
            uriBuilder.Query = "state=" + state;
            var task = client.GetAsync(uriBuilder.Uri);
            task.Wait();
            var output = task.Result;
            if (output.IsSuccessStatusCode)
            {
                var display = output.Content.ReadAsAsync<List<string>>();
                display.Wait();
                list = display.Result;
            }

            return list;


        }
        public List<string> getstates()
        {

            List<string> list = null;
            UriBuilder uriBuilder = new UriBuilder("https://localhost:44337/api/BusBooking/StateDrop/");
            var task = client.GetAsync(uriBuilder.Uri);
            task.Wait();
            var output = task.Result;
            if (output.IsSuccessStatusCode)
            {
                var display = output.Content.ReadAsAsync<List<string>>();
                display.Wait();
                list = display.Result;
            }

            return list;
        }
        public CDetails insert(CDetails c)
        {
            UriBuilder builder = new UriBuilder("https://localhost:44337/api/BusBooking/addCustomer/");
            var json = JsonConvert.SerializeObject(c);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var task = client.PostAsync(builder.Uri, data);
            task.Wait();
            var output = task.Result;
            if (output.IsSuccessStatusCode)
            {
                var display = output.Content.ReadAsAsync<CDetails>();
                return display.Result;
            }
            else
            {
                var display = output.Content.ReadAsAsync<CDetails>();
                return display.Result;
            }
        }
        public string Admin(string userID, string UserPassword)
        {

            string list = null;
            UriBuilder uriBuilder = new UriBuilder("https://localhost:44337/api/BusBooking/Admin/{userID}/{UserPassword}");
            uriBuilder.Query = "userID=" + userID + "&UserPassword=" + UserPassword;

            var task = client.GetAsync(uriBuilder.Uri);
            task.Wait();
            var output = task.Result;
            if (output.IsSuccessStatusCode)
            {
                var display = output.Content.ReadAsAsync<string>();
                display.Wait();
                list = display.Result;
            }

            return list;
        }

        public string addAdmin(Admin A)
        {

            string list = null;
            UriBuilder uriBuilder = new UriBuilder("https://localhost:44337/api/BusBooking/Admin/addAdmin/");

            var json = JsonConvert.SerializeObject(A);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var task = client.PostAsync(uriBuilder.Uri, data);
            task.Wait();
            var output = task.Result;
            if (output.IsSuccessStatusCode)
            {
                var display = output.Content.ReadAsAsync<string>();
                display.Wait();
                list = display.Result;
            }

            return list;
        }
       
        public List<string> getTime()
        {

            List<string> list = null;
            UriBuilder uriBuilder = new UriBuilder("https://localhost:44337/api/BusBooking/getTime/");
            var task = client.GetAsync(uriBuilder.Uri);
            task.Wait();
            var output = task.Result;
            if (output.IsSuccessStatusCode)
            {
                var display = output.Content.ReadAsAsync<List<string>>();
                display.Wait();
                list = display.Result;
            }

            return list;
        }
        public List<string> getBus()
        {

            List<string> list = null;
            UriBuilder uriBuilder = new UriBuilder("https://localhost:44337/api/BusBooking/getBus/");
            var task = client.GetAsync(uriBuilder.Uri);
            task.Wait();
            var output = task.Result;
            if (output.IsSuccessStatusCode)
            {
                var display = output.Content.ReadAsAsync<List<string>>();
                display.Wait();
                list = display.Result;
            }

            return list;
        }
        public List<string> getbusid()
        {

            List<string> list = null;
            UriBuilder uriBuilder = new UriBuilder("https://localhost:44337/api/BusBooking/getbusid/");
            var task = client.GetAsync(uriBuilder.Uri);
            task.Wait();
            var output = task.Result;
            if (output.IsSuccessStatusCode)
            {
                var display = output.Content.ReadAsAsync<List<string>>();
                display.Wait();
                list = display.Result;
            }

            return list;
        }
        public List<string> getrouteid()
        {

            List<string> list = null;
            UriBuilder uriBuilder = new UriBuilder("https://localhost:44337/api/BusBooking/getrouteid/");
            var task = client.GetAsync(uriBuilder.Uri);
            task.Wait();
            var output = task.Result;
            if (output.IsSuccessStatusCode)
            {
                var display = output.Content.ReadAsAsync<List<string>>();
                display.Wait();
                list = display.Result;
            }

            return list;
        }
        public BTicket cancelbookticket(string id, int count)
        {

            BTicket ticket = null;
            UriBuilder uriBuilder1 = new UriBuilder("https://localhost:44337/api/BusBooking/TicketBooking1/{id}");
            uriBuilder1.Query = "id=" + id;

            var task1 = client.GetAsync(uriBuilder1.Uri);
            task1.Wait();
            var output1 = task1.Result;
            if (output1.IsSuccessStatusCode)
            {
                var jsonString = output1.Content.ReadAsStringAsync().Result;
                dynamic data = JsonConvert.DeserializeObject<dynamic>(jsonString);
                BTicket t = new BTicket();
                t.TicketID = data.TicketID;
                t.CustomerID = data.CustomerID;
                t.NumberOfTickets = count;
                t.ScheduleId = data.ScheduleId;
                ticket = t;
            }

            return ticket;
        }
    }
}