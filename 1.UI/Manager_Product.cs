using System;
using System.Windows.Forms;
using MyProject.Infrastructure;
using MyProject.Domain;
using MyProject.Service;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections;

namespace MyProject.UI
{
    public partial class Manager_Product : Form
    {
        private ModelStateDictionary ModelState;
        private IManagerService _service;
        IEnumerable listproduct;
        public Manager_Product()
        {
            if (_service == null)
            {
                _service = DataFactory.getManagerService(ModelState,Information.PersistanceStrategy);
            }
            InitializeComponent();
            InitControls();
            InitData();
        }
        public Manager_Product(IManagerService service)
            : this()
        {
            _service = service;
        }

        #region Event-Handler
        private void btnThem_Click(object sender, EventArgs e)
        {
            XoaTrangChiTiet();
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            HienChiTiet(true);
            txtMaSP.Text = _service.getNewIDProduct();
            cboDM.SelectedIndex = 0;
            cboNCC.SelectedIndex = 0;
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            btnThem.Enabled = false;
            btnXoa.Enabled = false;
            HienChiTiet(true);
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            HienChiTiet(true);
        }
        private void btnHuy_Click(object sender, EventArgs e)
        {
            errProdive.Clear();
            //Thiết lập lại các nút như ban đầu
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
            btnThem.Enabled = true;
            //xoa trang
            XoaTrangChiTiet();
            //Cam nhap
            HienChiTiet(false);
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            errProdive.Clear();
            int flag = 0;          
            SanPham Target = null;
            if (CheckText())
            {
                return;
            }
            Target = getProduct();
            //Thuc hiện Thêm
            if (btnThem.Enabled == true)
            {
                if (_service.CreateProduct(Target))
                    MessageBox.Show("Thêm thành công");
                else
                    flag = 1;
            }
            //Sửa
            if (btnSua.Enabled == true)
            {
                if (_service.EditProduct(Target))
                    MessageBox.Show("Sửa thành công");
                else
                    flag = 1;               
            }
            //Xóa
            if (btnXoa.Enabled == true)
            {
                DialogResult dlr = MessageBox.Show("Bạn có muốn xóa sản phẩm", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlr == DialogResult.Yes)
                {
                    if (_service.DeleteProduct(Target))
                        MessageBox.Show("Xóa thành công");
                    else flag = 1;
                }
            }
            if (flag == 0)
            {
                progressBar1.Visible = true;
                View();
                HienChiTiet(false);
                btnThem.Enabled = true;
                btnXoa.Enabled = true;
                btnSua.Enabled = true;
                XoaTrangChiTiet();
            }
            else
                ViewErrors();
        }
        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            if (txtTimkiem.Text.Equals(""))
            {
                progressBar1.Maximum = 100;
                progressBar1.Step = 1;
                progressBar1.Value = 0;
                progressBar1.Visible = true;
                View();
            }
            else
            {
                dgvSanPham.DataSource = _service.SearchProducts(txtTimkiem.Text, cboLoaisp.SelectedItem.ToString());
            }
            ViewErrors();
        }
        private void dgvSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Hien thi nut sua
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            HienChiTiet(false);
            //Bắt lỗi khi người sử dụng kích linh tinh lên datagrid
            try
            {
                txtMaSP.Text = dgvSanPham[0, e.RowIndex].Value.ToString();
                txtTenSP.Text = dgvSanPham[3, e.RowIndex].Value.ToString();
                cboNCC.SelectedIndex = cboNCC.Items.IndexOf(dgvSanPham[1, e.RowIndex].Value.ToString());
                cboDM.SelectedIndex = cboDM.Items.IndexOf(dgvSanPham[2, e.RowIndex].Value.ToString());
                txtDonGia.Text = dgvSanPham[4, e.RowIndex].Value.ToString();
                txtSL.Text = dgvSanPham[5, e.RowIndex].Value.ToString();
                txtXuatXu.Text = dgvSanPham[6, e.RowIndex].Value.ToString();
                txtTrongLuong.Text = dgvSanPham[7, e.RowIndex].Value.ToString();
                txtKichThuoc.Text = dgvSanPham[8, e.RowIndex].Value.ToString();
                txtDonVi.Text = dgvSanPham[9, e.RowIndex].Value.ToString();
            }
            catch { }
        }
        private void btnDanhsach_Click(object sender, EventArgs e)
        {
            progressBar1.Maximum = 100;
            progressBar1.Step = 1;
            progressBar1.Value = 0;
            progressBar1.Visible = true;
            View();
   
        }
        //Hỗ trợ tự động hiển thị trên Combobox
        private void cboLoaisp_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboLoaisp.AutoCompleteMode = AutoCompleteMode.Suggest;
            cboLoaisp.AutoCompleteSource = AutoCompleteSource.ListItems;
        }
        private void cboNCC_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboNCC.AutoCompleteMode = AutoCompleteMode.Suggest;
            cboNCC.AutoCompleteSource = AutoCompleteSource.ListItems;
        }
        private void cboDM_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboDM.AutoCompleteMode = AutoCompleteMode.Suggest;
            cboDM.AutoCompleteSource = AutoCompleteSource.ListItems;
        }
        //Components Điều hướng
        private void label4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void Manager_Product_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Information.Nhanvien.ChucVu != "Giám đốc chi nhánh")
                Information.frmLogin.Show();
        }
        private void Manager_Product_Load(object sender, EventArgs e)
        {
            lbHello.Text = "Xin chào, " + Information.Nhanvien.Ten + " \nChức vụ: " + Information.Nhanvien.ChucVu + " ";
            ptrHinhAnh.ImageLocation = ("..\\..\\Images\\QuanLy.jpg");           
            progressBar1.Maximum = 100;
            progressBar1.Step = 1;
            progressBar1.Value = 0;
            progressBar1.Visible = true;
            View();
            HienChiTiet(false);
            cboLoaisp.SelectedIndex = 0;
        }
        private void label17_Click(object sender, EventArgs e)
        {
            Manager_Supplier UI = new Manager_Supplier(_service);
            UI.Show();
            this.Hide();
        }
        private void label16_Click(object sender, EventArgs e)
        {
            Manager_Employee UI = new Manager_Employee();
            UI.Show();
            this.Visible = false;
        }
        private void label18_Click(object sender, EventArgs e)
        {
            Manager_Category UI = new Manager_Category();
            UI.Show();
            this.Visible = false;
        }
        private void label1_Click(object sender, EventArgs e)
        {
            Account_UI UI = new Account_UI();
            UI.ShowDialog();
            Manager_Product_Load(sender, e);
        }
        private void label15_Click(object sender, EventArgs e)
        {
            MenuStatistical UI = new MenuStatistical();
            UI.ShowDialog();
            this.Visible = false;
        }
        private void label19_Click(object sender, EventArgs e)
        {
            CodeSales UI = new CodeSales();
            UI.ShowDialog();
        }
        //Progess-bar
        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            for (int i = 1; i <= 100; i++)
            {
                // Wait 100 milliseconds.
                Thread.Sleep(10);
                // Report progress.
                bgdWorker1.ReportProgress(i);
            }
        }
        private void backgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }
        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            dgvSanPham.DataSource = listproduct;
            progressBar1.Visible = false;
        }
        #endregion
        #region View
        public void View()
        {        
                bgdWorker1.RunWorkerAsync();
                listproduct = _service.ListProducts();
                cboDM.Items.Clear();
                cboNCC.Items.Clear();
                foreach (NhaCungCap item in _service.ListSuppliers())
                {
                    cboNCC.Items.Add(item.MaNCC);
                }
                foreach (DanhMucSP item in _service.ListCategorys())
                {
                    cboDM.Items.Add(item.MaDM);
                }
            
        }
        private void HienChiTiet(Boolean hien)
        {
            cboNCC.Enabled = hien;
            txtTenSP.Enabled = hien;
            cboDM.Enabled = hien;
            txtDonGia.Enabled = hien;
            txtSL.Enabled = hien;
            txtXuatXu.Enabled = hien;
            txtTrongLuong.Enabled = hien;
            txtKichThuoc.Enabled = hien;
            txtDonVi.Enabled = hien;
            //Ẩn hiện 2 nút Lưu và Hủy
            btnLuu.Enabled = hien;
            btnHuy.Enabled = hien;
        }
        private void XoaTrangChiTiet()
        {
            txtMaSP.Clear();
            cboNCC.SelectedIndex = -1;
            txtTenSP.Clear();
            cboDM.SelectedIndex = -1;
            txtDonGia.Clear();
            txtSL.Clear();
            txtTrongLuong.Clear();
            txtKichThuoc.Clear();
            txtXuatXu.Clear();
            txtDonVi.Clear();
        }
        private void ViewErrors()
        {
            errProdive.Clear();
            foreach (var entry in ModelState)
            {
                switch (entry.Key)
                {
                    case "MaSP":
                        errProdive.SetError(txtMaSP, entry.Value);
                        break;
                    case "MaNCC":
                        errProdive.SetError(cboNCC, entry.Value);
                        break;                   
                    case "MaDM":
                        errProdive.SetError(cboDM, entry.Value);
                        break;
                    case "DonGia":
                        errProdive.SetError(txtDonGia, entry.Value);
                        break;
                    case "SoLuong":
                        errProdive.SetError(txtSL, entry.Value);
                        break;
                    case "Timkiem":
                        errProdive.SetError(txtTimkiem, entry.Value);
                        break;
                    case "TenSP":
                        errProdive.SetError(txtTenSP, entry.Value);
                        break;                  
                    case "TrongLuong":
                        errProdive.SetError(txtTrongLuong, entry.Value);
                        break;
                    case "KichThuoc":
                        errProdive.SetError(txtKichThuoc, entry.Value);
                        break;                      
                    default:
                        break;
                }
            }

        }
        #endregion
        #region helper
        private void InitData()
        {
            ModelState = new ModelStateDictionary();
            _service = DataFactory.getManagerService(ModelState, Information.PersistanceStrategy);
        }
        public Boolean CheckText()
        {
            int flag = 0;
            if (txtDonGia.Text.Length == 0 || txtDonGia.Text.Length > 0 && !Regex.IsMatch(txtDonGia.Text, @"\d"))
            {
                errProdive.SetError(txtDonGia, "Vui lòng nhập đơn giá hợp lệ");
                flag = 1;
            }
            if (txtSL.Text.Length == 0 || txtSL.Text.Length > 0 && !Regex.IsMatch(txtSL.Text, @"\d"))
            {
                errProdive.SetError(txtSL, "Vui lòng nhập số lượng hợp lệ");
                flag = 1;
            }
            if (txtTrongLuong.Text.Length == 0 || txtTrongLuong.Text.Length > 0 && !Regex.IsMatch(txtTrongLuong.Text, @"\d"))
            {
                errProdive.SetError(txtTrongLuong, "Vui lòng nhập trọng lượng hợp lệ");
                flag = 1;
            }
            if ( txtKichThuoc.Text.Length == 0 || txtKichThuoc.Text.Length > 0 && !Regex.IsMatch(txtKichThuoc.Text, @"\d"))
            {
                errProdive.SetError(txtKichThuoc, "Vui lòng nhập kích thước hợp lệ");
                flag = 1;
            }
            if (flag == 1)
                return true;
            return false;
        }
        SanPham getProduct()
        {

            return new SanPham()
            {
                MaSP = txtMaSP.Text,
                MaNCC = cboNCC.SelectedIndex==-1?"":cboNCC.SelectedItem.ToString(),
                MaDM = cboDM.SelectedIndex==-1?"":cboDM.SelectedItem.ToString(),
                TenSP = txtTenSP.Text,
                DonGia = decimal.Parse(txtDonGia.Text),
                SoLuong = int.Parse(txtSL.Text),
                XuatXu = txtXuatXu.Text,
                TrongLuong = double.Parse(txtTrongLuong.Text),
                KichThuoc = double.Parse(txtKichThuoc.Text),
                DonVi = txtDonVi.Text
            };

        }
        private void InitControls()
        {
            dgvSanPham.ReadOnly = true;
            dgvSanPham.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
        #endregion

    
    }
}
