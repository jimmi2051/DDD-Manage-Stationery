using System;
using System.Collections.Generic;
using System.Linq;
using MyProject.Domain;
namespace MyProject.Repository
{
    public class ProductRepository:IProductRepository
    {
        private QLVanPhongEntities _entities = QLVanPhongEntities.Instance;
        #region Commands
        public SanPham CreateProduct(SanPham target)
        {
            _entities.SanPhams.Add(target);
            _entities.SaveChanges();
            return target;
        }
        public void DeleteProduct(SanPham productToDelete)
        {
            _entities.SanPhams.Remove(productToDelete);
            _entities.SaveChanges();
        }
        public SanPham EditProduct(SanPham productToEdit)
        {          
            var originalProduct = GetProduct(productToEdit.MaSP);
            _entities.Entry(originalProduct).CurrentValues.SetValues(productToEdit);
            _entities.SaveChanges(); 
            return productToEdit;
        }
        #endregion
        #region Queries
        public IEnumerable<SanPham> ListProducts()
        {
            return _entities.SanPhams.ToList();
        }
        public SanPham GetProduct(String Key)
        {
            return _entities.SanPhams.Where(c => c.MaSP.Equals(Key)).FirstOrDefault();
        }
        public IEnumerable<SanPham> SearchProducts(String Key)
        {
            return _entities.SanPhams.Where(c => c.MaSP.Contains(Key)).ToList();
        }
        public IEnumerable<SanPham> SearchProductsbyNCC(String Key)
        {
            return _entities.SanPhams.Where(c => c.MaNCC.Contains(Key)).ToList();
        }
        public IEnumerable<SanPham> SearchProductsbyType(String Key)
        {
            return _entities.SanPhams.Where(c => c.MaDM.Contains(Key)).ToList();
        }
        public IEnumerable<SanPham> SearchProductsbyName(String Key)
        {
            return _entities.SanPhams.Where(c => c.TenSP.Contains(Key)).ToList();
        }
        public IEnumerable<SanPham> SearchProductsbyTypeName(String key)
        {
            return _entities.SanPhams.Where(c => c.DanhMucSP.TenDM.Contains(key)).ToList();
        }
        public IEnumerable<SanPham> StatisticalProduct(String SqlCmd)
        {
            return _entities.Database.SqlQuery<SanPham>(SqlCmd);
        }
        #endregion
    }
}
