using System;
using System.Windows.Forms;
using MyProject.Infrastructure;
using MyProject.Domain;
using MyProject.Service;
using System.Text.RegularExpressions;

namespace MyProject.UI
{
    public partial class WareHouse : Form
    {
        private ModelStateDictionary ModelState;
        IWareHouseService _service;
        public WareHouse()
        {
            InitializeComponent();
            if (_service == null)
                _service = DataFactory.getWareHouseService(ModelState,Information.PersistanceStrategy);
            InitData();
        }
        public WareHouse(IWareHouseService service)
            : this()
        {
            _service = service;
        }
        #region View
        private void View()
        {
            try
            {
                dgvKho.DataSource = _service.ListWareHouse();
            }
            catch 
            {
                MessageBox.Show("Lỗi: Hệ thống đang cập nhập ", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        
        }
        #endregion
        #region Event-handler
        
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtMaPhieu.Text.Equals(""))
                MessageBox.Show("Vui lòng chọn sản phẩm", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            grbSua.Visible = true;
        }
        private void btnHuy_Click(object sender, EventArgs e)
        {
            XoaTrangThongTin();
            grbSua.Visible = false;
        }
        private void dgvKho_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            XoaTrangThongTin();
            try
            {
                txtMaSP.Text = dgvKho[0, e.RowIndex].Value.ToString();
                txtMaPhieu.Text = dgvKho[1, e.RowIndex].Value.ToString();
                txtSoLuong.Text = dgvKho[2, e.RowIndex].Value.ToString();
                txtNgayNhap.Text = dgvKho[3, e.RowIndex].Value.ToString();
                txtNgayXuat.Text = dgvKho[4, e.RowIndex].Value.ToString();
            }
            catch { }
        }
        private void btnChitiet_Click(object sender, EventArgs e)
        {
            View();
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (CheckQuality())
            {
                Kho editWareHouse = getWareHouse();
                if (_service.EditWareHouse(editWareHouse))
                {
                    MessageBox.Show("Sửa thành công");
                    View();
                    XoaTrangThongTin();
                    grbSua.Visible = false;
                }
                else
                    MessageBox.Show("Lỗi hệ thống ", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnTimKiem_Click(object sender, EventArgs e)
        {         
            if (txtTimkiem.Text == "")
                View();
            else
                dgvKho.DataSource = _service.SearchWareHouse(txtTimkiem.Text, cboLoai.SelectedItem.ToString());
        }
        private void btnSapXep_Click(object sender, EventArgs e)
        {            
            dgvKho.DataSource = _service.Sort(cboLoaiSX.SelectedIndex.ToString());
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMaPhieu.Equals(""))
                MessageBox.Show("Vui lòng chọn dữ liệu");
            else
            {
                DialogResult dlr = MessageBox.Show("Bạn có muốn xóa hóa đơn", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlr == DialogResult.Yes)
                {
                    Kho warehouseToDelete = getWareHouse();
                    if (_service.DeleteWareHouse(warehouseToDelete))
                        MessageBox.Show("Thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                View();
            }
        }
        private void cboLoai_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboLoai.AutoCompleteMode = AutoCompleteMode.Suggest;
            cboLoai.AutoCompleteSource = AutoCompleteSource.ListItems;
        }
        private void cboLoaiSX_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboLoaiSX.AutoCompleteMode = AutoCompleteMode.Suggest;
            cboLoaiSX.AutoCompleteSource = AutoCompleteSource.ListItems;
        }
        //Components Điều hướng
        private void WareHouse_Load(object sender, EventArgs e)
        {
            lbHello.Text = "Xin chào, " + Information.Nhanvien.Ten + " \nChức vụ: " + Information.Nhanvien.ChucVu + " ";
            ptrHinhAnh.ImageLocation = ("..\\..\\Images\\QuanLy.jpg");
            View();
            cboLoai.SelectedIndex = 0;
            cboLoaiSX.SelectedIndex = 0;
        }
        private void WareHouse_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Information.Nhanvien.ChucVu != "Giám đốc chi nhánh")
                Information.frmLogin.Show();
        }
        private void label4_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void label1_Click(object sender, EventArgs e)
        {
            Account_UI UI = new Account_UI();
            UI.ShowDialog();
            WareHouse_Load(sender, e);
        }
        private void label15_Click(object sender, EventArgs e)
        {
            Coupon UI = new Coupon();
            UI.Show();
            Visible = false;
        }
        #endregion
        #region Help
        private void InitData()
        {
            ModelState = new ModelStateDictionary();
            _service = DataFactory.getWareHouseService(ModelState, Information.PersistanceStrategy);
        }
        void XoaTrangThongTin()
        {
            txtMaSP.Clear();
            txtMaPhieu.Clear();
            txtSoLuong.Clear();
            txtNgayNhap.Clear();
            txtNgayXuat.Clear();
        }
       
        Kho getWareHouse()
        {
            return new Kho()
            {
                 MaSP = txtMaSP.Text,
                  MaPhieu = txtMaPhieu.Text,
                 SoLuong = int.Parse(txtSoLuong.Text),
                 NgayLap = txtNgayNhap.Text==""?DateTime.Now:DateTime.Parse(txtNgayNhap.Text),
                 NgayXuat = txtNgayXuat.Text==""? DateTime.Now:DateTime.Parse(txtNgayXuat.Text)
            };
        }
        private bool CheckQuality()
        {
            int flag = 0;
            if (txtSoLuong.Text.Length > 0 && !Regex.IsMatch(txtSoLuong.Text, @"\d"))
            {
                errProdive.SetError(txtSoLuong, "Vui lòng nhập hợp lệ");
                flag = 1;
            }
            if (txtSoLuong.Text.Length == 0)
            {
                errProdive.SetError(txtSoLuong, "Vui lòng nhập");
                flag = 1;
            }
            if (flag == 1)
                return false;
            return true;
        }
        #endregion

        private void txtSoLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            errProdive.Clear();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            Statistical_WareHouse UI = new Statistical_WareHouse();
            UI.Show();
            this.Visible = false;
        }
    }
}
