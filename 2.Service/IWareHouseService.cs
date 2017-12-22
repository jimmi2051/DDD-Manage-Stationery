using System;
using System.Collections;
using MyProject.Domain;
using System.Collections.Generic;
namespace MyProject.Service
{
    public interface IWareHouseService
    {
        //Repository của warehouse
        bool CreateWareHouse(Kho warehousetoCreate);
        bool EditWareHouse(Kho warehouseToEdit);
        bool DeleteWareHouse(Kho warehouseToDelete);
        Kho getWareHouse(String msp);
        IEnumerable ListWareHouse();
        IEnumerable SearchWareHouse(String key, String Type);
        IEnumerable Sort(String key);
        bool checkAmount(Kho warehouse, int target);
        List<Kho> Statistical(int Type,String MaSP);
        //Repository của Coupon
        bool CreateCoupon(PhieuNhapXuat couponToCreate);
        bool EditCoupon(PhieuNhapXuat couponToEdit);
        bool DeleteCoupon(PhieuNhapXuat couponToDelete);
        PhieuNhapXuat getCoupon(String key);
        PhieuNhapXuat createNewCoupon();
        IEnumerable ListCoupon();
        IEnumerable searchCoupon(String key, String type);
        //Repository của DetailCoupon
        bool CreateDetailCoupon(ChiTietPhieu DetailCouponToCreate);
        bool UpdateDetailCoupon(ChiTietPhieu DetailCouponToUpdate);
        IEnumerable listDetailCoupon(String key);
        bool DeleteDetailCoupon(ChiTietPhieu DetailCouponToDelete);
        ChiTietPhieu getDetailCoupon(String ID, String key);
        bool DeleteDetailCouponByID(String key);
    }
}
