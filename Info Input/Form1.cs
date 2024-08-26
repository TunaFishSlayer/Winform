using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Info_Input
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(MessageBox.Show("Bạn muốn thoát chương trình?","Cảnh báo",MessageBoxButtons.YesNo,MessageBoxIcon.Warning)== DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (tbxName.Text == "")
            {

            }    
        }
    }
}
