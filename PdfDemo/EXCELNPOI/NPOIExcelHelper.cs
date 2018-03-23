using System.Data;
using System.Configuration;
using System.Web;
using System.IO;
using System.Text;
using NPOI;
using NPOI.HPSF;
using NPOI.HSSF;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.POIFS;
using NPOI.Util;
using System.Windows.Forms;
using NPOI.SS.Util;
using System.Net;
using System.Security.Cryptography;
using Newtonsoft.Json;
using System;

namespace EXCELNPOI
{
    public class NPOIExcelHelper
    {
        /// <summary>
        /// DataTable导出到Excel文件
        /// </summary>
        /// <param name="dtSource">源DataTable</param>
        /// <param name="strHeaderText">表头文本</param>
        /// <param name="strFileName">保存位置</param>
        public static void DataTableToExcel(DataTable dtSource, string strHeaderText, string strFileName)
        {
            using (MemoryStream ms = DataTableToExcel1(dtSource, strHeaderText))
            {
                using (FileStream fs = new FileStream(strFileName, FileMode.Create, FileAccess.Write))
                {
                    byte[] data = ms.ToArray();
                    fs.Write(data, 0, data.Length);
                    fs.Flush();
                }
            }
        }
        public static void DataTableToExcel2(DataTable dtSource, string strHeaderText, string strFileName)
        {
            using (MemoryStream ms = DataTableToExcel2(dtSource, strHeaderText))
            {
                using (FileStream fs = new FileStream(strFileName, FileMode.Create, FileAccess.Write))
                {
                    byte[] data = ms.ToArray();
                    fs.Write(data, 0, data.Length);
                    fs.Flush();
                }
            }
        }
        /// <summary>
        /// DataTable导出到Excel的MemoryStream
        /// </summary>
        /// <param name="dtSource">源DataTable</param>
        /// <param name="strHeaderText">表头文本</param>
        public static MemoryStream DataTableToExcel1(DataTable dtSource, string strHeaderText)
        {
            Helper helper = new Helper("Data Source=172.128.2.1/veims;User ID=zte;Password=zsfyqch;");
            HSSFWorkbook workbook = new HSSFWorkbook();
            HSSFSheet sheet = (HSSFSheet)workbook.CreateSheet();

            #region 右击文件 属性信息
            {
                DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
                dsi.Company = "NPOI";
                workbook.DocumentSummaryInformation = dsi;

                SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
                si.Author = "文件作者信息"; //填加xls文件作者信息
                si.ApplicationName = "创建程序信息"; //填加xls文件创建程序信息
                si.LastAuthor = "最后保存者信息"; //填加xls文件最后保存者信息
                si.Comments = "作者信息"; //填加xls文件作者信息
                si.Title = "标题信息"; //填加xls文件标题信息
                si.Subject = "主题信息";//填加文件主题信息
                si.CreateDateTime = System.DateTime.Now;
                workbook.SummaryInformation = si;
            }
            #endregion

            HSSFCellStyle dateStyle = (HSSFCellStyle)workbook.CreateCellStyle();
            HSSFDataFormat format = (HSSFDataFormat)workbook.CreateDataFormat();
            dateStyle.DataFormat = format.GetFormat("yyyy-mm-dd");

            //取得列宽
            int[] arrColWidth = new int[dtSource.Columns.Count];
            foreach (DataColumn item in dtSource.Columns)
            {
                arrColWidth[item.Ordinal] = Encoding.GetEncoding(936).GetBytes(item.ColumnName.ToString()).Length;
            }
            for (int i = 0; i < dtSource.Rows.Count; i++)
            {
                for (int j = 0; j < dtSource.Columns.Count; j++)
                {
                    int intTemp = Encoding.GetEncoding(936).GetBytes(dtSource.Rows[i][j].ToString()).Length;
                    if (intTemp > arrColWidth[j])
                    {
                        arrColWidth[j] = intTemp;
                    }
                }
            }
            int rowIndex = 0;
            foreach (DataRow row in dtSource.Rows)
            {
                #region 新建表，填充表头，填充列头，样式
                if (rowIndex == 65535 || rowIndex == 0)
                {
                    if (rowIndex != 0)
                    {
                        sheet = (HSSFSheet)workbook.CreateSheet();
                    }

                    #region 表头及样式
                    {
                        HSSFRow headerRow = (HSSFRow)sheet.CreateRow(0);
                        headerRow.HeightInPoints = 25;
                        headerRow.CreateCell(0).SetCellValue(strHeaderText);

                        HSSFCellStyle headStyle = (HSSFCellStyle)workbook.CreateCellStyle();
                        //  headStyle.Alignment = CellHorizontalAlignment.CENTER;
                        HSSFFont font = (HSSFFont)workbook.CreateFont();
                        font.FontHeightInPoints = 20;
                        font.Boldweight = 700;
                        headStyle.SetFont(font);
                        headerRow.GetCell(0).CellStyle = headStyle;
                        // sheet.AddMergedRegion(new Region(0, 0, 0, dtSource.Columns.Count - 1));
                        //headerRow.Dispose();
                    }
                    #endregion


                    #region 列头及样式
                    {
                        HSSFRow headerRow = (HSSFRow)sheet.CreateRow(1);
                        HSSFCellStyle headStyle = (HSSFCellStyle)workbook.CreateCellStyle();
                        //headStyle.Alignment = CellHorizontalAlignment.CENTER;
                        HSSFFont font = (HSSFFont)workbook.CreateFont();
                        font.FontHeightInPoints = 10;
                        font.Boldweight = 700;
                        headStyle.SetFont(font);
                        foreach (DataColumn column in dtSource.Columns)
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
                            headerRow.GetCell(column.Ordinal).CellStyle = headStyle;

                            //设置列宽
                            sheet.SetColumnWidth(column.Ordinal, (arrColWidth[column.Ordinal] + 1) * 256);
                        }
                        // headerRow.Dispose();
                    }
                    #endregion

                    rowIndex = 2;
                }
                #endregion


                #region 填充内容
                HSSFRow dataRow = (HSSFRow)sheet.CreateRow(rowIndex);
                string dtname = row[1].ToString();
                #region
                string sql=string.Format(@"select   cc.COMMENTS,  cC.COLUMN_NAME,  
CASE WHEN  c.data_type='VARCHAR2' THEN '字符串型C'
     WHEN  c.data_type='NUMBER' THEN '数值型N'
     WHEN  c.data_type='CHAR' THEN '字符串型C'
     WHEN  c.data_type='DATE' THEN '日期时间型T'
     WHEN  c.data_type='FLOAT' THEN '数值型N'
     WHEN  c.data_type='CLOB' THEN '长文本text'
     WHEN  c.data_type='NCHAR' THEN '字符串型C'
     WHEN  c.data_type='RAW' THEN '字符串型C'
     WHEN  c.data_type='NVARCHAR2' THEN '字符串型C'  END  data_type   
  ,
c.data_length
,'无条件共享' A1,
'用户数据校核、业务协同及大数据应用' A2,'共享平台方式'A3,'数据库'A4,'否'A5
 from user_tab_columns c
 LEFT JOIN 
 user_col_comments cc 
  ON cc.COLUMN_NAME=c.COLUMN_NAME AND CC.Table_Name='{0}' 
 where
   c.Table_Name='{0}' order by c.COLUMN_NAME asc", dtname.ToUpper());
                #endregion
                DataTable dd = helper.ExecuteDataSet(sql).Tables[0];
                int kk = 0;//列控制 0到16列、27、28列 全部合并单元格  
             
                foreach (DataColumn column in dtSource.Columns)
                {
                    //第一列合并第三行到第五行   0 1  2  3  4  
                    //sheet.AddMergedRegion(new CellRangeAddress(2, 4, 0, 0));

                    if (dd.Rows.Count > 0 && kk<17)
                    { 
                        // 0到16列全部合并单元格
                        // 第KK列合并第2行到第dd.Rows.Count行
                        sheet.AddMergedRegion(new CellRangeAddress(rowIndex , rowIndex + dd.Rows.Count , kk, kk));
                       
                    }
                    if (kk > 26 && kk < 29)
                    {
                        //0到16列全部合并单元格
                        sheet.AddMergedRegion(new CellRangeAddress(rowIndex , rowIndex + dd.Rows.Count , kk, kk));

                    }

                  
                    HSSFCell newCell ;
                    string drValue  ;
                    //if ((kk > 16 && kk < 27) || (kk > 26 && kk < 29))
                    //{
                    //    int f = 0;
                    //    foreach (DataRow row1 in dd.Rows)
                    //    {
                    //        rowIndex++;
                    //        foreach (DataColumn column1 in dd.Columns)
                    //        {
                    //            newCell = (HSSFCell)dataRow.CreateCell(kk+column1.Ordinal);
                    //            drValue = row1[column1].ToString();
                    //            switch (column1.DataType.ToString())
                    //            {
                    //                case "System.String"://字符串类型
                    //                    newCell.SetCellValue(drValue);
                    //                    break;
                    //                case "System.DateTime"://日期类型
                    //                    System.DateTime dateV;
                    //                    System.DateTime.TryParse(drValue, out dateV);
                    //                    newCell.SetCellValue(dateV);

                    //                    newCell.CellStyle = dateStyle;//格式化显示
                    //                    break;
                    //                case "System.Boolean"://布尔型
                    //                    bool boolV = false;
                    //                    bool.TryParse(drValue, out boolV);
                    //                    newCell.SetCellValue(boolV);
                    //                    break;
                    //                case "System.Int16"://整型
                    //                case "System.Int32":
                    //                case "System.Int64":
                    //                case "System.Byte":
                    //                    int intV = 0;
                    //                    int.TryParse(drValue, out intV);
                    //                    newCell.SetCellValue(intV);
                    //                    break;
                    //                case "System.Decimal"://浮点型
                    //                case "System.Double":
                    //                    double doubV = 0;
                    //                    double.TryParse(drValue, out doubV);
                    //                    newCell.SetCellValue(doubV);
                    //                    break;
                    //                case "System.DBNull"://空值处理
                    //                    newCell.SetCellValue("");
                    //                    break;
                    //                default:
                    //                    newCell.SetCellValue("");
                    //                    break;
                    //            }
                    //        }
                    //        f++;
                    //    }
                    //}
                    //else
                    {
                      
                          newCell = (HSSFCell)dataRow.CreateCell(column.Ordinal);
                          drValue = row[column].ToString();
                        switch (column.DataType.ToString())
                        {
                            case "System.String"://字符串类型
                                newCell.SetCellValue(drValue);
                                break;
                            case "System.DateTime"://日期类型
                                System.DateTime dateV;
                                System.DateTime.TryParse(drValue, out dateV);
                                newCell.SetCellValue(dateV);

                                newCell.CellStyle = dateStyle;//格式化显示
                                break;
                            case "System.Boolean"://布尔型
                                bool boolV = false;
                                bool.TryParse(drValue, out boolV);
                                newCell.SetCellValue(boolV);
                                break;
                            case "System.Int16"://整型
                            case "System.Int32":
                            case "System.Int64":
                            case "System.Byte":
                                int intV = 0;
                                int.TryParse(drValue, out intV);
                                newCell.SetCellValue(intV);
                                break;
                            case "System.Decimal"://浮点型
                            case "System.Double":
                                double doubV = 0;
                                double.TryParse(drValue, out doubV);
                                newCell.SetCellValue(doubV);
                                break;
                            case "System.DBNull"://空值处理
                                newCell.SetCellValue("");
                                break;
                            default:
                                newCell.SetCellValue("");
                                break;
                        }
                    }
                    kk++;
                }
                #endregion
                rowIndex += dd.Rows.Count;
                rowIndex++;
            }
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;

                // sheet.Dispose();
                //workbook.Dispose();//一般只用写这一个就OK了，他会遍历并释放所有资源，但当前版本有问题所以只释放sheet
                return ms;
            }
        }

