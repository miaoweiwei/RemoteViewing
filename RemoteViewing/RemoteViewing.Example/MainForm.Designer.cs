namespace RemoteViewing.Example
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            RemoteViewing.Vnc.VncClient vncClient3 = new RemoteViewing.Vnc.VncClient();
            this.mainTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.topTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.lblPort = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.btnWhiteBoard = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblHostname = new System.Windows.Forms.Label();
            this.txtHostname = new System.Windows.Forms.TextBox();
            this.btnClearWhiteBoard = new System.Windows.Forms.Button();
            this.vncControl = new RemoteViewing.Windows.Forms.VncControl();
            this.tbxLineWidth = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panelLineColor = new System.Windows.Forms.Panel();
            this.btnLineColor = new System.Windows.Forms.Button();
            this.mainTableLayoutPanel.SuspendLayout();
            this.topTableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainTableLayoutPanel
            // 
            this.mainTableLayoutPanel.AutoSize = true;
            this.mainTableLayoutPanel.ColumnCount = 1;
            this.mainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainTableLayoutPanel.Controls.Add(this.topTableLayoutPanel, 0, 0);
            this.mainTableLayoutPanel.Controls.Add(this.vncControl, 0, 1);
            this.mainTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.mainTableLayoutPanel.Name = "mainTableLayoutPanel";
            this.mainTableLayoutPanel.RowCount = 3;
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.mainTableLayoutPanel.Size = new System.Drawing.Size(801, 442);
            this.mainTableLayoutPanel.TabIndex = 0;
            // 
            // topTableLayoutPanel
            // 
            this.topTableLayoutPanel.AutoSize = true;
            this.topTableLayoutPanel.ColumnCount = 13;
            this.topTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.topTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.topTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.topTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.topTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.topTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.topTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.topTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.topTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.topTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.topTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.topTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.topTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.topTableLayoutPanel.Controls.Add(this.lblPort, 9, 0);
            this.topTableLayoutPanel.Controls.Add(this.txtPassword, 12, 0);
            this.topTableLayoutPanel.Controls.Add(this.panelLineColor, 5, 0);
            this.topTableLayoutPanel.Controls.Add(this.txtPort, 10, 0);
            this.topTableLayoutPanel.Controls.Add(this.btnWhiteBoard, 1, 0);
            this.topTableLayoutPanel.Controls.Add(this.btnConnect, 0, 0);
            this.topTableLayoutPanel.Controls.Add(this.lblPassword, 11, 0);
            this.topTableLayoutPanel.Controls.Add(this.lblHostname, 7, 0);
            this.topTableLayoutPanel.Controls.Add(this.txtHostname, 8, 0);
            this.topTableLayoutPanel.Controls.Add(this.btnClearWhiteBoard, 6, 0);
            this.topTableLayoutPanel.Controls.Add(this.tbxLineWidth, 3, 0);
            this.topTableLayoutPanel.Controls.Add(this.label1, 2, 0);
            this.topTableLayoutPanel.Controls.Add(this.btnLineColor, 4, 0);
            this.topTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.topTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.topTableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.topTableLayoutPanel.Name = "topTableLayoutPanel";
            this.topTableLayoutPanel.RowCount = 1;
            this.topTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.topTableLayoutPanel.Size = new System.Drawing.Size(801, 25);
            this.topTableLayoutPanel.TabIndex = 0;
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPort.Location = new System.Drawing.Point(547, 0);
            this.lblPort.Margin = new System.Windows.Forms.Padding(0);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(89, 25);
            this.lblPort.TabIndex = 3;
            this.lblPort.Text = "VNC服务端端口:";
            this.lblPort.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtPassword
            // 
            this.txtPassword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPassword.Location = new System.Drawing.Point(735, 0);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(0);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(66, 21);
            this.txtPassword.TabIndex = 3;
            this.txtPassword.Text = "123456";
            // 
            // txtPort
            // 
            this.txtPort.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPort.Location = new System.Drawing.Point(639, 3);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(58, 21);
            this.txtPort.TabIndex = 2;
            this.txtPort.Text = "5901";
            // 
            // btnWhiteBoard
            // 
            this.btnWhiteBoard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnWhiteBoard.Location = new System.Drawing.Point(57, 0);
            this.btnWhiteBoard.Margin = new System.Windows.Forms.Padding(0);
            this.btnWhiteBoard.Name = "btnWhiteBoard";
            this.btnWhiteBoard.Size = new System.Drawing.Size(75, 25);
            this.btnWhiteBoard.TabIndex = 4;
            this.btnWhiteBoard.Text = "开启注释";
            this.btnWhiteBoard.UseVisualStyleBackColor = true;
            this.btnWhiteBoard.Click += new System.EventHandler(this.btnWhiteBoard_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.AutoSize = true;
            this.btnConnect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnConnect.Location = new System.Drawing.Point(0, 0);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(0);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(57, 25);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "连接";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPassword.Location = new System.Drawing.Point(700, 0);
            this.lblPassword.Margin = new System.Windows.Forms.Padding(0);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(35, 25);
            this.lblPassword.TabIndex = 4;
            this.lblPassword.Text = "密码:";
            this.lblPassword.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblHostname
            // 
            this.lblHostname.AutoSize = true;
            this.lblHostname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHostname.Location = new System.Drawing.Point(406, 0);
            this.lblHostname.Margin = new System.Windows.Forms.Padding(0);
            this.lblHostname.Name = "lblHostname";
            this.lblHostname.Size = new System.Drawing.Size(77, 25);
            this.lblHostname.TabIndex = 7;
            this.lblHostname.Text = "VNC服务端IP:";
            this.lblHostname.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtHostname
            // 
            this.txtHostname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtHostname.Location = new System.Drawing.Point(483, 0);
            this.txtHostname.Margin = new System.Windows.Forms.Padding(0);
            this.txtHostname.Name = "txtHostname";
            this.txtHostname.Size = new System.Drawing.Size(64, 21);
            this.txtHostname.TabIndex = 1;
            this.txtHostname.Text = "127.0.0.1";
            // 
            // btnClearWhiteBoard
            // 
            this.btnClearWhiteBoard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClearWhiteBoard.Location = new System.Drawing.Point(331, 0);
            this.btnClearWhiteBoard.Margin = new System.Windows.Forms.Padding(0);
            this.btnClearWhiteBoard.Name = "btnClearWhiteBoard";
            this.btnClearWhiteBoard.Size = new System.Drawing.Size(75, 25);
            this.btnClearWhiteBoard.TabIndex = 5;
            this.btnClearWhiteBoard.Text = "清空注释";
            this.btnClearWhiteBoard.UseVisualStyleBackColor = true;
            this.btnClearWhiteBoard.Click += new System.EventHandler(this.btnClearWhiteBoard_Click);
            // 
            // vncControl
            // 
            this.vncControl.AllowClipboardSharingFromServer = false;
            this.vncControl.AllowClipboardSharingToServer = false;
            this.vncControl.AllowInput = true;
            this.vncControl.AllowRemoteCursor = false;
            this.vncControl.BackColor = System.Drawing.SystemColors.Control;
            vncClient3.MaxUpdateRate = 15D;
            vncClient3.UserData = null;
            this.vncControl.Client = vncClient3;
            this.vncControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vncControl.Location = new System.Drawing.Point(3, 28);
            this.vncControl.Name = "vncControl";
            this.vncControl.Size = new System.Drawing.Size(795, 391);
            this.vncControl.TabIndex = 1;
            this.vncControl.Connected += new System.EventHandler(this.vncControl_Connected);
            this.vncControl.ConnectionFailed += new System.EventHandler(this.vncControl_ConnectionFailed);
            this.vncControl.Closed += new System.EventHandler(this.vncControl_Closed);
            // 
            // tbxLineWidth
            // 
            this.tbxLineWidth.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbxLineWidth.Location = new System.Drawing.Point(167, 0);
            this.tbxLineWidth.Margin = new System.Windows.Forms.Padding(0);
            this.tbxLineWidth.Name = "tbxLineWidth";
            this.tbxLineWidth.Size = new System.Drawing.Size(64, 21);
            this.tbxLineWidth.TabIndex = 0;
            this.tbxLineWidth.Text = "5";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(132, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "线宽:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelLineColor
            // 
            this.panelLineColor.BackColor = System.Drawing.Color.Red;
            this.panelLineColor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLineColor.Location = new System.Drawing.Point(306, 0);
            this.panelLineColor.Margin = new System.Windows.Forms.Padding(0);
            this.panelLineColor.Name = "panelLineColor";
            this.panelLineColor.Size = new System.Drawing.Size(25, 25);
            this.panelLineColor.TabIndex = 2;
            // 
            // btnLineColor
            // 
            this.btnLineColor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnLineColor.Location = new System.Drawing.Point(231, 0);
            this.btnLineColor.Margin = new System.Windows.Forms.Padding(0);
            this.btnLineColor.Name = "btnLineColor";
            this.btnLineColor.Size = new System.Drawing.Size(75, 25);
            this.btnLineColor.TabIndex = 8;
            this.btnLineColor.Text = "线条颜色";
            this.btnLineColor.UseVisualStyleBackColor = true;
            this.btnLineColor.Click += new System.EventHandler(this.btnLineColor_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(801, 442);
            this.Controls.Add(this.mainTableLayoutPanel);
            this.Name = "MainForm";
            this.Text = "RemoteViewing - Example VNC Client";
            this.mainTableLayoutPanel.ResumeLayout(false);
            this.mainTableLayoutPanel.PerformLayout();
            this.topTableLayoutPanel.ResumeLayout(false);
            this.topTableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel mainTableLayoutPanel;
        private System.Windows.Forms.TableLayoutPanel topTableLayoutPanel;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox txtHostname;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.TextBox txtPassword;
        private Windows.Forms.VncControl vncControl;
        private System.Windows.Forms.Button btnWhiteBoard;
        private System.Windows.Forms.Button btnClearWhiteBoard;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.Label lblHostname;
        private System.Windows.Forms.TextBox tbxLineWidth;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelLineColor;
        private System.Windows.Forms.Button btnLineColor;
    }
}