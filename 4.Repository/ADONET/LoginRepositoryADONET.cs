using System.Collections.Generic;
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
            string cmdsql = "SELECT * FROM NGUOIDUNG,NhanVien WHERE ID='" + ND.ID + "' AND Pass ='" + ND.Pass + "' AND NhanVien.MaNV = NguoiDung.MaNV";
            SqlCommand myCommand = new SqlCommand(cmdsql,connection);
            DataTable dataTable = new DataTable();
            try
            {
                SqlDataReader sqlDataReader = myCommand.ExecuteReader();
                dataTable.Load(sqlDataReader);
                return ConvertToListUser(dataTable).FirstOrDefault();
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
        public IEnumerable<NguoiDung> ConvertToListUser(DataTable dataTable)
        {
            return dataTable.AsEnumerable().Select(row => new NguoiDung
            {
                ID = row["ID"].ToString(),
                Pass = row["Pass"].ToString(),
                Mail = row["Mail"].ToString(),
                MaNV = row["MaNV"].ToString(),
                NhanVien = new NhanVien()
                {
                    MaNV = row["MaNV"].ToString(),
                    Ten = row["Ten"].ToString(),
                    ChucVu = row["ChucVu"].ToString(),
                    Diachi = row["DiaChi"].ToString(),
                    Luong = Convert.ToInt32(row["Luong"]),
                    sdt = row["sdt"].ToString(),
                },
            });
        }
        public IEnumerable<NguoiDung> listUser()
        {
            string cmdsql = "SELECT * FROM NGUOIDUNG ";
            SqlCommand myCommand = new SqlCommand();
            IEnumerable<NguoiDung> list =null;
            DataTable dataTable = new DataTable();
            try
            {
                myCommand.Connection = connection;
                myCommand.CommandText = cmdsql;
                SqlDataReader sqlDataReader = myCommand.ExecuteReader();
                dataTable.Load(sqlDataReader);
                list = ConvertToListUser(dataTable);
                return list;
            }
            catch
            {
                return null;
            }
        }
    }
}
