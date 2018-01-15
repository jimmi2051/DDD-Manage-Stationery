using System;
using System.Collections.Generic;

namespace MyProject.Domain
{
    public interface IProductRepository
    {
        //Commands
        SanPham CreateProduct(SanPham productToCreate);
        void DeleteProduct(SanPham productToDelete);
        SanPham EditProduct(SanPham productToEdit);
        //Queries
        SanPham GetProduct(String Key);
        IEnumerable<SanPham> ListProducts();
    }
}
