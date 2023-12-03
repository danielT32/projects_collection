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
    /// Interaction logic for PostView.xaml
    /// </summary>
    public partial class PostView : UserControl
    {
        Service1Client srv = new Service1Client();
        protected MainWindow main;
        protected Post post;
        private UserControl current;
        
        public PostView(Post post, MainWindow main, UserControl current) : this(post, main)
        {
            this.current = current;
        }
        public PostView(Post post, MainWindow main)
        {
            this.current = null;
            InitializeComponent();
            int likeCounter = srv.CountLikes(post);
            LCounter.Text = likeCounter.ToString();
            this.main = main;
            this.post = post;
            Like likedPost = new Like();
            likedPost.Post = this.post;
            likedPost.User = this.main.User;
            Follow followedUser = new Follow();
            followedUser.Follower = main.User;
            followedUser.Following = post.User;
            if (srv.IsLiked(likedPost))
            {
                UnLike_Button.Visibility = Visibility.Visible;
                Like_Button.Visibility = Visibility.Collapsed;
            }
            else
            {
                UnLike_Button.Visibility = Visibility.Collapsed;
                Like_Button.Visibility = Visibility.Visible;
            }
            if (this.main.User.ID == this.post.User.ID)
            {
                Edit.Visibility = Visibility.Visible;
                publicity.Visibility = Visibility.Visible;
                FollowB.Visibility = Visibility.Collapsed;
                UnFollowB.Visibility = Visibility.Collapsed;
            }
            else if(srv.IsFollowing(followedUser))
            {
                FollowB.Visibility = Visibility.Collapsed;
                UnFollowB.Visibility = Visibility.Visible;
            }
            else
            {
                UnFollowB.Visibility = Visibility.Collapsed;
                FollowB.Visibility = Visibility.Visible;
            }
            Content.Text = post.Content;
            PUName.Content = this.post.User.Uname;
            if (post.IsPrivate)
            {
                //publicity.SelectedIndex = 2;
                publicity.SelectedItem = publicity.Items[1];
            }
                
            else
            {
                //publicity.SelectedItem = 1;
                publicity.SelectedItem = publicity.Items[0];
            }
        }

        private void UFeed(object sender, RoutedEventArgs e)
        {
            this.main.ChangeContent.Content = new UserFeed(this.post.User, this.main, this.current);
        }

        private void FollowP(object sender, RoutedEventArgs e)
        {
            FollowB.Visibility = Visibility.Collapsed;
            UnFollowB.Visibility = Visibility.Visible;
            Follow follow = new Follow();
            follow.Follower = main.User;
            follow.Following = post.User;
            srv.InsertFollow(follow);
        }
        private void UnFollowP(object sender, RoutedEventArgs e)
        {
            UnFollowB.Visibility = Visibility.Collapsed;
            FollowB.Visibility = Visibility.Visible;
            Follow follow = new Follow();
            follow.Follower = main.User;
            follow.Following = post.User;
            srv.DeleteFollow(follow);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if(comboBox.SelectedIndex == 1 && (!this.post.IsPrivate))
            {
                this.post.IsPrivate = (!this.post.IsPrivate);
                srv.UpdatePost(post);
            }
            if (comboBox.SelectedIndex == 0 && this.post.IsPrivate)
            {
                this.post.IsPrivate = (!this.post.IsPrivate);
                srv.UpdatePost(post);
            }
        }

        private void EditContent(object sender, RoutedEventArgs e)
        {
            Content.IsReadOnly = false;
            Edit.Visibility = Visibility.Collapsed;
            Save.Visibility = Visibility.Visible;
            
        }

        private void SaveContent(object sender, RoutedEventArgs e)
        {
            Content.IsReadOnly = true;
            Edit.Visibility = Visibility.Visible;
            Save.Visibility = Visibility.Collapsed;
            post.Content = Content.Text;
            srv.UpdatePost(post);
        }

        private void Like_Click(object sender, RoutedEventArgs e)
        {
            Like like = new Like();
            like.Post = this.post;
            like.User = this.main.User;
            this.srv.InsertLike(like);
            LCounter.Text = this.srv.CountLikes(this.post).ToString();
            UnLike_Button.Visibility = Visibility.Visible;
            Like_Button.Visibility = Visibility.Collapsed;
        }
        private void UnLike_Click(object sender, RoutedEventArgs e)
        {
            Like like = new Like();
            like.Post = this.post;
            like.User = this.main.User;
            srv.DeleteLike(like);
            LCounter.Text = srv.CountLikes(this.post).ToString();
            UnLike_Button.Visibility = Visibility.Collapsed;
            Like_Button.Visibility = Visibility.Visible;
        }

        private void View_Comments_B(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if(CViewer.Visibility == Visibility.Collapsed)
            {
                CViewer.Visibility = Visibility.Visible;
                List<Comment> comments = srv.SelectCommentByPost(this.post).ToList();
                foreach (Comment comment in comments)
                {
                    UserControl userControl = new UserControl();
                    userControl.Content = new CommentView(comment, main, this.current);
                    userControl.Margin = new Thickness(5);
                    Comments_List_View.Children.Add(userControl);
                }
                button.Content = "^^stop comments view^^";
            }
            else
            {
                button.Content = "View Comments";
                Comments_List_View.Children.Clear();
                CViewer.Visibility = Visibility.Collapsed;
            }
        }

        private void SendComment_Click(object sender, RoutedEventArgs e)
        {
            if(My_Comment_Content.Text.Length >= 1)
            {
                Comment comment = new Comment();
                comment.User = this.main.User;
                comment.Post = this.post;
                comment.Content = My_Comment_Content.Text;
                srv.InsertComment(comment);
                My_Comment_Content.Text = "";
            }   
        }
    }
}
