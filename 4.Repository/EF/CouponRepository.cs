using MyProject.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
namespace MyProject.Repository
{
    public class CouponRepository : ICouponRepository
    {
        QLVanPhongEntities _entities = QLVanPhongEntities.Instance;
        public PhieuNhapXuat CreateCoupon(PhieuNhapXuat CouponToCreate)
        {
            _entities.PhieuNhapXuats.Add(CouponToCreate);
            _entities.SaveChanges();
            return CouponToCreate;
        }
        public void DeleteCoupon(PhieuNhapXuat CouponToDelete)
        {
            _entities.PhieuNhapXuats.Remove(GetCoupon(CouponToDelete.MaPhieu));
            _entities.SaveChanges();
        }
        public PhieuNhapXuat EditCoupon(PhieuNhapXuat CouponToEdit)
        {
            var originalCoupon = GetCoupon(CouponToEdit.MaPhieu);
            _entities.Entry(originalCoupon).CurrentValues.SetValues(CouponToEdit);
         
            _entities.SaveChanges();
            return CouponToEdit;
        }
        public PhieuNhapXuat GetCoupon(String Key)
        {
            return _entities.PhieuNhapXuats.Where(c => c.MaPhieu.Equals(Key)).FirstOrDefault();
        }
        public IEnumerable<PhieuNhapXuat> ListCoupons()
        {
                return _entities.PhieuNhapXuats.ToList();
          
        }
    }
}
