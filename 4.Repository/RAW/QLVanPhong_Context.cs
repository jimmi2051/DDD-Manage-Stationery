using System;
using System.Collections.Generic;
namespace MyProject.Domain
{
    public class QLVanPhong_Context 
    {
        private IList<NguoiDung> Nguoidung;
        private IList<SanPham> Sanpham;
        private IList<HoaDon> Hoadon;
        private IList<NhanVien> Nhanvien;
        private IList<ChiTietHoaDon> Chitiethoadon;
        private IList<NhaCungCap> Nhacungcap;
        private IList<DanhMucSP> DanhmucSP;
        private IList<Kho> Kho;
        private IList<PhieuNhapXuat> Phieunhapxuat;
        private IList<ChiTietPhieu> Chitietphieu;
        private QLVanPhong_Context()
        {
            Chitietphieu = new List<ChiTietPhieu>()
            {
                new ChiTietPhieu {  MaPhieu="MP001", MaSP ="SP001", DonGia=15000,SoLuong=30 },
                new ChiTietPhieu {  MaPhieu="MP001", MaSP ="SP002", DonGia=5000,SoLuong=30 },
                new ChiTietPhieu {  MaPhieu="MP001", MaSP ="SP003", DonGia=3000,SoLuong=30 },
            };
            Phieunhapxuat = new List<PhieuNhapXuat>()
            {
                new PhieuNhapXuat{  MaPhieu="MP001", MaNV="NV001", TongTien = 2000000 , TrangThai = "Nhập", NgayLap = DateTime.Now }
            };
            
            Kho = new List<Kho>()
            {
                new Kho{ MaSP="SP001", MaPhieu = "MP001", SoLuong=30, NgayLap=DateTime.Now,NgayXuat = DateTime.Now },
                new Kho{ MaSP="SP002", MaPhieu = "MP001", SoLuong=30, NgayLap=DateTime.Now,NgayXuat = DateTime.Now },
                new Kho{ MaSP="SP003", MaPhieu = "MP001", SoLuong=30, NgayLap=DateTime.Now,NgayXuat = DateTime.Now }
            };
            DanhmucSP = new List<DanhMucSP>()
            {
                new DanhMucSP {MaDM = "DM001", TenDM = "Dụng cụ học tập", SoLuong=15},
                new DanhMucSP { MaDM ="DM002",TenDM ="Thực phẩm",SoLuong = 10}
            };
            Nhanvien = new List<NhanVien>()
            {
                new NhanVien { MaNV="NV001", Ten="Lý thành", Diachi="126 Hồng bàng, Phường 11, Quận 10", sdt="0168500421", ChucVu="Quản lý",Luong=15000000 },
                new NhanVien { MaNV="NV002", Ten="Thảo nguyễn", Diachi="1 Cống Quỳnh, Phường 5, Quận 3", sdt="0158912312", ChucVu="Nhân viên bán hàng",Luong=5000000},
                new NhanVien{ MaNV="NV003",Ten="Nguyễn Tí",Diachi = "12 Lê Lợi, Phường 1, Quận 1",sdt = "01761212321",ChucVu = "Quản lý kho",Luong = 10000000}
            };
            Nguoidung = new List<NguoiDung>()
            {
                new NguoiDung {ID ="1",Pass = "e10adc3949ba59abbe56e057f20f883e",Mail="jimmi2051@gmail.com",MaNV="NV001"},
                new NguoiDung {ID ="2",Pass = "e10adc3949ba59abbe56e057f20f883e",Mail="jimmi2052@gmail.com",MaNV="NV002"},
                    new NguoiDung {ID ="3",Pass = "e10adc3949ba59abbe56e057f20f883e",Mail="jimmi2053@gmail.com",MaNV="NV003"}
            };
            Nhacungcap = new List<NhaCungCap>()
            {
                new NhaCungCap{  MaNCC="NC001", Ten="Công ty ABC", DiaChi ="123 A, Phường X, Quận B", sdt= "01685000421"},
                new NhaCungCap {MaNCC = "NC002",Ten="Đại học Sài Gòn",DiaChi ="273 An Dương Vương, P3, Quận 5",sdt="01869363585"}
            };
            Sanpham = new List<SanPham>()
            {
                new SanPham{ MaSP="SP001",MaNCC="NC001",MaDM="DM001",TenSP ="Bút bi xanh",DonGia=15000,SoLuong=15,XuatXu="trung quoc", KichThuoc=2,TrongLuong=2, DonVi="Cây" },
                new SanPham{ MaSP="SP002",MaNCC="NC001",MaDM="DM001",TenSP ="Bút bi đỏ",DonGia=5000,SoLuong=5,XuatXu="Lào", KichThuoc=1,TrongLuong=1, DonVi="Cây" },
                new SanPham{ MaSP="SP003",MaNCC="NC002",MaDM="DM002",TenSP ="Kẹo xanh",DonGia=3000,SoLuong=1,XuatXu="Việt Nam", KichThuoc=3,TrongLuong=3, DonVi="Cục"}
            };
            Hoadon = new List<HoaDon>()
            {
                new HoaDon { MaHD="HD001",NgayLap=DateTime.Now, MaKH="KH001",MaNV="NV001" ,TongTien = 510000, TrangThai="Đang chờ"}
            };
           
            Chitiethoadon = new List<ChiTietHoaDon>()
            {
                new ChiTietHoaDon{ MaHD="HD001", MaSP="SP001", DonGia=15000,SoLuong=10},
                new ChiTietHoaDon{ MaHD="HD001",MaSP="SP002",DonGia=5000,SoLuong=2}
         
            };
            
           
        }
        private static QLVanPhong_Context instance;

        internal static QLVanPhong_Context Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new QLVanPhong_Context();
                }
                return instance;
            }
        }

        public virtual IList<ChiTietHoaDon> ChiTietHoaDons { get { return Chitiethoadon; } }
        public virtual IList<ChiTietPhieu> ChiTietPhieux { get { return Chitietphieu; } set { } }
        public virtual IList<DanhMucSP> DanhMucSPs { get { return DanhmucSP; } set { } }
        public virtual IList<HoaDon> HoaDons { get { return Hoadon; } }
        public virtual IList<KhachHang> KhachHangs { get; set; }
        public virtual IList<Kho> Khoes { get { return Kho; } set { } }
        public virtual IList<MaKhuyenMai> MaKhuyenMais { get; set; }
        public virtual IList<NguoiDung> NguoiDungs { get { return Nguoidung; }  }
        public virtual IList<NhaCungCap> NhaCungCaps { get { return Nhacungcap; } }
        public virtual IList<NhanVien> NhanViens { get { return Nhanvien; } }
        public virtual IList<PhieuNhapXuat> PhieuNhapXuats { get { return Phieunhapxuat; } set { } }
        public virtual IList<SanPham> SanPhams { get { return Sanpham; } }
        internal void SaveChanges()
        {
            //TO DO: Persisting objects
        }
    }
}
