<%@ Page Title="" Language="C#" MasterPageFile="~/masterPage.Master" AutoEventWireup="true" CodeBehind="SimpleQuery.aspx.cs" Inherits="web_proj.SimpleQuery" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function detectField()
        {
            if (document.getElementById("field").value == "gender") {
                document.getElementById("query").innerHTML =
                    "<input type='radio' name='value' value='male' />זכר" +
                    "<input type='radio' name='value' value='female' />נקבה" +
                    "<input type='radio' name='value' value='other' />אחר";
            }
            else if (document.getElementById("field").value == "yearBorn") {
                var yearStr = "<select name='value'><option value='0'>בחר שנה</option>";
                var currYear = new Date().getFullYear();
                var fromYear = currYear - 40;
                var toYear = currYear - 10;

                for (var i = fromYear; i < toYear; i++)
                    yearStr += "<option value='" + i + "'>" + i + "</option>\n";
                document.getElementById("query").innerHTML = yearStr + "</select>";
            }
            else if (document.getElementById("field").value == "prefix") {
                var prefixStr = "<select name='value'>";
                prefixStr += "<option value='050'>050</option>";
                prefixStr += "<option value='052'>052</option>";
                prefixStr += "<option value='054'>054</option>";
                prefixStr += "<option value='057'>057</option>";
                prefixStr += "<option value='077'>077</option>";
                prefixStr += "<option value='03'>03</option>";
                prefixStr += "<option value='02'>02</option>";
                prefixStr += "<option value='04'>04</option>";
                prefixStr += "<option value='08'>08</option>";
                prefixStr += "<option value='09'>09</option>";
                prefixStr += "</select>";
                document.getElementById("query").innerHTML = prefixStr;
            }
            else if (document.getElementById("field").value == "genre") {
                var genreStr = "<select name='value'>";
                genreStr += "<option value='1'>פעולה</option>";
                genreStr += "<option value='2'>קומדיה</option>";
                genreStr += "<option value='3'>דרמה</option>";
                genreStr += "<option value='4'>פשע</option>";
                genreStr += "<option value='5'>אנימציה</option>";
                genreStr += "</select>";
                document.getElementById("query").innerHTML = genreStr;
            }
            else
                document.getElementById("query").innerHTML = "<input type='text' name='value' />";
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>הצגת נתונים לפי חתך</h1>
    <form method="post" Runat="Server">
        <select name="field" id="field" onclick="detectField(); ">
            <option value="userName">שם משתמש</option>
            <option value="lName">שם משפחה</option>
            <option value="fName">שם פרטי</option>
            <option value="email">דוא'ל</option>
            <option value="gender">מגדר</option>
            <option value="prefix">קידומת טלפון</option>
            <option value="phone">טלפון</option>
            <option value="genre">ז'אנר</option>
        </select>
        <div id ="query"></div>
        <br /><br />
        <input type="submit" name="submit" value="חפש" />
        <h2 dir = "ltr"><%= sql %></h2>
        <table style ="border: 1px solid black; margin: 0px auto;">
            <%= st %>
        </table>
        
        <h3><%= msg %></h3>
    </form>
</asp:Content>

