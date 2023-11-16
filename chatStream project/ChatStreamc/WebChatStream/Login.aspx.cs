using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebChatStream.ServiceReference1;
using System.Text.RegularExpressions;

namespace WebChatStream
{
    public partial class Login : System.Web.UI.Page
    {
        Service1Client srv = new Service1Client();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Form["submit"] != null)
            {
                string username = Request.Form["userNameEnt"];
                string password = Request.Form["passwordEnt"];
                if (username.Length >= 3 && password.Length >= 4)
                {
                    if (!Regex.IsMatch(username, "^[a-zA-Z0-9]*$")) //condition checks if username is numbers and letters only.
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('User name should contain only letters and numbers')", true);
                    }
                    else
                    {
                        //tries to log in:
                        User user = srv.Login(username, password);
                        if (user != null && user.ID > 0)
                        {//succsessful login
                            // Store the object in Session state
                            Session["mainUserObj"] = user;
                            // Redirect to the target page
                            Response.Redirect("~/Chats.aspx");
                        }
                        else
                        {//failed to login
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Failed to login. Check Your User name and password!')", true);
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Username should contain at least 3 characters and password at least 4')", true);
                }
            }

        }
    }
}