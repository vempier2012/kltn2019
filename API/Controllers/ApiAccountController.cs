using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;

namespace Mini_Project_v1.Controllers
{
    
    public class ApiAccountController : ApiController
    {
        DBContext db = new DBContext();

        //GET Method
        public IHttpActionResult GetAllAccounts()
        {
            var accounts = db.Accounts.Include(a => a.Nhanvien);
            List<Account> alist = new List<Account>();

            foreach (var a in accounts)
            {
                foreach (var b in a.Nhanvien.Accounts)
                {
                    Account x = new Account
                    {
                        IDA = a.IDA,
                        IDNV = b.IDNV,
                        Taikhoan = a.Taikhoan,
                        Matkhau = b.Matkhau,
                        Email = a.Email,
                        Role = b.Role
                    };
                    alist.Add(x);
                }
            }
            if (alist.Count() == 0)
            {
                return NotFound();
            }
            return Ok(alist.ToList());
        }

        public IHttpActionResult GetAccountById(int id)
        {
            var accounts = db.Accounts.Include(a => a.Nhanvien);
            Account acc = null;
            
            foreach (var a in accounts.Where(s => s.IDA == id))
            {
                foreach (var b in a.Nhanvien.Accounts)
                {
                    Account x = new Account
                    {
                        IDA = a.IDA,
                        IDNV = b.IDNV,
                        Taikhoan = a.Taikhoan,
                        Matkhau = b.Matkhau,
                        Email = a.Email,
                        Role = b.Role
                    };
                    acc = x;
                }
            }            

            if (acc == null)
            {
                return NotFound();
            }
            return Ok(acc);
        }

        public IHttpActionResult GetAccountByIDNV(string idnv)
        {
            var accounts = db.Accounts.Include(a => a.Nhanvien);
            Account acc = null;

            foreach (var a in accounts.Where(s => s.IDNV == idnv))
            {
                foreach (var b in a.Nhanvien.Accounts)
                {
                    Account x = new Account
                    {
                        IDA = a.IDA,
                        IDNV = b.IDNV,
                        Taikhoan = a.Taikhoan,
                        Matkhau = b.Matkhau,
                        Email = a.Email,
                        Role = b.Role
                    };
                    acc = x;
                }
            }

            if (acc == null)
            {
                return NotFound();
            }
            return Ok(acc);
        }

        public IHttpActionResult GetAllAccounts(string username)
        {
            var accounts = db.Accounts.Include(a => a.Nhanvien);
            Account acc = null;

            foreach (var a in accounts.Where(s => s.Taikhoan == username))
            {
                foreach (var b in a.Nhanvien.Accounts)
                {
                    Account x = new Account
                    {
                        IDA = a.IDA,
                        IDNV = b.IDNV,
                        Taikhoan = a.Taikhoan,
                        Matkhau = b.Matkhau,
                        Email = a.Email,
                        Role = b.Role
                    };
                    acc = x;
                }
            }

            if (acc == null)
            {
                return NotFound();
            }
            return Ok(acc);
        }

        public IHttpActionResult GetAllAccounts(string username, string password)
        {
            var accounts = db.Accounts.Include(a => a.Nhanvien);
            Account acc = null;

            foreach (var a in accounts.Where(s => s.Taikhoan == username && s.Matkhau == password))
            {
                foreach (var b in a.Nhanvien.Accounts)
                {
                    Account x = new Account
                    {
                        IDA = a.IDA,
                        IDNV = b.IDNV,
                        Taikhoan = a.Taikhoan,
                        Matkhau = b.Matkhau,
                        Email = a.Email,
                        Role = b.Role
                    };
                    acc = x;
                }
            }

            if (acc == null)
            {
                return NotFound();
            }
            return Ok(acc);
        }

        //POST Method
        public ApiAccountController()
        {

        }

        public IHttpActionResult PostNewAccount(Account account)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid Data.");

            var value = db.Accounts.Include(s => s.Nhanvien);
            db.Accounts.Add(new Account()
            {
                IDA = account.IDA,
                IDNV = account.IDNV,
                Taikhoan = account.Taikhoan,
                Matkhau = account.Matkhau,
                Email = account.Email,
                Role = account.Role
            });
            db.SaveChanges();

            return Ok();
        }

        //PUT
        public IHttpActionResult Put(Account account)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");


            var old = db.Accounts.Where(s => s.IDA == account.IDA)
                                                    .FirstOrDefault<Account>();

            if (old != null)
            {
                old.IDA = account.IDA;
                old.IDNV = account.IDNV;
                old.Taikhoan = account.Taikhoan;
                old.Matkhau = account.Matkhau;
                old.Email = account.Email;
                old.Role = account.Role;
                db.SaveChanges();
            }
            else
            {
                return NotFound();
            }

            return Ok();
        }


        //DELETE method
        public IHttpActionResult Delete(int id)
        {
            if (id == 0)
                return BadRequest("Not a valid student id");

            var acc = db.Accounts
                .Where(s => s.IDA == id)
                .FirstOrDefault();

            db.Entry(acc).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();

            return Ok();
        }
    }
}
