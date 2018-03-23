using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EXCELNPOI
{
    public partial class Form1 : Form
    {
        DataTable dt;
        public Form1()
        {
            InitializeComponent();
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
                dr["b"] = i + 1000;
                dr["c"] = i + 10000;
                dt.Rows.Add(dr);
            }
            //   data1.DataSource = dt;
           

        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Stopwatch timeWatch = System.Diagnostics.Stopwatch.StartNew();
            // ExcelHelper.DatagridviewToExcel(dataGridView1, "D:/AA.xlsx");


            Helper helper = new Helper("Data Source=172.128.2.1/veims;User ID=zte;Password=zsfyqch;");


            string sql = @"
                         select t.table_name,  trunc((t.num_rows/10000  ),4) num_rows ,
                         case  when   trunc(u.tablegb,4)=0 then 0.0001 
                         when trunc(u.tablegb,4)='' then 0.00001 else  trunc(u.tablegb,4)  
                         end tablegb   from user_tables t  
                         left join 
                         (Select Segment_Name,(Sum(bytes)/1024/1024/1024 )as tablegb  From User_Extents Group By Segment_Name) u
                         on u.Segment_Name=t.table_name 
                         order by t.table_name asc
                        ";

            DataSet ds = helper.ExecuteDataSet(sql);


            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("信息资源名称", typeof(string)));
            dt.Columns.Add(new DataColumn("信息资源英文名", typeof(string)));
            dt.Columns.Add(new DataColumn("信息资源代码", typeof(string)));
            dt.Columns.Add(new DataColumn("信息资源提供方", typeof(string)));
            dt.Columns.Add(new DataColumn("提供方内部部门", typeof(string)));
            dt.Columns.Add(new DataColumn("资源提供方代码", typeof(string)));
            dt.Columns.Add(new DataColumn("信息资源摘要", typeof(string)));
            dt.Columns.Add(new DataColumn("来源信息系统名称", typeof(string)));
            dt.Columns.Add(new DataColumn("信息资源格式分类", typeof(string)));
            dt.Columns.Add(new DataColumn("信息资源格式类型", typeof(string)));
            dt.Columns.Add(new DataColumn("其他类型资源格式描述", typeof(string)));
            dt.Columns.Add(new DataColumn("数据存储总量（G）", typeof(string)));
            dt.Columns.Add(new DataColumn("结构化信息记录总数（万）", typeof(string)));
            dt.Columns.Add(new DataColumn("已共享的数据存储量（G）", typeof(string)));
            dt.Columns.Add(new DataColumn("已共享结构化记录数（万）", typeof(string)));
            dt.Columns.Add(new DataColumn("已开放的数据存储量（G）", typeof(string)));
            dt.Columns.Add(new DataColumn("已开放结构化记录数（万）", typeof(string)));
            dt.Columns.Add(new DataColumn("信息项名称", typeof(string)));
            dt.Columns.Add(new DataColumn("信息项英文名", typeof(string)));
            dt.Columns.Add(new DataColumn("数据类型", typeof(string)));
            dt.Columns.Add(new DataColumn("数据长度", typeof(string)));
            dt.Columns.Add(new DataColumn("共享类型", typeof(string)));
            dt.Columns.Add(new DataColumn("共享条件", typeof(string)));
            dt.Columns.Add(new DataColumn("共享方式分类", typeof(string)));
            dt.Columns.Add(new DataColumn("共享方式类型", typeof(string)));
            dt.Columns.Add(new DataColumn("是否向社会开放", typeof(string)));
            dt.Columns.Add(new DataColumn("开放条件", typeof(string)));
            dt.Columns.Add(new DataColumn("更新周期", typeof(string)));
            dt.Columns.Add(new DataColumn("发布日期", typeof(string)));
            dt.Columns.Add(new DataColumn("关联及类目名称", typeof(string)));

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = getval(ds.Tables[0].Rows[i][0].ToString().ToUpper(), "表",'_');
                dr[1] = ds.Tables[0].Rows[i][0].ToString();
                dr[2] = "";
                dr[3] = "市环保局";
                dr[4] = "技术处";
                dr[5] = "12320100671322000Q";
                dr[6] = ds.Tables[0].Rows[i][0].ToString() + "表";
                dr[7] = "机动车排气污染监督管理系统";
                dr[8] = "数据库";
                dr[9] = "oracle";
                dr[10] = "";
                dr[11] = ds.Tables[0].Rows[i][2];
                dr[12] = ds.Tables[0].Rows[i][1];
                dr[13] = ds.Tables[0].Rows[i][2];
                dr[14] = ds.Tables[0].Rows[i][1];
                dr[15] = ds.Tables[0].Rows[i][2];
                dr[16] = ds.Tables[0].Rows[i][1];
                dr[17]="2";
                dr[18]="3";
                dr[19]="4";
                dr[20]="5";
                dr[21]="6";
                dr[22]="";
                dr[23]="";
                dr[24]="";
                dr[25]="";
                dr[26]="";
                dr[27] = "每日";
                dr[28] = "2017/09/10";
                dr[29]="";

                dt.Rows.Add(dr);
            }


























            // NPOIHelper.DataTableToExcel(dt, "测试", "D:/测试.xls");
           // NPOIExcelHelper.DataTableToExcel(dt, "测试", "D:/mytest1.xls");
            NPOIExcelHelper.DataTableToExcel2(dt, "测试", "D:/mytest2.xls");
            timeWatch.Stop();
            MessageBox.Show("总共耗时" + timeWatch.Elapsed);

        }


       



        private TranClass tranClass = new TranClass();
        public string getval(string con,string remark,char split)
        {
            string res = "";
            string[] dd = con.Split(split);
            
            for (int k = 0; k < dd.Length; k++)
            {
                res += gettt(dd[k].ToUpper());
            }
            res += remark;
            return res;
        }
        private string gettt(string con)
        {

            WebClient client = new WebClient();  //引用System.Net
            string fromTranslate = con.ToUpper(); //翻译前的内容
          
            if (!string.IsNullOrEmpty(fromTranslate))
            {
                //例：将apple从英文翻译成中文：
                //请求参数：
                //q=apple
                //from=en
                //to=zh
                //appid=20180117000115918
                //salt=1435660288
                //平台分配的密钥: 12345678
                //生成sign：
                //>拼接字符串1
                //拼接appid=20180117000115918+q=apple+salt=1435660288+密钥=12345678
                //得到字符串1 =20180117000115918apple143566028812345678
                //>计算签名sign（对字符串1做md5加密，注意计算md5之前，串1必须为UTF-8编码）
                //sign=md5(20180117000115918apple143566028812345678)
                //sign=f89f9594663708c1605f3d736d01d2d4
                string sign = "20180117000115918" + fromTranslate + "1435660288zdOZfj0t3qKbnQOMSBWS";
                var md5 = new MD5CryptoServiceProvider();
            var result1 = Encoding.UTF8.GetBytes(sign);
            var output = md5.ComputeHash(result1);
               sign= BitConverter.ToString(output).Replace("-", "").ToLower();

              
               string url= string.Format("http://api.fanyi.baidu.com/api/trans/vip/translate?q={0}&from=en&to=zh&appid=20180117000115918&salt=1435660288&sign={1}",con,sign);
                //client_id为自己的api_id，q为翻译对象，from为翻译语言，to为翻译后语言
                var buffer = client.DownloadData(url);
                var  result = Encoding.UTF8.GetString(buffer);
                StringReader sr = new StringReader(result);
                JsonTextReader jsonReader = new JsonTextReader(sr); //引用Newtonsoft.Json 自带
                JsonSerializer serializer = new JsonSerializer();
                var r = serializer.Deserialize<TranClass>(jsonReader); //因为获取后的为json对象 ，实行转换
               return   r.Trans_result[0].dst;  //dst为翻译后的值
            }
            return con.ToUpper();
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            Bind();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string year = DateTime.Now.Year.ToString().Substring(0, 2);
            string ss = year + "320100001801141315580165".Substring(8, 6);
            DateTime dt = DateTime.ParseExact(ss, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
            string date = dt.ToString("u").Substring(0, 10).Replace("-", "_").Replace("/", "_");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox2.Text = gettt(textBox1.Text);
        }
    }


    public class TranClass
    {
        public string From { get; set; }
        public string To { get; set; }
        public List<Trans_result> Trans_result { get; set; }
    }
    public class Trans_result
    {
        public string src { get; set; }
        public string dst { get; set; }
    }
}
