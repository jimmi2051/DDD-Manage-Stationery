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
        public IEnumerable<SanPham> StatisticalProduct(String SqlCmd)
        {
            return _entities.Database.SqlQuery<SanPham>(SqlCmd);
        }
        #endregion
    }
}
