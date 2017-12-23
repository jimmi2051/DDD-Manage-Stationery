using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using MyProject.Domain;
namespace MyProject.Repository.ADONET
{
    public class SupplierRepositoryADONET : ISupplierRepository
    {
        private SqlConnection connection = ConnectionADONET.Connection;
        public NhaCungCap CreateSupplier(NhaCungCap supplierToCreate)
        {
            throw new NotImplementedException();
        }

        public void DeleteSupplier(NhaCungCap supplierToDelete)
        {
            throw new NotImplementedException();
        }

        public NhaCungCap EditSupplier(NhaCungCap supplierToEdit)
        {
            throw new NotImplementedException();
        }

        public NhaCungCap GetSupplier(string Key)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<NhaCungCap> ListSuppliers()
        {
            if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
            {
                connection.Open();
            }
            //Kiểm tra thông tin đăng nhập
            string cmdsql = "SELECT * FROM NhaCungCap ";
            SqlCommand myCommand = new SqlCommand(cmdsql, connection);
            DataTable dataTable = new DataTable();
            try
            {
                SqlDataReader sqlDataReader = myCommand.ExecuteReader();
                dataTable.Load(sqlDataReader);
                return Infrastructure.Encode.ConvertToNumberale<NhaCungCap>(dataTable).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        public IEnumerable<NhaCungCap> SearchSuppliers(string Key)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<NhaCungCap> SearchSuppliersbyName(string Key)
        {
            throw new NotImplementedException();
        }
    }
}
