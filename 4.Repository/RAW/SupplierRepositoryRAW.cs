using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyProject.Domain;
namespace MyProject.Repository.RAW
{
    public class SupplierRepositoryRAW :ISupplierRepository
    {
        QLVanPhong_Context _entities = QLVanPhong_Context.Instance;
        public NhaCungCap CreateSupplier(NhaCungCap target)
        {
            _entities.NhaCungCaps.Add(target);
            _entities.SaveChanges();
            return target;
        }
        public void DeleteSupplier(NhaCungCap SupplierToDelete)
        {
            var originalSupplier = GetSupplier(SupplierToDelete.MaNCC);
            _entities.NhaCungCaps.Remove(originalSupplier);
            _entities.SaveChanges();
        }
        public NhaCungCap EditSupplier(NhaCungCap SupplierToEdit)
        {
            var originalSupplier = GetSupplier(SupplierToEdit.MaNCC);
            _entities.NhaCungCaps.Remove(originalSupplier);
            _entities.NhaCungCaps.Add(SupplierToEdit);
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
