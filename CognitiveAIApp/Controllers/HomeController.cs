using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CognitiveAIApp.Models;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Net;
using System.IO;
using System.Text;
using System.Net.Http.Headers;

namespace CognitiveAIApp.Controllers
{
    public class HomeController : Controller
    {

        static HttpClient client = new HttpClient();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page Gnana.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Comments()
        {

            ViewData["Message"] = "Your comments Page";
            return View();

        }

        public IActionResult SubmitComments(string comments)
        {


            client.BaseAddress = new Uri("http://cognitiveaiwebapi20180514093947.azurewebsites.net/api/TextAnalytics");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string TextAPIUrl = "http://cognitiveaiwebapi20180514093947.azurewebsites.net/api/TextAnalytics";
            var content = new JValue(comments);

            var responseTask = client.PostAsync(TextAPIUrl, new JObject(content));
            responseTask.Wait();
            var result = responseTask.Result;


            string data = string.Empty;

            if (result.IsSuccessStatusCode)
            {

                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();

                data = readTask.Result;

            }


            //string strId = UserId_TextBox.Text;
            //string strName = Name_TextBox.Text;

            ASCIIEncoding encoding = new ASCIIEncoding();
            //string postData = "userid=" + strId;
            //postData += ("&username=" + strName);
            byte[] data1 = encoding.GetBytes(comments);

            // Prepare web request...
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create("http://cognitiveaiwebapi20180514093947.azurewebsites.net/api/TextAnalytics");
            myRequest.Method = "POST";
            myRequest.ContentType = "application/x-www-form-urlencoded";
            myRequest.ContentLength = data1.Length;
            Stream newStream = myRequest.GetRequestStream();
            // Send the data.
            newStream.Write(data1, 0, data.Length);
            newStream.Close();

            // ViewData["Message"] = comments;
            // return View();
            //string TextAPIResults = string.Empty;
            //var data = string.Empty;
            //var content = new JValue(comments);
            //string TextAPIUrl = "http://cognitiveaiwebapi20180514093947.azurewebsites.net/api/TextAnalytics";
            //using (var client = new HttpClient())
            //{

            //    client.BaseAddress = new Uri(TextAPIUrl);
            //    //HTTP POST
            //   // var content = new JValue(comments);
            //    var responseTask = client.PostAsync(TextAPIUrl,new StringContent(content.ToString()));
            //    responseTask.Wait();

            //    var result = responseTask.Result;
            //    if (result.IsSuccessStatusCode)
            //    {

            //        var readTask = result.Content.ReadAsStringAsync();
            //        readTask.Wait();

            //        data = readTask.Result;

            //    }
            //}
            ViewData["Message"] = data;
            return View();

        }
    }
}
