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
    }
}
