using System;
using System.Collections.Generic;
using System.Linq;
using MyProject.Domain;
namespace MyProject.Repository.RAW
{
    public class BillRepositoryRAW : IBillRepository
    {
        private QLVanPhong_Context _entities = QLVanPhong_Context.Instance;
        public HoaDon CreateBill(HoaDon target)
        {
            _entities.HoaDons.Add(target);
            _entities.SaveChanges();
            return target;
        }
        public void DeleteBill(HoaDon BillToDelete)
        {
            var originalBill = GetBill(BillToDelete.MaHD);
            _entities.HoaDons.Remove(originalBill);
            _entities.SaveChanges();
        }
        public HoaDon EditBill(HoaDon BillToEdit)
        {
          
            var originalBill = GetBill(BillToEdit.MaHD);
            _entities.HoaDons.Remove(originalBill);
            _entities.HoaDons.Add(BillToEdit);
            _entities.SaveChanges();
            return BillToEdit;
        }
        public IEnumerable<HoaDon> ListBills()
        {
            return _entities.HoaDons.ToList();
        }
        public HoaDon GetBill(String Key)
        {
            return (from c in _entities.HoaDons
                    where c.MaHD.Equals(Key)
                    select c).FirstOrDefault();
        }
        public KhachHang checkCustomer(String key)
        {
            return new KhachHang();
        }
        public NhanVien checkEmployee(String key)
        {
            return (from c in _entities.NhanViens
                    where c.MaNV.Equals(key)
                    select c).FirstOrDefault();
        }
    }
}
