using System;
using System.Collections;
using MyProject.Domain;
using System.Collections.Generic;
namespace MyProject.Service
{
    public interface IManagerService:IDisposable
    {
        //Repository Product
        bool CreateProduct(SanPham productToCreate);
        bool DeleteProduct(SanPham productToDelete);
        bool EditProduct(SanPham productToEdit);
        SanPham GetProduct(String Key);
        IEnumerable ListProducts();
        IEnumerable SearchProducts(String Key, String Type, int Sort, int TypeSort, Decimal PriceofStart, Decimal PriceofEnd);
        String getNewIDProduct();
        List<SanPham> StatisticalProduct(int Type, String MaSP, String DateStart, String DateEnd);
        //Repository Supplier
        bool CreateSupplier(NhaCungCap supplierToCreate);
        bool DeleteSupplier(NhaCungCap supplierToDelete);
        bool EditSupplier(NhaCungCap supplierToEdit);
        NhaCungCap GetSupplier(String Key);
        IEnumerable ListSuppliers();
        IEnumerable SearchSuppliers(String Key, String Type);
        String getNewIDSup();
        //Repository Employee
        bool CreateEmployee(NhanVien employeeToCreate);
        bool DeleteEmployee(NhanVien employeeToDelete);
        bool EditEmployee(NhanVien employeeToEdit);
        bool CheckEmployee(NhanVien employeeToCheck);
        NhanVien GetEmployee(String Key);
        IEnumerable ListEmployees();
        IEnumerable SearchEmployees(String Key, String Type);
        String getNewIDEmployee();
        //Product category
        bool CreateCategory(DanhMucSP categoryToCreate);
        bool DeleteCategory(DanhMucSP categoryToDelete);
        bool EditCategory(DanhMucSP categoryToEdit);
        DanhMucSP GetCategory(String Key);
        IEnumerable ListCategorys();
        IEnumerable SearchCategorys(String Key, String Type);
        String getNewIDCat();
        //User
        bool CreateUser(NguoiDung target);
        bool DeleteUser(NguoiDung target);
        bool EditUser(NguoiDung target);
        NguoiDung getUser(String key);
        NguoiDung CheckUser(String key);
    }
}
