using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace PdfDemo
{
    public partial class FormImage2 : Form
    {
       string url1 = "";
        string url2 = "";
        
        public FormImage2(string ip, string date, string no)
        {
            InitializeComponent();
            if (!string.IsNullOrWhiteSpace(ip) && !string.IsNullOrWhiteSpace(date) && !string.IsNullOrWhiteSpace(no))
            {
                this.pb_f.Click += new EventHandler(pictureBox1_Click);//注册事件
                this.pb_b.Click += new EventHandler(pictureBox2_Click);//注册事件



                date = date.Replace("-", "_").Replace("/", "_");
                url1 = string.Format("http://{0}/pic/{1}/{2}_F.jpg ", ip, date, no);
                url2 = string.Format("http://{0}/pic/{1}/{2}_B.jpg ", ip, date, no);

                pb_f.Image = Image.FromStream(System.Net.WebRequest.Create(url1).GetResponse().GetResponseStream());

                pb_b.Image = Image.FromStream(System.Net.WebRequest.Create(url2).GetResponse().GetResponseStream());


            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            ShowImage sl = new ShowImage(url1);
            sl.ShowDialog();
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            ShowImage sl = new ShowImage(url2);
            sl.ShowDialog();
        }
       

    }
}
