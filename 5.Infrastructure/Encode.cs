using System;
using System.Collections.Generic;
using System.Data;
using MyProject.Domain;
namespace MyProject.Infrastructure
{
    public class Encode
    {
        public static byte[] encryptData(string data)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider md5Hasher = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] hashedBytes;
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(data));
            return hashedBytes;
        }
        public static string md5(string data)
        {
            return BitConverter.ToString(encryptData(data)).Replace("-", "").ToLower();
        }
        public static IEnumerable<HoaDon> ConvertToTankReadings(DataTable dataTable)
        {
            return dataTable.AsEnumerable().Select(row => new HoaDon
            {
                MaHD = row["MaHD"].ToString(),
                MaNV = row["MaNV"].ToString(),
                MaKH = row["MaKH"].ToString(),
                TongTien = Convert.ToDecimal(row["TongTien"]),
                TrangThai = row["TrangThai"].ToString(),
                NgayLap = Convert.ToDateTime(row["NgayLap"]),
            });
        }
    }
}
