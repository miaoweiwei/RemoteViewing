#region License
/*
RemoteViewing VNC Client/Server Library for .NET
Copyright (c) 2013 James F. Bellinger <http://www.zer7.com/software/remoteviewing>
All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are met: 

1. Redistributions of source code must retain the above copyright notice, this
   list of conditions and the following disclaimer. 
2. Redistributions in binary form must reproduce the above copyright notice,
   this list of conditions and the following disclaimer in the documentation
   and/or other materials provided with the distribution. 

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR
ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
(INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/
#endregion

using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using System.Windows.Forms;
using RemoteViewing.Vnc;
using RemoteViewing.Vnc.Server;
using RemoteViewing.Windows.Forms.Server;

namespace RemoteViewing.ServerExample
{
    class Program
    {
        static string Password = "123456";
        static int  Port=5901;

        [STAThread]
        static void Main(string[] args)
        {
            StartVncService();

            Application.Run();

            Console.WriteLine("输入\"Q\"键退出。");
            ConsoleKey key = Console.ReadKey(true).Key;
            while (key != ConsoleKey.Q)
            {
                key = Console.ReadKey(true).Key;
            }
            Console.WriteLine("退出程序！。");
            CloseVncService();
        }
        /// <summary> 开启C#版VNC服务 </summary>
        public static void StartVncService()
        {
            RemoteViewService remoteViewService = RemoteViewService.GetVncService();
            remoteViewService.ServicePort = Port;
            remoteViewService.Password = Password;
            remoteViewService.BacklogMaxCount = 2;
            if (!remoteViewService.IsVncServiceRun)
                remoteViewService.StartListener();
        }
        /// <summary> 关闭C#版VNC服务 </summary>
        public static void CloseVncService()
        {
            RemoteViewService remoteViewService = RemoteViewService.GetVncService();
            remoteViewService.StopListener();
        }
    }
}
