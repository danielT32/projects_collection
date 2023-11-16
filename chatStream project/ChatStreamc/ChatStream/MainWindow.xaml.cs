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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ChatStream.ServiceReference1;

namespace ChatStream
{
    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Service1Client srv = new Service1Client();
        private User user;
        

        public User User { get => user; set => user = value; }
        /// <summary>
        /// initialize main window
        /// </summary>
        /// <param name="user">user that has been logined</param>
        public MainWindow(User user)
        {
            InitializeComponent();
            this.ChangeContent.Content = new OpenMenu();
            this.user = user;
            HelloT.Text += "Hello " + user.Uname;
        }
        
        //Exit MainWindow, Log out, moves to login window
        private void OutB(object sender, RoutedEventArgs e)
        {
            (new Login()).Show();
            this.Close();
        }

        // opens profile's data with edit options
        private void ProfileB(object sender, RoutedEventArgs e)
        {
            
            this.ChangeContent.Content = new Profile(this.user);
        }
        
        //opens this main user's personal feed
        private void FeedB(object sender, RoutedEventArgs e)
        {
            this.ChangeContent.Content = new Feed(this);
        }

        //opens friend list with option to add friends
        private void FriendB_click(object sender, RoutedEventArgs e)
        {
            this.ChangeContent.Content = new FriendListShow(this);
        }

        //opens chats menu
        private void Chat_Click(object sender, RoutedEventArgs e)
        {
            this.ChangeContent.Content = new ChatsShow(this);
        }
        //gets current UserControle Dispalyed
        public UserControl GetCurrentControle()
        {
            UserControl control = null;
            try
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    if (ChangeContent.Content is UserControl userControl)
                    {
                        control = userControl;
                        // Do something with 'control'
                    }
                });
            }
            catch (Exception)
            {
                return null;
            }
            
            
            return control;
        }
    }
}
