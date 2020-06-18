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
        private static ObservableCollection<ReceiveModel> aUsers;

        public ObservableCollection<ReceiveModel> AUsers
        {
            get { return aUsers; }
            set { aUsers = value; RaisePropertyChanged(); }
        }
        public ReceiveMsgViewModel() {
            aUsers = new ObservableCollection<ReceiveModel>();
            refresh();
        }

        public async static void refresh()
        {
            App.Receive.ForEach(async item =>
            {
                User user = await App.UserManager.GetUserTasksAsync(item.Key);
                aUsers.Add(new ReceiveModel()
                {
                    UserId = item.Key,
                    Avatar = user.Imgurl,
                    Name = user.Name,
                    Num=App.Receive[item.Key].Split('|').Length
                });
            });
        }
    }
    public class ReceiveModel 
    {
        public long UserId { get; set; }
        public string Avatar { get; set; }
        public string Name { get; set; }
        public int Num { get; set; }
    }
}
