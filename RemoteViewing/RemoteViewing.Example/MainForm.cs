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
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Windows.Forms;
using Newtonsoft.Json;
using RemoteViewing.Vnc;
using RemoteViewing.Windows.Forms;
using Microsoft.CSharp;
using Newtonsoft.Json.Linq;

namespace RemoteViewing.Example
{
    public partial class MainForm : Form
    {
        private bool isStartWhiteBoard = false;
        private bool isMouseLeftPress = false;

        private Color selectColor = Color.Red;


        /// <summary>
        /// 服务端屏幕的尺寸
        /// </summary>
        private Size remoteScreenSize=Size.Empty;
        /// <summary>
        /// 是否已经连接
        /// </summary>
        private bool isConnect
        {
            get {return vncControl.Client.IsConnected; }
        }

        public MainForm()
        {
            InitializeComponent();

            this.btnLineColor.BackColor = selectColor;

            this.ResizeBegin += (sObj, eArgs) => this.SuspendLayout();
            this.ResizeEnd += (sObj, eArgs) => this.ResumeLayout(true);
            this.ResizeEnd += (sObj, eArgs) => { Refresh(); };
            this.SizeChanged += (s, o) => { Refresh(); };

            vncControl.AllowRemoteCursor = false;//显示鼠标
            vncControl.AllowClipboardSharingFromServer = false;//不让程序自动向服务端发送剪切板
            vncControl.AllowClipboardSharingToServer = false;//不让程序自动接收服务端发送的剪切板
            vncControl.AllowInput = false;//是否发送鼠标事件相关信息
            
            vncControl.MouseMove += VncControl_MouseMove;
            vncControl.MouseDown += VncControl_MouseDown;
            vncControl.MouseUp += VncControl_MouseUp;
            vncControl.MouseLeave += VncControl_MouseLeave;

            vncControl.MouseWheel += VncControl_MouseWheel;

            vncControl.KeyDown += VncControl_KeyDown;
            vncControl.KeyUp += VncControl_KeyUp;

            ButtonStatusSet();
        }

        #region 画图控制

        private void btnWhiteBoard_Click(object sender, EventArgs e)
        {
            string OperateMsg = "";
            if (!isStartWhiteBoard)
            {
                isStartWhiteBoard = true;
                OperateMsg = "StartWhiteBoard"; //发送开启白板
            }
            else
            {
                isStartWhiteBoard = false;
                OperateMsg = "CloseWhiteBoard"; //发送关闭白板
            }
            int lineWidth = int.TryParse(tbxLineWidth.Text, out lineWidth) ? lineWidth : 5;
            Dictionary<string, dynamic> DicMsg = new Dictionary<string, dynamic>()
            {
                {"LineWidth", lineWidth},
                {"LineColor", new int[] {selectColor.A,selectColor.R,selectColor.G,selectColor.B}},
                {"OperateMsg", OperateMsg},
            };
            
            string sendStr = JsonConvert.SerializeObject(DicMsg);
            vncControl.Client.SendLocalClipboardChange(sendStr);
            ButtonStatusSet();
        }

        private void btnLineColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog=new ColorDialog();
            if (colorDialog.ShowDialog(this) == DialogResult.OK)
            {
                selectColor = colorDialog.Color;
                panelLineColor.BackColor = selectColor;
            }
        }

        private void btnClearWhiteBoard_Click(object sender, EventArgs e)
        {
            if (isStartWhiteBoard)
            {
                int lineWidth = int.TryParse(tbxLineWidth.Text, out lineWidth) ? lineWidth : 5;
                Dictionary<string, dynamic> DicMsg = new Dictionary<string, dynamic>()
                {
                    {"LineWidth", lineWidth},
                    {"LineColor", new int[] {selectColor.A,selectColor.R,selectColor.G,selectColor.B}},
                    {"OperateMsg", "ClearWhiteBoard"},
                };

                string sendStr = JsonConvert.SerializeObject(DicMsg);
                vncControl.Client.SendLocalClipboardChange(sendStr);//发送清空白板
            }
        }

