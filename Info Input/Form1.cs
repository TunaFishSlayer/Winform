using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

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
            if (MessageBox.Show("Bạn muốn thoát chương trình?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (check())
            {
                StringBuilder output = new StringBuilder();
                output.AppendLine("Họ tên: \t\t" + tbxName.Text);
                output.AppendLine("Giới tính: \t\t" + cbbGender.SelectedItem);
                output.AppendLine("Ngày sinh: \t" + dateTimePicker1.Value.ToString("dd-mm-yyyy")); 
                output.AppendLine("Địa chỉ \t\t" + tbxAddress.Text);
                output.AppendLine("CCCD: \t\t" + tbxID.Text);
                tbxOutput.Text = output.ToString();
            }
        }
        
        bool check()
        {
            long cccd;
            if (tbxName.Text == "")
            {
                MessageBox.Show("Hãy nhập tên", "Thông báo");
                tbxName.Focus();
                return false;
            }
            if (tbxAddress.Text == "")
            {
                MessageBox.Show("Hãy nhập địa chỉ", "Thông báo");
                tbxAddress.Focus();
                return false;
            }
            if (tbxID.Text == "")
            {
                MessageBox.Show("Hãy nhập số CCCD", "Thông báo");
                tbxID.Focus();
                return false;
            }
            if (cbbGender.SelectedIndex == -1)
            {
                MessageBox.Show("Hãy chọn giới tính", "Thông báo");
                return false;
            }
            if (tbxName.Text.Any(char.IsDigit))
            {
                MessageBox.Show("Hãy nhập lại tên, tên không thể có số", "Thông báo");
                tbxName.Focus();
                return false;
            }
            if (!(long.TryParse(tbxID.Text, out cccd)))
            {
                MessageBox.Show("Hãy nhập lại đúng định dạng CCCD", "Thông báo");
                tbxID.Focus();
                return false;
            }
            if (cccd < 100000000000 || cccd > 999999999999)   
            {
                MessageBox.Show("CCCD có 12 số", "Thông báo");
                tbxID.Focus();
                return false;
            }

            return true;
        }
        public string CleanWhiteSpace(string input)
        {
            return string.Join(" ", input.Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
        }

        public string ToTitleCase(string input)
        {
            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            return textInfo.ToTitleCase(input.ToLower());
        }
        private void tbxName_Leave(object sender, EventArgs e)
        {
            tbxName.Text = CleanWhiteSpace(ToTitleCase(tbxName.Text));
        }

        private void tbxAddress_Leave(object sender, EventArgs e)
        {
            tbxAddress.Text = CleanWhiteSpace(ToTitleCase(tbxAddress.Text));
        }
    }
}
