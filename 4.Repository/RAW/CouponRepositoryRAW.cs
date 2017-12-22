using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyProject.Domain;
namespace MyProject.Repository.RAW
{
    public class CouponRepositoryRAW : ICouponRepository
    {
        QLVanPhong_Context entities = QLVanPhong_Context.Instance;
        public PhieuNhapXuat CreateCoupon(PhieuNhapXuat CouponToCreate)
        {
            throw new NotImplementedException();
        }

        public void DeleteCoupon(PhieuNhapXuat CouponToDelete)
        {
            throw new NotImplementedException();
        }

        public PhieuNhapXuat EditCoupon(PhieuNhapXuat CouponToEdit)
        {
            throw new NotImplementedException();
        }

        public PhieuNhapXuat GetCoupon(string Key)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PhieuNhapXuat> getCouponByEm(string key)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PhieuNhapXuat> getCouponByID(string ID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PhieuNhapXuat> getCouponByStt(string key)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PhieuNhapXuat> ListCoupons()
        {
            return entities.PhieuNhapXuats.ToList();
        }
    }
}
