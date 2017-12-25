using System;
using System.Collections.Generic;
using System.Linq;
using MyProject.Domain;
namespace MyProject.Repository.ADONET

{
    public class BillRepositoryADONET : IBillRepository
    {
        public void ComfirmBill(string ID, int quality)
        {
            throw new NotImplementedException();
        }

        public HoaDon CreateBill(HoaDon BillToCreate)
        {
            throw new NotImplementedException();
        }

        public void DeleteBill(HoaDon BillToDelete)
        {
            throw new NotImplementedException();
        }

        public HoaDon EditBill(HoaDon BillToEdit)
        {
            throw new NotImplementedException();
        }

        public HoaDon GetBill(string Key)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<HoaDon> getBillByDate(string SqlCmd)
        {
            throw new NotImplementedException();
        }
        public string getCommand()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<HoaDon> ListBills()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<HoaDon> LoadData()
        {
            throw new NotImplementedException();
        }

        public void SetSQLDependency()
        {
            throw new NotImplementedException();
        }
    }
}
