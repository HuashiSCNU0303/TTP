using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTP.Model;
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
            User user2 = await App.UserManager.GetUserByNameTasksAsync(user.Name);
            if (user.PassWord == user2.PassWord)
            {
                await DisplayAlert("成功", "登陆成功！", "OK");
                App.StaticUser = user2;
                await Navigation.PopAsync();
            }
            else 
            {
                await DisplayAlert("错位", "密码错误！", "OK");
            }
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            User user = BindingContext as User;
            user.TomatoPoints = 0;
            user.Imgurl = null;
            User user2= await App.UserManager.AddUserTaskAsync(user);
            App.StaticUser = user2;
            await DisplayAlert("成功", "注册成功！", "OK");
            await Navigation.PopAsync();
        }
    }
}