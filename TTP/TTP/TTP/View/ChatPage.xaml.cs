using Newtonsoft.Json;
using Syncfusion.XForms.Chat;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TTP.Model;
using TTP.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Markup;
using Xamarin.Forms.Xaml;

namespace TTP.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatPage : ContentPage
    {
        private ClientWebSocket client;
        private CancellationTokenSource cts;
        private Thread thread;
        public ChatPage()
        {
            InitializeComponent();
            sfChat.SendMessage += SfChat_SendMessage;
            client = new ClientWebSocket();
            cts = new CancellationTokenSource();
            thread = new Thread(() =>
            {
                ConnectToServerAsync();
            });
            thread.Start();
        }

        private void SfChat_SendMessage(object sender, Syncfusion.XForms.Chat.SendMessageEventArgs e)
        {
            SendMessageAsync(e.Message.Text);
        }

        async void ConnectToServerAsync()
        {
            CharPageViewModel charPageViewModel = BindingContext as CharPageViewModel;
            await client.ConnectAsync(new Uri(string.Format(Constants.WsUrl, charPageViewModel.CurrentUser.UserId)), cts.Token);
            await Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    WebSocketReceiveResult result;
                    var message = new ArraySegment<byte>(new byte[4096]);
                    do
                    {
                        result = await client.ReceiveAsync(message, cts.Token);
                        var messageBytes = message.Skip(message.Offset).Take(result.Count).ToArray();
                        string serialisedMessae = Encoding.UTF8.GetString(messageBytes);

                        try
                        {
                            //var msg = JsonConvert.DeserializeObject<Message>(serialisedMessae);
                            Debug.WriteLine(@"\tERROR {0}", serialisedMessae);
                            
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                charPageViewModel.Messages.Add(new TextMessage()
                                {
                                    Author = new Author() { Name = charPageViewModel.SendToUser.Name, Avatar = charPageViewModel.SendToUser.Avatar },
                                    Text = serialisedMessae
                                }
                                );

                                BindingContext = charPageViewModel;
                            });
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Invalide message format. {ex.Message}");
                        }

                    } while (!result.EndOfMessage);
                }
            }, cts.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);

        }
        async void SendMessageAsync(string message)
        {
            CharPageViewModel charPageViewModel = BindingContext as CharPageViewModel;
            var msg = new ChatMessage()
            {
                SenderId = charPageViewModel.CurrentUser.UserId,
                ReceiverId = charPageViewModel.SendToUser.UserId,
                Message = message
            };

            string serialisedMessage = JsonConvert.SerializeObject(msg);

            var byteMessage = Encoding.UTF8.GetBytes(serialisedMessage);
            var segmnet = new ArraySegment<byte>(byteMessage);

            await client.SendAsync(segmnet, WebSocketMessageType.Text, true, cts.Token);
        }
    }
}