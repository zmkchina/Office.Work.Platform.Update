using System;
using System.Drawing;

namespace Office.Work.Platform.Update
{
    partial class MainWin
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ProgressBar prgBar;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnUpdate;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 150);
            this.WindowState = System.Windows.Forms.FormWindowState.Normal;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "升级程序";
            this.Load += MainWin_Load;
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.prgBar = new System.Windows.Forms.ProgressBar();
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.TabIndex = 0;
            this.btnUpdate.Text = "升级";
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            this.btnUpdate.Location = new System.Drawing.Point(200, 100);
            this.Controls.Add(btnUpdate);

            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Name = "btnClose";
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "关闭";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            this.btnClose.Location = new System.Drawing.Point(280, 100);
            this.Controls.Add(btnClose);

            // 
            // prgBar
            // 
            this.prgBar.Location = new System.Drawing.Point(50, 50);
            this.prgBar.Name = "prgBar";
            this.prgBar.Size = new System.Drawing.Size(300, 20);
            this.prgBar.BackColor = Color.FromArgb(255, 255, 0, 0);
            this.prgBar.Value = 0;
            this.prgBar.Maximum = 100;
            this.prgBar.TabIndex = 2;
            this.prgBar.Visible = true;
            this.Controls.Add(prgBar);
        }





        #endregion
    }
}

