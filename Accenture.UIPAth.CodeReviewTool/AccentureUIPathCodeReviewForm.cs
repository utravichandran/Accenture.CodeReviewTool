using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Accenture.UIPAth.CodeReviewTool.Infrastructure;

namespace Accenture.UIPAth.CodeReviewTool
{
    public partial class AccentureUIPathCodeReviewForm : Form
    {
        public AccentureUIPathCodeReviewForm()
        {
           
            InitializeComponent();
           
            this.dtgCodeReview.AutoResizeColumns();
            this.dtgCodeReview.ClipboardCopyMode =
                DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.Refresh();
        }
        private void folderBrowserDialog2_HelpRequest(object sender, EventArgs e)
        {

        }



        private void lbl_Click(object sender, EventArgs e)
        {

        }
        private void btnCloseApplication_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure To Close ?", "Exit", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                System.Windows.Forms.Application.Exit();
            }


        }
        private void btnBrowseFile_Click_1(object sender, EventArgs e)
       {
                   OpenFileDialog openFileDialog1 = new OpenFileDialog();

                   openFileDialog1.InitialDirectory = @"C:\";
                   openFileDialog1.Title = "Select XAML Files for Review";
                   openFileDialog1.CheckFileExists = true;
                   openFileDialog1.CheckPathExists = true;
                   openFileDialog1.DefaultExt = "xaml";
                   openFileDialog1.Filter = "XAML files (*.xaml)|*.xaml";
                   openFileDialog1.FilterIndex = 2;
                   openFileDialog1.RestoreDirectory = true;
                   openFileDialog1.ReadOnlyChecked = true;
                   openFileDialog1.ShowReadOnly = true;
                   txtFilePath.Text = openFileDialog1.FileName;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.txtFilePath.Visible = true;
                this.txtFilePath.Text = openFileDialog1.FileName;
            }
        }
        private void AccentureUIPathCodeReviewForm_Load(object sender, EventArgs e)
        {

        }

       
        private void button2_Click(object sender, EventArgs e)
        {
            ExportToExcel excel=new ExportToExcel();
            
            excel.ExportDataToExcel(dtgCodeReview);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            dtgCodeReview.Rows.Clear();
            string path = txtFilePath.Text;
            string filename = path;

            ReadXML read = new ReadXML();
            read.ReadDataFromXML(dtgCodeReview, filename);
            
           
        }
        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

       
       

        private void btnCopyToClipboard_Click(object sender, EventArgs e)
        {
            CopyToClipboard clipboard = new CopyToClipboard();

            clipboard.CopyDataToClipboard(dtgCodeReview);

        }


    }
}
