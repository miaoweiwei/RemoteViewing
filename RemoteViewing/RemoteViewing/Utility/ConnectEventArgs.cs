using System;
using System.Net.Sockets;
using RemoteViewing.Vnc;

namespace RemoteViewing.Utility
{
    /// <summary> 连接事件参数 </summary>
    public class ConnectEventArgs : EventArgs
    {
        public ConnectEventArgs(TcpClient currentTcpClient, VncFramebuffer currentVncFramebuffer)
        {
            CurrentTcpClient = currentTcpClient;
            CurrentVncFramebuffer = currentVncFramebuffer;
        }

        /// <summary> 当前连接的TCP </summary>
        public TcpClient CurrentTcpClient { get; }
        /// <summary> 当前连接存储VNC会话的图像数据。 </summary>
        public VncFramebuffer CurrentVncFramebuffer { get; }
    }
}