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
    public struct Server
    {
        public int RequestCount { get; set; }
        public int Capacity { get; set; }
        public bool IsAvaible { get; set; }
        public CancellationTokenSource CancellationToken { get; set; }
    };
    public partial class Form1 : Form, INotifyPropertyChanged
    {
        public List<Server> SubServers;
        public Server MainServer;
        private CancellationTokenSource cancellationToken;
        private string _foo;

        public string Foo
        {
            get { return _foo; }
            set
            {
                _foo = value;
                OnPropertyChanged("Foo");
            }
        }

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
            lblSubServers.DataBindings.Add(new Binding("Text", this, "Foo"));
            SubServers = new List<Server>();
            SubServers.Add(new Server()
            {
                RequestCount = 10,
                IsAvaible = true,
                Capacity = 5000,
                CancellationToken = new CancellationTokenSource()
            });
            SubServers.Add(new Server()
            {
                Capacity = 5000,
                IsAvaible = true,
                RequestCount = 10,
                CancellationToken = new CancellationTokenSource()
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
        }
        public async Task AddRequestToMainServer()
        {
            Random randomNumber = new Random();
            Task addRequest = Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    MainServer.CancellationToken.Token.ThrowIfCancellationRequested();
                    MainServer.RequestCount += randomNumber.Next(1, 100);
                    await MainServerController();
                    //Console.WriteLine("Added Request - >Main Server Request Count :{0}", MainServer.RequestCount);
                    await Task.Delay(100);
                }
            }
            , MainServer.CancellationToken.Token);

            Task responseRequest = Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    MainServer.CancellationToken.Token.ThrowIfCancellationRequested();
                    MainServer.RequestCount -= randomNumber.Next(1, 50);
                    //Console.WriteLine("Response Request - >Main Server Request Count :{0}", MainServer.RequestCount);
                    await Task.Delay(200);
                }
            }
            , MainServer.CancellationToken.Token);

            MainServer.CancellationToken.Token.Register(() =>
            {
                Console.WriteLine("request stop");
            });
        }

        public async Task MainServerController()
        {
            //Console.WriteLine("Main Server Request Count :" + MainServer.RequestCount.ToString());
            if (MainServer.RequestCount >= (MainServer.Capacity * 0.20))
            {
                //Console.WriteLine("MAIN SERVER 70 PERCENT FULL");
                await CallSubServer();
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
                    int count = server.RequestCount + 50;
                    server = new Server() {
                        Capacity = 5000,
                        RequestCount = count,
                        CancellationToken = new CancellationTokenSource(),
                        IsAvaible = count > 5000 ? false : true
                    };
                    server.RequestCount = server.RequestCount + 50;
                    isThereAvaibleServer = true;
                    return;
                }
            });
            if (!isThereAvaibleServer)
            {
                Console.WriteLine("called createSubServer");
                await CreateSubServer(50);
            }
        }
        public async Task CheckSubServerCapacity()
        {
            Console.WriteLine("checkSubServer CAPACITY");
            bool isThereAvaibleSubServer = false;
            Label txt = null;
            txt = lblSubServers;
            Task check = await Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    cancellationToken.Token.ThrowIfCancellationRequested();
                    SubServers.ForEach(async server =>
                    {
                        server.IsAvaible = server.RequestCount >= (server.Capacity * 0.20) ? false : true;
                        if (!server.IsAvaible)
                        {
                            Console.WriteLine("called createSubServer.");
                            await CreateSubServer(server.RequestCount / 2);
                            server.RequestCount = server.RequestCount / 2;
                            server.IsAvaible = true;
                        }
                        if (server.RequestCount == 0 || server.RequestCount < 0)
                        {
                            Console.WriteLine("sub server req count = 0");
                            if (SubServers.Count > 2)
                            {
                                Console.WriteLine("thread cancelled.");
                                server.CancellationToken.Cancel();
                                SubServers.Remove(server);
                            }
                        }
                    });
                }
            }
            , cancellationToken.Token);
            //Task hello = Task.Factory.StartNew(async () =>
            //{
            //    while (true)
            //    {
            //        cancellationToken.Token.ThrowIfCancellationRequested();
            //        //SubServers.ForEach(async server =>
            //        //{
            //        //    Console.WriteLine("hello");
            //        //    await Task.Delay(60000);
            //        //});
            //    }
            //}, cancellationToken.Token);

            cancellationToken.Token.Register(() =>
            {
                Console.WriteLine("all operations stopped.");
                Console.WriteLine("subservers :" + SubServers.Count.ToString() + "\nMain server :" + MainServer.RequestCount.ToString());
            });
        }
        public async Task CreateSubServer(int requestCount)
        {
            Console.WriteLine("SUBSERVER CREATED!");
            SubServers.Add(new Server()
            {
                IsAvaible = true,
                RequestCount = requestCount,
                CancellationToken = new CancellationTokenSource(),
                Capacity = 5000
            });
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
        //public async Task FirstMethod(CancellationToken cancellationToken)
        //{
        //    if (cancellationToken.IsCancellationRequested)
        //    {
        //        Console.WriteLine("first thread canceled ");
        //        return;
        //    }
        //    await Task.Run(async () =>
        //    {
        //        //await Task.Delay(7000);
        //        Console.WriteLine("first method running");
        //    });
        //    Console.WriteLine("first method..");
        //}
        //public async Task Second(CancellationToken cancellationToken)
        //{
        //    if (cancellationToken.IsCancellationRequested)
        //    {
        //        Console.WriteLine("second thread canceled ");
        //        return;
        //    }
        //    Console.WriteLine("second  method..");

        //}


    }
}
