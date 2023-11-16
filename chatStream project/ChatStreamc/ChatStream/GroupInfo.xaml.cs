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
    /// Interaction logic for GroupInfo.xaml
    /// </summary>
    public partial class GroupInfo : UserControl
    {
        //properties
        Service1Client srv = new Service1Client();
        private MainWindow main;
        private Group group;
        private UserControl previous;
        private bool isAdmin;
        public GroupInfo(MainWindow main, GMember member, UserControl previous)
        {
            this.main = main;
            this.group = srv.GetGroupByID(member.Group.ID); //get group data
            this.previous = previous;
            member.User = main.User;
            InitializeComponent();
            GNameTxt.Text = this.group.GNname + "#" + group.ID; //show group name with ID
            this.isAdmin = srv.IsGroupAdmin(member);
            RefreshGMembers();//show all group members
            //dispay if group is private or public
            PublicityDisplayText.Text = this.group.Publicity ? "Public" : "Private";
            if(this.isAdmin)
            {
                AdminsOptionPanel.Visibility = Visibility.Visible;
                EditGroupName.Visibility = Visibility.Visible;
                //display the right button to change the group's publicity:
                (this.group.Publicity ? MakePublic_btn : MakePrivate_btn).Visibility = Visibility.Collapsed;
            }
            
        }

        /// <summary>
        /// refreash the memebers list of the group. add editing options 
        /// for admins
        /// </summary>
        private void RefreshGMembers()
        {
            GMember gMember = new GMember();
            gMember.User = this.main.User;
            gMember.Group = this.group;
            this.isAdmin = srv.IsGroupAdmin(gMember);
            List<GMember> gMembers = srv.GetGroupMembers(this.group).ToList();
            MemberListStack.Children.Clear();
            foreach (GMember member in gMembers)
            {
                WrapPanel wrapPanel = new WrapPanel();
                EntityButton uButton = new EntityButton();//button for member-user's feed with info

                TextBlock name = new TextBlock();//user's name
                uButton.Style = FindResource("SR_Buttons") as Style;
                uButton.Content = member.User.Uname;
                uButton.Entity = member.User;
                uButton.Click += UFeed;
                name.Style = FindResource("textS") as Style;
                name.Foreground = Brushes.Black;
                name.Text = member.User.Fname + " " + member.User.Lname;
                name.FontSize = 20;
                wrapPanel.Children.Add(uButton as Button);
                wrapPanel.Children.Add(name);
                if (this.isAdmin)
                {
                    EntityButton makeAdmin = new EntityButton();
                    EntityButton removeAdmin = new EntityButton();
                    EntityButton removeMember = new EntityButton();
                    makeAdmin.Style = removeAdmin.Style = removeMember.Style = FindResource("RW_Buttons") as Style;
                    makeAdmin.Content = "Make Admin";
                    removeAdmin.Content = "Remove Admin";
                    removeMember.Content = member.User.ID != this.main.User.ID ? "Remove Member" : "Leave";
                    makeAdmin.Entity = removeAdmin.Entity = removeMember.Entity= member;
                    makeAdmin.Click += MakeAdmin_Click;
                    removeAdmin.Click += RemoveAdmin_Click;
                    removeMember.Click += RemoveMember_Click;
                    (member.Admin ? makeAdmin : removeAdmin).Visibility = Visibility.Collapsed;
                    wrapPanel.Children.Add(makeAdmin as Button);
                    wrapPanel.Children.Add(removeAdmin as Button);
                    wrapPanel.Children.Add(removeMember as Button);
                }
                else
                {
                    if(member.Admin)
                    {
                        TextBlock adminText = new TextBlock();
                        adminText.Style = FindResource("textS") as Style;
                        adminText.Foreground = Brushes.Black;
                        adminText.Text = "Admin";
                        adminText.FontSize = 30;
                        wrapPanel.Children.Add(adminText);
                    }
                    if(member.User.ID == this.main.User.ID)
                    {
                        EntityButton removeMember = new EntityButton();
                        removeMember.Style = FindResource("RW_Buttons") as Style;
                        removeMember.Content = "Leave";
                        removeMember.Entity = member;
                        removeMember.Click += RemoveMember_Click;
                        wrapPanel.Children.Add(removeMember as Button);
                    }
                }
                MemberListStack.Children.Add(wrapPanel);

            }
        }
        //make a user admin in a group button click
        private void MakeAdmin_Click(object sender, RoutedEventArgs e)
        {
            EntityButton eButton = sender as EntityButton;
            GMember member = eButton.Entity as GMember;
            member.Admin = true;
            srv.UpdateGMemberStatus(member);
        }
        //remove admin from a user button click
        private void RemoveAdmin_Click(object sender, RoutedEventArgs e)
        {
            EntityButton eButton = sender as EntityButton;
            GMember member = eButton.Entity as GMember;
            member.Admin = false;
            srv.UpdateGMemberStatus(member);
        }
        // delete group member button click
        private void RemoveMember_Click(object sender, RoutedEventArgs e)
        {
            EntityButton eButton = sender as EntityButton;
            GMember member = eButton.Entity as GMember;
            srv.DeleteGMember(member);
            if(member.User.ID == this.main.User.ID)
            {
                this.main.ChangeContent.Content = new ChatsShow(this.main);
            }
        }
        //add member to the group button click
        private void AddMemberBtn_Click(object sender, RoutedEventArgs e)
        {
            MemberAddStack.Visibility = Visibility.Visible;
            ((Button)sender).Visibility = Visibility.Collapsed;
            MainScroll.ScrollToBottom();
        }
        //clear user search for adding button click
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            USerach_TB.Text = "";
        }
        //make group public button click
        private void MakePublic_btn_Click(object sender, RoutedEventArgs e)
        {
            this.group.Publicity = true;
            srv.UpdateGroup(this.group);
            Thread.Sleep(2000);//wait for while, to make sure this would became public before the update is done
            this.group = srv.GetGroupByID(this.group.ID);
            MakePublic_btn.Visibility = group.Publicity ? Visibility.Collapsed : Visibility.Visible;
            MakePrivate_btn.Visibility = group.Publicity ? Visibility.Visible : Visibility.Collapsed;
            PublicityDisplayText.Text = this.group.Publicity ? "Public" : "Private";
        }
        //make the group private button click
        private void MakePrivate_btn_Click(object sender, RoutedEventArgs e)
        {
            this.group.Publicity = false;
            srv.UpdateGroup(this.group);
            Thread.Sleep(2000);
            this.group = srv.GetGroupByID(this.group.ID);
            MakePublic_btn.Visibility = group.Publicity ? Visibility.Collapsed : Visibility.Visible;
            MakePrivate_btn.Visibility = group.Publicity ? Visibility.Visible : Visibility.Collapsed;
            PublicityDisplayText.Text = this.group.Publicity ? "Public" : "Private";
        }

        private void USerach_TB_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            MemberAddStack_Results.Children.Clear();
            if (textBox.Text.Length > 0)
            {
                
                List<User> userlist = srv.SelectUserByUName(textBox.Text).ToList();
                foreach (User user in userlist)
                {
                    WrapPanel wrapPanel = new WrapPanel();
                    EntityButton uButton = new EntityButton();
                    EntityButton addAsMember = new EntityButton();
                    TextBlock name = new TextBlock();
                    uButton.Style = FindResource("SR_Buttons") as Style;
                    uButton.Content = user.Uname;
                    uButton.Entity = user;
                    uButton.Click += UFeed;
                    name.Style = FindResource("textS") as Style;
                    name.Foreground = Brushes.Black;
                    name.Text = user.Fname + " " + user.Lname;
                    name.FontSize = 20;
                    addAsMember.Style = FindResource("SR_Buttons") as Style;
                    addAsMember.Content = "Add";
                    addAsMember.Entity = user;
                    addAsMember.Click += AddAsMember_Click;
                    wrapPanel.Children.Add(uButton as Button);
                    wrapPanel.Children.Add(name);
                    wrapPanel.Children.Add(addAsMember as Button);
                    MemberAddStack_Results.Children.Add(wrapPanel);
                }
                MainScroll.ScrollToBottom();
            }
        }

        private void UFeed(object sender, RoutedEventArgs e)
        {
            EntityButton userButton = sender as EntityButton;
            this.main.ChangeContent.Content = new UserFeed((User)userButton.Entity, this.main, this);
        }
        //add user to group
        private void AddAsMember_Click(object sender, RoutedEventArgs e)
        {
            EntityButton userButton = sender as EntityButton;
            srv.InsertGMemeber(this.group, (User)userButton.Entity);
            Thread.Sleep(2000);//wait 2 seconds before refreshing
            RefreshGMembers();
        }
        //stop showing the adding options. stop showing the user search
        private void CloseAdd_Click(object sender, RoutedEventArgs e)
        {
            MemberAddStack.Visibility = Visibility.Collapsed;
            AddMemberBtn.Visibility = Visibility.Visible;
            Clear_Click(sender, e);
        }
        //open editing the group name
        private void EditGroupName_Click(object sender, RoutedEventArgs e)
        {
            GNameTxt.Text = this.group.GNname;
            GNameTxt.IsReadOnly = false;
            GNameTxt.BorderBrush = Brushes.Black;
            SaveGroupName.Visibility = Visibility.Visible;
            EditGroupName.Visibility = Visibility.Collapsed;
        }
        //save group's name
        private void SaveGroupName_Click(object sender, RoutedEventArgs e)
        {
            GNameTxt.IsReadOnly = true;
            this.group.GNname = GNameTxt.Text;
            GNameTxt.Text = this.group.GNname + "#" + this.group.ID; 
            GNameTxt.BorderBrush = Brushes.Transparent;
            srv.UpdateGroup(group);
            SaveGroupName.Visibility = Visibility.Collapsed;
            EditGroupName.Visibility = Visibility.Visible;
        }


        //refresh all members
        private void RefreshMembers_Click(object sender, RoutedEventArgs e)
        {
            RefreshGMembers();
        }
        //exit this UserControle and move to the previous one
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.main.ChangeContent.Content = this.previous;
            if (this.previous is ChatsShow)
            {
                (this.previous as ChatsShow).StartRefreash();
            }
        }

        private void OpenChat_Click(object sender, RoutedEventArgs e)
        {
            this.main.ChangeContent.Content = new ChatWithGroup(this.main, this.group, this);
        }
    }
}
