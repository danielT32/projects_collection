using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace web_proj
{
    public partial class deleteUser : System.Web.UI.Page
    {
        public string st = "";
        public string msg = "";
        public string sqlDelete = "";
        public string usersTName = "usersTbl";
        public string fileName = "myDemoDB.mdf";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["admin"] == "no")
            {
                msg = "<div align = center><h3>";
                msg += "אינך מנהל, אין לך הרשאות להיכנס לעמוד זה";
                msg += "</h3>";
                msg += "[<a href = 'mainpage1.aspx'>חזור</a>]";
                msg += "</div>";
            }
            else
            {
                string sqlSelect = "SELECT * FROM " + usersTName;
                DataTable table = Helper.ExecuteDataTable(fileName, sqlSelect);
                string userToDelete = "";
                int length = table.Rows.Count;
                if (length == 0)
                    msg = "אין נרשמים";
                else
                {

                    st += "<tr>";
                    st += "<th style = 'width: 100px; border: 1px solid black;' >שם משתמש</th>";
                    st += "<th style = 'width: 80px; border: 1px solid black;' >שם משפחה</th>";
                    st += "<th style = 'width: 60px; border: 1px solid black;' >שם פרטי</th>";
                    st += "<th style = 'width: 140px; border: 1px solid black;' >דוא\"ל</th>";
                    st += "<th style = 'width: 60px; border: 1px solid black;' >מגדר</th>";
                    st += "<th style = 'width: 100px; border: 1px solid black;' >סיסמה</th>";
                    st += "<th style = 'width: 100px; border: 1px solid black;' >טלפון</th>";
                    st += "<th style = 'border: 1px solid black;' >action</th>";
                    st += "<th style = 'border: 1px solid black;' >drama</th>";
                    st += "<th style = 'border: 1px solid black;' >comedy</th>";
                    st += "<th style = 'border: 1px solid black;' >crime</th>";
                    st += "<th style = 'border: 1px solid black;' >animetion</th>";
                    st += "<th style = 'border: 1px solid black;' >DELETE</th>";
                    st += "</tr>";


                    for(int i = 0; i < length; i++)
                    {
                        st += "<tr>";
                        st += "<td style = 'border: 1px solid black;' >" + table.Rows[i]["userName"] + "</td>";
                        st += "<td style = 'border: 1px solid black;' >" + table.Rows[i]["lastName"] + "</td>";
                        st += "<td style = 'border: 1px solid black;' >" + table.Rows[i]["firstName"] + "</td>";
                        st += "<td style = 'width: 60; text-align: left;border: 1px solid black;'>" + table.Rows[i]["email"] + "</td>";
                        st += "<td style = 'border: 1px solid black;' >" + table.Rows[i]["gender"] + "</td>";
                        st += "<td style = 'border: 1px solid black;' >" + table.Rows[i]["psw"] + "</td>";
                        st += "<td style = 'border: 1px solid black;' >" + table.Rows[i]["prefix"] + "-" + table.Rows[i]["phoneNum"] + "</td>";
                        st += "<td style = 'border: 1px solid black;' >" + table.Rows[i]["action"] + "</td>";
                        st += "<td style = 'border: 1px solid black;' >" + table.Rows[i]["drama"] + "</td>";
                        st += "<td style = 'border: 1px solid black;' >" + table.Rows[i]["comedy"] + "</td>";
                        st += "<td style = 'border: 1px solid black;' >" + table.Rows[i]["crime"] + "</td>";
                        st += "<td style = 'border: 1px solid black;' >" + table.Rows[i]["animetion"] + "</td>";
                        userToDelete = table.Rows[i]["userName"].ToString();
                        st += "<td style = 'text-align: center; border: 1px solid black;'>";
                        st += "<a href = 'DeleteRecord.aspx?userName=" + userToDelete + "'>[delete]</a>";
                        st += "</tr>";
                    }

                }

            }
        }
    }
}