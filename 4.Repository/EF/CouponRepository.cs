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
            _entities.Insert_PhieuNhapXuat(CouponToCreate.MaPhieu,CouponToCreate.MaNV , CouponToCreate.TongTien, CouponToCreate.NgayLap, CouponToCreate.TrangThai);
            _entities.SaveChanges();
            return CouponToCreate;
        }
        public void DeleteCoupon(PhieuNhapXuat CouponToDelete)
        {
            _entities.Delete_PhieuNhapXuat(CouponToDelete.MaPhieu);
            _entities.SaveChanges();
        }
        public PhieuNhapXuat EditCoupon(PhieuNhapXuat CouponToEdit)
        {
            //var originalCoupon = GetCoupon(CouponToEdit.MaPhieu);
            //_entities.Entry(originalCoupon).CurrentValues.SetValues(CouponToEdit);
            _entities.Update_PhieuNhapXuat(CouponToEdit.MaPhieu, CouponToEdit.MaNV, CouponToEdit.TongTien, CouponToEdit.NgayLap, CouponToEdit.TrangThai);
            _entities.SaveChanges();
            return CouponToEdit;
        }
        public PhieuNhapXuat GetCoupon(String Key)
        {
            return _entities.Database.SqlQuery<PhieuNhapXuat>("SELECT * FROM PhieuNhapXuat WHERE MaPhieu LIKE '%" + Key + "%'").FirstOrDefault();
        }
        public IEnumerable<PhieuNhapXuat> ListCoupons()
        {
            //    return _entities.PhieuNhapXuats.ToList();
            return _entities.Database.SqlQuery<PhieuNhapXuat>("SELECT * FROM PhieuNhapXuat").ToList();
        }
        public IEnumerable<PhieuNhapXuat> getCouponByID(String ID)
        {
            return _entities.Database.SqlQuery<PhieuNhapXuat>("SELECT * FROM PhieuNhapXuat WHERE MaPhieu LIKE '%"+ID+"%'").ToList();
        }        
        public IEnumerable<PhieuNhapXuat> getCouponByEm(String key)
        {
            return _entities.Database.SqlQuery<PhieuNhapXuat>("SELECT * FROM PhieuNhapXuat WHERE MaNV LIKE '%" + key + "%'").ToList();
        }
        public IEnumerable<PhieuNhapXuat> getCouponByStt(String key)
        {
            return _entities.Database.SqlQuery<PhieuNhapXuat>("SELECT * FROM PhieuNhapXuat WHERE TrangThai LIKE '%" + key + "%'").ToList();
        }
    }
}
