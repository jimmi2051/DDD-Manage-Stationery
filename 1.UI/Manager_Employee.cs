using System;
using System.Windows.Forms;
using MyProject.Domain;
using MyProject.Service;
using System.Text.RegularExpressions;
using MyProject.Infrastructure;
namespace MyProject.UI
{
    public partial class Manager_Employee : Form
    {
        private ModelStateDictionary ModelState;
        public static IManagerService _service;
        public Manager_Employee()
        {

            if (_service == null)
            {
                _service = DataFactory.getManagerService(ModelState,Information.PersistanceStrategy);
            }
            InitializeComponent();
            InitControls();
            InitData();
        }
         public Manager_Employee(IManagerService service)
            : this()
        {
            _service = service;
        }
        
        #region View
        private void View()
        {
            dgvNhanVien.DataSource = _service.ListEmployees();
        }
        private void HienChiTiet(Boolean hien)
        {
            txtTen.Enabled = hien;
            txtSdt.Enabled = hien;
            cboChucVu.Enabled = hien;
            txtLuong.Enabled = hien;
            txtDiachi.Enabled = hien;
            //Ẩn hiện 2 nút Lưu và Hủy
            btnLuu.Enabled = hien;
            btnHuy.Enabled = hien;
        }
        private void XoaTrangChiTiet()
        {
            txtTen.Clear();
            txtMaNV.Clear();
            txtSdt.Clear();
            cboChucVu.SelectedIndex = -1;
            txtLuong.Clear();
            txtDiachi.Clear();
        }
        private void ViewErrors()
        {
            errProdive.Clear();
            foreach (var entry in ModelState)
            {
                switch (entry.Key)
                {
                    case "MaNV":
                        errProdive.SetError(txtMaNV, entry.Value);
                        break;
                    case "Luong":
                        errProdive.SetError(txtLuong, entry.Value);
                        break;
                    case "Sdt":
                        errProdive.SetError(txtSdt, entry.Value);
                        break;
                    case "Ten":
                        errProdive.SetError(txtTen, entry.Value);
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
        #region Event-handler    
       private void btnDanhsach_Click(object sender, EventArgs e)
       {
           View();
       } 
        private void Manager_Employee_Load(object sender, EventArgs e)
        {
            lbHello.Text = "Xin chào, " + Information.Nhanvien.Ten + " \nChức vụ: " + Information.Nhanvien.ChucVu + " ";
            ptrHinhAnh.ImageLocation = ("..\\..\\Images\\QuanLy.jpg");
            View();
            HienChiTiet(false);
            cboLoai.SelectedIndex = 0;
            
        }

        private void button15_Click(object sender, EventArgs e)
        {
            View();
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            XoaTrangChiTiet();
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            HienChiTiet(true);
            txtMaNV.Text = _service.getNewIDEmployee();
            InitCombobox("Abc");
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

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtMaNV.Text == Information.Nhanvien.MaNV)
            {
                MessageBox.Show("Lỗi: Không được tự sửa thông tin cá nhân ở đây. Vui lòng sử dụng chức năng tài khoản ", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            errProdive.Clear();
            int flag = 0;
            NhanVien Target = null;
            if (CheckText())
                return;
            Target = getEmployee();
            if (btnThem.Enabled == true)
            {
                if (_service.CreateEmployee(Target))
                {
                    MessageBox.Show("Thêm thành công");
                    InitCombobox();
                }
                else flag = 1;
            }
            if (btnSua.Enabled == true)
            {
                if (_service.EditEmployee(Target))
                    MessageBox.Show("Sửa thành công");
                else
                    flag = 1;
            }
            if (btnXoa.Enabled == true)
            {
                DialogResult dlr = MessageBox.Show("Bạn có muốn xóa hóa đơn", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlr == DialogResult.Yes)
                {
                    if (_service.DeleteEmployee(Target))
                        MessageBox.Show("Xóa thành công");
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

        private void btnHuy_Click(object sender, EventArgs e)
        {
            //Thiết lập lại các nút như ban đầu
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
            btnThem.Enabled = true;
            InitCombobox();
            //xoa trang
            XoaTrangChiTiet();
            //Cam nhap
            HienChiTiet(false);
        }

        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            if (txtTimkiem.Text.Equals(""))
                View();
            else
                dgvNhanVien.DataSource = _service.SearchEmployees(txtTimkiem.Text, cboLoai.SelectedItem.ToString());

        }
        private void btnLammoi_Click(object sender, EventArgs e)
        {
            txtTimkiem.Text = "";
            cboLoai.SelectedIndex = 0;
        }
        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            InitCombobox();
            HienChiTiet(false);
            //Bắt lỗi khi người sử dụng kích linh tinh lên datagrid
            try
            {
                txtMaNV.Text = dgvNhanVien[0, e.RowIndex].Value.ToString();
                txtTen.Text = dgvNhanVien[1, e.RowIndex].Value.ToString();
                txtDiachi.Text = dgvNhanVien[2, e.RowIndex].Value.ToString();
                txtSdt.Text = dgvNhanVien[3, e.RowIndex].Value.ToString();
                if (dgvNhanVien[4, e.RowIndex].Value.ToString() == "Nhân viên bán hàng")
                    cboChucVu.SelectedIndex = 0;
                if (dgvNhanVien[4, e.RowIndex].Value.ToString() == "Quản lý")
                    cboChucVu.SelectedIndex = 1;
                if (dgvNhanVien[4, e.RowIndex].Value.ToString() == "Quản lý kho")
                    cboChucVu.SelectedIndex = 2;
                if (dgvNhanVien[4, e.RowIndex].Value.ToString() == "Giám đốc chi nhánh")
                    cboChucVu.SelectedIndex = 3;
                txtLuong.Text = dgvNhanVien[5, e.RowIndex].Value.ToString();
            }
            catch { }
        }

        //Components Điều hướng
        private void Manager_Employee_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Information.Nhanvien.ChucVu != "Giám đốc chi nhánh")
                Information.frmLogin.Show();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Account_UI UI = new Account_UI();
            UI.ShowDialog();
            Manager_Employee_Load(sender, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Manager_Product UI = new Manager_Product();
            UI.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Manager_Supplier UI = new Manager_Supplier(_service);
            UI.Show();
            this.Hide();
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
        private void btnUser_Click_1(object sender, EventArgs e)
        {
            NhanVien customerTarget = _service.GetEmployee(txtMaNV.Text);
            Account_UI UI = new Account_UI(customerTarget);
            UI.ShowDialog();
        }
        private void btnCheck_Click(object sender, EventArgs e)
        {
            NhanVien customerTarget = getEmployee();
            if (_service.CheckEmployee(customerTarget))
            {
                NhanVien newtarget = _service.GetEmployee(customerTarget.MaNV);
                if (customerTarget.Luong < newtarget.Luong)
                    MessageBox.Show("Đạt tiêu chuẩn, lương tăng " + (newtarget.Luong - customerTarget.Luong) + " VND ");
                else
                    MessageBox.Show("Không đạt tiêu chuẩn ");
                View();
            }
            else
                MessageBox.Show("Lỗi: Hệ thống đang bận ", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        #endregion
        #region Help
        private void InitCombobox()
        {
            cboChucVu.Items.Clear();
            cboChucVu.Items.Add("Nhân viên bán hàng");
            cboChucVu.Items.Add("Quản lý");
            cboChucVu.Items.Add("Quản lý kho");
            cboChucVu.Items.Add("Giám đốc chi nhánh");
            cboChucVu.SelectedIndex = 0;
        }
        private void InitCombobox(string Add)
        {
            cboChucVu.Items.Clear();
            cboChucVu.Items.Add("Nhân viên bán hàng");
            cboChucVu.SelectedIndex = 0;
            cboChucVu.Enabled = false;
        }
        private void InitData()
        {
            ModelState = new ModelStateDictionary();
            _service = DataFactory.getManagerService(ModelState, Information.PersistanceStrategy);
        }
           
        bool CheckText()
        {
            int flag = 0;
            if (txtLuong.Text.Length > 0 && !Regex.IsMatch(txtLuong.Text, @"\d"))
            {
                errProdive.SetError(txtLuong, "Vui lòng nhập dữ liệu hợp lệ");
                flag = 1;
            }
            if (flag == 1)
                return true;
            return false;
        }
        NhanVien getEmployee()
        {
            return new NhanVien()
            {
                MaNV = txtMaNV.Text,
                Ten = txtTen.Text,
                Diachi = txtDiachi.Text,
                sdt = txtSdt.Text,
                ChucVu = cboChucVu.SelectedItem.ToString(),
                Luong = txtLuong.Text=="" ? 0: int.Parse(txtLuong.Text),
                 
            };
        }
        private void InitControls()
        {
            dgvNhanVien.ReadOnly = true;
            dgvNhanVien.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }





        #endregion

    
    }
}
