<%@ Page Title="" Language="C#" MasterPageFile="~/masterPage.Master" AutoEventWireup="true" CodeBehind="mainpage1.aspx.cs" Inherits="web_proj.page1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Styles/stylesheet.css" rel="stylesheet" />
    <style>
        td{width:150px}
    </style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="koteret">על האתר</h1>
    <p>
        אתר המלצות לסדרות וסרטים מומלצים, עם קישורים לטריילרים.
    </p>
    <div class="kategories">קטגוריות:</div> <br />
    <div class="heads">סרטים</div> <br />
    <table style="margin: 0px auto;">
        <tr style="border:2px">
            <td>
                פעולה,אקשן<br />
                3
            </td>
            <td>
                קומדיה<br />
                1
            </td>
            <td>
                דרמה<br />
                1
            </td>
            <td>
                פשע<br />
                1
            </td>
            <td>
                אנימציה ואנימה<br />
                2
            </td>
        </tr>
    </table> <br />
    <div class="heads">סדרות</div> <br />
    <table style="margin: 0px auto;">
        <tr style="border:2px">
            <td>
                פעולה,אקשן<br />
                2
            </td>
            <td>
                קומדיה<br />
                1
            </td>
            <td>
                דרמה<br />
                2
            </td>
            <td>
                פשע<br />
                2
            </td>
            <td>
                אנימציה<br />
                2
            </td>
        </tr>
    </table>
    

</asp:Content>
