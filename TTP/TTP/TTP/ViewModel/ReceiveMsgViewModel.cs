using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TTP.Model;
using Xamarin.Forms.Internals;

namespace TTP.ViewModel
{
    public class ReceiveMsgViewModel : ViewModelBase
    {
        private static ObservableCollection<CharPageViewModel.AUser> aUsers;

        public ObservableCollection<CharPageViewModel.AUser> AUsers
        {
            get { return aUsers; }
            set { aUsers = value; RaisePropertyChanged(); }
        }
        public ReceiveMsgViewModel() {
            aUsers = new ObservableCollection<CharPageViewModel.AUser>();
            refresh();
        }

        public async static void refresh()
        {
            App.Receive.ForEach(async item =>
            {
                User user = await App.UserManager.GetUserTasksAsync(item.Key);
                aUsers.Add(new CharPageViewModel.AUser()
                {
                    UserId = item.Key,
                    Avatar = user.Imgurl,
                    Name = user.Name
                });
            });
        }
    }
}
