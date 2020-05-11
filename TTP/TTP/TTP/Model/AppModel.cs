using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TTP.Model
{
    class AppModel : ViewModelBase
    {
        public string AppName { get; set; }
        public string PackageName { get; set; }
        public ImageSource AppIcon { get; set; }
    }
}
