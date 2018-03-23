using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OracleTest
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int s = Convert.ToInt32(textBox2.Text) / Convert.ToInt32(textBox3.Text);
            int t = s * Convert.ToInt32(textBox3.Text);
            textBox1.Text = s.ToString()+"###  "+t.ToString();
        }
    }
}
