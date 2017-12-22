using System;
using System.Collections.Generic;
namespace MyProject.Domain
{
    public interface ICategoryRepository
    {
        DanhMucSP CreateProductCategory(DanhMucSP productcategoryToCreate);
        void DeleteProductCategory(DanhMucSP productcategoryToDelete);
        DanhMucSP EditProductCategory(DanhMucSP productcategoryToEdit);
        DanhMucSP GetProductCategory(String Key);
        IEnumerable<DanhMucSP> ListProductCategorys();
        IEnumerable<DanhMucSP> SearchCategorys(String Key);
        IEnumerable<DanhMucSP> SearchCategorysByName(String Key);
    }
}
