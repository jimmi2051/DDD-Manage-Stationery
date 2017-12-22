using System;
using System.Windows.Forms;
using MyProject.Infrastructure;
using MyProject.Domain;
using MyProject.Service;
namespace MyProject.UI
{
    public partial class Manager_Supplier : Form
    {
        private ModelStateDictionary ModelState;
        private IManagerService _service;
        public Manager_Supplier()
        {
            if (_service == null)
            {
                _service = DataFactory.getManagerService(ModelState,Information.PersistanceStrategy);
            }
            InitializeComponent();
            InitControls();
            InitData();
        }
      
        public Manager_Supplier(IManagerService service)
            : this()
        {
            _service = service;
        }
        private void Manager_Supplier_Load(object sender, EventArgs e)
        {
            lbHello.Text = "Xin chào, " + Information.Nhanvien.Ten + " \nChức vụ: " + Information.Nhanvien.ChucVu + " ";
            ptrHinhAnh.ImageLocation = ("..\\..\\Images\\QuanLy.jpg");
            View();
            HienChiTiet(false);
        }

        #region View
        private void View()
        {
            dgvNhaCC.DataSource = _service.ListSuppliers(); 
        }
        private void HienChiTiet(Boolean hien)
        {
            txtTen.Enabled = hien;
            txtDiachi.Enabled = hien;
            txtSdt.Enabled = hien;
            //Ẩn hiện 2 nút Lưu và Hủy
            btnLuu.Enabled = hien;
            btnHuy.Enabled = hien;
        }
        private void XoaTrangChiTiet()
        {
            txtTen.Clear();
            txtMaNCC.Clear();
            txtDiachi.Clear();
            txtSdt.Clear();
        }
        private void ViewErrors()
        {
            errProdive.Clear();
            foreach (var entry in ModelState)
            {
                switch (entry.Key)
                {
                    case "MaNCC":
                        errProdive.SetError(txtMaNCC, entry.Value);
                        break;
                    case "sdt":
                        errProdive.SetError(txtSdt, entry.Value);
                        break;
                    case "Timkiem":
                        errProdive.SetError(txtTimkiem, entry.Value);
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
            _service = DataFactory.getManagerService(ModelState, Information.PersistanceStrategy);
        }
        NhaCungCap getSupplier()
        {
            return new NhaCungCap()
            {
                MaNCC = txtMaNCC.Text,
                Ten = txtTen.Text,
                DiaChi = txtDiachi.Text,
                sdt=txtSdt.Text
            };

        }
        private void InitControls()
        {
            dgvNhaCC.ReadOnly = true;
            dgvNhaCC.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
        #endregion
        #region Event-Handler
        private void btnThem_Click(object sender, EventArgs e)
        {
            XoaTrangChiTiet();
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            HienChiTiet(true);
            txtMaNCC.Text = _service.getNewIDSup();
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
            //Thiết lập lại các nút như ban đầu
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
            btnThem.Enabled = true;
            //xoa trang
            XoaTrangChiTiet();
            //Cam nhap
            HienChiTiet(false);
        }
        private void btnDanhsach_Click(object sender, EventArgs e)
        {
        View();
        }
        private void Manager_Supplier_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Information.Nhanvien.ChucVu != "Giám đốc chi nhánh")
                Information.frmLogin.Show();
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            int flag = 0;
            NhaCungCap Target = getSupplier();
            if (btnThem.Enabled == true)
            {
                if (_service.CreateSupplier(Target))
                {
                    MessageBox.Show("Thêm thành công");
                }
                else flag = 1;

            }
            if (btnSua.Enabled == true)
            {
                if (_service.EditSupplier(Target))
                {
                    MessageBox.Show("Sửa thành công");

                }
                else flag = 1;
            }
            if (btnXoa.Enabled == true)
            {
                DialogResult dlr = MessageBox.Show("Bạn có muốn xóa hóa đơn", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlr == DialogResult.Yes)
                {
                    if (_service.DeleteSupplier(Target))
                    {
                        MessageBox.Show("Xóa thành công");
                    }
                    else flag = 1;
                }
            }
            if (flag == 0)
            {
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
        private void dgvNhaCC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Hien thi nut sua
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            HienChiTiet(false);
            //Bắt lỗi khi người sử dụng kích linh tinh lên datagrid
            try
            {
                txtMaNCC.Text = dgvNhaCC[0, e.RowIndex].Value.ToString();
                txtTen.Text = dgvNhaCC[1, e.RowIndex].Value.ToString();
                txtDiachi.Text = dgvNhaCC[2, e.RowIndex].Value.ToString();
                txtSdt.Text = dgvNhaCC[3, e.RowIndex].Value.ToString();
            }
            catch { }          
        }
        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            if (cboLoai.SelectedIndex == -1)
                cboLoai.SelectedIndex = 0;
            if (txtTimkiem.Text.Equals(""))
                View();
            else
                dgvNhaCC.DataSource = _service.SearchSuppliers(txtTimkiem.Text, cboLoai.SelectedItem.ToString());
            ViewErrors();
        }
        private void label1_Click(object sender, EventArgs e)
        {
            Account_UI UI = new Account_UI();
            UI.ShowDialog();
            Manager_Supplier_Load(sender, e);
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Manager_Product UI = new Manager_Product();
            UI.Show();
            this.Visible = false;
        }

        private void label16_Click(object sender, EventArgs e)
        {
            Manager_Employee UI = new Manager_Employee();
            UI.Show();
            this.Visible = false;
        }
        private void label4_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void label18_Click(object sender, EventArgs e)
        {
            Manager_Category UI = new Manager_Category();
            UI.Show();
            this.Visible = false;
        }
        private void label15_Click(object sender, EventArgs e)
        {
            MenuStatistical UI = new MenuStatistical();
            UI.ShowDialog();
            this.Visible = false;
        }

        #endregion

        private void label19_Click(object sender, EventArgs e)
        {
            CodeSales UI = new CodeSales();
            UI.ShowDialog();
        }
    }
}
