using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Mini_Project_v1.Models;

namespace Mini_Project_v1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult Trangchu()
        {
            Nhanvien nhanvien = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://192.168.136.80:8080/api/");
                //HTTP GET
                var responseTask = client.GetAsync("apinhanvien?id=" + Session["IDNV"]);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Nhanvien>();
                    readTask.Wait();

                    nhanvien = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    nhanvien = null;

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }

            return View(nhanvien);
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