        private TranClass tranClass = new TranClass();
        public static string getval(string con, string remark, char split)
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
        private static string gettt(string con)
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
                sign = BitConverter.ToString(output).Replace("-", "").ToLower();


                string url = string.Format("http://api.fanyi.baidu.com/api/trans/vip/translate?q={0}&from=en&to=zh&appid=20180117000115918&salt=1435660288&sign={1}", con, sign);
                //client_id为自己的api_id，q为翻译对象，from为翻译语言，to为翻译后语言
                var buffer = client.DownloadData(url);
                var result = Encoding.UTF8.GetString(buffer);
                StringReader sr = new StringReader(result);
                JsonTextReader jsonReader = new JsonTextReader(sr); //引用Newtonsoft.Json 自带
                JsonSerializer serializer = new JsonSerializer();
                var r = serializer.Deserialize<TranClass>(jsonReader); //因为获取后的为json对象 ，实行转换
                return r.Trans_result[0].dst;  //dst为翻译后的值
            }
            return con.ToUpper();
        }



        public static MemoryStream DataTableToExcel2(DataTable dtSource, string strHeaderText)
        {
            Helper helper = new Helper("Data Source=172.128.2.1/veims;User ID=zte;Password=zsfyqch;");
            HSSFWorkbook workbook = new HSSFWorkbook();
            HSSFSheet sheet = (HSSFSheet)workbook.CreateSheet();

            #region 右击文件 属性信息
            {
                DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
                dsi.Company = "NPOI";
                workbook.DocumentSummaryInformation = dsi;

                SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
                si.Author = "文件作者信息"; //填加xls文件作者信息
                si.ApplicationName = "创建程序信息"; //填加xls文件创建程序信息
                si.LastAuthor = "最后保存者信息"; //填加xls文件最后保存者信息
                si.Comments = "作者信息"; //填加xls文件作者信息
                si.Title = "标题信息"; //填加xls文件标题信息
                si.Subject = "主题信息";//填加文件主题信息
                si.CreateDateTime = System.DateTime.Now;
                workbook.SummaryInformation = si;
            }
            #endregion

            HSSFCellStyle dateStyle = (HSSFCellStyle)workbook.CreateCellStyle();
            HSSFDataFormat format = (HSSFDataFormat)workbook.CreateDataFormat();
            dateStyle.DataFormat = format.GetFormat("yyyy-mm-dd");

            //取得列宽
            int[] arrColWidth = new int[dtSource.Columns.Count];
            foreach (DataColumn item in dtSource.Columns)
            {
                arrColWidth[item.Ordinal] = Encoding.GetEncoding(936).GetBytes(item.ColumnName.ToString()).Length;
            }
            for (int i = 0; i < dtSource.Rows.Count; i++)
            {
                for (int j = 0; j < dtSource.Columns.Count; j++)
                {
                    int intTemp = Encoding.GetEncoding(936).GetBytes(dtSource.Rows[i][j].ToString()).Length;
                    if (intTemp > arrColWidth[j])
                    {
                        arrColWidth[j] = intTemp;
                    }
                }
            }
            int rowIndex = 0;
            foreach (DataRow row in dtSource.Rows)
            {
                #region 新建表，填充表头，填充列头，样式
                if (rowIndex == 65535 || rowIndex == 0)
                {
                    if (rowIndex != 0)
                    {
                        sheet = (HSSFSheet)workbook.CreateSheet();
                    }

                    #region 表头及样式
                    {
                        HSSFRow headerRow = (HSSFRow)sheet.CreateRow(0);
                        headerRow.HeightInPoints = 25;
                        headerRow.CreateCell(0).SetCellValue(strHeaderText);

                        HSSFCellStyle headStyle = (HSSFCellStyle)workbook.CreateCellStyle();
                        //  headStyle.Alignment = CellHorizontalAlignment.CENTER;
                        HSSFFont font = (HSSFFont)workbook.CreateFont();
                        font.FontHeightInPoints = 20;
                        font.Boldweight = 700;
                        headStyle.SetFont(font);
                        headerRow.GetCell(0).CellStyle = headStyle;
                        // sheet.AddMergedRegion(new Region(0, 0, 0, dtSource.Columns.Count - 1));
                        //headerRow.Dispose();
                    }
                    #endregion


                    #region 列头及样式
                    {
                        HSSFRow headerRow = (HSSFRow)sheet.CreateRow(1);
                        HSSFCellStyle headStyle = (HSSFCellStyle)workbook.CreateCellStyle();
                        //headStyle.Alignment = CellHorizontalAlignment.CENTER;
                        HSSFFont font = (HSSFFont)workbook.CreateFont();
                        font.FontHeightInPoints = 10;
                        font.Boldweight = 700;
                        headStyle.SetFont(font);
                        foreach (DataColumn column in dtSource.Columns)
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
                            headerRow.GetCell(column.Ordinal).CellStyle = headStyle;

                            //设置列宽
                            sheet.SetColumnWidth(column.Ordinal, (arrColWidth[column.Ordinal] + 1) * 256);
                        }
                        // headerRow.Dispose();
                    }
                    #endregion

                    rowIndex = 2;
                }
                #endregion


                #region 填充内容
                
                string dtname = row[1].ToString();
                #region
                string sql = string.Format(@"select  CASE WHEN cc.COMMENTS is null then cC.COLUMN_NAME else  cc.COMMENTS end COMMENTS,   cC.COLUMN_NAME,  
CASE WHEN  c.data_type='VARCHAR2' THEN '字符串型C'
     WHEN  c.data_type='NUMBER' THEN '数值型N'
     WHEN  c.data_type='CHAR' THEN '字符串型C'
     WHEN  c.data_type='DATE' THEN '日期时间型T'
     WHEN  c.data_type='FLOAT' THEN '数值型N'
     WHEN  c.data_type='CLOB' THEN '长文本text'
     WHEN  c.data_type='NCHAR' THEN '字符串型C'
     WHEN  c.data_type='RAW' THEN '字符串型C'
     WHEN  c.data_type='NVARCHAR2' THEN '字符串型C'  END  data_type   
  ,
c.data_length
,'无条件共享' A1,
'用户数据校核、业务协同及大数据应用' A2,'共享平台方式'A3,'数据库'A4,'否'A5
 from user_tab_columns c
 LEFT JOIN 
 user_col_comments cc 
  ON cc.COLUMN_NAME=c.COLUMN_NAME AND CC.Table_Name='{0}' 
 where
   c.Table_Name='{0}' order by c.COLUMN_NAME asc", dtname.ToUpper());
                #endregion
                DataTable dd = helper.ExecuteDataSet(sql).Tables[0];

                for (int i = 0; i < dd.Rows.Count; i++)
                {
                    dd.Rows[i]["COMMENTS"] = getval(dd.Rows[i]["COMMENTS"].ToString().ToUpper(), "字段", '_'); 
                
                }

                foreach (DataRow row1 in dd.Rows)
                {



                    HSSFRow dataRow = (HSSFRow)sheet.CreateRow(rowIndex);
                    foreach (DataColumn column in dd.Columns)
                    {

                       

                        HSSFCell newCell;
                        string drValue;



                        newCell = (HSSFCell)dataRow.CreateCell(column.Ordinal);
                        drValue = row1[column].ToString();
                        switch (column.DataType.ToString())
                        {
                            case "System.String"://字符串类型
                                newCell.SetCellValue(drValue);
                                break;
                            case "System.DateTime"://日期类型
                                System.DateTime dateV;
                                System.DateTime.TryParse(drValue, out dateV);
                                newCell.SetCellValue(dateV);

                                newCell.CellStyle = dateStyle;//格式化显示
                                break;
                            case "System.Boolean"://布尔型
                                bool boolV = false;
                                bool.TryParse(drValue, out boolV);
                                newCell.SetCellValue(boolV);
                                break;
                            case "System.Int16"://整型
                            case "System.Int32":
                            case "System.Int64":
                            case "System.Byte":
                                int intV = 0;
                                int.TryParse(drValue, out intV);
                                newCell.SetCellValue(intV);
                                break;
                            case "System.Decimal"://浮点型
                            case "System.Double":
                                double doubV = 0;
                                double.TryParse(drValue, out doubV);
                                newCell.SetCellValue(doubV);
                                break;
                            case "System.DBNull"://空值处理
                                newCell.SetCellValue("");
                                break;
                            default:
                                newCell.SetCellValue("");
                                break;
                        }

                       
                    }
                    rowIndex++;
                }
                #endregion
                rowIndex++;
              
            }
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;

                // sheet.Dispose();
                //workbook.Dispose();//一般只用写这一个就OK了，他会遍历并释放所有资源，但当前版本有问题所以只释放sheet
                return ms;
            }
        }














        /// <summary>
        /// DataGridView导出到Excel文件
        /// </summary>
        /// <param name="dtSource">源DataTGridview</param>
        /// <param name="strHeaderText">表头文本</param>
        /// <param name="strFileName">保存位置</param>
        public static void DataGridViewToExcel(DataGridView myDgv, string strHeaderText, string strFileName)
        {
            using (MemoryStream ms = DataGridViewToExcel(myDgv, strHeaderText))
            {
                using (FileStream fs = new FileStream(strFileName, FileMode.Create, FileAccess.Write))
                {
                    byte[] data = ms.ToArray();
                    fs.Write(data, 0, data.Length);
                    fs.Flush();
                }
            }
        }

        /// <summary>
        /// DataTable导出到Excel的MemoryStream
        /// </summary>
        /// <param name="dtSource">源DataTable</param>
        /// <param name="strHeaderText">表头文本</param>
        public static MemoryStream DataTableToExcel(DataTable dtSource, string strHeaderText)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            HSSFSheet sheet = (HSSFSheet)workbook.CreateSheet();

            #region 右击文件 属性信息
            {
                DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
                dsi.Company = "NPOI";
                workbook.DocumentSummaryInformation = dsi;

                SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
                si.Author = "文件作者信息"; //填加xls文件作者信息
                si.ApplicationName = "创建程序信息"; //填加xls文件创建程序信息
                si.LastAuthor = "最后保存者信息"; //填加xls文件最后保存者信息
                si.Comments = "作者信息"; //填加xls文件作者信息
                si.Title = "标题信息"; //填加xls文件标题信息
                si.Subject = "主题信息";//填加文件主题信息
                si.CreateDateTime = System.DateTime.Now;
                workbook.SummaryInformation = si;
            }
            #endregion

            HSSFCellStyle dateStyle = (HSSFCellStyle)workbook.CreateCellStyle();
            HSSFDataFormat format = (HSSFDataFormat)workbook.CreateDataFormat();
            dateStyle.DataFormat = format.GetFormat("yyyy-mm-dd");

            //取得列宽
            int[] arrColWidth = new int[dtSource.Columns.Count];
            foreach (DataColumn item in dtSource.Columns)
            {
                arrColWidth[item.Ordinal] = Encoding.GetEncoding(936).GetBytes(item.ColumnName.ToString()).Length;
            }
            for (int i = 0; i < dtSource.Rows.Count; i++)
            {
                for (int j = 0; j < dtSource.Columns.Count; j++)
                {
                    int intTemp = Encoding.GetEncoding(936).GetBytes(dtSource.Rows[i][j].ToString()).Length;
                    if (intTemp > arrColWidth[j])
                    {
                        arrColWidth[j] = intTemp;
                    }
                }
            }
            int rowIndex = 0;
            foreach (DataRow row in dtSource.Rows)
            {
                #region 新建表，填充表头，填充列头，样式
                if (rowIndex == 65535 || rowIndex == 0)
                {
                    if (rowIndex != 0)
                    {
                        sheet = (HSSFSheet)workbook.CreateSheet();
                    }

                    #region 表头及样式
                    {
                        HSSFRow headerRow = (HSSFRow)sheet.CreateRow(0);
                        headerRow.HeightInPoints = 25;
                        headerRow.CreateCell(0).SetCellValue(strHeaderText);

                        HSSFCellStyle headStyle = (HSSFCellStyle)workbook.CreateCellStyle();
                        //  headStyle.Alignment = CellHorizontalAlignment.CENTER;
                        HSSFFont font = (HSSFFont)workbook.CreateFont();
                        font.FontHeightInPoints = 20;
                        font.Boldweight = 700;
                        headStyle.SetFont(font);
                        headerRow.GetCell(0).CellStyle = headStyle;
                        // sheet.AddMergedRegion(new Region(0, 0, 0, dtSource.Columns.Count - 1));
                        //headerRow.Dispose();
                    }
                    #endregion


                    #region 列头及样式
                    {
                        HSSFRow headerRow = (HSSFRow)sheet.CreateRow(1);
                        HSSFCellStyle headStyle = (HSSFCellStyle)workbook.CreateCellStyle();
                        //headStyle.Alignment = CellHorizontalAlignment.CENTER;
                        HSSFFont font = (HSSFFont)workbook.CreateFont();
                        font.FontHeightInPoints = 10;
                        font.Boldweight = 700;
                        headStyle.SetFont(font);
                        foreach (DataColumn column in dtSource.Columns)
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
                            headerRow.GetCell(column.Ordinal).CellStyle = headStyle;

                            //设置列宽
                            sheet.SetColumnWidth(column.Ordinal, (arrColWidth[column.Ordinal] + 1) * 256);
                        }
                        // headerRow.Dispose();
                    }
                    #endregion

                    rowIndex = 2;
                }
                #endregion


                #region 填充内容
                HSSFRow dataRow = (HSSFRow)sheet.CreateRow(rowIndex);
                foreach (DataColumn column in dtSource.Columns)
                {
                    HSSFCell newCell = (HSSFCell)dataRow.CreateCell(column.Ordinal);

                    string drValue = row[column].ToString();

                    switch (column.DataType.ToString())
                    {
                        case "System.String"://字符串类型
                            newCell.SetCellValue(drValue);
                            break;
                        case "System.DateTime"://日期类型
                            System.DateTime dateV;
                            System.DateTime.TryParse(drValue, out dateV);
                            newCell.SetCellValue(dateV);

                            newCell.CellStyle = dateStyle;//格式化显示
                            break;
                        case "System.Boolean"://布尔型
                            bool boolV = false;
                            bool.TryParse(drValue, out boolV);
                            newCell.SetCellValue(boolV);
                            break;
                        case "System.Int16"://整型
                        case "System.Int32":
                        case "System.Int64":
                        case "System.Byte":
                            int intV = 0;
                            int.TryParse(drValue, out intV);
                            newCell.SetCellValue(intV);
                            break;
                        case "System.Decimal"://浮点型
                        case "System.Double":
                            double doubV = 0;
                            double.TryParse(drValue, out doubV);
                            newCell.SetCellValue(doubV);
                            break;
                        case "System.DBNull"://空值处理
                            newCell.SetCellValue("");
                            break;
                        default:
                            newCell.SetCellValue("");
                            break;
                    }

                }
                #endregion

                rowIndex++;
            }
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;

               // sheet.Dispose();
                //workbook.Dispose();//一般只用写这一个就OK了，他会遍历并释放所有资源，但当前版本有问题所以只释放sheet
                return ms;
            }
        }
        /// <summary>
        /// DataTable导出到Excel的MemoryStream
        /// </summary>
        /// <param name="myDgv">源DataTable</param>
        /// <param name="strHeaderText">表头文本</param>
        public static MemoryStream DataGridViewToExcel(DataGridView myDgv, string strHeaderText)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            HSSFSheet sheet = (HSSFSheet)workbook.CreateSheet();

            #region 右击文件 属性信息
            {
                DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
                dsi.Company = "NPOI";
                workbook.DocumentSummaryInformation = dsi;

                SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
                si.Author = "文件作者信息"; //填加xls文件作者信息
                si.ApplicationName = "创建程序信息"; //填加xls文件创建程序信息
                si.LastAuthor = "最后保存者信息"; //填加xls文件最后保存者信息
                si.Comments = "作者信息"; //填加xls文件作者信息
                si.Title = "标题信息"; //填加xls文件标题信息
                si.Subject = "主题信息";//填加文件主题信息
                si.CreateDateTime = System.DateTime.Now;
                workbook.SummaryInformation = si;
            }
            #endregion

            HSSFCellStyle dateStyle = (HSSFCellStyle)workbook.CreateCellStyle();
            HSSFDataFormat format = (HSSFDataFormat)workbook.CreateDataFormat();
            dateStyle.DataFormat = format.GetFormat("yyyy-mm-dd");

            //取得列宽
            int[] arrColWidth = new int[myDgv.Columns.Count];
            foreach (DataGridViewColumn item in myDgv.Columns)
            {
                arrColWidth[item.Index] = Encoding.GetEncoding(936).GetBytes(item.HeaderText.ToString()).Length;
            }
            for (int i = 0; i < myDgv.Rows.Count; i++)
            {
                for (int j = 0; j < myDgv.Columns.Count; j++)
                {
                    int intTemp = Encoding.GetEncoding(936).GetBytes(myDgv.Rows[i].Cells[j].ToString()).Length;
                    if (intTemp > arrColWidth[j])
                    {
                        arrColWidth[j] = intTemp;
                    }
                }
            }
            int rowIndex = 0;
            foreach (DataGridViewRow row in myDgv.Rows)
            {
                #region 新建表，填充表头，填充列头，样式
                if (rowIndex == 65535 || rowIndex == 0)
                {
                    if (rowIndex != 0)
                    {
                        sheet = (HSSFSheet)workbook.CreateSheet();
                    }

                    #region 表头及样式
                    {
                        HSSFRow headerRow = (HSSFRow)sheet.CreateRow(0);
                        headerRow.HeightInPoints = 25;
                        headerRow.CreateCell(0).SetCellValue(strHeaderText);

                        HSSFCellStyle headStyle = (HSSFCellStyle)workbook.CreateCellStyle();
                        //  headStyle.Alignment = CellHorizontalAlignment.CENTER;
                        HSSFFont font = (HSSFFont)workbook.CreateFont();
                        font.FontHeightInPoints = 20;
                        font.Boldweight = 700;
                        headStyle.SetFont(font);
                        headerRow.GetCell(0).CellStyle = headStyle;
                        // sheet.AddMergedRegion(new Region(0, 0, 0, dtSource.Columns.Count - 1));
                        //headerRow.Dispose();
                    }
                    #endregion


                    #region 列头及样式
                    {
                        HSSFRow headerRow = (HSSFRow)sheet.CreateRow(1);
                        HSSFCellStyle headStyle = (HSSFCellStyle)workbook.CreateCellStyle();
                        //headStyle.Alignment = CellHorizontalAlignment.CENTER;
                        HSSFFont font = (HSSFFont)workbook.CreateFont();
                        font.FontHeightInPoints = 10;
                        font.Boldweight = 700;
                        headStyle.SetFont(font);
                        foreach (DataGridViewColumn column in myDgv.Columns)
                        {
                            headerRow.CreateCell(column.Index).SetCellValue(column.HeaderText);
                            headerRow.GetCell(column.Index).CellStyle = headStyle;

                            //设置列宽
                            sheet.SetColumnWidth(column.Index, (arrColWidth[column.Index] + 1) * 256);
                        }
                        // headerRow.Dispose();
                    }
                    #endregion

                    rowIndex = 2;
                }
                #endregion


                #region 填充内容
                HSSFRow dataRow = (HSSFRow)sheet.CreateRow(rowIndex);
                if (row.Index > 0)
                {
                    foreach (DataGridViewColumn column in myDgv.Columns)
                    {
                        HSSFCell newCell = (HSSFCell)dataRow.CreateCell(column.Index);

                        string drValue = myDgv[column.Index, row.Index - 1].Value.ToString();

                        switch (column.ValueType.ToString())
                        {
                            case "System.String"://字符串类型
                                newCell.SetCellValue(drValue);
                                break;
                            case "System.DateTime"://日期类型
                                System.DateTime dateV;
                                System.DateTime.TryParse(drValue, out dateV);
                                newCell.SetCellValue(dateV);

                                newCell.CellStyle = dateStyle;//格式化显示
                                break;
                            case "System.Boolean"://布尔型
                                bool boolV = false;
                                bool.TryParse(drValue, out boolV);
                                newCell.SetCellValue(boolV);
                                break;
                            case "System.Int16"://整型
                            case "System.Int32":
                            case "System.Int64":
                            case "System.Byte":
                                int intV = 0;
                                int.TryParse(drValue, out intV);
                                newCell.SetCellValue(intV);
                                break;
                            case "System.Decimal"://浮点型
                            case "System.Double":
                                double doubV = 0;
                                double.TryParse(drValue, out doubV);
                                newCell.SetCellValue(doubV);
                                break;
                            case "System.DBNull"://空值处理
                                newCell.SetCellValue("");
                                break;
                            default:
                                newCell.SetCellValue("");
                                break;
                        }

                    }
                }
                else
                { rowIndex--; }
                #endregion

                rowIndex++;
            }
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;

                //sheet.Dispose();
                //workbook.Dispose();//一般只用写这一个就OK了，他会遍历并释放所有资源，但当前版本有问题所以只释放sheet
                return ms;
            }
        }



        /// <summary>读取excel
        /// 默认第一行为标头
        /// </summary>
        /// <param name="strFileName">excel文档路径</param>
        /// <returns></returns>
        public static DataTable Import(string strFileName)
        {
            DataTable dt = new DataTable();

            HSSFWorkbook hssfworkbook;
            using (FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
            {
                hssfworkbook = new HSSFWorkbook(file);
            }
            HSSFSheet sheet = (HSSFSheet)hssfworkbook.GetSheetAt(0);
            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

            HSSFRow headerRow = (HSSFRow)sheet.GetRow(0);
            int cellCount = headerRow.LastCellNum;

            for (int j = 0; j < cellCount; j++)
            {
                HSSFCell cell = (HSSFCell)headerRow.GetCell(j);
                dt.Columns.Add(cell.ToString());
            }

            for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
            {
                HSSFRow row = (HSSFRow)sheet.GetRow(i);
                DataRow dataRow = dt.NewRow();

                for (int j = row.FirstCellNum; j < cellCount; j++)
                {
                    if (row.GetCell(j) != null)
                        dataRow[j] = row.GetCell(j).ToString();
                }

                dt.Rows.Add(dataRow);
            }
            return dt;
        }
    }
}
