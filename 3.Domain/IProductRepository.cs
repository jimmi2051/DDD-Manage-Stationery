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
        IEnumerable<SanPham> SearchProducts(String Key);
        IEnumerable<SanPham> SearchProductsbyNCC(String Key);
        IEnumerable<SanPham> SearchProductsbyType(String Key);
        IEnumerable<SanPham> SearchProductsbyName(String Key);
        IEnumerable<SanPham> SearchProductsbyTypeName(String key);
        IEnumerable<SanPham> StatisticalProduct(String SqlCmd);
    }
}
