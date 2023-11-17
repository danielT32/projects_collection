using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebChatStream.ServiceReference1;
using System.Web.UI.HtmlControls;
using System.Threading;


namespace WebChatStream
{
    public partial class Chats : System.Web.UI.Page
    {
        private User mainUser;
        private User chatUser;
        private int updateState;
        private Service1Client srv = new Service1Client();
        private bool inChat;
        public Chats()
        {
            mainUser = null;
            chatUser = null;
            inChat = false;
        }




        protected void Page_Load(object sender, EventArgs e)
        {
            this.chatUser = (User)Session["chatUserObj"];
            if (mainUser == null && Session["mainUserObj"] != null)
            {
                this.updateState = 0;
                this.mainUser = (User)Session["mainUserObj"];
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
            if (mainUser != null)
            {
                if (this.chatUser == null)
                {
                    int curUpdateState = srv.GetChatsDMUpdateState(this.mainUser);
                    if (curUpdateState != this.updateState)
                    {
                        this.updateState = srv.GetChatsDMUpdateState(this.mainUser);
                        List<User> chatUsers = srv.GetAllDMChats(this.mainUser).ToList();
                        ShowChatsList(chatUsers);
                    }
                }
                else
                {
                    this.updateState = 0;
                    if (this.inChat)
                    {
                        Thread.Sleep(500);
                        List<DMessage> messages = srv.SelectUnSeenDM(this.mainUser, this.chatUser).ToList();
                        AppendMessage(messages);
                    }
                    else
                    {
                        contentView.Controls.Clear();
                        Label uName = new Label();
                        uName.Text = chatUser.Uname;
                        uName.Font.Size = 30;
                        uName.Style["text-align"] = "center";
                        contentView.Controls.Add(uName);
                        contentView.Controls.Add(new HtmlGenericControl("br"));
                        List<DMessage> messages = srv.SelectAllDMsChat(this.mainUser, this.chatUser).ToList();
                        AppendMessage(messages);
                        inChat = true;
                        MessageSendPanel.Visible = true;
                    }
                    
                }
            }
            
        }
        private void Chat_Click(object sender, EventArgs e)
        {
            Button userButton = sender as Button;
            Session["chatUserObj"] = srv.SelectByUserID(int.Parse(userButton.ID));
            this.chatUser = (User)Session["chatUserObj"];
            contentView.Controls.Clear();
            Label uName = new Label();
            uName.Text = chatUser.Uname;
            uName.Font.Size = 30;
            uName.Style["text-align"] = "center";
            contentView.Controls.Add(uName);
            contentView.Controls.Add(new HtmlGenericControl("br"));
            List<DMessage> messages = srv.SelectAllDMsChat(this.mainUser, this.chatUser).ToList();
            AppendMessage(messages);
            inChat = true;
            MessageSendPanel.Visible = true;
        }
        protected void Timer1_Tick(object sender, EventArgs e)
        {
            if (this.chatUser == null)
            {
                int curUpdateState = srv.GetChatsDMUpdateState(this.mainUser);
                if (curUpdateState != this.updateState)
                {
                    this.updateState = srv.GetChatsDMUpdateState(this.mainUser);
                    List<User> chatUsers = srv.GetAllDMChats(this.mainUser).ToList();
                    ShowChatsList(chatUsers);
                }
            }
            else
            {
                this.updateState = 0;
                if (this.inChat)
                {
                    Thread.Sleep(500);
                    List<DMessage> messages = srv.SelectUnSeenDM(this.mainUser, this.chatUser).ToList();
                    AppendMessage(messages);
                    if(messageSentTxt.Text != "")
                    {//scroll to bottom if textbox contains text
                        RegisterStartupScript("ScrollScript", "document.getElementById('objectId').scrollIntoView(true);");
                    }
                }
                else
                {
                    contentView.Controls.Clear();
                    Label uName = new Label();
                    uName.Text = chatUser.Uname;
                    uName.Font.Size = 30;
                    uName.Style["text-align"] = "center";
                    contentView.Controls.Add(uName);
                    contentView.Controls.Add(new HtmlGenericControl("br"));
                    List<DMessage> messages = srv.SelectAllDMsChat(this.mainUser, this.chatUser).ToList();
                    AppendMessage(messages);
                    inChat = true;
                    MessageSendPanel.Visible = true;
                }

            }
        }
        private void ShowChatsList(List<User> userlist)
        {
            foreach(User user in userlist)
            {
                Button button = new Button();
                button.Text = user.Uname;
                button.CssClass = "button";
                button.Style["text-align"] = "center";
                button.OnClientClick = "Chat_Click";
                button.Click += new EventHandler(Chat_Click);
                button.ID = user.ID.ToString();
                contentView.Controls.Add(button);
                contentView.Controls.Add(new HtmlGenericControl("br"));

            }
        }
        private void AppendMessage(List<DMessage> messagelist)
        {
            foreach (DMessage message in messagelist)
            {
                Panel panel = new Panel();
                Label content = new Label();
                Label date = new Label();
                
                date.Text = message.Time.ToString();
                date.Font.Size = 25;
                content.Text = message.SourceUser.Uname + ": " + message.Content + "\t";
                content.Font.Size = 30;
                panel.Controls.Add(content);
                panel.Controls.Add(date);
                panel.BackColor = message.SourceUser.ID == this.mainUser.ID ? System.Drawing.Color.DarkCyan: System.Drawing.Color.DarkGray;
                contentView.Controls.Add(panel);

            }
        }
        protected void Send_Click(object sender, EventArgs e)
        {
            DMessage message = new DMessage();
            message.Content = messageSentTxt.Text;
            message.SourceUser = this.mainUser;
            message.DestUser = this.chatUser;
            srv.InsertDM(message);
            messageSentTxt.Text = "";
            this.chatUser = (User)Session["chatUserObj"];
            Response.Redirect("~/Chats.aspx");

        }
        protected void Back_Click(object sender, EventArgs e)
        {
            this.chatUser = null;
            Session["chatUserObj"] = null;
            contentView.Controls.Clear();
            this.inChat = false;
            MessageSendPanel.Visible = false;
            Response.Redirect("~/Chats.aspx");
        }

    }
    public class EntityButton : Button
    {
        private BaseEntity entity;

        public BaseEntity Entity { get => entity; set => entity = value; }
    }
}