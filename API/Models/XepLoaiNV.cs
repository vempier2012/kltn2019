namespace API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("XepLoaiNV")]
    public partial class XepLoaiNV
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public XepLoaiNV()
        {
            Nhanviens = new HashSet<Nhanvien>();
        }

        [Key]
        [StringLength(20)]
        public string MaLoaiNV { get; set; }

        [StringLength(50)]
        public string TenLoaiNV { get; set; }

        [StringLength(150)]
        public string MoTa { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Nhanvien> Nhanviens { get; set; }
    }
}
