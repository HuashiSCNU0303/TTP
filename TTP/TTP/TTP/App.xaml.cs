using System;
using System.Collections.Generic;
using TTP.Data;
using TTP.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TTP
{
    public partial class App : Application
    {
        public static TimeRecordManager RecordManager { get; private set; }
        public static GoodsItemManager GoodsManager { get; private set; }
        public static User StaticUser { get;  set; }

        public static event Action<bool> ClockPageChanged;
        
        private static bool isClockPageOn = false;
        public static bool IsClockPageOn
        {
            get
            {
                return isClockPageOn;
            }
            set
            {
                isClockPageOn = value;
                ClockPageChanged(value);
            } 
        }

        public static List<string> AppWhiteList; // 白名单应用
        public App()
        {
            InitializeComponent();
            GoodsManager = new GoodsItemManager(new GoodsRestService());
            RecordManager = new TimeRecordManager();
            //判断以前是否登陆过
            Judgeloginstatus();
            InitWhiteList();
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
        private void Judgeloginstatus() {
            if (true)
            {
                StaticUser = new User()
                {
                    Name = "wo"
                };
            }
            else
            {
                StaticUser = null;
            }
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
