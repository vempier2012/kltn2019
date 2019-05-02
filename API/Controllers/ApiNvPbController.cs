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
    public class ApiNvPbController : ApiController
    {
        DBContext db = new DBContext();

        public IHttpActionResult GetAllNVs_Pbs()
        {
            var nv_pb = db.NhanViensPhongBans.Include(a => a.DanhMucChucVu);//chỉ cần include tất cả các bảng ra là có thể truy vấn hết trong for each
            List<NhanViensPhongBan> alist = new List<NhanViensPhongBan>();

            foreach (var a in nv_pb)
            {
                NhanViensPhongBan x = new NhanViensPhongBan
                {
                    IDNV = a.IDNV,
                    MaPB = a.MaPB,
                    MaChucVu = a.MaChucVu,
                    Describe = a.Describe
                };
                alist.Add(x);
            }
            if (alist.Count() == 0)
            {
                return NotFound();
            }
            return Ok(alist.ToList());
        }

        //lấy các phòng ban mà nhân viên đó làm kèm chức vụ theo tên của nhân viên
        public IHttpActionResult GetAllPhongBanByName(string name)
        {
            //var nv_pb = db.NhanViensPhongBans.Include(a => a.DanhMucChucVu);
            List<NhanViensPhongBan> nv_pb = new List<NhanViensPhongBan>();

            foreach (var a in nv_pb.Where(s => s.Nhanvien.Ten == name))
            {
                NhanViensPhongBan x = new NhanViensPhongBan
                {
                    IDNV = a.IDNV,
                    MaPB = a.MaPB,
                    MaChucVu = a.DanhMucChucVu.MaChucVu,
                    Describe = a.Describe
                };
                nv_pb.Add(x);
            }
            if (nv_pb.Count() == 0)
            {
                return NotFound();
            }
            return Ok(nv_pb.ToList());
        }

        //get phòng ban khi có id nhân viên
        public IHttpActionResult GetAllPhongBanById(string idnv)
        {
            var nv_pb = db.NhanViensPhongBans.Include(a => a.Nhanvien).Include(a => a.PhongBan);
            List<PhongBan> dep = new List<PhongBan>();

            foreach (var a in nv_pb.Where(s => s.IDNV == idnv))
            {
                PhongBan x = new PhongBan
                {
                    MaPB = a.MaPB,
                    TenPB = a.PhongBan.TenPB,
                    DiaDiem = a.PhongBan.DiaDiem,
                    SDT = a.PhongBan.SDT
                };
                dep.Add(x);
            }
            if (dep.Count() == 0)
            {
                return NotFound();
            }
            return Ok(dep.ToList());
        }

        //get thông tin để kiểm tra trùng
        public IHttpActionResult Get(string idnv, string mapb)
        {
            var nv_pb = db.NhanViensPhongBans.Include(a => a.Nhanvien).Include(a => a.PhongBan).Include(a => a.DanhMucChucVu);
            NhanViensPhongBan nv = new NhanViensPhongBan();

            foreach (var a in nv_pb.Where(s => s.IDNV == idnv && s.MaPB == mapb))
            {
                NhanViensPhongBan x = new NhanViensPhongBan
                {
                    IDNV = a.IDNV,
                    MaPB = a.MaPB,
                    MaChucVu = a.MaChucVu,
                    Describe = a.Describe
                };
                nv = x;
            }
            if (nv.IDNV == null && nv.MaPB == null)
            {
                return NotFound();
            }
            return Ok(nv);
        }


        //GET all nhân viên trong cùng 1 phòng ban
        public IHttpActionResult GetAllNhanviensPhongBan(string phongban)//token có thể thay đổi
        {
            var nv_pb = db.NhanViensPhongBans.Include(a => a.Nhanvien).Include(a => a.PhongBan).Include(a => a.Nhanvien.XepLoaiNV);
            //var nhanviens = db.Nhanviens.Include(a => a.XepLoaiNV);
            List<Nhanvien> alist = new List<Nhanvien>();

            foreach (var a in nv_pb.Where(s => s.MaPB == phongban))
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
        public IHttpActionResult Post(NhanViensPhongBan nhanViensPhongBan)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid Data.");

            db.NhanViensPhongBans.Add(new NhanViensPhongBan
            {
                IDNV = nhanViensPhongBan.IDNV,
                MaPB = nhanViensPhongBan.MaPB,
                MaChucVu = nhanViensPhongBan.MaChucVu,
                Describe = nhanViensPhongBan.Describe
            });
            db.SaveChanges();

            return Ok();
        }

        //PUT
        public IHttpActionResult Put(NhanViensPhongBan nhanViensPhongBan)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");


            var old = db.NhanViensPhongBans.Where(s => s.IDNV == nhanViensPhongBan.IDNV && s.MaPB == nhanViensPhongBan.MaPB)
                                                    .FirstOrDefault<NhanViensPhongBan>();

            if (old != null)
            {
                old.MaChucVu = nhanViensPhongBan.MaChucVu;
                old.Describe = nhanViensPhongBan.Describe;
                db.SaveChanges();
            }
            else
            {
                return NotFound();
            }

            return Ok();
        }


        //DELETE method
        public IHttpActionResult Delete(string idnv, string mapb)
        {
            if (idnv == null && mapb == null)
                return BadRequest("Not a valid student id");

            var nv_pb = db.NhanViensPhongBans
                .Where(s => s.IDNV == idnv && s.MaPB == mapb)
                .FirstOrDefault();

            db.Entry(nv_pb).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();

            return Ok();
        }

    }
}
