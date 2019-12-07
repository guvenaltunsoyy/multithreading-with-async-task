namespace yazlab_multithreading_project
{
    partial class Form1
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStopRequest = new System.Windows.Forms.Button();
            this.myFlow = new System.Windows.Forms.FlowLayoutPanel();
            this.myFlow2 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.myConsole2 = new System.Windows.Forms.RichTextBox();
            this.myConsole = new System.Windows.Forms.RichTextBox();
            this.lblSubServers = new System.Windows.Forms.Label();
            this.lblMainServer = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblTotalRequest = new System.Windows.Forms.Label();
            this.btnStartMainServer = new System.Windows.Forms.Button();
            this.btnStopMainServer = new System.Windows.Forms.Button();
            this.myFlow.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(933, 19);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(117, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStopRequest
            // 
            this.btnStopRequest.Location = new System.Drawing.Point(933, 48);
            this.btnStopRequest.Name = "btnStopRequest";
            this.btnStopRequest.Size = new System.Drawing.Size(117, 23);
            this.btnStopRequest.TabIndex = 1;
            this.btnStopRequest.Text = "Stop";
            this.btnStopRequest.UseVisualStyleBackColor = true;
            this.btnStopRequest.Click += new System.EventHandler(this.btnStopRequest_Click);
            // 
            // myFlow
            // 
            this.myFlow.Controls.Add(this.myFlow2);
            this.myFlow.Controls.Add(this.panel1);
            this.myFlow.Location = new System.Drawing.Point(13, 13);
            this.myFlow.Name = "myFlow";
            this.myFlow.Size = new System.Drawing.Size(914, 727);
            this.myFlow.TabIndex = 1;
            // 
            // myFlow2
            // 
            this.myFlow2.AutoScroll = true;
            this.myFlow2.Location = new System.Drawing.Point(3, 3);
            this.myFlow2.Name = "myFlow2";
            this.myFlow2.Size = new System.Drawing.Size(470, 724);
            this.myFlow2.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.myConsole2);
            this.panel1.Controls.Add(this.myConsole);
            this.panel1.Location = new System.Drawing.Point(479, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(424, 598);
            this.panel1.TabIndex = 3;
            // 
            // myConsole2
            // 
            this.myConsole2.BackColor = System.Drawing.SystemColors.MenuText;
            this.myConsole2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.myConsole2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.myConsole2.HideSelection = false;
            this.myConsole2.Location = new System.Drawing.Point(0, 273);
            this.myConsole2.Name = "myConsole2";
            this.myConsole2.Size = new System.Drawing.Size(424, 264);
            this.myConsole2.TabIndex = 1;
            this.myConsole2.Text = "";
            // 
            // myConsole
            // 
            this.myConsole.BackColor = System.Drawing.SystemColors.MenuText;
            this.myConsole.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.myConsole.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.myConsole.HideSelection = false;
            this.myConsole.Location = new System.Drawing.Point(0, 3);
            this.myConsole.Name = "myConsole";
            this.myConsole.Size = new System.Drawing.Size(424, 264);
            this.myConsole.TabIndex = 0;
            this.myConsole.Text = "";
            this.myConsole.UseWaitCursor = true;
            // 
            // lblSubServers
            // 
            this.lblSubServers.AutoSize = true;
            this.lblSubServers.Location = new System.Drawing.Point(933, 254);
            this.lblSubServers.Name = "lblSubServers";
            this.lblSubServers.Size = new System.Drawing.Size(26, 13);
            this.lblSubServers.TabIndex = 5;
            this.lblSubServers.Text = "Sub";
            // 
            // lblMainServer
            // 
            this.lblMainServer.AutoSize = true;
            this.lblMainServer.Location = new System.Drawing.Point(933, 241);
            this.lblMainServer.Name = "lblMainServer";
            this.lblMainServer.Size = new System.Drawing.Size(30, 13);
            this.lblMainServer.TabIndex = 4;
            this.lblMainServer.Text = "Main";
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblTotalRequest
            // 
            this.lblTotalRequest.AutoSize = true;
            this.lblTotalRequest.Location = new System.Drawing.Point(933, 267);
            this.lblTotalRequest.Name = "lblTotalRequest";
            this.lblTotalRequest.Size = new System.Drawing.Size(31, 13);
            this.lblTotalRequest.TabIndex = 6;
            this.lblTotalRequest.Text = "Total";
            // 
            // btnStartMainServer
            // 
            this.btnStartMainServer.Location = new System.Drawing.Point(933, 77);
            this.btnStartMainServer.Name = "btnStartMainServer";
            this.btnStartMainServer.Size = new System.Drawing.Size(117, 23);
            this.btnStartMainServer.TabIndex = 2;
            this.btnStartMainServer.Text = "Start Main Server Request";
            this.btnStartMainServer.UseVisualStyleBackColor = true;
            this.btnStartMainServer.Click += new System.EventHandler(this.btnStartMainServer_Click);
            // 
            // btnStopMainServer
            // 
            this.btnStopMainServer.Location = new System.Drawing.Point(933, 106);
            this.btnStopMainServer.Name = "btnStopMainServer";
            this.btnStopMainServer.Size = new System.Drawing.Size(117, 23);
            this.btnStopMainServer.TabIndex = 3;
            this.btnStopMainServer.Text = "Stop Main Server";
            this.btnStopMainServer.UseVisualStyleBackColor = true;
            this.btnStopMainServer.Click += new System.EventHandler(this.btnStopMainServer_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1151, 752);
            this.Controls.Add(this.btnStopMainServer);
            this.Controls.Add(this.btnStartMainServer);
            this.Controls.Add(this.lblTotalRequest);
            this.Controls.Add(this.myFlow);
            this.Controls.Add(this.lblSubServers);
            this.Controls.Add(this.btnStopRequest);
            this.Controls.Add(this.lblMainServer);
            this.Controls.Add(this.btnStart);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.myFlow.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStopRequest;
        private System.Windows.Forms.FlowLayoutPanel myFlow;
        private System.Windows.Forms.RichTextBox myConsole;
        private System.Windows.Forms.RichTextBox myConsole2;
        private System.Windows.Forms.Label lblSubServers;
        private System.Windows.Forms.Label lblMainServer;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblTotalRequest;
        private System.Windows.Forms.FlowLayoutPanel myFlow2;
        private System.Windows.Forms.Button btnStartMainServer;
        private System.Windows.Forms.Button btnStopMainServer;
        private System.Windows.Forms.Panel panel1;
    }
}

