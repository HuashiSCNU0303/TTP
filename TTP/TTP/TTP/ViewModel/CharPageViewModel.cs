using Syncfusion.DataSource.Extensions;
using Syncfusion.XForms.Chat;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace TTP.ViewModel
{
    public class CharPageViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<object> messages;

        /// <summary>
        /// Current user of chat.
        /// </summary>
        private AUser currentUser;
        private AUser sendToUser;

        public CharPageViewModel()
        {
            messages = new ObservableCollection<object>();
            currentUser = new AUser() { Name = "张俊杰", Avatar = "person.png", UserId = 14 };
            sendToUser = new AUser { Name = "朋友", Avatar = "person.png", UserId = 14 };
            GenerateMessages();
        }

        public CharPageViewModel(AUser a,AUser b)
        {
            messages = new ObservableCollection<object>();
            currentUser = a;
            sendToUser = b;
        }

        public CharPageViewModel(AUser a, AUser b,string[] msgs)
        {
            messages = new ObservableCollection<object>();
            currentUser = a;
            sendToUser = b;
            msgs.ForEach(msg=>messages.Add(new TextMessage() {
                Author = sendToUser,
                Text = msg,
            }));
        }


        /// <summary>
        /// Gets or sets the collection of messages of a conversation.
        /// </summary>
        public ObservableCollection<object> Messages
        {
            get
            {
                return this.messages;
            }

            set
            {
                this.messages = value;
            }
        }

        /// <summary>
        /// Gets or sets the current user of the message.
        /// </summary>
        public AUser CurrentUser
        {
            get
            {
                return this.currentUser;
            }
            set
            {
                this.currentUser = value;
                RaisePropertyChanged("CurrentUser");
            }
        }

        public AUser SendToUser
        {
            get
            {
                return this.sendToUser;
            }
            set
            {
                this.currentUser = value;
                RaisePropertyChanged("sendToUser");
            }
        }

        /// <summary>
        /// Property changed handler.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Occurs when property is changed.
        /// </summary>
        /// <param name="propName">changed property name</param>
        public void RaisePropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        private void GenerateMessages()
        {
            this.messages.Add(new TextMessage()
            {
                Author = currentUser,
                Text = "Hi guys, good morning! I'm very delighted to share with you the news that our team is going to launch a new mobile application.",
            });

            this.messages.Add(new TextMessage()
            {
                Author = sendToUser,
                Text = "Oh! That's great.",
            });

            this.messages.Add(new TextMessage()
            {
                Author = sendToUser,
                Text = "That is good news.",
            });

            this.messages.Add(new TextMessage()
            {
                Author = sendToUser,
                Text = "What kind of application is it and when are we going to launch?",
            });

            this.messages.Add(new TextMessage()
            {
                Author = currentUser,
                Text = "A kind of Emergency Broadcast App.",
            });
        }

        public class AUser : Author
        {
            public long UserId{get;set;}
        }
    }
}
