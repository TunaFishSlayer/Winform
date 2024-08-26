using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextfileReader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog FDialog = new OpenFileDialog())
                {
                    FDialog.Title = "Open TXT File";
                    FDialog.Filter = "TXT Files|*.txt";
                    FDialog.InitialDirectory = @"C:\";

                    if (FDialog.ShowDialog() == DialogResult.OK)
                    {
                        FDialog.CheckFileExists = true;
                        FDialog.CheckPathExists = true;
                        tbFilePath.Text = FDialog.FileName;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred: " + ex.Message, "Unexpected Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btReadFile_Click(object sender, EventArgs e)
        {
            try
            {
                string filePath = tbFilePath.Text;
                if (string.IsNullOrEmpty(filePath))
                {
                    MessageBox.Show("Please select a file first.", "File Path Missing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                DataTable dt = new DataTable();
                string[] lines = System.IO.File.ReadAllLines(filePath);
                if (lines.Length == 0)
                {
                    throw new Exception("The file is empty");
                }
                string[] headers = lines[0].Split(',');
                foreach (string header in headers)
                {
                    dt.Columns.Add(header, typeof(string));
                }
                for (int i = 1; i < lines.Length; i++)
                {
                    string[] fields = lines[i].Split('|');
                    dt.Rows.Add(fields);
                }
                dataGridView1.DataSource = dt;
                int totalAmountC = 0;
                int totalAmountP = 0;
                int counterC = 0;
                int counterP = 0;
                foreach (DataRow row in dt.Rows)
                {     
                    if (int.TryParse(row["amount"].ToString(), out int amount))
                    {
                    if (row["status"].ToString() == "P") { totalAmountP += amount; counterP++; }
                    if (row["status"].ToString() == "C") { totalAmountC += amount; counterC++; }
                    }                       
                }
                StringBuilder output = new StringBuilder();

                output.AppendLine(string.Format("Sl trạng thái P:\t{0:N0}\tSố tiền trạng thái P:\t{1:N0}", counterP, totalAmountP));
                output.AppendLine(string.Format("Sl trạng thái C:\t{0:N0}\tSố tiền trạng thái C:\t{1:N0}", counterC, totalAmountC));
                output.AppendLine(string.Format("Tổng sl:\t\t{0:N0}\tTổng tiền:\t{1:N0}", counterC + counterP, totalAmountC + totalAmountP));

                tbFileContent.Text = output.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred: " + ex.Message, "Unexpected Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}