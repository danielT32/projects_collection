using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for Feed.xaml
    /// </summary>
    public partial class Feed : UserControl
    {
        private MainWindow main;
        Service1Client srv = new Service1Client();
        public Feed(MainWindow main)
        {
            this.main = main;
            InitializeComponent();
            //create a UserControl of CreatePost, where user can createpost
            CreatePostV.Content = new CreatePost(main.User, AddPost);
            ((CreatePost)CreatePostV.Content).Visibility = Visibility.Collapsed;
            //receive all the posts that user should see in the feed
            List<Post> posts = srv.SelectMyFeed(this.main.User).ToList();
            
            foreach (Post post in posts) //show each post
            {
                UserControl userControl = new UserControl();
                userControl.Content = new PostView(post, main, this);
                userControl.Margin = new Thickness(10);
                FeedView.Children.Add(userControl);
            }
        }
        /// <summary>
        /// the method runs when an event of changing the text box content occurs
        /// the method shows all the users that start with what was typed in text box
        /// </summary>
        /// <param name="sender">textbox</param>
        /// <param name="e"></param>
        private void USerach_TB_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            Search_Results.Children.Clear();
            if (textBox.Text.Length > 0) //if user searched uname
            {
                //select all the usernames starts with what was typed in the search bar
                List<User> userlist = srv.SelectUserByUName(textBox.Text).ToList();
                foreach (User user in userlist)
                {
                    WrapPanel wrapPanel = new WrapPanel();
                    EntityButton uButton = new EntityButton();
                    TextBlock name = new TextBlock();
                    //buttom with username that shows user's personal feed and info when clicked
                    uButton.Style = FindResource("SR_Buttons") as Style;
                    uButton.Content = user.Uname;
                    uButton.Entity = user;
                    uButton.Click += UFeed;
                    //Text Block that presents the full name of the user
                    name.Style = FindResource("textS") as Style;
                    name.Foreground = Brushes.Black;
                    name.Text = user.Fname + " " + user.Lname;
                    name.FontSize = 20;
                    wrapPanel.Children.Add(uButton as Button);
                    wrapPanel.Children.Add(name);
                    Search_Results.Children.Add(wrapPanel);// add the search results to the stackpanel
                }
            }
        }
        /// <summary>
        /// opens user's feed by clicking the button named by username.
        /// </summary>
        /// <param name="sender">button</param>
        /// <param name="e"></param>
        private void UFeed(object sender, RoutedEventArgs e)
        {
            EntityButton userButton = sender as EntityButton;
            this.main.ChangeContent.Content = new UserFeed((User)userButton.Entity, this.main, this);
        }
        
        /// <summary>
        /// this button shows the add post UserControle in order to allow the user 
        /// to add a post.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddPost_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            button.Visibility = Visibility.Collapsed;
            ((CreatePost)CreatePostV.Content).Visibility = Visibility.Visible;
        }
        /// <summary>
        /// the clear Search button clears the content searched in the textbox 
        /// </summary>
        /// <param name="sender">button</param>
        /// <param name="e"></param>
        private void ClearSearch_Click(object sender, RoutedEventArgs e)
        {
            USerach_TB.Text = "";
        }
        /// <summary>
        /// refreshes all the posts in the feed
        /// </summary>
        /// <param name="sender">refresh button </param>
        /// <param name="e"></param>
        public void Refresh_Click(object sender, RoutedEventArgs e)
        {
            FeedView.Children.Clear();
            List<Post> posts = srv.SelectMyFeed(this.main.User).ToList();
            foreach (Post post in posts)
            {
                UserControl userControl = new UserControl();
                userControl.Content = new PostView(post, main, this);
                userControl.Margin = new Thickness(10);
                FeedView.Children.Add(userControl);
            }
        }
    }
    /*public class UserButton : Button
    {
        private User user;
        public UserButton() : base() { this.user = null; }
        public UserButton(User user) : base() { this.user = user; }

        public User User { get => user; set => user = value; }
    }*/
    public class EntityButton : Button
    {
        private BaseEntity entity;
        public EntityButton() : base() { this.entity = null; }
        public EntityButton(BaseEntity entity) : base() { this.entity = entity; }

        public BaseEntity Entity { get => entity; set => entity = value; }
    }
}
