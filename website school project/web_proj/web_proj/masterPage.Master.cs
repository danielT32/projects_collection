using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web_proj
{
    public partial class masterPage : System.Web.UI.MasterPage
    {

        public string loginMsg;
        protected void Page_Load(object sender, EventArgs e)
        {
            loginMsg += "<h3> ";
            loginMsg = "שלום ";
            loginMsg += Session["userName"];
            loginMsg += " </h3> <br />";
            if (Session["userName"] == "אורח")
            {
                loginMsg += "[<a href='login.aspx'>התחבר</a>]<br />";
                loginMsg += "[<a href='signIn1.aspx'>הרשם</a>]<br />";
            }
            else if(Session["admin"].ToString() == "yes")
            {
                loginMsg += "[<a href='SimpleQuery.aspx'>דף מנהל</a>]<br />";
                loginMsg += "[<a href = 'logout.aspx'>התנתק</a>]<br />";
            }
            else
            {
                loginMsg += "[<a href = 'updateUser.aspx'>עדכון פרטים</a>]<br />";
                loginMsg += "[<a href = 'logout.aspx'>התנתק</a>]<br />";
            }
        }
    }
}