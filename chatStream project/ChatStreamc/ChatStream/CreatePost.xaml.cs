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
    /// Interaction logic for CreatePost.xaml
    /// </summary>
    public partial class CreatePost : UserControl
    {
        private User pUser;
        private Button sButton;
        Service1Client srv = new Service1Client();
        public CreatePost(User user, Button button)
        {
            InitializeComponent();
            publicity.SelectedIndex = 0;
            sButton = button;
            pUser = user;
        }


        private void CreateContent(object sender, RoutedEventArgs e)
        {
            Post post = new Post();
            post.Content = Content.Text;
            post.IsPrivate = publicity.SelectedIndex == 1;
            post.User = pUser;
            srv.InsertPost(post);
            Content.Text = "";
            sButton.Visibility = Visibility.Visible;
            this.Visibility = Visibility.Collapsed;
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            sButton.Visibility = Visibility.Visible;
            this.Visibility = Visibility.Collapsed;
        }


    }
}
