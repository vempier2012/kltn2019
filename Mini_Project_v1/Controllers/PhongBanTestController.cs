using Mini_Project_v1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Mini_Project_v1.Controllers
{
    public class PhongBanTestController : Controller
    {
        public ActionResult Index()
        {
            IEnumerable<PhongBan> phongBans = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://192.168.136.80:8080/api/");
                //HTTP GET
                var responseTask = client.GetAsync("apiphongban");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<PhongBan>>();
                    readTask.Wait();

                    phongBans = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    phongBans = Enumerable.Empty<PhongBan>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }

            return View(phongBans);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(PhongBan phongBan)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://192.168.136.80:8080/api/apiphongban");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<PhongBan>("apiphongban", phongBan);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(phongBan);
        }

        public ActionResult Edit(string id)
        {
            PhongBan phongBan = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://192.168.136.80:8080/api/");
                //HTTP GET
                var responseTask = client.GetAsync("apiphongban?id=" + id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<PhongBan>();
                    readTask.Wait();

                    phongBan = readTask.Result;
                }
            }
            return View(phongBan);
        }

        [HttpPost]
        public ActionResult Edit(PhongBan phongBan)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://192.168.136.80:8080/api/apiphongban");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<PhongBan>("apiphongban", phongBan);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(phongBan);
        }


        //DELETE method
        public ActionResult Delete(string id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://192.168.136.80:8080/api/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("apiphongban/" + id);
                deleteTask.Wait();

                var result = deleteTask.Result;//bug giá trị trả về bằng null
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }
    }
}