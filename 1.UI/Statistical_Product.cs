using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MyProject.Domain;
using Microsoft.Reporting.WinForms;
using System.Collections;
using MyProject.Infrastructure;
using MyProject.Service;
namespace MyProject.UI
{
    public partial class Statistical_Product : Form
    {
        IManagerService _service;
        ModelStateDictionary modelState;
        public Statistical_Product()
        {
            InitializeComponent();
            if (_service == null)
                _service = DataFactory.getManagerService(modelState, Information.PersistanceStrategy);
            InitData();
        }

        #region Help
        private void SetVisiable(Boolean key)
        {
            label1.Visible = key;
            label2.Visible = key;
            dtpDateStart.Visible = key;
            dtpDateEnd.Visible = key;
        }
        private void InitData()
        {
            modelState = new ModelStateDictionary();
            _service = DataFactory.getManagerService(modelState, Information.PersistanceStrategy);
        }
        #endregion
        #region View
        private void ViewErrors()
        {
            errProvide.Clear();
            foreach (var entry in modelState)
            {
                switch (entry.Key)
                {
                    case "Date":
                        errProvide.SetError(dtpDateStart, entry.Value);
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion
        #region Event-Handler
        private void btnThongKe_Click(object sender, EventArgs e)
        {
            try
            {
                errProvide.Clear();
                string MaSP = null;
                List<SanPham> ds;/*_entities.Database.SqlQuery<SanPham>(sqlcmd).ToList();*/
                ds = _service.StatisticalProduct(cboSort.SelectedIndex, MaSP, dtpDateStart.Value.ToString(), dtpDateEnd.Value.ToString());
                // Khai báo chế độ xử lý báo cáo, trong trường hợp này lấy báo cáo ở local
                reportViewer1.ProcessingMode = ProcessingMode.Local;
                //Đường dẫn báo cáo
                reportViewer1.LocalReport.ReportPath = @"..\..\..\5.Infrastructure\ReportProduct.rdlc";
                //Nếu có dữ liệu
                if (ds.Count > 0)
                {
                    //Tạo nguồn dữ liệu cho báo cáo
                    ReportDataSource rds = new ReportDataSource();
                    rds.Name = "DataSet1";
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
                    ViewErrors();
                }
            }
            catch
            {
                MessageBox.Show("Lỗi: Không xác định", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cboSort_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            cboSort.AutoCompleteMode = AutoCompleteMode.Suggest;
            cboSort.AutoCompleteSource = AutoCompleteSource.ListItems;
            if (cboSort.SelectedIndex == 1 || cboSort.SelectedIndex == 2)
                SetVisiable(true);
            else
                SetVisiable(false);
        }

        private void Statistical_Product_Load(object sender, EventArgs e)
        {
            reportViewer1.RefreshReport();
            cboIDProduct.Items.Add("Tất cả");
            IEnumerable enumerable = _service.ListProducts();
            foreach (SanPham item in enumerable)
            {
                cboIDProduct.Items.Add(item.MaSP);
            }
            cboIDProduct.SelectedIndex = 0;
            cboSort.SelectedIndex = 0;
        }
        private void cboIDProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboIDProduct.AutoCompleteMode = AutoCompleteMode.Suggest;
            cboIDProduct.AutoCompleteSource = AutoCompleteSource.ListItems;
        }


        //Components Điều hướng
        private void Statistical_Product_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Information.Nhanvien.ChucVu != "Giám đốc chi nhánh")
                Information.frmLogin.Show();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Account_UI UI = new Account_UI();
            UI.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            Manager_Product UI = new Manager_Product();
            UI.Show();
            this.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Manager_Employee UI = new Manager_Employee();
            UI.Show();
            this.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Manager_Supplier UI = new Manager_Supplier();
            UI.Show();
            this.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Manager_Category UI = new Manager_Category();
            UI.Show();
            this.Visible = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            CodeSales UI = new CodeSales();
            UI.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            MenuStatistical UI = new MenuStatistical();
            UI.ShowDialog();
            this.Visible = false;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion




    }
}
