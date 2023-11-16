using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web_proj
{
    public partial class DeleteRecord : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string fileName = "myDemoDB.mdf";
            if(Session["admin"].ToString() == "yes")
            {
                string userName = Request.QueryString["userName"].ToString();
                string sqlDelete = "DELETE FROM usersTbl WHERE userName ='" + userName + "'";
                Helper.DoQuery(fileName, sqlDelete);

                Response.Redirect("deleteUser.aspx");
            }
        }
    }
}