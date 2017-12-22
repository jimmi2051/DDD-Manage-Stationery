using System;
using System.Windows.Forms;
using MyProject.Domain;
using MyProject.Service;
using MyProject.Infrastructure;
using System.Text.RegularExpressions;
namespace MyProject.UI
{
    public partial class CodeSales : Form
    {
        ICodesService _service;
        ModelStateDictionary modelState;
        public CodeSales()
        {
            InitializeComponent();
            if (_service == null)
                _service = DataFactory.getCodeService(modelState, Information.PersistanceStrategy);
            InitData();
        }
        private void InitData()
        {
            modelState = new ModelStateDictionary();
            _service = DataFactory.getCodeService(modelState, Information.PersistanceStrategy);
        }
        private void View()
        {
            dgvCodes.DataSource = _service.listCodes();
        }
        private void CodeSales_Load(object sender, EventArgs e)
        {
            View();
            cboStatus.SelectedIndex = 0;
        }
        private void ViewErrors()
        {
            errProdive.Clear();
            foreach (var item in modelState)
            {
                switch (item.Key)
                {
                    case "MaKM":
                        errProdive.SetError(txtIDCode, item.Value);
                        break;
                    case "TiLe":
                        errProdive.SetError(txtRatio, item.Value);
                        break;
                    default:
                        break;
                }
            }
        }
        private void dgvCodes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtIDCode.Text = dgvCodes[0, e.RowIndex].Value.ToString();
                if (dgvCodes[1, e.RowIndex].Value.ToString().Equals("1"))
                    cboStatus.SelectedIndex = 0;
                else cboStatus.SelectedIndex = 1;
                txtRatio.Text = dgvCodes[2, e.RowIndex].Value.ToString();
            }
            catch { }
        }
        private MaKhuyenMai GetCodes()
        {
            return new MaKhuyenMai()
            {
                MaKM = txtIDCode.Text,
                TrangThai = cboStatus.SelectedIndex == 0 ? "1" : "0",
                TiLe = float.Parse(txtRatio.Text)
            };
        }
        private void button1_Click(object sender, EventArgs e)
        {
            errProdive.Clear();
            if (checkRatio())
                return;
            MaKhuyenMai Target = GetCodes();
            var CodetoEdit = _service.getCodes(Target.MaKM);
            if (CodetoEdit == null)
                _service.CreateCode(Target);
            else
                _service.UpdateCode(Target);
            View();
            ViewErrors();
        }
        private void ClearText()
        {
            txtIDCode.Clear();
            txtRatio.Clear();
            cboStatus.SelectedIndex = -1;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            ClearText();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            dgvCodes.DataSource = _service.searchCodes(txtKeyofSearch.Text);
        }
        private bool checkRatio()
        {
            if(txtRatio.Text.Length == 0 )
            {
                errProdive.SetError(txtRatio, "Vui lòng nhập tỉ lệ hợp lệ");
                return true;
            }
            if (txtRatio.Text.Length > 0 && !Regex.IsMatch(txtRatio.Text, @"\d"))
            {
                errProdive.SetError(txtRatio, "Vui lòng nhập tỉ lệ hợp lệ");
                return true;
            }
            return false;
        }
    }
}
