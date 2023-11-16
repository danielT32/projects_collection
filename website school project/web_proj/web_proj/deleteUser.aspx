<%@ Page Title="" Language="C#" MasterPageFile="~/masterPage.Master" AutoEventWireup="true" CodeBehind="deleteUser.aspx.cs" Inherits="web_proj.deleteUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>מחיקת משתמש</h1>
    <%=msg %>
    <table style ="border: 1px solid black; margin: 0px auto;">
            <%= st %>
    </table>
</asp:Content>
