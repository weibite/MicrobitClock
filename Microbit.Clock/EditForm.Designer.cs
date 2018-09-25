namespace Microbit.Clock
{
    partial class EditForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.dtpEventTime = new System.Windows.Forms.DateTimePicker();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnSelectMusic = new System.Windows.Forms.Button();
            this.lblMusicPath = new System.Windows.Forms.Label();
            this.cboxCanPlay = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(64, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "标题：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "提醒时间：";
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(111, 22);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(201, 21);
            this.txtTitle.TabIndex = 2;
            // 
            // dtpEventTime
            // 
            this.dtpEventTime.CustomFormat = "yyyy-MM-dd HH:mm";
            this.dtpEventTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEventTime.Location = new System.Drawing.Point(112, 64);
            this.dtpEventTime.Name = "dtpEventTime";
            this.dtpEventTime.Size = new System.Drawing.Size(200, 21);
            this.dtpEventTime.TabIndex = 3;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(236, 179);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 32);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "关   闭";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(111, 179);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 32);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "保   存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnSelectMusic
            // 
            this.btnSelectMusic.Location = new System.Drawing.Point(31, 107);
            this.btnSelectMusic.Name = "btnSelectMusic";
            this.btnSelectMusic.Size = new System.Drawing.Size(65, 29);
            this.btnSelectMusic.TabIndex = 7;
            this.btnSelectMusic.Text = "设置音乐";
            this.btnSelectMusic.UseVisualStyleBackColor = true;
            this.btnSelectMusic.Click += new System.EventHandler(this.btnSelectMusic_Click);
            // 
            // lblMusicPath
            // 
            this.lblMusicPath.AutoSize = true;
            this.lblMusicPath.Location = new System.Drawing.Point(29, 148);
            this.lblMusicPath.Name = "lblMusicPath";
            this.lblMusicPath.Size = new System.Drawing.Size(47, 12);
            this.lblMusicPath.TabIndex = 8;
            this.lblMusicPath.Text = " lable3";
            // 
            // cboxCanPlay
            // 
            this.cboxCanPlay.AutoSize = true;
            this.cboxCanPlay.Location = new System.Drawing.Point(111, 114);
            this.cboxCanPlay.Name = "cboxCanPlay";
            this.cboxCanPlay.Size = new System.Drawing.Size(72, 16);
            this.cboxCanPlay.TabIndex = 9;
            this.cboxCanPlay.Text = "是否播放";
            this.cboxCanPlay.UseVisualStyleBackColor = true;
            // 
            // EditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(405, 229);
            this.Controls.Add(this.cboxCanPlay);
            this.Controls.Add(this.lblMusicPath);
            this.Controls.Add(this.btnSelectMusic);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dtpEventTime);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设置闹钟--微比特电子闹钟V1.0  (Author：陈亚)";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.DateTimePicker dtpEventTime;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnSelectMusic;
        private System.Windows.Forms.Label lblMusicPath;
        private System.Windows.Forms.CheckBox cboxCanPlay;
    }
}