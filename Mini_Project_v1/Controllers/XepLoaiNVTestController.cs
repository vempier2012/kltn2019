using Mini_Project_v1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Mini_Project_v1.Controllers
{
    public class XepLoaiNVTestController : Controller
    {
        // GET: XepLoaiNVTest
        public ActionResult Index()
        {
            IEnumerable<XepLoaiNV> xepLoaiNVs = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://192.168.136.80:8080/api/");
                //HTTP GET
                var responseTask = client.GetAsync("apixeploainv");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<XepLoaiNV>>();
                    readTask.Wait();

                    xepLoaiNVs = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    xepLoaiNVs = Enumerable.Empty<XepLoaiNV>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }

            return View(xepLoaiNVs);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(XepLoaiNV xepLoaiNV)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://192.168.136.80:8080/api/apixeploainv");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<XepLoaiNV>("apixeploainv", xepLoaiNV);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(xepLoaiNV);
        }

        public ActionResult Edit(string id)//lấy data để hiển thị
        {
            XepLoaiNV xepLoaiNV = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://192.168.136.80:8080/api/");
                //HTTP GET
                var responseTask = client.GetAsync("apixeploainv?id=" + id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<XepLoaiNV>();
                    readTask.Wait();

                    xepLoaiNV = readTask.Result;
                }
            }
            return View(xepLoaiNV);
        }

        [HttpPost]
        public ActionResult Edit(XepLoaiNV xepLoaiNV)//thay đổi (put)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://192.168.136.80:8080/api/apixeploainv");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<XepLoaiNV>("apixeploainv", xepLoaiNV);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(xepLoaiNV);
        }


        //DELETE method
        public ActionResult Delete(string id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://192.168.136.80:8080/api/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("apixeploainv/" + id);
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