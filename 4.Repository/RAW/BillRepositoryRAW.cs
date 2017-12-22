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
            //_entities.UpdateBill(BillToEdit.MaSP, BillToEdit.MaNCC, BillToEdit.MaDM, BillToEdit.TenSP, BillToEdit.DonGia, BillToEdit.SoLuong, BillToEdit.XuatXu, BillToEdit.TrongLuong, BillToEdit.KichThuoc, BillToEdit.DonVi);
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
        public IEnumerable<HoaDon> SearchBills(String Key)
        {
            return (from c in _entities.HoaDons
                    where c.MaHD.Equals(Key)
                    select c).ToList();
        }
        public HoaDon GetLastBill()
        {
            HoaDon[] ar = _entities.HoaDons.ToArray();
            return ar[ar.Length - 1];
        }
        public IEnumerable<HoaDon> getBillByID(String ID)
        {
            return (from c in _entities.HoaDons
                    where c.MaHD.Equals(ID)
                    select c).ToList();
        }
        public IEnumerable<HoaDon> getBillByIDEm(String ID)
        {
            return (from c in _entities.HoaDons
                    where c.MaNV.Equals(ID)
                    select c).ToList();
        }
        public IEnumerable<HoaDon> getBillByIDCu(String ID)
        {
            return (from c in _entities.HoaDons
                    where c.MaKH.Equals(ID)
                    select c).ToList();
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
        public MaKhuyenMai getCode(String key)

        {
            return null;
        }
        public void deleteCode(MaKhuyenMai target)
        {
            
        }

        public IEnumerable<HoaDon> getBillByDate(string SqlCmd)
        {
            throw new NotImplementedException();
        }

        public void ComfirmBill(string ID, int quality)
        {
            throw new NotImplementedException();
        }

        public void SetSQLDependency()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<HoaDon> LoadData()
        {
            throw new NotImplementedException();
        }

        public string getCommand()
        {
            throw new NotImplementedException();
        }
    }
}
