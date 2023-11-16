<%@ Page Title="" Language="C#" MasterPageFile="~/masterPage.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="web_proj.login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form name ="loginFrm" id ="loginFrm" method = "post" runat="server">
        <br /><br />
        <table id = "table1" dir ="rtl" align ="center">
            <tr>
                <td>שם משתמש:</td>
                <td><input type= "text" name = "userName" id = "userName" /></td>
            </tr>
            <tr>
                <td>סיסמה:</td>
                <td><input type= "password" name = "psw" id = "psw" /></td>
            </tr>
            <td colspan ="2" align="center">
                <input type = "submit" name = "submit" value ="התחבר" />
            </td>
        </table>
    </form>


</asp:Content>
