using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Text;
using TTP.Model;

namespace TTP.ViewModel
{
    class PersonalCenterViewModel : ViewModelBase
    {
        private User currentUser;
        public User CurrentUser
        {
            get { return currentUser; }
            set { currentUser = value; RaisePropertyChanged(); }
        }

        public PersonalCenterViewModel()
        {
            User staticUser = App.StaticUser;
            if (staticUser.Imgurl == null || staticUser.Imgurl == "")
            {
                currentUser = new User()
                {
                    Name = staticUser.Name,
                    PassWord = staticUser.PassWord,
                    Imgurl = "person.png"
                };
            }
            else {
                currentUser = staticUser;
            }
            
        }
    }
}
