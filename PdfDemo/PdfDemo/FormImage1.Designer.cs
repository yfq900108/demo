namespace PdfDemo
{
    partial class FormImage1
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pb_v = new System.Windows.Forms.PictureBox();
            this.pb_l = new System.Windows.Forms.PictureBox();
            this.pb_r = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_v)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_l)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_r)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34F));
            this.tableLayoutPanel1.Controls.Add(this.pb_v, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pb_l, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.pb_r, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(979, 518);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // pb_v
            // 
            this.pb_v.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pb_v.Location = new System.Drawing.Point(3, 3);
            this.pb_v.Name = "pb_v";
            this.pb_v.Size = new System.Drawing.Size(317, 512);
            this.pb_v.TabIndex = 0;
            this.pb_v.TabStop = false;
            // 
            // pb_l
            // 
            this.pb_l.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pb_l.Location = new System.Drawing.Point(326, 3);
            this.pb_l.Name = "pb_l";
            this.pb_l.Size = new System.Drawing.Size(317, 512);
            this.pb_l.TabIndex = 1;
            this.pb_l.TabStop = false;
            // 
            // pb_r
            // 
            this.pb_r.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pb_r.Location = new System.Drawing.Point(649, 3);
            this.pb_r.Name = "pb_r";
            this.pb_r.Size = new System.Drawing.Size(327, 512);
            this.pb_r.TabIndex = 2;
            this.pb_r.TabStop = false;
            // 
            // FormImage1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(979, 518);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FormImage1";
            this.Text = "FormImage1";
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pb_v)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_l)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_r)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox pb_v;
        private System.Windows.Forms.PictureBox pb_l;
        private System.Windows.Forms.PictureBox pb_r;
    }
}