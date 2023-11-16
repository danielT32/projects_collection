<%@ Page Title="" Language="C#" MasterPageFile="~/masterPage.Master" AutoEventWireup="true" CodeBehind="signIn1.aspx.cs" Inherits="web_proj.signIn1" %>
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
            
            var uName = document.getElementById("userName").value;
            if (uName.length < 2) {
                document.getElementById("mUName").value = "שם משתמש קצר מדי או לא קיים ";
                document.getElementById("mUName").style.display = "inline";
                flag = false;
            }
            else
                document.getElementById("mUName").style.display = "none";

            
            
            var email = document.getElementById("email").value;
            var size = email.length;
            var atSign = email.indexOf('@');
            var dotSign = email.indexOf('.', atSign);
            var msg = "";
            if (size < 6)
            {
                msg = "כתובת דואל קצרה מידי או לא קיימת ";
                valid = false;
            }
            else if (atSign == -1)
            {
                msg = "סימן @ לא קיים בכתובת ";
                valid = false;
            }
            else if (atSign == -1)
            {
                msg = "כתובת דואל קצרה מידי או לא קדדמת ";
                valid = false;
            }
            else if (atSign != email.lastIndexOf('@'))
            {
                msg = "אסור יותר מ@ אחד בכתובת הדואל.";
                valid = false;
            }
            else if (atSign < 2 || email.lastIndexOf('@') == size - 1)
            {
                msg = "סימן ה@ אינו יכול להיות במקום ראשון או אחרון ";
                valid = false;
            }
            else if (email.indexOf('.') == 0 || email.lastIndexOf('.') == size - 1)
            {
                msg = "נקודה לא יכולה להיות תו ראשון או אחרון בכתובת  ";
                valid = false;
            }
            else if (dotSign - atSign <= 1)
            {
                msg = "נקודה חייבת להיות לפחות 2 תווים אחרי הסימן @";
                valid = false;
            }
            else if (isValidString(email) == false)
            {
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


            var gender = document.getElementsByName("gender");
            var ansChecked = false;
            for (var i = 0; i < gender.length; i++) {
                if (gender[i].checked == true)
                    ansChecked = true;
            }

            if (ansChecked == false) {
                document.getElementById("mGender").value = "לא נבחר מגדר";
                document.getElementById("mGender").style.display = "inline";
                flag = false;
            }
            else
                document.getElementById("mGender").style.display = "none";

            var password = document.getElementById("psw1").value;
            if (password.length < 5 || password.length > 8) {
                document.getElementById("mPassword").value = "סיסמא קצרה מידי או ארוכה מידי  (בין 6 ל 8 תווים( ";
                document.getElementById("mPassword").style.display = "inline";
                flag = false;
            }
            else
                document.getElementById("mPassword").style.display = "none";
            var password1 = document.getElementById("psw2").value;
            if (password != password1) {
                document.getElementById("mPassword1").value = "סיסמות לא תואמות";
                document.getElementById("mPassword1").style.display = "inline";
                flag = false;
            } else
                document.getElementById("mPassword1").style.display = "none";
            if (password1.length < 5 || password1.length > 8) {
                document.getElementById("mPassword1").value = " סיסמא קצרה מידי או ארוכה מידי  (בין 6 ל 8 תווים(";
                document.getElementById("mPassword1").style.display = "inline";
                flag = false;
            }
            else
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


        <h1>טופס רישום</h1>
    <form method="post" runat="server" onsubmit ="return chkForm();">
        <table>
            <tr>
                <td>שם משתמש: </td>
                <td><input type="text" id="userName" name="userName" /> </td>
                <td>
                    <input type="text" id="mUName" name="mUName" size="30"
                        style="display:none; background-color:yellow; font-weight:bold;"
                            disabled="disabled"/>
                </td>
            </tr>
            <tr>
                <td>שם פרטי: </td>
                <td><input type="text" id="firstName" name="firstName" /></td>
                <td>
                    <input type="text" id="mfName" name="mfName" size="30"
                        style="display:none; background-color:yellow; font-weight:bold;"
                            disabled="disabled"/>
                </td>
            </tr> 
            <tr>
                <td>שם משפחה: </td>
                <td><input type="text" id="familyName" name="familyName" /> </td>
                <td>
                    <input type="text" id="mlName" name="mlName" size="30"
                        style="display:none; background-color:yellow; font-weight:bold;"
                            disabled="disabled"/>
                </td>
            </tr>
            <tr>
                <td>כתובת דוא"ל: </td>
                <td><input type="text" id="email" name="email" /> </td>
                <td><input type="text" id="mMail" name="mMail" size="30"
                        style="display:none; background-color:yellow; font-weight:bold;"
                            disabled="disabled"/>
                </td>
            </tr>
            <tr>
                <td>סיסמא: </td>
                <td><input type="password" id="psw1" name="psw1" /> </td>
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
                <td><input type="radio" name="gender" value = "male" >זכר
                    <input type="radio" name="gender" value = "female">נקבה 
                    <input type="radio" name="gender" value = "other">אחר 
                </td>
                <td>
                    <input type="text" id="mGender" name="mGender" size="30"
                        style="display:none; background-color:yellow; font-weight:bold;"
                            disabled="disabled"/>
                </td>
            </tr>
            <tr>
                <td> מספר טלפון : </td>
                <td style="direction:ltr">
                    <select name="prefix" id="prefix">
                        <option value="choose">קידומת</option>
                        <option value="050">050</option>
                        <option value="052">052</option>
                        <option value="053">053</option>
                        <option value="054">054</option>
                        <option value="055">055</option>
                        <option value="057">057</option>
                        <option value="058">058</option>
                        <option value="02">02</option>
                        <option value="03">03</option>
                        <option value="04">04</option>
                        <option value="08">08</option>
                        <option value="09">09</option>
                        <option value="077">077</option>
                    </select>
                    <input type="text" id="phoneNum" name="phoneNum" />
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
                    <input type = "checkbox" name = "genre" value = "action" />פעולה
                    <input type = "checkbox" name = "genre" value = "comedy" />קומדיה
                    <input type = "checkbox" name = "genre" value = "drama" />דראמה
                    <input type = "checkbox" name = "genre" value = "crime" />פשע
                    <input type = "checkbox" name = "genre" value = "animetion" />אנימציה

                </td>
                <td>
                    <input type="text" id="mGenre" name="mGenre" size="30"
                        style="display:none; background-color:yellow; font-weight:bold;"
                            disabled="disabled"/>
                </td>
            </tr>

            <tr>
                <td colspan="3" style="text-align:center">
                    <input type="submit" name="submit" value=" שלח " />
                </td>
            </tr>
        </table>
        <br/><br />
        <% = st %>
        <% = msgSent %>
        <% = sqlMsg %>
    </form>






</asp:Content>
