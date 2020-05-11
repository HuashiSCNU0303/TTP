using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TTP.Model;

namespace TTP.ViewModel
{
    public class PersonalCenterViewModel : ViewModelBase
    {
        private static User currentUser;
        public User CurrentUser
        {
            get { return currentUser; }
            set { currentUser = value; RaisePropertyChanged(); }
        }
        public PersonalCenterViewModel()
        {
            currentUser =new User();
            getUser();
        }

        public async void getUser() {
            User user = await App.UserManager.GetUserTasksAsync(20);
            CurrentUser = user;
            if (CurrentUser.Imgurl == null || CurrentUser.Imgurl == "")
            {
                CurrentUser.Imgurl = "person.png";
            }
            App.StaticUser = CurrentUser;
        }
    }
}
