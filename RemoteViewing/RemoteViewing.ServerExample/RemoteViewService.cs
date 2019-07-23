using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using RemoteViewing.Utility;
using RemoteViewing.Vnc;
using RemoteViewing.Vnc.Server;
using RemoteViewing.Windows.Forms;
using RemoteViewing.Windows.Forms.Server;

namespace RemoteViewing.ServerExample
{
    /// <summary> 同屏服务端 </summary>
    public class RemoteViewService
    {
        private string password = "123456";
        /// <summary> 连接密码默认12346 </summary>
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        private int servicePort = 5901;
        /// <summary> 连接端口默认5901 </summary>
        public int ServicePort
        {
            get { return servicePort; }
            set { servicePort = value; }
        }

        private int backlog = 3;
        /// <summary> 连接的最大个数，连接越多越占CPU，默认为3个，建议5个之内 </summary>
        public int BacklogMaxCount
        {
            get { return backlog; }
            set { backlog = value; }
        }

        private Thread VncServiceThread;

        /// <summary> 同屏服务端是否启动 </summary>
        public bool IsVncServiceRun
        {
            get
            {
                if (VncServiceThread != null)
                    return VncServiceThread.IsAlive;
                return false;
            }
        }

        private List<VncServerSession> sessionList;
        /// <summary> 会话列表 </summary>
        public List<VncServerSession> SessionList
        {
            get { return sessionList; }
        }

        private bool isStartWhiteBoard = false;
        private WhiteBoardForm whiteBoard;


        private TcpListener tcpListener;
        private AsyncCallback connectCallBack;

        #region 单例

        private static RemoteViewService _singleton;
        private static readonly object SyncRoot = new object();
        /// <summary> 同屏服务端初始化 </summary>
        private RemoteViewService()
        {
            connectCallBack = new AsyncCallback(AcceptTcpClient);
            sessionList = new List<VncServerSession>();
            //初始化白板
            whiteBoard = WhiteBoardForm.GetWhiteBoardForm();
        }
        public static RemoteViewService GetVncService()
        {
            if (_singleton == null)
            {
                lock (SyncRoot)
                {
                    if (_singleton == null)
                    {
                        _singleton = new RemoteViewService();
                    }
                }
            }
            return _singleton;
        }

        #endregion

        private void VncListener()
        {
            try
            {
                string localIp = GetLocalIpAddress();
                Console.WriteLine($"开启VNC服务，IP:{localIp},端口:{this.servicePort}，密码:{this.password}");
                tcpListener = new TcpListener(IPAddress.Any, this.servicePort);
                tcpListener.Start(this.backlog);
                //tcpListener.BeginAcceptTcpClient(connectCallBack, tcpListener);
                while (true)
                {
                    TcpClient tcpClient = tcpListener.AcceptTcpClient();
                    ConnectService(tcpClient);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"开启VNC服务失败，原因：{ex.Message}");
            }
        }

        /// <summary> 接入一个异步连接 </summary>
        private void AcceptTcpClient(IAsyncResult ar)
        {
            TcpListener listener = ar.AsyncState as TcpListener;
            TcpClient tcpClient = listener.EndAcceptTcpClient(ar);
            ConnectService(tcpClient);
        }

        private void ConnectService(TcpClient tcpClient)
        {
            try
            {
                string loalIp = GetLocalIpAddress();

                IPEndPoint remotePoint = tcpClient.Client.RemoteEndPoint as IPEndPoint;
                Console.WriteLine($"VNC连接接入,客户端ip:{remotePoint.Address},端口:{remotePoint.Port}");

                VncServerSessionOptions options = new VncServerSessionOptions();
                //设置需要密码验证
                options.AuthenticationMethod = AuthenticationMethod.Password;

                // 创建一个会话
                VncServerSession session = new VncServerSession();

                session.Connected += HandleConnected;
                session.ConnectionFailed += HandleConnectionFailed;
                session.Closed += HandleClosed;
                session.PasswordProvided += HandlePasswordProvided;
                session.CreatingDesktop += Session_CreatingDesktop;
                session.KeyChanged += Session_KeyChanged;
                session.PointerChanged += Session_PointerChanged;
                session.RemoteClipboardChanged += Session_RemoteClipboardChanged;
                //设置分享的屏幕等信息 连接名字 name
                string name = loalIp + ":" + this.servicePort + "--" + remotePoint.Address + ":" + remotePoint.Port;
                VncScreenFramebufferSource vncScreenSource = new VncScreenFramebufferSource(name, Screen.PrimaryScreen);
                session.SetFramebufferSource(vncScreenSource);
                session.Connect(tcpClient, options);

                sessionList.Add(session);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"连接出现错误，原因：{ex.Message}");
            }
        }

