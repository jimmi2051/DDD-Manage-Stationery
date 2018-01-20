using System;
using System.Windows.Forms;
using MyProject.Service;
using MyProject.Domain;
using MyProject.Infrastructure;
namespace MyProject.UI
{
    public partial class Manager_Category: Form
    {
        private ModelStateDictionary ModelState;
        private IManagerService _service;
        public Manager_Category()
        {
            if (_service == null)
            {
                _service = DataFactory.getManagerService(ModelState, Information.PersistanceStrategy);
            }
            InitializeComponent();
            InitControls();
            InitData();
        }
        public Manager_Category(IManagerService service) : this()
        {
            _service = service;
        }
        #region Event-Handler 
        private void cboLoai_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboLoai.AutoCompleteMode = AutoCompleteMode.Suggest;
            cboLoai.AutoCompleteSource = AutoCompleteSource.ListItems;
        }
        private void dgvDanhMucSP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Hien thi nut sua
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            HienChiTiet(false);
            //Bắt lỗi khi người sử dụng kích linh tinh lên datagrid
            try
            {
                txtMaDM.Text = dgvDanhMucSP[0, e.RowIndex].Value.ToString();
                txtTenDM.Text = dgvDanhMucSP[1, e.RowIndex].Value.ToString();
                txtSoLuong.Text = dgvDanhMucSP[2, e.RowIndex].Value.ToString();
            }
            catch { }
        }
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (txtTimkiem.Text.Equals(""))
                View();
            else
                dgvDanhMucSP.DataSource = _service.SearchCategorys(txtTimkiem.Text, cboLoai.SelectedItem.ToString());
            ViewErrors();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            XoaTrangChiTiet();
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            HienChiTiet(true);
            txtMaDM.Text = _service.getNewIDCat();
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

            int flag = 0;
            //Kiểm tra text box 
            if (!checkSoLuong())
                return;
            DanhMucSP Target = getProductCategory();
            if (btnThem.Enabled == true)
            {
                if (_service.CreateCategory(Target))
                {
                    MessageBox.Show("Thêm thành công");
                }
                else flag = 1;
            }
            if (btnSua.Enabled == true)
            {
                if (_service.EditCategory(Target))
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
                    if (_service.DeleteCategory(Target))
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
        private void btnLammoi_Click(object sender, EventArgs e)
        {
            txtTimkiem.Text = "";
        }

        //Components Điều hướng
        private void Manager_ProductCategory_Load(object sender, EventArgs e)
        {
            lbHello.Text = "Xin chào, " + Information.Nhanvien.Ten + " \nChức vụ: " + Information.Nhanvien.ChucVu + " ";
            ptrHinhAnh.ImageLocation = ("..\\..\\Images\\QuanLy.jpg");
            View();
            HienChiTiet(false);
            cboLoai.SelectedIndex = 0;
        }
        private void Manager_ProductCategory_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Information.Nhanvien.ChucVu != "Giám đốc chi nhánh")
                Information.frmLogin.Show();
        }
        private void button9_Click(object sender, EventArgs e)
        {
            Account_UI UI = new Account_UI();
            UI.ShowDialog();
            Manager_ProductCategory_Load(sender, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Manager_Product UI = new Manager_Product();
            UI.Show();
            Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Manager_Employee UI = new Manager_Employee();
            UI.Show();
            Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Manager_Supplier UI = new Manager_Supplier(_service);
            UI.Show();
            Hide();
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
            Close();
        }
        #endregion
        #region View
        public void View()
        {
            dgvDanhMucSP.DataSource = _service.ListCategorys();

        }
        private void HienChiTiet(Boolean hien)
        {
            txtTenDM.Enabled = hien;
            txtSoLuong.Enabled = hien;
            //Ẩn hiện 2 nút Lưu và Hủy
            btnLuu.Enabled = hien;
            btnHuy.Enabled = hien;
        }
        private void XoaTrangChiTiet()
        {
            txtMaDM.Clear();
            txtTenDM.Clear();
            txtSoLuong.Clear();
        }
        private void ViewErrors()
        {
            errProdive.Clear();
            foreach (var entry in ModelState)
            {
                switch (entry.Key)
                {
                    case "MaDM":
                        errProdive.SetError(txtMaDM, entry.Value);
                        break;
                    case "Ten":
                        errProdive.SetError(txtTenDM, entry.Value);
                        break;
                    case "SoLuong":
                        errProdive.SetError(txtSoLuong, entry.Value);
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
        #region helper
        private void InitData()
        {
            ModelState = new ModelStateDictionary();
            _service = DataFactory.getManagerService(ModelState, Information.PersistanceStrategy);
        }
        DanhMucSP getProductCategory()
        {

            return new DanhMucSP()
            {
                MaDM = txtMaDM.Text,
                TenDM = txtTenDM.Text,
                SoLuong = txtSoLuong.Text == "" ? 0 : int.Parse(txtSoLuong.Text),

            };

        }
        private void InitControls()
        {
            dgvDanhMucSP.ReadOnly = true;
            dgvDanhMucSP.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
        private Boolean checkSoLuong()
        {
            if (txtSoLuong.Text.Length > 0 && !System.Text.RegularExpressions.Regex.IsMatch(txtSoLuong.Text, @"\d")||txtSoLuong.Text.Length == 0 )
            {
                errProdive.SetError(txtSoLuong, "Vui lòng nhập số lượng hợp lệ ..");
                return false;
            }
            return true;
        }



        #endregion
  //New

      
    }
}

