namespace Mini_Project_v1.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DBContext : DbContext
    {
        public DBContext()
            : base("name=DBContext")
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<DanhMucChucVu> DanhMucChucVus { get; set; }
        public virtual DbSet<LuongNV> LuongNVs { get; set; }
        public virtual DbSet<Nhanvien> Nhanviens { get; set; }
        public virtual DbSet<NhanViensPhongBan> NhanViensPhongBans { get; set; }
        public virtual DbSet<PhongBan> PhongBans { get; set; }
        public virtual DbSet<RoleAccount> RoleAccounts { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<XepLoaiNV> XepLoaiNVs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .Property(e => e.IDNV)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.Taikhoan)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.Matkhau)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.Role)
                .IsUnicode(false);

            modelBuilder.Entity<DanhMucChucVu>()
                .Property(e => e.MaChucVu)
                .IsUnicode(false);

            modelBuilder.Entity<LuongNV>()
                .Property(e => e.ID)
                .IsUnicode(false);

            modelBuilder.Entity<LuongNV>()
                .Property(e => e.HeSoLuong)
                .HasPrecision(5, 2);

            modelBuilder.Entity<LuongNV>()
                .Property(e => e.HeSoPhuCap)
                .HasPrecision(5, 2);

            modelBuilder.Entity<Nhanvien>()
                .Property(e => e.IDNV)
                .IsUnicode(false);

            modelBuilder.Entity<Nhanvien>()
                .Property(e => e.GioiTinh)
                .IsUnicode(false);

            modelBuilder.Entity<Nhanvien>()
                .Property(e => e.CMND)
                .IsUnicode(false);

            modelBuilder.Entity<Nhanvien>()
                .Property(e => e.SDT)
                .IsUnicode(false);

            modelBuilder.Entity<Nhanvien>()
                .Property(e => e.MaLoaiNV)
                .IsUnicode(false);

            modelBuilder.Entity<Nhanvien>()
                .HasMany(e => e.LuongNVs)
                .WithOptional(e => e.Nhanvien)
                .HasForeignKey(e => e.ID);

            modelBuilder.Entity<Nhanvien>()
                .HasMany(e => e.NhanViensPhongBans)
                .WithRequired(e => e.Nhanvien)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NhanViensPhongBan>()
                .Property(e => e.IDNV)
                .IsUnicode(false);

            modelBuilder.Entity<NhanViensPhongBan>()
                .Property(e => e.MaPB)
                .IsUnicode(false);

            modelBuilder.Entity<NhanViensPhongBan>()
                .Property(e => e.Describe)
                .IsUnicode(false);

            modelBuilder.Entity<NhanViensPhongBan>()
                .Property(e => e.MaChucVu)
                .IsUnicode(false);

            modelBuilder.Entity<PhongBan>()
                .Property(e => e.MaPB)
                .IsUnicode(false);

            modelBuilder.Entity<PhongBan>()
                .Property(e => e.SDT)
                .IsUnicode(false);

            modelBuilder.Entity<PhongBan>()
                .HasMany(e => e.NhanViensPhongBans)
                .WithRequired(e => e.PhongBan)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RoleAccount>()
                .Property(e => e.Role)
                .IsUnicode(false);

            modelBuilder.Entity<RoleAccount>()
                .Property(e => e.MaChucVu)
                .IsUnicode(false);

            modelBuilder.Entity<XepLoaiNV>()
                .Property(e => e.MaLoaiNV)
                .IsUnicode(false);
        }
    }
}
