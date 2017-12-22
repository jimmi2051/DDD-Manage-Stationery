using System;
using System.Collections.Generic;
namespace MyProject.Domain
{
    public interface IDetailCouponRepository
    {
        ChiTietPhieu createDetailCoupon(ChiTietPhieu DetailCouponToCreate);
        void deleteDetailCoupon(ChiTietPhieu DetailCouponToDelete);
        ChiTietPhieu editDetailCoupon(ChiTietPhieu DetailCouponToEdit);
        ChiTietPhieu getDetailCoupon(String ID, String key);
        IEnumerable<ChiTietPhieu> ListDetailCouponByID(String key);
        void deleteDetailCouponbyID(String key);
    }
}
