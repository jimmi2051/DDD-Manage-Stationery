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
        SqlCommand setData(SqlCommand cmd, DanhMucSP categoryTarget)
        {
            cmd.Parameters.Add("@MaDM", SqlDbType.VarChar).Value = categoryTarget.MaDM;
            cmd.Parameters.Add("@TenDM", SqlDbType.NVarChar).Value = categoryTarget.TenDM;
            cmd.Parameters.Add("@SoLuong", SqlDbType.Int).Value = categoryTarget.SoLuong;
            return cmd;
        }
        public DanhMucSP CreateProductCategory(DanhMucSP productcategoryToCreate)
        {
            if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
            {
                connection.Open();
            }
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "InSert_DanhMucSP";
            cmd = setData(cmd, productcategoryToCreate);
            cmd.Connection = connection;
            cmd.ExecuteNonQuery();
            connection.Close();
            return productcategoryToCreate;

        }
        public void DeleteProductCategory(DanhMucSP productcategoryToDelete)
        {
            if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
            {
                connection.Open();
            }
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Delete_DanhMucSP";
            cmd.Parameters.Add("@MaDM", SqlDbType.VarChar).Value = productcategoryToDelete.MaDM;
            cmd.Connection = connection;
            cmd.ExecuteNonQuery();
            connection.Close();

        }

        public DanhMucSP EditProductCategory(DanhMucSP productcategoryToEdit)
        {

            if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
            {
                connection.Open();
            }
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Update_DanhMucSP";
            cmd = setData(cmd, productcategoryToEdit);
            cmd.Connection = connection;
            cmd.ExecuteNonQuery();
            connection.Close();
            return productcategoryToEdit;

        }
        public DanhMucSP GetProductCategory(string Key)
        {

            if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
            {
                connection.Open();
            }
            //Kiểm tra thông tin đăng nhập
            string cmdsql = "SELECT * FROM DanhMucSP Where MaDM='" + Key + "' ";
            SqlCommand myCommand = new SqlCommand(cmdsql, connection);
            DataTable dataTable = new DataTable();
            try
            {
                SqlDataReader sqlDataReader = myCommand.ExecuteReader();
                dataTable.Load(sqlDataReader);
                DanhMucSP Target = Infrastructure.Encode.ConvertToNumberale<DanhMucSP>(dataTable).FirstOrDefault();
                return Target;
            }
            catch
            {
                return null;
            }
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
            catch
            {
                return null;
            }

        }
    }
}
