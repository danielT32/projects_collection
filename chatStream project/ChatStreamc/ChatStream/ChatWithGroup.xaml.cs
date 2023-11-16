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
using System.Threading;
using ChatStream.ServiceReference1;

namespace ChatStream
{
    /// <summary>
    /// Interaction logic for ChatWithGroup.xaml
    /// </summary>
    public partial class ChatWithGroup : UserControl
    {
        private MainWindow main;
        private Group group;
        private UserControl sourseCont;
        Service1Client srv = new Service1Client();
        private bool isAutoUpdate;
        private Thread autoUpdate;
        public ChatWithGroup(MainWindow main, Group group, UserControl sourseCont)
        {

            this.main = main;
            this.sourseCont = sourseCont;
            this.group = group;
            this.isAutoUpdate = true;
            InitializeComponent();
            GroupNameText.Text = group.GNname;
            this.autoUpdate = new Thread(this.AutoRefresh);
            this.autoUpdate.IsBackground = true;
            List<GMessage> messages = srv.GetGMessages(this.main.User, this.group).ToList();
            foreach (GMessage gMessage in messages)
            {
                AppendMessage(gMessage);
            }
            this.autoUpdate.Start();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.main.ChangeContent.Content = this.sourseCont;
            if(this.sourseCont is ChatsShow)
            {
                (this.sourseCont as ChatsShow).StartRefreash();
            }
            this.isAutoUpdate = false;
        }

        private void AppendMessage(GMessage message)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                var horizontalAl = HorizontalAlignment.Left;
                var messageColour = new SolidColorBrush(Color.FromRgb(0x00, 0x5d, 0x6e));
                if (message.User.ID != this.main.User.ID)
                {
                    horizontalAl = HorizontalAlignment.Right;
                    messageColour = new SolidColorBrush(Color.FromRgb(0x4e, 0xad, 0x51));
                }
                WrapPanel wrapPanel = new WrapPanel()
                {
                    Width = double.NaN,
                    HorizontalAlignment = horizontalAl
                };

                Border border = new Border()
                {
                    Margin = new Thickness(5),
                    Width = double.NaN,
                    Padding = new Thickness(5),
                    BorderThickness = new Thickness(1),
                    BorderBrush = Brushes.Black,
                    Background = messageColour,
                    CornerRadius = new CornerRadius(10)
                };
                StackPanel stackMessage = new StackPanel()
                {
                    Width = double.NaN,
                    HorizontalAlignment = horizontalAl
                };
                TextBlock contentTextBlock = new TextBlock()
                {
                    Foreground = Brushes.White,
                    Width = double.NaN,
                    TextWrapping = TextWrapping.Wrap,
                    Text = message.Content,
                    Padding = new Thickness(5, 0, 5, 0)
                };
                TextBlock nameBlock = new TextBlock()
                {
                    Foreground = Brushes.White,
                    Width = double.NaN,
                    TextWrapping = TextWrapping.Wrap,
                    Text = message.User.Uname,
                    Padding = new Thickness(5, 0, 5, 0)
                };
                nameBlock.FontSize = 10;
                contentTextBlock.FontSize = 20;
                stackMessage.Children.Add(nameBlock);
                stackMessage.Children.Add(contentTextBlock);
                
                border.Child = stackMessage;
                wrapPanel.Children.Add(border);
                ChatMessages.Children.Add(wrapPanel);
                ChatScroller.ScrollToBottom();
            });



        }
        private void AutoRefresh()
        {
            while (this.isAutoUpdate)
            {
                Thread.Sleep(2000);
                List<GMessage> messages = srv.GetUnSeenGMessages(this.main.User, this.group).ToList();
                foreach (GMessage gMessage in messages)
                {
                    AppendMessage(gMessage);
                }
            }
        }
        private void SendMessage_Click(object sender, RoutedEventArgs e)
        {
            if (textedMessage.Text != "")
            {
                GMessage message = new GMessage();
                message.Content = textedMessage.Text;
                message.User = this.main.User;
                message.Group = this.group;
                //message.Date = DateTime.Now;
                srv.InsertGMessage(message);
                textedMessage.Text = "";
            }

        }
    }
}
