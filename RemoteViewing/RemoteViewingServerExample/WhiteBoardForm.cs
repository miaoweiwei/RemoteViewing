using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace RemoteViewingServerExample
{
    public partial class WhiteBoardForm : Form
    {
        private Point lastPoint = Point.Empty;
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

        private int pointCount = 0;
        private Graphics graphics;

        [DllImport("user32.dll", SetLastError = false)]
        static extern IntPtr SetParent(IntPtr hwndChild, IntPtr hWndNewParent);
        /// <summary> 获取窗口的样式 </summary> 
        [DllImport("user32.dll")]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        /// <summary> 设置窗口的样式 </summary> 
        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        private const int GWL_EXSTYLE = -20;
        private const int WS_EX_LAYERED = 0X8000000;

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

            //设置窗体不显示在任务栏里和不显示在任务管理器的进程里
            int ExdStyle = GetWindowLong(this.Handle, GWL_EXSTYLE);
            SetWindowLong(this.Handle, GWL_EXSTYLE, ExdStyle | WS_EX_LAYERED);
            //或者设置父窗体也可以是该窗体不显示在任务栏，任务管理器，在Alt+Tab切换程序的时候也不会显示
        }

        public sealed override string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        public sealed override Color BackColor
        {
            get { return base.BackColor; }
            set { base.BackColor = value; }
        }

        public static WhiteBoardForm GetWhiteBoardForm()
        {
            if (_singleton == null || _singleton.IsDisposed)
            {
                lock (SyncRoot)
                {
                    if (_singleton == null || _singleton.IsDisposed)
                    {
                        _singleton=new WhiteBoardForm();
                    }
                }
            }
            return _singleton;
        }

        #endregion

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            //不关闭窗口
            e.Cancel = true;
        }

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
                if (pointCount<3)
                {
                    this.BringToFront();
                    pointCount++;
                    lastPoint = targetPoint;
                    return;
                }
                graphics.DrawLine(new Pen(penColor, penWidth), lastPoint, targetPoint);
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
                pointCount = 0;
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
            else
            {
                pointCount = 0;
                graphics.Clear(SystemColors.Control);
            }
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
            {
                this.Show();
                //白板显示过后再获取画图板，否则会出现不能画到任务栏上
                if (graphics == null)
                    graphics = Graphics.FromHwnd(this.Handle);
            }
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
