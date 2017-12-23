using System.Linq;
using MyProject.Domain;
using System.Data.SqlClient;
using System.Data;
using System;

namespace MyProject.Repository.ADONET
{
    public class LoginRepositoryADONET : ILoginRepository
    {
        private SqlConnection connection = ConnectionADONET.Connection;
        public NguoiDung getUser(NguoiDung ND)
        {
            if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
            {
                connection.Open();
            }

            //Kiểm tra thông tin đăng nhập
            string cmdsql = "SELECT * FROM NGUOIDUNG WHERE ID='" + ND.ID + "' AND Pass ='" + ND.Pass + "' ";
            SqlCommand myCommand = new SqlCommand(cmdsql,connection);
            DataTable dataTable = new DataTable();       
            try
            {
                SqlDataReader sqlDataReader = myCommand.ExecuteReader();
                dataTable.Load(sqlDataReader);
                NguoiDung Target = Infrastructure.Encode.ConvertToNumberale<NguoiDung>(dataTable).FirstOrDefault();
                //Lấy thông tin nhân viên sau khi đăng nhập thành công
                string cmdsql2 = "SELECT * FROM NhanVien WHERE MaNV='" + Target.MaNV + "' ";
                SqlCommand myCommand2 = new SqlCommand(cmdsql2, connection);
                DataTable dataTable2 = new DataTable();
                SqlDataReader sqlDataReader2 = myCommand2.ExecuteReader();
                dataTable2.Load(sqlDataReader2);
                Target.NhanVien = Infrastructure.Encode.ConvertToNumberale<NhanVien>(dataTable2).FirstOrDefault();
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
        public NguoiDung getUserbyName(NguoiDung Target)
        {
            if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
            {
                connection.Open();
            }
            //Kiểm tra thông tin đăng nhập
            string cmdsql = "SELECT * FROM NGUOIDUNG WHERE ID='" + Target.ID + "' AND Mail ='" + Target.Mail + "' ";
            SqlCommand myCommand = new SqlCommand(cmdsql, connection);
            DataTable dataTable = new DataTable();
            try
            {
                SqlDataReader sqlDataReader = myCommand.ExecuteReader();
                dataTable.Load(sqlDataReader);
                return Infrastructure.Encode.ConvertToNumberale<NguoiDung>(dataTable).FirstOrDefault();
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
    }
}
