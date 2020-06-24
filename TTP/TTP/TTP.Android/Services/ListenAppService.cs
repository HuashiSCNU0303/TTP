using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

using Android.App;
using Android.App.Usage;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using Java.Lang;
using Java.Lang.Reflect;

namespace TTP.Droid
{
    [Service(Exported = true)]
    class ListenAppService : Service
    {
        Timer timer;
        Dictionary<string, bool> whiteList;
        public const int SERVICE_RUNNING_NOTIFICATION_ID = 10000;
        public const string ChannelID = "MyChannel";
        public const string ChannelName = "ListenAppService";
        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public override void OnCreate()
        {
            base.OnCreate();
            whiteList = new Dictionary<string, bool>();
            timer = new Timer(HandleTimerCallback, null, 100, 500);
        }

        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            IList<string> list = intent.GetStringArrayListExtra("WhiteList");
            whiteList.Clear();
            foreach (string item in list)
            {
                if (!whiteList.ContainsKey(item))
                {
                    whiteList.Add(item, true);
                }
            }

            // 设置前台服务
            NotificationChannel chan = new NotificationChannel(ChannelID, ChannelName, NotificationImportance.None);
            chan.LockscreenVisibility = NotificationVisibility.Private;
            NotificationManager nm = (NotificationManager)GetSystemService(NotificationService);
            nm.CreateNotificationChannel(chan);
            var notificationBuilder = new NotificationCompat.Builder(this, ChannelID);
            var notification = notificationBuilder.SetOngoing(true)
                                                  .SetSmallIcon(Resource.Drawable.timer1)
                                                  .SetContentTitle("TTP")
                                                  .SetContentText("正在锁机中，不允许切换到白名单以外的应用...")
                                                  .Build();
            StartForeground(SERVICE_RUNNING_NOTIFICATION_ID, notification);
            return base.OnStartCommand(intent, flags, startId);
        }

        public override void OnDestroy()
        {
            timer.Dispose();
            base.OnDestroy();
        }

        void HandleTimerCallback(object state)
        {
            UsageStatsManager mUsageStatsManager = (UsageStatsManager)GetSystemService(UsageStatsService);
            long endTime = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
            long beginTime = endTime - 1000 * 60 * 2;
            string topPackageName = "";

            IList<UsageStats> queryUsageStats = mUsageStatsManager.QueryUsageStats(UsageStatsInterval.Best, beginTime, endTime);
            if (queryUsageStats == null || queryUsageStats.Count == 0)
            {
                return;
            }
            queryUsageStats = queryUsageStats.OrderByDescending(u => u.LastTimeUsed).ToList();

            UsageStats usageStatsIns = queryUsageStats[0];
            Field mLastEventField = null;
            try
            {
                mLastEventField = usageStatsIns.Class.GetField("mLastEvent");
            }
            catch (Java.Lang.Exception e)
            {
                e.PrintStackTrace();
            }

            foreach (UsageStats usageStats in queryUsageStats)
            {
                if (mLastEventField != null)
                {
                    int lastEvent = 0;
                    try
                    {
                        lastEvent = mLastEventField.GetInt(usageStats);
                    }
                    catch (IllegalAccessException e)
                    {
                        e.PrintStackTrace();
                    }

                    if (lastEvent == 1)
                    {
                        topPackageName = usageStats.PackageName;
                        break;
                    }
                }
            }
            Console.WriteLine("当前应用的包名：" + topPackageName);
            if (!whiteList.ContainsKey(topPackageName))
            {
                Intent intent = new Intent("com.companyname.ttp.ListenAppService");
                intent.PutExtra("Message", 40);
                SendBroadcast(intent);
            }
        }
    }
}