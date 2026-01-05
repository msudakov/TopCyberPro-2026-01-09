<%@ Language="C#" src="default.cs" Inherits="MyNamespace.MyClass" %>
<head>
<title>SHA-256 Demo</title>
<link rel="icon" type="image/x-icon" href="favicon.png">
</head>
<h1>SHA-256 Cookie Integrity Checker</h1>
<form id="MyForm" runat="server">
	<table class="center">
		<tr>
			<td><b>Cookie data (hex):</b></td>
			<td><asp:textbox id="DataTextBox" style="width: 600px;" runat="server"></asp:textbox></td>
		</tr>
		<tr>
			<td><b>Cookie data (raw):</b></td>
			<td><asp:textbox id="DataTextBoxRaw" ReadOnly="true" style="width: 600px; background-color: lightgrey;" TextMode="MultiLine" Rows="3" runat="server"></asp:textbox></td>
		</tr>
		<tr>
			<td><b>Cookie hash:</b></td>
			<td><asp:textbox id="HashTextBox" style="width: 600px;" runat="server"></asp:textbox></td>
		</tr>
		<tr>
			<td colspan=2 style="padding-top: 10px;"><center><asp:button id="SubmitButton" style="width: 400px; font-size: 20px;" text="Verify Cookie" Onclick="Verify" runat="server"></asp:button></center></td>
		</tr>
	</table>
	
	<asp:label id="MyLabel" runat="server" />
</form>
