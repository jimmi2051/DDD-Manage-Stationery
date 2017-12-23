using System;
using System.Collections.Generic;
using System.Linq;
using MyProject.Domain;
namespace MyProject.Repository.RAW
{
    public class ProductRepositoryRAW : IProductRepository 
    {
        QLVanPhong_Context _entities = QLVanPhong_Context.Instance;
        public SanPham CreateProduct(SanPham target)
        {
            _entities.SanPhams.Add(target);
            _entities.SaveChanges();
            return target;
        }
        public void DeleteProduct(SanPham productToDelete)
        {
            
          //  _entities.SanPham.Remove(productToDelete);
            var originalProduct = GetProduct(productToDelete.MaSP);
            _entities.SanPhams.Remove(originalProduct);
            _entities.SaveChanges();
        }
        public SanPham EditProduct(SanPham productToEdit)
        {           
            SanPham originalProduct = GetProduct(productToEdit.MaSP);
            if (!originalProduct.Equals(productToEdit))
            {
                originalProduct.MaDM = productToEdit.MaDM;
                originalProduct.MaNCC = productToEdit.MaNCC;
                originalProduct.TenSP = productToEdit.TenSP;
                originalProduct.SoLuong = productToEdit.SoLuong;
                originalProduct.DonGia = productToEdit.DonGia;
                originalProduct.XuatXu = productToEdit.XuatXu;
                originalProduct.TrongLuong = productToEdit.TrongLuong;
                originalProduct.KichThuoc = productToEdit.KichThuoc;
                originalProduct.DonVi = productToEdit.DonVi;
            }
            _entities.SaveChanges();
            return productToEdit;
        }
        
        public IEnumerable<SanPham> ListProducts()
        {
            return _entities.SanPhams.ToList();
        }
        public SanPham GetProduct(String Key)
        {
            return (from c in _entities.SanPhams
                    where c.MaSP.Equals(Key)
                    select c).FirstOrDefault();
        }
        public IEnumerable<SanPham> SearchProducts(String Key)
        {
            return (from c in _entities.SanPhams
                    where c.MaSP.Equals(Key)
                    select c).ToList();
        }
public IEnumerable<SanPham> SearchProductsbyNCC(String Key)
        {
            return (from c in _entities.SanPhams
                    where c.MaNCC.Equals(Key)
                    select c).ToList();
        }
       public  IEnumerable<SanPham> SearchProductsbyType(String Key)
        {
            return (from c in _entities.SanPhams
                    where c.MaDM.Equals(Key)
                    select c).ToList();
        }
         public IEnumerable<SanPham> SearchProductsbyName(String Key)
        {
            return (from c in _entities.SanPhams
                    where c.TenSP.Equals(Key)
                    select c).ToList();

        }
        public IEnumerable<SanPham> SearchProductsbyTypeName(string key)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SanPham> StatisticalProduct(string SqlCmd)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SanPham> ListProductsSQLCMD()
        {
            throw new NotImplementedException();
        }
    }
}
