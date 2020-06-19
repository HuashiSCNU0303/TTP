using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTP.Model;
using TTP.Services;
using TTP.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TTP.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            BindingContext = new User();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            User user = BindingContext as User;
            if (user.Name == "" || user.Name == null || user.PassWord == "" || user.PassWord == null) 
            {
                DependencyService.Get<IToastService>().LongAlert("用户名和密码不能为空");
                return;
            }
            User user2 = await App.UserManager.GetUserByNameTasksAsync(user.Name);
            if (user.PassWord == user2.PassWord)
            {
                DependencyService.Get<IToastService>().LongAlert("登陆成功！");
                App.StaticUser = user2;
                user2.TotalTimes = new TimeSpan();
                App.CurrentUserID = App.StaticUser.UserId;
                await Navigation.PopAsync();
            }
            else 
            {
                DependencyService.Get<IToastService>().LongAlert("密码错误！");
            }
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            User user = BindingContext as User;
            if (user.Name == "" || user.Name == null || user.PassWord == "" || user.PassWord == null)
            {
                DependencyService.Get<IToastService>().LongAlert("用户名和密码不能为空");
                return;
            }
            user.TomatoPoints = 0;
            user.Imgurl = null;
            User user2 = await App.UserManager.AddUserTaskAsync(user);
            App.StaticUser = user2;
            user2.TotalTimes = new TimeSpan();
            App.CurrentUserID = App.StaticUser.UserId;
            DependencyService.Get<IToastService>().LongAlert("注册成功！");
            await Navigation.PopAsync();
        }
    }
}