using System;
using System.Collections.Generic;
using MyProject.Domain;
namespace MyProject.Repository.ADONET
{
    public class ProductRepositoryADONET : IProductRepository
    {
        public SanPham CreateProduct(SanPham productToCreate)
        {
            throw new NotImplementedException();
        }

        public void DeleteProduct(SanPham productToDelete)
        {
            throw new NotImplementedException();
        }

        public SanPham EditProduct(SanPham productToEdit)
        {
            throw new NotImplementedException();
        }

        public SanPham GetProduct(string Key)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SanPham> ListProducts()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SanPham> ListProductsSQLCMD()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SanPham> SearchProducts(string Key)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SanPham> SearchProductsbyName(string Key)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SanPham> SearchProductsbyNCC(string Key)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SanPham> SearchProductsbyType(string Key)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SanPham> SearchProductsbyTypeName(string key)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SanPham> StatisticalProduct(string SqlCmd)
        {
            throw new NotImplementedException();
        }
    }
}
