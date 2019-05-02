namespace Mini_Project_v1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PhongBan")]
    public partial class PhongBan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PhongBan()
        {
            NhanViensPhongBans = new HashSet<NhanViensPhongBan>();
        }

        [Key]
        [StringLength(20)]
        public string MaPB { get; set; }

        [StringLength(150)]
        public string TenPB { get; set; }

        [StringLength(150)]
        public string DiaDiem { get; set; }

        [StringLength(20)]
        public string SDT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NhanViensPhongBan> NhanViensPhongBans { get; set; }
    }
}
