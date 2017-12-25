using System;
using System.Collections.Generic;
using System.Linq;
using MyProject.Domain;
namespace MyProject.Repository.RAW
{
    public class BillDetailRepositoryRAW : IBillDetailRespository
    {
        private QLVanPhong_Context _entities = QLVanPhong_Context.Instance;
        #region Command
        public ChiTietHoaDon createBillDetail(ChiTietHoaDon billDetailToCreate)
        {
            _entities.ChiTietHoaDons.Add(billDetailToCreate);
            _entities.SaveChanges();
            return billDetailToCreate;
        }
        public void deleteBillDetailbyID(String key)
        {
            //_entities.ChiTietHoaDon.Remove;
            _entities.SaveChanges();
        }
        public void deleteBillDetail(ChiTietHoaDon billDetailToDelete)
        {
            var originalBill = getBillDetail(billDetailToDelete.MaHD, billDetailToDelete.MaSP);
            _entities.ChiTietHoaDons.Remove(originalBill);
            _entities.SaveChanges();
        }
        public ChiTietHoaDon editBillDetail(ChiTietHoaDon billDetailToEdit)
        {
            var originalBill = getBillDetail(billDetailToEdit.MaHD, billDetailToEdit.MaSP);
            _entities.ChiTietHoaDons.Remove(originalBill);
            _entities.ChiTietHoaDons.Add(billDetailToEdit);
            _entities.SaveChanges();
            return billDetailToEdit;
        }
        #endregion
        #region Queries
        public ChiTietHoaDon getBillDetail(String ID, String key)
        {
            return (from c in _entities.ChiTietHoaDons
                    where c.MaHD.Equals(ID) && c.MaSP.Equals(key)
                    select c).FirstOrDefault();
        }
        public IEnumerable<ChiTietHoaDon> ListBillDetailByID(String key)
        {
            IEnumerable<ChiTietHoaDon> chiTietHoaDon = null;
            chiTietHoaDon= (from c in _entities.ChiTietHoaDons
                    where c.MaHD.Equals(key)
                    select c
                    ).ToList();
            foreach (ChiTietHoaDon item in chiTietHoaDon)
            {
                item.SanPham = _entities.SanPhams.Where(c => c.MaSP.Equals(item.MaSP)).FirstOrDefault();
            }
            return chiTietHoaDon;
        }
        public ChiTietHoaDon getFirstBillDetail(String key)
        {
            return null;
        }
        #endregion
    }
}
