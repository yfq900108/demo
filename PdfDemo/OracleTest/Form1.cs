using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OracleTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Helper helper = new Helper("Data Source=172.128.2.1/veims;User ID=zte;Password=zsfyqch;");


          //  string strp1 = System.Windows.Forms.Application.StartupPath;
          //  strp1 += ("\\demo\\" + "1112111.xls");
          //  ExportToExcel(helper.ExecuteDataSet("select * from base_user t").Tables[0], strp1);



            string data1 = string.Empty;
            string str = System.Windows.Forms.Application.StartupPath;
            str += ("\\demo\\" + "test.txt");
            FileStream stream = new FileStream(str, FileMode.Open, FileAccess.ReadWrite);
            StreamReader reader = new StreamReader(stream, Encoding.UTF8);
            data1 = reader.ReadToEnd();
            reader.Dispose();
            reader.Close();
            stream.Close();
            if (data1 != null)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("名称", typeof(string)));
                dt.Columns.Add(new DataColumn("国0", typeof(string)));
                dt.Columns.Add(new DataColumn("国1", typeof(string)));
                dt.Columns.Add(new DataColumn("国2", typeof(string)));
                dt.Columns.Add(new DataColumn("国3", typeof(string)));
                dt.Columns.Add(new DataColumn("国4", typeof(string)));
                dt.Columns.Add(new DataColumn("国5", typeof(string)));
                dt.Columns.Add(new DataColumn("零排放", typeof(string)));
                dt.Columns.Add(new DataColumn("混合动力", typeof(string)));
                string[] vals1 = data1.Split(';');
                for (int i = 0; i < vals1.Length - 1; i++)
                {
                    DataRow dr = dt.NewRow();

                    string[] vv = vals1[i].Split('#');

                    dr[0] = (vv[0].Replace("--",""));


                    DataTable dv = new DataTable();

                    dv=  helper.ExecuteDataSet(vv[1]).Tables[0];
                    if (dv.Rows.Count > 0)
                    {
                        for (int k = 0; k < dv.Rows.Count; k++)
                        {
                            if (dv.Rows[k][0].ToString() == "401")
                            {
                                dr[1] = dv.Rows[k][1];
                            }
                            if (dv.Rows[k][0].ToString() == "402")
                            {
                                dr[2] = dv.Rows[k][1];
                            }
                            if (dv.Rows[k][0].ToString() == "403")
                            {
                                dr[3] = dv.Rows[k][1];
                            }
                            if (dv.Rows[k][0].ToString() == "404")
                            {
                                dr[4] = dv.Rows[k][1];
                            }
                            if (dv.Rows[k][0].ToString() == "405")
                            {
                                dr[5] = dv.Rows[k][1];
                            }
                            if (dv.Rows[k][0].ToString() == "406")
                            {
                                dr[6] = dv.Rows[k][1];
                            }
                            if (dv.Rows[k][0].ToString() == "407")
                            {
                                dr[7] = dv.Rows[k][1];
                            }

                        }

                    }
                    else
                    {

                        dr[1] = 0;
                        dr[2] = 0;
                        dr[3] = 0;
                        dr[4] = 0;
                        dr[5] = 0;
                        dr[6] = 0;
                        dr[7] = 0;
                        dr[8] = 0;

                    }
                    dt.Rows.Add(dr);

                }

                string strp = System.Windows.Forms.Application.StartupPath;
                 strp += ("\\demo\\" + "11122.xls");
                 ExportToExcel(dt, strp);
            }
              

        }

        private string MakeInspectionPeriod2(DateTime registerDate, string vehicleType, string vehiclePurpose)
        {
            string s = "";

            #region ...
            try
            {
                /*
                TimeSpan x = DateTime.Now - registerDate;
                int n = (int)(x.TotalDays ) / 365 * 12 + 2;
                */

                int n = ((DateTime.Today.Year - registerDate.Year) * 12) + ((DateTime.Today.Month - registerDate.Month) + 2);

                Console.WriteLine(n.ToString());

                if (vehiclePurpose == "1202")
                {
                    #region 营运
                    if (vehicleType == "308")//校车
                    {
                        s = "6";
                    }
                    else if (vehicleType == "301" || vehicleType == "302" || vehicleType == "305")//载客汽车 & 不管大小车 & 教练汽车； 
                    {
                        if (n < 60)
                        {
                            s = "12";
                        }
                        else
                        {
                            s = "6";
                        }
                    }
                    else if (vehicleType == "303" || vehicleType == "304" || vehicleType == "315")//载货汽车
                    {
                        if (n < 120)
                        {
                            s = "12";
                        }
                        else
                        {
                            s = "6";
                        }
                    }
                    else if (vehicleType == "306")//306警用汽车； 
                    {
                        if (n < 72)
                        {
                            s = "24";
                        }
                        else if (n >= 72 && n <= 270)
                        {
                            s = "12";
                        }
                        else
                        {
                            s = "6";
                        }
                    }
                    else//307其它 
                    {
                        s = "12";
                    }
                    #endregion
                }
                else
                {
                    #region 非营运
                    if (vehicleType == "301" || vehicleType == "304")
                    {
                        if (n < 120)
                        {
                            s = "12";
                        }
                        else
                        {
                            s = "6";
                        }
                    }
                    else if (vehicleType == "302")
                    {
                        if (n < 72)
                        {
                            s = "24";
                        }
                        else if (n >= 72 && n <= 180)
                        {
                            s = "12";
                        }
                        else
                        {
                            s = "6";
                        }
                    }
                    else if (vehicleType == "303")
                    {
                        if (n < 120)
                        {
                            s = "12";
                        }
                        else
                        {
                            s = "6";
                        }
                    }
                    else if (vehicleType == "308")//校车
                    {
                        s = "6";
                    }
                    else if (vehicleType == "305")//305教练汽车； 
                    {
                        if (n < 60)
                        {
                            s = "12";
                        }
                        else
                        {
                            s = "6";
                        }
                    }
                    else if (vehicleType == "306")//306警用汽车； 
                    {
                        if (n < 72)
                        {
                            s = "24";
                        }
                        else if (n >= 72 && n <= 270)
                        {
                            s = "12";
                        }
                        else
                        {
                            s = "6";
                        }
                    }
                    else//307其它 
                    {
                        s = "12";
                    }
                    #endregion
                }

            }
            catch
            {
            }
            #endregion

            return s;
        }
        private string MakeInspectionPeriod(DateTime registerDate, string vehicleType, string vehiclePurpose)
        {
            string s = "";

            #region ...
            try
            {
                /*
                TimeSpan x = DateTime.Now - registerDate;
                int n = (int)(x.TotalDays ) / 365 * 12 + 2;
                */

                int n = ((DateTime.Today.Year - registerDate.Year) * 12) + ((DateTime.Today.Month - registerDate.Month) + 2);

                Console.WriteLine(n.ToString());

                if (vehiclePurpose == "1202")
                {
                    #region 营运
                    if (vehicleType == "308")//校车
                    {
                        s = "6";
                    }
                    else if (vehicleType == "301" || vehicleType == "302" || vehicleType == "305")//载客汽车 & 不管大小车 & 教练汽车； 
                    {
                        if (n < 60)
                        {
                            s = "12";
                        }
                        else
                        {
                            s = "6";
                        }
                    }
                    else if (vehicleType == "303" || vehicleType == "304" || vehicleType == "315")//载货汽车
                    {
                        if (n < 120)
                        {
                            s = "12";
                        }
                        else
                        {
                            s = "6";
                        }
                    }
                    else if (vehicleType == "306")//306警用汽车； 
                    {
                        if (n < 72)
                        {
                            s = "24";
                        }
                        else if (n >= 72 && n <= 270)
                        {
                            s = "12";
                        }
                        else
                        {
                            s = "6";
                        }
                    }
                    else//307其它 
                    {
                        s = "12";
                    }
                    #endregion
                }
                else
                {
                    #region 非营运
                    if (vehicleType == "301" || vehicleType == "304")
                    {
                        if (n < 120)
                        {
                            s = "12";
                        }
                        else
                        {
                            s = "6";
                        }
                    }
                    else if (vehicleType == "302")
                    {
                        if (n < 72)
                        {
                            s = "24";
                        }
                        else if (n >= 72 && n <= 180)
                        {
                            s = "12";
                        }
                        else
                        {
                            s = "6";
                        }
                    }
                    else if (vehicleType == "303")
                    {
                        if (n < 120)
                        {
                            s = "12";
                        }
                        else
                        {
                            s = "6";
                        }
                    }
                    else if (vehicleType == "308")//校车
                    {
                        s = "6";
                    }
                    else if (vehicleType == "305")//305教练汽车； 
                    {
                        if (n < 60)
                        {
                            s = "12";
                        }
                        else
                        {
                            s = "6";
                        }
                    }
                    else if (vehicleType == "306")//306警用汽车； 
                    {
                        if (n < 72)
                        {
                            s = "24";
                        }
                        else if (n >= 72 && n <= 270)
                        {
                            s = "12";
                        }
                        else
                        {
                            s = "6";
                        }
                    }
                    else//307其它 
                    {
                        s = "12";
                    }
                    #endregion
                }

            }
            catch
            {
            }
            #endregion

            return s;
        }



        /// <summary>  
        /// 导出文件，使用文件流。该方法使用的数据源为DataTable,导出的Excel文件没有具体的样式。  
        /// </summary>  
        /// <param name="dt"></param>  
        public static string ExportToExcel(System.Data.DataTable dt, string path)
        {
            KillSpecialExcel();
            string result = string.Empty;
            try
            {
                // 实例化流对象，以特定的编码向流中写入字符。  
                StreamWriter sw = new StreamWriter(path, false, Encoding.GetEncoding("gb2312"));

                StringBuilder sb = new StringBuilder();
                for (int k = 0; k < dt.Columns.Count; k++)
                {
                    // 添加列名称  
                    sb.Append(dt.Columns[k].ColumnName.ToString() + "\t");
                }
                sb.Append(Environment.NewLine);
                // 添加行数据  
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow row = dt.Rows[i];
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        // 根据列数追加行数据  
                        sb.Append(row[j].ToString() + "\t");
                    }
                    sb.Append(Environment.NewLine);
                }
                sw.Write(sb.ToString());
                sw.Flush();
                sw.Close();
                sw.Dispose();

                // 导出成功后打开  
                //System.Diagnostics.Process.Start(path);  
            }
            catch (Exception)
            {
                result = "请保存或关闭可能已打开的Excel文件";
            }
            finally
            {
                dt.Dispose();
            }
            return result;
        }
        /// <summary>  
        /// 结束进程  
        /// </summary>  
        private static void KillSpecialExcel()
        {
            foreach (System.Diagnostics.Process theProc in System.Diagnostics.Process.GetProcessesByName("EXCEL"))
            {
                if (!theProc.HasExited)
                {
                    bool b = theProc.CloseMainWindow();
                    if (b == false)
                    {
                        theProc.Kill();
                    }
                    theProc.Close();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(MakeInspectionPeriod2(Convert.ToDateTime("2004-08-28"), textBox1.Text, textBox2.Text));
        }  
    }
}
