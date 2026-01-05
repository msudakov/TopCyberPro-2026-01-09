<%@ Language="C#" src="default.cs" Inherits="MyNamespace.MyClass" %>
<head>
<title>AES-256 Demo</title>
<link rel="icon" type="image/x-icon" href="favicon.png">
</head>
<h1>AES-256 Ciphertext Validity Checker</h1>
<form id="MyForm" runat="server">
	<table class="center">
		<tr>
			<td><b>Ciphertext:</b></td>
			<td><asp:textbox id="CipherTextBox" style="width: 600px;" runat="server"></asp:textbox></td>
		</tr>
		<tr>
			<td colspan=2 style="padding-top: 10px;"><center><asp:button id="SubmitButton" style="width: 400px; font-size: 20px;" text="Validate Ciphertext" Onclick="Verify" runat="server"></asp:button></center></td>
		</tr>
	</table>
	<br /><br />
	The provided ciphertext is: <b><asp:label id="MyLabel" runat="server" /></b>
</form>
