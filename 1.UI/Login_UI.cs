using System;
using System.Windows.Forms;
using MyProject.Domain;
using MyProject.Infrastructure;
using MyProject.Service;
namespace MyProject.UI
{
    public partial class Login_UI : Form
    {
        private ModelStateDictionary ModelState;
        //Khai báo service
        Form UI;
        private ILoginService _service;
        public Login_UI()
        {

            if (_service == null)
            {
                //Factory
                _service = DataFactory.getLoginService(this.ModelState, Information.PersistanceStrategy);
            }
            InitializeComponent();
            InitData();
        }
        public Login_UI(ILoginService service) : this()
        {
            _service = service;
        }
        #region Helper
        private void InitData()
        {
            ModelState = new ModelStateDictionary();
            _service = DataFactory.getLoginService(this.ModelState, Information.PersistanceStrategy);
        }
        NguoiDung getND()
        {
            return new NguoiDung
            {
                ID = txtusername.Text,
                Pass = txtpassword.Text
            };

        }
        private void VisibleTxt(bool key)
        {
            txtTenTK.Visible = key;
            txtEmail.Visible = key;
            label5.Visible = key;
        }
        private void ClearTxt()
        {
            txtTenTK.Clear();
            txtEmail.Clear();
            label4.Text = "Tên tài khoản ";
        }
        #endregion
        #region Event_Handler
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (_service.getUser(getND()))
            {
                bgdWorker.RunWorkerAsync();
                switch (Information.Nhanvien.ChucVu)
                {
                    case "Quản lý":
                        UI = new Manager_Product();
                        break;
                    case "Quản lý kho":
                        UI = new WareHouse();
                        break;
                    case "Giám đốc chi nhánh":
                        UI = new MenuAdmin();
                        break;
                    default:
                        UI = new Seller_UI();
                        break;
                }
                progressBar1.Visible = true;
                Information.frmLogin = this;
                txtpassword.Text = "";
            }
            else            
                 MessageBox.Show("Lỗi: Vui lòng kiểm tra lại thông tin ", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            ViewErrors();       
       }
            
        private void btnexit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        private void txtusername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtpassword.Focus();
            }
        }
        private void txtpassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                InvokeOnClick(btnLogin, e);
            }
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            errProdive.Clear();
            grbResetPassword.Visible = true;
            linkLabel1.Enabled = false;
        }
        private void btnSend_Click(object sender, EventArgs e)
        {
            NguoiDung target = new NguoiDung();
            target.ID = txtTenTK.Text;
            target.Mail = txtEmail.Text;
            if (_service.CheckUser(target))
            {
                VisibleTxt(false);
                label4.Text = "Mật khẩu sẽ được gửi tới Email: \n" + txtEmail.Text + "\ntrong thời gian sớm nhất";
                btnSend.Enabled = false;
            }
            else
            {
                ViewErrors();
                errProdive.SetError(txtTenTK, "Vui lòng kiểm tra lại thông tin ");
                errProdive.SetError(txtEmail, "Vui lòng kiểm tra lại thông tin ");
            }
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            grbResetPassword.Visible = false;
            linkLabel1.Enabled = true;
            btnSend.Enabled = true;
            VisibleTxt(true);
            ClearTxt();
        }
        //Progess-bar
        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            for (int i = 1; i <= 100; i++)
            {
                // Wait 100 milliseconds.
                System.Threading.Thread.Sleep(10);
                // Report progress.
                bgdWorker.ReportProgress(i);
            }
        }
        private void backgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }
        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            this.Hide();
            UI.Show();
            progressBar1.Visible = false;
        }
        private void Login_UI_Load(object sender, EventArgs e)
        {
            progressBar1.Maximum = 100;
            progressBar1.Step = 1;
            progressBar1.Value = 0;
        }
        #endregion
        #region view
        private void ViewErrors()
        {
            errProdive.Clear();
            foreach (var entry in ModelState)
            {
                switch (entry.Key)
                {
                    case "Username":
                            errProdive.SetError(txtusername, entry.Value);
                            errProdive.SetError(txtTenTK, entry.Value);
                        break;
                    case "Password":
                        errProdive.SetError(txtpassword, entry.Value);
                        break;
                    case "Email":
                        errProdive.SetError(txtEmail, entry.Value);
                        break;
                    default:
                        break;
                }
            }         
        }
        #endregion

     
      
    }
}

