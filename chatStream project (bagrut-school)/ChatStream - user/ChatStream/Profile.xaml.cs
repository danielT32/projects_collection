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
    /// Interaction logic for Profile.xaml
    /// </summary>
    public partial class Profile : UserControl
    {
        Service1Client srv = new Service1Client();
        protected User user;
        public Profile(User user)
        {
            InitializeComponent();
            this.user = user;
            UName.Text = user.Uname;
            Lname.Text = user.Lname;
            FName.Text = user.Fname;
            Description.Text = user.Description;
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Save.Visibility = Visibility.Visible;
            Edit.Visibility = Visibility.Collapsed;
            UName.IsReadOnly = false;
            FName.IsReadOnly = false;
            Lname.IsReadOnly = false;
            Description.IsReadOnly = false;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Save.Visibility = Visibility.Collapsed;
            Edit.Visibility = Visibility.Visible;
            user.Uname = UName.Text;
            user.Lname = Lname.Text;
            user.Fname = FName.Text;
            user.Description = Description.Text;
            srv.UpdateUser(this.user);
            UName.IsReadOnly = true;
            FName.IsReadOnly = true;
            Lname.IsReadOnly = true;
            Description.IsReadOnly = true;
        }
    }
}
