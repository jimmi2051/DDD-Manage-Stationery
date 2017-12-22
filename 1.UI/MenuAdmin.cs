using MyProject.Infrastructure;
using System;
using System.Windows.Forms;
namespace MyProject.UI
{
    public partial class MenuAdmin : Form
    {
        public MenuAdmin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Seller_UI UI = new Seller_UI();
            UI.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Manager_Product UI = new Manager_Product();
            UI.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            WareHouse UI = new WareHouse();
            UI.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MenuAdmin_FormClosed(object sender, FormClosedEventArgs e)
        {
            Information.frmLogin.Show();
        }
    }
}
