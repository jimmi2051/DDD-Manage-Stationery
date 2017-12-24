using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using MyProject.Domain;
namespace MyProject.Repository.ADONET
{
    public class EmployeeRepositoryADONET : IEmployeeRepository
    {
        private SqlConnection connection = ConnectionADONET.Connection;
        #region Command
        SqlCommand setData(SqlCommand cmd, NhanVien employeeTarget)
        {
            cmd.Parameters.Add("@MaNV", SqlDbType.VarChar).Value = employeeTarget.MaNV;
            cmd.Parameters.Add("@Ten", SqlDbType.NVarChar).Value = employeeTarget.Ten;
            cmd.Parameters.Add("@DiaChi", SqlDbType.NVarChar).Value = employeeTarget.Diachi;
            cmd.Parameters.Add("@Sdt", SqlDbType.VarChar).Value = employeeTarget.sdt;
            cmd.Parameters.Add("@Luong", SqlDbType.Money).Value = employeeTarget.Luong;   
            cmd.Parameters.Add("@ChucVu", SqlDbType.NVarChar).Value = employeeTarget.ChucVu;       
            return cmd;
        }

        public NhanVien CreateEmployee(NhanVien employeeToCreate)
        {
            if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
            {
                connection.Open();
            }
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "InSert_NhanVien";
            cmd = setData(cmd, employeeToCreate);
            cmd.Connection = connection;
            cmd.ExecuteNonQuery();
            connection.Close();
            return employeeToCreate;
        }

        public void DeleteEmployee(NhanVien employeeToDelete)
        {
            if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
            {
                connection.Open();
            }
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Delete_NhanVien";
            cmd.Parameters.Add("@MaNV", SqlDbType.VarChar).Value = employeeToDelete.MaNV;
            cmd.Connection = connection;
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        public NhanVien EditEmployee(NhanVien employeeToEdit)
        {
            if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
            {
                connection.Open();
            }
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Update_NhanVien";
            cmd = setData(cmd, employeeToEdit);
            cmd.Connection = connection;
            cmd.ExecuteNonQuery();
            connection.Close();
            return employeeToEdit;
        }
        #endregion
        #region Query
        public NhanVien GetEmployee(string Key)
        {
            if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
            {
                connection.Open();
            }
            //Kiểm tra thông tin đăng nhập
            string cmdsql = "SELECT * FROM NhanVien Where MaNV='" + Key + "' ";
            SqlCommand myCommand = new SqlCommand(cmdsql, connection);
            DataTable dataTable = new DataTable();
            try
            {
                SqlDataReader sqlDataReader = myCommand.ExecuteReader();
                dataTable.Load(sqlDataReader);
                NhanVien Target = Infrastructure.Encode.ConvertToNumberale<NhanVien>(dataTable).FirstOrDefault();
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

        public IEnumerable<NhanVien> ListEmployees()
        {
            if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
            {
                connection.Open();
            }
            //Kiểm tra thông tin đăng nhập
            string cmdsql = "SELECT * FROM NhanVien ";
            SqlCommand myCommand = new SqlCommand(cmdsql, connection);
            DataTable dataTable = new DataTable();
            try
            {
                SqlDataReader sqlDataReader = myCommand.ExecuteReader();
                dataTable.Load(sqlDataReader);
                return Infrastructure.Encode.ConvertToNumberale<NhanVien>(dataTable).ToList();          
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
        #endregion
    }
}
