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
            _entities.InSert_ChiTietHD(billDetailToCreate.MaHD, billDetailToCreate.MaSP, billDetailToCreate.DonGia, billDetailToCreate.SoLuong);
            _entities.SaveChanges();
            return billDetailToCreate;
        }
        //Xóa 1 đống chi tiết hóa đơn theo Ma hóa đơn
        public void deleteBillDetailbyID(String key)
        {
            _entities.DeleteAll_ChiTietHD(key);
            _entities.SaveChanges();
        }
        //Xóa 1 chi tiết hóa đơn
        public void deleteBillDetail(ChiTietHoaDon billDetailToDelete)
        {
            _entities.Delete_ChiTietHD(billDetailToDelete.MaHD, billDetailToDelete.MaSP);
            _entities.SaveChanges();           
        }
        public ChiTietHoaDon editBillDetail(ChiTietHoaDon billDetailToEdit)
        {
            //var originalBill = getBillDetail(billDetailToEdit.MaHD, billDetailToEdit.MaSP);
           // _entities.Entry(originalBill).CurrentValues.SetValues(billDetailToEdit);
            _entities.Update_ChiTietHD(billDetailToEdit.MaHD, billDetailToEdit.MaSP, billDetailToEdit.DonGia, billDetailToEdit.SoLuong);
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
            IEnumerable<ChiTietHoaDon> list = _entities.Database.SqlQuery<ChiTietHoaDon>("SELECT * FROM CHITIETHOADON WHERE MaHD='" + key + "' ").ToList();
            foreach (ChiTietHoaDon item in list)
            {
                item.SanPham = (_entities.SanPhams.Where(c => c.MaSP.Equals(item.MaSP)).FirstOrDefault());
                item.HoaDon = _entities.HoaDons.Where(c => c.MaHD.Equals(item.MaHD)).FirstOrDefault();
            }
            return list;
        }
        public ChiTietHoaDon getFirstBillDetail(String Key)
        {
            return _entities.ChiTietHoaDons.Where(c => c.MaHD.Equals(Key)).FirstOrDefault();
        }
        #endregion
    }
}
