using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.Entity;

namespace Mini_Project_v1.Controllers
{
    public class ApiPhongbanController : ApiController
    {
        DBContext db = new DBContext();

        //GET Method
        public IHttpActionResult GetAllPhongBans()
        {
            var phongbans = db.PhongBans;
            List<PhongBan> alist = new List<PhongBan>();

            foreach (var a in phongbans)
            {
                PhongBan x = new PhongBan
                {
                    MaPB = a.MaPB,
                    TenPB = a.TenPB,
                    DiaDiem = a.DiaDiem,
                    SDT = a.SDT
                };
                alist.Add(x);
            }
            if (alist.Count() == 0)
            {
                return NotFound();
            }
            return Ok(alist);
        }

        public IHttpActionResult GetPhongBanById(string id)
        {
            var phongbans = db.PhongBans;
            PhongBan depart = null;

            foreach (var a in phongbans.Where(s => s.MaPB == id))
            {
                PhongBan x = new PhongBan
                {
                    MaPB = a.MaPB,
                    TenPB = a.TenPB,
                    DiaDiem = a.DiaDiem,
                    SDT = a.SDT
                };
                depart = x;
            }
            if (depart == null)
            {
                return NotFound();
            }
            return Ok(depart);
        }

        public IHttpActionResult GetAllPhongBans(string departmentname)
        {
            var phongbans = db.PhongBans;
            PhongBan depart = null;

            foreach (var a in phongbans.Where(s => s.TenPB == departmentname))
            {
                PhongBan x = new PhongBan
                {
                    MaPB = a.MaPB,
                    TenPB = a.TenPB,
                    DiaDiem = a.DiaDiem,
                    SDT = a.SDT
                };
                depart = x;
            }
            if (depart == null)
            {
                return NotFound();
            }
            return Ok(depart);
        }

        //POST method
        public IHttpActionResult PostNewPhongBan(PhongBan phongBan)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid Data.");

            db.PhongBans.Add(new PhongBan()
            {
                MaPB = phongBan.MaPB,
                TenPB = phongBan.TenPB,
                DiaDiem = phongBan.DiaDiem,
                SDT = phongBan.SDT
            });
            db.SaveChanges();

            return Ok();
        }

        //PUT
        public IHttpActionResult Put(PhongBan phongBan)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            
            var oldPhongBan = db.PhongBans.Where(s => s.MaPB == phongBan.MaPB)
                                                    .FirstOrDefault<PhongBan>();

            if (oldPhongBan != null)
            {
                oldPhongBan.TenPB = phongBan.TenPB;
                oldPhongBan.DiaDiem = phongBan.DiaDiem;
                oldPhongBan.SDT = phongBan.SDT;

                db.SaveChanges();
            }
            else
            {
                return NotFound();
            }            

            return Ok();
        }


        //DELETE method
        public IHttpActionResult Delete(string id)
        {
            if (id == null)
                return BadRequest("Not a valid student id");
                        
            var phongban = db.PhongBans
                .Where(s => s.MaPB == id)
                .FirstOrDefault();

            db.Entry(phongban).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();            

            return Ok();
        }
    }
}
