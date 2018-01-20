using System;
using System.Linq;
using System.Windows.Forms;
using MyProject.Domain;
using Microsoft.Reporting.WinForms;
using System.Collections;
using System.Collections.Generic;
using MyProject.Infrastructure;
using MyProject.Service;
namespace MyProject.UI
{
    public partial class Statistical_Bill : Form
    {
        private ISellerService _service;
        private ModelStateDictionary ModelState;
        QLVanPhongEntities _entities = QLVanPhongEntities.Instance;
        public Statistical_Bill()
        {
            InitializeComponent();
            if (_service == null)
                _service = DataFactory.getSellerService(ModelState, Information.PersistanceStrategy);
            InitData();
        }
        #region View
        private void ViewErrors()
        {
            errProvide.Clear();
            foreach (var entry in ModelState)
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
        #region Help
        private void InitData()
        {
            ModelState = new ModelStateDictionary();
            _service = DataFactory.getSellerService(ModelState, Information.PersistanceStrategy);
        }
#endregion
        #region Event-Handler
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                errProvide.Clear();
                String MaNV = null;
                if (cboIDEmployee.SelectedIndex != 0)
                {
                    MaNV = cboIDEmployee.SelectedItem.ToString();
                }
                List<HoaDon> ds = _service.Statistical(dtpDateStart.Value.ToString(), dtpDateEnd.Value.ToString(), MaNV, cboSort.SelectedIndex);
                //Khai báo chế độ xử lý báo cáo, trong trường hợp này lấy báo cáo ở local
                reportViewer1.ProcessingMode = ProcessingMode.Local;
                //Đường dẫn báo cáo
                reportViewer1.LocalReport.ReportPath = @"..\..\..\5.Infrastructure\Report_Bill.rdlc";
                //Nếu có dữ liệu
                if (ds != null)
                {
                    //Tạo nguồn dữ liệu cho báo cáo
                    ReportDataSource rds = new ReportDataSource();
                    rds.Name = "HoaDon";
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
                    ViewErrors();
                    reportViewer1.LocalReport.DataSources.Clear();
                }
            }
            catch
            {
                MessageBox.Show("Lỗi: Không xác định", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

            reportViewer1.RefreshReport();

            IEnumerable enumerable = _entities.NhanViens.ToList();        
            cboIDEmployee.Items.Add("Tất cả");
            foreach (NhanVien item in enumerable)
            {
                cboIDEmployee.Items.Add(item.MaNV);
            }
            cboIDEmployee.SelectedIndex = 0;
            cboSort.SelectedIndex = 0;
        }

        private void cboIDEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboIDEmployee.AutoCompleteMode = AutoCompleteMode.Suggest;
            cboIDEmployee.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        private void cboSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboSort.AutoCompleteMode = AutoCompleteMode.Suggest;
            cboSort.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        //Component dieu huong

        private void button9_Click(object sender, EventArgs e)
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
        private void Statistical_Bill_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Information.Nhanvien.ChucVu != "Giám đốc chi nhánh")
                Information.frmLogin.Show();
        }
        #endregion



    }
}
