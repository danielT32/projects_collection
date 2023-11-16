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
using System.Net.Mail;

namespace ChatStream
{
    /// <summary>
    /// Interaction logic for SignUp.xaml
    /// </summary>
    
    public partial class SignUp : Window
    {
        Service1Client srv = new Service1Client();
        public SignUp()
        {
            InitializeComponent();
        }

        private void LoginW(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();
            
        }
        /// <summary>
        /// method checks if entered data is valid.
        /// </summary>
        /// <param name="user">users data</param>
        /// <returns>true if valid, false if not</returns>
        private bool IsUserValid(User user)
        {
            bool valid = true;
            if (!Regex.IsMatch(user.Uname, "^[a-zA-Z0-9]*$") || user.Uname.Length < 3)
            {
                valid = false;
                MessageBox.Show("User name Should contain only letters and/or numbers\n" +
                    "and the length is 3 characters minimum");
            }
            if (!Regex.IsMatch(user.Lname, "^[a-zA-Z0-9]*$") || user.Lname.Length < 3)
            {
                valid = false;
                MessageBox.Show("last name Should contain only letters and/or numbers\n" +
                    "and the length is 3 characters minimum");
            }
            if (!Regex.IsMatch(user.Fname, "^[a-zA-Z0-9]*$") || user.Fname.Length < 3)
            {
                MessageBox.Show("first name Should contain only letters and/or numbers\n" +
                    "and the length is 3 characters minimum");
                valid = false;
            }
            if ( user.Password.Length < 4)
            {
                MessageBox.Show("Password should contain 4 characters minimum");
                valid = false;
            }
            try//check if email is in valid format by trying create MailAddress object.
            {
                MailAddress mailAddress = new MailAddress(user.Email);
            }
            catch (Exception)
            {
                valid = false;
                MessageBox.Show("Not valid Email, check again your mail adress");
            }
            return valid;
        }
        /// <summary>
        /// adds user to the server if valid data was entered. than logins and moves
        /// to the main window
        /// </summary>
        /// <param name="sender">button</param>
        /// <param name="e"></param>
        private void EnterBtn(object sender, RoutedEventArgs e)
        {
            User user = new User();
            if (UPassword1.Password == UPassword2.Password)
            {
                //add info to a User object
                user.Uname = UName.Text;
                user.Lname = Lname.Text;
                user.Fname = FName.Text;
                user.Password = UPassword1.Password;
                user.EntrDate = DateTime.Now;
                user.Description = user.Uname;
                user.Email = EMail.Text;
                if (IsUserValid(user))
                {
                    srv.InsertUser(user);
                    user = srv.Login(user.Uname, user.Password);
                    if (user != null)
                    {
                        MainWindow main = new MainWindow(user);
                        main.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Failed to enter, try another username");
                    }
                }
            }
            else
            {
                MessageBox.Show("The First password and the Second password don't match!");
            }  
        }
    }
}
