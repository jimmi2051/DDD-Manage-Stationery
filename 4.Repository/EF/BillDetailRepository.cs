using System;
using System.Collections.Generic;
using System.Linq;
using MyProject.Domain;
namespace MyProject.Repository
{
    public class BillDetailRepository : IBillDetailRespository
    {
        private QLVanPhongEntities _entities = QLVanPhongEntities.Instance;
        #region Command
        public ChiTietHoaDon createBillDetail(ChiTietHoaDon billDetailToCreate)
        {
            _entities.ChiTietHoaDons.Add(billDetailToCreate);
            _entities.SaveChanges();
            return billDetailToCreate;
        }
        //Xóa 1 đống chi tiết hóa đơn theo Ma hóa đơn
        public void deleteBillDetailbyID(String key)
        {
            foreach (ChiTietHoaDon item in _entities.ChiTietHoaDons.Where(c=>c.MaHD.Equals(key)).ToList())
            {
                _entities.ChiTietHoaDons.Remove(item);
            }
            _entities.SaveChanges();
        }
        //Xóa 1 chi tiết hóa đơn
        public void deleteBillDetail(ChiTietHoaDon billDetailToDelete)
        {
            _entities.ChiTietHoaDons.Remove(getBillDetail(billDetailToDelete.MaHD,billDetailToDelete.MaSP));
            _entities.SaveChanges();           
        }
        public ChiTietHoaDon editBillDetail(ChiTietHoaDon billDetailToEdit)
        {
            var originalBill = getBillDetail(billDetailToEdit.MaHD, billDetailToEdit.MaSP);
            _entities.Entry(originalBill).CurrentValues.SetValues(billDetailToEdit);
            _entities.SaveChanges();
            return billDetailToEdit;
        }
        #endregion
        #region Queries
        public ChiTietHoaDon getBillDetail(String ID, String key)
        {
            return _entities.ChiTietHoaDons.Where(c => c.MaHD.Equals(ID) && c.MaSP.Equals(key)).FirstOrDefault();
        }
        public IEnumerable<ChiTietHoaDon> ListBillDetailByID(String key)
        {
            return _entities.ChiTietHoaDons.Where(c => c.MaHD.Equals(key)).ToList();
        }
        public ChiTietHoaDon getFirstBillDetail(String Key)
        {
            return _entities.ChiTietHoaDons.Where(c => c.MaHD.Equals(Key)).FirstOrDefault();
        }

        public IEnumerable<ChiTietHoaDon> ListBillDetail()
        {
            return _entities.ChiTietHoaDons.ToList();
        }
        #endregion
    }
}
