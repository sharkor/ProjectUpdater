using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Collections;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace ProjectUpdaterManager
{
    public partial class Main : Form
    {
        private const String RESIN_SERVICE_NAME = "resin";

        private const String UPDATER_SERVICE_NAME = "ProjectUpdater";

        private Dictionary<String, SocketClientManager> clients = new Dictionary<String, SocketClientManager>();
        public Main()
        {
            InitializeComponent();
        }

        private void btnInstall_Click(object sender, EventArgs e)
        {
            string CurrentDirectory = System.Environment.CurrentDirectory;
            System.Environment.CurrentDirectory = CurrentDirectory + "\\Service";
            Process process = new Process();
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.FileName = "Install.bat";
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            System.Environment.CurrentDirectory = CurrentDirectory;

        }

        private void btnUnstall_Click(object sender, EventArgs e)
        {
            string CurrentDirectory = System.Environment.CurrentDirectory;
            System.Environment.CurrentDirectory = CurrentDirectory + "\\Service";
            Process process = new Process();
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.FileName = "Unstall.bat";
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            System.Environment.CurrentDirectory = CurrentDirectory;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                ServiceController serviceController = new ServiceController(UPDATER_SERVICE_NAME);
                if ("Stopped".Equals(serviceController.Status.ToString()))
                    serviceController.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "执行命令错误");
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                ServiceController serviceController = new ServiceController(UPDATER_SERVICE_NAME);
                if (serviceController.CanStop)
                    serviceController.Stop();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "执行命令错误");
            }
        }

        private void ServiceTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                ServiceController serviceController = new ServiceController(UPDATER_SERVICE_NAME);
                this.lbelStatus.Text = serviceController.Status.ToString();
                this.tbPort.ReadOnly = true;
            }
            catch
            {
                this.lbelStatus.Text = "未侦测到服务";
                this.tbPort.ReadOnly = false;
            }
            try
            {
                ServiceController serviceController = new ServiceController(RESIN_SERVICE_NAME);
                this.lbelResinStatus.Text = serviceController.Status.ToString();
            }
            catch
            {
                this.lbelResinStatus.Text = "未侦测到服务";
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            ExeConfigurationFileMap map = new ExeConfigurationFileMap();
            map.ExeConfigFilename = System.Environment.CurrentDirectory + "\\Service\\ProjectUpdater.exe.config";   ////（引号里面的是你的配置文件的在程序的绝对路径）。
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);
            this.tbPort.Text = config.AppSettings.Settings["port"].Value;
            this.ServiceTimer.Start();
            //this.RemoteTimer.Start();
            refreshRemote();
        }

        private void btnStartResin_Click(object sender, EventArgs e)
        {
            try
            {
                ServiceController serviceController = new ServiceController(RESIN_SERVICE_NAME);
                if ("Stopped".Equals(serviceController.Status.ToString()))
                    serviceController.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "执行命令错误");
            }
        }

        private void btnStopResin_Click(object sender, EventArgs e)
        {
            try
            {
                ServiceController serviceController = new ServiceController(RESIN_SERVICE_NAME);
                if (serviceController.CanStop)
                    serviceController.Stop();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "执行命令错误");
            }
        }

        private void btnUpdatePackagerSelector_Click(object sender, EventArgs e)
        {
            DialogResult dr = this.ofdUpdatePackage.ShowDialog();
            if (DialogResult.OK.Equals(dr))
            {
                lbelPackagePath.Text = ofdUpdatePackage.FileName;
            }
        }

        private void lbelPackagePath_TextChanged(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(this.lbelPackagePath, lbelPackagePath.Text);
        }

        private void btnStartUpdate_Click(object sender, EventArgs e)
        {
            //文件准备
            if (ofdUpdatePackage.FileName != null)
            {
                FileInfo updaterInfo = new FileInfo(ofdUpdatePackage.FileName);
                if (!updaterInfo.Exists)
                {
                    MessageBox.Show("更新包不存在，请选择有效的更新包！");
                    return;
                }
                FileStream fs = updaterInfo.OpenRead();
                byte[] updaterPakcage = new byte[fs.Length];
                fs.Read(updaterPakcage, 0, updaterPakcage.Length);
                
                //校验数据大小，如果过大，则切割发包
                string md5 = GetMD5Hash(fs);
                //格式｛'resinCMD':'status|start|stop','updateCMD':'restart|stop','updatePackage':'update.zip',
                //      'bagCount':20,'MD5':'md5','bagSize':1024*3｝
                int bagSize = 1024 * 3;
                int bagCount = (Int32.Parse(updaterInfo.Length.ToString()) / bagSize) + (updaterInfo.Length % bagSize > 0 ? 1 : 0);
                string cmd = "{'resinCMD':'','updateCMD':'" + (cbUpdateRestart.Checked ? "restart" : "") +
                    "','updatePackage':'" + updaterInfo.Name + "','bagCount':" + bagCount + ",'MD5':'" + md5 + "','bagSize':" + bagSize + "}";
                
                if (clients.Count > 0)
                {
                    foreach (KeyValuePair<String, SocketClientManager> _scm in clients)
                    {
                        if (_scm.Value._socket.Connected)
                        {
                            //TODO 切割数据包
                            //逐台发送数据
                            //1.发送前导命令
                            _scm.Value.SendMsg(cmd);
                            //2.发送数据包
                            for (int i = 0; i < bagCount; i++)
                            {
                                if (i < (bagCount - 1))
                                {
                                    byte[] data = new byte[bagSize];
                                    fs.Read(data, 0, data.Length);
                                    //_scm.Value.SendMsg(data);

                                }
                                //尾包
                                else
                                {
                                    long data_length = updaterInfo.Length % bagSize;
                                    //如果尾包
                                    if (data_length.Equals(0L))
                                    {
                                        data_length = bagSize;
                                    }
                                    byte[] data = new byte[data_length];
                                    fs.Read(data, 0, data.Length);
                                    //_scm.Value.SendMsg(data);
                                }

                            }
                        }

                    }
                }
                else
                {
                    MessageBox.Show("请首先连接到服务器！");
                }
            }
            else
                MessageBox.Show("请选择更新包！");
        }

        /// <summary>
        /// 客户端文件传输子线程方法(上传方法)
        /// </summary>


        private void tbPort_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ExeConfigurationFileMap map = new ExeConfigurationFileMap();
                map.ExeConfigFilename = System.Environment.CurrentDirectory + "\\Service\\ProjectUpdater.exe.config";   ////（引号里面的是你的配置文件的在程序的绝对路径）。
                Configuration config = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);

                string configPort = config.AppSettings.Settings["port"].Value;
                if (!configPort.Equals(((TextBox)sender).Text))
                {
                    config.AppSettings.Settings["port"].Value = ((TextBox)sender).Text;
                    config.Save();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            for (int j = 0; j < chklbRemoteUpdater.Items.Count; j++)
                chklbRemoteUpdater.SetItemChecked(j, "全选".Equals(btnSelectAll.Text));
            btnSelectAll.Text = "全选".Equals(btnSelectAll.Text) ? "全否" : "全选";
        }

        private void btnAddRemote_Click(object sender, EventArgs e)
        {
            AddRemote form = new AddRemote();
            form.ShowDialog();
            refreshRemote();

        }
        private void refreshRemote()
        {
            ExeConfigurationFileMap map = new ExeConfigurationFileMap();
            map.ExeConfigFilename = System.Environment.CurrentDirectory + "\\ProjectUpdaterManager.exe.config";   ////（引号里面的是你的配置文件的在程序的绝对路径）。
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);

            KeyValueConfigurationElement ips = config.AppSettings.Settings["ip"];

            chklbRemoteUpdater.Items.Clear();
            if (ips != null && ips.Value != null)
            {
                string[] ip_tmp = ips.Value.Split(',');
                foreach (string ip in ip_tmp)
                {
                    if (!string.IsNullOrEmpty(ip))
                    {
                        chklbRemoteUpdater.Items.Add(ip, false);
                        if (!tabCmdView.TabPages.ContainsKey(ip))
                        {
                            TabPage tp = new TabPage();
                            tp.Name = ip;
                            tp.Text = ip;
                            TextBox remoteLog = new TextBox();
                            remoteLog.BackColor = System.Drawing.SystemColors.InactiveCaption;
                            remoteLog.Dock = System.Windows.Forms.DockStyle.Fill;
                            remoteLog.ForeColor = System.Drawing.SystemColors.HotTrack;
                            //remoteLog.Location = new System.Drawing.Point(3, 3);
                            remoteLog.Multiline = true;
                            remoteLog.Name = "tb" + ip;
                            //remoteLog.Size = new System.Drawing.Size(237, 284);
                            //remoteLog.TabIndex = 0;
                            remoteLog.ScrollBars = ScrollBars.Both;
                            remoteLog.Text = "";
                            tp.Controls.Add(remoteLog);
                            tabCmdView.TabPages.Add(tp);
                        }
                    }
                }
                List<string> ip_lst = ip_tmp.ToList();
                foreach (TabPage tp in tabCmdView.TabPages)
                {
                    if (!ip_lst.Contains(tp.Name))
                    {
                        tabCmdView.TabPages.Remove(tp);
                    }
                }
            }

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            //获取已选ip
            ExeConfigurationFileMap map = new ExeConfigurationFileMap();
            map.ExeConfigFilename = System.Environment.CurrentDirectory + "\\ProjectUpdaterManager.exe.config";   ////（引号里面的是你的配置文件的在程序的绝对路径）。
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);

            KeyValueConfigurationElement remote = config.AppSettings.Settings["ip"];
            string ips = "";
            if (remote != null)
                ips = remote.Value;
            foreach (var ip in chklbRemoteUpdater.CheckedItems)
            {
                ips = ips.Replace(ip.ToString(), "");
            }
            //如果中间剔掉ip将会产生双逗号或者首尾逗号
            ips = ips.Replace(",,", ",");
            if (ips.StartsWith(","))
                ips = ips.Substring(1);
            if (ips.EndsWith(","))
                ips = ips.Substring(0, ips.Length - 1);
            config.AppSettings.Settings["ip"].Value = ips;
            config.Save();
            refreshRemote();
        }
        private void RemoteTimer_Tick(object sender, EventArgs e)
        {
            foreach (KeyValuePair<String, SocketClientManager> _scm in clients)
            {
                if (_scm.Value._socket.Connected)
                {
                    string cmd = "{'resinCMD':'status','updateCMD':'','updatePackage':''}";
                    _scm.Value.SendMsg(cmd);
                }
            }
        }

        private void btnOpposite_Click(object sender, EventArgs e)
        {
            for (int j = 0; j < chklbRemoteUpdater.Items.Count; j++)
                chklbRemoteUpdater.SetItemChecked(j, !chklbRemoteUpdater.GetItemChecked(j));
        }


        private SocketClientManager InitSocket(String ip, int port, TextBox tbSocketLog)
        {
            tbSocketLog.Text = "";
            SocketClientManager _scm = new SocketClientManager(ip, port, tbSocketLog);
            _scm.OnReceiveMsg += OnReceiveMsg;
            _scm.OnConnected += OnConnected;
            _scm.OnFaildConnect += OnFaildConnect;
            _scm.Start();
            return _scm;
        }

        public void OnReceiveMsg(SocketClientManager scm)
        {
            byte[] buffer = scm.socketInfo.buffer;
            string msg = Encoding.UTF8.GetString(buffer).Replace("\0", "");
            if (string.IsNullOrEmpty(msg))
                return;
            else if (msg.StartsWith("{'status'"))
            {
                JObject ja = (JObject)JsonConvert.DeserializeObject(msg);
                string status = ja["status"].ToString();
                if (chklbRemoteUpdater.InvokeRequired)
                {
                    this.Invoke(new Action(() =>
                    {
                        for (int j = 0; j < chklbRemoteUpdater.Items.Count; j++)
                        {
                            string text = chklbRemoteUpdater.Items[j].ToString();
                            string ip = text.Substring(0, text.IndexOf("[") < 0 ? text.Length : text.IndexOf("["));
                            if (ip.Equals(scm.ip))
                                chklbRemoteUpdater.Items[j] = ip + "[" + status + "]";
                        }
                    }));
                }
            }
            else
            {
                if (scm.tbSocketLog.InvokeRequired)
                {
                    this.Invoke(new Action(() =>
                    {
                        scm.tbSocketLog.Text += GetDateNow() + "收到数据：" + msg + "\r\n";
                    }));
                }
            }
        }

        public void OnConnected(SocketClientManager scm)
        {
            Console.WriteLine(GetDateNow() + "连接服务器" + scm.ip + " : " + scm.port + "成功\r\n");
            string ipClient = scm._socket.LocalEndPoint.ToString().Split(':')[0];
            string posrClient = scm._socket.LocalEndPoint.ToString().Split(':')[1];
            if (scm.tbSocketLog.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    scm.tbSocketLog.Text += GetDateNow() + "连接服务器" + scm.ip + ":" + scm.port + "成功\r\n";
                    scm.tbSocketLog.Text += GetDateNow() + "当前连接：" + ipClient + ":" + posrClient + "\r\n";
                }));
            }
        }

        public void OnFaildConnect(SocketClientManager scm)
        {
            Console.WriteLine(GetDateNow() + "连接服务器" + scm.ip + " : " + scm.port + " 失败\r\n");
            if (scm.tbSocketLog.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    scm.tbSocketLog.Text += GetDateNow() + "连接服务器" + scm.ip + " : " + scm.port + " 失败\r\n";
                }));
            }
        }

        public string GetDateNow()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff  ");
        }

        private void btnConnectRemote_Click(object sender, EventArgs e)
        {
            clients.Clear();
            //逐台进行连接
            foreach (string ip in chklbRemoteUpdater.CheckedItems)
            {
                clients.Add(ip, InitSocket(ip, Int32.Parse(tbPort.Text), ((TextBox)this.Controls.Find("tb" + ip, true)[0])));
            }
            //一旦产生连接，即停用连接按钮同时停用复选框
            ((Button)sender).Enabled = false;
            btnDisConnectRemote.Enabled = true;
            chklbRemoteUpdater.Enabled = false;
        }

        private void btnDisConnectRemote_Click(object sender, EventArgs e)
        {
            ((Button)sender).Enabled = false;
            chklbRemoteUpdater.Enabled = true;
            btnConnectRemote.Enabled = true;
            foreach (KeyValuePair<String, SocketClientManager> _scm in clients)
            {
                if (_scm.Value._socket.Connected)
                {
                    _scm.Value._isConnected = false;
                    _scm.Value.SendMsg("\0\0\0faild");
                    _scm.Value.tbSocketLog.Text += GetDateNow() + "断开中......\r\n";
                    _scm.Value._socket.Shutdown(System.Net.Sockets.SocketShutdown.Both);
                    _scm.Value._socket.Close();
                    _scm.Value.tbSocketLog.Text += GetDateNow() + "已经断开连接。\r\n";

                }
            }
            clients.Clear();
        }
        /**
         * 启动服务，要求线连接服务器，再进行启动服务
         * 命令规则：
         * 第一个字节为命令字节数组长度
         * 截掉命令字节数组后的字节数组为更新包
         * 
         **/
        private void btnStartSelected_Click(object sender, EventArgs e)
        {
            SendResinCmd("start");
        }

        private void btnStopSelected_Click(object sender, EventArgs e)
        {
            SendResinCmd("stop");
        }

        /**
         * 向远程服务发送命令
         * resinCMD：
         * start|stop|restart|status
         **/
        private void SendResinCmd(String ResinCmd)
        {

            if (clients.Count > 0)
            {
                
                foreach (KeyValuePair<String, SocketClientManager> _scm in clients)
                {
                    if (_scm.Value._socket.Connected)
                    {
                        string cmd = "{'resinCMD':'" + ResinCmd + "','updateCMD':'','updatePackage':''}";
                        _scm.Value.SendMsg(cmd);
                    }
                }

            }
            else
            {
                MessageBox.Show("请首先连接到服务器！");
            }
        }
        public static string GetMD5Hash(Stream inputStream)
        {
            try
            {
                System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(inputStream);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("GetMD5Hash() fail,error:" + ex.Message);
            }
        }
    }
}
