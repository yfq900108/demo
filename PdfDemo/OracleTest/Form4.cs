using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OracleTest
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataSet dd = ExcelToDS("D:\\ttttt.xls");
            Helper helper = new Helper("Data Source=172.128.2.1/veims;User ID=zte;Password=zsfyqch;");

            DataTable ff = dd.Tables[0];
            string sss = "";
            int ss = 0;
            //2897
            foreach (DataRow dr in ff.Rows)
            {
                if (ss >0 )
                {
                    if (dr[8].ToString() == "零排放")
                    {
                        sss = "407";
                    }
                    if (dr[8].ToString() == "国5")
                    {
                        sss = "406";
                    }
                    if (dr[8].ToString() == "国4")
                    {
                        sss = "405";
                    }
                    if (dr[8].ToString() == "国3")
                    {
                        sss = "404";
                    }
                    if (dr[8].ToString() == "国2")
                    {
                        sss = "403";
                    }
                    if (dr[8].ToString() == "国1")
                    {
                        sss = "402";
                    }
                    if (dr[8].ToString() == "国0")
                    {
                        sss = "401";
                    }

                    string sql = string.Format(@"update vehicle_base_info set 
REMARK=REMARK||'-201836，许立峰更新STANDARD为：'||'403'||'-' ,STANDARD='403'  where vehicle_sn='{0}'

 ",   dr[6].ToString());

                    ss += helper.ExecuteNonQuery1(sql);
                }
                else
                {
                    ss += 1;
                }

            }



            MessageBox.Show(ss.ToString());
        }

        public DataSet ExcelToDS(string Path)
        {
            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + Path + ";" + "Extended Properties='Excel 8.0;HDR=NO;IMEX=1';";  
            OleDbConnection conn = new OleDbConnection(strConn);
            conn.Open();
            string strExcel = "";
            OleDbDataAdapter myCommand = null;
            DataSet ds = null;
            strExcel = "select * from [sheet1$]";
            myCommand = new OleDbDataAdapter(strExcel, strConn);
            ds = new DataSet();
            myCommand.Fill(ds, "table1");
            return ds;
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DataSet dd = ExcelToDS("D:\\citycode.xls");
            Helper helper = new Helper("Data Source=172.128.2.1/veims;User ID=zte;Password=yufuqiang;");

            DataTable ff = dd.Tables[0];
            string sss = "";
            int ss = 0;
            //2897
            foreach (DataRow dr in ff.Rows)
            {
                if (ss > 0 && dr[0].ToString() != "无查询结果" && dr[0].ToString() != "")
                {
                    string sql = string.Format(@" insert into base_city_code values ('{0}','{1}') ", dr[0].ToString(), dr[1].ToString());

                    ss += helper.ExecuteNonQuery1(sql);
                }
                else
                {
                    ss += 1;
                }
            }



            MessageBox.Show(ss.ToString());
        }
    }
}
