using System;
using System.Windows.Forms;
using MyProject.Infrastructure;
namespace MyProject.UI
{
    public partial class MenuStatistical : Form
    {
        public MenuStatistical()
        {
            InitializeComponent();
        }

        private void MenuStatistical_Load(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            Statistical_Product UI = new Statistical_Product();
            UI.Show();
            this.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Statistical_Bill UI = new Statistical_Bill();
            UI.Show();
                this.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }
    }
}
