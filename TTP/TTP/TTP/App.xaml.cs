using System;
using System.Collections.Generic;
using TTP.Data;
using TTP.Model;
using TTP.View;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TTP
{
    public partial class App : Application
    {
        public static GoodsItemManager GoodsManager { get; private set; }
        public static UserManager UserManager { get; private set; }
        public static TomatoTimeManager TomatoTimeManager { get; private set; }
        public static AppManager AppManager { get; private set; }
        public static User StaticUser { get;  set; }
        public static event Action<bool> ClockPageChanged;

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
        public App()
        {
            InitializeComponent();
            GoodsManager = new GoodsItemManager(new GoodsRestService());
            UserManager = new UserManager(new UserRestService());
            TomatoTimeManager = new TomatoTimeManager(new TomatoTimeService());
            AppManager = new AppManager(new AppRestService());
            StaticUser = new User();
            StaticUser.TotalTimes = new TimeSpan();
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
        private void InitWhiteList()
        {
            AppWhiteList = new List<string>();
            // 在数据库里加一个字段吧，白名单应用列表……
            AppWhiteList.Add("com.android.calculator2");
            AppWhiteList.Add("com.android.settings");
            AppWhiteList.Add("com.companyname.ttp"); // 一定要有
        }
    }
}
