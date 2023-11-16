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
using System.Threading;
namespace ChatStream
{
    /// <summary>
    /// Interaction logic for ChatsShow.xaml
    /// </summary>
    /// public class UserButton : Button
public partial class ChatsShow : UserControl
    {
        Service1Client srv = new Service1Client();
        private MainWindow main;
        private int updateState = 0;
        private Thread refreshThread;
        private bool typeDM = false;
        private bool isUserControlPresent = true;
        public ChatsShow(MainWindow main)
        {
            this.main = main;
            isUserControlPresent = true;
            InitializeComponent();
            StartRefreash();
        }
        private void UFeed(object sender, RoutedEventArgs e)
        {
            EntityButton userButton = sender as EntityButton;
            this.main.ChangeContent.Content = new UserFeed((User)userButton.Entity, this.main, this);
            isUserControlPresent = false;
        }
        private void GShow(Object sender, RoutedEventArgs e)
        {
            EntityButton memberBtn = sender as EntityButton;
            this.main.ChangeContent.Content = new GroupInfo(main,(GMember)memberBtn.Entity, this);
            isUserControlPresent = false;
        }
        public void StartRefreash()
        {
            refreshThread = new Thread(this.RefreshAll);
            refreshThread.IsBackground = true;
            refreshThread.Start();
        }
        private void ChatUBtn(object sender, RoutedEventArgs e)/////////////////////
        {
            EntityButton userButton = sender as EntityButton;
            this.main.ChangeContent.Content = new ChatWithUser(this.main, (User)userButton.Entity, this);
            isUserControlPresent = false;
        }
        private void ChatGBtn(object sender, RoutedEventArgs e)/////////////////////
        {
            EntityButton groupButton = sender as EntityButton;
            this.main.ChangeContent.Content = new ChatWithGroup(this.main, (Group)groupButton.Entity, this);
            isUserControlPresent = false;
        }
        private void generateUserResults(StackPanel stack, List<User> userlist)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                stack.Children.Clear();
                foreach (User user in userlist)
                {
                    WrapPanel wrapPanel = new WrapPanel();
                    EntityButton uButton = new EntityButton();
                    EntityButton chatButton = new EntityButton();
                    uButton.Style = FindResource("SR_Buttons") as Style;
                    uButton.Content = user.Uname;
                    uButton.Entity = user;
                    uButton.Click += UFeed;

                    chatButton.Style = FindResource("SR_Buttons") as Style;
                    chatButton.Background = Brushes.Lavender;
                    chatButton.Content = "Chat";
                    chatButton.Entity = user;
                    chatButton.Click += ChatUBtn;
                    wrapPanel.Children.Add(uButton as Button);
                    wrapPanel.Children.Add(chatButton as Button);
                    stack.Children.Add(wrapPanel);
                }
            });
        }
        private void generateGroupResults(StackPanel stack, List<Group> grouplist)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                stack.Children.Clear();
                foreach (Group group in grouplist)
                {
                    WrapPanel wrapPanel = new WrapPanel();
                    GMember gMember = new GMember();
                    gMember.User = this.main.User;
                    gMember.Group = group;
                    if (srv.IsGroupMember(gMember))
                    {
                        gMember.Admin = srv.IsGroupAdmin(gMember);
                        EntityButton gButton = new EntityButton();
                        EntityButton chatButton = new EntityButton();
                        gButton.Style = FindResource("SR_Buttons") as Style;
                        gButton.Content = group.GNname + "#" + group.ID;
                        gButton.Entity = gMember;
                        gButton.Click += GShow;

                        chatButton.Style = FindResource("SR_Buttons") as Style;
                        chatButton.Background = Brushes.Lavender;
                        chatButton.Content = "Chat";
                        chatButton.Entity = group;
                        chatButton.Click += ChatGBtn;
                        wrapPanel.Children.Add(gButton as Button);
                        wrapPanel.Children.Add(chatButton as Button);
                        
                    }
                    else
                    {
                        TextBlock gName = new TextBlock();
                        gName.Style = FindResource("textS") as Style;
                        gName.Foreground = Brushes.Black;
                        gName.Text = group.GNname + "#" + group.ID;
                        gName.FontSize = 30;

                        EntityButton joinButton = new EntityButton();
                        joinButton.Style = FindResource("SR_Buttons") as Style;
                        joinButton.Content = "Join";
                        joinButton.Entity = gMember;
                        joinButton.Click += JoinGroup;
                        wrapPanel.Children.Add(gName);
                        wrapPanel.Children.Add(joinButton as Button);
                    }
                    stack.Children.Add(wrapPanel);
                }
            });
        }
        private void USerach_TB_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool isDirect = (bool)DirectRB.IsChecked;
            Search_Results.Children.Clear();
            if (USerach_TB.Text.Length > 0)
            {
                if(isDirect)
                {
                    List<User> userlist = srv.SelectUserByUName(USerach_TB.Text).ToList();
                    generateUserResults(Search_Results, userlist);
                }
                else
                {
                    List<Group> grouplist = srv.SearchPublicGroups(USerach_TB.Text).ToList();
                    generateGroupResults(Search_Results, grouplist);
                }
                
                
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            this.typeDM = false;
        }

        private void DirectRB_Checked(object sender, RoutedEventArgs e)
        {
            this.typeDM = true; 
        }

        private void RefreshAll()
        {
            int currentUpdateState;
            bool typeDMPrevious = this.typeDM;
            Thread.Sleep(2000);
            isUserControlPresent = true;
            while (isUserControlPresent)
            {
                if (this.typeDM)
                {
                    Application.Current.Dispatcher.Invoke(() => { 
                        DSerchTextShow.Visibility = Visibility.Visible;
                    GSerchTextShow.Visibility = Visibility.Collapsed;
                    CreateG.Visibility = Visibility.Collapsed;
                });

                    currentUpdateState = srv.GetChatsDMUpdateState(this.main.User);
                    if (this.updateState != currentUpdateState && typeDMPrevious == this.typeDM)
                    {
                        this.updateState = currentUpdateState;
                        List<User> chatsUsers = srv.GetAllDMChats(main.User).ToList();
                        generateUserResults(MyChatsPanel, chatsUsers);
                    }
                }
                else
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        DSerchTextShow.Visibility = Visibility.Collapsed;
                        GSerchTextShow.Visibility = Visibility.Visible;
                        CreateG.Visibility = Visibility.Visible;
                    });
                    currentUpdateState = srv.GetMyGroupsUpdateState(this.main.User);
                    if (this.updateState != currentUpdateState && typeDMPrevious == this.typeDM)
                    {
                        this.updateState = currentUpdateState;
                        List<Group> chatsUsers = srv.GetMyGroups(main.User).ToList();
                        generateGroupResults(MyChatsPanel, chatsUsers);
                    }

                }
                typeDMPrevious = this.typeDM;

                Thread.Sleep(2000);
            }
        }             
            
        private void JoinGroup(object sender, RoutedEventArgs e)
        {
            EntityButton memberBtn = sender as EntityButton;
            GMember gMember = (GMember)memberBtn.Entity;
            gMember = srv.InsertGMemeber(gMember.Group, this.main.User);
            this.main.ChangeContent.Content = new GroupInfo(main, gMember, this);

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            USerach_TB.Text = "";
        }

        private void CreateG_Click(object sender, RoutedEventArgs e)
        {
            this.main.ChangeContent.Content = new CreateGroup(this.main, this);

        }
    }
}
