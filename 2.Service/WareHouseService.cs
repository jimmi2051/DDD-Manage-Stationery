using System;
using System.Collections.Generic;
using System.Linq;
using MyProject.Repository;
using MyProject.Domain;
using MyProject.Infrastructure;
using System.Collections;
using System.Text.RegularExpressions;
namespace MyProject.Service
{
    #region Sort
    class WareHouseCompareASC : IComparer<Kho>
    {
        public int Compare(Kho a, Kho b)
        {
            return a.SoLuong.Value.CompareTo(b.SoLuong.Value);
        }
    }
    class WareHouseCompareDESC : IComparer<Kho>
    {
        public int Compare(Kho a, Kho b)
        {
            return b.SoLuong.Value.CompareTo(a.SoLuong.Value);
        }
    }
    #endregion
    public class WareHouseService : IWareHouseService
    {
        private IValidationDictionary _validationDictionary;
        private IWareHouseRepository _warehouserepository;
        private ICouponRepository _couponrepository;
        private IDetailCouponRepository _detailCouponrepository;
        private IProductRepository _productrepository;
        public WareHouseService(IValidationDictionary validationDictionary)
            : this(validationDictionary, new WareHouseRepository(),new CouponRepository(),new DetailCouponRepository(),new ProductRepository())
        {
        }
        public WareHouseService(IValidationDictionary validationDictionary, IWareHouseRepository warehouserepository, ICouponRepository couponrepository, IDetailCouponRepository detailCouponrepository,IProductRepository productRepository)

