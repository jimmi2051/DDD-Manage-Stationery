using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using MyProject.Domain;
namespace MyProject.Repository.ADONET
{
    public class UserRepositoryADONET : IUserRepository
    {
        private SqlConnection connection = ConnectionADONET.Connection;
        SqlCommand setData(SqlCommand cmd, NguoiDung userTarget)
        {
            cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = userTarget.ID;
            cmd.Parameters.Add("@Pass", SqlDbType.VarChar).Value = userTarget.Pass;
            cmd.Parameters.Add("@Mail", SqlDbType.VarChar).Value = userTarget.Mail;
            cmd.Parameters.Add("@MaNV",SqlDbType.VarChar).Value = userTarget.MaNV;
            return cmd;
        }
        public NguoiDung CheckUser(string key)
        {
            if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
            {
                connection.Open();
            }
            //Kiểm tra thông tin đăng nhập
            string cmdsql = "SELECT * FROM NguoiDung Where MaNV='" + key + "' ";
            SqlCommand myCommand = new SqlCommand(cmdsql, connection);
            DataTable dataTable = new DataTable();
            try
            {
                SqlDataReader sqlDataReader = myCommand.ExecuteReader();
                dataTable.Load(sqlDataReader);
                NguoiDung Target = Infrastructure.Encode.ConvertToNumberale<NguoiDung>(dataTable).FirstOrDefault();
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

        public void DeleteUser(NguoiDung target)
        {
            if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
            {
                connection.Open();
            }
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Delete_NguoiDung";
            cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = target.ID;
            cmd.Connection = connection;
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        public NguoiDung getUser(string key)
        {

            if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
            {
                connection.Open();
            }
            //Kiểm tra thông tin đăng nhập
            string cmdsql = "SELECT * FROM NguoiDung Where ID='" + key + "' ";
            SqlCommand myCommand = new SqlCommand(cmdsql, connection);
            DataTable dataTable = new DataTable();
            try
            {
                SqlDataReader sqlDataReader = myCommand.ExecuteReader();
                dataTable.Load(sqlDataReader);
                NguoiDung Target = Infrastructure.Encode.ConvertToNumberale<NguoiDung>(dataTable).FirstOrDefault();
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

        public NguoiDung InsertUser(NguoiDung target)
        {
            if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
            {
                connection.Open();
            }
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Insert_NguoiDung";
            cmd = setData(cmd, target);
            cmd.Connection = connection;
            cmd.ExecuteNonQuery();
            connection.Close();
            return target;
        }

        public NguoiDung UpdateUser(NguoiDung target)
        {
            if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
            {
                connection.Open();
            }
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Update_NguoiDung";
            cmd = setData(cmd, target);
            cmd.Connection = connection;
            try
            {
                cmd.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }
            return target;
        }
    }
}
