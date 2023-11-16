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
    /// Interaction logic for UserFeed.xaml
    /// </summary>
    public partial class UserFeed : UserControl
    {
        Service1Client srv = new Service1Client();
        protected User user;
        protected MainWindow main;
        private UserControl previous;

        public UserControl Previous { get => previous; set => previous = value; }
        public UserFeed(User user, MainWindow main, UserControl previous) : this(user, main) 
        {
            this.previous = previous;
        }
        public UserFeed(User user, MainWindow main)
        {
            this.previous = null;
            InitializeComponent();
            this.user = user;
            this.main = main;
            UName.Text += user.Uname;
            FullName.Text += user.Fname + " " + user.Lname;
            Description.Text += user.Description;
            List<Post> posts = srv.SelectUserFeed(this.main.User, this.user).ToList();
            Follow follow = new Follow();
            follow.Follower = this.main.User;
            follow.Following = this.user;
            if(srv.IsFollowing(follow))
            {
                this.FollowB.Visibility = Visibility.Collapsed;
                this.UnFollowB.Visibility = Visibility.Visible;
            }
            else
            {
                this.FollowB.Visibility = Visibility.Visible;
                this.UnFollowB.Visibility = Visibility.Collapsed;
                
            }
            if(user.ID == main.User.ID)
            {
                AddPost.Visibility = Visibility.Visible;
                CreatePostV.Content = new CreatePost(this.user, AddPost);
                ((CreatePost)CreatePostV.Content).Visibility = Visibility.Collapsed;

            }
            foreach (Post post in posts)
            {
                UserControl userControl = new UserControl();
                userControl.Content = new PostView(post, main, this);
                userControl.Margin = new Thickness(10);
                FeedView.Children.Add(userControl);
            }
            Friend friend = srv.SelectFriend(this.main.User, this.user);
            if(friend == null)
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
                    if(friend.User2.ID == this.main.User.ID)
                        AcceptF_B.Visibility = Visibility.Visible;
                }
            }
        }

        private void FollowB_Click(object sender, RoutedEventArgs e)
        {
            FollowB.Visibility = Visibility.Collapsed;
            UnFollowB.Visibility = Visibility.Visible;
            Follow follow = new Follow();
            follow.Follower = this.main.User;
            follow.Following = this.user;
            srv.InsertFollow(follow);
        }

        private void UnFollowB_Click(object sender, RoutedEventArgs e)
        {
            UnFollowB.Visibility = Visibility.Collapsed;
            FollowB.Visibility = Visibility.Visible;
            Follow follow = new Follow();
            follow.Follower = this.main.User;
            follow.Following = this.user;
            srv.DeleteFollow(follow);
        }

        public void Refresh_Click(object sender, RoutedEventArgs e)
        {
            FeedView.Children.Clear();
            List<Post> posts = srv.SelectUserFeed(this.main.User, this.user).ToList();
            foreach (Post post in posts)
            {
                UserControl userControl = new UserControl();
                userControl.Content = new PostView(post, main, this);
                userControl.Margin = new Thickness(10);
                FeedView.Children.Add(userControl);
            }
        }
        private void AddPost_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            button.Visibility = Visibility.Collapsed;
            ((CreatePost)CreatePostV.Content).Visibility = Visibility.Visible;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if(this.previous != null)
            {
                
                if (this.previous is UserFeed)
                {
                    UserFeed feed = this.previous as UserFeed;
                    feed.Refresh_Click(sender, e);
                }
                else if (this.previous is Feed)
                {
                    Feed feed = this.previous as Feed;
                    feed.Refresh_Click(sender, e);
                }
                else if (this.previous is ChatsShow)
                {
                    (this.previous as ChatsShow).StartRefreash();
                }
                this.main.ChangeContent.Content = this.previous;
            }
                

        }

        private void FriendB_Click(object sender, RoutedEventArgs e)
        {
            Friend friend = new Friend();
            friend.User1 = this.main.User;
            friend.User2 = this.user;
            FriendB.Visibility = Visibility.Collapsed;
            DeclineF_B.Visibility = Visibility.Visible;
            srv.InsertFriend(friend);
        }

        private void UnFriendB_Click(object sender, RoutedEventArgs e)
        {
            Friend friend = new Friend();
            friend.User1 = this.main.User;
            friend.User2 = this.user;
            UnFriendB.Visibility = Visibility.Collapsed;
            DeclineF_B.Visibility = Visibility.Collapsed;
            AcceptF_B.Visibility = Visibility.Collapsed;
            FriendB.Visibility = Visibility.Visible;
            srv.DeleteFriend(friend);
        }

        private void AcceptF_B_Click(object sender, RoutedEventArgs e)
        {
            Friend friend = new Friend();
            friend.User1 = this.main.User;
            friend.User2 = this.user;
            UnFriendB.Visibility = Visibility.Visible;
            DeclineF_B.Visibility = Visibility.Collapsed;
            AcceptF_B.Visibility = Visibility.Collapsed;
            FriendB.Visibility = Visibility.Collapsed;
            srv.ApproveFriend(friend);
        }
    }
}
