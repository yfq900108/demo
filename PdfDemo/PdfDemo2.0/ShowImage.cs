using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace PdfDemo2._0
{
    public partial class ShowImage : Form
    {
        public ShowImage(string url)
        {
            InitializeComponent();

            pictureBox1.Image = Image.FromStream(System.Net.WebRequest.Create(url).GetResponse().GetResponseStream());
        }
    }
}
