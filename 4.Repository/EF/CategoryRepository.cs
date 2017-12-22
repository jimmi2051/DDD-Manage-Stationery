using System;
using System.Collections.Generic;
using System.Linq;
using MyProject.Domain;
namespace MyProject.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private QLVanPhongEntities _entities = QLVanPhongEntities.Instance;
        #region Commands
        public DanhMucSP CreateProductCategory(DanhMucSP target)
        {
            _entities.InSert_DanhMucSP(target.MaDM, target.TenDM, target.SoLuong);
            _entities.SaveChanges();
            return target;
        }
        public void DeleteProductCategory(DanhMucSP target)
        {
            _entities.Delete_DanhMucSP(target.MaDM);
            _entities.SaveChanges();
        }
        public DanhMucSP EditProductCategory(DanhMucSP productcategoryToEdit)
        {
           var originalProductCategory = GetProductCategory(productcategoryToEdit.MaDM);
           _entities.Entry(originalProductCategory).CurrentValues.SetValues(productcategoryToEdit);
            //_entities.Update_DanhMucSP(productcategoryToEdit.MaDM, productcategoryToEdit.TenDM, productcategoryToEdit.SoLuong);
            _entities.SaveChanges();
            return productcategoryToEdit;
        }
        #endregion
        #region Queries
        public IEnumerable<DanhMucSP> ListProductCategorys()
        {
            return _entities.DanhMucSPs.ToList();
        }
        public DanhMucSP GetProductCategory(String Key)
        {
            return _entities.DanhMucSPs.Where(c => c.MaDM.Equals(Key)).FirstOrDefault();
        }
        public IEnumerable<DanhMucSP> SearchCategorys(String Key)
        {
            return _entities.DanhMucSPs.Where(c => c.MaDM.Contains(Key)).ToList();
        }
        public IEnumerable<DanhMucSP> SearchCategorysByName(String Key)
        {
            return _entities.DanhMucSPs.Where(c => c.TenDM.Contains(Key)).ToList();
        }
        #endregion

    }
}
