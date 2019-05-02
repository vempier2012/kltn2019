using Mini_Project_v1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Mini_Project_v1.Controllers
{
    //[Authorize(Roles = "admin")]
    public class AccountsTestController : Controller
    {
        DBContext db = new DBContext();

        public ActionResult LogControl()
        {
            return View();
        }


        // GET: AccountsTest
        public ActionResult Index()
        {
            IEnumerable<Account> accounts = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://192.168.136.80:8080/api/");
                //HTTP GET
                var responseTask = client.GetAsync("apiaccount");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Account>>();
                    readTask.Wait();

                    accounts = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    accounts = Enumerable.Empty<Account>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }

            return View(accounts);
        }

        public ActionResult Create()
        {
            IEnumerable<RoleAccount> roleAccounts = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://192.168.136.80:8080/api/");
                //HTTP GET
                var responseTask = client.GetAsync("apiroleaccount");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<RoleAccount>>();
                    readTask.Wait();

                    roleAccounts = readTask.Result;
                    ViewBag.Role = new SelectList(roleAccounts, "Role", "TenRole");
                }
                else //web api sent error response 
                {
                    //log response status here..

                    roleAccounts = Enumerable.Empty<RoleAccount>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
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
                    ViewBag.IDNV = new SelectList(nhanviens, "IDNV", "IDNV");
                }
                else //web api sent error response 
                {
                    //log response status here..

                    nhanviens = Enumerable.Empty<Nhanvien>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult Create(Account account)
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
                    ViewBag.IDNV = new SelectList(nhanviens, "IDNV", "IDNV");
                }
                else //web api sent error response 
                {
                    //log response status here..

                    nhanviens = Enumerable.Empty<Nhanvien>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            IEnumerable<RoleAccount> roleAccounts = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://192.168.136.80:8080/api/");
                //HTTP GET
                var responseTask = client.GetAsync("apiroleaccount");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<RoleAccount>>();
                    readTask.Wait();

                    roleAccounts = readTask.Result;
                    ViewBag.Role = new SelectList(roleAccounts, "Role", "TenRole");
                }
                else //web api sent error response 
                {
                    //log response status here..

                    roleAccounts = Enumerable.Empty<RoleAccount>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            using (var client1 = new HttpClient())
            {
                client1.BaseAddress = new Uri("http://192.168.136.80:8080/api/");
                //HTTP GET
                var responseTask = client1.GetAsync("apiaccount?username=" + account.Taikhoan);
                responseTask.Wait();

                var result1 = responseTask.Result;
                if (result1.IsSuccessStatusCode)
                {
                    ViewBag.IDNV = new SelectList(nhanviens, "IDNV", "IDNV", account.IDNV);
                    ViewBag.Role = new SelectList(roleAccounts, "Role", "TenRole", account.Role);
                    ViewBag.CreateFail = "Tên tài khoản này đã có người sử dụng. Vui lòng chọn tên tài khoản khác";
                    return View();
                }
                else
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("http://192.168.136.80:8080/api/apiaccount");

                        //HTTP POST
                        var postTask = client.PostAsJsonAsync<Account>("apiaccount", account);
                        postTask.Wait();

                        var result = postTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            ViewBag.IDNV = new SelectList(nhanviens, "IDNV", "IDNV", account.IDNV);
                            ViewBag.Role = new SelectList(roleAccounts, "Role", "TenRole", account.Role);
                            return RedirectToAction("Index");
                        }
                    }
                }
            }
            

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(account);
        }

        
        public ActionResult Edit(string id)
        {            
            IEnumerable<RoleAccount> roleAccounts = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://192.168.136.80:8080/api/");
                //HTTP GET
                var responseTask = client.GetAsync("apiroleaccount");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<RoleAccount>>();
                    readTask.Wait();

                    roleAccounts = readTask.Result;
                    ViewBag.Role = new SelectList(roleAccounts, "Role", "TenRole");
                }
                else //web api sent error response 
                {
                    //log response status here..

                    roleAccounts = Enumerable.Empty<RoleAccount>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            Account account = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://192.168.136.80:8080/api/");
                //HTTP GET
                var responseTask = client.GetAsync("apiaccount?id=" + id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Account>();
                    readTask.Wait();

                    account = readTask.Result;
                }
                else
                {
                    responseTask = client.GetAsync("apiaccount?idnv=" + id);
                    responseTask.Wait();

                    result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<Account>();
                        readTask.Wait();

                        account = readTask.Result;
                    }
                }
                return View(account);
            }
        }

        [HttpPost]
        public ActionResult Edit(Account account)
        {
            IEnumerable<RoleAccount> roleAccounts = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://192.168.136.80:8080/api/");
                //HTTP GET
                var responseTask = client.GetAsync("apiroleaccount");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<RoleAccount>>();
                    readTask.Wait();

                    roleAccounts = readTask.Result;
                    ViewBag.Role = new SelectList(roleAccounts, "Role", "TenRole");
                }
                else //web api sent error response 
                {
                    //log response status here..

                    roleAccounts = Enumerable.Empty<RoleAccount>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://192.168.136.80:8080/api/apiaccount");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<Account>("apiaccount", account);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    ViewBag.Role = new SelectList(roleAccounts, "Role", "TenRole", account.Role);
                    return RedirectToAction("Index");
                }
            }
            ViewBag.Role = new SelectList(roleAccounts, "Role", "TenRole", account.Role);
            return View(account);
        }

        //DELETE method
        public ActionResult Delete(string id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://192.168.136.80:8080/api/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("apiaccount/" + id);
                deleteTask.Wait();

                var result = deleteTask.Result;//bug giá trị trả về bằng null
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
                //ViewBag.IDNV = new SelectList(db.Nhanviens, "IDNV", "Ten");
                //Session.Abandon();
                return View();           
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(Account objUser, string returnUr)
        {
            //Session.Abandon();
            Account account = null;

            if (objUser.Taikhoan == null || objUser.Matkhau == null)
            {
                ViewBag.Error = "Vui lòng nhập đầy đủ thông tin";
                return View("Login");
            }
            else
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://192.168.136.80:8080/api/");
                    //HTTP GET
                    var responseTask = client.GetAsync("apiaccount?username=" + objUser.Taikhoan + "&password=" + objUser.Matkhau);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<Account>();
                        readTask.Wait();

                        account = readTask.Result;
                    }
                    else
                    {
                        ViewBag.Error = "Tài khoản hoặc mật khẩu không đúng.";
                        return View("Login");
                    }
                }
                var obj = db.Accounts.Where(x => x.Taikhoan.Equals(account.Taikhoan) && x.Matkhau.Equals(account.Matkhau)).FirstOrDefault();

                if (obj == null)
                {
                    ViewBag.Failed = "Lỗi";
                    return View("Login");
                }
                if (obj != null)
                {
                    var obj2 = db.Nhanviens.Where(x => x.IDNV.Equals(obj.IDNV)).FirstOrDefault();
                    if (obj.Role == "admin")
                    {
                        Session["userName"] = obj2.Ho.ToString() + " " + obj2.Ten.ToString();
                        Session["IDNV"] = obj2.IDNV.ToString();
                        return Redirect(returnUr ?? Url.Action("Trangchu", "Home"));
                    }
                    else
                    {
                        ViewBag.RoleConfirm = "Không phải Admin";
                        return Redirect(returnUr ?? Url.Action("Trangchu", "Home"));
                    }
                }
                else
                {
                    ViewBag.Failed = "tên đăng nhập or mật khẩu sai";
                    return View("Login");
                }
            }
        }

        public ActionResult LogOut()
        {
            Session.RemoveAll();
            Session.Abandon();
            return RedirectToAction("Login", "AccountsTest");
        }
    }
}