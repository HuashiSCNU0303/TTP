using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Provider;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content;
using System.IO;
using System.Threading.Tasks;
using Acr.UserDialogs;

namespace TTP.Droid
{
    [Activity(Label = "TTP", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        internal static MainActivity Instance { get; private set; }
        Intent listenAppservice;
        private readonly static int PackageUsageStatsId = 2000;
        public static int MyTaskId;
        RestartMsgReceiver restartMsgReceiver = null;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            UserDialogs.Init(this);
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
            Instance = this;

            App.ClockPageChanged += SetService;
            Xamarin.Forms.DependencyService.Register<OpenAppService>();
            restartMsgReceiver = new RestartMsgReceiver();
            RegisterReceiver(restartMsgReceiver, new IntentFilter("com.companyname.ttp.ListenAppService"));

            MyTaskId = TaskId;

            // 获取本机所有应用
            Console.WriteLine("初始化本机应用！");
            App.AppManager.InitAllApps();
            // 检查PACKAGE_USAGE_STATS权限
            AppOpsManager appOps = (AppOpsManager)GetSystemService(AppOpsService);
            int mode = (int)appOps.CheckOpNoThrow("android:get_usage_stats", Process.MyUid(), PackageName);
            if (mode != (int)AppOpsManagerMode.Allowed)
            {
                AlertDialog.Builder builder = new AlertDialog.Builder(this);
                AlertDialog alertDialog = builder.SetTitle("请求权限")
                                                 .SetMessage("需要访问应用使用情况的权限，否则无法使用")
                                                 .SetPositiveButton("好的", new EventHandler<DialogClickEventArgs>((sender, e) =>
                                                 {
                                                     Intent intent = new Intent(Settings.ActionUsageAccessSettings);
                                                     StartActivityForResult(intent, PackageUsageStatsId);
                                                 }))
                                                 .SetNegativeButton("不给", new EventHandler<DialogClickEventArgs>((sender, e) =>
                                                 {
                                                     Finish();
                                                 }))
                                                 .SetCancelable(false).Create();
                alertDialog.Show();
            }

        }
        public static readonly int PickImageId = 1000;
        public TaskCompletionSource<Stream> PickImageTaskCompletionSource { set; get; }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent intent)
        {
            base.OnActivityResult(requestCode, resultCode, intent);

            if (requestCode == PickImageId)
            {
                if ((resultCode == Result.Ok) && (intent != null))
                {
                    Android.Net.Uri uri = intent.Data;
                    Stream stream = ContentResolver.OpenInputStream(uri);

                    // Set the Stream as the completion of the Task
                    PickImageTaskCompletionSource.SetResult(stream);
                }
                else
                {
                    PickImageTaskCompletionSource.SetResult(null);
                }
            }
            else if (requestCode == PackageUsageStatsId)
            {
                AppOpsManager appOps = (AppOpsManager)GetSystemService(AppOpsService);
                int mode = (int)appOps.CheckOpNoThrow("android:get_usage_stats", Process.MyUid(), PackageName);
                if (mode == (int)AppOpsManagerMode.Allowed)
                {
                    AlertDialog.Builder builder = new AlertDialog.Builder(this);
                    AlertDialog alertDialog = builder.SetMessage("权限已获取")
                                                     .SetCancelable(true).Create();
                    alertDialog.Show();
                }
                else
                {
                    AlertDialog.Builder builder = new AlertDialog.Builder(this);
                    AlertDialog alertDialog = builder.SetTitle("尚未给予权限")
                                                     .SetMessage("需要访问应用使用情况的权限，否则无法使用")
                                                     .SetPositiveButton("好的", new EventHandler<DialogClickEventArgs>((sender, e) =>
                                                     {
                                                         Intent intent = new Intent(Settings.ActionUsageAccessSettings);
                                                         StartActivityForResult(intent, PackageUsageStatsId);
                                                     }))
                                                     .SetNegativeButton("不给", new EventHandler<DialogClickEventArgs>((sender, e) =>
                                                     {
                                                         Finish();
                                                     })).Create();
                    alertDialog.Show();
                }
            }
        }

        public override void OnBackPressed()
        {
            if (!CHD.HWBackButtonManager.Instance.NotifyHWBackButtonPressed())
            {
                return;
            }
            base.OnBackPressed();
        }

        public void SetService(bool isClockPageOn)
        {
            if (isClockPageOn)
            {
                if (listenAppservice == null)
                {
                    listenAppservice = new Intent(this, typeof(ListenAppService));
                    listenAppservice.PutStringArrayListExtra("WhiteList", App.AppManager.WhiteList);
                    foreach (var temp in App.AppManager.WhiteList)
                    {
                        Console.WriteLine("此时白名单：" + temp);
                    }
                }
                StartService(listenAppservice);
            }
            else
            {
                if (listenAppservice != null)
                {
                    StopService(listenAppservice);
                    listenAppservice = null;
                }
            }
        }
    }
}