        {
            _validationDictionary = validationDictionary;
            _warehouserepository = warehouserepository;
            _couponrepository = couponrepository;
            _detailCouponrepository = detailCouponrepository;
            _productrepository = productRepository;
        }
        #region Validatetion
        public bool ValidateString(String key)
        {
            _validationDictionary.Clear();
            if (key.Trim().Length > 0 && !Regex.IsMatch(key, @"\w"))
                _validationDictionary.AddError("Timkiem", "Vui lòng nhập từ khóa hợp lệ");
            return _validationDictionary.IsValid;
        }
        public bool ValidatetoWarehouse(Kho warehoustToValidate)
        {
            _validationDictionary.Clear();
            if (warehoustToValidate.SoLuong < 0)
                _validationDictionary.AddError("Soluong", "Vui lòng nhập số lượng hợp lệ");
            return _validationDictionary.IsValid;
        }
        #endregion
        #region WareHouse
        public bool CreateWareHouse(Kho warehousetoCreate)
        {
            if (!ValidatetoWarehouse(warehousetoCreate))
                return false;
            try
            {
                _warehouserepository.CreateWareHouse(warehousetoCreate);
            }
            catch
            {
                return false;
            }
            return true;
        }
        public bool EditWareHouse(Kho warehouseToEdit)
        {
            if (!ValidatetoWarehouse(warehouseToEdit))
                return false;
            try
            {
                _warehouserepository.UpdateWareHouse(warehouseToEdit);
            }
            catch
            {
                return false;
            }
            return true;
        }
        public bool DeleteWareHouse(Kho warehouseToDelete)
        {
            try
            {
                _warehouserepository.DeleteWareHouse(warehouseToDelete);
            }
            catch
            {
                return false;
            }
            return true;
        }
        public Kho getWareHouse(String msp)
        {
            return _warehouserepository.getWareHouse(msp);
        }
        IEnumerable listWarehouse;
        public IEnumerable ListWareHouse()
        {
            return listWarehouse=_warehouserepository.listWareHouses();
        }
        public IEnumerable SearchWareHouse(String key, String Type)
        {
            ValidateString(key);
            List<Kho> result = new List<Kho>();
            if (Type == "Mã sản phẩm")
                foreach (Kho item in listWarehouse)
                {
                    if (item.MaSP.Contains(key))
                        result.Add(item);
                }
            if (Type == "Mã phiếu")
                foreach (Kho item in listWarehouse)
                {
                    if (item.MaPhieu.Contains(key))
                        result.Add(item);
                }
            return result;
        }
        public IEnumerable Sort(String key) 
        {
            List<Kho> list = _warehouserepository.listWareHouses().ToList();
            if (key == "1")            
                list.Sort(new WareHouseCompareASC());            
            if(key == "2")
                list.Sort(new WareHouseCompareDESC());
            return list;
        }
        #endregion
        #region Coupon
        public bool CreateCoupon(PhieuNhapXuat couponToCreate)
        { 
            //Kiểm tra logic
            //Kiểm tra database
            try
            {
                _couponrepository.CreateCoupon(couponToCreate);
            }
            catch
            {
                return false;
            }
            return true;
            
        }
        public bool EditCoupon(PhieuNhapXuat couponToEdit)
        { 
            //Kiểm tra logic
            //kiểm tra database
            try
            {
                _couponrepository.EditCoupon(couponToEdit);
            }
            catch
            {
                return false;
            }
            return true;
        }
        public bool DeleteCoupon(PhieuNhapXuat couponToDelete)
        {
            try
            {              
                _couponrepository.DeleteCoupon(couponToDelete);
            }
            catch
            {
                return false;
            }
            return true;
        }
        public PhieuNhapXuat getCoupon(String key)
        {
            return _couponrepository.GetCoupon(key);
        }
        IEnumerable listCoupon;
        public IEnumerable ListCoupon()
        {
            return listCoupon=_couponrepository.ListCoupons();
        }
        public IEnumerable searchCoupon(String key, String type)
        {
            ValidateString(key);
            List<PhieuNhapXuat> result = new List<PhieuNhapXuat>();
            if (type == "Mã nhân viên")
                foreach (PhieuNhapXuat item in listCoupon)
                {
                    if (item.MaNV.Contains(key))
                        result.Add(item);
                }
            if (type == "Trạng thái")
                foreach (PhieuNhapXuat item in listCoupon)
                {
                    if (item.TrangThai.Contains(key))
                        result.Add(item);
                }
            if (type == "Mã phiếu")
                foreach (PhieuNhapXuat item in listCoupon)
                {
                    if (item.MaPhieu.Contains(key))
                        result.Add(item);
                }
            return result;
        }
        public PhieuNhapXuat createNewCoupon()
        {
            PhieuNhapXuat target = _couponrepository.ListCoupons().LastOrDefault();
            if (target != null)
            {
                int SoPhieu = int.Parse(target.MaPhieu.Substring(2));
                SoPhieu++;
                string newMaPhieu;
                if (SoPhieu < 10)
                {
                    newMaPhieu = "MP00" + SoPhieu.ToString();
                }
                else if (SoPhieu < 100)
                {
                    newMaPhieu = "MP0" + SoPhieu.ToString();
                }
                else newMaPhieu = "MP" + SoPhieu.ToString();
                return new PhieuNhapXuat()
                {
                    MaPhieu = newMaPhieu,
                    MaNV = Information.Nhanvien.MaNV,
                    NgayLap = DateTime.Now,
                    TongTien = 0,
                    TrangThai = null,
                };
            }
            else 
            {
                return new PhieuNhapXuat()
                {
                    MaPhieu = "MP001",
                    MaNV = Information.Nhanvien.MaNV,
                    NgayLap=DateTime.Now,
                    TongTien = 0,
                    TrangThai = null,
                };
            }
        }
        #endregion
        #region Product
        IEnumerable listToSearch;
        public IEnumerable ListProducts()
        {
            return listToSearch = _productrepository.ListProducts();
        }
        public IEnumerable SearchProducts(String Key, String Type, decimal pricestart, decimal priceend)
        {
            ValidateString(Key);
            List<SanPham> result = new List<SanPham>();
            if (Type == "Mã nhà cung cấp")
                foreach (SanPham item in listToSearch)
                {
                    if (item.MaNCC.Contains(Key) && item.DonGia >= pricestart && item.DonGia <= priceend)
                        result.Add(item);
                }
            if (Type == "Loại sản phẩm")
                foreach (SanPham item in listToSearch)
                {
                    if (item.DanhMucSP.TenDM.Contains(Key) && item.DonGia >= pricestart && item.DonGia <= priceend)
                        result.Add(item);
                }
            if (Type == "Tên sản phẩm")
                foreach (SanPham item in listToSearch)
                {
                    if (item.TenSP.Contains(Key) && item.DonGia >= pricestart && item.DonGia <= priceend)
                        result.Add(item);
                }
            if (Type == "Mã sản phẩm")
                foreach (SanPham item in listToSearch)
                {
                    if (item.MaSP.Contains(Key) && item.DonGia >= pricestart && item.DonGia <= priceend)
                        result.Add(item);
                }
            return result;
        }
        public SanPham GetProduct(String Key)
        {
            return _productrepository.GetProduct(Key);
        }
        #endregion
        #region DetailCoupon     
        public IEnumerable listDetailCoupon(String key)
        {
            return _detailCouponrepository.ListDetailCouponByID(key);
        }
        public bool CreateDetailCoupon(ChiTietPhieu DetailCouponToCreate)
        {
            //            if (!ValidateDetailCoupon(DetailCouponToCreate))
            //   return false;
            try
            {
                _detailCouponrepository.createDetailCoupon(DetailCouponToCreate);
                SanPham product = _productrepository.GetProduct(DetailCouponToCreate.MaSP);
                Kho target = getWareHouse(DetailCouponToCreate.MaSP);
                PhieuNhapXuat Phieu = getCoupon(DetailCouponToCreate.MaPhieu);
                if (Phieu.TrangThai.Equals("Nhập"))
                {
                    target.SoLuong = target.SoLuong + DetailCouponToCreate.SoLuong;
                    target.NgayLap = Phieu.NgayLap;
                }
                else
                {
                    target.SoLuong = target.SoLuong - DetailCouponToCreate.SoLuong;
                    product.SoLuong = product.SoLuong+ DetailCouponToCreate.SoLuong;
                    target.NgayXuat = Phieu.NgayLap;
                    _productrepository.EditProduct(product);
                }               
                _warehouserepository.UpdateWareHouse(target);
            }
            catch
            {
                return false;
            }
            return true;
        }
        public bool UpdateDetailCoupon(ChiTietPhieu DetailCouponToEdit)
        {
            try
            {
                SanPham product = _productrepository.GetProduct(DetailCouponToEdit.MaSP);
                Kho target = getWareHouse(DetailCouponToEdit.MaSP);
                PhieuNhapXuat Phieu = getCoupon(DetailCouponToEdit.MaPhieu);
                ChiTietPhieu chitiet = getDetailCoupon(DetailCouponToEdit.MaPhieu, DetailCouponToEdit.MaSP);
                if (Phieu.TrangThai.Equals("Nhập"))
                {
                    target.SoLuong = target.SoLuong - chitiet.SoLuong + DetailCouponToEdit.SoLuong;
                    target.NgayLap = Phieu.NgayLap;
                }
                else
                {
                    target.SoLuong = target.SoLuong + chitiet.SoLuong - DetailCouponToEdit.SoLuong;
                    product.SoLuong = product.SoLuong - chitiet.SoLuong + DetailCouponToEdit.SoLuong;
                    target.NgayXuat = Phieu.NgayLap;
                    _productrepository.EditProduct(product);
                }             
                _warehouserepository.UpdateWareHouse(target);
            }
            catch { return false; }
            _detailCouponrepository.editDetailCoupon(DetailCouponToEdit);
            return true;
        }
        public bool DeleteDetailCoupon(ChiTietPhieu DetailCouponToDelete)
        {
            try
            {
                _detailCouponrepository.deleteDetailCoupon(DetailCouponToDelete);
            }
            catch
            {
                return false;
            }
            return true;
        }
        public ChiTietPhieu getDetailCoupon(String ID, String key)
        {
            return _detailCouponrepository.getDetailCoupon(ID, key);
        }
 
        public bool DeleteDetailCouponByID(String key)
        {

            try
            {
                _detailCouponrepository.deleteDetailCouponbyID(key);
            }
            catch { return false; }
            return true;
        }
        #endregion
        public bool checkAmount(Kho warehouse, int target)
        {
            if (warehouse.SoLuong < target)
                return false;
            return true;
        }
    
        public List<Kho> Statistical(int Type,String MaSP)
        {
            string sqlcmd = "SELECT * FROM Kho ";
            if (MaSP != null)
                sqlcmd += "Where MaSP='" + MaSP + "'";
            if(Type==1)
                sqlcmd += " ORDER BY SoLuong ASC ";
            if(Type == 2 )
                sqlcmd += " ORDER BY SoLuong DESC ";
            if(Type == 3 )
                sqlcmd += "ORDER BY NgayLap DESC";
            if(Type == 4 )
                sqlcmd += "ORDER BY NgayXuat DESC";
            return _warehouserepository.StatisticalWareHouse(sqlcmd).ToList();
        }
    }
}
    

