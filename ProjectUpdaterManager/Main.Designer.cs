namespace ProjectUpdaterManager
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.label1 = new System.Windows.Forms.Label();
            this.lbelStatus = new System.Windows.Forms.LinkLabel();
            this.btnInstall = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnUnstall = new System.Windows.Forms.Button();
            this.ServiceTimer = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbPort = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnStopResin = new System.Windows.Forms.Button();
            this.btnStartResin = new System.Windows.Forms.Button();
            this.lbelResinStatus = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chklbRemoteUpdater = new System.Windows.Forms.CheckedListBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnAddRemote = new System.Windows.Forms.Button();
            this.btnStopSelected = new System.Windows.Forms.Button();
            this.btnStartSelected = new System.Windows.Forms.Button();
            this.btnDisConnectRemote = new System.Windows.Forms.Button();
            this.btnConnectRemote = new System.Windows.Forms.Button();
            this.btnOpposite = new System.Windows.Forms.Button();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.pbUpdate = new System.Windows.Forms.ProgressBar();
            this.cbUpdateRestart = new System.Windows.Forms.CheckBox();
            this.btnStartUpdate = new System.Windows.Forms.Button();
            this.btnUpdatePackagerSelector = new System.Windows.Forms.Button();
            this.lbelPackagePath = new System.Windows.Forms.LinkLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.ofdUpdatePackage = new System.Windows.Forms.OpenFileDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.tabCmdView = new System.Windows.Forms.TabControl();
            this.RemoteTimer = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "服务状态：";
            // 
            // lbelStatus
            // 
            this.lbelStatus.AutoSize = true;
            this.lbelStatus.Location = new System.Drawing.Point(91, 15);
            this.lbelStatus.Name = "lbelStatus";
            this.lbelStatus.Size = new System.Drawing.Size(29, 12);
            this.lbelStatus.TabIndex = 1;
            this.lbelStatus.TabStop = true;
            this.lbelStatus.Text = "未知";
            // 
            // btnInstall
            // 
            this.btnInstall.Location = new System.Drawing.Point(13, 80);
            this.btnInstall.Name = "btnInstall";
            this.btnInstall.Size = new System.Drawing.Size(75, 23);
            this.btnInstall.TabIndex = 2;
            this.btnInstall.Text = "安装服务";
            this.btnInstall.UseVisualStyleBackColor = true;
            this.btnInstall.Click += new System.EventHandler(this.btnInstall_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(13, 51);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "启动服务";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(94, 51);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 2;
            this.btnStop.Text = "停止服务";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnUnstall
            // 
            this.btnUnstall.Location = new System.Drawing.Point(94, 80);
            this.btnUnstall.Name = "btnUnstall";
            this.btnUnstall.Size = new System.Drawing.Size(75, 23);
            this.btnUnstall.TabIndex = 2;
            this.btnUnstall.Text = "卸载服务";
            this.btnUnstall.UseVisualStyleBackColor = true;
            this.btnUnstall.Click += new System.EventHandler(this.btnUnstall_Click);
            // 
            // ServiceTimer
            // 
            this.ServiceTimer.Tick += new System.EventHandler(this.ServiceTimer_Tick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbPort);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnStop);
            this.groupBox1.Controls.Add(this.lbelStatus);
            this.groupBox1.Controls.Add(this.btnStart);
            this.groupBox1.Controls.Add(this.btnInstall);
            this.groupBox1.Controls.Add(this.btnUnstall);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(179, 109);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "小卫士";
            // 
            // tbPort
            // 
            this.tbPort.Location = new System.Drawing.Point(93, 29);
            this.tbPort.Name = "tbPort";
            this.tbPort.Size = new System.Drawing.Size(56, 21);
            this.tbPort.TabIndex = 4;
            this.tbPort.Text = "2005";
            this.tbPort.TextChanged += new System.EventHandler(this.tbPort_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "服务端口：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnStopResin);
            this.groupBox2.Controls.Add(this.btnStartResin);
            this.groupBox2.Controls.Add(this.lbelResinStatus);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(197, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(168, 109);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Resin状态";
            // 
            // btnStopResin
            // 
            this.btnStopResin.Location = new System.Drawing.Point(87, 41);
            this.btnStopResin.Name = "btnStopResin";
            this.btnStopResin.Size = new System.Drawing.Size(75, 52);
            this.btnStopResin.TabIndex = 2;
            this.btnStopResin.Text = "停止服务";
            this.btnStopResin.UseVisualStyleBackColor = true;
            this.btnStopResin.Click += new System.EventHandler(this.btnStopResin_Click);
            // 
            // btnStartResin
            // 
            this.btnStartResin.Location = new System.Drawing.Point(6, 41);
            this.btnStartResin.Name = "btnStartResin";
            this.btnStartResin.Size = new System.Drawing.Size(75, 52);
            this.btnStartResin.TabIndex = 2;
            this.btnStartResin.Text = "启动服务";
            this.btnStartResin.UseVisualStyleBackColor = true;
            this.btnStartResin.Click += new System.EventHandler(this.btnStartResin_Click);
            // 
            // lbelResinStatus
            // 
            this.lbelResinStatus.AutoSize = true;
            this.lbelResinStatus.Location = new System.Drawing.Point(83, 17);
            this.lbelResinStatus.Name = "lbelResinStatus";
            this.lbelResinStatus.Size = new System.Drawing.Size(29, 12);
            this.lbelResinStatus.TabIndex = 1;
            this.lbelResinStatus.TabStop = true;
            this.lbelResinStatus.Text = "未知";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "服务状态：";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox3.Controls.Add(this.chklbRemoteUpdater);
            this.groupBox3.Location = new System.Drawing.Point(12, 220);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(353, 251);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "远程计算机";
            // 
            // chklbRemoteUpdater
            // 
            this.chklbRemoteUpdater.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chklbRemoteUpdater.Font = new System.Drawing.Font("方正兰亭超细黑简体", 9.5F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.chklbRemoteUpdater.FormattingEnabled = true;
            this.chklbRemoteUpdater.Location = new System.Drawing.Point(3, 17);
            this.chklbRemoteUpdater.Name = "chklbRemoteUpdater";
            this.chklbRemoteUpdater.Size = new System.Drawing.Size(347, 231);
            this.chklbRemoteUpdater.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnRemove);
            this.groupBox4.Controls.Add(this.btnAddRemote);
            this.groupBox4.Controls.Add(this.btnStopSelected);
            this.groupBox4.Controls.Add(this.btnStartSelected);
            this.groupBox4.Controls.Add(this.btnDisConnectRemote);
            this.groupBox4.Controls.Add(this.btnConnectRemote);
            this.groupBox4.Controls.Add(this.btnOpposite);
            this.groupBox4.Controls.Add(this.btnSelectAll);
            this.groupBox4.Location = new System.Drawing.Point(12, 127);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(347, 87);
            this.groupBox4.TabIndex = 6;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "远程计算机控制面板";
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(264, 20);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(77, 23);
            this.btnRemove.TabIndex = 2;
            this.btnRemove.Text = "移除";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnAddRemote
            // 
            this.btnAddRemote.Location = new System.Drawing.Point(181, 20);
            this.btnAddRemote.Name = "btnAddRemote";
            this.btnAddRemote.Size = new System.Drawing.Size(77, 23);
            this.btnAddRemote.TabIndex = 1;
            this.btnAddRemote.Text = "添加";
            this.btnAddRemote.UseVisualStyleBackColor = true;
            this.btnAddRemote.Click += new System.EventHandler(this.btnAddRemote_Click);
            // 
            // btnStopSelected
            // 
            this.btnStopSelected.Location = new System.Drawing.Point(264, 49);
            this.btnStopSelected.Name = "btnStopSelected";
            this.btnStopSelected.Size = new System.Drawing.Size(77, 23);
            this.btnStopSelected.TabIndex = 0;
            this.btnStopSelected.Text = "停止服务";
            this.btnStopSelected.UseVisualStyleBackColor = true;
            this.btnStopSelected.Click += new System.EventHandler(this.btnStopSelected_Click);
            // 
            // btnStartSelected
            // 
            this.btnStartSelected.Location = new System.Drawing.Point(181, 49);
            this.btnStartSelected.Name = "btnStartSelected";
            this.btnStartSelected.Size = new System.Drawing.Size(77, 23);
            this.btnStartSelected.TabIndex = 0;
            this.btnStartSelected.Text = "启动服务";
            this.btnStartSelected.UseVisualStyleBackColor = true;
            this.btnStartSelected.Click += new System.EventHandler(this.btnStartSelected_Click);
            // 
            // btnDisConnectRemote
            // 
            this.btnDisConnectRemote.Location = new System.Drawing.Point(98, 49);
            this.btnDisConnectRemote.Name = "btnDisConnectRemote";
            this.btnDisConnectRemote.Size = new System.Drawing.Size(77, 23);
            this.btnDisConnectRemote.TabIndex = 0;
            this.btnDisConnectRemote.Text = "断开";
            this.btnDisConnectRemote.UseVisualStyleBackColor = true;
            this.btnDisConnectRemote.Click += new System.EventHandler(this.btnDisConnectRemote_Click);
            // 
            // btnConnectRemote
            // 
            this.btnConnectRemote.Location = new System.Drawing.Point(15, 49);
            this.btnConnectRemote.Name = "btnConnectRemote";
            this.btnConnectRemote.Size = new System.Drawing.Size(77, 23);
            this.btnConnectRemote.TabIndex = 0;
            this.btnConnectRemote.Text = "连接";
            this.btnConnectRemote.UseVisualStyleBackColor = true;
            this.btnConnectRemote.Click += new System.EventHandler(this.btnConnectRemote_Click);
            // 
            // btnOpposite
            // 
            this.btnOpposite.Location = new System.Drawing.Point(98, 20);
            this.btnOpposite.Name = "btnOpposite";
            this.btnOpposite.Size = new System.Drawing.Size(77, 23);
            this.btnOpposite.TabIndex = 0;
            this.btnOpposite.Text = "反选";
            this.btnOpposite.UseVisualStyleBackColor = true;
            this.btnOpposite.Click += new System.EventHandler(this.btnOpposite_Click);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(15, 20);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(77, 23);
            this.btnSelectAll.TabIndex = 0;
            this.btnSelectAll.Text = "全选";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.pbUpdate);
            this.groupBox5.Controls.Add(this.cbUpdateRestart);
            this.groupBox5.Controls.Add(this.btnStartUpdate);
            this.groupBox5.Controls.Add(this.btnUpdatePackagerSelector);
            this.groupBox5.Controls.Add(this.lbelPackagePath);
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Location = new System.Drawing.Point(371, 12);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(253, 129);
            this.groupBox5.TabIndex = 7;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "应用更新";
            // 
            // pbUpdate
            // 
            this.pbUpdate.Location = new System.Drawing.Point(6, 109);
            this.pbUpdate.Name = "pbUpdate";
            this.pbUpdate.Size = new System.Drawing.Size(241, 14);
            this.pbUpdate.TabIndex = 5;
            // 
            // cbUpdateRestart
            // 
            this.cbUpdateRestart.AutoSize = true;
            this.cbUpdateRestart.Location = new System.Drawing.Point(103, 45);
            this.cbUpdateRestart.Name = "cbUpdateRestart";
            this.cbUpdateRestart.Size = new System.Drawing.Size(144, 16);
            this.cbUpdateRestart.TabIndex = 4;
            this.cbUpdateRestart.Text = "先停更新后再启动服务";
            this.cbUpdateRestart.UseVisualStyleBackColor = true;
            // 
            // btnStartUpdate
            // 
            this.btnStartUpdate.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnStartUpdate.Location = new System.Drawing.Point(69, 70);
            this.btnStartUpdate.Name = "btnStartUpdate";
            this.btnStartUpdate.Size = new System.Drawing.Size(126, 33);
            this.btnStartUpdate.TabIndex = 3;
            this.btnStartUpdate.Text = "执行更新";
            this.btnStartUpdate.UseVisualStyleBackColor = true;
            this.btnStartUpdate.Click += new System.EventHandler(this.btnStartUpdate_Click);
            // 
            // btnUpdatePackagerSelector
            // 
            this.btnUpdatePackagerSelector.Location = new System.Drawing.Point(22, 41);
            this.btnUpdatePackagerSelector.Name = "btnUpdatePackagerSelector";
            this.btnUpdatePackagerSelector.Size = new System.Drawing.Size(75, 23);
            this.btnUpdatePackagerSelector.TabIndex = 2;
            this.btnUpdatePackagerSelector.Text = "选择更新包";
            this.btnUpdatePackagerSelector.UseVisualStyleBackColor = true;
            this.btnUpdatePackagerSelector.Click += new System.EventHandler(this.btnUpdatePackagerSelector_Click);
            // 
            // lbelPackagePath
            // 
            this.lbelPackagePath.AutoSize = true;
            this.lbelPackagePath.Location = new System.Drawing.Point(52, 17);
            this.lbelPackagePath.Name = "lbelPackagePath";
            this.lbelPackagePath.Size = new System.Drawing.Size(143, 12);
            this.lbelPackagePath.TabIndex = 1;
            this.lbelPackagePath.TabStop = true;
            this.lbelPackagePath.Text = "请选择更新包，格式为zip";
            this.toolTip1.SetToolTip(this.lbelPackagePath, "请选择更新包，格式为zip");
            this.lbelPackagePath.TextChanged += new System.EventHandler(this.lbelPackagePath_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "更新包：";
            // 
            // ofdUpdatePackage
            // 
            this.ofdUpdatePackage.AddExtension = false;
            this.ofdUpdatePackage.FileName = "Updater.zip";
            this.ofdUpdatePackage.Filter = "小卫士更新包|*.zip|所有文件|*.*";
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox6.Controls.Add(this.tabCmdView);
            this.groupBox6.Location = new System.Drawing.Point(371, 147);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(314, 324);
            this.groupBox6.TabIndex = 8;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "更新结果";
            // 
            // tabCmdView
            // 
            this.tabCmdView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabCmdView.Location = new System.Drawing.Point(3, 17);
            this.tabCmdView.Name = "tabCmdView";
            this.tabCmdView.SelectedIndex = 0;
            this.tabCmdView.Size = new System.Drawing.Size(308, 304);
            this.tabCmdView.TabIndex = 0;
            // 
            // RemoteTimer
            // 
            this.RemoteTimer.Interval = 2000;
            this.RemoteTimer.Tick += new System.EventHandler(this.RemoteTimer_Tick);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(697, 483);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "应用更新小卫士";
            this.Load += new System.EventHandler(this.Main_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel lbelStatus;
        private System.Windows.Forms.Button btnInstall;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnUnstall;
        private System.Windows.Forms.Timer ServiceTimer;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.LinkLabel lbelResinStatus;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnStopResin;
        private System.Windows.Forms.Button btnStartResin;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckedListBox chklbRemoteUpdater;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnStopSelected;
        private System.Windows.Forms.Button btnStartSelected;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.OpenFileDialog ofdUpdatePackage;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel lbelPackagePath;
        private System.Windows.Forms.Button btnUpdatePackagerSelector;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnStartUpdate;
        private System.Windows.Forms.CheckBox cbUpdateRestart;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbPort;
        private System.Windows.Forms.ProgressBar pbUpdate;
        private System.Windows.Forms.Button btnAddRemote;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Timer RemoteTimer;
        private System.Windows.Forms.TabControl tabCmdView;
        private System.Windows.Forms.Button btnDisConnectRemote;
        private System.Windows.Forms.Button btnConnectRemote;
        private System.Windows.Forms.Button btnOpposite;
    }
}

