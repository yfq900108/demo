namespace PdfDemo
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pdfDocument1 = new O2S.Components.PDFView4NET.PDFDocument(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pdfPageView1 = new O2S.Components.PDFView4NET.PDFPageView();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pdfDocument1
            // 
            this.pdfDocument1.Metadata = null;
            this.pdfDocument1.PageLayout = O2S.Components.PDFView4NET.PDFPageLayout.SinglePage;
            this.pdfDocument1.PageMode = O2S.Components.PDFView4NET.PDFPageMode.UseNone;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.pdfPageView1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(716, 603);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // pdfPageView1
            // 
            this.pdfPageView1.DefaultEllipseAnnotationBorderWidth = 1D;
            this.pdfPageView1.DefaultInkAnnotationWidth = 1D;
            this.pdfPageView1.DefaultRectangleAnnotationBorderWidth = 1D;
            this.pdfPageView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pdfPageView1.Document = this.pdfDocument1;
            this.pdfPageView1.DownscaleLargeImages = false;
            this.pdfPageView1.EnableRepeatedKeys = false;
            this.pdfPageView1.Location = new System.Drawing.Point(3, 3);
            this.pdfPageView1.Name = "pdfPageView1";
            this.pdfPageView1.PageDisplayLayout = O2S.Components.PDFView4NET.PDFPageDisplayLayout.OneColumn;
            this.pdfPageView1.PageNumber = 0;
            this.pdfPageView1.RenderingProgressColor = System.Drawing.Color.Empty;
            this.pdfPageView1.RequiredFormFieldHighlightColor = System.Drawing.Color.Empty;
            this.pdfPageView1.ScrollPosition = new System.Drawing.Point(0, 0);
            this.pdfPageView1.Size = new System.Drawing.Size(710, 536);
            this.pdfPageView1.SubstituteFonts = null;
            this.pdfPageView1.TabIndex = 1;
            this.pdfPageView1.WorkMode = O2S.Components.PDFView4NET.UserInteractiveWorkMode.None;
            this.pdfPageView1.ZoomMode = O2S.Components.PDFView4NET.PDFZoomMode.FitWidth;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(716, 603);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private O2S.Components.PDFView4NET.PDFDocument pdfDocument1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private O2S.Components.PDFView4NET.PDFPageView pdfPageView1;
    }
}

