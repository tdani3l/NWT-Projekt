using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Net;
using System.Data.Entity;

namespace GUI_CLIENT
{
    /// <summary>
    /// Interaktionslogik für SignUp.xaml
    /// </summary>
    public partial class SignUp : Window
    {
        SignalR_UserEntities entities = new SignalR_UserEntities();

        public SignUp()
        {
            InitializeComponent();

            entities.UserR.Load();
        }

        private void btn_signup_Click(object sender, RoutedEventArgs e)
        {
            UserR user = new UserR();
            if (tb_name.Text.Length >= 3 && tb_name.Text.Length <= 10)
            {
                user.UserName = tb_name.Text;
            }
            else
            {
                console.Text = "Username has to be between 3 and 10 chars long";
                return;
            }
            if (pb_passwd.Password.Length >= 6)
            {
                if(pb_passwd.Password == pb_passwd2.Password)
                {
                    user.Password = pb_passwd.Password;
                }
                else
                {
                    console.Text = "Passwords are not identical";
                }
            }
            else
            {
                console.Text = "Password has to be at least 6 chars long";
                return;
            }
            user.PreName = tb_preName.Text;
            user.LastName = tb_surName.Text;
            
            try
            {
                entities.UserR.Add(user);
                entities.SaveChanges();
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException)
            {
                console.Text = "There is already an User with this Name";
                return;
            }

            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
        }
    }
}
