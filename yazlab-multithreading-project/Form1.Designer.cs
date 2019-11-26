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
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStopRequest = new System.Windows.Forms.Button();
            this.myFlow = new System.Windows.Forms.FlowLayoutPanel();
            this.myConsole = new System.Windows.Forms.RichTextBox();
            this.mainServerProgress = new System.Windows.Forms.ProgressBar();
            this.myConsole2 = new System.Windows.Forms.RichTextBox();
            this.lblMainServer = new System.Windows.Forms.Label();
            this.lblSubServers = new System.Windows.Forms.Label();
            this.myFlow.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(928, 19);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStopRequest
            // 
            this.btnStopRequest.Location = new System.Drawing.Point(928, 48);
            this.btnStopRequest.Name = "btnStopRequest";
            this.btnStopRequest.Size = new System.Drawing.Size(75, 23);
            this.btnStopRequest.TabIndex = 0;
            this.btnStopRequest.Text = "Stop";
            this.btnStopRequest.UseVisualStyleBackColor = true;
            this.btnStopRequest.Click += new System.EventHandler(this.btnStopRequest_Click);
            // 
            // myFlow
            // 
            this.myFlow.Controls.Add(this.myConsole);
            this.myFlow.Controls.Add(this.lblSubServers);
            this.myFlow.Controls.Add(this.lblMainServer);
            this.myFlow.Controls.Add(this.mainServerProgress);
            this.myFlow.Controls.Add(this.myConsole2);
            this.myFlow.Location = new System.Drawing.Point(13, 13);
            this.myFlow.Name = "myFlow";
            this.myFlow.Size = new System.Drawing.Size(909, 565);
            this.myFlow.TabIndex = 1;
            // 
            // myConsole
            // 
            this.myConsole.BackColor = System.Drawing.SystemColors.MenuText;
            this.myConsole.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.myConsole.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.myConsole.Location = new System.Drawing.Point(3, 3);
            this.myConsole.Name = "myConsole";
            this.myConsole.Size = new System.Drawing.Size(424, 264);
            this.myConsole.TabIndex = 1;
            this.myConsole.Text = "";
            // 
            // mainServerProgress
            // 
            this.mainServerProgress.Location = new System.Drawing.Point(515, 3);
            this.mainServerProgress.Name = "mainServerProgress";
            this.mainServerProgress.Size = new System.Drawing.Size(100, 23);
            this.mainServerProgress.TabIndex = 0;
            // 
            // myConsole2
            // 
            this.myConsole2.BackColor = System.Drawing.SystemColors.MenuText;
            this.myConsole2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.myConsole2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.myConsole2.Location = new System.Drawing.Point(3, 273);
            this.myConsole2.Name = "myConsole2";
            this.myConsole2.Size = new System.Drawing.Size(424, 264);
            this.myConsole2.TabIndex = 1;
            this.myConsole2.Text = "";
            // 
            // lblMainServer
            // 
            this.lblMainServer.AutoSize = true;
            this.lblMainServer.Location = new System.Drawing.Point(474, 0);
            this.lblMainServer.Name = "lblMainServer";
            this.lblMainServer.Size = new System.Drawing.Size(35, 13);
            this.lblMainServer.TabIndex = 2;
            this.lblMainServer.Text = "label1";
            // 
            // lblSubServers
            // 
            this.lblSubServers.AutoSize = true;
            this.lblSubServers.Location = new System.Drawing.Point(433, 0);
            this.lblSubServers.Name = "lblSubServers";
            this.lblSubServers.Size = new System.Drawing.Size(35, 13);
            this.lblSubServers.TabIndex = 3;
            this.lblSubServers.Text = "label2";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1120, 628);
            this.Controls.Add(this.myFlow);
            this.Controls.Add(this.btnStopRequest);
            this.Controls.Add(this.btnStart);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.myFlow.ResumeLayout(false);
            this.myFlow.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStopRequest;
        private System.Windows.Forms.FlowLayoutPanel myFlow;
        private System.Windows.Forms.ProgressBar mainServerProgress;
        private System.Windows.Forms.RichTextBox myConsole;
        private System.Windows.Forms.RichTextBox myConsole2;
        private System.Windows.Forms.Label lblSubServers;
        private System.Windows.Forms.Label lblMainServer;
    }
}

