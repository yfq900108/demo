using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace PdfDemo2._0
{
    public partial class FormPDF : Form
    {
        public FormPDF(string ip, string date, string no)
        {
            InitializeComponent();

            if (!string.IsNullOrEmpty(ip) && !string.IsNullOrEmpty(date) && !string.IsNullOrEmpty(no))
            {
                string str = System.Windows.Forms.Application.StartupPath;
                str += string.Format("\\REPORT\\" + "{0}.pdf", no);
                //000290290220180104090034.101
                //pdfDocument1.Load(str);

                axAcroPDF1.LoadFile(MyOpenFileDialog(str));

                bimg1.BackgroundImage = Image.FromFile(@"SIGN\name1.jpg");
                bimg2.BackgroundImage = Image.FromFile(@"SIGN\name2.jpg");
                bimg3.BackgroundImage = Image.FromFile(@"SIGN\name3.jpg");

            }


        }
        string MyOpenFileDialog(string str)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "PDF文档(*.pdf)|*.pdf";
            ofd.FileName = str;

            return ofd.FileName;

        }
        private void FormPDF_Load(object sender, EventArgs e)
        {

        }

        private void bimg1_Click(object sender, EventArgs e)
        {
            Program.IsPDFOK = true;
            axAcroPDF1.Dispose();
            this.Close();
        }

        private void bimg2_Click(object sender, EventArgs e)
        {
            Program.IsPDFOK = true;
            axAcroPDF1.Dispose();
            this.Close();

        }

        private void bimg3_Click(object sender, EventArgs e)
        {
            Program.IsPDFOK = true;
            axAcroPDF1.Dispose();
            this.Close();
        }




    }
}
