using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace web_proj
{
    public partial class login : System.Web.UI.Page
    {
        public string msg = "";
        public string sqlLogin = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Request.Form["submit"] != null)
            {
                string userName = Request.Form["userName"];
                string psw = Request.Form["psw"];

                string fileName = "myDemoDB.mdf";
                string tableName = "usersTbl";

                sqlLogin = "SELECT * FROM " + tableName + " WHERE userName = '" + userName + "' AND psw = '" + psw + "'";
                if(Helper.IsExist(fileName, sqlLogin))
                {
                    DataTable table = Helper.ExecuteDataTable(fileName, sqlLogin);

                    int length = table.Rows.Count;
                    if(length == 0)
                    {
                        Response.Redirect("login.aspx");
                        msg = "no users";
                    }
                    else
                    {
                        Session["userName"] = table.Rows[0]["userName"];
                        Session["userFName"] = table.Rows[0]["firstName"];
                        Response.Redirect("mainPage1.aspx");
                    }
                }
            }
        }
    }
}