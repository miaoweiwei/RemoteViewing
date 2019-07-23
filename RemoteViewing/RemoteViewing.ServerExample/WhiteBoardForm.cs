using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace RemoteViewing.ServerExample
{
    public partial class WhiteBoardForm : Form
    {
        private Point? lastPoint = null;
        /// <summary> 上一个点 </summary>
        public Point? LastPoint
        {
            get { return lastPoint; }
        }
        
        private Color penColor=Color.Red;
        /// <summary> 画笔颜色,默认红色</summary>
        public Color PenColor
        {
            get { return penColor; }
            set { penColor = value; }
        }

        private int penWidth = 5;
        /// <summary> 画笔的粗度，默认5像素</summary>
        public int PenWidth
        {
            get { return penWidth; }
            set { penWidth = value; }
        }

        private Graphics graphics;
        
        #region 单例

        private static WhiteBoardForm _singleton;
        private static readonly  object SyncRoot=new object();
        private WhiteBoardForm()
        {
            InitializeComponent();

            this.Text = "";
            this.ShowInTaskbar = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.TransparencyKey = SystemColors.Control;
            this.TopMost = true;

            graphics = Graphics.FromHwnd(this.Handle);
        }

        public static WhiteBoardForm GetWhiteBoardForm()
        {
            if (_singleton == null)
            {
                lock (SyncRoot)
                {
                    if (_singleton == null)
                    {
                        _singleton=new WhiteBoardForm();
                    }
                }
            }
            return _singleton;
        }

        #endregion
        
        private delegate void DrawTrackDelegate(Point targetPoint);
        /// <summary>
        /// 画轨迹
        /// </summary>
        public void DrawTrack(Point targetPoint)
        {
            if (this.InvokeRequired)
            {
                DrawTrackDelegate d=new DrawTrackDelegate(DrawTrack);
                this.BeginInvoke(d, new object[] {targetPoint});
            }
            else
            {
                if (lastPoint == null)
                {
                    lastPoint = targetPoint;
                    return;
                }

                graphics.DrawLine(new Pen(penColor, penWidth), (Point)lastPoint, targetPoint);
                lastPoint = targetPoint;
            }
        }

        private delegate void TheTimeDrawEndDelegate();
        /// <summary>
        /// 结束本次画图
        /// </summary>
        public void TheTimeDrawEnd()
        {
            if (this.InvokeRequired)
            {
                TheTimeDrawEndDelegate d = new TheTimeDrawEndDelegate(TheTimeDrawEnd);
                this.BeginInvoke(d, new object[] { });
            }
            else
                lastPoint = null;
        }

        private delegate void ClearWhiteBoardDelegate();
        /// <summary>
        /// 清空白板
        /// </summary>
        public void ClearWhiteBoard()
        {
            if (this.InvokeRequired)
            {
                ClearWhiteBoardDelegate d=new ClearWhiteBoardDelegate(ClearWhiteBoard);
                this.BeginInvoke(d, new object[] { });
            }
            graphics.Clear(SystemColors.Control);
        }

        private delegate void ShowWhiteBoardDelegate();
        /// <summary>
        /// 显示白板
        /// </summary>
        public void ShowWhiteBoard()
        {
            if (this.InvokeRequired)
            {
                ShowWhiteBoardDelegate d = new ShowWhiteBoardDelegate(ShowWhiteBoard);
                this.BeginInvoke(d, new object[] { });
            }
            else
                this.Show();
        }


        private delegate void HideWhiteBoardDelegate();
        /// <summary>
        /// 隐藏白板
        /// </summary>
        public void HideWhiteBoard()
        {
            if (this.InvokeRequired)
            {
                HideWhiteBoardDelegate d=new HideWhiteBoardDelegate(HideWhiteBoard);
                this.BeginInvoke(d, new object[] { });
            }
            else
                this.Hide();
        }
    }
}
