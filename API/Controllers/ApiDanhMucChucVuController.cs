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
    public class ApiDanhMucChucVuController : ApiController
    {
        DBContext db = new DBContext();

        public IHttpActionResult GetAllDMCV()
        {
            var dmcv = db.DanhMucChucVus;
            List<DanhMucChucVu> DanhMucChucVus = new List<DanhMucChucVu>();

            foreach (var a in dmcv)
            {
                DanhMucChucVu x = new DanhMucChucVu
                {
                    MaChucVu = a.MaChucVu,
                    TenChucVu = a.TenChucVu,
                    MoTa = a.MoTa
                };
                DanhMucChucVus.Add(x);
            }
            if(DanhMucChucVus.Count == 0)
            {
                return NotFound();
            }
            return Ok(DanhMucChucVus.ToList());
        }

        public IHttpActionResult GetDMCVbyId(string id)
        {
            var dmcv = db.DanhMucChucVus;
            DanhMucChucVu DanhMucChucVu = null;

            foreach (var a in dmcv.Where(s => s.MaChucVu == id))
            {
                DanhMucChucVu x = new DanhMucChucVu
                {
                    MaChucVu = a.MaChucVu,
                    TenChucVu = a.TenChucVu,
                    MoTa = a.MoTa
                };
                DanhMucChucVu = x;
            }
            if (DanhMucChucVu == null)
            {
                return NotFound();
            }
            return Ok(DanhMucChucVu);
        }

        public IHttpActionResult GetDMCVbyName(string name)
        {
            var dmcv = db.DanhMucChucVus;
            DanhMucChucVu DanhMucChucVu = null;

            foreach (var a in dmcv.Where(s => s.TenChucVu == name))
            {
                DanhMucChucVu x = new DanhMucChucVu
                {
                    MaChucVu = a.MaChucVu,
                    TenChucVu = a.TenChucVu,
                    MoTa = a.MoTa
                };
                DanhMucChucVu = x;
            }
            if (DanhMucChucVu == null)
            {
                return NotFound();
            }
            return Ok(DanhMucChucVu);
        }

        //POST method
        public IHttpActionResult Post(DanhMucChucVu DanhMucChucVu)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid Data.");
            
            db.DanhMucChucVus.Add(new DanhMucChucVu
            {
                MaChucVu = DanhMucChucVu.MaChucVu,
                TenChucVu = DanhMucChucVu.TenChucVu,
                MoTa = DanhMucChucVu.MoTa
            });
            db.SaveChanges();

            return Ok();
        }

        //PUT
        public IHttpActionResult Put(DanhMucChucVu DanhMucChucVu)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");


            var old = db.DanhMucChucVus.Where(s => s.MaChucVu == DanhMucChucVu.MaChucVu)
                                                    .FirstOrDefault<DanhMucChucVu>();

            if (old != null)
            {
                old.TenChucVu = DanhMucChucVu.TenChucVu;
                old.MoTa = DanhMucChucVu.MoTa;
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

            var danhmuc = db.DanhMucChucVus
                .Where(s => s.MaChucVu == id)
                .FirstOrDefault();

            db.Entry(danhmuc).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();

            return Ok();
        }
    }
}
