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
        public WareHouseService(IValidationDictionary validationDictionary)
            : this(validationDictionary, new WareHouseRepository(),new CouponRepository(),new DetailCouponRepository())
        {
        }
        public WareHouseService(IValidationDictionary validationDictionary, IWareHouseRepository warehouserepository,ICouponRepository couponrepository,IDetailCouponRepository detailCouponrepository)

        {
            _validationDictionary = validationDictionary;
            _warehouserepository = warehouserepository;
            _couponrepository = couponrepository;
            _detailCouponrepository = detailCouponrepository;
        }
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

        public IEnumerable ListWareHouse()
        {
            return _warehouserepository.listWareHouses();
        }
        public IEnumerable SearchWareHouse(String key, String Type)
        {
            ValidateString(key);
            if (Type == "Mã sản phẩm")
                return _warehouserepository.searchWareHouse(key);
            return _warehouserepository.searchWareHouseBy(key);
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
                _detailCouponrepository.deleteDetailCouponbyID(couponToDelete.MaPhieu);
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
        public IEnumerable ListCoupon()
        {
            return _couponrepository.ListCoupons();
        }
        public IEnumerable searchCoupon(String key, String type)
        {
            ValidateString(key);
            if (type == "Mã nhân viên")
                return _couponrepository.getCouponByEm(key);
            if (type == "Trạng thái")
                return _couponrepository.getCouponByStt(key);
            return _couponrepository.getCouponByID(key);
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
            }
            catch
            {
                return false;
            }
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
        public bool UpdateDetailCoupon(ChiTietPhieu ct)
        {
          //  if (!ValidateDetailCoupon(ct))
//return false;
            try
            {
                _detailCouponrepository.editDetailCoupon(ct);
            }
            catch { return false; }
            return true;
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
    

