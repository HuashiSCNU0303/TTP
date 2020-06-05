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
        public static User currentUser;
        public User CurrentUser
        {
            get { return currentUser; }
            set 
            { 
                currentUser = value;
                App.StaticUser = CurrentUser;
                RaisePropertyChanged(); 
            }
        }
        public PersonalCenterViewModel()
        {
            currentUser = new User();
            getUser();
        }

        public static void getUser() {
            currentUser = App.StaticUser;
            if (currentUser.Imgurl == null || currentUser.Imgurl == "")
            {
                currentUser.Imgurl = "person.png";
            }
        }
    }
}
