namespace MyProject.Domain
{
    using System.Data.Entity;

    public partial class QLVanPhongEntities : DbContext
    {
        private QLVanPhongEntities()
            : base("name=QLVanPhongEntities")
        {
        }
        private static QLVanPhongEntities instance;
        public static QLVanPhongEntities Instance
        {
            get
            {
                if (instance == null)
                    instance = new QLVanPhongEntities();
                return instance;
            }
        }
        public virtual DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; }
        public virtual DbSet<ChiTietPhieu> ChiTietPhieux { get; set; }
        public virtual DbSet<DanhMucSP> DanhMucSPs { get; set; }
        public virtual DbSet<HoaDon> HoaDons { get; set; }
        public virtual DbSet<KhachHang> KhachHangs { get; set; }
        public virtual DbSet<Kho> Khoes { get; set; }
        public virtual DbSet<MaKhuyenMai> MaKhuyenMais { get; set; }
        public virtual DbSet<NguoiDung> NguoiDungs { get; set; }
        public virtual DbSet<NhaCungCap> NhaCungCaps { get; set; }
        public virtual DbSet<NhanVien> NhanViens { get; set; }
        public virtual DbSet<PhieuNhapXuat> PhieuNhapXuats { get; set; }
        public virtual DbSet<SanPham> SanPhams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChiTietHoaDon>()
                .Property(e => e.MaHD)
                .IsUnicode(false);

            modelBuilder.Entity<ChiTietHoaDon>()
                .Property(e => e.MaSP)
                .IsUnicode(false);

            modelBuilder.Entity<ChiTietHoaDon>()
                .Property(e => e.DonGia)
                .HasPrecision(19, 4);

            modelBuilder.Entity<ChiTietPhieu>()
                .Property(e => e.MaPhieu)
                .IsUnicode(false);

            modelBuilder.Entity<ChiTietPhieu>()
                .Property(e => e.MaSP)
                .IsUnicode(false);

            modelBuilder.Entity<ChiTietPhieu>()
                .Property(e => e.DonGia)
                .HasPrecision(19, 4);

            modelBuilder.Entity<DanhMucSP>()
                .Property(e => e.MaDM)
                .IsUnicode(false);

            modelBuilder.Entity<HoaDon>()
                .Property(e => e.MaHD)
                .IsUnicode(false);

            modelBuilder.Entity<HoaDon>()
                .Property(e => e.MaNV)
                .IsUnicode(false);

            modelBuilder.Entity<HoaDon>()
                .Property(e => e.MaKH)
                .IsUnicode(false);

            modelBuilder.Entity<HoaDon>()
                .Property(e => e.TongTien)
                .HasPrecision(19, 4);

            modelBuilder.Entity<HoaDon>()
                .HasMany(e => e.ChiTietHoaDons)
                .WithRequired(e => e.HoaDon)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.MaKH)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.Sdt)
                .IsUnicode(false);

            modelBuilder.Entity<Kho>()
                .Property(e => e.MaSP)
                .IsUnicode(false);

            modelBuilder.Entity<Kho>()
                .Property(e => e.MaPhieu)
                .IsUnicode(false);

            modelBuilder.Entity<MaKhuyenMai>()
                .Property(e => e.MaKM)
                .IsUnicode(false);

            modelBuilder.Entity<NguoiDung>()
                .Property(e => e.ID)
                .IsUnicode(false);

            modelBuilder.Entity<NguoiDung>()
                .Property(e => e.Pass)
                .IsUnicode(false);

            modelBuilder.Entity<NguoiDung>()
                .Property(e => e.Mail)
                .IsUnicode(false);

            modelBuilder.Entity<NguoiDung>()
                .Property(e => e.MaNV)
                .IsUnicode(false);

            modelBuilder.Entity<NhaCungCap>()
                .Property(e => e.MaNCC)
                .IsUnicode(false);

            modelBuilder.Entity<NhaCungCap>()
                .Property(e => e.sdt)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.MaNV)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.sdt)
                .IsUnicode(false);

            modelBuilder.Entity<PhieuNhapXuat>()
                .Property(e => e.MaPhieu)
                .IsUnicode(false);

            modelBuilder.Entity<PhieuNhapXuat>()
                .Property(e => e.MaNV)
                .IsUnicode(false);

            modelBuilder.Entity<PhieuNhapXuat>()
                .Property(e => e.TongTien)
                .HasPrecision(19, 4);

            modelBuilder.Entity<PhieuNhapXuat>()
                .HasMany(e => e.ChiTietPhieux)
                .WithRequired(e => e.PhieuNhapXuat)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PhieuNhapXuat>()
                .HasMany(e => e.Khoes)
                .WithRequired(e => e.PhieuNhapXuat)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.MaSP)
                .IsUnicode(false);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.MaNCC)
                .IsUnicode(false);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.MaDM)
                .IsUnicode(false);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.DonGia)
                .HasPrecision(19, 4);

            modelBuilder.Entity<SanPham>()
                .HasMany(e => e.ChiTietHoaDons)
                .WithRequired(e => e.SanPham)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SanPham>()
                .HasMany(e => e.ChiTietPhieux)
                .WithRequired(e => e.SanPham)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SanPham>()
                .HasMany(e => e.Khoes)
                .WithRequired(e => e.SanPham)
                .WillCascadeOnDelete(false);
        }
    }
}
