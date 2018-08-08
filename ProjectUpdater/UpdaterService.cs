using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace ProjectUpdater
{
    public partial class UpdaterService : ServiceBase
    {
        SocketManager _sm = null;

        StreamWriter sw = new StreamWriter(@System.AppDomain.CurrentDomain.BaseDirectory + "\\Updater.log", true);

        private const String RESIN_SERVICE_NAME = "resin";
        public UpdaterService()
        {
            Console.SetOut(sw);
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                Console.WriteLine(GetDateNow() + "  " + "服务启动中...");
                //获取ip、port 并启动服务
                ExeConfigurationFileMap map = new ExeConfigurationFileMap();
                map.ExeConfigFilename = System.AppDomain.CurrentDomain.BaseDirectory + "\\ProjectUpdater.exe.config";   ////（引号里面的是你的配置文件的在程序的绝对路径）。
                Configuration config = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);
                int configPort = Int32.Parse(config.AppSettings.Settings["port"].Value);
                _sm = new SocketManager(configPort);
                _sm.OnReceiveMsg += OnReceiveMsg;
                _sm.OnConnected += OnConnected;
                _sm.OnDisConnected += OnDisConnected;
                _sm.Start();
                Console.WriteLine(GetDateNow() + "  " + "服务启动成功");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }
        protected override void OnStop()
        {
            Console.WriteLine(GetDateNow() + "  " + "服务关闭中...");
            _sm.Stop();
            Console.WriteLine(GetDateNow() + "  " + "服务已经关闭");
            sw.Flush();
            sw.Close();
        }


        public void OnReceiveMsg(string ip)
        {
            byte[] buffer = _sm._listSocketInfo[ip].msgBuffer;
            if (_sm._complete)
            {

                //获取命令
                string command = Encoding.UTF8.GetString(buffer);
                JObject ja = (JObject)JsonConvert.DeserializeObject(command);
                if (ja == null)
                    return;
                string updatePackage = (ja["updatePackage"] == null) ? "" : ja["updatePackage"].ToString();
                int bagCount = (ja["bagCount"] == null) ? 0 : Int32.Parse(ja["bagCount"].ToString());
                string md5 = (ja["md5"] == null) ? "" : ja["md5"].ToString();
                int bagSize = (ja["bagSize"] == null) ? 0 : Int32.Parse(ja["bagSize"].ToString());
                string updateCMD = (ja["updateCMD"] == null) ? "" : ja["updateCMD"].ToString();
                //依赖 bagCount 判定是否含有数据包
                if (bagCount > 0)
                {
                    _sm._complete = false;
                    _sm._ja = ja;
                    //处理更新包
                    string patchPath = System.AppDomain.CurrentDomain.BaseDirectory + "\\Patch\\"
                        + DateTime.Now.ToString("yyyy-MM-dd HH.mm.ss") + "\\";
                    //判断文件路径是否存在，不存在则创建文件夹 
                    if (!System.IO.Directory.Exists(@patchPath))
                    {
                        System.IO.Directory.CreateDirectory(@patchPath);//不存在就创建目录 
                    }
                    _sm._patchFS = new FileStream(patchPath + updatePackage, FileMode.OpenOrCreate, FileAccess.Write);
                }
                //如果没有数据包，则直接执行命令
                else
                {
                    //_sm.SendMsg("\r\n收到命令：" + command, ip);
                    //执行命令
                    switch (ja["resinCMD"].ToString())
                    {
                        case "start":
                            StartResin();
                            break;
                        case "stop":
                            StopResin();
                            break;
                        case "restart":
                            StopResin();
                            StartResin();
                            break;
                        default:
                            ServiceController serviceController = new ServiceController(RESIN_SERVICE_NAME);
                            string status = new ServiceController(RESIN_SERVICE_NAME).Status.ToString();
                            _sm.SendMsg("{'status':'" + status + "'}", ip);
                            break;
                    }
                }


            }
            //更新动作
            else if (!_sm._complete && _sm._ja != null)
            {
                JObject ja = _sm._ja;
                int bagCount = (ja["bagCount"] == null) ? 0 : Int32.Parse(ja["bagCount"].ToString());
                if (bagCount <= ++_sm._completeStep)
                {
                    //处理命令
                    string updatePackage = (ja["updatePackage"] == null) ? "" : ja["updatePackage"].ToString();
                    string md5 = (ja["md5"] == null) ? "" : ja["md5"].ToString();
                    int bagSize = (ja["bagSize"] == null) ? 0 : Int32.Parse(ja["bagSize"].ToString());
                    string updateCMD = (ja["updateCMD"] == null) ? "" : ja["updateCMD"].ToString();

                    //接收更新
                    byte[] data = new byte[bagSize];
                    if (_sm._patchFS != null && _sm._patchFS.CanWrite)
                        _sm._patchFS.Write(data, 0, data.Length);
                    if (bagCount == _sm._completeStep)
                    {
                        //执行更新前清理工作
                        _sm._complete = true;
                        _sm._ja = null;
                        _sm._completeStep = 0;
                        _sm._patchFS.Close();
                        _sm._patchFS = null;
                        //TODO 执行更新
                    }
                }
            }
            //获取更新

            //if (hasUpdater)
            //{
            //    byte[] updater = buffer.Skip(cmdlength + 1).ToArray<byte>();
            //    bool restart = "restart".Equals(ja["updateCMD"].ToString());
            //    PathUpdate(updater, ja["updatePackage"].ToString(), restart, ip);
            //}
        }

        private void StopResin(string ip)
        {
            try
            {
                ServiceController serviceController = new ServiceController(RESIN_SERVICE_NAME);
                if (serviceController.CanStop)
                    serviceController.Stop();
                do
                {
                    //等服务彻底关闭后继续执行，否则可能产生文件占用
                    Console.WriteLine("等待Resin彻底停止..." + new ServiceController(RESIN_SERVICE_NAME).Status.ToString());
                    System.Threading.Thread.Sleep(500);
                } while (!"Stopped".Equals(new ServiceController(RESIN_SERVICE_NAME).Status.ToString()));
            }
            catch (Exception ex)
            {
                string exmsg = "停止Resin异常：" + ex.StackTrace;
                Console.WriteLine(exmsg);
                _sm.SendMsg(exmsg, ip);
            }
        }
        private void StartResin(string ip)
        {
            try
            {
                ServiceController serviceController = new ServiceController(RESIN_SERVICE_NAME);
                if ("Stopped".Equals(serviceController.Status.ToString()))
                    serviceController.Start();
            }
            catch (Exception ex)
            {
                string exmsg = "启动Resin错误：" + ex.StackTrace;
                Console.WriteLine(exmsg);
                _sm.SendMsg(exmsg, ip);
            }
        }
        /**
         * 接收更新包并且执行更新
         **/
        private void PathUpdate(byte[] updater, string SendFileName, bool restart, string ip)
        {


            _sm.SendMsg("\r\n收到更新文件:" + SendFileName, ip);
            //处理更新包
            string patchPath = System.AppDomain.CurrentDomain.BaseDirectory + "\\Patch\\" + DateTime.Now.ToString("yyyy-MM-dd HH.mm.ss") + "\\";
            //判断文件路径是否存在，不存在则创建文件夹 
            if (!System.IO.Directory.Exists(@patchPath))
            {
                System.IO.Directory.CreateDirectory(@patchPath);//不存在就创建目录 
            }
            patchPath += SendFileName;
            FileStream MyFileStream = new FileStream(patchPath, FileMode.Create, FileAccess.Write);
            MyFileStream.Write(updater, 0, updater.Length);
            MyFileStream.Close();
            //执行更新
            //解压文件
            string unzipPath = patchPath.Substring(0, patchPath.LastIndexOf(".zip"));
            UnZip(patchPath, unzipPath);
            string patchCmd = unzipPath + "\\patch.cmd";
            if (File.Exists(patchCmd))
            {
                //执行更新动作

                //停止服务
                if (restart)
                    StopResin(ip);
                //执行更新动作
                string CurrentDirectory = System.Environment.CurrentDirectory;
                System.Environment.CurrentDirectory = unzipPath;
                Process process = new Process();
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.FileName = "patch.cmd";
                process.StartInfo.CreateNoWindow = true;
                process.Start();
                System.Environment.CurrentDirectory = CurrentDirectory;
                string updateMsg = "更新成功";
                Console.WriteLine(updateMsg);
                _sm.SendMsg(updateMsg, ip);
                //启动服务

                if (restart)
                    StartResin(ip);
            }
            else
            {
                string exmsg = "更新包内未找到patch.cmd";
                Console.WriteLine(exmsg);
                _sm.SendMsg(exmsg, ip);
            }
            _sm.SendMsg("更新完毕", ip);

        }
        public void OnConnected(string clientIP)
        {
            string ipstr = clientIP.Split(':')[0];
            string portstr = clientIP.Split(':')[1];
            Console.WriteLine(GetDateNow() + "  " + clientIP + "已连接至本机\r\n");
            _sm.SendMsg("你已经连接到我：" + clientIP, clientIP);
        }

        public void OnDisConnected(string clientIp)
        {
            Console.WriteLine(GetDateNow() + "  " + clientIp + "已经断开连接！\r\n");
        }
        public void SendMsgLog(string msg, string ipClient)
        {
            Console.WriteLine(GetDateNow() + "  " + "[发送" + ipClient + "]  " + msg + "\r\n\r\n");
        }

        public void ReceiveMsgLog(string msg, string ipClient)
        {
            Console.WriteLine(GetDateNow() + "  " + "[接收" + ipClient + "]  " + msg + "\r\n\r\n");

        }

        private void StopResin()
        {
            try
            {
                ServiceController serviceController = new ServiceController(RESIN_SERVICE_NAME);
                if (serviceController.CanStop)
                    serviceController.Stop();
                do
                {
                    //等服务彻底关闭后继续执行，否则可能产生文件占用
                    Console.WriteLine("等待Resin彻底停止..." + new ServiceController(RESIN_SERVICE_NAME).Status.ToString());
                    System.Threading.Thread.Sleep(500);
                } while (!"Stopped".Equals(new ServiceController(RESIN_SERVICE_NAME).Status.ToString()));
            }
            catch (Exception ex)
            {
                Console.WriteLine("停止Resin异常：" + ex.StackTrace);
            }
        }
        private void StartResin()
        {
            try
            {
                ServiceController serviceController = new ServiceController(RESIN_SERVICE_NAME);
                if ("Stopped".Equals(serviceController.Status.ToString()))
                    serviceController.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine("启动Resin错误：" + ex.StackTrace);
            }
        }
        private void RestartResin()
        {
            StopResin();
            StartResin();
        }

        #region  解压文件 .zip文件

        /// <summary>
        /// 解压功能(解压压缩文件到指定目录)
        /// </summary>
        /// <param name="FileToUpZip">待解压的文件</param>
        /// <param name="ZipedFolder">指定解压目标目录</param>
        public static void UnZip(string FileToUpZip, string ZipedFolder)
        {
            if (!File.Exists(FileToUpZip))
            {
                return;
            }

            if (!Directory.Exists(ZipedFolder))
            {
                Directory.CreateDirectory(ZipedFolder);
            }

            ICSharpCode.SharpZipLib.Zip.ZipInputStream s = null;
            ICSharpCode.SharpZipLib.Zip.ZipEntry theEntry = null;

            string fileName;
            FileStream streamWriter = null;
            try
            {
                s = new ICSharpCode.SharpZipLib.Zip.ZipInputStream(File.OpenRead(FileToUpZip));
                while ((theEntry = s.GetNextEntry()) != null)
                {

                    if (theEntry.Name != String.Empty)
                    {
                        fileName = Path.Combine(ZipedFolder, theEntry.Name);
                        ///判断文件路径是否是文件夹

                        if (fileName.EndsWith("/") || fileName.EndsWith("\\"))
                        {
                            Directory.CreateDirectory(fileName);
                            continue;
                        }

                        streamWriter = File.Create(fileName);
                        int size = 2048;
                        byte[] data = new byte[2048];
                        while (true)
                        {
                            size = s.Read(data, 0, data.Length);
                            if (size > 0)
                            {
                                streamWriter.Write(data, 0, size);
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
            }
            finally
            {
                if (streamWriter != null)
                {
                    streamWriter.Close();
                    streamWriter = null;
                }
                if (theEntry != null)
                {
                    theEntry = null;
                }
                if (s != null)
                {
                    s.Close();
                    s = null;
                }
                GC.Collect();
                GC.Collect(1);
            }
        }


        #endregion

        private bool CheckZIP(byte[] buffer)
        {
            Console.WriteLine(Convert.ToInt32(buffer[0]) == 80);
            if (buffer.Length > 3)
            {
                if (Convert.ToInt32(buffer[0]).Equals(80) &&
                    Convert.ToInt32(buffer[1]).Equals(75) &&
                    Convert.ToInt32(buffer[2]).Equals(3))
                {
                    return true;
                }
            }
            return false;
        }
        public string GetDateNow()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        }

    }
}
