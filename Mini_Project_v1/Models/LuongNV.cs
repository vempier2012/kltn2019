namespace Mini_Project_v1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LuongNV")]
    public partial class LuongNV
    {
        [StringLength(20)]
        public string ID { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BacLuong { get; set; }

        public decimal? LuongCoBan { get; set; }

        public decimal? HeSoLuong { get; set; }

        public decimal? HeSoPhuCap { get; set; }

        public decimal? LuongThuong { get; set; }

        public decimal? LuongTru { get; set; }

        public decimal? LuongTong { get; set; }

        public virtual Nhanvien Nhanvien { get; set; }
    }
}
