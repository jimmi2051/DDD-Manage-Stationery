using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyProject.Service;
using MyProject.Infrastructure;
using MyProject.Domain;
namespace MyProject.UI
{
    public partial class Manager_Customer : Form
    {
        private ModelStateDictionary ModelState;
        private ISellerService _service;    
        public Manager_Customer()
        {
            if (_service == null)
            {
                _service = DataFactory.getSellerService(ModelState ,Information.PersistanceStrategy);
            }
            InitializeComponent();
            InitData();
        }
        public Manager_Customer(ISellerService service):this()
        {
            _service = service;
        }

        #region Help
        private void InitData()
        {
            ModelState = new ModelStateDictionary();
            _service = DataFactory.getSellerService(ModelState, Information.PersistanceStrategy);
        }
        //Xóa trắng thông tin 
        void clearTextBox()
        {
            txtMaKH.Clear();
            txtTen.Clear();
            txtSdt.Clear();
            txtDiachi.Clear();
        }
        KhachHang getCustomer() {
            return new KhachHang()
            {
                MaKH = txtMaKH.Text,
                Ten = txtTen.Text,
                Sdt=txtSdt.Text,
                DiaChi=txtDiachi.Text
            };
        }
        #endregion
        #region View
        private void View()
        {
            dgvKhachHang.DataSource = _service.ListCustomers();
        }
        private void HienChiTiet(Boolean hien)
        {
            txtTen.Enabled = hien;
            txtSdt.Enabled = hien;         
            txtDiachi.Enabled = hien;
            //Ẩn hiện 2 nút Lưu và Hủy
            btnLuu.Enabled = hien;
            btnHuy.Enabled = hien;
        }
        private void ViewErrors()
        {
            errProdive.Clear();
            foreach (var entry in ModelState)
            {
                switch (entry.Key)
                {

                    case "MaKH":
                        errProdive.SetError(txtMaKH, entry.Value);
                        break;
                    case "TenKH":
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
        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            clearTextBox();
            btnSua.Enabled = true ;
            btnXoa.Enabled = true ;
            try
            {
                txtMaKH.Text = dgvKhachHang[0, e.RowIndex].Value.ToString();
                txtTen.Text = dgvKhachHang[1, e.RowIndex].Value.ToString();
                txtSdt.Text = dgvKhachHang[3, e.RowIndex].Value.ToString();
                txtDiachi.Text = dgvKhachHang[2, e.RowIndex].Value.ToString();
            }
            catch { }
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            clearTextBox();
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            HienChiTiet(true);
            txtMaKH.Text = _service.getNewIDCustomer();
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
            clearTextBox();
            //Cam nhap
            HienChiTiet(false);
        }

        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            if (cboLoai.SelectedIndex == -1)
                cboLoai.SelectedIndex = 0;
            if (txtTimkiem.Text.Equals(""))
                View();
            else
                dgvKhachHang.DataSource = _service.searchCustomer(txtTimkiem.Text, cboLoai.SelectedItem.ToString());
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            int flag = 0;
            errProdive.Clear();
            KhachHang Target = getCustomer();
            if (btnThem.Enabled == true )
            {
                if (_service.CreateCustomer(Target))
                    MessageBox.Show("Thêm thành công");
                else flag = 1;
            }
            if (btnSua.Enabled == true)
            {
                if (_service.EditCustomer(Target))
                    MessageBox.Show("Sửa thành công");
                else flag = 1;
            }
            if (btnXoa.Enabled == true )
            {
                DialogResult dlr = MessageBox.Show("Bạn có muốn xóa khách hàng ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlr == DialogResult.Yes)
                {
                    if (_service.DeleteCustomer(Target))
                        MessageBox.Show("Xóa thành công");
                    else
                        flag = 1;
                }
            }
            if (flag == 0)
            {              
                View();
                HienChiTiet(false);
                btnThem.Enabled = true;
                btnXoa.Enabled = true;
                btnSua.Enabled = true;
                clearTextBox();
            }
            else
                ViewErrors();
        }
        //Component Điều hướng
        private void Manager_Customer_Load(object sender, EventArgs e)
        {
            lbHello.Text = "Xin chào, " + Information.Nhanvien.Ten + " \nChức vụ: " + Information.Nhanvien.ChucVu + " ";
            ptrHinhAnh.ImageLocation = ("..\\..\\Images\\QuanLy.jpg");
            View();
            HienChiTiet(false);        
        }
        private void Manager_Customer_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Information.Nhanvien.ChucVu != "Giám đốc chi nhánh")
                Information.frmLogin.Show();
        }
        private void label4_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void label2_Click(object sender, EventArgs e)
        {
            Seller_UI UI = new Seller_UI();
            UI.Show();
            this.Visible = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Account_UI UI = new Account_UI();
            UI.ShowDialog();
            Manager_Customer_Load(sender, e);
        }



        #endregion

    }
}
