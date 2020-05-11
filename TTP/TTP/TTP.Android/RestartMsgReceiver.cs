using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace TTP.Droid
{
    [BroadcastReceiver]
    public class RestartMsgReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            ActivityManager activityManager = context.ApplicationContext.GetSystemService(Context.ActivityService) as ActivityManager;
            foreach (var taskInfo in activityManager.AppTasks) 
            {
                if (taskInfo.TaskInfo.Id == MainActivity.MyTaskId)
                {
                    taskInfo.MoveToFront();
                    break;
                }
            }
        }
    }
}