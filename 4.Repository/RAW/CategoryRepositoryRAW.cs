using System;
using System.Collections.Generic;
using MyProject.Domain;
using System.Linq;

namespace MyProject.Repository.RAW
{
    public class CategoryRepositoryRAW : ICategoryRepository
    {
        private QLVanPhong_Context db = QLVanPhong_Context.Instance;
        public DanhMucSP CreateProductCategory(DanhMucSP productcategoryToCreate)
        {
            throw new NotImplementedException();
        }

        public void DeleteProductCategory(DanhMucSP productcategoryToDelete)
        {
            throw new NotImplementedException();
        }

        public DanhMucSP EditProductCategory(DanhMucSP productcategoryToEdit)
        {
            throw new NotImplementedException();
        }

        public DanhMucSP GetProductCategory(string Key)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DanhMucSP> ListProductCategorys()
        {
            return db.DanhMucSPs.ToList();
        }

        public IEnumerable<DanhMucSP> SearchCategorys(string Key)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DanhMucSP> SearchCategorysByName(string Key)
        {
            throw new NotImplementedException();
        }
    }
}
