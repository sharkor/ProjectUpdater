namespace ProjectUpdaterManager
{
    partial class AddRemote
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddRemote));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.iacRemote = new IPAddressControlLib.IPAddressControl();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnTelnet = new System.Windows.Forms.Button();
            this.btnPing = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.iacRemote);
            this.groupBox1.Controls.Add(this.btnClose);
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Controls.Add(this.btnTelnet);
            this.groupBox1.Controls.Add(this.btnPing);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(284, 82);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "服务器信息";
            // 
            // iacRemote
            // 
            this.iacRemote.AllowInternalTab = false;
            this.iacRemote.AutoHeight = true;
            this.iacRemote.BackColor = System.Drawing.SystemColors.Window;
            this.iacRemote.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.iacRemote.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.iacRemote.Location = new System.Drawing.Point(77, 20);
            this.iacRemote.MinimumSize = new System.Drawing.Size(96, 21);
            this.iacRemote.Name = "iacRemote";
            this.iacRemote.ReadOnly = false;
            this.iacRemote.Size = new System.Drawing.Size(96, 21);
            this.iacRemote.TabIndex = 1;
            this.iacRemote.Text = "...";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(140, 47);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(59, 47);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "添加";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnTelnet
            // 
            this.btnTelnet.Location = new System.Drawing.Point(226, 20);
            this.btnTelnet.Name = "btnTelnet";
            this.btnTelnet.Size = new System.Drawing.Size(50, 23);
            this.btnTelnet.TabIndex = 3;
            this.btnTelnet.Text = "telnet";
            this.btnTelnet.UseVisualStyleBackColor = true;
            this.btnTelnet.Click += new System.EventHandler(this.btnTelnet_Click);
            // 
            // btnPing
            // 
            this.btnPing.Location = new System.Drawing.Point(183, 20);
            this.btnPing.Name = "btnPing";
            this.btnPing.Size = new System.Drawing.Size(37, 23);
            this.btnPing.TabIndex = 2;
            this.btnPing.Text = "ping";
            this.btnPing.UseVisualStyleBackColor = true;
            this.btnPing.Click += new System.EventHandler(this.btnPing_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "服务器IP：";
            // 
            // AddRemote
            // 
            this.AcceptButton = this.btnAdd;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 82);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddRemote";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "增加远程计算机";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnPing;
        private System.Windows.Forms.Button btnTelnet;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnAdd;
        private IPAddressControlLib.IPAddressControl iacRemote;
    }
}