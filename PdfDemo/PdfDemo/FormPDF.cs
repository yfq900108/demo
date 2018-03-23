using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace PdfDemo
{
    public partial class FormPDF : Form
    {
        public FormPDF(string ip,string date,string no)
        {
            InitializeComponent();

            if (!string.IsNullOrWhiteSpace(ip) && !string.IsNullOrWhiteSpace(date) && !string.IsNullOrWhiteSpace(no))
            {
                string str = System.Windows.Forms.Application.StartupPath;
                str += string.Format("\\REPORT\\" + "{0}.pdf",no);
                //000290290220180104090034.101
                pdfDocument1.Load(str);



                bimg1.BackgroundImage = Image.FromFile(@"SIGN\name1.jpg");
                bimg2.BackgroundImage = Image.FromFile(@"SIGN\name2.jpg");
                bimg3.BackgroundImage = Image.FromFile(@"SIGN\name3.jpg");

            }
         

        }

        private void FormPDF_Load(object sender, EventArgs e)
        {
          
        }

        private void bimg1_Click(object sender, EventArgs e)
        {
            this.Close();  
        }

        private void bimg2_Click(object sender, EventArgs e)
        {
            this.Close();  

        }

        private void bimg3_Click(object sender, EventArgs e)
        {
            this.Close();  
        }

        

        
    }
}
