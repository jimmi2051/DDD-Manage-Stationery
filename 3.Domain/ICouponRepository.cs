using System;
using System.Collections.Generic;
namespace MyProject.Domain
{
    public interface ICouponRepository
    {       
        PhieuNhapXuat CreateCoupon(PhieuNhapXuat CouponToCreate);
        void DeleteCoupon(PhieuNhapXuat CouponToDelete);
        PhieuNhapXuat EditCoupon(PhieuNhapXuat CouponToEdit);
        PhieuNhapXuat GetCoupon(String Key);
        IEnumerable<PhieuNhapXuat> ListCoupons();
        IEnumerable<PhieuNhapXuat> getCouponByID(String ID);
        IEnumerable<PhieuNhapXuat> getCouponByEm(String key);
        IEnumerable<PhieuNhapXuat> getCouponByStt(String key);
    }
}
