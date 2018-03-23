using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Runtime.InteropServices;
using System.Diagnostics;


namespace ColorStatistics
{
    public partial class FrmTest : Form
    {
        public FrmTest()
        {
            InitializeComponent();
        }

        DataTable dt = new DataTable();
        List<Statistics.MajorColor> MC;
        int PixelAmount = 0;

        private void CmdOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.FilterIndex = 4;
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Thumb.Image =  (Bitmap)Bitmap.FromFile(openFileDialog.FileName);
              
            }
        }
        public static Color IntToColor(int color)
        {
            int R = color & 255;
            int G = (color & 65280) / 256;
            int B = (color & 16711680) / 65536;
            return Color.FromArgb(255, R, G, B);
        }
        public static string  IntToColorValue(int color)
        {
            int R = color & 255;
            int G = (color & 65280) / 256;
            int B = (color & 16711680) / 65536;
            return "(" + R.ToString() + "," + G.ToString() + "," + B.ToString()+")";
        }
         public static string  IntToColorValueCC(int color)
        {
            int R = color & 255;
            int G = (color & 65280) / 256;
            int B = (color & 16711680) / 65536;
            //（0，0，x） x=255/graylevel*i
             ColorRGB RGB=new ColorRGB (R,G,B);
            ColorHSV HSV= ColorHelper.RgbToHsv(RGB);
            return "(" + (R * 0.299 + G * 0.587 + B * 0.114) + ")   " + "(" + (R - G) + ")(" + (R - B) + ")(" + (G - B) + ")    " + "(" + (HSV.V) + ")  (" + (HSV.S) + ")       (" + (HSV.H) + ")    ";
        }

         public static string IntToColorValueCCC(int color)
         {
             int R = color & 255;
             int G = (color & 65280) / 256;
             int B = (color & 16711680) / 65536;
             //（0，0，x） x=255/graylevel*i
             ColorRGB RGB = new ColorRGB(R, G, B);
             ColorHSV HSV = ColorHelper.RgbToHsv(RGB);
             return "" + (R * 0.299 + G * 0.587 + B * 0.114) + "," + "" + (R - G) + "," + (R - B) + "," + (G - B) + "," + "" + (HSV.V) + "," + (HSV.S) + "," + (HSV.H) + "";
         }
        
        private void PicR_Click(object sender, EventArgs e)
        {

        }

        private void PicR_Paint(object sender, PaintEventArgs e)
        {
            if (MC != null)
            {
                e.Graphics.Clear(PicR.BackColor);
                Font font = new Font("宋体", 9f);
                SolidBrush B = new SolidBrush(Color.Black);
                e.Graphics.DrawString("      颜色        ", font, B, new PointF(0, 0));
                e.Graphics.DrawString("     百分比      ", font, B, new PointF(120, 0));
                e.Graphics.DrawString("数量", font, B, new PointF(250, 0));
                e.Graphics.DrawString("   RGB   ", font, B, new PointF(300, 0));
                e.Graphics.DrawString("   RGB灰度值 R-G  R-B G-B         饱和度    明度        色相240-360°对应蓝色", font, B, new PointF(450, 0));
                //H:色调 0°对应红色， 120°对应绿色， 240°对应蓝色---对应不同的颜色 取值范围0~360度
                //S:饱和度  比若说:红色的纯度，越白纯度越低，取值范围0~1
                //V:亮度  比如你穿了一件红色衣服 在白天亮度较高(0~255之间)傍晚或者黄昏就比较低(0~255之间)，即多少的光照上去反射出来被看见，取值范围0~255
                B.Dispose();



             

                for (int i = 0; i < MC.Count; i++)
                {
                    B = new SolidBrush(IntToColor(MC[i].Color));
                    e.Graphics.FillRectangle(B, new Rectangle(0, (i + 1) * 20, 100, 15));
                    e.Graphics.DrawString(((double)MC[i].Amount / PixelAmount).ToString(), font, B, new PointF(120, (i + 1) * 20 + 3));
                    e.Graphics.DrawString(MC[i].Amount.ToString(), font, B, new PointF(250, (i + 1) * 20 + 3));
                    e.Graphics.DrawString(IntToColorValue(MC[i].Color), font, B, new PointF(300, (i + 1) * 20 + 3));
                    e.Graphics.DrawString(IntToColorValueCC(MC[i].Color), font, B, new PointF(450, (i + 1) * 20 + 3));
                    B.Dispose();
                }
              
                 
                font.Dispose();



                dt = new DataTable();
                DataColumn dc = new DataColumn("a");
                dt.Columns.Add(dc); 
                dc = new DataColumn("颜色");
                dt.Columns.Add(dc);
                dc = new DataColumn("百分比");
                dt.Columns.Add(dc);
                dc = new DataColumn("数量");
                dt.Columns.Add(dc);
                dc = new DataColumn("RGB");
                dt.Columns.Add(dc);
                dc = new DataColumn("RGB灰度值");
                dt.Columns.Add(dc);
                dc = new DataColumn("R减G");
                dt.Columns.Add(dc);
                dc = new DataColumn("R减B");
                dt.Columns.Add(dc);
                dc = new DataColumn("G减B");
                dt.Columns.Add(dc);
                dc = new DataColumn("饱和度");
                dt.Columns.Add(dc);
                dc = new DataColumn("明度");
                dt.Columns.Add(dc);
                dc = new DataColumn("色相");
                dt.Columns.Add(dc);
                for (int i = 0; i < MC.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["a"] = i;
                    dr["颜色"] = IntToColor(MC[i].Color);
                    dr["百分比"] = (double)MC[i].Amount / PixelAmount;
                    dr["数量"] = MC[i].Amount;
                    dr["RGB"] = IntToColorValue(MC[i].Color);
                    string[] DD = IntToColorValueCCC(MC[i].Color).Split(',');
                    dr["RGB灰度值"] = DD[0];
                    dr["R减G"] = DD[1];
                    dr["R减B"] = DD[2];
                    dr["G减B"] = DD[3];
                    dr["饱和度"] = DD[4];
                    dr["明度"] = DD[5];
                    dr["色相"] = DD[6];
                    if (Convert.ToInt32(DD[1]) < 0 && Convert.ToInt32(DD[2]) < 0 && Convert.ToInt32(DD[3]) < 0)
                    { 
                    dt.Rows.Add(dr);
                    }
                }
                dt.DefaultView.Sort = "色相 desc";

                dt = dt.DefaultView.ToTable();
                dataGridView1.DataSource = dt ;
              label1.Text=  BlueScore(dt).ToString();
                //dataGridView1.Sort(dataGridView1.Columns["色相"], ListSortDirection.Descending);
            }
        }
        /// <summary>
        /// 计算[蓝天值]
        /// </summary>
        /// <param name="ds">数据源</param>
        /// <returns>返回 1-100 的整数</returns>
        public int BlueScore(DataTable t)
        {
            int Score = 0;

            //临时数组；
            string[] TmpArray;

            //R\G\B总合；
            int Sum_R = 0;
            int Sum_G = 0;
            int Sum_B = 0;

            //R\G\B均值；
            int AVG_R = 0;
            int AVG_G = 0;
            int AVG_B = 0;

            //饱和度总合\均值； 
            int Sum_Saturation = 0;
            int AVG_Saturation = 0;

            //5种分值
            int Score_R, Score_G, Score_B, Score_Saturation;

            try
            {
                #region ...
                //01、一共多少条？
                int Total = t.Rows.Count;
                //循环1次；
                foreach (DataRow r in t.Rows)
                {
                    try
                    {
                        TmpArray = r["RGB"].ToString().Replace("(", "").Replace(")", "").Split(',');
                        Sum_R += Int32.Parse(TmpArray[0]);
                        Sum_G += Int32.Parse(TmpArray[1]);
                        Sum_B += Int32.Parse(TmpArray[2]);
                        Sum_Saturation += Int32.Parse(r["饱和度"].ToString());
                    }
                    catch
                    {

                    }
                }

                //02、求RGB均值；
                // * R均值；
                AVG_R = Sum_R / Total;
                // * G均值；
                AVG_G = Sum_G / Total;
                // * B均值；
                AVG_B = Sum_B / Total;

                //03、给[RGB]均值打分（1-40分）
                // * R均值；
                //   15 * ( ( R - 128 ) / 128 )
                Score_R = Convert.ToInt32(15f * (AVG_R % 50 / 50f));
                // * G均值；
                //   15 * ( ( G - 128 ) / 128 )
                //Score_G = Convert.ToInt32(15f * (AVG_G / 50f));
                //Score_G = Convert.ToInt32(15f * (Math.Abs(AVG_G - 128f) / 128f));
                Score_G = Convert.ToInt32(15f * (Convert.ToDouble(AVG_R) / Convert.ToDouble(AVG_G)));
                // * B均值；（1-20分）
                //   30 * ( ( B - 128 ) / 128 )
                Score_B = Convert.ToInt32(30f * (Math.Abs(AVG_B - 128f) / 128f));

                //04、求[饱和度]均值；
                AVG_Saturation = Sum_Saturation / Total;
                //05、给[饱和度]均值打分（1-30分）
                // 30 * ( ( [饱和度] - 128 ) / 128 )
                Score_Saturation = Convert.ToInt32(40f * (Math.Abs(AVG_Saturation - 128f) / 128f));
                //06、将 [RGB]得分 + [饱和度]得分相加；
                Score = Score_R + Score_G + Score_B + Score_Saturation;

                #endregion

                if((Score + Score * 0.1) < 100)
                {
                    Score += Convert.ToInt32(Score * 0.1);
                }
            }
            catch
            {
            }

            return Score;
        }

        private void CmdDeal_Click(object sender, EventArgs e)
        {
           
            if (Thumb.Image !=null ) 
            {
                Stopwatch Sw = new Stopwatch();
                Sw.Start();
                MC = Statistics.PrincipalColorAnalysis((Bitmap)Thumb.Image,SliderColorAmount.Value, SliderDelta.Value);
                Sw.Stop();
                LblStatus.Text   = "计算主成分用时: "  +Sw.ElapsedMilliseconds.ToString() + " 毫秒";
                PixelAmount = Thumb.Image.Width * Thumb.Image.Height;
                PicR.Refresh();
            }
        }

        private void SliderColorAmount_Scroll(object sender, EventArgs e)
        {
            LblAmount.Text = SliderColorAmount.Value.ToString();
        }

        private void SliderDelta_Scroll(object sender, EventArgs e)
        {
            LblDelta.Text = SliderDelta.Value.ToString();
        }

        private void FrmTest_Load(object sender, EventArgs e)
        {
            CmdDeal_Click(sender,e);
        }










        #region RGB / HSV / HSL 颜色模型类
        /// <summary>
        /// 类      名：ColorHSL
        /// 功      能：H 色相 \ S 饱和度(纯度) \ L 亮度 颜色模型 
        /// 日      期：2015-02-08
        /// 修      改：2015-03-20
        /// 作      者：ls9512
        /// </summary>
        public class ColorHSL
        {
            public ColorHSL(int h, int s, int l)
            {
                this._h = h;
                this._s = s;
                this._l = l;
            }

            private int _h;
            private int _s;
            private int _l;

            /// <summary>
            /// 色相
            /// </summary>
            public int H
            {
                get { return this._h; }
                set
                {
                    this._h = value;
                    this._h = this._h > 360 ? 360 : this._h;
                    this._h = this._h < 0 ? 0 : this._h;
                }
            }

            /// <summary>
            /// 饱和度(纯度)
            /// </summary>
            public int S
            {
                get { return this._s; }
                set
                {
                    this._s = value;
                    this._s = this._s > 255 ? 255 : this._s;
                    this._s = this._s < 0 ? 0 : this._s;
                }
            }

            /// <summary>
            /// 饱和度
            /// </summary>
            public int L
            {
                get { return this._l; }
                set
                {
                    this._l = value;
                    this._l = this._l > 255 ? 255 : this._l;
                    this._l = this._l < 0 ? 0 : this._l;
                }
            }
        }

        /// <summary>
        /// 类      名：ColorHSV
        /// 功      能：H 色相 \ S 饱和度(纯度) \ V 明度 颜色模型 
        /// 日      期：2015-01-22
        /// 修      改：2015-03-20
        /// 作      者：ls9512
        /// </summary>
        public class ColorHSV
        {
            /// <summary>
            /// 构造方法
            /// </summary>
            /// <param name="h"></param>
            /// <param name="s"></param>
            /// <param name="v"></param>
            public ColorHSV(int h, int s, int v)
            {
                this._h = h;
                this._s = s;
                this._v = v;
            }

            private int _h;
            private int _s;
            private int _v;

            /// <summary>
            /// 色相
            /// </summary>
            public int H
            {
                get { return this._h; }
                set
                {
                    this._h = value;
                    this._h = this._h > 360 ? 360 : this._h;
                    this._h = this._h < 0 ? 0 : this._h;
                }
            }

            /// <summary>
            /// 饱和度(纯度)
            /// </summary>
            public int S
            {
                get { return this._s; }
                set
                {
                    this._s = value;
                    this._s = this._s > 255 ? 255 : this._s;
                    this._s = this._s < 0 ? 0 : this._s;
                }
            }

            /// <summary>
            /// 明度
            /// </summary>
            public int V
            {
                get { return this._v; }
                set
                {
                    this._v = value;
                    this._v = this._v > 255 ? 255 : this._v;
                    this._v = this._v < 0 ? 0 : this._v;
                }
            }
        }

        /// <summary>
        /// 类      名：ColorRGB
        /// 功      能：R 红色 \ G 绿色 \ B 蓝色 颜色模型
        ///                 所有颜色模型的基类，RGB是用于输出到屏幕的颜色模式，所以所有模型都将转换成RGB输出
        /// 日      期：2015-01-22
        /// 修      改：2015-03-20
        /// 作      者：ls9512
        /// </summary>
        public class ColorRGB
        {
            /// <summary>
            /// 构造方法
            /// </summary>
            /// <param name="r"></param>
            /// <param name="g"></param>
            /// <param name="b"></param>
            public ColorRGB(int r, int g, int b)
            {
                this._r = r;
                this._g = g;
                this._b = b;
            }

            private int _r;
            private int _g;
            private int _b;

            /// <summary>
            /// 红色
            /// </summary>
            public int R
            {
                get { return this._r; }
                set
                {
                    this._r = value;
                    this._r = this._r > 255 ? 255 : this._r;
                    this._r = this._r < 0 ? 0 : this._r;
                }
            }

            /// <summary>
            /// 绿色
            /// </summary>
            public int G
            {
                get { return this._g; }
                set
                {
                    this._g = value;
                    this._g = this._g > 255 ? 255 : this._g;
                    this._g = this._g < 0 ? 0 : this._g;
                }
            }

            /// <summary>
            /// 蓝色
            /// </summary>
            public int B
            {
                get { return this._b; }
                set
                {
                    this._b = value;
                    this._b = this._b > 255 ? 255 : this._b;
                    this._b = this._b < 0 ? 0 : this._b;
                }
            }

            /// <summary>
            /// 获取实际颜色
            /// </summary>
            /// <returns></returns>
            public Color GetColor()
            {
                return Color.FromArgb(this._r, this._g, this._b);
            }
        }
        #endregion

        /// <summary>
        /// 类      名：ColorHelper
        /// 功      能：提供从RGB到HSV/HSL色彩空间的相互转换
        /// 日      期：2015-02-08
        /// 修      改：2015-03-20
        /// 作      者：ls9512
        /// </summary>
        public static class ColorHelper
        {
            /// <summary>
            /// RGB转换HSV
            /// </summary>
            /// <param name="rgb"></param>
            /// <returns></returns>
            public static ColorHSV RgbToHsv(ColorRGB rgb)
            {
                float min, max, tmp, H, S, V;
                float R = rgb.R * 1.0f / 255, G = rgb.G * 1.0f / 255, B = rgb.B * 1.0f / 255;
                tmp = Math.Min(R, G);
                min = Math.Min(tmp, B);
                tmp = Math.Max(R, G);
                max = Math.Max(tmp, B);
                // H
                H = 0;
                if (max == min)
                {
                    H = 0;
                }
                else if (max == R && G > B)
                {
                    H = 60 * (G - B) * 1.0f / (max - min) + 0;
                }
                else if (max == R && G < B)
                {
                    H = 60 * (G - B) * 1.0f / (max - min) + 360;
                }
                else if (max == G)
                {
                    H = H = 60 * (B - R) * 1.0f / (max - min) + 120;
                }
                else if (max == B)
                {
                    H = H = 60 * (R - G) * 1.0f / (max - min) + 240;
                }
                // S
                if (max == 0)
                {
                    S = 0;
                }
                else
                {
                    S = (max - min) * 1.0f / max;
                }
                // V
                V = max;
                return new ColorHSV((int)H, (int)(S * 255), (int)(V * 255));
            }

            /// <summary>
            /// HSV转换RGB
            /// </summary>
            /// <param name="hsv"></param>
            /// <returns></returns>
            public static ColorRGB HsvToRgb(ColorHSV hsv)
            {
                if (hsv.H == 360) hsv.H = 359; // 360为全黑，原因不明
                float R = 0f, G = 0f, B = 0f;
                if (hsv.S == 0)
                {
                    return new ColorRGB(hsv.V, hsv.V, hsv.V);
                }
                float S = hsv.S * 1.0f / 255, V = hsv.V * 1.0f / 255;
                int H1 = (int)(hsv.H * 1.0f / 60), H = hsv.H;
                float F = H * 1.0f / 60 - H1;
                float P = V * (1.0f - S);
                float Q = V * (1.0f - F * S);
                float T = V * (1.0f - (1.0f - F) * S);
                switch (H1)
                {
                    case 0: R = V; G = T; B = P; break;
                    case 1: R = Q; G = V; B = P; break;
                    case 2: R = P; G = V; B = T; break;
                    case 3: R = P; G = Q; B = V; break;
                    case 4: R = T; G = P; B = V; break;
                    case 5: R = V; G = P; B = Q; break;
                }
                R = R * 255;
                G = G * 255;
                B = B * 255;
                while (R > 255) R -= 255;
                while (R < 0) R += 255;
                while (G > 255) G -= 255;
                while (G < 0) G += 255;
                while (B > 255) B -= 255;
                while (B < 0) B += 255;
                return new ColorRGB((int)R, (int)G, (int)B);
            }

            /// <summary>
            /// RGB转换HSL
            /// </summary>
            /// <param name="rgb"></param>
            /// <returns></returns>
            public static ColorHSL RgbToHsl(ColorRGB rgb)
            {
                float min, max, tmp, H, S, L;
                float R = rgb.R * 1.0f / 255, G = rgb.G * 1.0f / 255, B = rgb.B * 1.0f / 255;
                tmp = Math.Min(R, G);
                min = Math.Min(tmp, B);
                tmp = Math.Max(R, G);
                max = Math.Max(tmp, B);
                // H
                H = 0;
                if (max == min)
                {
                    H = 0;  // 此时H应为未定义，通常写为0
                }
                else if (max == R && G > B)
                {
                    H = 60 * (G - B) * 1.0f / (max - min) + 0;
                }
                else if (max == R && G < B)
                {
                    H = 60 * (G - B) * 1.0f / (max - min) + 360;
                }
                else if (max == G)
                {
                    H = H = 60 * (B - R) * 1.0f / (max - min) + 120;
                }
                else if (max == B)
                {
                    H = H = 60 * (R - G) * 1.0f / (max - min) + 240;
                }
                // L 
                L = 0.5f * (max + min);
                // S
                S = 0;
                if (L == 0 || max == min)
                {
                    S = 0;
                }
                else if (0 < L && L < 0.5)
                {
                    S = (max - min) / (L * 2);
                }
                else if (L > 0.5)
                {
                    S = (max - min) / (2 - 2 * L);
                }
                return new ColorHSL((int)H, (int)(S * 255), (int)(L * 255));
            }

            /// <summary>
            /// HSL转换RGB
            /// </summary>
            /// <param name="hsl"></param>
            /// <returns></returns>
            public static ColorRGB HslToRgb(ColorHSL hsl)
            {
                float R = 0f, G = 0f, B = 0f;
                float S = hsl.S * 1.0f / 255, L = hsl.L * 1.0f / 255;
                float temp1, temp2, temp3;
                if (S == 0f) // 灰色
                {
                    R = L;
                    G = L;
                    B = L;
                }
                else
                {
                    if (L < 0.5f)
                    {
                        temp2 = L * (1.0f + S);
                    }
                    else
                    {
                        temp2 = L + S - L * S;
                    }
                    temp1 = 2.0f * L - temp2;
                    float H = hsl.H * 1.0f / 360;
                    // R
                    temp3 = H + 1.0f / 3.0f;
                    if (temp3 < 0) temp3 += 1.0f;
                    if (temp3 > 1) temp3 -= 1.0f;
                    R = temp3;
                    // G
                    temp3 = H;
                    if (temp3 < 0) temp3 += 1.0f;
                    if (temp3 > 1) temp3 -= 1.0f;
                    G = temp3;
                    // B
                    temp3 = H - 1.0f / 3.0f;
                    if (temp3 < 0) temp3 += 1.0f;
                    if (temp3 > 1) temp3 -= 1.0f;
                    B = temp3;
                }
                R = R * 255;
                G = G * 255;
                B = B * 255;
                return new ColorRGB((int)R, (int)G, (int)B);
            }
        }

        private void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (this.dataGridView1.Rows[e.RowIndex].Cells["RGB"].Value!=null)
                {
                     string[] DD  =this.dataGridView1.Rows[e.RowIndex].Cells["RGB"].Value.ToString().Replace("(","").Replace(")","").Split(',');
                     this.dataGridView1.Rows[e.RowIndex].Cells["颜色"].Style.BackColor =
                         //, R, G, B)this.dataGridView1.Rows[e.RowIndex].Cells["颜色"].Value.ToString()
                         //(216,192,168

                          Color.FromArgb(255, Convert.ToInt32(DD[0]), Convert.ToInt32(DD[1]), Convert.ToInt32(DD[2]));
                         
                }
        }
    }
}
