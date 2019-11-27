using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace yazlab_multithreading_project
{
    public partial class Form1 : Form, INotifyPropertyChanged
    {
        public List<Server> SubServers;
        public Server MainServer;
        private CancellationTokenSource cancellationToken;
        public int TotalRequest { get; set; }

        protected virtual void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        public Form1()
        {
            InitializeComponent();
            SubServers = new List<Server>();
            SubServers.Add(new Server()
            {
                RequestCount = 10,
                IsAvaible = true,
                Capacity = 5000,
                CancellationToken = new CancellationTokenSource(),
                ProgressBar = new ProgressBar()
                {
                    Minimum = 0,
                    Maximum = 5000,
                    Tag = "Sub Server",
                    Value = 10
                }
            });
            SubServers.Add(new Server()
            {
                Capacity = 5000,
                IsAvaible = true,
                RequestCount = 10,
                CancellationToken = new CancellationTokenSource(),
                ProgressBar = new ProgressBar()
                {
                    Minimum = 0,
                    Maximum = 5000,
                    Tag = "Sub Server",
                    Value = 10
                }
            });

            cancellationToken = new CancellationTokenSource();

            MainServer = new Server()
            {
                IsAvaible = true,
                Capacity = 10000,
                RequestCount = 0,
                CancellationToken = new CancellationTokenSource()
            };
            RichTextBox richTextBox = myConsole;
            RichTextBox richTextBox2 = myConsole2;
            richTextBox.BackColor = Color.Black;
            richTextBox.ForeColor = Color.Lime;
            richTextBox2.BackColor = Color.Black;
            richTextBox2.ForeColor = Color.Lime;
            richTextBox.Text = "MyConsole >.. \n";
            richTextBox2.Text = "MyConsole2 >.. \n";
            CheckForIllegalCrossThreadCalls = false;

        }
        public string LabelSubServer
        {
            get
            {
                return this.lblSubServers.Text;
            }
            set
            {
                this.lblSubServers.Text = value;
            }
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            cancellationToken = new CancellationTokenSource();
            MainServer.CancellationToken = new CancellationTokenSource();
            AddRequestToMainServer();
            CheckSubServerCapacity();
            timer1.Start();

        }
        public async Task AddRequestToMainServer()
        {
            int req = 0;
            TotalRequest = 0;
            Random randomNumber = new Random();
            Task addRequest = Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    req = randomNumber.Next(1, 100);
                    MainServer.CancellationToken.Token.ThrowIfCancellationRequested();
                    MainServer.RequestCount += req;
                    TotalRequest += req;
                    lblTotalRequest.Invoke(new Action(() => lblTotalRequest.Text = TotalRequest.ToString()));
                    myConsole.Invoke(new Action(() => myConsole.AppendText("Main Server Request: " + MainServer.RequestCount.ToString() + "\n")));
                    await Task.Delay(100);
                }
            }
            , MainServer.CancellationToken.Token);

            Task responseRequest = Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    req = randomNumber.Next(1, 50);
                    MainServer.CancellationToken.Token.ThrowIfCancellationRequested();
                    MainServer.RequestCount -= req;
                    TotalRequest -= req;
                    await Task.Delay(200);
                }
            }
            , MainServer.CancellationToken.Token);

            MainServer.CancellationToken.Token.Register(() =>
            {
                timer1.Stop();
                Console.WriteLine("Requests Stoped");
            });
        }

        public async Task MainServerController()
        {
            if (MainServer.RequestCount >= (MainServer.Capacity * 0.20))
            {
                //await Task.Run(() => CallSubServer());
            }
        }
        public async Task CallSubServer()
        {
            bool isThereAvaibleServer = false;
            MainServer.RequestCount -= 50;
            SubServers.ForEach(server =>
            {
                if (server.IsAvaible && !server.CancellationToken.IsCancellationRequested && !isThereAvaibleServer)
                {
                    server.RequestCount = server.RequestCount + 50;
                    isThereAvaibleServer = true;
                    return;
                }
            });
            if (!isThereAvaibleServer)
            {
                Console.WriteLine("Called CreateSubServer");
                await CreateSubServer(50);
            }
        }
        public async Task CheckSubServerCapacity()
        {
            Console.WriteLine("CheckSubServer CAPACITY");
            Task check = Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    cancellationToken.Token.ThrowIfCancellationRequested();
                    SubServers.OrderBy(x=> x.RequestCount).ToList().ForEach(async server =>
                    {
                        server.IsAvaible = server.RequestCount >= (server.Capacity * 0.70) ? false : true;
                        await Task.Delay(100);
                        if (!server.IsAvaible)
                        {
                            Console.WriteLine("Called CreateSubServer.");
                            await CreateSubServer(server.RequestCount / 2);
                            int req = server.RequestCount / 2;
                            server.RequestCount = (req);
                            server.IsAvaible = true;
                        }
                        if (server.RequestCount == 0 || server.RequestCount < 0)
                        {
                            Console.WriteLine("Sub Server Req count = 0");
                            if (SubServers.Count > 2)
                            {
                                Console.WriteLine("Thread Cancelled.");
                                server.IsAvaible = false;
                                server.CancellationToken.Cancel();
                                //SubServers.Remove(server);
                            }
                        }
                    });
                }
            }
            , cancellationToken.Token);

            Task getRequest =  Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    Console.WriteLine("Get Request.");
                    cancellationToken.Token.ThrowIfCancellationRequested();
                    await Task.Delay(500);
                    await CallSubServer();
                }
            }
            , cancellationToken.Token);
            cancellationToken.Token.Register(() =>
            {
                Console.WriteLine("all operations stopped.");
                Console.WriteLine("subservers :" + SubServers.Count.ToString() + "\nMain server :" + MainServer.RequestCount.ToString());
            });
        }
        public async Task CreateSubServer(int requestCount)
        {
            var server = new Server()
            {
                IsAvaible = true,
                RequestCount = requestCount,
                CancellationToken = new CancellationTokenSource(),
                Capacity = 5000,
            };
            SubServers.Add(server);
            Console.WriteLine("SUBSERVER CREATED!");
            try
            {
                if (myConsole2.InvokeRequired)
                {
                    myConsole2.Invoke(new Action(() => myConsole2.AppendText("Sub Server Created..")));
                }
                else
                {
                    myConsole2.AppendText("Sub Server Created..");
                }
            }
            catch (InvalidOperationException)
            {

                Console.WriteLine("INVOKE HATASI");
            }
            //myFlow.Invoke(new Action(() =>
            //{
            //    myFlow.Controls.Add(server.ProgressBar);
            //}));
        }

        private void btnStopRequest_Click(object sender, EventArgs e)
        {
            MainServer.CancellationToken.Cancel();
            cancellationToken.Cancel();
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            cancellationToken.Cancel();
            MainServer.CancellationToken.Cancel();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private async void Form1_Load(object sender, EventArgs e)
        {

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            mainServerProgress.Value = MainServer.RequestCount;
            lblMainServer.Text = "Main Server Request: " + MainServer.RequestCount.ToString();
            LabelSubServer = "Sub Server Count: " + SubServers.Where(x => !x.CancellationToken.IsCancellationRequested).ToList().Count.ToString();
            //myConsole.AppendText("Sub Server Count: " + SubServers.Count.ToString() + "\n");
            SubServers.ForEach(server =>
            {
                myConsole2.AppendText("Sub Server Count: " + server.RequestCount.ToString() + "\n");
            });
        }
        public async void UpdateProgressBar()
        {
            Task updateProgress = Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    MainServer.CancellationToken.Token.ThrowIfCancellationRequested();
                    await Task.Delay(200);
                    SubServers.ForEach(x =>
                    {
                        x.ProgressBar.Value = x.RequestCount;
                    });
                }
            }
            , MainServer.CancellationToken.Token);
        }
    }
}
