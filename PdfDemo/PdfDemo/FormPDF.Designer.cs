namespace PdfDemo
{
    partial class FormPDF
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pdfPageView1 = new O2S.Components.PDFView4NET.PDFPageView();
            this.pdfDocument1 = new O2S.Components.PDFView4NET.PDFDocument(this.components);
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.bimg1 = new System.Windows.Forms.Button();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.bimg2 = new System.Windows.Forms.Button();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.bimg3 = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.pdfPageView1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(798, 543);
            this.tableLayoutPanel1.TabIndex = 0;
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
            this.pdfPageView1.Size = new System.Drawing.Size(792, 482);
            this.pdfPageView1.SubstituteFonts = null;
            this.pdfPageView1.TabIndex = 1;
            this.pdfPageView1.WorkMode = O2S.Components.PDFView4NET.UserInteractiveWorkMode.None;
            this.pdfPageView1.ZoomMode = O2S.Components.PDFView4NET.PDFZoomMode.FitWidth;
            // 
            // pdfDocument1
            // 
            this.pdfDocument1.Metadata = null;
            this.pdfDocument1.PageLayout = O2S.Components.PDFView4NET.PDFPageLayout.SinglePage;
            this.pdfDocument1.PageMode = O2S.Components.PDFView4NET.PDFPageMode.UseNone;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel4, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel5, 2, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 491);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(792, 49);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.bimg1, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(255, 43);
            this.tableLayoutPanel3.TabIndex = 3;
            // 
            // bimg1
            // 
            this.bimg1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bimg1.Location = new System.Drawing.Point(3, 3);
            this.bimg1.Name = "bimg1";
            this.bimg1.Size = new System.Drawing.Size(249, 37);
            this.bimg1.TabIndex = 1;
            this.bimg1.Text = "签名一";
            this.bimg1.UseVisualStyleBackColor = true;
            this.bimg1.Click += new System.EventHandler(this.bimg1_Click);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.bimg2, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(264, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(255, 43);
            this.tableLayoutPanel4.TabIndex = 4;
            // 
            // bimg2
            // 
            this.bimg2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bimg2.Location = new System.Drawing.Point(3, 3);
            this.bimg2.Name = "bimg2";
            this.bimg2.Size = new System.Drawing.Size(249, 37);
            this.bimg2.TabIndex = 2;
            this.bimg2.Text = "签名二";
            this.bimg2.UseVisualStyleBackColor = true;
            this.bimg2.Click += new System.EventHandler(this.bimg2_Click);
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Controls.Add(this.bimg3, 0, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(525, 3);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(264, 43);
            this.tableLayoutPanel5.TabIndex = 5;
            // 
            // bimg3
            // 
            this.bimg3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bimg3.Location = new System.Drawing.Point(3, 3);
            this.bimg3.Name = "bimg3";
            this.bimg3.Size = new System.Drawing.Size(258, 37);
            this.bimg3.TabIndex = 3;
            this.bimg3.Text = "签名三";
            this.bimg3.UseVisualStyleBackColor = true;
            this.bimg3.Click += new System.EventHandler(this.bimg3_Click);
            // 
            // FormPDF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(798, 543);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FormPDF";
            this.Text = "FormPDF";
            this.Load += new System.EventHandler(this.FormPDF_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private O2S.Components.PDFView4NET.PDFPageView pdfPageView1;
        private O2S.Components.PDFView4NET.PDFDocument pdfDocument1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button bimg1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Button bimg2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Button bimg3;
    }
}