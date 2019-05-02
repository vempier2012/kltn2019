namespace API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class NhanViensPhongBan
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
        public string IDNV { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string MaPB { get; set; }

        [StringLength(150)]
        public string Describe { get; set; }

        [StringLength(20)]
        public string MaChucVu { get; set; }

        public virtual DanhMucChucVu DanhMucChucVu { get; set; }

        public virtual Nhanvien Nhanvien { get; set; }

        public virtual PhongBan PhongBan { get; set; }
    }
}
