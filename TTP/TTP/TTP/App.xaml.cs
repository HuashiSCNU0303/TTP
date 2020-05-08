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
                    Name = "一个测试用户",
                    PassWord="root",
                    Imgurl = "http://att3.citysbs.com/200x200/hangzhou/2020/04/15/11/dd6719bd4287d9efd49434c43563a032_v2_.jpg"
                };
            }
            else
            {
                StaticUser = null;
            }
        }
    }
}
