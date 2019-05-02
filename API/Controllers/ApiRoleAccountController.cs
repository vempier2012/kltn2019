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
    public class ApiRoleAccountController : ApiController
    {
        DBContext db = new DBContext();

        public IHttpActionResult GetAllRoleAccounts()
        {
            var roles = db.RoleAccounts.Include(s => s.DanhMucChucVu);
            List<RoleAccount> rol = new List<RoleAccount>();

            foreach(var a in roles)
            {
                RoleAccount x = new RoleAccount
                {
                    Role = a.Role,
                    TenRole = a.TenRole,
                    MoTa = a.MoTa,
                    MaChucVu = a.DanhMucChucVu.MaChucVu
                };
                rol.Add(x);
            }
            if (rol.Count == 0)
            {
                return NotFound();
            }
            return Ok(rol.ToList());
        }

        public IHttpActionResult GetAllRoleAccounts(string id)
        {
            var roles = db.RoleAccounts.Include(s => s.DanhMucChucVu);
            RoleAccount rol = null;

            foreach (var a in roles.Where(x => x.Role == id))
            {
                RoleAccount x = new RoleAccount
                {
                    Role = a.Role,
                    TenRole = a.TenRole,
                    MoTa = a.MoTa,
                    MaChucVu = a.DanhMucChucVu.MaChucVu
                };
                rol = x;
            }
            if (rol.Role == null)
            {
                return NotFound();
            }
            return Ok(rol);
        }

        //POST method
        public IHttpActionResult PostNewPhongBan(RoleAccount roleAccount)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid Data.");

            db.RoleAccounts.Add(new RoleAccount()
            {
                Role = roleAccount.Role,
                TenRole = roleAccount.TenRole,
                MoTa = roleAccount.MoTa,
                MaChucVu = roleAccount.MaChucVu
            });
            db.SaveChanges();

            return Ok();
        }

        //PUT
        public IHttpActionResult Put(RoleAccount roleAccount)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");


            var old = db.RoleAccounts.Where(s => s.Role == roleAccount.Role)
                                                    .FirstOrDefault<RoleAccount>();

            if (old != null)
            {
                old.Role = roleAccount.Role;
                old.TenRole = roleAccount.TenRole;
                old.MaChucVu = roleAccount.MaChucVu;
                old.MoTa = roleAccount.MoTa;

                db.SaveChanges();
            }
            else
            {
                return NotFound();
            }

            return Ok();
        }


        //DELETE method
        public IHttpActionResult Delete(string role)
        {
            if (role == null)
                return BadRequest("Not a valid student id");

            var rol = db.RoleAccounts
                .Where(s => s.Role == role)
                .FirstOrDefault();

            db.Entry(rol).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();

            return Ok();
        }
    }
}
