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
        public static GoodsItemManager GoodsManager { get; private set; }
        public static User StaticUser { get;  set; }
        public App()
        {
            InitializeComponent();
            GoodsManager = new GoodsItemManager(new GoodsRestService());
            //判断以前是否登陆过
            Judgeloginstatus();
            MainPage = new NavigationPage(new MainPage());
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
    }
}
