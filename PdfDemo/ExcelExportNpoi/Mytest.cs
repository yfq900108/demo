using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ExcelExportNpoi
{
    public partial class Mytest : Form
    {
        DataTable dt;
        public Mytest()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Bind();
          
           // ExcelHelper.DataTableToExcel(dt, "D:/Ab.xls");

        }      
        void Bind()
        {
            dt = new DataTable();
            DataColumn dc = new DataColumn("a");
            dt.Columns.Add(dc);
            dc = new DataColumn("b");
            dt.Columns.Add(dc);
            dc = new DataColumn("c");
            dt.Columns.Add(dc);
            for (int i = 0; i < 50000; i++)
            {

                DataRow dr = dt.NewRow();
                dr["a"] = i;
                dr["b"] = i+1000;
                dr["c"] = i + 10000;
                dt.Rows.Add(dr);
            }
            //   data1.DataSource = dt;
            dataGridView1.DataSource = dt;
 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Stopwatch timeWatch = System.Diagnostics.Stopwatch.StartNew();
           // ExcelHelper.DatagridviewToExcel(dataGridView1, "D:/AA.xlsx");
            
           
           // NPOIHelper.DataTableToExcel(dt, "测试", "D:/测试.xls");
            NPOIExcelHelper.DataTableToExcel(dt, "测试", "D:/mytest1.xls");
            timeWatch.Stop();
            MessageBox.Show("总共耗时"+timeWatch.Elapsed);
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
          
         
        }
    }
}
