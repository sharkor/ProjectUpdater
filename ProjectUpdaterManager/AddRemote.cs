using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectUpdaterManager
{
    public partial class AddRemote : Form
    {
        public AddRemote()
        {
            InitializeComponent();
        }

        private void btnPing_Click(object sender, EventArgs e)
        {
            if (iacRemote.Text.Length < 7)
            {
                MessageBox.Show("请完善IP信息");
                return;
            }
            Ping ping = new Ping();
            PingReply pingReply = ping.Send(iacRemote.Text);
            if (pingReply.Status == IPStatus.Success)
            {
                MessageBox.Show("当前在线，已ping通！");
            }
            else
            {
                MessageBox.Show("不在线，ping不通！");
            }
        }

        private void btnTelnet_Click(object sender, EventArgs e)
        {
            ExeConfigurationFileMap map = new ExeConfigurationFileMap();
            map.ExeConfigFilename = System.Environment.CurrentDirectory + "\\Service\\ProjectUpdater.exe.config";
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);
            string configPort = config.AppSettings.Settings["port"].Value;
            try
            {

                Telnet p = new Telnet(iacRemote.Text, Int32.Parse(configPort), 50);

                if (p.Connect() == false)
                {
                    MessageBox.Show("连接失败");
                    return;
                }
                MessageBox.Show("连接成功");

            }
            catch (Exception ex)
            {
                MessageBox.Show("连接失败," + ex.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ExeConfigurationFileMap map = new ExeConfigurationFileMap();
            map.ExeConfigFilename = System.Environment.CurrentDirectory + "\\ProjectUpdaterManager.exe.config";   ////（引号里面的是你的配置文件的在程序的绝对路径）。
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);
            var ip = config.AppSettings.Settings["ip"];
            if(iacRemote.Text.Length<7)
            {
                MessageBox.Show("请填写完整的IP！");
                return;
            }
            else if (!string.IsNullOrEmpty(ip + "") &&
                (config.AppSettings.Settings["ip"].Value+",").IndexOf(iacRemote.Text+",") >= 0)
            {
                MessageBox.Show("IP:" + iacRemote.Text + "已经存在！");
                return;
            }
            config.AppSettings.Settings.Add("ip", iacRemote.Text);
            config.Save();
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
