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
        Label Label, Percentage;
        public ServerProgressBar(int progressValue, int ProgressMin, int ProgressMax, string tag)
        {
            ProgressBar = new ProgressBar()
            {
                Minimum = ProgressMin,
                Maximum = ProgressMax,
                Value = progressValue,
                Tag = tag,
                Width = 150,
                Height = 30
            };
            Label = new Label()
            {
                Text = tag,
            };
            Percentage = new Label() {
                Text = " % 0",
            };
            this.Size = new System.Drawing.Size(450, 40);
            this.Controls.Add(ProgressBar);
            this.Controls.Add(Label);
            this.Controls.Add(Percentage);
        }
        public void ChangeValues(int value = 0, decimal percentage = 0)
        {
            this.ProgressBar.Value = value;
            this.Label.Text = this.ProgressBar.Tag.ToString();
            this.Percentage.Text = " % " + percentage;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);

        }
    }
}
