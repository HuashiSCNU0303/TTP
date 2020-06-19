using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TTP.Data;
using TTP.Model;
using TTP.View;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TTP
{
    public partial class App : Application
    {
        private static ClientWebSocket client;
        private static User staticUser;
        private static CancellationTokenSource cts;
        private static Thread thread;

        public static Dictionary<long,string> Receive;

        public static GoodsItemManager GoodsManager { get; private set; }
        public static UserManager UserManager { get; private set; }
        public static TomatoTimeManager TomatoTimeManager { get; private set; }
        public static AppManager AppManager { get; private set; }
        public static User StaticUser
        {
            get { return staticUser; }
            set
            {
                if (value.UserId == 0)
                {
                    return;
                }
                staticUser = value;
                try { thread.Start(); }
                catch (Exception e)
                {
                    return;
                }

            }
        }
        public static event Action<bool> ClockPageChanged;

        public static event Action<string> TextReceive;

        private static bool isClockPageOn = false;
        public static bool IsClockPageOn
        {
            get { return isClockPageOn; }
            set
            {
                isClockPageOn = value;
                ClockPageChanged(value);
            }
        }

        // 登录后在番茄钟页面加载用户近期的使用记录
        public static event Action<long> LogInStatusChanged;
        private static bool isLogIn = false;
        public static bool IsLogIn
        {
            get { return isLogIn; }
            set
            {
                isLogIn = value;
                LogInStatusChanged(StaticUser.UserId);
            }
        }

        public static List<string> AppWhiteList; // 白名单应用
        public static Dictionary<string, List<TomatoTime>> UserTomatoTimes { get; set; }
        public App()
        {
            InitializeComponent();
            Receive = new Dictionary<long, string>();
            GoodsManager = new GoodsItemManager(new GoodsRestService());
            UserManager = new UserManager(new UserRestService());
            TomatoTimeManager = new TomatoTimeManager(new TomatoTimeService());
            AppManager = new AppManager(new AppRestService());
            staticUser = new User();
            StaticUser.TotalTimes = new TimeSpan();
            UserTomatoTimes = new Dictionary<string, List<TomatoTime>>();

            client = new ClientWebSocket();
            cts = new CancellationTokenSource();
            thread = new Thread(() =>
            {
                ConnectToServerAsync();
            });

            //thread.Start();
            //判断以前是否登陆过
            JudgeloginstatusAsync();
            // InitWhiteList();
            MainPage = new MainPage();
            
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
        private void JudgeloginstatusAsync() 
        {
        }
        

        async void ConnectToServerAsync()
        {
            await client.ConnectAsync(new Uri(string.Format(Constants.WsUrl,StaticUser.UserId )), cts.Token);
            await Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    WebSocketReceiveResult result;
                    var message = new ArraySegment<byte>(new byte[4096]);
                    do
                    {
                        result = await client.ReceiveAsync(message, cts.Token);
                        var messageBytes = message.Skip(message.Offset).Take(result.Count).ToArray();
                        string serialisedMessae = Encoding.UTF8.GetString(messageBytes);

                        try
                        {
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                try
                                {
                                    TextReceive(serialisedMessae);
                                }
                                catch (Exception e){ 

                                }
                                string[] msgs = serialisedMessae.Split('|');
                                if (Receive.ContainsKey(Int32.Parse(msgs[1])))
                                {
                                    Receive[Int32.Parse(msgs[1])] += "|" + msgs[0];
                                }
                                else {
                                    Receive.Add(Int32.Parse(msgs[1]), msgs[0]);
                                }
                            });
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Invalide message format. {ex.Message}");
                        }

                    } while (!result.EndOfMessage);
                }
            }, cts.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);

        }
        public static async void SendMessageAsync(string message,long id)
        {
            var msg = new ChatMessage()
            {
                SenderId = StaticUser.UserId,
                ReceiverId = id,
                Message = message
            };

            string serialisedMessage = JsonConvert.SerializeObject(msg);

            var byteMessage = Encoding.UTF8.GetBytes(serialisedMessage);
            var segmnet = new ArraySegment<byte>(byteMessage);

            await client.SendAsync(segmnet, WebSocketMessageType.Text, true, cts.Token);
        }
    }
}