        private void Session_RemoteClipboardChanged(object sender, RemoteClipboardChangedEventArgs e)
        {
            //借用共享剪切板的通信，并没有去更新本地剪切板。
            if (e.Contents == "StartWhiteBoard")
            {
                isStartWhiteBoard = true;
                whiteBoard.ShowWhiteBoard();
                Console.WriteLine("开始白板！");
            }
            else if (e.Contents == "TheTimeDrawEnd")
            {
                if (isStartWhiteBoard)
                {
                    whiteBoard.TheTimeDrawEnd();
                    Console.WriteLine("结束本次画图！");
                }
            }else if (e.Contents == "ClearWhiteBoard")
            {
                if (isStartWhiteBoard)
                {
                    whiteBoard.ClearWhiteBoard();
                    Console.WriteLine("清空白板！");
                }
            }
            else if (e.Contents == "CloseWhiteBoard")
            {
                isStartWhiteBoard = false;
                whiteBoard.ClearWhiteBoard();
                whiteBoard.HideWhiteBoard();
                Console.WriteLine("关闭白板！");
            }
        }

        private void Session_PointerChanged(object sender, PointerChangedEventArgs e)
        {
            Console.WriteLine($"鼠标事件,X:{e.X},Y:{e.Y},PressedButtons:{e.PressedButtons}");
            //点击鼠标的左键的同时移动鼠标才可以画图
            if (isStartWhiteBoard && whiteBoard != null && !whiteBoard.IsDisposed)
                if (e.PressedButtons == 1)
                    whiteBoard.DrawTrack(new Point(e.X, e.Y));
        }






        private void Session_KeyChanged(object sender, KeyChangedEventArgs e)
        {
            Keys key = (Keys)e.Keysym;
            Console.WriteLine($"按键事件，Keysym:{e.Keysym}, Pressed:{e.Pressed}, 按键:{key}");
        }

        private void Session_CreatingDesktop(object sender, CreatingDesktopEventArgs e)
        {

        }

        private void HandleConnected(object sender, ConnectEventArgs e)
        {
            IPEndPoint localpPoint = e.CurrentTcpClient.Client.LocalEndPoint as IPEndPoint;
            IPEndPoint remotePoint = e.CurrentTcpClient.Client.RemoteEndPoint as IPEndPoint;
            Console.WriteLine($"连接成功:{localpPoint.Address}:{localpPoint.Port}-->{remotePoint.Address}:{remotePoint.Port}");
        }

        private void HandlePasswordProvided(object sender, PasswordProvidedEventArgs e)
        {
            bool flag = e.Accept(this.password.ToCharArray());
        }

        private void HandleClosed(object sender, ConnectEventArgs e)
        {
            IPEndPoint localpPoint = e.CurrentTcpClient.Client.LocalEndPoint as IPEndPoint;
            IPEndPoint remotePoint = e.CurrentTcpClient.Client.RemoteEndPoint as IPEndPoint;
            Console.WriteLine($"连接关闭:{localpPoint.Address}:{localpPoint.Port} X {remotePoint.Address}:{remotePoint.Port}");
        }

        private void HandleConnectionFailed(object sender, ConnectEventArgs e)
        {
            IPEndPoint localpPoint = e.CurrentTcpClient.Client.LocalEndPoint as IPEndPoint;
            IPEndPoint remotePoint = e.CurrentTcpClient.Client.RemoteEndPoint as IPEndPoint;
            Console.WriteLine($"连接失败:{localpPoint.Address}:{localpPoint.Port} X {remotePoint.Address}:{remotePoint.Port}");
        }

        private string GetLocalIpAddress()
        {
            string loalIp = "127.0.0.1";
            try
            {
                IPAddress[] localIps = Dns.GetHostAddresses(Dns.GetHostName());
                foreach (IPAddress address in localIps)
                {
                    if (!address.IsIPv6LinkLocal)
                    {
                        if (!address.ToString().Contains("169.254"))
                        {
                            loalIp = address.ToString();
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"获取本地IP出错，原因：{ex.Message}");
            }
            return loalIp;
        }
        
        public void StartListener()
        {
            if (VncServiceThread == null)
            {
                VncServiceThread = new Thread(VncListener);
                VncServiceThread.IsBackground = true;
                VncServiceThread.Start();
            }
            else if (VncServiceThread.IsAlive)
            {
                Console.WriteLine("VNC服务已经启动请勿重复启动！");
            }
        }

        public void StopListener()
        {
            if (VncServiceThread != null && VncServiceThread.IsAlive)
            {
                VncServiceThread.Abort();
                VncServiceThread.DisableComObjectEagerCleanup();
                VncServiceThread = null;
            }
        }
    }
}
