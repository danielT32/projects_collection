using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace web_proj
{
    public partial class updateUser : System.Web.UI.Page
    {
        public string msg = "";
        public string sqlUpdate = "";
        public string sqlSelect = "";

        public string userName, firstName, lastName, email, psw, gender, prefix, phoneNum;
        public string genre, action, comedy, drama, crime, animetion;

        protected void Page_Load(object sender, EventArgs e)
        {
            string fileName = "myDemoDB.mdf";
            userName = Session["userName"].ToString().Trim();

            if (userName == "אורח")
            {
                msg = "אינך משתמש רשום במערכת";
                Response.Redirect("mainpage1.aspx");
            }
            sqlSelect = "SELECT * FROM usersTbl WHERE userName = '" + userName + "'";
            DataTable table = Helper.ExecuteDataTable(fileName, sqlSelect);

            int length = table.Rows.Count;
            if(length == 0)
            {
                msg = "אינך משתמש רשום במערכת";
            }
            else
            {
                firstName = table.Rows[0]["firstName"].ToString().Trim();
                lastName = table.Rows[0]["lastName"].ToString().Trim();
                email = table.Rows[0]["email"].ToString().Trim();
                prefix = table.Rows[0]["prefix"].ToString().Trim();
                phoneNum = table.Rows[0]["phoneNum"].ToString().Trim();
                gender = table.Rows[0]["gender"].ToString().Trim();
                action = table.Rows[0]["action"].ToString().Trim();
                comedy = table.Rows[0]["comedy"].ToString().Trim();
                drama = table.Rows[0]["drama"].ToString().Trim();
                crime = table.Rows[0]["crime"].ToString().Trim();
                animetion = table.Rows[0]["animetion"].ToString().Trim();
                psw = table.Rows[0]["psw"].ToString().Trim();

            }

            if (this.IsPostBack)
            {
                firstName = Request.Form["firstName"];
                lastName = Request.Form["familyName"];
                email = Request.Form["email"];
                psw = Request.Form["psw1"];
                gender = Request.Form["gender"];
                prefix = Request.Form["prefix"];
                phoneNum = Request.Form["phoneNum"];
                genre = Request.Form["genre"].ToString();

                action = "F";
                comedy = "F";
                drama = "F";
                crime = "F";
                animetion = "F";
                if (genre.Contains("action"))
                {
                    action = "T";
                }
                if (genre.Contains("drama"))
                {
                    drama = "T";
                }
                if (genre.Contains("comedy"))
                {
                    comedy = "T";
                }
                if (genre.Contains("crime"))
                {
                    crime = "T";
                }
                if (genre.Contains("animetion"))
                {
                    animetion = "T";
                }

                sqlUpdate = "UPDATE usersTbl ";
                sqlUpdate += "SET firstName = '" + firstName + "', ";
                sqlUpdate += "lastName = '" +  lastName + "', ";
                sqlUpdate += "email = '" +  email + "', ";
                sqlUpdate += "psw = '" +  psw + "', ";
                sqlUpdate += "gender = '" + gender + "', ";
                sqlUpdate += "prefix = '" + prefix + "', ";
                sqlUpdate += "phoneNum = '" + phoneNum + "', ";
                sqlUpdate += "genre = '" + genre + "', ";
                sqlUpdate += "action = '" + action + "', ";
                sqlUpdate += "comedy = '" + comedy + "', ";
                sqlUpdate += "drama = '" + drama + "', ";
                sqlUpdate += "crime = '" + crime + "', ";
                sqlUpdate += "animetion = '" + animetion + "' ";
                sqlUpdate += "WHERE userName = '" + userName + "'";
                Helper.DoQuery(fileName, sqlUpdate);

                msg = "הפרטים עודכנו בהצלחה";

            }
        }
    }
}