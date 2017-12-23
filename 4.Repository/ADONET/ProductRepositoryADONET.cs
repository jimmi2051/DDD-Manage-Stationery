using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using MyProject.Domain;
namespace MyProject.Repository.ADONET
{
    public class ProductRepositoryADONET : IProductRepository
    {
        private SqlConnection connection = ConnectionADONET.Connection;
        SqlCommand setData(SqlCommand cmd, SanPham Sp1)
        {
            cmd.Parameters.Add("@MaSP", SqlDbType.VarChar).Value = Sp1.MaSP;
            cmd.Parameters.Add("@MaNCC", SqlDbType.VarChar).Value = Sp1.MaNCC;
            cmd.Parameters.Add("@MaDM", SqlDbType.VarChar).Value = Sp1.MaDM;
            cmd.Parameters.Add("@TenSP", SqlDbType.NVarChar).Value = Sp1.TenSP;
            cmd.Parameters.Add("@DonGia", SqlDbType.Money).Value = Sp1.DonGia;
            cmd.Parameters.Add("@SoLuong", SqlDbType.Int).Value = Sp1.SoLuong;
            cmd.Parameters.Add("@XuatXu", SqlDbType.NVarChar).Value = Sp1.XuatXu;
            cmd.Parameters.Add("@TrongLuong", SqlDbType.Float).Value = Sp1.TrongLuong;
            cmd.Parameters.Add("@KichThuoc", SqlDbType.Float).Value = Sp1.KichThuoc;
            cmd.Parameters.Add("@DonVi", SqlDbType.NVarChar).Value = Sp1.DonVi;
            return cmd;
        }
        public SanPham CreateProduct(SanPham productToCreate)
        {
            if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
            {
                connection.Open();
            }
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "InSert_SanPham";
            cmd = setData(cmd, productToCreate);
            cmd.ExecuteNonQuery();
            return productToCreate;
        }

        public void DeleteProduct(SanPham productToDelete)
        {
            if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
            {
                connection.Open();
            }
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Delete_SanPham";
            cmd.Parameters.Add("@MaSP", SqlDbType.VarChar).Value = productToDelete.MaSP;
            cmd.ExecuteNonQuery();
        }

        public SanPham EditProduct(SanPham productToEdit)
        {
            if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
            {
                connection.Open();
            }
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Update_SanPham";
            cmd = setData(cmd, productToEdit);
            cmd.ExecuteNonQuery();
            return productToEdit;
        }

        public SanPham GetProduct(string Key)
        {
            if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
            {
                connection.Open();
            }
            //Kiểm tra thông tin đăng nhập
            string cmdsql = "SELECT * FROM SanPham WHERE MaSP='" + Key + "' ";
            SqlCommand myCommand = new SqlCommand(cmdsql, connection);
            DataTable dataTable = new DataTable();
            try
            {
                
                SqlDataReader sqlDataReader = myCommand.ExecuteReader();
                dataTable.Load(sqlDataReader);
                SanPham Target = Infrastructure.Encode.ConvertToNumberale<SanPham>(dataTable).FirstOrDefault();
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

        public IEnumerable<SanPham> ListProducts()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SanPham> ListProductsSQLCMD()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SanPham> SearchProducts(string Key)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SanPham> SearchProductsbyName(string Key)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SanPham> SearchProductsbyNCC(string Key)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SanPham> SearchProductsbyType(string Key)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SanPham> SearchProductsbyTypeName(string key)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SanPham> StatisticalProduct(string SqlCmd)
        {
            throw new NotImplementedException();
        }
    }
}
