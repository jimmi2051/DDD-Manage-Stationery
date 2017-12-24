using MyProject.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Permissions;

namespace MyProject.Repository
{
    public class BillRepository : IBillRepository
    {
        private QLVanPhongEntities _entities = QLVanPhongEntities.Instance;
        #region Queries
        public IEnumerable<HoaDon> ListBills()
        {
            IEnumerable<HoaDon> list = _entities.Database.SqlQuery<HoaDon>("SELECT * FROM HoaDon").ToList();
            foreach (var item in list)
            {
                item.NhanVien = _entities.NhanViens.Where(c => c.MaNV.Equals(item.MaNV)).FirstOrDefault();
                item.KhachHang = _entities.KhachHangs.Where(c => c.MaKH.Equals(item.MaKH)).FirstOrDefault();
            }
            return list;
        }
        public HoaDon GetBill(String Key)
        {
            return _entities.Database.SqlQuery<HoaDon>("SELECT * FROM HoaDon WHERE MaHD = '" + Key + "'").FirstOrDefault();
        }
        public IEnumerable<HoaDon> getBillByID(String ID)
        {
            return _entities.Database.SqlQuery<HoaDon>("SELECT * FROM HoaDon WHERE MaHD Like '%" + ID + "%'").ToList();
        }
        public IEnumerable<HoaDon> getBillByIDEm(String ID)
        {
            return _entities.Database.SqlQuery<HoaDon>("SELECT * FROM HoaDon WHERE MaNV Like '%" + ID + "%'").ToList();
        }
        public IEnumerable<HoaDon> getBillByIDCu(String ID)
        {
            return _entities.Database.SqlQuery<HoaDon>("SELECT * FROM HoaDon WHERE MaKH Like '%" + ID + "%'").ToList();
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
            _entities.HoaDons.Remove(BillToDelete);
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

        public void ComfirmBill(string ID, int quality)
        {
            
            _entities.SaveChanges();
        }
        #endregion
        #region SQLDependency
        SqlConnection con = null;
        public void SetSQLDependency()
        {
            Infrastructure.Information.StrConnect = _entities.Database.Connection.ConnectionString;
            SqlClientPermission perm = new SqlClientPermission(PermissionState.Unrestricted);
            perm.Demand();
            try
            {
                SqlDependency.Start(Infrastructure.Information.StrConnect);
            }
            catch (Exception ex)
            {
                throw new System.Exception("Fails to Start the SqlDependency in the ImmediateNotificationRegister public class", ex);
            }
            con = new SqlConnection(Infrastructure.Information.StrConnect);
        }
        public String getCommand() {
            return "SELECT MaHD,MaNV,MaKH,TongTien,TrangThai,NgayLap  from dbo.HoaDon";
        }     
        #endregion
    }
}