        private void VncControl_MouseLeave(object sender, EventArgs e)
        {
            if (isStartWhiteBoard && isMouseLeftPress)
            {
                isMouseLeftPress = false;
                TheTimeDrawEnd();
            }
        }
        private void VncControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (isStartWhiteBoard && isMouseLeftPress)
            {
                isMouseLeftPress = false;
                TheTimeDrawEnd();
            }
        }
        private void VncControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (isStartWhiteBoard && isMouseLeftPress)
            {
                Size vncControlSize = vncControl.Size;

                int mouseX = e.X;
                int mouseY = e.Y;

                if (e.X < 0)
                    mouseX = 0;
                if (e.X > vncControlSize.Width)
                    mouseX = vncControlSize.Width;

                if (e.Y < 0)
                    mouseY = 0;
                if (e.Y > vncControlSize.Height)
                    mouseY = vncControlSize.Height;

                int remoteMouseX =(int) Math.Round( 1.0*(mouseX * remoteScreenSize.Width) / vncControlSize.Width);
                int remoteMouseY = (int) Math.Round(1.0 * (mouseY * remoteScreenSize.Height) / vncControlSize.Height);

                vncControl.Client.SendPointerEvent(remoteMouseX, remoteMouseY, 1);
            }
        }
        private void VncControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (isStartWhiteBoard && e.Button == MouseButtons.Left)
                isMouseLeftPress = true;

            int pressedButtons = 0;
            switch (e.Button)
            {
                case MouseButtons.Left:
                    pressedButtons = 1; break;
                case MouseButtons.Middle:
                    pressedButtons = 2; break;
                case MouseButtons.Right:
                    pressedButtons = 4; break;
                default:
                    pressedButtons = 0; break;
            }
            vncControl.Client.SendPointerEvent(e.X, e.Y, pressedButtons);
        }
        
        /// <summary> 本次画图结束 </summary>
        private void TheTimeDrawEnd()
        {
            if (isStartWhiteBoard)
            {
                int lineWidth = int.TryParse(tbxLineWidth.Text, out lineWidth) ? lineWidth : 5;
                Dictionary<string, dynamic> DicMsg = new Dictionary<string, dynamic>()
                {
                    {"LineWidth", lineWidth},
                    {"LineColor", new int[] {selectColor.A,selectColor.R,selectColor.G,selectColor.B}},
                    {"OperateMsg", "TheTimeDrawEnd"},
                };
                string sendStr = JsonConvert.SerializeObject(DicMsg);
                vncControl.Client.SendLocalClipboardChange(sendStr);//发送本次画图结束
            }
        }

        #endregion

        #region 鼠标滚轮

        private void VncControl_MouseWheel(object sender, MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            int mask = 0;
            if (e.Delta < 0)
                mask=8;
            else if (e.Delta > 0)
                mask = 16;

            vncControl.Client.SendPointerEvent(e.X, e.Y, mask);
        }

        #endregion

        #region 键盘按键

        private void VncControl_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.Control)
                //vncControl.Client.SendKeyEvent(VncKeysym.FromKeyCode(e.KeyData),true);
                vncControl.Client.SendKeyEvent((int)e.KeyData,true);
        }
        private void VncControl_KeyUp(object sender, KeyEventArgs e)
        {
            //if (e.KeyData==Keys.ControlKey)
                //vncControl.Client.SendKeyEvent(VncKeysym.FromKeyCode(e.KeyData), false);
                vncControl.Client.SendKeyEvent((int)e.KeyData, false);
        }

        #endregion
        
        #region 连接

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (vncControl.Client.IsConnected)
            {
                vncControl.Client.Close();
            }
            else
            {
                var hostname = txtHostname.Text.Trim();
                if (hostname == "")
                {
                    MessageBox.Show(this, "Hostname isn't set.", "Hostname",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int port;
                if (!int.TryParse(txtPort.Text, out port) || port < 1 || port > 65535)
                {
                    MessageBox.Show(this, "Port must be between 1 and 65535.", "Port",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var options = new VncClientConnectOptions();
                if (txtPassword.Text != "")
                {
                    options.Password = txtPassword.Text.ToCharArray();
                }

                try
                {
                    try
                    {
                        Cursor = Cursors.WaitCursor;
                        try
                        {
                            vncControl.Client.Connect(hostname, port, options);
                        }
                        finally
                        {
                            Cursor = Cursors.Default; 
                        }
                    }
                    catch (VncException ex)
                    {
                        MessageBox.Show(this,
                            "Connection failed (" + ex.Reason.ToString() + ","+ex.Message+").",
                            "Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    catch (SocketException ex)
                    {
                        MessageBox.Show(this,
                            "Connection failed (" + ex.SocketErrorCode.ToString() + ").",
                            "Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    vncControl.Focus();
                }
                finally
                {
                    if (options.Password != null)
                    {
                        Array.Clear(options.Password, 0, options.Password.Length);
                    }
                }
            }
        }

        private void vncControl_Connected(object sender, EventArgs e)
        {
            isStartWhiteBoard = false;
            isMouseLeftPress = false;
            ButtonStatusSet();

            int remoteScreenWidth = vncControl.Client.Framebuffer.Width;
            int remoteHeight = vncControl.Client.Framebuffer.Height;
            remoteScreenSize=new Size(remoteScreenWidth,remoteHeight);

        }

        private void vncControl_Closed(object sender, EventArgs e)
        {
            isStartWhiteBoard = false;
            isMouseLeftPress = false;
            ButtonStatusSet();
        }

        private void vncControl_ConnectionFailed(object sender, EventArgs e)
        {
            isStartWhiteBoard = false;
            isMouseLeftPress = false;
            ButtonStatusSet();
        }

        #endregion
        
        /// <summary> 设置按钮状态 </summary>
        private void ButtonStatusSet()
        {
            if (isConnect)
            {
                btnConnect.Text = "关闭VNC服务连接";
                btnWhiteBoard.Enabled = true;
            }
            else
            {
                btnConnect.Text = "连接VNC服务";
                btnWhiteBoard.Text = "开启白板";
                btnWhiteBoard.Enabled = false;
            }


            if (isConnect && isStartWhiteBoard)
            {
                btnWhiteBoard.Text = "关闭白板";
                btnClearWhiteBoard.Enabled = true;

                tbxLineWidth.Enabled = false;
                btnLineColor.Enabled = false;
            }
            else
            {
                btnWhiteBoard.Text = "开启白板";
                btnClearWhiteBoard.Enabled = false;

                tbxLineWidth.Enabled = true;
                btnLineColor.Enabled = true;
            }
        }

        
    }
}
