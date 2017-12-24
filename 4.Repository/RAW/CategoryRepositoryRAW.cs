using System;
using System.Collections.Generic;
using MyProject.Domain;
using System.Linq;

namespace MyProject.Repository.RAW
{
    public class CategoryRepositoryRAW : ICategoryRepository
    {
        private QLVanPhong_Context _entities = QLVanPhong_Context.Instance;
        #region Commands
        public DanhMucSP CreateProductCategory(DanhMucSP target)
        {
            _entities.DanhMucSPs.Add(target);
            _entities.SaveChanges();
            return target;
        }
        public void DeleteProductCategory(DanhMucSP target)
        {
            _entities.DanhMucSPs.Remove(target);
            _entities.SaveChanges();
        }
        public DanhMucSP EditProductCategory(DanhMucSP productcategoryToEdit)
        {
            var originalProductCategory = GetProductCategory(productcategoryToEdit.MaDM);
            if(!originalProductCategory.Equals(productcategoryToEdit))
            {
                originalProductCategory.TenDM = productcategoryToEdit.TenDM;
                originalProductCategory.SoLuong = productcategoryToEdit.SoLuong;
            }      
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
        #endregion
    }
}
