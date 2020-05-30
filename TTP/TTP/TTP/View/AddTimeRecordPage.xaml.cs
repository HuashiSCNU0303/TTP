using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TTP.Model;
using TTP.ViewModel;
using TTP.Services;
using TTP.Data;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Globalization;

namespace TTP.View
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddTimeRecordPage : PopupPage
    {
        public event Action<int, int, string> SetTimeEvent;

        public static readonly BindableProperty HoursProperty = BindableProperty.Create("Hours", typeof(int), typeof(AddTimeRecordPage), 0);
        public static readonly BindableProperty MinutesProperty = BindableProperty.Create("Minutes", typeof(int), typeof(AddTimeRecordPage), 0);
        public static readonly BindableProperty DescriptionProperty = BindableProperty.Create("Description", typeof(string), typeof(AddTimeRecordPage), "");
        public int Hours
        {
            set { SetValue(HoursProperty, value); }
            get { return (int)GetValue(HoursProperty); }
        }
        public int Minutes
        {
            set { SetValue(MinutesProperty, value); }
            get { return (int)GetValue(MinutesProperty); }
        }
        public string Description
        {
            set { SetValue(DescriptionProperty, value); }
            get { return (string)GetValue(DescriptionProperty); }
        }

        public AddTimeRecordPage()
        {
            InitializeComponent();

            entryHours.BindingContext = this;
            entryHours.SetBinding(Entry.TextProperty, new Binding("Hours") { Converter = new ExamineConverter(), Mode = BindingMode.OneWayToSource });
            entryMinutes.BindingContext = this;
            entryMinutes.SetBinding(Entry.TextProperty, new Binding("Minutes") { Converter = new ExamineConverter(), Mode = BindingMode.OneWayToSource });
            entryDescription.BindingContext = this;
            entryDescription.SetBinding(Entry.TextProperty, new Binding("Description"));
        }

        private void btnReturn_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }

        protected override void OnDisappearing()
        {
            if (Hours != 0 || Minutes != 0)
            {
                DependencyService.Get<IToastService>().ShortAlert("开始锁机啦！");
                SetTimeEvent(Hours, Minutes, Description);
            }
            base.OnDisappearing();
        }

        private async void btnAddRecord_Clicked(object sender, EventArgs e)
        {
            if (Hours < 0 || Minutes < 0) 
            {
                await DisplayAlert("输入有误", "请重新输入！", "OK");
                return;
            }
            if (Minutes >= 60)
            {
                await DisplayAlert("输入有误", "分钟数应该＜60，请重新输入！", "OK");
                return;
            }
            
            await PopupNavigation.Instance.PopAsync();
        }
    }

    class ExamineConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                int value_int = System.Convert.ToInt32(value);
            }
            catch (FormatException)
            {
                return -1;
            }
            return value;
        }
    }
}