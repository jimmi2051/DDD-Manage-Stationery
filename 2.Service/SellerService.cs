using System;
using System.Linq;
using MyProject.Domain;
using System.Collections;
using MyProject.Infrastructure;
using MyProject.Repository;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace MyProject.Service
{
    public class SellerService : ISellerService
    {
        private IValidationDictionary _validationDictionary;
        private IBillRepository _billrepository;
        private IProductRepository _productrepository;
        private IBillDetailRespository _billdetailrepository;
        private ICustomerRepository _customerrepository;
        private ICodeSalesRepository _codesrepository;

        public SellerService(IValidationDictionary validationDictionary)
            : this(validationDictionary, new BillRepository(), new ProductRepository(), new BillDetailRepository(), new CustomerRepository(),new CodeSalesRepository())
        { }
        public SellerService(IValidationDictionary validationDictionary, IBillRepository billrepository, IProductRepository productrespository, IBillDetailRespository billdetailrepository, ICustomerRepository customerrepository,ICodeSalesRepository coderepository)
        {
            _validationDictionary = validationDictionary;
            _billrepository = billrepository;
            _productrepository = productrespository;
            _billdetailrepository = billdetailrepository;
            _customerrepository = customerrepository;
            _codesrepository = coderepository;
        }
        #region Validation
        public bool ValidateDate(String DateStart, String DateEnd)
        {
            _validationDictionary.Clear();
            DateTime d1 = DateTime.Parse(DateStart);
            DateTime d2 = DateTime.Parse(DateEnd);
            if (d1 > d2 || d1 > DateTime.Now || d2 > DateTime.Now)
                _validationDictionary.AddError("Date", "Lỗi: Vui lòng kiểm tra lại ngày");
            return _validationDictionary.IsValid;
        }
        public bool ValidateString(String key)
        {
            _validationDictionary.Clear();
            if (key.Trim().Length > 0 && !Regex.IsMatch(key, @"\w"))
                _validationDictionary.AddError("Timkiem", "Vui lòng nhập từ khóa hợp lệ");
            return _validationDictionary.IsValid;
            
        }
        public bool ValidateBill(HoaDon billToValidate)
        {
            _validationDictionary.Clear();
            if (billToValidate.MaKH != null)
            {
                if (_customerrepository.GetCustomer(billToValidate.MaKH) == null)
                    _validationDictionary.AddError("MaKH", "Không tồn tại mã khách hàng");
            }
            if (billToValidate.TongTien < 0 || billToValidate.TongTien.ToString().Length>0 && !Regex.IsMatch(billToValidate.TongTien.ToString(),@"\d"))
                _validationDictionary.AddError("TongTien", "Tổng tiền không hợp lệ");
            return _validationDictionary.IsValid;
        }
        public bool ValidateProduct(SanPham productToValidate)
        {
            _validationDictionary.Clear();
            if (productToValidate.SoLuong.ToString().Length > 0 && !Regex.IsMatch(productToValidate.SoLuong.ToString(), @"\d")||productToValidate.SoLuong<0)
                _validationDictionary.AddError("SoLuong", "Số lượng không hợp lệ");
            return _validationDictionary.IsValid;
        }
        public bool ValidateCustomer(KhachHang customerToValidate)
        {
            _validationDictionary.Clear();
            if (customerToValidate.MaKH.Trim().Length == 0)
                _validationDictionary.AddError("MaKH", "Vui lòng nhập mã khách hàng");
            if (customerToValidate.Ten.Trim().Length == 0)
                _validationDictionary.AddError("TenKH", "Vui lòng nhập họ tên  ");
            return _validationDictionary.IsValid;
        }
        #endregion
        #region Hóa đơn
        public bool CreateBill(HoaDon billToCreate)
        {
            if (!ValidateBill(billToCreate))
            {
                return false;
            }
            try
            {
                _billrepository.CreateBill(billToCreate);
            }
            catch
            {
                return false;
            }
            return true;
        }
        public bool DeleteBill(HoaDon billToDelete)
        {
            try
            {                
                _billrepository.DeleteBill(billToDelete);
            }
            catch
            {
                return false;
            }
            return true;
        }
        public bool EditBill(HoaDon billToEdit)
        {
            if (!ValidateBill(billToEdit))
                return false;
            // Database logic
            try
            {
                _billrepository.EditBill(billToEdit);
            }
            catch
            {
                return false;
            }
            return true;
        }
        IEnumerable listBillToSearch;
        public IEnumerable ListBills()
        {
            return listBillToSearch=_billrepository.ListBills();
        }
        public HoaDon getBill(String Key)
        {
            return _billrepository.GetBill(Key);
        }
        public IEnumerable searchBill(String Key, String Type)
        {
            ValidateString(Key);
            IList < HoaDon > result = new List<HoaDon>();
            if (Type == "Mã nhân viên")
                foreach (HoaDon item in listBillToSearch)
                {
                    if(item.MaNV.Contains(Key))
                    result.Add(item);
                }

            if (Type == "Mã khách hàng")
                foreach (HoaDon item in listBillToSearch)
                {
                    if (item.MaKH.Contains(Key))
                        result.Add(item);
                }
            if(Type == "Mã hóa đơn")
                foreach (HoaDon item in listBillToSearch)
                {
                    if (item.MaHD.Contains(Key))
                        result.Add(item);
                }
            return result;
        }
        public bool CheckFirstBill(String key)
        {
            if (_billdetailrepository.getFirstBillDetail(key) == null)
                return false;
            return true;
        }
        public class SortASC : IComparer<HoaDon> //Tao 1 class sort theo ho
        {
            public int Compare(HoaDon x, HoaDon y)
            {
                return x.TongTien.Value.CompareTo(y.TongTien.Value);
            }
        }
        public class SortDESC : IComparer<HoaDon> //Tao 1 class sort theo ho
        {
            public int Compare(HoaDon x, HoaDon y)
            {
                return y.TongTien.Value.CompareTo(x.TongTien.Value);
            }
        }
        public List<HoaDon> Statistical(String DateStart, String DateEnd, String MaNV, int Type)
        {
            if (!ValidateDate(DateStart, DateEnd))
                return null;
            IEnumerable<HoaDon> list = _billrepository.ListBills();
            List<HoaDon> result= new List<HoaDon>();
            result = list.Where(c => c.NgayLap >= DateTime.Parse(DateStart) && c.NgayLap <= DateTime.Parse(DateEnd)).ToList();
            if (MaNV != null)
                result = result.FindAll(c => c.MaNV.Equals(MaNV));
            if (Type == 1)
                result.Sort(new SortASC());
            if (Type == 2)
                result.Sort(new SortDESC());
            return result;
        }
        //Xác nhận thanh toán thành công. Giảm số lượng trong Sản phẩm
        public bool Comfirm(String key)
        {
            IEnumerable list = _billdetailrepository.ListBillDetailByID(key);
            foreach (ChiTietHoaDon item in list)
            {
                try
                {
                    foreach (SanPham product in listToSearch)
                    {
                        if (product.MaSP.Equals(item.MaSP))
                        {
                            product.SoLuong = product.SoLuong - item.SoLuong;
                            _productrepository.EditProduct(product);
                        }
                    }
                }
                catch
                {
                    return false;
                }
            }
            HoaDon billtoup = _billrepository.GetBill(key);
            billtoup.TrangThai = "Đã thanh toán";
            _billrepository.EditBill(billtoup);
            return true;
        }
        //Tạo mã hóa đơn mới
        private String getMaHD()
        {
            string newMaHD = "HD001";
            var c = _billrepository.ListBills().LastOrDefault();
            if (c != null)
            {
                int SoHD = int.Parse(c.MaHD.Substring(2));
                SoHD++;
                if (SoHD < 10)
                {
                    newMaHD = "HD00" + SoHD.ToString();
                }
                else if (SoHD < 100)
                {
                    newMaHD = "HD0" + SoHD.ToString();
                }
                else newMaHD = "HD" + SoHD.ToString();
            }
            return newMaHD;
        }
        //Help Tạo mới 1 hóa đơn tự động
        public HoaDon billCreateNew()
        {
            String NewMaHD = getMaHD();
            return new HoaDon()
            {
                MaHD = NewMaHD,
                NgayLap = DateTime.Now,
                MaNV = Information.Nhanvien.MaNV,
                MaKH = null,
                TongTien = 0,
                TrangThai = "Khởi tạo"
            };
        }
        #endregion
        #region Sản phẩm
        //Kiểm tra số lượng sản phẩm
        public bool checkAmount(string product_id, int target)
        {
            SanPham product = _productrepository.GetProduct(product_id);
            if (product.SoLuong < target)
                return false;
            return true;
        }
        IEnumerable listToSearch;
        public IEnumerable ListProducts()
        {
            return listToSearch=_productrepository.ListProducts();
        }
        public IEnumerable SearchProducts(String Key, String Type,decimal pricestart,decimal priceend)
        {
            ValidateString(Key);
            List<SanPham> result = new List<SanPham>();
            if (Type == "Mã nhà cung cấp")
                foreach (SanPham item in listToSearch)
                {
                    if (item.MaNCC.Contains(Key) && item.DonGia>=pricestart && item.DonGia<= priceend )
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
        #region Chi tiết hóa đơn
        public IEnumerable listBillDetail(String key)
        {
            return _billdetailrepository.ListBillDetailByID(key);
        }
        public bool CreateBillDetail(ChiTietHoaDon billdetailToCreate)
        {
            try
            {
                _billdetailrepository.createBillDetail(billdetailToCreate);
            }
            catch
            {
                return false;
            }
            return true;
        }
        public bool DeleteBillDetail(ChiTietHoaDon billdetailToDelete)
        {
            try
            {
                _billdetailrepository.deleteBillDetail(billdetailToDelete);
            }
            catch
            {
                return false;
            }
            return true;
        }
        public ChiTietHoaDon getBillDetail(String ID, String key)
        {
            return _billdetailrepository.getBillDetail(ID, key);
        }
        public bool UpdateBillDetail(ChiTietHoaDon ct)
        {
            try
            {
                _billdetailrepository.editBillDetail(ct);
            }
            catch
            {
                return false;
            }
            return true;
        }
        public bool DeleteBillDetailByID(String key)
        {
            try
            {
                _billdetailrepository.deleteBillDetailbyID(key);
            }
            catch
            {
                return false;
            }
            return true;
        }
        #endregion
        #region Customer
        public bool CreateCustomer(KhachHang CustomerToCreate)
        {
            if (!ValidateCustomer(CustomerToCreate))
            {
                return false;
            } try
            {
                _customerrepository.CreateCustomer(CustomerToCreate);
            }
            catch
            {
                return false;
            }
            return true;
        }
        public bool DeleteCustomer(KhachHang CustomerToDelete)
        {
            try
            {
                _customerrepository.DeleteCustomer(CustomerToDelete);
            }
            catch {
                return false;
            }
            return true;
        }
        public bool EditCustomer(KhachHang CustomerToEdit)
        {
            if (!ValidateCustomer(CustomerToEdit))
                return false;
            try
            {
                _customerrepository.EditCustomer(CustomerToEdit);

            }
            catch {
                return false;
            }
            return true;
        }
        IEnumerable listcustomerToSearch;
        public IEnumerable ListCustomers()
        {
            return listcustomerToSearch=_customerrepository.ListCustomers();
        }
        public IEnumerable searchCustomer(String Key, String type)
        {
            ValidateString(Key);
            List<KhachHang> result = new List<KhachHang>();
            if (type == "Tên")
            {
                foreach (KhachHang item in listcustomerToSearch)
                {
                    if (item.Ten.Contains(Key))
                        result.Add(item);
                }
            }
            if(type == "Mã khách hàng")
                foreach (KhachHang item in listcustomerToSearch)
                {
                    if (item.MaKH.Contains(Key))
                        result.Add(item);
                }
            return result;
        }
        public KhachHang getCustomer(String key)
        {
            return _customerrepository.GetCustomer(key);
        }
        public String getNewIDCustomer()
        {
            string newID = "KH001";
            var c = _customerrepository.ListCustomers().LastOrDefault();
            if (c != null)
            {
                int SoHD = int.Parse(c.MaKH.Substring(2));
                SoHD++;
                if (SoHD < 10)
                {
                    newID = "KH00" + SoHD.ToString();
                }
                else if (SoHD < 100)
                {
                    newID = "KH0" + SoHD.ToString();
                }
                else newID = "KH" + SoHD.ToString();
            }
            return newID;
        }
        #endregion
        #region Mã khuyến mãi

            public MaKhuyenMai getCode(String key)
        {
            return _codesrepository.getCodes(key);
        }
        public bool deleteCode(MaKhuyenMai target)
        {
            try
            {
                _codesrepository.DeleteCode(target);
            }
            catch
            {
                return false;
            }
            return true;

        }

        #endregion
       

    }
}
