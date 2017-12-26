using System;
using System.Windows.Forms;
using MyProject.Infrastructure;
using MyProject.Service;
using MyProject.Domain;
using System.Collections;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Linq;
using System.ComponentModel;
using System.Data;
using System.Collections.Generic;

namespace MyProject.UI
{
    public partial class Seller_UI : Form
    {
        private ModelStateDictionary ModelState;
        private ISellerService _service;
        HoaDon billtoBackup;
        MaKhuyenMai CodeSales = null;
        HoaDon BilltoChecked;
        Timer timer = new Timer();
        public Seller_UI()
        {
            if (_service == null)
            {
                _service = DataFactory.getSellerService(ModelState, Information.PersistanceStrategy);
            }
            InitializeComponent();
            InitData();
        }
        public Seller_UI(ISellerService service)
            : this()
        {
            _service = service;
        }
        #region View
        //Hiển thị lỗi
        private void ViewErrors()
        {
            errProdive.Clear();
            foreach (var entry in ModelState)
            {
                switch (entry.Key)
                {
                    case "MaKH":
                        if (txtMaKH.Text != "") errProdive.SetError(txtMaKH, entry.Value);
                        else errProdive.SetError(txtIDCustomertoEdit, entry.Value);
                        break;
                    case "SoLuong":
                        errProdive.SetError(txtQualitytoAdd, entry.Value);
                        break;
                    case "Timkiem":
                        errProdive.SetError(txtKeyofBilltoSearch, entry.Value);
                        break;
                    default:
                        break;
                }
            }
        }
        //Xóa trắng bảng sản phẩm đã chọn       
        private void ClearDGV(DataGridView dgv)
        {
            for (int i = 0; i < dgv.Rows.Count - 1; i++)
            {
                dgv.Rows.RemoveAt(i);
                i--;
                while (dgv.Rows.Count == 0)
                    continue;
            }
            try { dgv.Rows.RemoveAt(0); }
            catch { }
        }
        //Hiển thị mặc định hóa đơn 
        public void View()
        {
            try
            {
                dgvHoadon.DataSource = _service.ListBills();
            }
            catch
            {
                MessageBox.Show("Lỗi: Hệ thống đang cập nhập ", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //hiển thị sản phẩm
        private void viewSanPham()
        {
            ClearDGV(dgvProduct);
            IEnumerable enumerable = _service.ListProducts();
            foreach (SanPham item in enumerable)
            {
                dgvProduct.Rows.Add(
                       item.MaSP,
                       item.TenSP,
                       item.DanhMucSP.TenDM,
                       LamTron(item.DonGia+item.DonGia*30/100),
                       item.DonVi
                       );
            }   
        }
        private void viewSanPham(String key, String type)
        {
            ClearDGV(dgvProduct);
            decimal pricestart = 0;
            decimal priceend = decimal.MaxValue;
            if (txtPriceStart.Text.Length != 0)
                pricestart = decimal.Parse(txtPriceStart.Text);
            if (txtPriceEnd.Text.Length != 0)
                priceend = decimal.Parse(txtPriceEnd.Text);
            IEnumerable enumerable = _service.SearchProducts(key, type,pricestart,priceend);
            foreach (SanPham item in enumerable)
            {
                dgvProduct.Rows.Add(
                       item.MaSP,
                       item.TenSP,
                       item.DanhMucSP.TenDM,
                        LamTron(item.DonGia + item.DonGia * 30 / 100),
                       item.DonVi
                       );
            }
        }
        //Hiển thị dữ liệu đã có 
        private void ViewDetailAvailable(DataGridView dgvTarget)
        {
            IEnumerable list = _service.listBillDetail(txtIDBilltoDetail.Text);
            foreach (ChiTietHoaDon item in list)
            {
                dgvTarget.Rows.Add(
                                      item.MaSP,
                                      item.SanPham.TenSP,
                                      item.SoLuong,
                                      item.DonGia);
            }
        }
        //Xem chi tiết sản phẩm
        private void ViewDetail()
        {
            if (txtIDBilltoEdit.Text.Equals(""))
                MessageBox.Show("Vui lòng chọn hóa đơn");
            else
            {
                //Làm trắng bảng chi tiết đã chọn
                ClearDGV();
                HoaDon target = _service.getBill(txtIDBilltoEdit.Text);
                txtIDBilltoDetail.Text = txtIDBilltoEdit.Text;
                txtTotaltoFake.Text = String.Format("{0:#,0 vnđ}", target.TongTien);
                tabControl.SelectedTab = tabChitiet;
                viewSanPham();
                if (target.TongTien > 0)
                    ViewDetailAvailable(dgvDachon);
                cboTypeofProduct.SelectedIndex = 0;
            }
        }
        //Xóa trắng bảng sản phẩm đã chọn       
        private void ClearDGV()
        {
            for (int i = 0; i < dgvDachon.Rows.Count - 1; i++)
            {
                dgvDachon.Rows.RemoveAt(i);
                i--;
                while (dgvDachon.Rows.Count == 0)
                    continue;
            }
            try
            {
                txtTotaltoFake.Text = String.Format("{0:#,0 vnđ}", 0);
                dgvDachon.Rows.RemoveAt(0);
            }
            catch { }
        }
        #endregion
        #region Help
        private decimal? LamTron(decimal? Target)
        {
            String strTarget = Target.ToString();
            decimal k =decimal.Parse(strTarget.Substring(strTarget.Length-3));           
            return (int)Target-k;
        }
        private void InitData()
        {
            ModelState = new ModelStateDictionary();
            _service = DataFactory.getSellerService(ModelState, Information.PersistanceStrategy);
        }
        //Xóa trắng thông tin 
        void ClearTextBox()
        {
            txtIDBilltoEdit.Clear();
            txtIDEmployeetoEdit.Clear();
            txtIDCustomertoEdit.Clear();
            txtDatetoEdit.Clear();
            txtTotaltoEdit.Clear();
        }
        //Tính tổng tiền trên bảng đã chọn
        private decimal calculatedBill()
        {
            {
                decimal tamtinh = 0;
                for (int row = 0; row < dgvDachon.Rows.Count; row++)
                {
                    tamtinh += decimal.Parse(dgvDachon.Rows[row].Cells[3].Value.ToString()) * decimal.Parse(dgvDachon.Rows[row].Cells[2].Value.ToString());
                }
                return tamtinh;
            }
        }
        //Kiểm tra số lượng thêm
        private bool CheckQuality()
        {
            int flag = 0;
            if (txtQualitytoAdd.Text.Equals(""))
            {
                errProdive.SetError(txtQualitytoAdd, "Vui lòng nhập số lượng");
                flag = 1;
            }
            if (txtQualitytoAdd.Text.Length > 0 && !Regex.IsMatch(txtQualitytoAdd.Text, @"\d"))
            {
                errProdive.SetError(txtQualitytoAdd, "Vui lòng nhập số lượng hợp lệ");
                flag = 1;
            }
            if (txtQualitytoAdd.Text.Length > 0 && int.Parse(txtQualitytoAdd.Text) < 0)
            {
                errProdive.SetError(txtQualitytoAdd, "Vui lòng nhập số lượng hợp lệ");
                flag = 1;
            }
            int selectedrow = dgvProduct.CurrentCell.RowIndex;
            String MaSP = dgvProduct.Rows[selectedrow].Cells[0].Value.ToString();
            if (!_service.checkAmount(MaSP, int.Parse(txtQualitytoAdd.Text)))
            {
                errProdive.SetError(txtQualitytoAdd, "Hết hàng");
                flag = 1;
            }
            if (flag == 1)
                return true;
            return false;
        }
        //Hiển thị dữ liệu chi tiết hóa đơn giả lên bảng dgvDachon
        private void UpdateDGV()
        {
            int check = 0;
            int soluong;
            int selectedrow = dgvProduct.CurrentCell.RowIndex;
            String MaSP = dgvProduct.Rows[selectedrow].Cells[0].Value.ToString();
            string tensp = dgvProduct.Rows[selectedrow].Cells[1].Value.ToString();
            decimal dongia = Convert.ToDecimal(dgvProduct.Rows[selectedrow].Cells[3].Value.ToString());
            for (int row = 0; row < dgvDachon.Rows.Count; row++)
            {
                if (MaSP.Equals(dgvDachon.Rows[row].Cells[0].Value.ToString()))
                {
                    soluong = Convert.ToInt32(txtQualitytoAdd.Text) + Convert.ToInt32(dgvDachon.Rows[row].Cells[2].Value);
                    if (!_service.checkAmount(MaSP, soluong))
                    {
                        errProdive.SetError(txtQualitytoAdd, "Hết hàng");
                        return;
                    }
                    dgvDachon.Rows[row].Cells[2].Value = soluong.ToString();
                    dgvDachon.Rows[row].Cells[3].Value = dongia;
                    check = 1;
                    break;
                }
            }
            if (check == 0)
            {
                dgvDachon.Rows.Add(
                 MaSP,
                  tensp,
                  txtQualitytoAdd.Text,
                  dongia);
            }
            txtTotaltoFake.Text = String.Format("{0:#,0 vnđ}", calculatedBill());
        }
        //Check hóa đơn
        private bool CheckBill()
        {
            if (txtStatustoEdit.Text.Equals("Đã thanh toán"))
            {
                MessageBox.Show("Hóa đơn đã được thanh toán", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        //Sự kiện kiểm tra sau 30p' hủy hóa đon
        void timer_Tick(object sender, EventArgs e)
        {
            if (_service.getBill(BilltoChecked.MaHD).TrangThai=="Đang chờ ")
            {
                BilltoChecked.TrangThai = "Hủy";
                _service.DeleteBillDetailByID(BilltoChecked.MaHD);
                BilltoChecked.TongTien = 0;
                _service.EditBill(BilltoChecked);
                View();
                ViewErrors();
                MessageBox.Show("Hóa đơn " + BilltoChecked.MaHD + " đã bị hủy do hết thời gian ");
            }
            timer.Enabled = false;// Alert the user
        }
        //Tạo hóa đơn để chỉnh sửa
        HoaDon getBill()
        {

            return new HoaDon()
            {
                MaHD = txtIDBilltoEdit.Text,
                MaNV = txtIDEmployeetoEdit.Text,
                NgayLap = DateTime.Parse(txtDatetoEdit.Text),
                TongTien = txtTotaltoEdit.Text == "" ? 0 : decimal.Parse(txtTotaltoEdit.Text),
                TrangThai = txtStatustoEdit.Text,
                MaKH = txtIDCustomertoEdit.Text == "" ? "" : txtIDCustomertoEdit.Text,
            };
        }
        //Khởi tạo chi tiết hóa đơn
        ChiTietHoaDon getBillDetail(String mahd, string masp, decimal dongia, int soluong)
        {
            return new ChiTietHoaDon()
            {
                MaHD = mahd,
                MaSP = masp,
                DonGia = dongia,
                SoLuong = soluong
            };
        }
        //In hóa đơn 
        private void InHoaDon()
        {
            int i = 0;
            grbInhoadon.Visible = true;
            IEnumerable list = _service.listBillDetail(txtIDBilltoDetail.Text);
            listView1.Items.Clear();
            foreach (ChiTietHoaDon item in list)
            {
                ListViewItem listItem = listView1.Items.Add(i.ToString(),item.SanPham.TenSP,0);
                listItem.SubItems.Add(item.DonGia.ToString());
                listItem.SubItems.Add(item.SoLuong.ToString());
                listItem.SubItems.Add((item.DonGia*item.SoLuong).ToString());
            }
            if (txtIDCustomertoEdit.Text == "")
                lblKhachHang2.Text = "Khách vãng lai";
            else
                lblKhachHang2.Text = " Mã khách hàng: " + _service.getCustomer(txtIDCustomertoEdit.Text).MaKH + "|| Họ và tên: " + _service.getCustomer(txtIDCustomertoEdit.Text).Ten;
            lblNhanVien.Text = " Mã nhân viên: " + Information.Nhanvien.MaNV + "|| Họ và tên: " + Information.Nhanvien.Ten;
            lblTongTien.Text = "\r\n Tổng tiền: " + txtThanhtien.Text + "\r\n Thanh toán tiền mặt: " + txtKhachdua.Text + "\r\n Tiền thừa: " + txtTienthua.Text;
            lblNgay.Text = _service.getBill(txtIDBilltoDetail.Text).NgayLap.ToString();
        }

        #endregion
        #region Event-Handler
        //Lấy thông tin hóa đơn từ datagrid
        private void dgvHoadon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ClearTextBox();
            try
            {
                txtIDBilltoEdit.Text = dgvHoadon[0, e.RowIndex].Value.ToString();
                txtDatetoEdit.Text = dgvHoadon[1, e.RowIndex].Value.ToString();
                txtIDEmployeetoEdit.Text = dgvHoadon[2, e.RowIndex].Value.ToString();
                txtTotaltoEdit.Text = dgvHoadon[4, e.RowIndex].Value.ToString();
                txtStatustoEdit.Text = dgvHoadon[5, e.RowIndex].Value.ToString();
                txtIDCustomertoEdit.Text = dgvHoadon[3, e.RowIndex].Value.ToString();
            }
            catch { }
        }
        //Click 2 lần vào bảng
        private void dgvHoadon_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ViewDetail();
        }
        //Tự động thêm hóa đơn
        private void btnThem_Click(object sender, EventArgs e)
        {
            dgvHoadon.ClearSelection();
            HoaDon billToCreate = _service.billCreateNew();
            if (txtMaKH.Text != "")
                billToCreate.MaKH = txtMaKH.Text;
            if (_service.CreateBill(billToCreate))
            {
                MessageBox.Show("Thành công");
                View();
                dgvHoadon.CurrentCell = dgvHoadon[0, dgvHoadon.Rows.Count - 1];
                dgvHoadon.Rows[dgvHoadon.Rows.Count - 1].Selected = true;
            }
            else
            {
                MessageBox.Show("Hệ thống đang bận, Vui lòng thử lại", "Error", MessageBoxButtons.RetryCancel,MessageBoxIcon.Error);
            }
            ViewErrors();
        
        }
        //Sửa hóa đơn
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtIDBilltoEdit.Text.Equals(""))
                MessageBox.Show("Vui lòng chọn hóa đơn");
            else
            {
                billtoBackup = getBill();
                inforBill.Visible = true;
            }
        }
        //Thoát chỉnh sửa hóa đơn
        private void btnThoat_Click(object sender, EventArgs e)
        {
            inforBill.Visible = false;
        }
        //Lưu chỉnh sửa hóa đơn
        private void btnsaveEdit_Click(object sender, EventArgs e)
        {
            HoaDon billToEdit = getBill();
            if (_service.EditBill(billToEdit))
            {
                MessageBox.Show("Thành công","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.None);
                inforBill.Visible = false;
                View();
            }
            else
            {
                MessageBox.Show("Lỗi: Vui lòng thử lại sau", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            ViewErrors();
        }
        //Phục hồi hóa đơn chưa chỉnh sửa
        private void btnbackuptoEdit_Click(object sender, EventArgs e)
        {
            txtIDCustomertoEdit.Text = billtoBackup.MaKH;
        }
        //Xóa hóa đơn
        private void btnXoa_Click(object sender, EventArgs e)
        {
            //Check hóa đơn đã đươc chọn chưa
            if (txtIDBilltoEdit.Text.Equals(""))
                MessageBox.Show("Vui lòng chọn hóa đơn");
            else
            {
                HoaDon billtoDelete = getBill();
                DialogResult dlr = MessageBox.Show("Bạn có muốn xóa hóa đơn", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlr == DialogResult.Yes)
                {
                    if (_service.DeleteBill(billtoDelete))
                        MessageBox.Show("Thành công");
                    View();
                   
                }
            }
        }
        //Hiển thị chi tiết hóa đơn
        private void btnChitiet_Click(object sender, EventArgs e)
        {
            ViewDetail();
        }
        //Tìm kiếm hóa đơn
        private void btnTimkiem_Click(object sender, EventArgs e)
        {                
            if (txtKeyofBilltoSearch.Text.Equals(""))
                View();
            else
                dgvHoadon.DataSource = _service.searchBill(txtKeyofBilltoSearch.Text, cboTypeofBill.SelectedItem.ToString());
            ViewErrors();
        }
        //Tìm kiếm sản phẩm trong chi tiết hóa đơn
        private void btnTimkiemsp_Click(object sender, EventArgs e)
        {                
            if (txtKeyofProducttoSearch.Text.Equals(""))
                viewSanPham();
            else
                viewSanPham(txtKeyofProducttoSearch.Text, cboTypeofProduct.SelectedItem.ToString());
        }
        //Thêm dữ liệu giả trên 2 datagridview
        private void btnThemsp_Click_1(object sender, EventArgs e)
        {
            //Kiểm tra hóa đơn và số lượng nhập vào
            if (!CheckBill()||CheckQuality())
                return;
            UpdateDGV();
        }
        //Xóa các dữ liệu giả trên bảng đã chọn
        private void btnXoasp_Click(object sender, EventArgs e)
        {
            if (CheckBill())
            {
                foreach (DataGridViewCell oneCell in dgvDachon.SelectedCells)
                {
                    if (oneCell.Selected)
                    {
                        ChiTietHoaDon ct = _service.getBillDetail(txtIDBilltoDetail.Text, MaSP.ToString());
                        _service.DeleteBillDetail(ct);
                        dgvDachon.Rows.RemoveAt(oneCell.RowIndex);
                    }
                }
                txtTotaltoFake.Text = String.Format("{0:#,0 vnđ}", calculatedBill());
                HoaDon billtoUpdate = _service.getBill(txtIDBilltoDetail.Text);
                billtoUpdate.TongTien = calculatedBill();
                billtoUpdate.TrangThai = "Đang chờ ";
                _service.EditBill(billtoUpdate);
            }
        }
        //Reset lại dgvDachon
        private void btnReset_Click(object sender, EventArgs e)
        {
            if (CheckBill())               
                ClearDGV();
        }
        //Lưu dữ các chi tiết hóa đơn - thêm chi tiết hóa đơn
        private void btnLuu_Click(object sender, EventArgs e)
        {
            decimal TongTien = 0;
            if (CheckBill())
            {
                if (dgvDachon.Rows.Count != 0)
                {
                    for (int row = 0; row < dgvDachon.Rows.Count; row++)
                    {
                        ChiTietHoaDon billdetailto = getBillDetail(txtIDBilltoDetail.Text, dgvDachon.Rows[row].Cells[0].Value.ToString(), decimal.Parse(dgvDachon.Rows[row].Cells[3].Value.ToString()), int.Parse(dgvDachon.Rows[row].Cells[2].Value.ToString()));
                        if (_service.getBillDetail(billdetailto.MaHD, billdetailto.MaSP) == null)
                        {
                            _service.CreateBillDetail(billdetailto);
                        }
                        else
                        {
                            _service.UpdateBillDetail(billdetailto);
                        }
                        TongTien += decimal.Parse(dgvDachon.Rows[row].Cells[3].Value.ToString())*int.Parse(dgvDachon.Rows[row].Cells[2].Value.ToString());
                    }
                }
                else
                {
                    _service.DeleteBillDetailByID(txtIDBilltoDetail.Text);
                }
                HoaDon billtoUpdate = _service.getBill(txtIDBilltoDetail.Text);
                billtoUpdate.TongTien = TongTien;
                billtoUpdate.TrangThai = "Đang chờ ";
                _service.EditBill(billtoUpdate);
                View();
                ViewErrors();
                BilltoChecked = billtoUpdate;
                txtTongtien.Text = _service.getBill(txtIDBilltoDetail.Text).TongTien.ToString();
                txtThanhtien.Text = txtTongtien.Text;
                btnTratien.Enabled = true;
                timer.Tick += new EventHandler(timer_Tick); 
                timer.Interval = (1000) * (30);            
                timer.Enabled = true; 
                timer.Start();
            }          
        }         
        //Chặn bấm tùm lum
        private void tabControl_Click(object sender, EventArgs e)
        {
            if ((tabControl.SelectedTab == tabChitiet ) && txtIDBilltoDetail.Text.Equals(""))
            {
                tabControl.SelectedTab = tabHoadon;
                MessageBox.Show("Vui lòng chọn hóa đơn để xem chi tiết");
                return;
            }
        }
        private void txtSoLuong_TextChanged(object sender, EventArgs e)
        {
            errProdive.Clear();
        }
        private void txtTimkiem_TextChanged(object sender, EventArgs e)
        {
            errProdive.Clear();
        }
        private void cboLoai_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboTypeofBill.AutoCompleteMode = AutoCompleteMode.Suggest;
            cboTypeofBill.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        private void cboLoaisp_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboTypeofProduct.AutoCompleteMode = AutoCompleteMode.Suggest;
            cboTypeofProduct.AutoCompleteSource = AutoCompleteSource.ListItems;
        }
        //Component: Điều hướng
        private void Seller_UI_Load(object sender, EventArgs e)
        {
            lbHello.Text = "Xin chào, " + Information.Nhanvien.Ten + " \nChức vụ: " + Information.Nhanvien.ChucVu + " ";
            ptrHinhAnh.ImageLocation = ("..\\..\\Images\\QuanLy.jpg");
            View();
            cboTypeofBill.SelectedIndex = 0;         
        }
        private void Seller_UI_FormClosed(object sender, FormClosedEventArgs e)
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
            Seller_UI_Load(sender, e);
        }
        private void label15_Click(object sender, EventArgs e)
        {
            Manager_Customer UI = new Manager_Customer();
            UI.Show();
            Visible = false;
        }
        #endregion
        private void btnTratien_Click_1(object sender, EventArgs e)
        {
            if (CheckBill())
            {
                if (txtTienthua.Text.Equals("-0.000") || txtTienthua.Text.Equals(""))
                    MessageBox.Show("Bạn chưa trả đủ tiền", "Lỗi: ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    if (_service.Comfirm(txtIDBilltoDetail.Text))
                    {
                        MessageBox.Show("Thanh toán thành công ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.None);
                        btnTratien.Enabled = false;
                        InHoaDon();
                    }
                    else
                    {
                        MessageBox.Show("Lỗi thanh toán", "Lỗi", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                    }
                }
            }
            View();
        }

        private void btnAddcode_Click_1(object sender, EventArgs e)
        {
            float tongtien = float.Parse(_service.getBill(txtIDBilltoDetail.Text).TongTien.ToString());
            if (txtMagiamgia.Text.Length == 5)
                CodeSales = _service.getCode(txtMagiamgia.Text);
            else errProdive.SetError(txtMagiamgia, "Mã nhập chưa đúng vui lòng kiểm tra lại");
            if (CodeSales == null)
                errProdive.SetError(txtMagiamgia, "Mã nhập chưa đúng vui lòng kiểm tra lại");
            else
            {
                errProdive.Clear();
                txtThanhtien.Text = (tongtien - tongtien / 100 * CodeSales.TiLe).ToString();
                txtKhachdua_TextChanged_1(sender, e);
            }
        }

        private void btnPrint_Click_1(object sender, EventArgs e)
        {
            if (ExportExcel.ExportEx(txtIDBilltoDetail.Text, lblNhanVien.Text, lblKhachHang2.Text, txtDatetoEdit.Text, this.listView1, lblTongTien.Text))
            {
                grbInhoadon.Visible = false;
                tabControl.SelectedTab = tabHoadon;
            }
        }

        private void txtKhachdua_TextChanged_1(object sender, EventArgs e)
        {
            try
            {
                if (decimal.Parse(txtKhachdua.Text) >= decimal.Parse(txtThanhtien.Text))
                    txtTienthua.Text = (decimal.Parse(txtKhachdua.Text) - decimal.Parse(txtThanhtien.Text)).ToString();
                else txtTienthua.Text = "-0.000";
            }
            catch { }
        }
    }
}
