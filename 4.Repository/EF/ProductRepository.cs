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
            _entities.Insert_SanPham(target.MaSP, target.MaNCC, target.MaDM, target.TenSP, target.DonGia, target.SoLuong, target.XuatXu, target.TrongLuong, target.KichThuoc, target.DonVi);
            _entities.SaveChanges();
            return target;
        }
        public void DeleteProduct(SanPham productToDelete)
        {
            _entities.Delete_SanPham(productToDelete.MaSP);
            _entities.SaveChanges();
        }
        public SanPham EditProduct(SanPham productToEdit)
        {
            //_entities.Update_SanPham(productToEdit.MaSP, productToEdit.MaNCC, productToEdit.MaDM, productToEdit.TenSP, productToEdit.DonGia, productToEdit.SoLuong, productToEdit.XuatXu, productToEdit.TrongLuong, productToEdit.KichThuoc, productToEdit.DonVi);
            var originalProduct = GetProduct(productToEdit.MaSP);
            _entities.Entry(originalProduct).CurrentValues.SetValues(productToEdit);
            _entities.SaveChanges(); 
            return productToEdit;
        }
        #endregion
        #region Queries
        public IEnumerable<SanPham> ListProducts()
        {
            IEnumerable<SanPham> list = _entities.Database.SqlQuery<SanPham>("SELECT * FROM SanPham").ToList();
            foreach (SanPham item in list)
            {
                item.DanhMucSP = (_entities.DanhMucSPs.Where(c => c.MaDM.Equals(item.MaDM)).FirstOrDefault());
                item.NhaCungCap = (_entities.NhaCungCaps.Where(c => c.MaNCC.Equals(item.MaNCC)).FirstOrDefault());
            }
            return list;
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
