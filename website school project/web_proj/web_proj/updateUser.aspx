<%@ Page Title="" Language="C#" MasterPageFile="~/masterPage.Master" AutoEventWireup="true" CodeBehind="updateUser.aspx.cs" Inherits="web_proj.updateUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
            function chkForm() {
                var flag = true;
                var prefix = document.getElementById("prefix").value;
                var phoneNum = document.getElementById("phoneNum").value;
                if (prefix == "choose") {
                    document.getElementById("mPhone").value = " לא נבחרה קידומת";
                    document.getElementById("mPhone").style.display = "inline";
                    flag = false;
                }
                else if (isNaN(phoneNum)) {
                    document.getElementById("mPhone").value = "מספר טלפון חייב להכיל ספרות בלבד";
                    document.getElementById("mPhone").style.display = "inline";
                    flag = false;
                }
                else if (phoneNum.length != 7) {
                    document.getElementById("mPhone").value = "מספר טלפון חייב להכיל 7 ספרות ";
                    document.getElementById("mPhone").style.display = "inline";
                    flag = false;
                }
                else
                    document.getElementById("mPhone").style.display = "none";



                var email = document.getElementById("email").value;
                var size = email.length;
                var atSign = email.indexOf('@');
                var dotSign = email.indexOf('.', atSign);
                var msg = "";
                if (size < 6) {
                    msg = "כתובת דואל קצרה מידי או לא קיימת ";
                    valid = false;
                }
                else if (atSign == -1) {
                    msg = "סימן @ לא קיים בכתובת ";
                    valid = false;
                }
                else if (atSign == -1) {
                    msg = "כתובת דואל קצרה מידי או לא קדדמת ";
                    valid = false;
                }
                else if (atSign != email.lastIndexOf('@')) {
                    msg = "אסור יותר מ@ אחד בכתובת הדואל.";
                    valid = false;
                }
                else if (atSign < 2 || email.lastIndexOf('@') == size - 1) {
                    msg = "סימן ה@ אינו יכול להיות במקום ראשון או אחרון ";
                    valid = false;
                }
                else if (email.indexOf('.') == 0 || email.lastIndexOf('.') == size - 1) {
                    msg = "נקודה לא יכולה להיות תו ראשון או אחרון בכתובת  ";
                    valid = false;
                }
                else if (dotSign - atSign <= 1) {
                    msg = "נקודה חייבת להיות לפחות 2 תווים אחרי הסימן @";
                    valid = false;
                }
                else if (isValidString(email) == false) {
                    msg = "נמצאו תווים אסורים";
                    valid = false;
                }


                var fName = document.getElementById("firstName").value;
                if (fName.length < 2) {
                    document.getElementById("mfName").value = " שם פרטי קצר מידי או לא קיים ";
                    document.getElementById("mfName").style.display = "inline";
                    flag = false;
                }
                else
                    document.getElementById("mfName").style.display = "none";

                var lName = document.getElementById("familyName").value;
                if (lName.length < 2) {
                    document.getElementById("mlName").value = "שם משפחה קצר מידי או לא קיים ";
                    document.getElementById("mlName").style.display = "inline";
                    flag = false;
                }
                else
                    document.getElementById("mlName").style.display = "none";

                var password = document.getElementById("psw1").value;
                if (password.length < 5 || password.length > 8) {
                    document.getElementById("mPassword").value = "סיסמא קצרה מידי או ארוכה מידי  (בין 6 ל 8 תווים( ";
                    document.getElementById("mPassword").style.display = "inline";
                    flag = false;
                }
                else
                    document.getElementById("mPassword").style.display = "none";
                var password1 = document.getElementById("psw2").value;
                if (password1.length < 5 || password1.length > 8) {
                    document.getElementById("mPassword1").value = " סיסמא קצרה מידי או ארוכה מידי  (בין 6 ל 8 תווים(";
                    document.getElementById("mPassword1").style.display = "inline";
                    flag = false;
                }
                else
                    document.getElementById("mPassword1").style.display = "none";
                if (password != password1) {
                    document.getElementById("mPassword1").value = "סיסמות לא תואמות";
                    document.getElementById("mPassword1").style.display = "inline";
                    flag = false;
                } else
                    document.getElementById("mPassword1").style.display = "none";

                var genre = document.getElementsByName("genre");
                var genreChecked = false;
                for (var i = 0; i < genre.length; i++)
                    if (genre[i].checked)
                        genreChecked = true;
                if (genreChecked == false) {
                    document.getElementById("mGenre").value = "לא נבחר ז'אנר";
                    document.getElementById("mGenre").style.display = "inline";
                    flag = false;
                }
                else
                    document.getElementById("mGenre").style.display = "none";

                if (msg != "") {
                    document.getElementById("mMail").value = msg;
                    document.getElementById("mMail").style.display = "inline";
                    flag = false;
                }
                else
                    document.getElementById("mMail").style.display = "none";



                return flag;
            }
            function isValidString(str) {
                var quot = "\"";
                if (str.indexOf(quot) != -1)
                    return false;
                var badStr = "$%^&* ()_ + []{ }<>?אבגדהוזחטיכךלמםנןסעפצקרשת";
                var i = 0, p, ch;
                while (i < badStr.length) {
                    ch = badStr.charAt(i);
                    p = str.indexOf(ch);
                    if (p != -1) {
                        return false;
                    }
                    i++;
                }
                return true;
            }

            function onlyNumString(str) { //same as isNaN
                var goodStr = "0123456789";
                var i = 0, goodIndex, strCh;
                while (i < str.length) {
                    strCh = str.charAt(i);
                    goodIndex = goodStr.indexOf(strCh);
                    if (goodIndex != -1) {
                        return false;
                    }
                    i++;
                }
                return true;
            }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>עדכון פרטים</h1>
    <form method="post" runat="server" onsubmit ="return chkForm();">
        <table>
            <tr>
                <td>שם משתמש: </td>
                <td><input type="text" id="userName" name="userName" disabled ="disabled" value ="<%= userName %>" /> </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>שם פרטי: </td>
                <td><input type="text" id="firstName" name="firstName" value ="<%= firstName %>" /></td>
                <td>
                    <input type="text" id="mfName" name="mfName" size="30"
                        style="display:none; background-color:yellow; font-weight:bold;"
                            disabled="disabled"/>
                </td>
            </tr> 
            <tr>
                <td>שם משפחה: </td>
                <td><input type="text" id="familyName" name="familyName" value ="<%= lastName %>" /> </td>
                <td>
                    <input type="text" id="mlName" name="mlName" size="30"
                        style="display:none; background-color:yellow; font-weight:bold;"
                            disabled="disabled"/>
                </td>
            </tr>
            <tr>
                <td>כתובת דוא"ל: </td>
                <td><input type="text" id="email" name="email" value="<%= email %>" /> </td>
                <td><input type="text" id="mMail" name="mMail" size="30"
                        style="display:none; background-color:yellow; font-weight:bold;"
                            disabled="disabled"/>
                </td>
            </tr>
            <tr>
                <td>סיסמא: </td>
                <td><input type="password" id="psw1" name="psw1" value="<%= psw %>"/> </td>
                <td>
                    <input type="text" id="mPassword" name="mPassword" size="35"
                        style="display:none; background-color:yellow; font-weight:bold;"
                            disabled="disabled"/>
                </td>
            </tr>
            <tr>
                <td> סיסמא : </td>
                <td><input type="password" id="psw2" name="psw2" /> </td>
                <td>
                    <input type="text" id="mPassword1" name="mPassword1" size="35"
                        style="display:none; background-color:yellow; font-weight:bold;"
                            disabled="disabled"/>
                </td>
            </tr>
            <tr>
                <td> מגדר : </td>
                <td><% if (gender == "male") { %>
                    <input type="radio" name="gender" value = "male" checked>זכר
                    <input type="radio" name="gender" value = "female">נקבה 
                    <input type="radio" name="gender" value = "other">אחר 
                    <% } else if (gender == "female") { %>
                    <input type="radio" name="gender" value = "male" >זכר
                    <input type="radio" name="gender" value = "female" checked>נקבה 
                    <input type="radio" name="gender" value = "other">אחר
                    <% } else { %>
                    <input type="radio" name="gender" value = "male" >זכר
                    <input type="radio" name="gender" value = "female" >נקבה 
                    <input type="radio" name="gender" value = "other" checked >אחר
                    <% } %>

                </td>
                <td></td>
            </tr>
            <tr>
                <td> מספר טלפון : </td>
                <td style="direction:ltr">
                    <select name="prefix" id="prefix">
                        <% if (prefix == "050") { %>
                        <option value="050" selected>050</option>
                        <% } else { %>
                        <option value="050" >050</option>
                        <% } %>
                        <% if (prefix == "052") { %>
                        <option value="052" selected>052</option>
                        <% } else { %>
                        <option value="052" >052</option>
                        <% } %>
                        <% if (prefix == "053") { %>
                        <option value="053" selected>053</option>
                        <% } else { %>
                        <option value="053" >053</option>
                        <% } %>
                        <% if (prefix == "054") { %>
                        <option value="054" selected>054</option>
                        <% } else { %>
                        <option value="054" >054</option>
                        <% } %>
                        <% if (prefix == "055") { %>
                        <option value="055" selected>055</option>
                        <% } else { %>
                        <option value="055" >055</option>
                        <% } %>
                         <% if (prefix == "057") { %>
                        <option value="057" selected>057</option>
                        <% } else { %>
                        <option value="057" >057</option>
                        <% } %>
                         <% if (prefix == "058") { %>
                        <option value="058" selected>058</option>
                        <% } else { %>
                        <option value="058" >058</option>
                        <% } %>
                         <% if (prefix == "02") { %>
                        <option value="02" selected>02</option>
                        <% } else { %>
                        <option value="02" >02</option>
                        <% } %>
                         <% if (prefix == "03") { %>
                        <option value="03" selected>03</option>
                        <% } else { %>
                        <option value="03" >03</option>
                        <% } %>
                         <% if (prefix == "04") { %>
                        <option value="04" selected>04</option>
                        <% } else { %>
                        <option value="04" >04</option>
                        <% } %>
                        <% if (prefix == "08") { %>
                        <option value="08" selected>08</option>
                        <% } else { %>
                        <option value="08" >08</option>
                        <% } %>
                        <% if (prefix == "09") { %>
                        <option value="09" selected>09</option>
                        <% } else { %>
                        <option value="09" >09</option>
                        <% } %>
                        <% if (prefix == "077") { %>
                        <option value="077" selected>077</option>
                        <% } else { %>
                        <option value="077" >077</option>
                        <% } %>
                    </select>
                    <input type="text" id="phoneNum" name="phoneNum" value ="<%=phoneNum %>"/>
                </td>
                <td>
                    <input type="text" id="mPhone" name="mPhone" size="30"
                        style="display:none; background-color:yellow; font-weight:bold;"
                            disabled="disabled"/>
                </td>
            </tr>
            <tr>
                <td> ז'אנרים : </td>
                <td>
                    <%if (action == "T"){ %>
                    <input type = "checkbox" name = "genre" value = "action" checked="checked"/>פעולה
                    <% } else { %>
                    <input type = "checkbox" name = "genre" value = "action" />פעולה
                    <% } %>
                    <%if (comedy == "T"){ %>
                    <input type = "checkbox" name = "genre" value = "comedy" checked="checked"/>קומדיה
                    <% } else { %>
                    <input type = "checkbox" name = "genre" value = "comedy" />קומדיה
                    <% } %>
                    <%if (drama == "T"){ %>
                    <input type = "checkbox" name = "genre" value = "drama" checked="checked"/>דרמה
                    <% } else { %>
                    <input type = "checkbox" name = "genre" value = "drama" />דרמה
                    <% } %>
                    <%if (crime == "T"){ %>
                    <input type = "checkbox" name = "genre" value = "crime" checked="checked"/>פשע
                    <% } else { %>
                    <input type = "checkbox" name = "genre" value = "crime" />פשע
                    <% } %>
                    <%if (animetion == "T"){ %>
                    <input type = "checkbox" name = "genre" value = "animetion" checked="checked"/>אנימציה
                    <% } else { %>
                    <input type = "checkbox" name = "genre" value = "animetion" />אנימציה
                    <% } %>
                </td>
                <td>
                    <input type="text" id="mGenre" name="mGenre" size="30"
                        style="display:none; background-color:yellow; font-weight:bold;"
                            disabled="disabled"/>
                </td>
            </tr>

            <tr>
                <td colspan="3" style="text-align:center">
                    <input type="submit" name="submit" value=" עדכן " />
                </td>
            </tr>
        </table>
        <div style="direction:ltr"><%=msg %></div>
        <div style="direction:rtl"><%=sqlUpdate %></div>
        <br/><br />
    </form>
</asp:Content>
