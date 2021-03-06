﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace PdfDemo
{
    public partial class FormImage1 : Form
    {
        string url1 = "";
        string url2 = "";
        string url3 = "";
        public FormImage1(string ip, string date, string no)
        {
            InitializeComponent();


            if (!string.IsNullOrWhiteSpace(ip) && !string.IsNullOrWhiteSpace(date) && !string.IsNullOrWhiteSpace(no))
            {
                this.pb_v.Click += new EventHandler(pictureBox1_Click);//注册事件
                this.pb_l.Click += new EventHandler(pictureBox2_Click);//注册事件
                this.pb_r.Click += new EventHandler(pictureBox3_Click);//注册事件



                date = date.Replace("-", "_").Replace("/", "_");
                url1 = string.Format("http://{0}/uic/{1}/{2}_V.jpg ", ip, date, no);
                url2 = string.Format("http://{0}/uic/{1}/{2}_L.jpg ", ip, date, no);
                url3 = string.Format("http://{0}/uic/{1}/{2}_R.jpg ", ip, date, no);
                pb_v.Image = Image.FromStream(System.Net.WebRequest.Create(url1).GetResponse().GetResponseStream());

                pb_l.Image = Image.FromStream(System.Net.WebRequest.Create(url2).GetResponse().GetResponseStream());
                pb_r.Image = Image.FromStream(System.Net.WebRequest.Create(url3).GetResponse().GetResponseStream());



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
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            ShowImage sl = new ShowImage(url3);
            sl.ShowDialog();
        }

        
    }
}
