using Mini_Project_v1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Mini_Project_v1.Controllers
{
    public class NvPbTestController : Controller
    {
        // GET: NvPbTest
        public ActionResult Index()
        {
            IEnumerable<NhanViensPhongBan> nhanViensPhongBans = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://192.168.136.80:8080/api/");
                //HTTP GET
                var responseTask = client.GetAsync("apinvpb");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<NhanViensPhongBan>>();
                    readTask.Wait();

                    nhanViensPhongBans = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    nhanViensPhongBans = Enumerable.Empty<NhanViensPhongBan>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(nhanViensPhongBans);
        }

        //lấy danh sách nhân viên trong cùng 1 phòng ban
        public ActionResult DanhSachNhanVien(string id)
        {
            IEnumerable<Nhanvien> nhanViens = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://192.168.136.80:8080/api/");
                //HTTP GET
                var responseTask = client.GetAsync("apinvpb?phongban=" + id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Nhanvien>>();
                    readTask.Wait();

                    nhanViens = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    nhanViens = Enumerable.Empty<Nhanvien>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(nhanViens);
        }

        //thêm mới 1 nhân viên vào 1 phòng ban
        public ActionResult Create()
        {
            //lấy ra danh sách nhân viên, phòng ban, danh mục chức vụ để làm dropdown list
            IEnumerable<Nhanvien> nhanViens = null;
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

                    nhanViens = readTask.Result;
                    ViewBag.IDNV = new SelectList(nhanViens, "IDNV", "Ten");
                }
                else //web api sent error response 
                {
                    //log response status here..

                    nhanViens = Enumerable.Empty<Nhanvien>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
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
                    ViewBag.MaPB = new SelectList(phongBans, "MaPB", "TenPB");
                }
                else //web api sent error response 
                {
                    //log response status here..

                    nhanViens = Enumerable.Empty<Nhanvien>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            IEnumerable<DanhMucChucVu> danhMucChucVus = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://192.168.136.80:8080/api/");
                //HTTP GET
                var responseTask = client.GetAsync("apidanhmucchucvu");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<DanhMucChucVu>>();
                    readTask.Wait();

                    danhMucChucVus = readTask.Result;
                    ViewBag.MaChucVu = new SelectList(danhMucChucVus, "MaChucVu", "TenChucVu");
                }
                else //web api sent error response 
                {
                    //log response status here..

                    nhanViens = Enumerable.Empty<Nhanvien>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View();
        }
        //thêm 1 nhân viên có sẵn vào 1 phòng ban nào đó
        [HttpPost]
        public ActionResult Create(NhanViensPhongBan nvpb)
        {
            //lấy ra danh sách nhân viên, phòng ban, danh mục chức vụ để làm dropdown list
            IEnumerable<Nhanvien> nhanViens = null;
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

                    nhanViens = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    nhanViens = Enumerable.Empty<Nhanvien>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
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
            IEnumerable<DanhMucChucVu> danhMucChucVus = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://192.168.136.80:8080/api/");
                //HTTP GET
                var responseTask = client.GetAsync("apidanhmucchucvu");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<DanhMucChucVu>>();
                    readTask.Wait();

                    danhMucChucVus = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    danhMucChucVus = Enumerable.Empty<DanhMucChucVu>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }

            using (var client1 = new HttpClient())
            {

                client1.BaseAddress = new Uri("http://192.168.136.80:8080/api/");
                //HTTP GET
                var responseTask = client1.GetAsync("apinvpb?idnv=" + nvpb.IDNV + "&mapb=" + nvpb.MaPB);//kiểm tra xem nhân viên đó đã có hay chưa
                responseTask.Wait();
                
                var result1 = responseTask.Result;
                if (result1.IsSuccessStatusCode)//IsSuccessStatusCode, sử dụng content để kiểm tra giá trị trả về 
                {
                    ViewBag.CreateFail = "Nhân viên này đã có trong phòng ban này.";
                    ViewBag.IDNV = new SelectList(nhanViens, "IDNV", "Ten", nvpb.IDNV);
                    ViewBag.MaPB = new SelectList(phongBans, "MaPB", "TenPB", nvpb.MaPB);
                    ViewBag.MaChucVu = new SelectList(danhMucChucVus, "MaChucVu", "TenChucVu", nvpb.MaChucVu);
                    return View();
                }
                else
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("http://192.168.136.80:8080/api/apiaccount");

                        //HTTP POST
                        var postTask = client.PostAsJsonAsync<NhanViensPhongBan>("apinvpb", nvpb);
                        postTask.Wait();

                        var result = postTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Index");
                        }
                    }
                }
            }


            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
            ViewBag.IDNV = new SelectList(nhanViens, "IDNV", "Ten", nvpb.IDNV);
            ViewBag.MaPB = new SelectList(phongBans, "MaPB", "TenPB", nvpb.MaPB);
            ViewBag.MaChucVu = new SelectList(danhMucChucVus, "MaChucVu", "TenChucVu", nvpb.MaChucVu);
            return View(nvpb);
        }

        //Chỉnh sửa thông tin phòng ban, chức vụ của nhân viên đó
        public ActionResult Edit(string id, string id2)
        {
            IEnumerable<DanhMucChucVu> danhMucChucVus = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://192.168.136.80:8080/api/");
                //HTTP GET
                var responseTask = client.GetAsync("apidanhmucchucvu");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<DanhMucChucVu>>();
                    readTask.Wait();

                    danhMucChucVus = readTask.Result;
                    ViewBag.MaChucVu = new SelectList(danhMucChucVus, "MaChucVu", "TenChucVu");
                }
                else //web api sent error response 
                {
                    //log response status here..

                    danhMucChucVus = Enumerable.Empty<DanhMucChucVu>();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }

            NhanViensPhongBan nhanViensPhongBan = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://192.168.136.80:8080/api/");
                //HTTP GET
                var responseTask = client.GetAsync("apinvpb?idnv=" + id + "&mapb=" + id2);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<NhanViensPhongBan>();
                    readTask.Wait();

                    nhanViensPhongBan = readTask.Result;
                }
            }
            return View(nhanViensPhongBan);
        }
        [HttpPost]
        public ActionResult Edit(NhanViensPhongBan nhanViensPhongBan)
        {
            IEnumerable<DanhMucChucVu> danhMucChucVus = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://192.168.136.80:8080/api/");
                //HTTP GET
                var responseTask = client.GetAsync("apidanhmucchucvu");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<DanhMucChucVu>>();
                    readTask.Wait();

                    danhMucChucVus = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    danhMucChucVus = Enumerable.Empty<DanhMucChucVu>();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://192.168.136.80:8080/api/apinvpb");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<NhanViensPhongBan>("apinvpb", nhanViensPhongBan);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    ViewBag.MaChucVu = new SelectList(danhMucChucVus, "MaChucVu", "TenChucVu", nhanViensPhongBan.MaChucVu);
                    return RedirectToAction("Index");
                }
            }
            ViewBag.MaChucVu = new SelectList(danhMucChucVus, "MaChucVu", "TenChucVu", nhanViensPhongBan.MaChucVu);
            return View(nhanViensPhongBan);
        }
        
        //DELETE method
        //public ActionResult Delete(NhanViensPhongBan nhanViensPhong)
        //{
        //    NhanViensPhongBan nhanViensPhongBan = null;

        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri("http://192.168.136.80:8080/api/");
        //        //HTTP GET
        //        var responseTask = client.GetAsync("apinvpb?idnv=" + nhanViensPhong.IDNV);
        //        responseTask.Wait();

        //        var result = responseTask.Result;
        //        if (result.IsSuccessStatusCode)
        //        {
        //            var readTask = result.Content.ReadAsAsync<NhanViensPhongBan>();
        //            readTask.Wait();

        //            nhanViensPhongBan = readTask.Result;
        //        }
        //    }
        //    return View(nhanViensPhongBan);
        //}
        public ActionResult Delete(string id, string id2)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://192.168.136.80:8080/api/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("apinvpb?idnv=" + id + "&mapb=" + id2);
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