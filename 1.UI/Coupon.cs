using System;
using System.Windows.Forms;
using MyProject.Domain;
using MyProject.Service;
using MyProject.Infrastructure;
using System.Collections;
using System.Text.RegularExpressions;
namespace MyProject.UI
{
    public partial class Coupon : Form
    {
        private ModelStateDictionary ModelState;
        IWareHouseService _service;
        PhieuNhapXuat target;
        public Coupon()
        {
            if (_service == null)
                _service = DataFactory.getWareHouseService(ModelState, Information.PersistanceStrategy);
            InitializeComponent();
            InitData();

        }
        #region view
        private void ViewErrors()
        {
            errProdive.Clear();
            foreach (var entry in ModelState)
            {
                switch (entry.Key)
                {
                    case "Timkiem":
                        errProdive.SetError(txtKeyofCoupon, entry.Value);
                        break;
                    default:
                        break;
                }
            }
        }
        private void View()
        {
            dgvCoupon.DataSource = _service.ListCoupon();
        }
        //hiển thị sản phẩm
        private void ViewProduct()
        {
            ClearDGV(dgvProduct);
            IEnumerable enumerable;
            using (Manager_Service _tempservice = DataFactory.getManagerService(ModelState, Information.PersistanceStrategy))
            {
                enumerable = _tempservice.ListProducts();
            }
                
                foreach (SanPham item in enumerable)
                {
                    dgvProduct.Rows.Add(
                        item.MaSP,
                        item.TenSP,
                        item.DanhMucSP.TenDM,
                        item.DonGia,
                        item.DonVi
                        );
                }
            
        }
        private void ViewProduct(String key, String type)
        {
            using (IManagerService _tempservice = DataFactory.getManagerService(ModelState, Information.PersistanceStrategy))
            {
                ClearDGV(dgvProduct);
                IEnumerable enumerable = _tempservice.SearchProducts(key, type);
                foreach (SanPham item in enumerable)
                {
                    dgvProduct.Rows.Add(
                        item.MaSP,
                        item.TenSP,
                        item.DanhMucSP.TenDM,
                        item.DonGia,
                        item.DonVi
                        );
                }
            }
        }
        private void ViewProduct(String DetailIn)
        {
            
            using (IManagerService _tempservice = DataFactory.getManagerService(ModelState, Information.PersistanceStrategy))
            {
                IEnumerable enumerable = _tempservice.ListProducts();
                cboIDProductofDetailIn.Items.Clear();
                foreach (SanPham item in enumerable)
                {
                    cboIDProductofDetailIn.Items.Add(item.MaSP);
                }
                cboIDProductofDetailIn.SelectedIndex = 0;
            }
        }
        //Hiển thị dữ liệu đã có Nhập - xuất
        private void ViewDetailAvaible(DataGridView dataGridView)
        {
            IEnumerable list = _service.listDetailCoupon(txtIDCoupontoEdit.Text);
            foreach (ChiTietPhieu item in list)
            {
                dataGridView.Rows.Add(
                                   item.MaSP,
                                   item.SanPham.TenSP,
                                   item.SoLuong,
                                   item.DonGia);
            }
        }       
        //Xem chi tiết xuất
        private void ViewDetailOut()
        {           
                ClearDGV(dgvDetailOut);
                tabControl.SelectedTab = tabChitiet;
                txtIDCouponDetailOut.Text = txtIDCoupontoEdit.Text;
                ViewProduct();
                if (_service.getCoupon(txtIDCoupontoEdit.Text).TongTien > 0)
                {
                    txtTotalFakeOut.Text = String.Format("{0:#,0 vnđ}", _service.getCoupon(txtIDCoupontoEdit.Text).TongTien);
                    ViewDetailAvaible(dgvDetailOut);
                }
                else
                {
                    txtTotalFakeOut.Text = "0 vnđ";
                }          
        }     
        //Xem chi tiết nhập 
        private void ViewDetailIn()
        {
            try
            {
                ClearDGV(dgvDetailIn);
                tabControl.SelectedTab = tabChitiet2;
                txtIDCouponDetailIn.Text = txtIDCoupontoEdit.Text;
                ViewProduct("Combo Mã sản phẩm");
                if (_service.getCoupon(txtIDCoupontoEdit.Text).TongTien > 0)
                {
                    txtTotalDetailIn.Text = String.Format("{0:#,0 vnđ}", _service.getCoupon(txtIDCoupontoEdit.Text).TongTien);
                    ViewDetailAvaible(dgvDetailIn);
                }
                else
                {
                    txtTotalDetailIn.Text = "0 vnđ";
                }
            }
            catch
            {
                MessageBox.Show("Vui lòng chọn phiếu","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Error);
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
            try{dgv.Rows.RemoveAt(0);}
            catch { }
        }
        #endregion
        #region Help
        //Khởi tạo data nếu chưa có
        private void InitData()
        {
            ModelState = new ModelStateDictionary();
            _service = DataFactory.getWareHouseService(ModelState, Information.PersistanceStrategy);
        }
        //Hiển thị nút bấm xuất
        private void EnabledButtonOut(Boolean key)
        {
            btnSaveOfDetail.Enabled = key;
            btnAddProduct.Enabled = key;
            btnResetProduct.Enabled = key;
            btnDeleteProduct.Enabled = key;
        }
        //Hiển thị nút bấm nhập
        private void EnabledButtonIn(Boolean key)
        {
            btnSaveOfDetailIn.Enabled = key;
            btnProductAddDetailIn.Enabled = key;
            btnDeleteProductIn.Enabled = key;
            btnResetProductIn.Enabled = key;
        }
        //Hiển thị nút của coupon
        private void EnabledButtonCoupon(Boolean key)
        {
            btnSaveofCoupon.Enabled = key;
            btnExitofCoupon.Enabled = key;
            cboStatustoEdit.Enabled = key;
        }
        //Xóa trắng bảng sản phẩm đã chọn       
        void ClearTextBox()
        {
            txtIDCoupontoEdit.Clear();
            txtIDEmployeetoEdit.Clear();
            txtTotaltoEdit.Clear();
            txtDatetoEdit.Clear();
            cboStatustoEdit.SelectedIndex = 0;
        }
        //Tính tổng tiền trên bảng đã chọn
        private decimal CalculatorTotal(DataGridView dataGridView)
        {
            {
                decimal Total = 0;
                for (int row = 0; row < dataGridView.Rows.Count; row++)
                {
                    Total += decimal.Parse(dataGridView.Rows[row].Cells[3].Value.ToString()) * decimal.Parse(dataGridView.Rows[row].Cells[2].Value.ToString());
                }
                return Total;
            }
        }
        //Thêm sản phẩm nhập
        private void InsertDGVDetailIn()
        {
            int check = 0;
            string NameProduct;
            int Quality = int.Parse(txtQualityProductIn.Text);
            decimal? Price;
            string MaSP = cboIDProductofDetailIn.SelectedItem.ToString();
            using (IManagerService service = DataFactory.getManagerService(ModelState, Information.PersistanceStrategy))
            {
                NameProduct = service.GetProduct(MaSP).TenSP;
                Price = service.GetProduct(MaSP).DonGia;
            }
            for (int row = 0; row < dgvDetailIn.Rows.Count; row++)
            {
                if (MaSP.Equals(dgvDetailIn.Rows[row].Cells[0].Value.ToString()))
                {
                    Quality = Convert.ToInt32(txtQualityProductIn.Text) + Convert.ToInt32(dgvDetailIn.Rows[row].Cells[2].Value);
                    dgvDetailIn.Rows[row].Cells[2].Value = Quality.ToString();
                    dgvDetailIn.Rows[row].Cells[3].Value = Price;
                    check = 1;
                    break;
                }
            }
            if (check == 0)
            {
                dgvDetailIn.Rows.Add(
                 MaSP,
                 NameProduct,
                 Quality,
                 Price);
            }
            txtTotalDetailIn.Text = String.Format("{0:#,0 vnđ}", CalculatorTotal(dgvDetailIn));
        }
        //Thêm sản phẩm xuất
        private void InsertDGVDetailOut()
        {
            int check = 0;
            int Quality;
            int selectedrow = dgvProduct.CurrentCell.RowIndex;
            String MaSP = dgvProduct.Rows[selectedrow].Cells[0].Value.ToString();
            string NameProduct = dgvProduct.Rows[selectedrow].Cells[1].Value.ToString();
            decimal Price = Convert.ToDecimal(dgvProduct.Rows[selectedrow].Cells[3].Value.ToString());
            for (int row = 0; row < dgvDetailOut.Rows.Count; row++)
            {
                if (MaSP.Equals(dgvDetailOut.Rows[row].Cells[0].Value.ToString()))
                {
                    Quality = Convert.ToInt32(txtQualityofProduct.Text) + Convert.ToInt32(dgvDetailOut.Rows[row].Cells[2].Value);
                    dgvDetailOut.Rows[row].Cells[2].Value = Quality.ToString();
                    dgvDetailOut.Rows[row].Cells[3].Value = Price;
                    check = 1;
                    break;
                }
            }
            if (check == 0)
            {
                dgvDetailOut.Rows.Add(
                 MaSP,
                  NameProduct,
                  txtQualityofProduct.Text,
                  Price);
            }
            txtTotalFakeOut.Text = String.Format("{0:#,0 vnđ}", CalculatorTotal(dgvDetailOut));
        }
        //Khởi tạo chi tiết phiếu bằng thông tin cho trước
        private ChiTietPhieu getCouponDetail(String maphieu, string masp, decimal Price, int Quality)
        {
            return new ChiTietPhieu()
            {
                MaPhieu = maphieu,
                MaSP = masp,
                DonGia = Price,
                SoLuong = Quality
            };
        }
        private Boolean CheckQuality()
        {
            errProdive.Clear();
            int flag = 0;
            if (txtQualityofProduct.Text.Length > 0 && !Regex.IsMatch(txtQualityofProduct.Text, @"\d"))
            {
                errProdive.SetError(txtQualityofProduct, "Vui lòng nhập hợp lệ");
                flag = 1;
            }
            if (txtQualityofProduct.Text.Length == 0)
            {
                errProdive.SetError(txtQualityofProduct, "Vui lòng nhập");
                flag = 1;
            }
            try
            {
                int SelectedRow = dgvProduct.CurrentCell.RowIndex;
                String MaSP = dgvProduct.Rows[SelectedRow].Cells[0].Value.ToString();
                if (!_service.checkAmount(_service.getWareHouse(MaSP), int.Parse(txtQualityofProduct.Text)))
                {
                    errProdive.SetError(txtQualityofProduct, "Số lượng không đủ ");
                    flag = 1;
                }
                if (flag == 1)
                    return false;
                return true;
            }
            catch
            {
                return false;
            }
        }
        //Khởi tạo đối tượng của kho
        private Kho getWareHouse(string maSP, string maPhieu)
        {
            return new Kho()
            {
                MaSP = maSP,
                MaPhieu = maPhieu,
                SoLuong = 0,
                NgayLap = null,
                NgayXuat = null
            };
        }
        #endregion
        #region Event-handler         
        //Thêm
        private void btnAddCounpon_Click(object sender, EventArgs e)
        {
            //Tự tạo 1 phiếu mới dựa <tự động>
            target = _service.createNewCoupon();
            txtIDCoupontoEdit.Text = target.MaPhieu;
            txtIDEmployeetoEdit.Text = target.MaNV;
            txtTotaltoEdit.Text = target.TongTien.ToString();
            txtDatetoEdit.Text = target.NgayLap.ToString();
            EnabledButtonCoupon(true);
        }
        //Cofirm tạo phiếu mới
        private void btnSaveofCoupon_Click(object sender, EventArgs e)
        {
            errProdive.Clear();
            PhieuNhapXuat editCoupon = _service.getCoupon(txtIDCoupontoEdit.Text);
            try
            {
                target.TrangThai = cboStatustoEdit.SelectedItem.ToString();
                if (_service.CreateCoupon(target))
                {
                    View();
                    dgvCoupon.CurrentCell = dgvCoupon[0, dgvCoupon.Rows.Count - 1];
                    dgvCoupon.Rows[dgvCoupon.Rows.Count - 1].Selected = true;
                }
                EnabledButtonCoupon(false);
            }
            catch
            {
                errProdive.SetError(cboStatustoEdit, "Vui lòng chọn dữ liệu hợp lệ ");
            }
        }
        //Xóa phiếu
        private void btnXoa_Click(object sender, EventArgs e)
        {
            target = _service.getCoupon(txtIDCoupontoEdit.Text);
            DialogResult dlr = MessageBox.Show("Bạn có muốn xóa phiếu ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlr == DialogResult.Yes)
            {
                if (_service.DeleteCoupon(target))
                {
                    MessageBox.Show("Đã xóa", "Thông báo ");
                    View();
                }
                else
                {
                    MessageBox.Show("Đã xảy ra lỗi", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        //Hủy k thêm phiếu
        private void btnThoat_Click(object sender, EventArgs e)
        {
            ClearTextBox();
            EnabledButtonCoupon(false);
        }
        //Click vào dgv
        private void dgvPhieu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            EnabledButtonCoupon(false);
            ClearTextBox();
            try
            {
                txtIDCoupontoEdit.Text = dgvCoupon[0, e.RowIndex].Value.ToString();
                txtIDEmployeetoEdit.Text = dgvCoupon[1, e.RowIndex].Value.ToString();                
                txtTotaltoEdit.Text = dgvCoupon[2, e.RowIndex].Value.ToString();
                txtDatetoEdit.Text = dgvCoupon[3, e.RowIndex].Value.ToString();
                String text = dgvCoupon[4, e.RowIndex].Value.ToString();
                if (text.Equals("Nhập"))
                    cboStatustoEdit.SelectedIndex = 0;
                else
                    cboStatustoEdit.SelectedIndex = 1;
            }
            catch { }
        }
        //Tìm kiếm phiếu
        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            try
            {
                errProdive.Clear();
                if (txtKeyofCoupon.Text.Equals(""))
                    View();
                else
                    dgvCoupon.DataSource = _service.searchCoupon(txtKeyofCoupon.Text, cboTypeofCoupon.SelectedItem.ToString());
                ViewErrors();
            }
            catch
            {
                errProdive.SetError(cboTypeofCoupon, "Vui lòng chọn dữ liệu hợp lệ" );
            }
        }
        //Thêm chi tiet phiếu - Xuất dữ liệu
        private void btnSaveOfDetailOut_Click(object sender, EventArgs e)
        {
            int flag = 0;
            Decimal TotalMoney = 0; 
            DialogResult dlr = Decimal.Parse(txtTotaltoEdit.Text) > 0 ? MessageBox.Show("Bạn có muốn thay đổi chi tiết phiếu ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk)
                : MessageBox.Show("Bạn có muốn lưu không? ", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
            if (dlr == DialogResult.Yes)
            {
                for (int row = 0; row < dgvDetailOut.Rows.Count; row++)
                {
                    String IDProduct = dgvDetailOut.Rows[row].Cells[0].Value.ToString();
                    Decimal Price = decimal.Parse(dgvDetailOut.Rows[row].Cells[3].Value.ToString());
                    int Quality = int.Parse(dgvDetailOut.Rows[row].Cells[2].Value.ToString());
                    ChiTietPhieu coupondetailto = getCouponDetail(txtIDCouponDetailOut.Text, IDProduct, Price, Quality);
                    if (_service.getDetailCoupon(txtIDCouponDetailOut.Text, IDProduct) == null)
                    {
                        if (!_service.CreateDetailCoupon(coupondetailto))
                            flag = 1;
                    }
                    else
                    {
                        if (!_service.UpdateDetailCoupon(coupondetailto))
                            flag = 1;
                    }
                    TotalMoney += Price * Quality;
                }
                if (flag == 0)
                {
                    PhieuNhapXuat Target = _service.getCoupon(txtIDCouponDetailOut.Text);
                    Target.TongTien = TotalMoney;
                    _service.EditCoupon(Target);
                    View();
                }
                else
                    MessageBox.Show("Lỗi: Hệ thống đang bận vui lòng thử lại sau", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Thêm sản phẩm phiếu xuất
        private void BtnThemsp_Click(object sender, EventArgs e)
        {        
           if (CheckQuality())
                InsertDGVDetailOut();        
        }
        //Xóa sản phẩm phiếu xuất trên datagridview
        private void btnXoasp_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewCell oneCell in dgvDetailOut.SelectedCells)
            {
                if (oneCell.Selected)
                {
                    ChiTietPhieu ct = _service.getDetailCoupon(txtIDCouponDetailOut.Text, dgvDetailOut[0, oneCell.RowIndex].Value.ToString());
                    _service.DeleteDetailCoupon(ct);
                    dgvDetailOut.Rows.RemoveAt(oneCell.RowIndex);
                }
            }
            txtTotalFakeOut.Text = String.Format("{0:#,0 vnđ}", CalculatorTotal(dgvDetailOut));
        }
        //Reset bảng xuất
        private void btnReset_Click(object sender, EventArgs e)
        {
            ClearDGV(dgvDetailOut);
        }
        //Lưu sản phẩm nhập
        private void btnLuuNhap_Click(object sender, EventArgs e)
        {
            Decimal TotalMoney=0;
            DialogResult dlr = Decimal.Parse(txtTotaltoEdit.Text) > 0 ? MessageBox.Show("Bạn có muốn thay đổi chi tiết phiếu ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk)
                : MessageBox.Show("Bạn có muốn lưu không? ", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
            if (dlr == DialogResult.Yes)
            {
                for (int row = 0; row < dgvDetailIn.Rows.Count; row++)
                {
                    String IDProduct = dgvDetailIn.Rows[row].Cells[0].Value.ToString();
                    Decimal Price = decimal.Parse(dgvDetailIn.Rows[row].Cells[3].Value.ToString());
                    int Quality = int.Parse(dgvDetailIn.Rows[row].Cells[2].Value.ToString());
                    ChiTietPhieu coupondetailto = getCouponDetail(txtIDCouponDetailIn.Text, IDProduct, Price, Quality);
                    if (_service.getDetailCoupon(txtIDCouponDetailIn.Text, IDProduct) == null)
                    {
                        _service.CreateDetailCoupon(coupondetailto);
                    }
                    else
                    {
                        _service.UpdateDetailCoupon(coupondetailto);
                    }
                    TotalMoney += Price * Quality;
                }
                PhieuNhapXuat Target = _service.getCoupon(txtIDCouponDetailIn.Text);
                Target.TongTien = TotalMoney;
                _service.EditCoupon(Target);
                View();
            }
        }
        //Thêm sản phẩm nhập
        private void btnThemSPNhap_Click(object sender, EventArgs e)
        {
            try
            {
                errProdive.Clear();
                InsertDGVDetailIn();
            }
            catch
            {
                errProdive.SetError(cboIDProductofDetailIn, "Vui lòng chọn dữ liệu hợp lệ ");
            }
        }
        //Xóa san phẩm nhập
        private void button2_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewCell oneCell in dgvDetailIn.SelectedCells)
            {
                if (oneCell.Selected)
                {
                    ChiTietPhieu ct = _service.getDetailCoupon(txtIDCouponDetailOut.Text, dgvDetailIn[0, oneCell.RowIndex].Value.ToString());
                    _service.DeleteDetailCoupon(ct);
                    dgvDetailIn.Rows.RemoveAt(oneCell.RowIndex);
                }
            }
        }
        // Reset sản phẩm nhập
        private void btnResetNhap_Click(object sender, EventArgs e)
        {
            ClearDGV(dgvDetailIn);
        }
        //Components Điều hướng
        private void label2_Click(object sender, EventArgs e)
        {
            WareHouse UI = new WareHouse();
            UI.Show();
            Visible = false;
        }
        private void Coupon_Load(object sender, EventArgs e)
        {
            lbHello.Text = "Xin chào, " + Information.Nhanvien.Ten + " \nChức vụ: " + Information.Nhanvien.ChucVu + " ";
            ptrHinhAnh.ImageLocation = ("..\\..\\Images\\QuanLy.jpg");
            View();
            cboTypeofCoupon.SelectedIndex = 0;
            cboStatustoEdit.SelectedIndex = 0;
            cboTypeofProduct.SelectedIndex = 0;
        }

        private void Coupon_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(Information.Nhanvien.ChucVu!="Giám đốc chi nhánh")
            Information.frmLogin.Show();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void cboLoai_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboTypeofCoupon.AutoCompleteMode = AutoCompleteMode.Suggest;
            cboTypeofCoupon.AutoCompleteSource = AutoCompleteSource.ListItems;
        }
        private void cboTypeofProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboTypeofProduct.AutoCompleteMode = AutoCompleteMode.Suggest;
            cboTypeofProduct.AutoCompleteSource = AutoCompleteSource.ListItems;
        }
        private void cboIDProductofDetailIn_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboTypeofProduct.AutoCompleteMode = AutoCompleteMode.Suggest;
            cboTypeofProduct.AutoCompleteSource = AutoCompleteSource.ListItems;
        }
        private void label1_Click(object sender, EventArgs e)
        {
            Account_UI UI = new Account_UI();
            UI.ShowDialog();
            Coupon_Load(sender, e);
        }
        private void tabControl_Click(object sender, EventArgs e)
        {
            if ((tabControl.SelectedTab == tabChitiet || tabControl.SelectedTab == tabChitiet2) && txtIDCoupontoEdit.Text.Equals(""))
            {
                MessageBox.Show("Vui lòng chọn phiếu :", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl.SelectedTab = tabPhieu;
            }
            if (tabControl.SelectedTab == tabChitiet && cboStatustoEdit.SelectedItem.ToString().Equals("Nhập") && !txtIDCoupontoEdit.Text.Equals(""))
            {
                tabControl.SelectedTab = tabChitiet2;
            }
            if (tabControl.SelectedTab == tabChitiet2 && cboStatustoEdit.SelectedItem.ToString().Equals("Xuất") && !txtIDCoupontoEdit.Text.Equals(""))
            {
                tabControl.SelectedTab = tabChitiet;
            }
        }
        private void btnChitiet_Click(object sender, EventArgs e)
        {
            if (txtIDCoupontoEdit.Text.Equals(""))
                MessageBox.Show("Vui lòng chọn phiếu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                if (cboStatustoEdit.SelectedItem.ToString().Equals("Nhập"))
                    ViewDetailIn();
                else
                {
                    ViewDetailOut();
                }
            }
        }
        private void dgvCoupon_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (txtIDCoupontoEdit.Text.Equals(""))
                MessageBox.Show("Vui lòng chọn phiếu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                if (cboStatustoEdit.SelectedItem.ToString().Equals("Nhập"))
                    ViewDetailIn();
                else
                {
                    ViewDetailOut();
                }
            }
        }
        //Tìm kiếm sản phẩm
        private void btnSearchofProduct_Click(object sender, EventArgs e)
        {
            try
            {
                errProdive.Clear();
                if (txtKeyofProduct.Text.Equals(""))
                    ViewProduct();
                else
                    ViewProduct(txtKeyofProduct.Text, cboTypeofProduct.SelectedItem.ToString());
            }
            catch
            {
                errProdive.SetError(cboTypeofProduct, "Vui lòng chọn dữ liệu ");
            }
        }
        #endregion

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {
            Statistical_WareHouse UI = new Statistical_WareHouse();
            UI.Show();
            this.Visible = false;
        }
    }
}
