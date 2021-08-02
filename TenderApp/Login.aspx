<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TenderApp.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type = "text/javascript" >
        function preventBack() { window.history.forward(); }

        setTimeout("preventBack()", 0);
        sessionStorage.clear();
        window.onunload = function () { null }; 
   
</script>
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
                <asp:Menu ID="NavigationMenu" runat="server" CssClass="menu" EnableViewState="false" IncludeStyleBlock="false" Orientation="Horizontal">
                    <Items>
                        <asp:MenuItem NavigateUrl="~/Home.aspx" Text="Home"/>
                                         
                    </Items>
                </asp:Menu>
            </div>  
    <div style="margin-left:500px;">
    <asp:Label ID="lblMsg" runat="server" Text ="" Visible="false" ForeColor="Red" Font-Size="Larger"></asp:Label>
    <p style="color:Blue;">
        Please enter your User Id and Password.        
    </p>

<table style="color:Blue;">
<tr>
<td>
    User Id:
</td>
<td>
<asp:TextBox ID="txtUserName" runat="server"/>
<asp:RequiredFieldValidator ID="rfvUser" ErrorMessage="Please enter user id" 
        ControlToValidate="txtUserName" runat="server" ForeColor="Red" />
</td>
</tr>
<tr>
<td>
Password:
</td>
<td>
<asp:TextBox ID="txtPWD" runat="server" TextMode="Password"/>

</td>
</tr>
<tr>
<td>
</td>
<td align="left">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnSubmit" runat="server" Text="Submit" onclick="btnSubmit_Click" BackColor="Blue" ForeColor="White" Width="60px"/>
</td>
</tr>
</table>
    </div>
    </form>
</body>
</html>
