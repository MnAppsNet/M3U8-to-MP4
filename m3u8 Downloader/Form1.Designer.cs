namespace m3u8_Downloader
{
    partial class main
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
            this.input_link = new System.Windows.Forms.TextBox();
            this.label_link = new System.Windows.Forms.Label();
            this.button_download = new System.Windows.Forms.Button();
            this.list = new System.Windows.Forms.ListBox();
            this.status = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // input_link
            // 
            this.input_link.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.input_link.Location = new System.Drawing.Point(56, 12);
            this.input_link.Name = "input_link";
            this.input_link.Size = new System.Drawing.Size(367, 26);
            this.input_link.TabIndex = 0;
            // 
            // label_link
            // 
            this.label_link.AutoSize = true;
            this.label_link.Location = new System.Drawing.Point(12, 15);
            this.label_link.Name = "label_link";
            this.label_link.Size = new System.Drawing.Size(38, 20);
            this.label_link.TabIndex = 1;
            this.label_link.Text = "Link";
            // 
            // button_download
            // 
            this.button_download.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_download.Location = new System.Drawing.Point(16, 44);
            this.button_download.Name = "button_download";
            this.button_download.Size = new System.Drawing.Size(407, 38);
            this.button_download.TabIndex = 2;
            this.button_download.Text = "Download";
            this.button_download.UseVisualStyleBackColor = true;
            this.button_download.Click += new System.EventHandler(this.button_download_Click);
            // 
            // list
            // 
            this.list.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.list.FormattingEnabled = true;
            this.list.ItemHeight = 20;
            this.list.Location = new System.Drawing.Point(16, 88);
            this.list.Name = "list";
            this.list.Size = new System.Drawing.Size(407, 324);
            this.list.TabIndex = 3;
            // 
            // status
            // 
            this.status.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.status.AutoSize = true;
            this.status.Location = new System.Drawing.Point(12, 421);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(0, 20);
            this.status.TabIndex = 4;
            // 
            // main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(435, 450);
            this.Controls.Add(this.status);
            this.Controls.Add(this.list);
            this.Controls.Add(this.button_download);
            this.Controls.Add(this.label_link);
            this.Controls.Add(this.input_link);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(461, 510);
            this.MinimumSize = new System.Drawing.Size(461, 510);
            this.Name = "main";
            this.Text = "m3u8 Downloader";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox input_link;
        private System.Windows.Forms.Label label_link;
        private System.Windows.Forms.Button button_download;
        private System.Windows.Forms.ListBox list;
        private System.Windows.Forms.Label status;
    }
}

