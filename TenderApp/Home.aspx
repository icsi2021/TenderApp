<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="TenderApp.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

    <div style="margin-left:25%">
            <asp:Image ImageUrl="Styles/logo.JPG" runat="server" ID="Img_Logo"  />
                
            </div>
            <link href="Styles/Site.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    
    <div style="margin-left:30%;">
    <h1 style="color:Blue; font-size:x-large;font-weight:bold;">
                    ICSI Tender Application
                </h1> 
                </div>
    <br />
    <div class="clear hideSkiplink">
            </div>  
    <div>
    
<table style="color:Blue;" align="Center">
<tr>
<td>
    <asp:Button ID="btnAdmin" runat="server" BackColor="Blue" Font-Bold="True" Font-Size="50pt" ForeColor="White" Height="102px" Text="ADMIN" Width="254px" OnClick="btnAdmin_Click" />
&nbsp;</td>
<td>
<asp:Button ID="btnUser" runat="server" Text="BIDDER" onclick="btnUser_Click" Font-Bold="True" BackColor="Blue" ForeColor="White" Width="254px" Font-Size="50pt" Height="102px"/>
</td>
</tr>
</table>
    </div>
    </form>
</body>
</html>
