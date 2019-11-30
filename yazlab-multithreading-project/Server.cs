using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace yazlab_multithreading_project
{
    public class Server : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private int _requestCount;
        public int RequestCount
        {
            get { return _requestCount; }
            set
            {
                _requestCount = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(_requestCount)));
                }
            }
        }
        public int Capacity { get; set; }
        public bool IsAvaible { get; set; }
        public CancellationTokenSource CancellationToken { get; set; }
        public ProgressBar ProgressBar { get; set; }
        public ServerProgressBar ServerProgressBar { get; set; }
    }
}
