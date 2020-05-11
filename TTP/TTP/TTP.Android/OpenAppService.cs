using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using TTP.Services;
using Xamarin.Forms;

namespace TTP.Droid
{
    class OpenAppService : Activity, IOpenAppService
    {
        public OpenAppService()
        {

        }
        public Task<bool> Launch(string packageName)
        {
            Intent intent = Android.App.Application.Context.PackageManager.GetLaunchIntentForPackage(packageName);
            if (intent != null) 
            {
                intent.AddFlags(ActivityFlags.NewTask);
                Forms.Context.StartActivity(intent);
            }
            return Task.FromResult(true);
        }

        public string GetAppName(string packageName)
        {
            var appInfo = Android.App.Application.Context.PackageManager.GetApplicationInfo(packageName, Android.Content.PM.PackageInfoFlags.MetaData);
            var name = Android.App.Application.Context.PackageManager.GetApplicationLabel(appInfo);
            return name;
        }

        public ImageSource GetAppIcon(string packageName)
        {
            var icon = Android.App.Application.Context.PackageManager.GetApplicationIcon(packageName);
            int width = icon.IntrinsicWidth;
            int heigh = icon.IntrinsicHeight;
            icon.SetBounds(0, 0, width, heigh);
            Bitmap.Config config = (icon.Opacity != (int)Format.Opaque) ? Bitmap.Config.Argb8888 : Bitmap.Config.Rgb565;
            Bitmap bitmap = Bitmap.CreateBitmap(width, heigh, config);
            Canvas canvas = new Canvas(bitmap);
            icon.Draw(canvas);

            var stream = new MemoryStream();
            bitmap.Compress(Bitmap.CompressFormat.Png, 100, stream);
            bitmap.Recycle();
            byte[] bytes = stream.ToArray();

            var imageSource = ImageSource.FromStream(() => new MemoryStream(bytes));
            return imageSource;
        }
    }
}