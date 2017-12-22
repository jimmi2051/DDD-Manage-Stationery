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
        void ComfirmBill(string ID,int quality);
        
        //Queries
        HoaDon GetBill(String Key);
        IEnumerable<HoaDon> ListBills();
        IEnumerable<HoaDon> getBillByID(String ID);
        IEnumerable<HoaDon> getBillByIDEm(String ID);
        IEnumerable<HoaDon> getBillByIDCu(String ID);
        IEnumerable<HoaDon> getBillByDate(String SqlCmd);
        //SQLDependency
        void SetSQLDependency();
        String getCommand();
    }
}
