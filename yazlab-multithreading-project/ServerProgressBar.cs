using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace yazlab_multithreading_project
{
    public class ServerProgressBar : System.Windows.Forms.FlowLayoutPanel
    {
        ProgressBar ProgressBar;
        Label Name, Percentage, RequestCount;
        public ServerProgressBar(int progressValue, int ProgressMin, int ProgressMax, string tag)
        {
            ProgressBar = new ProgressBar()
            {
                Minimum = ProgressMin,
                Maximum = ProgressMax,
                Value = progressValue,
                Tag = tag,
                Width = 150,
                Height = 45
            };
            Name = new Label()
            {
                Text = tag,
            };
            Percentage = new Label() {
                Text = " % 0",
            };
            RequestCount = new Label()
            {
                Text = progressValue.ToString(),
            };
            this.Size = new System.Drawing.Size(520, 50);
            this.Controls.Add(ProgressBar);
            this.Controls.Add(Name);
            this.Controls.Add(Percentage);
            this.Controls.Add(RequestCount);
        }
        public void ChangeValues(int value = 0, decimal percentage = 0)
        {
            this.ProgressBar.Value = value;
            this.Name.Text = this.ProgressBar.Tag.ToString();
            this.Percentage.Text = " % " + percentage;
            this.RequestCount.Text = value.ToString();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);

        }
    }
}
