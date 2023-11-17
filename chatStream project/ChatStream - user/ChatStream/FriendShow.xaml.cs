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
    /// Interaction logic for FriendShow.xaml
    /// </summary>
    public partial class FriendShow : UserControl
    {
        Service1Client srv = new Service1Client();
        private User mainU;
        private MainWindow main;
        private User user;
        private FriendListShow friendListShow;
        public FriendShow(MainWindow main, User user, FriendListShow friendListShow)
        {
            this.mainU = main.User;
            this.user = user;
            this.main = main;
            this.friendListShow = friendListShow;
            Friend friend = srv.SelectFriend(this.mainU, this.user);
            InitializeComponent();
            UName_B.Content = user.Uname;
            FullName.Text = user.Fname + " " + user.Lname;
            if (friend == null)
            {
                FriendB.Visibility = Visibility.Visible;
            }
            else
            {
                if (friend.Approved)
                {
                    UnFriendB.Visibility = Visibility.Visible;
                }
                else
                {
                    DeclineF_B.Visibility = Visibility.Visible;
                    if (friend.User2.ID == this.mainU.ID)
                        AcceptF_B.Visibility = Visibility.Visible;
                }
            }
        }


        private void FriendB_Click(object sender, RoutedEventArgs e)
        {

            FriendB.Visibility = Visibility.Collapsed;
            DeclineF_B.Visibility = Visibility.Visible;
            Friend friend = new Friend();
            friend.User1 = this.mainU;
            friend.User2 = this.user;

            srv.InsertFriend(friend);
        }

        private void UnFriendB_Click(object sender, RoutedEventArgs e)
        {
            UnFriendB.Visibility = Visibility.Collapsed;
            DeclineF_B.Visibility = Visibility.Collapsed;
            AcceptF_B.Visibility = Visibility.Collapsed;
            FriendB.Visibility = Visibility.Visible;
            Friend friend = new Friend();
            friend.User1 = this.mainU;
            friend.User2 = this.user;

            srv.DeleteFriend(friend);
        }

        private void AcceptF_B_Click(object sender, RoutedEventArgs e)
        {
            UnFriendB.Visibility = Visibility.Visible;
            DeclineF_B.Visibility = Visibility.Collapsed;
            AcceptF_B.Visibility = Visibility.Collapsed;
            FriendB.Visibility = Visibility.Collapsed;
            Friend friend = new Friend();
            friend.User1 = this.mainU;
            friend.User2 = this.user;
            srv.ApproveFriend(friend);
        }
        private void UFeed(object sender, RoutedEventArgs e)
        {
            this.main.ChangeContent.Content = new UserFeed(this.user, this.main, this.friendListShow);
        }
    }
}
