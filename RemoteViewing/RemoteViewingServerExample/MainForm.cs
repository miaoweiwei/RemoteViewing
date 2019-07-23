using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace RemoteViewingServerExample
{
    public partial class MainForm : Form
    {
        private string Password = "123456";
        private  int Port = 5901;
        private bool isOpenVncService=false;

        private Thread VncTh;

        public MainForm()
        {
            InitializeComponent();

            if (!isOpenVncService)
                btnStartVNCservice.Text = "开启VNC服务";
            else
                btnStartVNCservice.Text = "关闭VNC服务";
        }


        private void btnStartVNCservice_Click(object sender, EventArgs e)
        {
            if (!isOpenVncService)
            {
                StartVncService();
                isOpenVncService = true;
                btnStartVNCservice.Text = "关闭VNC服务";
            }
            else
            {
                CloseVncService();
                isOpenVncService = false;
                btnStartVNCservice.Text = "开启VNC服务";
            }
        }

        /*************************************/
        /// <summary> 开启C#版VNC服务 </summary>
        private void StartVncService()
        {
            //if (VncTh != null)
            //{
            //    if (VncTh.IsAlive)
            //    {
            //        MessageBox.Show(this, "服务已经启动！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        return;
            //    }
            //    VncTh.DisableComObjectEagerCleanup();
            //    VncTh = null;
            //}
            //VncTh = new Thread(() =>
            //{
            //    RemoteViewService remoteViewService = RemoteViewService.GetVncService();
            //    remoteViewService.ServicePort = Port;
            //    remoteViewService.Password = Password;
            //    remoteViewService.BacklogMaxCount = 2;
            //    remoteViewService.AddLogEvent += RemoteViewService_AddLogEvent;
            //    if (!remoteViewService.IsVncServiceRun)
            //        remoteViewService.StartListener();
            //    VncTh.Interrupt();
            //});
            //VncTh.IsBackground = true;
            //VncTh.Start();
            RemoteViewService remoteViewService = RemoteViewService.GetVncService();
            remoteViewService.ServicePort = Port;
            remoteViewService.Password = Password;
            remoteViewService.BacklogMaxCount = 2;
            remoteViewService.AddLogEvent += RemoteViewService_AddLogEvent;
            if (!remoteViewService.IsVncServiceRun)
                remoteViewService.StartListener();
        }

        private void RemoteViewService_AddLogEvent(string logTxt)
        {
            if (this.InvokeRequired)
            {
                RemoteViewService.AddLogDelegate d = new RemoteViewService.AddLogDelegate(RemoteViewService_AddLogEvent);
                this.BeginInvoke(d, new object[] {logTxt});
            }
            else
            {
                string msg = string.Format("当前系统时间：{0} ", DateTime.Now.ToString("s"));
                msg = msg + "【" + logTxt + "】 " + Environment.NewLine;
                rtbxLog.AppendText(msg);
                rtbxLog.Focus();
            }
        }

        /// <summary> 关闭C#版VNC服务 </summary>
        private void CloseVncService()
        {
            Thread thread=new Thread(() =>
            {
                RemoteViewService remoteViewService = RemoteViewService.GetVncService();
                remoteViewService.StopListener();
                remoteViewService.AddLogEvent -= RemoteViewService_AddLogEvent;
            });
            thread.IsBackground = true;
            thread.Start();
        }

        private void btnClearLog_Click(object sender, EventArgs e)
        {
            rtbxLog.Text = "";
            rtbxLog.Focus();
        }
    }
}
