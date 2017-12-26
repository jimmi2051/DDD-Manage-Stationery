using MyProject.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MyProject.Repository
{
    public class BillRepository : IBillRepository
    {
        private QLVanPhongEntities _entities = QLVanPhongEntities.Instance;
        #region Queries
        public IEnumerable<HoaDon> ListBills()
        {
           return  _entities.HoaDons.ToList();
        }
        public HoaDon GetBill(String Key)
        {
            return _entities.HoaDons.Where(c => c.MaHD.Equals(Key)).FirstOrDefault();
        }
        public IEnumerable<HoaDon> getBillByDate(String SqlCmd)
        {
            return _entities.Database.SqlQuery<HoaDon>(SqlCmd);
        }
        #endregion
        #region Command
        public HoaDon CreateBill(HoaDon target)
        {
            _entities.HoaDons.Add(target);
            _entities.SaveChanges();
            return target;
        }
        public void DeleteBill(HoaDon BillToDelete)
        {
            _entities.HoaDons.Remove(GetBill(BillToDelete.MaHD));
            _entities.SaveChanges();
        }
        public HoaDon EditBill(HoaDon BillToEdit)
        {
            var originalBill = GetBill(BillToEdit.MaHD);
            _entities.Entry(originalBill).CurrentValues.SetValues(BillToEdit);
            // _entities.Update_HoaDon(BillToEdit.MaHD, BillToEdit.NgayLap, BillToEdit.MaNV, BillToEdit.MaKH, BillToEdit.TongTien, BillToEdit.TrangThai);
            _entities.SaveChanges();
            return BillToEdit;
        }
        #endregion
       
    }
}
