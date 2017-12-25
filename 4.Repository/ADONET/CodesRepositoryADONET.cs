using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using MyProject.Domain;
namespace MyProject.Repository.ADONET
{
    public class CodesRepositoryADONET : ICodeSalesRepository
    {
        private SqlConnection connection = ConnectionADONET.Connection;
        SqlCommand setData(SqlCommand cmd, MaKhuyenMai codesTarget)
        {
            cmd.Parameters.Add("@MaKM", SqlDbType.VarChar).Value = codesTarget.MaKM;
            cmd.Parameters.Add("@TiLe", SqlDbType.Float).Value = codesTarget.TiLe;
            cmd.Parameters.Add("@TrangThai", SqlDbType.NVarChar).Value = codesTarget.TrangThai;         
            return cmd;
        }
        public MaKhuyenMai CreateCode(MaKhuyenMai Target)
        {
            if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
            {
                connection.Open();
            }
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Insert_MaKhuyenMai";
            cmd = setData(cmd, Target);
            cmd.Connection = connection;
            cmd.ExecuteNonQuery();
            connection.Close();
            return Target;
        }

        public void DeleteCode(MaKhuyenMai Target)
        {
            if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
            {
                connection.Open();
            }
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Delete_MaKhuyenMai";
            cmd.Parameters.Add("@MaKM", SqlDbType.VarChar).Value = Target.MaKM;
            cmd.Connection = connection;
            cmd.ExecuteNonQuery();
            connection.Close();
        }
        public MaKhuyenMai UpdateCode(MaKhuyenMai Target)
        {
            if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
            {
                connection.Open();
            }
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Update_MaKhuyenMai";
            cmd = setData(cmd, Target);
            cmd.Connection = connection;
            try
            {
                cmd.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }
            return Target;
        }
        public MaKhuyenMai getCodes(string key)
        {
            if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
            {
                connection.Open();
            }
            //Kiểm tra thông tin đăng nhập
            string cmdsql = "SELECT * FROM MaKhuyenMai Where MaKM='" + key + "' ";
            SqlCommand myCommand = new SqlCommand(cmdsql, connection);
            DataTable dataTable = new DataTable();
            try
            {
                SqlDataReader sqlDataReader = myCommand.ExecuteReader();
                dataTable.Load(sqlDataReader);
                MaKhuyenMai Target = Infrastructure.Encode.ConvertToNumberale<MaKhuyenMai>(dataTable).FirstOrDefault();
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

        public IEnumerable<MaKhuyenMai> listCodes()
        {
            if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
            {
                connection.Open();
            }
            //Kiểm tra thông tin đăng nhập
            string cmdsql = "SELECT * FROM MaKhuyenMai ";
            SqlCommand myCommand = new SqlCommand(cmdsql, connection);
            DataTable dataTable = new DataTable();
            try
            {
                SqlDataReader sqlDataReader = myCommand.ExecuteReader();
                dataTable.Load(sqlDataReader);
                return Infrastructure.Encode.ConvertToNumberale<MaKhuyenMai>(dataTable).ToList();
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
    }
}
