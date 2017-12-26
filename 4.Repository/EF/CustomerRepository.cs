using System;
using System.Collections.Generic;
using System.Linq;
using MyProject.Domain;
namespace MyProject.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        QLVanPhongEntities _entities = QLVanPhongEntities.Instance;
        #region Commands
        public KhachHang CreateCustomer(KhachHang CustomerToCreate)
        {
            _entities.KhachHangs.Add(CustomerToCreate);
            _entities.SaveChanges();
            return CustomerToCreate;
        }
        public void DeleteCustomer(KhachHang CustomerToDelete)
        {
            _entities.KhachHangs.Remove(GetCustomer(CustomerToDelete.MaKH));
            _entities.SaveChanges();
        }
        public KhachHang EditCustomer(KhachHang CustomerToEdit)
        {
            var originalCustomer = GetCustomer(CustomerToEdit.MaKH);
            _entities.Entry(originalCustomer).CurrentValues.SetValues(CustomerToEdit);
          //  _entities.Update_KH(CustomerToEdit.MaKH, CustomerToEdit.Ten, CustomerToEdit.DiaChi, CustomerToEdit.Sdt);
            _entities.SaveChanges();
            return CustomerToEdit;
        }
        #endregion
        #region Queries
        public KhachHang GetCustomer(String Key)
        {
            return _entities.KhachHangs.Where(c => c.MaKH.Equals(Key)).FirstOrDefault();
        }
        public IEnumerable<KhachHang> ListCustomers()
        {
            return _entities.KhachHangs.ToList();
        }
        #endregion
    }
}
