using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace web_proj
{
    public partial class loginAdmin : System.Web.UI.Page
    {
        public string msg;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Request.Form["submit"] != null)
            {
                string mName = Request.Form["mName"];
                string psw = Request.Form["psw"];

                string fileName = "myDemoDB.mdf";
                string tableName = "managerTbl";

                string sqlLogin = "SELECT * FROM " + tableName + " WHERE mName = '" + mName + "' AND psw = '" + psw + "'";

                DataTable table = Helper.ExecuteDataTable(fileName, sqlLogin);
                int length = table.Rows.Count;
                if(length == 0)
                {
                    msg = "<div style='text-aligh: center;'>";
                    msg += "<h3>אינך מנהל, אין לך הרשאות לצפות בדף זה.</h3>";
                    msg += "<a href='mainpage1.aspx'>[המשך]</a>";
                    msg += "</div>";
                }
                else
                {
                    Session["userName"] = "מנהל";
                    Session["admin"] = "yes";
                    Response.Redirect("mainpage1.aspx");
                }
            }

            
        }
    }
}