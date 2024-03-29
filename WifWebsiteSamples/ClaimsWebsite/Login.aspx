﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Login to the application</title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 125px;
        }
        .style3
        {
            font-size: large;
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="style3">
    
        Windows Identity Foundation - Claims-aware ASP.NET Web Site</div>
    <p>
        &nbsp;</p>
    <asp:Label ID="Label3" runat="server" style="font-weight: 700" 
        Text="Login to the website"></asp:Label>
&nbsp;<br />

    <table class="style1">
        <tr>
            <td class="style2">
                <asp:Label ID="Label1" runat="server" Text="User name"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtUserName" runat="server">Adam Carter</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style2">
                <asp:Label ID="Label2" runat="server" Text="Password"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="148px">password</asp:TextBox>
            </td>
        </tr>
    </table>
    <p>
        <asp:Button ID="btnSubmit" runat="server" Text="Submit" Height="24px" 
            onclick="btnSubmit_Click" />
    </p>
    </form>
<p>Note: You can enter any non-empty user name. The password field 
        is optional.</p>
</body>
</html>

