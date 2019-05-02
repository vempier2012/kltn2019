namespace Mini_Project_v1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Nhanvien")]
    public partial class Nhanvien
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Nhanvien()
        {
            Accounts = new HashSet<Account>();
            LuongNVs = new HashSet<LuongNV>();
            NhanViensPhongBans = new HashSet<NhanViensPhongBan>();
        }

        [Key]
        [StringLength(20)]
        public string IDNV { get; set; }

        [StringLength(50)]
        [RegularExpression(@"^[^<>.,?;:'()!~@#/*""%_-]+$", ErrorMessage = "Không được sử dụng ký tự đặc biệt.")]
        public string Ten { get; set; }

        [StringLength(100)]
        [RegularExpression(@"^[^<>.,?;:'()!~@#/*""%_-]+$", ErrorMessage = "Không được sử dụng ký tự đặc biệt.")]
        public string Ho { get; set; }

        [Column(TypeName = "date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? NgaySinh { get; set; }

        [StringLength(10)]
        public string GioiTinh { get; set; }

        [StringLength(30)]
        public string CMND { get; set; }

        [StringLength(20)]
        public string SDT { get; set; }

        [StringLength(20)]
        public string MaLoaiNV { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Account> Accounts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LuongNV> LuongNVs { get; set; }

        public virtual XepLoaiNV XepLoaiNV { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NhanViensPhongBan> NhanViensPhongBans { get; set; }
    }
}
