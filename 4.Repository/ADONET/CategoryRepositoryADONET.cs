using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using MyProject.Domain;
namespace MyProject.Repository.ADONET
{
    public class CategoryRepositoryADONET : ICategoryRepository
    {
        private SqlConnection connection = ConnectionADONET.Connection;
        public DanhMucSP CreateProductCategory(DanhMucSP productcategoryToCreate)
        {
            throw new NotImplementedException();
        }

        public void DeleteProductCategory(DanhMucSP productcategoryToDelete)
        {
            throw new NotImplementedException();
        }

        public DanhMucSP EditProductCategory(DanhMucSP productcategoryToEdit)
        {
            throw new NotImplementedException();
        }

        public DanhMucSP GetProductCategory(string Key)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DanhMucSP> ListProductCategorys()
        {
            if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
            {
                connection.Open();
            }
            //Kiểm tra thông tin đăng nhập
            string cmdsql = "SELECT * FROM DanhMucSP ";
            SqlCommand myCommand = new SqlCommand(cmdsql, connection);
            DataTable dataTable = new DataTable();
            try
            {
                SqlDataReader sqlDataReader = myCommand.ExecuteReader();
                dataTable.Load(sqlDataReader);
                return Infrastructure.Encode.ConvertToNumberale<DanhMucSP>(dataTable).ToList();
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

        public IEnumerable<DanhMucSP> SearchCategorys(string Key)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DanhMucSP> SearchCategorysByName(string Key)
        {
            throw new NotImplementedException();
        }
    }
}
