namespace Mini_Project_v1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RoleAccount")]
    public partial class RoleAccount
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RoleAccount()
        {
            Accounts = new HashSet<Account>();
        }

        [Key]
        [StringLength(20)]
        public string Role { get; set; }

        [StringLength(50)]
        public string TenRole { get; set; }

        [StringLength(120)]
        public string MoTa { get; set; }

        [StringLength(20)]
        public string MaChucVu { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Account> Accounts { get; set; }

        public virtual DanhMucChucVu DanhMucChucVu { get; set; }
    }
}
