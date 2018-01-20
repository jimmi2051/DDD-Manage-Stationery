using System;
using System.Linq;
using MyProject.Infrastructure;
using System.Text.RegularExpressions;
using System.Collections;
using MyProject.Domain;
using MyProject.Repository;
using System.Collections.Generic;
namespace MyProject.Service
{
    public class Manager_Service : IManagerService
    {
        #region Set-Manager-Service
        private IValidationDictionary _validationDictionary;
        private IProductRepository _productrepository;
        private ISupplierRepository _supplierrepository;
        private IEmployeeRepository _employeerepository;
        private ICategoryRepository _categoryrepository;
        private IUserRepository _userrepository;
        public Manager_Service(IValidationDictionary validationDictionary)
            : this(validationDictionary, new ProductRepository(), new SupplierRepository(), new EmployeeRepository(), new CategoryRepository(), new UserRepository())
        { }
        public Manager_Service(IValidationDictionary validationDictionary, IProductRepository productrepository,
            ISupplierRepository supplierrepository, IEmployeeRepository employeerepository,
            ICategoryRepository categoryRepository,
            IUserRepository userrepository
            )
        {
            _validationDictionary = validationDictionary;
            _productrepository = productrepository;
            _supplierrepository = supplierrepository;
            _employeerepository = employeerepository;
            _categoryrepository = categoryRepository;
            _userrepository = userrepository;
        }
        #endregion
        #region Validate
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
            if (key.Length > 0 && !Regex.IsMatch(key, @"\w"))
                _validationDictionary.AddError("Timkiem", "Vui lòng nhập từ khóa hợp lệ");
            return _validationDictionary.IsValid;
        }
        //Đã check part 1
        public bool ValidateProduct(SanPham productToValidate)
        {
            _validationDictionary.Clear();
            if (productToValidate.MaSP.Trim().Length == 0)
                _validationDictionary.AddError("MaSP", "Mã sản phẩm không được để trống");
            if (productToValidate.MaNCC.Trim().Length == 0)
                _validationDictionary.AddError("MaNCC", "Mã nhà cung cấp không được để trống");
            if (productToValidate.MaDM.Trim().Length == 0)
                _validationDictionary.AddError("MaDM", "Mã danh mục không được để trống");
            if (productToValidate.TenSP.Trim().Length == 0)
                _validationDictionary.AddError("TenSP", "Tên sản phẩm không được để trống");
            if (productToValidate.DonGia < 0)
                _validationDictionary.AddError("DonGia", "Vui lòng nhập giá trị hợp lệ");
            if (productToValidate.KichThuoc < 0)
                _validationDictionary.AddError("KichThuoc", "Vui lòng nhập giá trị hợp lệ");
            if (productToValidate.TrongLuong < 0)
                _validationDictionary.AddError("TrongLuong", "Vui lòng nhập giá trị hợp lệ");
            if (productToValidate.SoLuong < 0)
                _validationDictionary.AddError("SoLuong", "Vui lòng nhập giá trị hợp lệ");
            return _validationDictionary.IsValid;
        }
        //Đã check lần 1
        public bool ValidateSupplier(NhaCungCap supplierToValidate)
        {
            _validationDictionary.Clear();
            if (supplierToValidate.MaNCC.Trim().Length == 0)
                _validationDictionary.AddError("MaNCC", "Mã nhà cung cấp không được để trống");
            if (supplierToValidate.Ten.Length == 0)
                _validationDictionary.AddError("Ten", "Vui lòng nhập tên");
            if (supplierToValidate.sdt.Trim().Length > 0 && !Regex.IsMatch(supplierToValidate.sdt, @"\d") || supplierToValidate.sdt.Length==0 || supplierToValidate.sdt.Length>12)
                _validationDictionary.AddError("sdt", "Vui long nhập số điện thoại hợp lệ");
            return _validationDictionary.IsValid;
        }
        public bool ValidateUser(NguoiDung usertoValidate)
        {
            _validationDictionary.Clear();
            if(usertoValidate.ID.Trim().Length == 0 )
                _validationDictionary.AddError("ID", "Vui lòng nhập mã nhân viên");
            if(usertoValidate.Mail.Length == 0)
                    _validationDictionary.AddError("Email", "Vui lòng nhập Email");
            if (usertoValidate.Mail.Length > 0 && !Regex.IsMatch(usertoValidate.Mail, @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"))
                _validationDictionary.AddError("Email", "Vui lòng nhập mail hợp lệ");
            if(usertoValidate.Pass.Length == 0 )
                _validationDictionary.AddError("Password", "Không được bỏ trống mật khẩu");
            return _validationDictionary.IsValid;
        }
        //Đã check lần 1
        public bool ValidateEmployee(NhanVien employeeValidate)
        {
            _validationDictionary.Clear();
            if (employeeValidate.MaNV.Length == 0)
                _validationDictionary.AddError("MaNV", "Vui lòng nhập mã nhân viên");
            if (employeeValidate.Ten.Length == 0)
                _validationDictionary.AddError("Ten", "Vui lòng nhập họ và tên");
            if (employeeValidate.Luong.ToString().Length == 0)
                _validationDictionary.AddError("Luong", "Vui lòng nhập lương ");
            if (employeeValidate.Luong < 0)
                _validationDictionary.AddError("Luong", "Vui lòng nhập lương hợp lệ ");
            if (employeeValidate.sdt.Trim().Length > 0 && !Regex.IsMatch(employeeValidate.sdt, @"\d") || employeeValidate.sdt.Length == 0 || employeeValidate.sdt.Length>11)
                _validationDictionary.AddError("Sdt", "Vui lòng nhập số điện thoại hợp lệ ");
            return _validationDictionary.IsValid;
        }
        public bool ValidateCategory(DanhMucSP categoryToValidate)
        {
            _validationDictionary.Clear();
            if (categoryToValidate.MaDM.Trim().Length == 0)
                _validationDictionary.AddError("MaDM", "Vui lòng nhập mã danh mục");
            if (categoryToValidate.TenDM.Length == 0)
                _validationDictionary.AddError("Ten", "Vui lòng nhập tên danh mục");
            if (categoryToValidate.SoLuong < 0 )
                _validationDictionary.AddError("SoLuong", "Vui lòng nhập số lượng hợp lệ");
            return _validationDictionary.IsValid;
        }
        public bool ValidatePrice(Decimal a, Decimal b)
        {
            _validationDictionary.Clear();
            if (a > b || a<0 || b < 0)
                _validationDictionary.AddError("Price", "Vui lòng nhập giá hợp lệ");
            return _validationDictionary.IsValid;
        }
        #endregion
        #region Compare List <Sort>
        public class SortPriceASC : IComparer<SanPham>
        {
            public int Compare(SanPham x, SanPham y)
            {
                return x.DonGia.Value.CompareTo(y.DonGia.Value);
            }
        }
        public class SortPriceDESC : IComparer<SanPham>
        {
            public int Compare(SanPham x, SanPham y)
            {
                return y.DonGia.Value.CompareTo(x.DonGia.Value);
            }
        }
        public class SortASC : IComparer<SanPham>
        {
            public int Compare(SanPham x, SanPham y)
            {
                return x.SoLuong.Value.CompareTo(y.SoLuong.Value);
            }
        }
        public class SortDESC : IComparer<SanPham>
        {
            public int Compare(SanPham x, SanPham y)
            {
                return y.SoLuong.Value.CompareTo(x.SoLuong.Value);
            }
        }
        #endregion
        //Đã check lần 2
        #region Product
        public bool CreateProduct(SanPham productToCreate)
        {
            // Validation logic
            if (!ValidateProduct(productToCreate))
            {
                return false;
            }
            // Database logic
            try
            {
                _productrepository.CreateProduct(productToCreate);
            }
            catch
            {
                return false;
            }
            return true;
        }
        public bool DeleteProduct(SanPham productToDelete)
        {
            // Database logic
            try
            {
                _productrepository.DeleteProduct(productToDelete);
            }
            catch
            {
                return false;
            }
            return true;
        }
        public bool EditProduct(SanPham productToEdit)
        {
            // Validation logic
            if (!ValidateProduct(productToEdit))
                return false;

            // Database logic
            try
            {
                _productrepository.EditProduct(productToEdit);
            }
            catch
            {
                return false;
            }
            return true;
        }
        public SanPham GetProduct(String Key)
        {
            return _productrepository.GetProduct(Key);
        }
        private IEnumerable listProductToSearch;
        public IEnumerable ListProducts()
        {
            return listProductToSearch=_productrepository.ListProducts().ToList();
        }

        public IEnumerable SearchProducts(String Key, String Type,int Sort, int TypeSort, Decimal PriceofStart, Decimal PriceofEnd)
        {           
            ValidateString(Key);
            ValidatePrice(PriceofStart, PriceofEnd);
            List<SanPham> result = new List<SanPham>();
            if (Type == "Mã nhà cung cấp")
                foreach (SanPham item in listProductToSearch)
                {
                    if (item.MaNCC.Contains(Key))
                        result.Add(item);
                }
            if (Type == "Mã danh mục")
                foreach (SanPham item in listProductToSearch)
                {
                    if (item.MaDM.Contains(Key))
                        result.Add(item);
                }
            if (Type == "Tên sản phẩm")
                foreach (SanPham item in listProductToSearch)
                {
                    if (item.TenSP.Contains(Key))
                        result.Add(item);
                }
            if(Type == "Mã sản phẩm")
            foreach (SanPham item in listProductToSearch)
                {
                    if (item.MaSP.Contains(Key))
                        result.Add(item);
                }
            if (Sort == 1 && TypeSort == 0 )
                result.Sort(new SortASC());
            if (Sort == 2 && TypeSort == 0)
                result.Sort(new SortDESC());
            if (Sort == 1 && TypeSort == 1)
                result.Sort(new SortPriceASC());
            if (Sort == 2 && TypeSort == 1)
                result.Sort(new SortPriceDESC());
            if ( PriceofEnd > 0)
               result=result.FindAll(c => c.DonGia >= PriceofStart && c.DonGia <= PriceofEnd);
            return result;            
        }
        public String getNewIDProduct()
        {
            string newID = "SP001";
            var c = _productrepository.ListProducts().LastOrDefault();
            if (c != null)
            {
                int SoHD = int.Parse(c.MaSP.Substring(2));
                SoHD++;
                if (SoHD < 10)
                {
                    newID = "SP00" + SoHD.ToString();
                }
                else if (SoHD < 100)
                {
                    newID = "SP0" + SoHD.ToString();
                }
                else newID = "SP" + SoHD.ToString();
            }
            return newID;
        }
        public List<SanPham> StatisticalProduct(int Type, String MaSP, String DateStart, String DateEnd)
        {
            if (!ValidateDate(DateStart, DateEnd))
                return null;         
            List<SanPham> result = new List<SanPham>();           
            if (Type == 3 || Type == 4)
            {
                result = _productrepository.ListProducts().ToList();
                if (MaSP != null)
                    result = result.FindAll(c => c.MaSP.Equals(MaSP));
                if (Type == 3)
                    result.Sort(new SortDESC());
                if (Type == 4)
                    result.Sort(new SortASC());
            }
            if (Type == 1 || Type == 2)
            {
                IBillDetailRespository _repository = new BillDetailRepository();
                foreach (ChiTietHoaDon item in _repository.ListBillDetail())
                {
                    if (item.HoaDon.NgayLap >= DateTime.Parse(DateStart) && item.HoaDon.NgayLap <= DateTime.Parse(DateEnd))
                    {
                        SanPham target = item.SanPham;
                        target.SoLuong = item.SoLuong;
                        target.DonGia = item.DonGia;
                        SanPham index = result.Find(c => c.MaSP.Equals(target.MaSP));
                        if (index != null)
                        {
                            index.SoLuong = index.SoLuong + target.SoLuong;
                        }
                        else
                            result.Add(target);
                    }
                }               
                if (MaSP != null)
                    result = result.FindAll(c => c.MaSP.Equals(MaSP));
                if (Type == 1)
                    result.Sort(new SortDESC());
                if(Type == 2 )
                    result.Sort(new SortASC());
            }
            if(Type==0)
                result = _productrepository.ListProducts().ToList();
            return result;
        }
        #endregion
        //Đã check lần 1 - Finish
        #region Supplier
        public bool CreateSupplier(NhaCungCap supplierToCreate)
        {
            if (!ValidateSupplier(supplierToCreate))
                return false;
            try
            {
                _supplierrepository.CreateSupplier(supplierToCreate);
            }
            catch
            {
                return false;
            }
            return true;
        }
        public bool DeleteSupplier(NhaCungCap supplierToDelete)
        {
            try
            {
                _supplierrepository.DeleteSupplier(supplierToDelete);
            }
            catch
            {
                return false;
            }
            return true;
        }
        public bool EditSupplier(NhaCungCap supplierToEdit)
        {
            if (!ValidateSupplier(supplierToEdit))
                return false;
            try
            {
                _supplierrepository.EditSupplier(supplierToEdit);
            }
            catch
            {
                return false;
            }
            return true;
        }
        public NhaCungCap GetSupplier(String Key)
        {
            return _supplierrepository.GetSupplier(Key);
        }
        IEnumerable listSupToSearch;
        public IEnumerable ListSuppliers()
        {
            return listSupToSearch=_supplierrepository.ListSuppliers();
        }
        public IEnumerable SearchSuppliers(String Key, String Type)
        {
            List<NhaCungCap> result = new List<NhaCungCap>();
            ValidateString(Key);
            if (Type == "Tên")
                foreach (NhaCungCap item in listSupToSearch)
                {
                    if (item.Ten.Contains(Key))
                        result.Add(item);
                }
            if(Type=="Mã nhà cung cấp")
                foreach (NhaCungCap item in listSupToSearch)
                {
                    if (item.MaNCC.Contains(Key))
                        result.Add(item);
                }
            return result;
        }
        public String getNewIDSup()
        {
            string newID = "NCC001";
            var c = _supplierrepository.ListSuppliers().LastOrDefault();
            if (c != null)
            {
                int SoHD = int.Parse(c.MaNCC.Substring(3));
                SoHD++;
                if (SoHD < 10)
                {
                    newID = "NCC00" + SoHD.ToString();
                }
                else if (SoHD < 100)
                {
                    newID = "NCC0" + SoHD.ToString();
                }
                else newID = "NCC" + SoHD.ToString();
            }
            return newID;
        }
        #endregion
        //Đã check lần 2 - Finish
        #region Employee
        public bool CreateEmployee(NhanVien employeeToCreate)
        {
            if (!ValidateEmployee(employeeToCreate))
                return false;
            try
            {
                _employeerepository.CreateEmployee(employeeToCreate);
            }
            catch {
                return false;
            }
            return true;
        }
        public bool DeleteEmployee(NhanVien employeeToDelete)
        {
            try
            {
                _employeerepository.DeleteEmployee(employeeToDelete);
            }
            catch {
                return false;
            }
            return true;
        }
        public bool EditEmployee(NhanVien employeeToEdit)
        {
            if (!ValidateEmployee(employeeToEdit))
                return false;
            try
            {
                _employeerepository.EditEmployee(employeeToEdit);
            }
            catch { return false; }
            return true;
        }
        public NhanVien GetEmployee(String Key)
        {
            return _employeerepository.GetEmployee(Key);
        }
        IEnumerable listEmployeeToSearch;
        public IEnumerable ListEmployees()
        {
            return listEmployeeToSearch=_employeerepository.ListEmployees();
        }
        public IEnumerable SearchEmployees(String Key, String Type)
        {
            ValidateString(Key);
            List<NhanVien> result = new List<NhanVien>();
            if (Type == "Tên")
            {
                foreach (NhanVien item in listEmployeeToSearch)
                {
                    if (item.Ten.Contains(Key))
                        result.Add(item);
                }
            }            
            if (Type == "Chức vụ")
                foreach (NhanVien item in listEmployeeToSearch)
                {
                    if (item.ChucVu.Contains(Key))
                        result.Add(item);
                }
            if (Type =="Mã nhân viên")
                foreach (NhanVien item in listEmployeeToSearch)
                {
                    if (item.MaNV.Contains(Key))
                        result.Add(item);
                }
            return result;
        }
        public String getNewIDEmployee()
        {
            string newID = "NV001";
            var c = _employeerepository.ListEmployees().LastOrDefault();
            if (c != null)
            {
                int SoHD = int.Parse(c.MaNV.Substring(2));
                SoHD++;
                if (SoHD < 10)
                {
                    newID = "NV00" + SoHD.ToString();
                }
                else if (SoHD < 100)
                {
                    newID = "NV0" + SoHD.ToString();
                }
                else newID = "NV" + SoHD.ToString();
            }
            return newID;
        }
        #endregion
        //Đã check lần 1-Finish
        #region Danh Mục SP
        public bool CreateCategory(DanhMucSP categoryToCreate)
        {
            if (!ValidateCategory(categoryToCreate))
                return false;
            try
            {
                _categoryrepository.CreateProductCategory(categoryToCreate);
            }
            catch
            {
                return false;
            }
            return true;
        }
        public bool DeleteCategory(DanhMucSP categoryToDelete)
        {
            try
            {
                _categoryrepository.DeleteProductCategory(categoryToDelete);
            }
            catch
            {
                return false;
            }
            return true;
        }
        public bool EditCategory(DanhMucSP categoryToEdit)
        {
            if (!ValidateCategory(categoryToEdit))
                return false;
            try
            {
                _categoryrepository.EditProductCategory(categoryToEdit);
            }
            catch
            {
                return false;
            }
            return true;
        }
        public DanhMucSP GetCategory(String Key)
        {
            return _categoryrepository.GetProductCategory(Key);
        }
        IEnumerable listCategoryToSearch;
        public IEnumerable ListCategorys()
        {
            return listCategoryToSearch=_categoryrepository.ListProductCategorys();
        }
        public IEnumerable SearchCategorys(String Key, String Type)
        {
            ValidateString(Key);
            List<DanhMucSP> result = new List<DanhMucSP>();
            if (Type == "Tên")
            {
                foreach (DanhMucSP item in listCategoryToSearch)
                {
                    if (item.TenDM.Contains(Key))
                        result.Add(item);
                }
            }
            if (Type == "Mã danh mục")
            {
                foreach (DanhMucSP item in listCategoryToSearch)
                {
                    if (item.MaDM.Contains(Key))
                        result.Add(item);
                }
            }
            return result;
        }
        public String getNewIDCat()
        {
            string newID = "DM001";
            var c = _categoryrepository.ListProductCategorys().LastOrDefault();
            if (c != null)
            {
                int SoHD = int.Parse(c.MaDM.Substring(2));
                SoHD++;
                if (SoHD < 10)
                {
                    newID = "DM00" + SoHD.ToString();
                }
                else if (SoHD < 100)
                {
                    newID = "DM0" + SoHD.ToString();
                }
                else newID = "DM" + SoHD.ToString();
            }
            return newID;
        }
        #endregion
        #region User
        public bool CreateUser(NguoiDung target)
        {
            if (!ValidateUser(target))
                return false;
            try
            {
                target.Pass = Encode.md5((target.Pass));
                _userrepository.InsertUser(target);
            }
            catch
            {
                return false;
            }
            return true;
        }
        public bool DeleteUser(NguoiDung target)
        {
            try
            {
                _userrepository.DeleteUser(target);
            }
            catch
            {
                return false;
            }
            return true;
        }
        public bool EditUser(NguoiDung target)
        {
            if (!ValidateUser(target))
                return false;
            try
            {
                target.Pass = Infrastructure.Encode.md5((target.Pass));
                _userrepository.UpdateUser(target);
            }
            catch
            {
                return false;
            }
            return true;
        }
        public NguoiDung getUser(String key)
        {
            return _userrepository.getUser(key);
        }
        public NguoiDung CheckUser(String key)
        {
            return _userrepository.CheckUser(key);
        }
        #endregion
        public void Dispose()
       {            
       }

        public bool CheckEmployee(NhanVien employeeToCheck)
        {
            try
            {
                
            }
            catch
            {
                return false;
            }
            return true;
        }
    }        
}
