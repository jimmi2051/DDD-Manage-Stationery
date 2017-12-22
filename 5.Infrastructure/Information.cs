using System.Configuration;
using MyProject.Domain;
using System.Windows.Forms;
namespace MyProject.Infrastructure
{
    public class Information
    {
        public static NhanVien Nhanvien { get; set; }
        public static NguoiDung Nguoidung{get;set;}
        public static Form frmLogin { get; set; }
        public static int Result { get; set; }
        public static string PersistanceStrategy
        {
            get { return ConfigurationManager.AppSettings["PersistanceStrategy"].ToString(); }
        }
        public static string ConnectionString
        {            
            get { return ConfigurationManager.ConnectionStrings["Connection"].ToString(); }
        }
        public static string StrConnect { get; set; }
    }
}
