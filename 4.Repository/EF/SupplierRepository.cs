using System;
using System.Collections.Generic;
using System.Linq;
using MyProject.Domain;
namespace MyProject.Repository
{
    public class SupplierRepository:ISupplierRepository
    {
        QLVanPhongEntities _entities = QLVanPhongEntities.Instance;
        public NhaCungCap CreateSupplier(NhaCungCap target)
        {
            _entities.NhaCungCaps.Add(target);
            _entities.SaveChanges();
            return target;
        }
        public void DeleteSupplier(NhaCungCap SupplierToDelete)
        {
            _entities.NhaCungCaps.Remove(SupplierToDelete);
            _entities.SaveChanges();
        }
        public NhaCungCap EditSupplier(NhaCungCap SupplierToEdit)
        {
            var originalsupplier = GetSupplier(SupplierToEdit.MaNCC);
            _entities.Entry(originalsupplier).CurrentValues.SetValues(SupplierToEdit);
            //_entities.Update_NCC(SupplierToEdit.MaNCC, SupplierToEdit.Ten, SupplierToEdit.DiaChi, SupplierToEdit.sdt);
            _entities.SaveChanges();
            return SupplierToEdit;
        }
        public NhaCungCap GetSupplier(String Key)
        {
            return _entities.NhaCungCaps.Where(c => c.MaNCC.Equals(Key)).FirstOrDefault();
        }
        public IEnumerable<NhaCungCap> ListSuppliers()
        {
            return _entities.NhaCungCaps.ToList();
        }
    }
}
