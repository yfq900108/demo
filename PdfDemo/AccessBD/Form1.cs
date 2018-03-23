using AXC;
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

namespace AccessBD
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        OleDbConnection conn;
        OleDbCommand da;
        private void button1_Click(object sender, EventArgs e)
        {
            string txtConn =
           "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=F:\\项目\\db\\ovehicles.mdb";
            conn = new OleDbConnection(txtConn);



            string txtCommand = "SELECT VEHICLE.* , '' as 排放等级     FROM VEHICLE";
            OleDbDataAdapter da = new OleDbDataAdapter(txtCommand, conn);
            DataSet ds = new DataSet("ds");
            da.Fill(ds, "Student");
            DataTable ff=ds.Tables[0];
            foreach(DataRow dr in   ff.Rows)
            {
                txtCommand = string.Format("SELECT FILENAME,PF FROM O_VEHICLES   where CLXH='{0}' AND FDJXH='{1}' ORDER BY FILENAME DESC ", dr["车辆型号"], dr["发动机型号"]);
                  da = new OleDbDataAdapter(txtCommand, conn);
                 DataSet dtt = new DataSet("ds");
                 da.Fill(dtt, "Student");
                 if (dtt.Tables.Count > 0)
                 {
                     foreach (DataRow drD in dtt.Tables[0].Rows)
                     {
                         if (Convert.ToDateTime(drD["FILENAME"]) < Convert.ToDateTime(dr["出厂时间"]))
                         {
                             dr["排放等级"] = drD["PF"];
                             break;
                         }
                       

                     }
                 }
            }
             NPOIExcelHelper.DataTableToExcel(ff, "测试", "D:/myt2.xls");
             
        }

        private void button2_Click(object sender, EventArgs e)
        {
          //  Helper helper = new Helper("Data Source=172.128.2.1/veims;User ID=zte;Password=zsfyqch;");
            string txtConn =
          "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=F:\\项目\\db\\ovehicles.mdb";
            conn = new OleDbConnection(txtConn);



            string txtCommand = "SELECT   O_VEHICLES.*       FROM O_VEHICLES";
            OleDbDataAdapter da = new OleDbDataAdapter(txtCommand, conn);
            DataSet ds = new DataSet("ds");
            da.Fill(ds, "Student");
            DataTable ff = ds.Tables[0];

            string sql = "";
            foreach (DataRow dr in ff.Rows)
            {
                sql += string.Format(@"insert into ZTE.VEHICLE_ITEM (ID, CLXH, CLMC, FDJXH, FDJSCC, MANUF, CLSB, FILENAME, PF, CLLB, VIN, BZ, JKC)
values ({0}, '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', to_date('{7}', 'YYYY-MM-DD'), {8}, '{9}', '{10}', '{11}', '{12}');\r\n", 
                dr["ID"], dr["CLXH"],   dr["CLMC"], dr["FDJXH"], dr["FDJSCC"], dr["MANUF"], dr["CLSB"], dr["FILENAME"], dr["PF"], dr["CLLB"], dr["VIN"], dr["BZ"], dr["JKC"]);
            }
            NetLog.WriteTextLog("insert",sql,DateTime.Now);
            //NPOIExcelHelper.DataTableToExcel(ff, "测试", "D:/myt5.xls");
        }
    }
}
