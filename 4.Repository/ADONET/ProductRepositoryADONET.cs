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
        SqlCommand setData(SqlCommand cmd, SanPham productTarget)
        {
            cmd.Parameters.Add("@MaSP", SqlDbType.VarChar).Value = productTarget.MaSP;
            cmd.Parameters.Add("@MaNCC", SqlDbType.VarChar).Value = productTarget.MaNCC;
            cmd.Parameters.Add("@MaDM", SqlDbType.VarChar).Value = productTarget.MaDM;
            cmd.Parameters.Add("@TenSP", SqlDbType.NVarChar).Value = productTarget.TenSP;
            cmd.Parameters.Add("@DonGia", SqlDbType.Money).Value = productTarget.DonGia;
            cmd.Parameters.Add("@SoLuong", SqlDbType.Int).Value = productTarget.SoLuong;
            cmd.Parameters.Add("@XuatXu", SqlDbType.NVarChar).Value = productTarget.XuatXu;
            cmd.Parameters.Add("@TrongLuong", SqlDbType.Float).Value = productTarget.TrongLuong;
            cmd.Parameters.Add("@KichThuoc", SqlDbType.Float).Value = productTarget.KichThuoc;
            cmd.Parameters.Add("@DonVi", SqlDbType.NVarChar).Value = productTarget.DonVi;
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
            cmd.Connection = connection;
            cmd.ExecuteNonQuery();        
            connection.Close();
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
            cmd.Connection = connection;
            cmd.ExecuteNonQuery();
            connection.Close();
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
            cmd.Connection = connection;
            cmd.ExecuteNonQuery();
            connection.Close();
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
                //Lấy thông tin danh muc của san pham
                string cmdsql2 = "SELECT * FROM DanhMucSP WHERE MaDM='" + Target.MaDM + "' ";
                SqlCommand myCommand2 = new SqlCommand(cmdsql2, connection);
                DataTable dataTable2 = new DataTable();
                SqlDataReader sqlDataReader2 = myCommand2.ExecuteReader();
                dataTable2.Load(sqlDataReader2);
                Target.DanhMucSP = Infrastructure.Encode.ConvertToNumberale<DanhMucSP>(dataTable2).FirstOrDefault();
                //Lấy thông tin của nhà cung cấp
                string cmdsql3 = "SELECT * FROM NhaCungCap WHERE MaNCC='" + Target.MaNCC + "' ";
                SqlCommand myCommand3 = new SqlCommand(cmdsql3, connection);
                DataTable dataTable3 = new DataTable();
                SqlDataReader sqlDataReader3 = myCommand3.ExecuteReader();
                dataTable3.Load(sqlDataReader3);
                Target.NhaCungCap = Infrastructure.Encode.ConvertToNumberale<NhaCungCap>(dataTable3).FirstOrDefault();
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
            if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
            {
                connection.Open();
            }
            //Kiểm tra thông tin đăng nhập
            string cmdsql = "SELECT * FROM SanPham,DanhMucSP,NhaCungCap Where SanPham.MaDM=DanhMucSP.MaDM AND SanPham.MaNCC=NhaCungCap.MaNCC ";
            SqlCommand myCommand = new SqlCommand(cmdsql, connection);
            DataTable dataTable = new DataTable();
            using (SqlDataReader reader = myCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    // Create a Favorites instance
                    SanPham target = new SanPham();
                    target.MaSP = reader["MaSP"].ToString();
                    target.MaNCC = reader["MaNCC"].ToString();
                    target.MaDM = reader["MaDM"].ToString();
                    target.TenSP = reader["TenSP"].ToString();
                    target.XuatXu = reader["XuatXu"].ToString();
                    target.DonGia = Convert.ToDecimal(reader["DonGia"]);
                    target.SoLuong = Convert.ToInt32(reader["SoLuong"]);
                    target.TrongLuong = Convert.ToDouble(reader["TrongLuong"]);
                    target.KichThuoc = Convert.ToDouble(reader["KichThuoc"]);
                    target.DonVi = reader["DonVi"].ToString();
                    target.NhaCungCap = new NhaCungCap()
                    {
                        MaNCC = reader["MaNCC"].ToString(),
                        Ten = reader["Ten"].ToString(),
                        DiaChi = reader["DiaChi"].ToString(),
                        sdt = reader["Sdt"].ToString(),
                    };
                    target.DanhMucSP = new DanhMucSP()
                    {
                        MaDM = reader["MaDM"].ToString(),
                        TenDM = reader["TenDM"].ToString(),
                    };
                    // ... etc ...
                    yield return target;
                }
            }                 
           connection.Close();         
        }

    }
}
