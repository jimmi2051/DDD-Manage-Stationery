using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MyProject.Domain;
using Microsoft.Reporting.WinForms;
using System.Collections;
using MyProject.Infrastructure;
using MyProject.Service;
namespace MyProject.UI
{
    public partial class Statistical_WareHouse : Form
    {
        IWareHouseService _service;
        ModelStateDictionary modelState;
        QLVanPhongEntities _entities = QLVanPhongEntities.Instance;
        public Statistical_WareHouse()
        {
            InitializeComponent();
            if (_service == null)
                _service = DataFactory.getWareHouseService(modelState, Information.PersistanceStrategy);
            InitData();
        }
        #region Help
        private void InitData()
        {
            modelState = new ModelStateDictionary();
            _service = DataFactory.getWareHouseService(modelState, Information.PersistanceStrategy);
        }
        #endregion
        #region Event-Handler
        private void Statistical_WareHouse_Load(object sender, EventArgs e)
        {
            reportViewer1.RefreshReport();
            cboIDProduct.Items.Add("Tất cả");
            IEnumerable enumerable = _entities.SanPhams.ToList();
            foreach (SanPham item in enumerable)
            {
                cboIDProduct.Items.Add(item.MaSP);
            }
            cboIDProduct.SelectedIndex = 0;
            cboSort.SelectedIndex = 0;
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            try
            {
                string MaSP = null;
                if (cboIDProduct.SelectedIndex != 0)
                    MaSP = cboIDProduct.SelectedItem.ToString();
                List<Kho> ds = _service.Statistical(cboSort.SelectedIndex, MaSP);
                // Khai báo chế độ xử lý báo cáo, trong trường hợp này lấy báo cáo ở local
                reportViewer1.ProcessingMode = ProcessingMode.Local;
                //Đường dẫn báo cáo
                reportViewer1.LocalReport.ReportPath = @"..\..\..\5.Infrastructure\Report_WareHouse.rdlc";
                //Nếu có dữ liệu
                if (ds.Count > 0)
                {
                    //Tạo nguồn dữ liệu cho báo cáo
                    ReportDataSource rds = new ReportDataSource();
                    rds.Name = "Kho";
                    rds.Value = ds;
                    //Xóa dữ liệu của báo cáo cũ trong trường hợp người dùng thực hiện câu truy vấn khác
                    reportViewer1.LocalReport.DataSources.Clear();
                    //Add dữ liệu vào báo cáo
                    reportViewer1.LocalReport.DataSources.Add(rds);
                    //Refresh lại báo cáo
                    reportViewer1.RefreshReport();
                }
                else
                {
                    reportViewer1.LocalReport.DataSources.Clear();
                    MessageBox.Show("Không có dữ liệu cần tìm");
                }
            }
            catch
            {
                MessageBox.Show("Lỗi: Không xác định", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void cboIDProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboIDProduct.AutoCompleteMode = AutoCompleteMode.Suggest;
            cboIDProduct.AutoCompleteSource = AutoCompleteSource.ListItems;
        }
        private void cboSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboSort.AutoCompleteMode = AutoCompleteMode.Suggest;
            cboSort.AutoCompleteSource = AutoCompleteSource.ListItems;
        }
        //Component điều hướng
        private void label5_Click(object sender, EventArgs e)
        {
            Account_UI UI = new Account_UI();
            UI.ShowDialog();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            WareHouse UI = new WareHouse();
            UI.Show();
            this.Visible = false;
        }

        private void label15_Click(object sender, EventArgs e)
        {
            Coupon UI = new Coupon();
            UI.Show();
            this.Visible = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void Statistical_WareHouse_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Information.Nhanvien.ChucVu != "Giám đốc chi nhánh")
                Information.frmLogin.Show();
        }

        #endregion


    }
}
