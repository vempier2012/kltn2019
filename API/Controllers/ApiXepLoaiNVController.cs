using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Mini_Project_v1.Controllers
{
    public class ApiXepLoaiNVController : ApiController
    {
        DBContext db = new DBContext();

        public IHttpActionResult GetAllRate()
        {
            var rate = db.XepLoaiNVs;
            List<XepLoaiNV> xepLoaiNVs = new List<XepLoaiNV>();

            foreach (var a in rate)
            {
                XepLoaiNV x = new XepLoaiNV
                {
                    MaLoaiNV = a.MaLoaiNV,
                    TenLoaiNV = a.TenLoaiNV,
                    MoTa = a.MoTa
                };
                xepLoaiNVs.Add(x);
            }
            if (xepLoaiNVs.Count == 0)
            {
                return NotFound();
            }
            return Ok(xepLoaiNVs.ToList());
        }

        public IHttpActionResult GetRatebyID(string id)
        {
            var rate = db.XepLoaiNVs;
            XepLoaiNV xepLoaiNV = null;

            foreach (var a in rate.Where(s => s.MaLoaiNV == id))
            {
                XepLoaiNV x = new XepLoaiNV
                {
                    MaLoaiNV = a.MaLoaiNV,
                    TenLoaiNV = a.TenLoaiNV,
                    MoTa = a.MoTa
                };
                xepLoaiNV = x;
            }
            if (xepLoaiNV == null)
            {
                return NotFound();
            }
            return Ok(xepLoaiNV);
        }

        public IHttpActionResult GetRatebyName(string name)
        {
            var rate = db.XepLoaiNVs;
            XepLoaiNV xepLoaiNV = null;

            foreach (var a in rate.Where(s => s.TenLoaiNV == name))
            {
                XepLoaiNV x = new XepLoaiNV
                {
                    MaLoaiNV = a.MaLoaiNV,
                    TenLoaiNV = a.TenLoaiNV,
                    MoTa = a.MoTa
                };
                xepLoaiNV = x;
            }
            if (xepLoaiNV == null)
            {
                return NotFound();
            }
            return Ok(xepLoaiNV);
        }


        //POST method
        public IHttpActionResult PostNewEmpType(XepLoaiNV xepLoaiNV)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid Data.");

            db.XepLoaiNVs.Add(new XepLoaiNV()
            {
                MaLoaiNV = xepLoaiNV.MaLoaiNV,
                TenLoaiNV = xepLoaiNV.TenLoaiNV,
                MoTa = xepLoaiNV.MoTa
            });
            db.SaveChanges();

            return Ok();
        }

        //PUT
        public IHttpActionResult Put(XepLoaiNV xepLoaiNV)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");


            var old = db.XepLoaiNVs.Where(s => s.MaLoaiNV == xepLoaiNV.MaLoaiNV)
                                                    .FirstOrDefault<XepLoaiNV>();

            if (old != null)
            {
                old.TenLoaiNV = xepLoaiNV.TenLoaiNV;
                old.MoTa = xepLoaiNV.MoTa;
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

            var type = db.XepLoaiNVs
                .Where(s => s.MaLoaiNV == id)
                .FirstOrDefault();

            db.Entry(type).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();

            return Ok();
        }
    }
}
