using Mini_Project_v1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Mini_Project_v1.Controllers
{
    public class DanhMucChucVuTestController : Controller
    {
        // GET: DanhMucChucVuTest
        public ActionResult Index()
        {
            IEnumerable<DanhMucChucVu> DanhMucChucVus = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://192.168.136.80:8080/api/");
                //HTTP GET
                var responseTask = client.GetAsync("apiDanhMucChucVu");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<DanhMucChucVu>>();
                    readTask.Wait();

                    DanhMucChucVus = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    DanhMucChucVus = Enumerable.Empty<DanhMucChucVu>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }

            return View(DanhMucChucVus);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(DanhMucChucVu DanhMucChucVu)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://192.168.136.80:8080/api/apiDanhMucChucVu");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<DanhMucChucVu>("apiDanhMucChucVu", DanhMucChucVu);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(DanhMucChucVu);
        }

        public ActionResult Edit(string id)
        {
            DanhMucChucVu DanhMucChucVu = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://192.168.136.80:8080/api/");
                //HTTP GET
                var responseTask = client.GetAsync("apiDanhMucChucVu?id=" + id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<DanhMucChucVu>();
                    readTask.Wait();

                    DanhMucChucVu = readTask.Result;
                }
            }
            return View(DanhMucChucVu);
        }

        [HttpPost]
        public ActionResult Edit(DanhMucChucVu DanhMucChucVu)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://192.168.136.80:8080/api/apiphongban");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<DanhMucChucVu>("apiDanhMucChucVu", DanhMucChucVu);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(DanhMucChucVu);
        }


        //DELETE method
        public ActionResult Delete(string id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://192.168.136.80:8080/api/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("apiDanhMucChucVu/" + id);
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
    }
}