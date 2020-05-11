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
            currentUser = App.StaticUser;
        }
    }
}
