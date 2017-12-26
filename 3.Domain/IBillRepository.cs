using System;
using System.Collections.Generic;

namespace MyProject.Domain
{
    public interface IBillRepository
    {
        //Commands
        HoaDon CreateBill(HoaDon BillToCreate);
        void DeleteBill(HoaDon BillToDelete);
        HoaDon EditBill(HoaDon BillToEdit);   
        //Queries
        HoaDon GetBill(String Key);
        IEnumerable<HoaDon> ListBills();
        IEnumerable<HoaDon> getBillByDate(String SqlCmd);
    }
}
