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
using ChatStream.ServiceReference1;
using System.Text.RegularExpressions;
namespace ChatStream
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        Service1Client srv = new Service1Client();
        public Login()
        {
            InitializeComponent();
        }

        private void SignUpW(object sender, RoutedEventArgs e)
        {
            SignUp signUp = new SignUp();
            signUp.Show();
            this.Close();
        }
        /// <summary>
        /// the button click collects the user's password and username, and logins if the info
        /// is valid
        /// </summary>
        /// <param name="sender">button</param>
        /// <param name="e"></param>
        private void EnterB(object sender, RoutedEventArgs e)
        {
            User user = null;
            if(UName.Text.Length >= 3 && UPassword.Password.Length >= 4)
            {
                if(!Regex.IsMatch(UName.Text, "^[a-zA-Z0-9]*$")) //condition checks if username is numbers and letters only.
                {
                    MessageBox.Show("User name should contain only letters and numbers");
                }
                else
                {
                    //tries to log in:
                    user = srv.Login(UName.Text, UPassword.Password);
                    if (user != null && user.ID > 0)
                    {//succsessful login
                        MainWindow main = new MainWindow(user);
                        main.Show();
                        this.Close();
                    }
                    else
                    {//failed to login
                        MessageBox.Show("Failed to login. Check Your User name and password!");
                    }
                }
            }
            else
            {
                MessageBox.Show("Username should contain at least 3 characters and password at least 4");
            } 
        }
    }
}
