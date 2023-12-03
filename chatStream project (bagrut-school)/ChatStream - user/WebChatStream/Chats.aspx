<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Chats.aspx.cs" Inherits="WebChatStream.Chats"%>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	
    <title>ChatStream</title>
    <style>
		body {
			margin: 0;
			padding: 0;
		}
		#main {
			height: 100vh;
			display: flex;
			flex-direction: column;
			background-color: #1f374d;
			color: white;
			font-size: 1.5rem;
		}
		#header {
			display: flex;
			justify-content: center;
			align-items: flex-end;
			height: 20vh;
			font-size: 4rem;
		}
		#content {
			display: flex;
			flex: 1;
			align-items: center;
			justify-content: center;
		}
		#login-box {
			display: flex;
			flex-direction: column;
			align-items: center;
			background-color: #3D3D3D;
			padding: 2rem;
			border-radius: 1rem;
			box-shadow: 0 0.5rem 1rem rgba(0,0,0,0.5);
		}
		.input {
			display: flex;
			flex-direction: row;
			align-items: center;
			margin: 1rem;
			width: 100%;
			font-size: 2rem;
		}
		.label {
			flex: 1;
			text-align: center;
			font-size: 2.5rem;
		}
		.input-box {
			flex: 2;
			border: none;
			background-color: #375b7a;
			color: white;
			padding: 0.5rem;
			border-radius: 0.5rem;
			font-size: 2rem;
		}
		.button {
			padding: 1rem 2rem;
			font-size: 2rem;
			background-color: #00B1F9;
			color: white;
			border: none;
			border-radius: 0.5rem;
			margin: 1rem;
			box-shadow: 0 0.5rem 1rem rgba(0,0,0,0.5);
			cursor: pointer;
		}
	</style>
	
</head>
<body>

    <form id="form1" runat="server">
        <div id="main">
            <asp:Label ID="Label1" runat="server" Text="Chats:" text-align="center"></asp:Label> <br />
			<div style="overflow:scroll; height:500px;">
				<asp:Panel ID="contentView" name="contentView" runat="server">
				</asp:Panel>
			</div>
			<asp:ScriptManager ID="ScriptManager1" runat="server" />
			<asp:Timer ID="Timer1" OnTick="Timer1_Tick" runat="server" Interval="10000" />
			
			<asp:panel runat="server" name="MessageSendPanel" ID="MessageSendPanel" Visible="false" >
				<asp:TextBox id="messageSentTxt" CssClass="input-box" name="messageSentTxt" runat="server" />
				<asp:Button runat="server" class="button" Text="Send" onclick="Send_Click" />
				<asp:Button runat="server" class="button" Text="Back" onclick="Back_Click" />
			</asp:panel>
			
        </div>
    </form>
</body>
</html>
