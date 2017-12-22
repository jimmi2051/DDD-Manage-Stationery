using System;
using System.Collections.Generic;

namespace MyProject.Domain
{
    public interface ICustomerRepository
    {
        KhachHang CreateCustomer(KhachHang CustomerToCreate);
        void DeleteCustomer(KhachHang CustomerToDelete);
        KhachHang EditCustomer(KhachHang CustomerToEdit);
        KhachHang GetCustomer(String Key);
        IEnumerable<KhachHang> ListCustomers();
        IEnumerable<KhachHang> SearchCustomers(String Key);
        IEnumerable<KhachHang> SearchCustomersbyName(String Key);
    }
}
