using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace web_proj
{
    public partial class SimpleQuery : System.Web.UI.Page
    {
        public string st = ""; //מחרוזת לטבלת תוצאות
        public string msg = ""; //מחרוזת כמה רשומות מתאימות
        public string sql = ""; // מחרוזת להצגת שאילתה
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["admin"] == "no")
            {
                msg = "<div align = center><h3>";
                msg += "אינך מנהל, אין לך הרשאות להיכנס לעמוד זה";
                msg += "</h3>";
                msg += "[<a href = 'mainpage1.aspx'>חזור</a>]";
                msg += "</div>";
            }
            else
            {
                // שם שדה וערכו:
                string field = Request.Form["field"];
                string value = Request.Form["value"];
                //מסד נתונים ושם הטבלה לשימוש שאילתא:
                string fileName = "myDemoDB.mdf";
                string tableName = "usersTbl";

                if (field == "lName")
                {
                    field = "lastName";
                }
                else if (field == "fName")
                {
                    field = "firstName";
                }
                else if (field == "phone")
                {
                    field = "phoneNum";
                }
                if (field == null)
                {
                    sql = "SELECT * FROM " + tableName;
                }
                else if (field == "gender" || field == "prefix")
                    sql = "SELECT * FROM " + tableName + " where (" + field + " = '" + value + "');";
                else if (field == "phone")
                {
                    sql = "SELECT * FROM " + tableName + " where (phoneNum = '" + value + "');";
                }
                else if (field == "genre")
                {
                    var val = int.Parse(value);
                    switch (val)
                    {
                        case 1: value = "action"; break;
                        case 2: value = "comedy"; break;
                        case 3: value = "drama"; break;
                        case 4: value = "crime"; break;
                        case 5: value = "animetion"; break;
                    }
                    sql = "SELECT * FROM " + tableName + " where (" + value + " = 'T');";
                }
                else if (field == "email")
                {
                    sql = "SELECT * FROM " + tableName + " where (" + field + " like '%" + value + "%');";
                }
                else
                    sql = "SELECT * FROM " + tableName + " where (" + field + " like N'" + value + "%');";
                if (field == null)
                {

                }
                DataTable table = Helper.ExecuteDataTable(fileName, sql);
                int length = table.Rows.Count;
                if (length == 0)
                    msg = "לא נמצאו רשומות תואמות לחיפוש ";
                else
                {
                    st += "<tr>";
                    st += "<th style = 'text-align: center; border: 1px solid black; width: 100px;' >שם משתמש</th>";
                    st += "<th style = 'text-align: center; border: 1px solid black; width: 80px;' >שם משפחה</th>";
                    st += "<th style = 'text-align: center; border: 1px solid black; width: 60px;' >שם פרטי</th>";
                    st += "<th style = 'text-align: center; border: 1px solid black; width: 140px;' >דוא'ל</th>";
                    st += "<th style = 'text-align: center; border: 1px solid black; width: 60px;' >מגדר</th>";
                    st += "<th style = 'text-align: center; border: 1px solid black; width: 100px;' > טלפון</th>";
                    st += "<th style = 'text-align: center; border: 1px solid black;' > genre</th>";
                    //st += "th style = 'text-align: center; border: 1px solid black; ' > comedy</th>";
                    //st += "th style = 'text-align: center; border: 1px solid black; ' > drama</th>";
                    //st += "th style = 'text-align: center; border: 1px solid black; ' > crime</th>";
                    //st += "th style = 'text-align: center; border: 1px solid black; ' > animetion</th>";
                    //st += "th style = 'text-align: center; border: 1px solid black; width: 100px;' >סיסמא</th>";
                    st += "</tr>";

                    for (int i = 0; i < length; i++)
                    {
                        st += "<tr>";
                        st += "<td style = 'text-align: center; border: 1px solid black;'>" + table.Rows[i]["userName"] + "</td>";
                        st += "<td style = 'border: 1px solid black;'>" + table.Rows[i]["lastName"] + "</td>";
                        st += "<td style = 'border: 1px solid black;'>" + table.Rows[i]["firstName"] + "</td>";
                        st += "<td style = 'border: 1px solid black;'>" + table.Rows[i]["email"] + "</td>";
                        st += "<td style = 'text-align: center; border: 1px solid black;'>" + table.Rows[i]["gender"] + "</td>";
                        st += "<td style = 'text-align: center; border: 1px solid black;'>" + table.Rows[i]["prefix"] + "-" + table.Rows[i]["phoneNum"] + "</td>";
                        st += "<td style = 'text-align: center; border: 1px solid black;'>" + table.Rows[i]["genre"] + "</td>";
                        //st += "<td style = 'text-align: center; border: 1px solid black;'>" + table.Rows[i]["comedy"] + "</td>";
                        //st += "<td style = 'text-align: center; border: 1px solid black;'>" + table.Rows[i]["drama"] + "</td>";
                        //st += "<td style = 'text-align: center; border: 1px solid black;'>" + table.Rows[i]["crime"] + "</td>";
                        //st += "<td style = 'text-align: center; border: 1px solid black;'>" + table.Rows[i]["animetion"] + "</td>";
                        //st += "<td style = 'text-align: center; border: 1px solid black;'>" + table.Rows[i]["psw"] + "</td>";
                        st += "</tr>";
                    }
                    msg = length + " אנשים העונים לתוצאת החיפוש";

                }
            }
        }
    }
}