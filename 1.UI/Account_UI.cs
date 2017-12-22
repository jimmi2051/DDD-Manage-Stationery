using System;
using System.Windows.Forms;
using MyProject.Domain;
using MyProject.Infrastructure;
using MyProject.Service;
using System.Collections;
namespace MyProject.UI
{
    public partial class Account_UI : Form
    {
        private ModelStateDictionary ModelState;
        private IManagerService _service;
        private NhanVien EmployeeToBackup;
        NguoiDung target = null;
        int style = 0;
        public Account_UI()
        {
            InitializeComponent();
            if (_service == null)
                _service = DataFactory.getManagerService(ModelState,Information.PersistanceStrategy);
            InitData();
        }
        public Account_UI(NhanVien employee):this()
        {
            EmployeeToBackup = employee;          
            style = 1;
        }
        private void InitData()
        {
            ModelState = new ModelStateDictionary();
            _service = _service = DataFactory.getManagerService(ModelState, Information.PersistanceStrategy);
        }
        private void ViewErrors()
        {
            errProdive.Clear();
            foreach (var entry in ModelState)
            {
                switch (entry.Key)
                {
                    case "Ten":
                        errProdive.SetError(txtHoTen, entry.Value);
                        break;
                    case "Sdt":
                        errProdive.SetError(txtSdt, entry.Value);
                        break;
                    case "ID":
                        errProdive.SetError(txtTenTK, entry.Value);
                        break;
                    case "Email":
                        errProdive.SetError(txtHoTen, entry.Value);
                        break;
                    case "Password":
                        errProdive.SetError(txtMatKhau, entry.Value);
                        break;
                    case "MaNV":
                        errProdive.SetError(txtSdt, entry.Value);
                        break;
                    default:
                        break;
                }
            }            
        }
        private void Account_UI_Load(object sender, EventArgs e)
        {
            if (style == 0)
            {
                txtHoTen.Text = Information.Nhanvien.Ten;
                txtDiaChi.Text = Information.Nhanvien.Diachi;
                txtEmail.Text = Information.Nguoidung.Mail;
                txtTenTK.Text = Information.Nguoidung.ID;
                txtSdt.Text = Information.Nhanvien.sdt;
                txtMatKhau.Text = "********";
                EmployeeToBackup = getEmployee();
            }
            else
            {
                lblNull2.Visible = false;
                txtEmail.Visible = false;
                if (EmployeeToBackup != null)
                {
                    target = _service.CheckUser(EmployeeToBackup.MaNV);
                    if (target != null)
                    {
                        txtTenTK.Text = target.ID;
                        txtMatKhau.Text = target.Pass;
                        txtHoTen.Text = target.Pass;
                        txtDiaChi.Text = target.Mail;
                    }
                    else
                    {
                        txtTenTK.Enabled = true;
                    }
                    txtSdt.Text = EmployeeToBackup.MaNV;
                }
                else
                   txtTenTK.Enabled = true;
                lblHoTen.Text = "Nhập lại mật khẩu: ";
                lblDiaChi.Text = "Email:  ";
                lblNull.Text = "Mã nhân viên: ";
                txtHoTen.PasswordChar = '*';                             
                txtMatKhau.Enabled = true;
            }
        }
        private void ClearTextBox()
        {
            txtTenTK.Clear();
            txtMatKhau.Clear();
            txtHoTen.Clear();
            txtDiaChi.Clear();
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        NhanVien getEmployee()
        {
            return new NhanVien()
            {
                MaNV = Information.Nhanvien.MaNV,
                Ten = txtHoTen.Text,
                Diachi = txtDiaChi.Text,
                ChucVu = Information.Nhanvien.ChucVu,
                sdt= txtSdt.Text,
                Luong =Information.Nhanvien.Luong
            };
        }
        NguoiDung getUser()
        {
            return new NguoiDung()
            {
                ID=txtTenTK.Text,
                 Pass = txtMatKhau.Text,
                 Mail=txtDiaChi.Text,
                MaNV = txtSdt.Text
            };
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (style == 1)
            {
                if (txtMatKhau.Text.Equals(txtHoTen.Text))
                {
                    NguoiDung target = getUser();
                    if (_service.getUser(target.ID) == null)
                    {
                        _service.CreateUser(target);
                        MessageBox.Show("123");
                    }
                    else
                        _service.EditUser(target);
                    ViewErrors();
                }
                else
                {
                    errProdive.SetError(txtMatKhau, "Mật khẩu không giống nhau");
                    errProdive.SetError(txtHoTen, "Mật khẩu không giống nhau");
                }
            }
            else
            {
                NhanVien toedit = getEmployee();
                if (_service.EditEmployee(toedit))
                {
                    MessageBox.Show("Thành công");
                    Information.Nhanvien = toedit;
                    this.Dispose();
                }
                ViewErrors();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (style == 0)
            {
                txtHoTen.Text = EmployeeToBackup.Ten;
                txtDiaChi.Text = EmployeeToBackup.Diachi;
                txtSdt.Text = EmployeeToBackup.sdt;
            }
            else
            {
                try
                {
                    txtTenTK.Text = target.ID;
                    txtMatKhau.Text = target.Pass;
                    txtHoTen.Text = target.Pass;
                    txtDiaChi.Text = target.Mail;
                    txtSdt.Text = EmployeeToBackup.MaNV;
                }
                catch
                {
                    ClearTextBox();
                }
            }
        }
    }
}
