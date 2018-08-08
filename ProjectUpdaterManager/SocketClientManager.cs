using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectUpdaterManager
{
    public class SocketClientManager
    {
        public Socket _socket = null;
        public EndPoint endPoint = null;
        public SocketInfo socketInfo = null;
        public bool _isConnected = false;
        public String ip = null;
        public int port = 90;
        public TextBox tbSocketLog = null;

        public delegate void OnConnectedHandler(SocketClientManager scm);
        public event OnConnectedHandler OnConnected;
        public event OnConnectedHandler OnFaildConnect;
        public delegate void OnReceiveMsgHandler(SocketClientManager scm);
        public event OnReceiveMsgHandler OnReceiveMsg;

        public SocketClientManager(string sip, int sport, TextBox tbSocketLog)
        {
            this.ip = sip;
            this.port = sport;
            this.tbSocketLog = tbSocketLog;
            IPAddress _ip = IPAddress.Parse(ip);
            endPoint = new IPEndPoint(_ip, port);
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void Start()
        {
            _socket.BeginConnect(endPoint, ConnectedCallback, _socket);
            _isConnected = true;
            Thread socketClient = new Thread(SocketClientReceive);
            socketClient.IsBackground = true;
            socketClient.Start();
        }

        public void SocketClientReceive()
        {
            while (_isConnected)
            {
                SocketInfo info = new SocketInfo();
                try
                {
                    _socket.BeginReceive(info.buffer, 0, info.buffer.Length, SocketFlags.None, ReceiveCallback, info);
                }
                catch (SocketException ex)
                {
                    _isConnected = false;
                    Console.WriteLine(ex.StackTrace);
                }

                Thread.Sleep(100);
            }
        }

        public void ReceiveCallback(IAsyncResult ar)
        {
            socketInfo = ar.AsyncState as SocketInfo;
            if (this.OnReceiveMsg != null) OnReceiveMsg(this);
        }

        public void ConnectedCallback(IAsyncResult ar)
        {
            Socket socket = ar.AsyncState as Socket;
            if (socket.Connected)
            {
                if (this.OnConnected != null) OnConnected(this);
            }
            else
            {
                if (this.OnFaildConnect != null) OnFaildConnect(this);
            }
        }

        public void SendMsg(string msg)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(msg);
            _socket.Send(buffer);
        }
        public void SendMsg(byte[] buffer)
        {
            _socket.Send(buffer);
        }

        public class SocketInfo
        {
            public Socket socket = null;
            public byte[] buffer = null;

            public SocketInfo()
            {
                buffer = new byte[1024 * 4];
            }
        }
      
    }
}
