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
    /// Interaction logic for FriendListShow.xaml
    /// </summary>
    public partial class FriendListShow : UserControl
    {
        Service1Client srv = new Service1Client();

        private MainWindow main;
        public FriendListShow(MainWindow main)
        {
            this.main = main;
            InitializeComponent();
            List<Friend> friendList = srv.SelectUserFriends(this.main.User).ToList();
            RefreshListResults(MyFriendsView, friendList);
            List<Friend> reqList = srv.SelectRecivedReq(this.main.User).ToList();
            RefreshListResults(PendReqView, reqList);
        }

        private void USerach_TB_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            FriendsSearchView.Children.Clear();
            if (textBox.Text.Length > 0)
            {
                List<User> userlist = srv.SelectUserByUName(textBox.Text).ToList();
                foreach (User user in userlist)
                {
                    FriendShow friendShow = new FriendShow(this.main, user, this);
                    FriendsSearchView.Children.Add(friendShow);
                }

            }
        }

        private void RefreshListResults(StackPanel panel, List<Friend> list)
        {
            panel.Children.Clear();
            foreach (Friend friend in list)
            {
                if (friend.User1.ID != this.main.User.ID)
                {
                    FriendShow friendShow = new FriendShow(this.main, friend.User1, this);
                    panel.Children.Add(friendShow);
                }

                if (friend.User2.ID != this.main.User.ID)
                {
                    FriendShow friendShow = new FriendShow(this.main, friend.User2, this);
                    panel.Children.Add(friendShow);
                }
            }
        }
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            USerach_TB.Text = "";
        }
    }
}
