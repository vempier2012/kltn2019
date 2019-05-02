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
    public class ApiNhanvienController : ApiController
    {
        
        DBContext db = new DBContext();

        //GET Method
        public IHttpActionResult GetAllNhanviens()
        {
            var nhanviens = db.Nhanviens.Include(a => a.XepLoaiNV);//chỉ cần include tất cả các bảng ra là có thể truy vấn hết trong for each
            List<Nhanvien> alist = new List<Nhanvien>();

            foreach (var a in nhanviens)
            {
                Nhanvien x = new Nhanvien
                {
                    IDNV = a.IDNV,
                    Ten = a.Ten,
                    Ho = a.Ho,
                    NgaySinh = a.NgaySinh,
                    GioiTinh = a.GioiTinh,
                    CMND = a.CMND,
                    SDT = a.SDT,
                    MaLoaiNV = a.XepLoaiNV.MaLoaiNV
                };
                alist.Add(x);
            }
            if (alist.Count() == 0)
            {
                return NotFound();
            }
            return Ok(alist.ToList());
        }
        
        public IHttpActionResult GetAllNhanviens(string name)//lấy nhân viên
        {
            var nhanviens = db.Nhanviens.Include(a => a.XepLoaiNV);//chỉ cần include tất cả các bảng ra là có thể truy vấn hết trong for each
            Nhanvien emp = null;

            foreach (var a in nhanviens.Where(s => s.Ten == name))
            {
                Nhanvien x = new Nhanvien
                {
                    IDNV = a.IDNV,
                    Ten = a.Ten,
                    Ho = a.Ho,
                    NgaySinh = a.NgaySinh,
                    GioiTinh = a.GioiTinh,
                    CMND = a.CMND,
                    SDT = a.SDT,
                    MaLoaiNV = a.XepLoaiNV.MaLoaiNV
                };
                emp = x;
            }
            if (emp == null)
            {
                return NotFound();
            }
            return Ok(emp);
        }

        //get nhân viên khi có id
        public IHttpActionResult NhanvienById(string id)//lấy nhân viên
        {
            var nhanviens = db.Nhanviens.Include(a => a.XepLoaiNV);//chỉ cần include tất cả các bảng ra là có thể truy vấn hết trong for each
            Nhanvien emp = null;

            foreach (var a in nhanviens.Where(s => s.IDNV == id))
            {
                Nhanvien x = new Nhanvien
                {
                    IDNV = a.IDNV,
                    Ten = a.Ten,
                    Ho = a.Ho,
                    NgaySinh = a.NgaySinh,
                    GioiTinh = a.GioiTinh,
                    CMND = a.CMND,
                    SDT = a.SDT,
                    MaLoaiNV = a.XepLoaiNV.MaLoaiNV
                };
                emp = x;
            }
            if (emp == null)
            {
                return NotFound();
            }
            return Ok(emp);
        }


        //GET all nhân viên trong cùng 1 phòng ban
        public IHttpActionResult GetAllNhanviensPhongBan(string phongban)//token có thể thay đổi
        {
            var nv_pb = db.NhanViensPhongBans.Include(a => a.Nhanvien).Include(a => a.PhongBan).Include(a => a.Nhanvien.XepLoaiNV);
            //var nhanviens = db.Nhanviens.Include(a => a.XepLoaiNV);
            List<Nhanvien> alist = new List<Nhanvien>();

            foreach (var a in nv_pb.Where(s => s.PhongBan.MaPB == phongban))
            {
                Nhanvien x = new Nhanvien
                {
                    IDNV = a.IDNV,
                    Ten = a.Nhanvien.Ten,
                    Ho = a.Nhanvien.Ho,
                    NgaySinh = a.Nhanvien.NgaySinh,
                    GioiTinh = a.Nhanvien.GioiTinh,
                    CMND = a.Nhanvien.CMND,
                    SDT = a.Nhanvien.SDT,
                    MaLoaiNV = a.Nhanvien.XepLoaiNV.MaLoaiNV
                };
                alist.Add(x);
            }
            if (alist.Count() == 0)
            {
                return NotFound();
            }
            return Ok(alist.ToList());
        }


        //POST method
        public IHttpActionResult Post(Nhanvien nhanvien)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid Data.");

            db.Nhanviens.Add(new Nhanvien
            {
                IDNV = nhanvien.IDNV,
                Ten = nhanvien.Ten,
                Ho = nhanvien.Ho,
                NgaySinh = nhanvien.NgaySinh,
                GioiTinh = nhanvien.GioiTinh,
                CMND = nhanvien.CMND,
                SDT = nhanvien.SDT,
                MaLoaiNV = nhanvien.MaLoaiNV
            });
            db.SaveChanges();

            return Ok();
        }

        //PUT
        public IHttpActionResult Put(Nhanvien nhanvien)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");


            var old = db.Nhanviens.Where(s => s.IDNV == nhanvien.IDNV)
                                                    .FirstOrDefault<Nhanvien>();

            if (old != null)
            {
                old.IDNV = nhanvien.IDNV;
                old.Ten = nhanvien.Ten;
                old.Ho = nhanvien.Ho;
                old.NgaySinh = nhanvien.NgaySinh;
                old.GioiTinh = nhanvien.GioiTinh;
                old.CMND = nhanvien.CMND;
                old.SDT = nhanvien.SDT;
                old.MaLoaiNV = nhanvien.MaLoaiNV;
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

            var nv = db.Nhanviens
                .Where(s => s.IDNV == id)
                .FirstOrDefault();

            db.Entry(nv).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();

            return Ok();
        }
    }
}
