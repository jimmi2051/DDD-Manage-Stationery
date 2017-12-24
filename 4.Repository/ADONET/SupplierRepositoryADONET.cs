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
        #region Command
        SqlCommand setData(SqlCommand cmd, NhaCungCap supplierTarget)
        {
            cmd.Parameters.Add("@MaNCC", SqlDbType.VarChar).Value = supplierTarget.MaNCC;
            cmd.Parameters.Add("@Ten", SqlDbType.NVarChar).Value = supplierTarget.Ten;
            cmd.Parameters.Add("@DiaChi", SqlDbType.NVarChar).Value = supplierTarget.DiaChi;
            cmd.Parameters.Add("@Sdt", SqlDbType.VarChar).Value = supplierTarget.sdt;
            return cmd;
        }
        public NhaCungCap CreateSupplier(NhaCungCap supplierToCreate)
        {
            if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
            {
                connection.Open();
            }
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "InSert_NhaCungCap";
            cmd = setData(cmd, supplierToCreate);
            cmd.Connection = connection;
            cmd.ExecuteNonQuery();
            connection.Close();
            return supplierToCreate;
        }

        public void DeleteSupplier(NhaCungCap supplierToDelete)
        {
            if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
            {
                connection.Open();
            }
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Delete_NhaCungCap";
            cmd.Parameters.Add("@MaNCC", SqlDbType.VarChar).Value = supplierToDelete.MaNCC;
            cmd.Connection = connection;
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        public NhaCungCap EditSupplier(NhaCungCap supplierToEdit)
        {
            if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
            {
                connection.Open();
            }
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Update_NhaCungCap";
            cmd = setData(cmd, supplierToEdit);
            cmd.Connection = connection;
            cmd.ExecuteNonQuery();
            connection.Close();
            return supplierToEdit;
        }
        #endregion
        #region Query
        public NhaCungCap GetSupplier(string Key)
        {
            if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
            {
                connection.Open();
            }
            //Kiểm tra thông tin đăng nhập
            string cmdsql = "SELECT * FROM NhaCungCap Where MaNCC='" + Key + "' ";
            SqlCommand myCommand = new SqlCommand(cmdsql, connection);
            DataTable dataTable = new DataTable();
            try
            {
                SqlDataReader sqlDataReader = myCommand.ExecuteReader();
                dataTable.Load(sqlDataReader);
                NhaCungCap Target = Infrastructure.Encode.ConvertToNumberale<NhaCungCap>(dataTable).FirstOrDefault();
                return Target;
            }
            catch
            {
                return null;
            }
            finally
            {
                connection.Close();
            }
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
        #endregion
    }
}
