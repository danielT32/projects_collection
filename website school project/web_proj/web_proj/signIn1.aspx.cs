using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace web_proj
{
    public partial class signIn1 : System.Web.UI.Page
    {
        public string st = "";
        public string sqlMsg = "";
        public string msgSent = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Form["submit"] != null)
            {
                st += "<table dir = 'ltr' border ='1' style = 'text-align:center'";
                st += "<tr><th colspan='2'>הפרטים שהתקבלו</th></tr>";

                string userName = Request.Form["userName"];
                string firstName = Request.Form["firstName"];
                string lastName = Request.Form["familyName"];
                string email = Request.Form["email"];
                string psw = Request.Form["psw1"];
                string gender = Request.Form["gender"];
                string prefix = Request.Form["prefix"];
                string phoneNum = Request.Form["phoneNum"];
                string genre = Request.Form["genre"].ToString();

                char action = 'F';
                char comedy = 'F';
                char drama = 'F';
                char crime = 'F';
                char animetion = 'F';
                if (genre.Contains("action"))
                {
                    action = 'T';
                }
                if (genre.Contains("drama"))
                {
                    drama = 'T';
                }
                if (genre.Contains("comedy"))
                {
                    comedy = 'T';
                }
                if (genre.Contains("crime"))
                {
                    crime = 'T';
                }
                if (genre.Contains("animetion"))
                {
                    animetion = 'T';
                }


                st += "<tr><td>user name: </td><td>" + userName + "</td></tr>";
                st += "<tr><td>first name: </td><td>" + firstName + "</td></tr>";
                st += "<tr><td>last name: </td><td>" + lastName + "</td></tr>";
                st += "<tr><td>email: </td><td>" + email + "</td></tr>";
                st += "<tr><td>password: </td><td>" + psw + "</td></tr>";
                st += "<tr><td>gender: </td><td>" + gender + "</td></tr>";
                st += "<tr><td>phone: </td><td>" + prefix +"-"+ phoneNum + "</td></tr>";
                st += "<tr><td>genre: </td><td>" + genre + "</td></tr>";

                st += "</table>";

                string fileName = "myDemoDB.mdf";
                string tableName = "usersTbl";
                string sqlInsert = "";
                string sqlSelect = "SELECT * FROM " + tableName + " WHERE userName = '" + userName + "'";
                if(Helper.IsExist(fileName, sqlSelect))
                {
                    msgSent = "user name has been taken";
                    sqlMsg = sqlSelect;
                }
                else
                {
                    sqlInsert = $"INSERT INTO {tableName} VALUES('{userName}', '{firstName}', '{lastName}', '{email}', '{psw}', '{gender}', '{prefix}', '{phoneNum}', '{genre}', '{action}', '{comedy}', '{drama}', '{crime}', '{animetion}'); ";
                    Helper.DoQuery(fileName, sqlInsert);
                    msgSent = "Success";
                }
            }
            
        }
    }
}