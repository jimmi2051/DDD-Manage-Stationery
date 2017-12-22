using System;
using MyProject.Domain;
using System.Collections;
using System.Collections.Generic;

namespace MyProject.Service
{
    public interface ISellerService
    {
        //Repository của Hoa đơn -- chức năng hóa đơn
        bool CreateBill(HoaDon billToCreate);
        bool DeleteBill(HoaDon billToDelete);
        bool EditBill(HoaDon billToEdit);
        IEnumerable ListBills();
        IEnumerable searchBill(String Key, String type);
        HoaDon billCreateNew();
        HoaDon getBill(String key);
        List<HoaDon> Statistical(String DateStart, String DateEnd, String MaNV, int Type);
        //Repository của Chi tiết hóa đơn
        bool CreateBillDetail(ChiTietHoaDon billdetailToCreate);
        bool UpdateBillDetail(ChiTietHoaDon billdetailToUpdate);
        IEnumerable listBillDetail(String key);
        bool DeleteBillDetail(ChiTietHoaDon billdetailToDelete);
        ChiTietHoaDon getBillDetail(String ID,String key);
        bool DeleteBillDetailByID(String key);
        bool CheckFirstBill(String key);
        //Repository của Sản phẩm
        bool checkAmount(string product_id, int target);
        IEnumerable ListProducts();
        IEnumerable SearchProducts(String Key,String type);
        SanPham GetProduct(String Key);
        bool Comfirm(String billID);
        //Repository của Khách han2g
        bool CreateCustomer(KhachHang CustomerToCreate);
        bool DeleteCustomer(KhachHang CustomerToDelete);
        bool EditCustomer(KhachHang CustomerToEdit);
        IEnumerable ListCustomers();
        IEnumerable searchCustomer(String Key, String type);
        String getNewIDCustomer();
        KhachHang getCustomer(String key);
        //Mã giảm giá
        MaKhuyenMai getCode(String key);
        bool deleteCode(MaKhuyenMai target);
        //SQLDependency
        String getCommand();
        void setSqlDependency();
    }
}
