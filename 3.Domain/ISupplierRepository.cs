using System;
using System.Collections.Generic;
namespace MyProject.Domain
{
    public interface ISupplierRepository
    {
        NhaCungCap CreateSupplier(NhaCungCap supplierToCreate);
        void DeleteSupplier(NhaCungCap supplierToDelete);
        NhaCungCap EditSupplier(NhaCungCap supplierToEdit);
        NhaCungCap GetSupplier(String Key);
        IEnumerable<NhaCungCap> ListSuppliers();
    }
}
