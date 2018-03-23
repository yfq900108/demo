namespace PdfDemo2._0
{
    partial class FormImage2
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
            this.pb_f = new System.Windows.Forms.PictureBox();
            this.pb_b = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_f)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_b)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.pb_f, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pb_b, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(979, 518);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // pb_f
            // 
            this.pb_f.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pb_f.Location = new System.Drawing.Point(3, 3);
            this.pb_f.Name = "pb_f";
            this.pb_f.Size = new System.Drawing.Size(483, 512);
            this.pb_f.TabIndex = 0;
            this.pb_f.TabStop = false;
            // 
            // pb_b
            // 
            this.pb_b.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pb_b.Location = new System.Drawing.Point(492, 3);
            this.pb_b.Name = "pb_b";
            this.pb_b.Size = new System.Drawing.Size(484, 512);
            this.pb_b.TabIndex = 1;
            this.pb_b.TabStop = false;
            // 
            // FormImage2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(979, 518);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FormImage2";
            this.Text = "FormImage2";
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pb_f)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_b)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox pb_f;
        private System.Windows.Forms.PictureBox pb_b;

    }
}