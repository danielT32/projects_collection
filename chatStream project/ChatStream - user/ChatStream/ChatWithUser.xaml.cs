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
    /// Interaction logic for ChatWithUser.xaml
    /// </summary>
    public partial class ChatWithUser : UserControl
    {
        private MainWindow main;
        private User user;
        private UserControl sourseCont;
        Service1Client srv = new Service1Client();
        private bool isAutoUpdate = true; 
        private Thread autoUpdate; 
        public ChatWithUser(MainWindow main, User user, UserControl sourseCont)
        {
            this.main = main;
            this.sourseCont = sourseCont;
            this.user = user;
            this.isAutoUpdate = true;
            InitializeComponent();
            UserNameChat.Text = user.Uname + "#id:" + user.ID;

            this.autoUpdate = new Thread(this.AutoRefresh);//this thread will update messages every 2 seconds
            List<DMessage> messages = srv.SelectAllDMsChat(this.main.User, this.user).ToList();
            foreach (DMessage dMessage in messages)
            {
                AppendMessage(dMessage);
            }

            this.autoUpdate.Start();//start update messages every 2 seconds
            this.autoUpdate.IsBackground = true;
        }
        //appends certain message to the visible chat
        private void AppendMessage(DMessage message)
        {
            Application.Current.Dispatcher.Invoke(() => ///this made to allow using this function from a thread
            {
                var horizontalAl = HorizontalAlignment.Left;
                var messageColour = new SolidColorBrush(Color.FromRgb(0x00, 0x5d, 0x6e));
                if (message.DestUser.ID == this.user.ID)
                {
                    horizontalAl = HorizontalAlignment.Right;
                    messageColour = new SolidColorBrush(Color.FromRgb(0x4e, 0xad, 0x51));
                }
                WrapPanel wrapPanel = new WrapPanel()
                {
                    Width = double.NaN,
                    HorizontalAlignment = horizontalAl
                };

                Border border = new Border()// message's circular border
                {
                    Margin = new Thickness(5),
                    Width = double.NaN,
                    Padding = new Thickness(5),
                    BorderThickness = new Thickness(1),
                    BorderBrush = Brushes.Black,
                    Background = messageColour,
                    CornerRadius = new CornerRadius(10)
                };
                WrapPanel wrapMessage = new WrapPanel()
                {
                    Width = double.NaN,
                    HorizontalAlignment = horizontalAl
                };
                TextBlock contentTextBlock = new TextBlock()//message content
                {
                    Foreground = Brushes.White,
                    Width = double.NaN,
                    TextWrapping = TextWrapping.Wrap,
                    Text = message.Content,
                    Padding = new Thickness(5, 0, 5, 0)
                };
                TextBlock dateBlock = new TextBlock() //message time sent
                {
                    Foreground = Brushes.White,
                    Width = double.NaN,
                    TextWrapping = TextWrapping.Wrap,
                    Text = message.Time.ToString(),
                    Padding = new Thickness(5, 0, 5, 0)
                };
                dateBlock.FontSize = 10;
                contentTextBlock.FontSize = 20;
                wrapMessage.Children.Add(contentTextBlock);
                wrapMessage.Children.Add(dateBlock);
                border.Child = wrapMessage;
                wrapPanel.Children.Add(border);
                ChatMessages.Children.Add(wrapPanel);
                ChatScroller.ScrollToBottom();
            });
            


        }
        private void AutoRefresh() //refreash all messages every 2 seconds.
        {
            while( isAutoUpdate)
            {
                Thread.Sleep(2000);
                List<DMessage> messages = srv.SelectUnSeenDM(this.main.User, this.user).ToList();
                foreach(DMessage dMessage in messages)
                {
                    AppendMessage(dMessage);
                }
            }
        }
        //sends a message to the second user
        private void SendMessage_Click(object sender, RoutedEventArgs e)
        {
            if(textedMessage.Text != "")
            {
                DMessage message = new DMessage();
                message.Content = textedMessage.Text;
                message.SourceUser = this.main.User;
                message.DestUser = this.user;
                message.Time = DateTime.Now;
                srv.InsertDM(message);
                textedMessage.Text = "";
                AppendMessage(message);
            }
            

        }
        //returns to the previos UserControl shown by mainwindow
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.main.ChangeContent.Content = this.sourseCont;
            if (this.sourseCont is ChatsShow)
            {
                (this.sourseCont as ChatsShow).StartRefreash();
            }
            isAutoUpdate = false;
        }
    }
}
