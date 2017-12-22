using MyProject.Domain;
using MyProject.Service;
using MyProject.Infrastructure;
using Moq;
using System.Collections.Generic;
using System;

namespace MyProjectTests.Service
{
    class ServiceFactory
    {
        private static Mock<IProductRepository> _mockProductRepository;
        private static Mock<ISupplierRepository> _mockSupplierRepository;
        private static Mock<IEmployeeRepository> _mockEmployeeRepository;
        private static Mock<ICategoryRepository> _mockCategoryRepository;
        private static Mock<IUserRepository> _mockUserRepository;
        public static Manager_Service getManagerService(ModelStateDictionary ModelState)
        {
            _mockProductRepository = new Mock<IProductRepository>();
            _mockSupplierRepository = new Mock<ISupplierRepository>();
            _mockEmployeeRepository = new Mock<IEmployeeRepository>();
            _mockCategoryRepository = new Mock<ICategoryRepository>();
            _mockUserRepository = new Mock<IUserRepository>();
           Manager_Service managerService = null;
            managerService = new Manager_Service(
                new ModelStateWrapper(ModelState),
                _mockProductRepository.Object,
                _mockSupplierRepository.Object,
                _mockEmployeeRepository.Object,
                _mockCategoryRepository.Object,
                _mockUserRepository.Object
                );
            return managerService;
        }
    }
    class QLVanPhong_Context
    {
        private IList<NguoiDung> Nguoidung;
        private IList<SanPham> Sanpham;
        private IList<HoaDon> Hoadon;
        private IList<NhanVien> Nhanvien;
        private IList<ChiTietHoaDon> Chitiethoadon;
        private IList<NhaCungCap> Nhacungcap;
        private QLVanPhong_Context()
        {
            Nhanvien = new List<NhanVien>()
            {
                new NhanVien { MaNV="NV001", Ten="Lý thành", Diachi="126 Hồng bàng", sdt="0168500421", ChucVu="Quản lý",Luong=50000 ,NguoiDungs=NguoiDung},
                new NhanVien { MaNV="NV002", Ten="Thảo nguyễn", Diachi="1 Ku tèo", sdt="0158912312", ChucVu="Nhân viên bán hàng",Luong=5000 ,NguoiDungs = NguoiDung}
            };
            Nguoidung = new List<NguoiDung>()
            {
                new NguoiDung {ID ="1",Pass = "e10adc3949ba59abbe56e057f20f883e",Mail="jimmi2051@gmail.com",MaNV="NV001", NhanVien= NhanVien[0]},
                new NguoiDung {ID ="2",Pass = "e10adc3949ba59abbe56e057f20f883e",Mail="jimmi2052@gmail.com",MaNV="NV002" ,NhanVien = NhanVien[1]}
            };
            Nhacungcap = new List<NhaCungCap>()
            {
                new NhaCungCap{  MaNCC="NC001", Ten="Cong ty cap nuoc", DiaChi ="O day ne", sdt= "01685000421",SanPhams = SanPham },
                new NhaCungCap {MaNCC = "NC002",Ten="Cong ty dai hoc",DiaChi ="273 AN duong vuong",sdt="01869363585",SanPhams=SanPham}
            };
            Sanpham = new List<SanPham>()
            {
                new SanPham{ MaSP="SP001",MaNCC="NC001",MaDM="DM001",TenSP ="Bút bi xanh",DonGia=50000,SoLuong=15,XuatXu="trung quoc", KichThuoc=2,TrongLuong=2, DonVi="Cây" , NhaCungCap = NhaCungCap[0]},
                new SanPham{ MaSP="SP002",MaNCC="NC001",MaDM="DM001",TenSP ="Bút bi đỏ",DonGia=5000,SoLuong=5,XuatXu="Lào", KichThuoc=1,TrongLuong=1, DonVi="Cây" ,NhaCungCap = NhaCungCap[0]},
                new SanPham{ MaSP="SP003",MaNCC="NC002",MaDM="DM002",TenSP ="kẹo xanh",DonGia=5000012,SoLuong=1,XuatXu="Viet nam", KichThuoc=3,TrongLuong=3, DonVi="Cuc" ,NhaCungCap = NhaCungCap[1]}
            };
            Hoadon = new List<HoaDon>()
            {
                new HoaDon { MaHD="HD001",NgayLap=DateTime.Now, MaKH="KH001",MaNV="NV001" ,TongTien = 510000, TrangThai="Đang chờ",ChiTietHoaDons = ChiTietHoaDon,NhanVien=NhanVien[0]}
            };

            Chitiethoadon = new List<ChiTietHoaDon>()
            {
                new ChiTietHoaDon{ MaHD="HD001", MaSP="SP001", DonGia=50000,SoLuong=10, HoaDon = HoaDon[0],SanPham = Sanpham[0]},
                new ChiTietHoaDon{ MaHD="HD001",MaSP="SP002",DonGia=5000,SoLuong=2 ,HoaDon = HoaDon[0],SanPham=SanPham[1]}

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
        internal IList<NhanVien> NhanVien
        {
            get { return Nhanvien; }
            set
            {
                if (NhanVien == null)
                    NhanVien = Nhanvien;
            }
        }

        internal IList<NguoiDung> NguoiDung { get { return Nguoidung; } }
        internal IList<SanPham> SanPham { get { return Sanpham; } }
        internal IList<HoaDon> HoaDon { get { return Hoadon; } }
        internal IList<ChiTietHoaDon> ChiTietHoaDon { get { return Chitiethoadon; } }
        internal IList<NhaCungCap> NhaCungCap { get { return Nhacungcap; } }
        internal void SaveChanges()
        {
            //TO DO: Persisting objects
        }
    }
}
