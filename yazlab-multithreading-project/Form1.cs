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
                ServerProgressBar = new ServerProgressBar(0, 0, 5000, "Sub Server 1")
            });
            SubServers.Add(new Server()
            {
                Capacity = 5000,
                IsAvaible = true,
                RequestCount = 10,
                CancellationToken = new CancellationTokenSource(),
                ServerProgressBar = new ServerProgressBar(0, 0, 5000, "Sub Server 2"),
            });

            cancellationToken = new CancellationTokenSource();

            MainServer = new Server()
            {
                IsAvaible = true,
                Capacity = 10000,
                RequestCount = 0,
                CancellationToken = new CancellationTokenSource(),
                ServerProgressBar = new ServerProgressBar(0, 0, 10000, "Main Server"),
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
            if (myFlow2.InvokeRequired)
            {
                myFlow2.Invoke(new Action(() =>
                {
                    myFlow2.Controls.Add(MainServer.ServerProgressBar);
                    myFlow2.Controls.Add(SubServers[0].ServerProgressBar);
                    myFlow2.Controls.Add(SubServers[1].ServerProgressBar);
                }));
            }
            else
            {
                myFlow2.Controls.Add(MainServer.ServerProgressBar);
                myFlow2.Controls.Add(SubServers[0].ServerProgressBar);
                myFlow2.Controls.Add(SubServers[1].ServerProgressBar);
            }
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
            timer1.Start();
            AddRequestToMainServer();
            CheckSubServerCapacity();
        }
        public async Task AddRequestToMainServer()
        {
            Console.WriteLine("ADDTOMAINSERVER");
            int req = 0;
            TotalRequest = 0;
            Random randomNumber = new Random();
            decimal percentageMain;
            Task addRequest = Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    if (MainServer.RequestCount >= 9900)
                    {
                        continue;
                    }
                    req = randomNumber.Next(1, 100);
                    MainServer.CancellationToken.Token.ThrowIfCancellationRequested();
                    MainServer.RequestCount += req;
                    TotalRequest += req;
                    percentageMain = ((Convert.ToDecimal(MainServer.RequestCount) / MainServer.Capacity) * 100);
                    MainServer.ServerProgressBar.ChangeValues(MainServer.RequestCount, percentageMain);
                    lblTotalRequest.Invoke(new Action(() => lblTotalRequest.Text = "Total Request: " + TotalRequest.ToString()));
                    myConsole.Invoke(new Action(() => myConsole.AppendText("Main Server Request: " + req.ToString() + "\n")));
                    await Task.Delay(100);
                }
            }
            , MainServer.CancellationToken.Token);

            Task responseRequest = Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    await Task.Delay(500);
                    if (MainServer.RequestCount <= 0)
                    {
                        MainServer.RequestCount = 0;
                        MainServer.ServerProgressBar.ChangeValues(0, 0);
                        myConsole.Invoke(new Action(() => myConsole.AppendText("Main Server Request: " + MainServer.RequestCount.ToString() + "\n")));
                        MainServer.CancellationToken.Cancel();
                    }
                    req = randomNumber.Next(1, 50);
                    MainServer.CancellationToken.Token.ThrowIfCancellationRequested();
                    MainServer.RequestCount -= req;
                    TotalRequest -= req;
                    myConsole.Invoke(new Action(() => myConsole.AppendText("Main Server Response: " + req.ToString() + "\n")));

                }
            }
            , MainServer.CancellationToken.Token);

            MainServer.CancellationToken.Token.Register(() =>
            {
                Console.WriteLine("Requests Stoped");
            });
        }
        public async Task CallSubServer()
        {
            Random random = new Random();
            bool isThereAvaibleServer = false;
            int rndm = random.Next(1, 50);
            MainServer.RequestCount -= rndm;
            SubServers.OrderBy(x => x.RequestCount).ToList().ForEach(server =>
            {
                if (server.IsAvaible && !server.CancellationToken.IsCancellationRequested && !isThereAvaibleServer)
                {
                    server.RequestCount = server.RequestCount + rndm;
                    isThereAvaibleServer = true;
                    return;
                }
            });
            if (!isThereAvaibleServer)
            {
                Console.WriteLine("Called CreateSubServer");
                await CreateSubServer(rndm);
            }
        }
        public async Task CheckSubServerCapacity()
        {
            Console.WriteLine("CheckSubServer CAPACITY");
            decimal percentage;
            Task check = Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    cancellationToken.Token.ThrowIfCancellationRequested();
                    SubServers.OrderBy(x => x.RequestCount).ToList().ForEach(async server =>
                     {
                         percentage = ((Convert.ToDecimal(server.RequestCount) / server.Capacity) * 100);
                         server.ServerProgressBar.ChangeValues(server.RequestCount > 0 ? server.RequestCount : 0, percentage);
                         if (server.RequestCount >= (server.Capacity * 0.70))
                         {
                             server.IsAvaible = false;
                             Console.WriteLine("Called CreateSubServer.");
                             await CreateSubServer(server.RequestCount / 2);
                             int req = server.RequestCount / 2;
                             server.RequestCount = (req);
                             server.IsAvaible = true;
                         }
                         if (server.RequestCount == 0 || server.RequestCount < 0)
                         {
                             if (SubServers.Count > 2)
                             {
                                 Console.WriteLine("Sub Server Req count = 0");
                                 Console.WriteLine("Thread Cancelled.");
                                 server.IsAvaible = false;
                                 server.CancellationToken.Cancel();
                                 if (myFlow2.InvokeRequired)
                                 {
                                     myFlow2.Invoke(new Action(() =>
                                     {
                                         myFlow2.Controls.Remove(server.ServerProgressBar);
                                     }));
                                 }
                                 else
                                 {
                                     myFlow2.Controls.Remove(server.ServerProgressBar);
                                 }
                                 SubServers.Remove(server);
                             }
                         }
                     });
                }
            }
            , cancellationToken.Token);

            Task getRequest = Task.Factory.StartNew(async () =>
           {
               while (true)
               {
                   cancellationToken.Token.ThrowIfCancellationRequested();
                   SubServers.ForEach((server) =>
                   {
                       if (MainServer.RequestCount > 50)
                           CallSubServer();
                   });
                   await Task.Delay(200);
               }
           }
            , cancellationToken.Token);
            Task response = Task.Factory.StartNew(async () =>
            {
                Random random = new Random();
                while (true)
                {
                    cancellationToken.Token.ThrowIfCancellationRequested();
                    SubServers.ForEach((server) =>
                    {
                        int rndm = random.Next(1, 50);
                        if (SubServers.Count == 2 && server.RequestCount <= 0)
                        {
                            server.RequestCount = 0;
                            return;
                        }
                        server.RequestCount -= rndm;
                    });
                    await Task.Delay(500);
                }
            }
             , cancellationToken.Token);
            cancellationToken.Token.Register(() =>
            {
                timer1.Stop();
                Console.WriteLine("All Operations Stopped.");
                Console.WriteLine("SubServers :" + SubServers.Count.ToString() + "\nMain server :" + MainServer.RequestCount.ToString());
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
                ServerProgressBar = new ServerProgressBar(0, 0, 5000, "Sub Server " + (SubServers.Count + 1)),
            };
            SubServers.Add(server);
            Console.WriteLine("SUBSERVER CREATED!");
            try
            {
                if (myConsole2.InvokeRequired)
                {
                    myConsole2.Invoke(new Action(() => myConsole2.AppendText("Sub Server Created..\n")));
                }
                else
                {
                    myConsole2.AppendText("Sub Server Created..");
                }
                if (myFlow2.InvokeRequired)
                {
                    myFlow2.Invoke(new Action(() =>
                    {
                        myFlow2.Controls.Add(server.ServerProgressBar);

                    }));
                }
                else
                {
                    myFlow2.Controls.Add(server.ServerProgressBar);
                }
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Invalid HATASI");
            }
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
            int i = 0;
            lblMainServer.Text = "Main Server Request: " + MainServer.RequestCount.ToString();
            LabelSubServer = "Sub Server Count: " + SubServers.Where(x => !x.CancellationToken.IsCancellationRequested).ToList().Count.ToString();
            //myConsole.AppendText("Sub Server Count: " + SubServers.Count.ToString() + "\n");
            try
            {
                SubServers.ForEach(server =>
                {
                    i++;
                    myConsole2.AppendText(i + ". Sub Server Count: " + server.RequestCount.ToString() + "\n");
                });
                i = 0;
            }
            catch (Exception)
            {

            }
        }
        private void btnStartMainServer_Click(object sender, EventArgs e)
        {
            MainServer.CancellationToken.Dispose();
            MainServer.CancellationToken = new CancellationTokenSource();
            AddRequestToMainServer();
            timer1.Start();
        }

        private void btnStopMainServer_Click(object sender, EventArgs e)
        {
            MainServer.CancellationToken.Cancel();
            timer1.Stop();
        }
    }
}
