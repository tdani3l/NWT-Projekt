using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace GUI_CLIENT
{
    /// <summary>
    /// Interaktionslogik für Chat.xaml
    /// </summary>
    public partial class Chat : Window
    {
        static HubConnection hubConnection = new HubConnection("http://localhost:24529");

        IHubProxy chatHubProxy = hubConnection.CreateHubProxy("ChatHub");

        private static string name;

        private string logCode = "lgchckevrnwlg";

        private string logApperanceChanged = "lgchckpprncchngd";

        private void ReceiveMessage(string text)
        {
            Application.Current.Dispatcher.BeginInvoke(
            DispatcherPriority.Background,
            new Action(() => 
                {
                    try
                    {
                        if (text.Contains(logCode))
                        {
                            UserBubble bubble = new UserBubble();
                            text = text.Replace(logCode,"");
                            bubble.name.Text = text;
                            sp_user.Children.Add(bubble);
                            sv_user.ScrollToBottom();
                        }
                        else if (text.Contains(logApperanceChanged))
                        {
                            sp_user.Children.Clear();
                            chatHubProxy.Invoke("Broadcast", logCode + name).Wait();
                        }
                        else
                        {
                            MessageBubble bubble = new MessageBubble();
                            var cut = text.Split('§');
                            bubble.name.Text = cut[0];
                            bubble.message.Text = cut[1];
                            bubble.date.Text = cut[2];
                            sp_messages.Children.Add(bubble);
                            sv_messages.ScrollToBottom();
                        }
                    }
                    catch (System.IndexOutOfRangeException)
                    {

                    }
                }
            ));
        }

        public Chat(UserR user)
        {
            InitializeComponent();

            name = user.UserName;

            Action<string> targetFunc = ReceiveMessage;
            chatHubProxy.On("ReceiveMessage", targetFunc);

            hubConnection.Start().Wait();
        }

        private void btn_senden_Click(object sender, RoutedEventArgs e)
        {
            hubConnection.Start().Wait();

            if (sender != null)
            {
                string message = tb_senden.Text;
                tb_senden.Text = "";

                chatHubProxy.Invoke("Broadcast", name + "§" + message + "§" + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString()).Wait();

                MessageBubble bubble = new MessageBubble();
                bubble.HorizontalAlignment = HorizontalAlignment.Right;
                Thickness margin = bubble.grid_bubble.Margin;
                margin.Left = 100;
                margin.Right = 5;
                margin.Top = 2;
                margin.Bottom = 2;
                bubble.grid_bubble.Margin = margin;
                bubble.name.Text = name;
                bubble.message.Text = message;
                bubble.message.TextAlignment = TextAlignment.Right;
                bubble.date.Text = DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString();
                sp_messages.Children.Add(bubble);
                sv_messages.ScrollToBottom();
            }
            else
            {
                chatHubProxy.Invoke("Broadcast", logApperanceChanged).Wait();
                Application.Current.Dispatcher.BeginInvoke(
                DispatcherPriority.Background,
                new Action(() =>
                {
                    chatHubProxy.Invoke("Broadcast", logCode + name).Wait();
                }));
            }            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            btn_senden_Click(null, null);
        }

        private void tb_senden_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (tb_senden.Text == "enter message here..")
            {
                tb_senden.Text = "";
                tb_senden.FontStyle = FontStyles.Normal;
                tb_senden.FontWeight = FontWeights.Normal;
            }
        }

        private void tb_senden_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btn_senden_Click(sender, e);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            chatHubProxy.Invoke("Broadcast", logApperanceChanged).Wait();
        }
    }
}
