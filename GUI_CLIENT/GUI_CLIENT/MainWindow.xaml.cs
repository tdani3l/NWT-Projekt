using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace GUI_CLIENT
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SignalR_UserEntities entities = new SignalR_UserEntities();

        public MainWindow()
        {
            InitializeComponent();

            entities.UserR.Load();
        }

        private void btn_signin_Click(object sender, RoutedEventArgs e)
        {
            if (Authentication(tb_name.Text, pb_passwd.Password))
            {
                Chat window = new Chat(GetUser(tb_name.Text));
                window.Show();
                this.Close();
            }
            else pb_passwd.Password = "";
        }

        public bool Authentication(string userName, string password)
        {
            foreach (var user in entities.UserR)
            {
                if(user.UserName == userName)
                {
                    if(user.Password == password)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public UserR GetUser(string userName)
        {
            foreach (var user in entities.UserR)
            {
                if(user.UserName == userName)
                {
                    return user;
                }
            }            
            return null;
        }

        private void btn_signup_Click(object sender, RoutedEventArgs e)
        {
            SignUp window = new SignUp();
            window.Show();
            this.Close();
        }

        private void pb_passwd_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (Authentication(tb_name.Text, pb_passwd.Password))
                {
                    Chat window = new Chat(GetUser(tb_name.Text));
                    window.Show();
                    this.Close();
                }
                else pb_passwd.Password = "";
            }
        }
    }
}
