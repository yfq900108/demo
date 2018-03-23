using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace PdfDemo2._0
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormImage1 FL1 = new FormImage1("172.128.99.1", "2018_01_04", "320115011801040951090210");
            FL1.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormImage2 FL2 = new FormImage2("172.128.99.1", "2018_01_04", "320115011801040951090210");
            FL2.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {

           // Form1 FPDF = new Form1( );
          //  FPDF.ShowDialog();

            if (Program.IsPDFOK)
            {
                OpenPDF();
            }

        }

        /// <summary>
        /// 打开检测报告
        /// </summary>
        private void OpenPDF()
        {
            Program.IsPDFOK = false;
            string FileName = string.Empty;
            
                    //当前检测报告签名窗体；
                     FormPDF FPDF = new FormPDF("172.128.99.1", "2018_01_04", "000290290220180104090034.101");
                    if (FPDF.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                    }
                    //删除检测报告文件（远程服务器上的）
                    //pStationService.deleteFile(FileName);
               
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Program.IsPDFOK)
            {
                OpenPDF();
            }
        }
    }
}
