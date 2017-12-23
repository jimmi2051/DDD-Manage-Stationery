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
        public bool ValidateSupplier(NhaCungCap supplierToValidate)
        {
            _validationDictionary.Clear();
            if (supplierToValidate.MaNCC.Trim().Length == 0)
                _validationDictionary.AddError("MaNCC", "Mã nhà cung cấp không được để trống");
            if (supplierToValidate.sdt.Trim().Length > 0 && !Regex.IsMatch(supplierToValidate.sdt, @"\d"))
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
        public bool ValidateEmployee(NhanVien employeeValidate)
        {
            _validationDictionary.Clear();
            if (employeeValidate.MaNV.Length == 0)
                _validationDictionary.AddError("MaNV", "Vui lòng nhập mã nhân viên");
            if (employeeValidate.Ten.Length == 0)
                _validationDictionary.AddError("Ten", "Vui lòng nhập họ và tên");
            if (employeeValidate.Luong.ToString().Length == 0)
                _validationDictionary.AddError("Luong", "Vui long nhập lương ");
            if (employeeValidate.Luong < 0)
                _validationDictionary.AddError("Luong", "Vui lòng nhập lương hợp lệ ");
            if (employeeValidate.sdt.Trim().Length > 0 && !Regex.IsMatch(employeeValidate.sdt, @"\d"))
                _validationDictionary.AddError("Sdt", "Vui lòng nhập số dt hop le ");
            return _validationDictionary.IsValid;
        }
        public bool ValidateCategory(DanhMucSP categoryToValidate)
        {
            _validationDictionary.Clear();
            if (categoryToValidate.MaDM.Trim().Length == 0)
                _validationDictionary.AddError("MaDM", "Vui lòng nhập mã danh mục");
            if (categoryToValidate.TenDM.Length == 0)
                _validationDictionary.AddError("Ten", "Vui lòng nhập tên danh mục");
            if (categoryToValidate.SoLuong < 0)
                _validationDictionary.AddError("SoLuong", "Vui lòng nhập số lượng hợp lệ");
            return _validationDictionary.IsValid;
        }
        #endregion
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
        public IEnumerable ListProducts()
        {
            return _productrepository.ListProducts().ToList();
        }
        public IEnumerable SearchProducts(String Key, String Type)
        {
            ValidateString(Key);
            if (Type == "Mã nhà cung cấp")
                return _productrepository.SearchProductsbyNCC(Key);
            if (Type == "Mã danh mục")
                return _productrepository.SearchProductsbyType(Key);
            if (Type == "Tên sản phẩm")
                return _productrepository.SearchProductsbyName(Key);
            //if (Type == "Loại sản phẩm")
            //    return _productrepository.SearchProductsbyTypeName(Key);
            return _productrepository.SearchProducts(Key);
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
            string sqlcmd="SELECT * FROM SanPham ";
            if (Type == 3 || Type == 4)
            {
                sqlcmd = "SELECT * FROM SanPham ";
                if (MaSP != null)
                    sqlcmd += "WHERE MaSP ='" + MaSP + "'";
                if (Type == 1)
                    sqlcmd += "ORDER BY SoLuong DESC ";
                if (Type == 2)
                    sqlcmd += "ORDER BY SoLuong ASC";
            }
            if (Type == 1 || Type == 2)
            {
                sqlcmd = "SELECT SanPham.MaSP,MaNCC,MaDM,TenSP,ChiTietHoaDon.DonGia,ChiTietHoaDon.SoLuong,XuatXu,TrongLuong,KichThuoc,DonVi " +
            "FROM SanPham,ChiTietHoaDon,HoaDon " +
           "WHERE Sanpham.MaSP = ChiTietHoaDon.MaSP AND HoaDon.MaHD = ChiTietHoaDon.MaHD " +
               " AND NgayLap >= '" + DateStart + "' AND NgayLap <= '" + DateEnd + "'";
                if (MaSP != null)
                    sqlcmd += "AND MaSP='" + MaSP + "'";
                if(Type == 1 )
                    sqlcmd+= "ORDER BY SoLuong DESC";
                if(Type == 2 )
                    sqlcmd+= "ORDER BY SoLuong ASC";
            }
            return _productrepository.StatisticalProduct(sqlcmd).ToList();
        }
        #endregion
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
        public IEnumerable ListSuppliers()
        {
            return _supplierrepository.ListSuppliers();
        }
        public IEnumerable SearchSuppliers(String Key, String Type)
        {
            ValidateString(Key);
            if (Type == "Tên")
                return _supplierrepository.SearchSuppliersbyName(Key);
            return _supplierrepository.SearchSuppliers(Key);

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
        public IEnumerable ListEmployees()
        {
            return _employeerepository.ListEmployees();
        }
        public IEnumerable SearchEmployees(String Key, String Type)
        {
            ValidateString(Key);
            if (Type == "Tên")
                return _employeerepository.SearchEmployeesbyName(Key);
            if (Type == "Chức vụ")
                return _employeerepository.SearchEmployessbyPosition(Key);
            return _employeerepository.SearchEmployees(Key);
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
        public IEnumerable ListCategorys()
        {
            return _categoryrepository.ListProductCategorys();
        }
        public IEnumerable SearchCategorys(String Key, String Type)
        {
            ValidateString(Key);
            if (Type == "Tên")
                return _categoryrepository.SearchCategorysByName(Key);
            return _categoryrepository.SearchCategorys(Key);
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
                target.Pass = Infrastructure.Encode.md5((target.Pass));
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
                _employeerepository.CheckEmployee(employeeToCheck);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }        
}
