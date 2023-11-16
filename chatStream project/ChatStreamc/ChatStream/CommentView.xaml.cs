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
    /// Interaction logic for CommentView.xaml
    /// </summary>
    public partial class CommentView : UserControl
    {
        Service1Client srv = new Service1Client();
        private Comment comment;
        private UserControl currentC;
        private MainWindow main;
        public CommentView(Comment comment, MainWindow main , UserControl currentC)
        {
            this.comment = comment;
            this.main = main;
            this.currentC = currentC;
            InitializeComponent();
            if(main.User.ID == comment.User.ID)
            {
                Edit_B.Visibility = Visibility.Visible;
            }
            CUName.Content = comment.User.Uname;
            Content.Text = comment.Content;


        }

        private void Edit_B_Click(object sender, RoutedEventArgs e)
        {
            Edit_B.Visibility = Visibility.Collapsed;
            Save_B.Visibility = Visibility.Visible;
            Content.IsReadOnly = false;
        }

        private void Save_B_Click(object sender, RoutedEventArgs e)
        {
            Edit_B.Visibility = Visibility.Visible;
            Save_B.Visibility = Visibility.Collapsed;
            Content.IsReadOnly = true;
            this.comment.Content = Content.Text;
            srv.UpdateComment(this.comment);
        }

        private void UFeed(object sender, RoutedEventArgs e)
        {
            this.main.ChangeContent.Content = new UserFeed(this.comment.User, this.main, this.currentC);
        }
    }
}
