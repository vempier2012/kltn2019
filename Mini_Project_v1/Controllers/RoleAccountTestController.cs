using Mini_Project_v1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Mini_Project_v1.Controllers
{
    public class RoleAccountTestController : Controller
    {
        // GET: RoleAccountTest
        public ActionResult Index()
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
                }
                else //web api sent error response 
                {
                    //log response status here..

                    roleAccounts = Enumerable.Empty<RoleAccount>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }

            return View(roleAccounts);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(RoleAccount roleAccount)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://192.168.136.80:8080/api/apiroleaccount");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<RoleAccount>("apiroleaccount", roleAccount);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(roleAccount);
        }

        public ActionResult Edit(string id)
        {
            RoleAccount role = new RoleAccount();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://192.168.136.80:8080/api/");
                //HTTP GET
                var responseTask = client.GetAsync("apiroleaccount?id=" + id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<RoleAccount>();
                    readTask.Wait();

                    role = readTask.Result;
                }
            }
            return View(role);
        }

        [HttpPost]
        public ActionResult Edit(RoleAccount roleAccount)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://192.168.136.80:8080/api/apiroleaccount");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<RoleAccount>("apiroleaccount", roleAccount);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(roleAccount);
        }

        //DELETE method
        public ActionResult Delete(string id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://192.168.136.80:8080/api/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("apiroleaccount/" + id);
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