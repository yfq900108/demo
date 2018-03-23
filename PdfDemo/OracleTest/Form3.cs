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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
          // FTPHelper ftp=new FTPHelper ();
           //  ftp.ExFTP("administrator", "654321", "ftp://172.128.74.195:2121/", @"D:\", "Update");

            FTP_Class ss = new FTP_Class();
           // ss.Connecttest("172.128.74.195:2121", "administrator", "654321");
            ss.FtpUpDown("172.128.74.186:2121", "administrator", "654321");
            string errorinfo="";
            string path = DateTime.Now.ToShortDateString().Replace("-", "_").Replace("/", "_");
            ss.MakeDir(path);

            string[] infos = ss.GetDirDetails(@"D:\Update\" + path,"png"); //获取当前目录下的所有文件和文件夹  

            for (int i = 0; i < infos.Length; i++)
            {
                //  UpLoadFile(dir + infos[0][i], ftpPath + dirName + @"/" + infos[0][i], ftpPath);
                //UploadDirectory(dir, ftpPath + dirName + @"/", infos[1][i]);
                ss.Upload(@"D:\Update\" + path + "\\" + infos[i], "172.128.74.186:2121\\" + path, out errorinfo);
            }
          
        }
    }
}
