namespace API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Account")]
    public partial class Account
    {
        [Key]
        public int IDA { get; set; }

        [StringLength(20)]
        public string IDNV { get; set; }

        [StringLength(20)]
        public string Taikhoan { get; set; }

        [StringLength(10)]
        public string Matkhau { get; set; }

        [StringLength(30)]
        public string Email { get; set; }

        [StringLength(20)]
        public string Role { get; set; }

        public virtual Nhanvien Nhanvien { get; set; }

        public virtual RoleAccount RoleAccount { get; set; }
    }
}
