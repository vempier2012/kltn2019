using Mini_Project_v1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Mini_Project_v1.Controllers
{
    public class NhanvienTestController : Controller
    {
        // GET: NhanvienTest
        public ActionResult Index()
        {
            IEnumerable<Nhanvien> nhanviens = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://192.168.136.80:8080/api/");
                //HTTP GET
                var responseTask = client.GetAsync("apinhanvien");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Nhanvien>>();
                    readTask.Wait();

                    nhanviens = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    nhanviens = Enumerable.Empty<Nhanvien>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }

            return View(nhanviens);
        }

        public ActionResult Create()
        {
            IEnumerable<XepLoaiNV> xl = null;
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

                    xl = readTask.Result;
                    ViewBag.MaLoaiNV = new SelectList(xl, "MaLoaiNV", "TenLoaiNV");
                }
                else //web api sent error response 
                {
                    //log response status here..

                    xl = Enumerable.Empty<XepLoaiNV>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult Create(Nhanvien nhanvien)
        {
            //hàm tự động sinh IDNV
            nhanvien.IDNV = autoID(nhanvien.MaLoaiNV.ToString());
            IEnumerable<XepLoaiNV> xl = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://192.168.136.80:8080/api/");
                //HTTP GET
                var responseTask = client.GetAsync("apinhanvien");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<XepLoaiNV>>();
                    readTask.Wait();

                    xl = readTask.Result;
                    ViewBag.MaLoaiNV = new SelectList(xl, "MaLoaiNV", "TenLoaiNV");
                }
                else //web api sent error response 
                {
                    //log response status here..

                    xl = Enumerable.Empty<XepLoaiNV>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://192.168.136.80:8080/api/apinhanvien");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<Nhanvien>("apinhanvien", nhanvien);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    ViewBag.MaLoaiNV = new SelectList(xl, "MaLoaiNV", "TenLoaiNV", nhanvien.MaLoaiNV);
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
            ViewBag.MaLoaiNV = new SelectList(xl, "MaLoaiNV", "TenLoaiNV", nhanvien.MaLoaiNV);
            return View(nhanvien);
        }

        public ActionResult Edit(string id)
        {
            Nhanvien nhanvien = null;
            IEnumerable<XepLoaiNV> xl = null;
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

                    xl = readTask.Result;
                    ViewBag.MaLoaiNV = new SelectList(xl, "MaLoaiNV", "TenLoaiNV");
                }
                else //web api sent error response 
                {
                    //log response status here..

                    xl = Enumerable.Empty<XepLoaiNV>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://192.168.136.80:8080/api/");
                //HTTP GET
                var responseTask = client.GetAsync("apinhanvien?id=" + id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Nhanvien>();
                    readTask.Wait();

                    nhanvien = readTask.Result;
                }
            }
            return View(nhanvien);
        }

        [HttpPost]
        public ActionResult Edit(Nhanvien nhanvien)
        {
            IEnumerable<XepLoaiNV> xl = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://192.168.136.80:8080/api/");
                //HTTP GET
                var responseTask = client.GetAsync("apinhanvien");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<XepLoaiNV>>();
                    readTask.Wait();

                    xl = readTask.Result;
                    ViewBag.MaLoaiNV = new SelectList(xl, "MaLoaiNV", "TenLoaiNV");
                }
                else //web api sent error response 
                {
                    //log response status here..

                    xl = Enumerable.Empty<XepLoaiNV>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://192.168.136.80:8080/api/apinhanvien");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<Nhanvien>("apinhanvien", nhanvien);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    ViewBag.MaLoaiNV = new SelectList(xl, "MaLoaiNV", "TenLoaiNV", nhanvien.MaLoaiNV);
                    return RedirectToAction("Index");
                }
            }
            ViewBag.MaLoaiNV = new SelectList(xl, "MaLoaiNV", "TenLoaiNV", nhanvien.MaLoaiNV);
            return View(nhanvien);
        }


        //DELETE method
        public ActionResult Delete(string id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://192.168.136.80:8080/api/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("apinhanvien/" + id);
                deleteTask.Wait();

                var result = deleteTask.Result;//bug giá trị trả về bằng null
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }

        //hàm xét giá trị khi cắt chuỗi
        public string autoID (string xlnv)
        {
            IEnumerable<Nhanvien> nhanviens = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://192.168.136.80:8080/api/");
                //HTTP GET
                var responseTask = client.GetAsync("apinhanvien");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Nhanvien>>();
                    readTask.Wait();

                    nhanviens = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    nhanviens = Enumerable.Empty<Nhanvien>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            var list = new List<int>();
            foreach (var item in nhanviens)
            {
                string a = item.IDNV.ToString();
                int len = xlnv.Length;
                if (a.Contains(xlnv))//kiểm tra trong chuỗi mã idnv có mã loại nv ko
                {
                    string b = a.Substring(len);
                    int c = int.Parse(b);
                    list.Add(c);
                }                
            }
            int max = 0; string final = null;
            if  (list.Count == 0)
            {
                final = (xlnv + (max + 1));
                return final;
            }
            else
            {
                max = list.Max();
                final = (xlnv + (max + 1));
                return final;
            }
            
        }
    }
}