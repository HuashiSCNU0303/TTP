using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TTP.ViewModel;
using TTP.Model;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Syncfusion.XForms.Buttons;

namespace TTP.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainTomatoTimerPage : ContentPage
    {
        public MainTomatoTimerPage()
        {
            InitializeComponent();
            BindingContext = new TimeRecordViewModel();
            showTime();
        }

        public void showTime()
        {
            Device.StartTimer(TimeSpan.FromSeconds(1), () => {
                Device.BeginInvokeOnMainThread(()=>labelTime.Text = DateTime.Now.ToString());
                return true;
            });
        }

        private async void btnAddRecord_Clicked(object sender, EventArgs e)
        {
            var newPage = new AddTimeRecordPage();
            newPage.SetTimeEvent += OnTimeSet;
            await PopupNavigation.Instance.PushAsync(newPage);
        }

        private async void OnTimeSet(int hours, int minutes, string description)
        {
            if (hours != 0 || minutes != 0)
            {
                Console.WriteLine("开始时：" + description);
                await PopupNavigation.Instance.PushAsync(new ClockPage
                {
                    recordLength = TimeSpan.FromHours(hours).Add(TimeSpan.FromMinutes(minutes)),
                    dynamicLength = TimeSpan.FromHours(hours).Add(TimeSpan.FromMinutes(minutes)),
                    startTime = DateTime.Now,
                    description = description,
                    BindingContext = new TimeRecordModel()
                });
            }
        }


    }
}