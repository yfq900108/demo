 
using System;
using System.Collections.Generic;


using System.Windows.Forms;

namespace PdfDemo2._0
{
    static class Program
    {

        /// <summary>
        /// PDF信息打开状态   true  没打开   false  打开   打开后不在重复打开
        /// </summary>
        public static bool IsPDFOK = true;
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form3());
        }
    }
}
