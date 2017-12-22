using System;
using System.Collections.Generic;
using System.Linq;
using MyProject.Domain;
namespace MyProject.Repository.RAW
{
    public class SanPhamCompare : IComparer<SanPham>
    {
        public int Compare(SanPham a, SanPham b)
        {
            return a.MaSP.CompareTo(b.MaSP);
        }
    }
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
            //_entities.UpdateProduct(productToEdit.MaSP, productToEdit.MaNCC, productToEdit.MaDM, productToEdit.TenSP, productToEdit.DonGia, productToEdit.SoLuong, productToEdit.XuatXu, productToEdit.TrongLuong, productToEdit.KichThuoc, productToEdit.DonVi);
            var originalProduct = GetProduct(productToEdit.MaSP);
            _entities.SanPhams.Remove(originalProduct);
            _entities.SanPhams.Add(productToEdit);
            _entities.SaveChanges();
            return productToEdit;
        }
        
        public IEnumerable<SanPham> ListProducts()
        {
            List<SanPham> Sanpham=_entities.SanPhams.ToList();
            foreach (SanPham item in Sanpham)
            {
                item.DanhMucSP = _entities.DanhMucSPs.Where(c => c.MaDM.Equals(item.MaDM)).FirstOrDefault();
                item.NhaCungCap = _entities.NhaCungCaps.Where(c => c.MaNCC.Equals(item.MaNCC)).FirstOrDefault();
            }
            Sanpham.Sort(new SanPhamCompare());
            return Sanpham;
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
