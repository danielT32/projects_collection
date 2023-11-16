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
    /// Interaction logic for CreateGroup.xaml
    /// </summary>
    public partial class CreateGroup : UserControl
    {
        MainWindow main;
        UserControl previous;
        Service1Client srv = new Service1Client();
        public CreateGroup(MainWindow main, UserControl previous)
        {
            InitializeComponent();
            this.main = main;
            this.previous = previous;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Group group = new Group();
            if(GName.Text.Length > 3)
            {
                group.GNname = GName.Text;
                group.Publicity = Public.IsChecked.GetValueOrDefault();
                group.Description = Description.Text + " ";
                srv.InsertGroup(group, this.main.User);
                this.main.ChangeContent.Content = this.previous;
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.main.ChangeContent.Content = this.previous;
        }
    }
}
