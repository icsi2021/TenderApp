<%@ Page Title="" Language="C#" MasterPageFile="~/SiteUser.Master" AutoEventWireup="true" CodeBehind="TenderUser.aspx.cs" Inherits="TenderApp.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentUser" runat="server">
    <style type="text/css">
        .auto-style1 {
            height: 21px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentUser" runat="server">
    <table width="100%">
         <tr><td align="center" colspan="4">
   
             <b>Bidder's Entry Form</b></td></tr>
<tr><td colspan="4">
    <asp:Label ID="Label1" runat="server" BorderStyle="Solid" 
        style="font-weight: 700" Text="Tender/RFP/EOI/RFQ closing/extended date time"></asp:Label>
    </td></tr>
    <tr><td>
   
    </td><td></td><td></td><td></td></tr>
<tr><td>Tender/RFP/EOI/RFQ Name*</td><td>
    <asp:DropDownList ID="ddlTenderName" runat="server" Width="181px" Height="20px">
        
    </asp:DropDownList>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlTenderName" ErrorMessage="*" ForeColor="#990000"></asp:RequiredFieldValidator>
    </td><td>Fees Type*</td><td>
    <asp:DropDownList ID="ddlFeeType" runat="server" Height="20px" Width="181px" AutoPostBack="True" OnSelectedIndexChanged="ddlFeeType_SelectedIndexChanged">
        <asp:ListItem>----Select----</asp:ListItem>
        <asp:ListItem Value="F">Fees</asp:ListItem>
        <asp:ListItem Value="A">EMD</asp:ListItem>
    </asp:DropDownList>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlFeeType" ErrorMessage="*" ForeColor="#990000"></asp:RequiredFieldValidator>
    </td></tr>
<tr><td>Bidder’s Business Name</td><td>
    <asp:TextBox ID="txtBusinessName" runat="server" Width="180px" Height="20px"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtBusinessName" ErrorMessage="*" ForeColor="#990000" ValidationGroup="r"></asp:RequiredFieldValidator>
    </td><td>Contact Person*</td><td>
    <asp:TextBox ID="txtContactPerson" runat="server" Width="180px" Height="20px"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtContactPerson" ErrorMessage="*" ForeColor="#990000" ValidationGroup="r"></asp:RequiredFieldValidator>
    </td></tr>
<tr><td>Address.*</td><td>
    <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" Width="179px"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtAddress" ErrorMessage="*" ForeColor="#990000" ValidationGroup="r"></asp:RequiredFieldValidator>
    </td><td>Email.*</td><td>
    <asp:TextBox ID="txtEmail" runat="server" Width="180px" Height="20px" TextMode="Email"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtEmail" ErrorMessage="*" ForeColor="#990000" ValidationGroup="r"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail" ErrorMessage="*" ForeColor="#990000" SetFocusOnError="True" ToolTip="Please enter valid email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="r"></asp:RegularExpressionValidator>
    </td></tr>
<tr><td>GST No.*</td><td>
    <asp:TextBox ID="txtGSTNo" runat="server" Width="180px" Height="20px"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtGSTNo" ErrorMessage="*" ForeColor="#990000" ValidationGroup="r"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtGSTNo" ErrorMessage="*" ForeColor="#990000" ValidationExpression="^[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[1-9A-Z]{1}Z[0-9A-Z]{1}$" ValidationGroup="r"></asp:RegularExpressionValidator>
    </td><td>Mobile No.*</td><td>
    <asp:TextBox ID="txtMobile" runat="server" Width="180px" Height="20px" TextMode="Number"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtMobile" ErrorMessage="*" ValidationGroup="r"></asp:RequiredFieldValidator>
    </td></tr>
<tr><td>PAN No.*</td><td>
    <asp:TextBox ID="txtPAN" runat="server" Width="180px" Height="20px"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtPAN" ErrorMessage="*" ForeColor="#990000" ValidationGroup="r"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtPAN" ErrorMessage="*" ForeColor="#990000" ValidationExpression="^[a-zA-Z0-9]{10}$" ValidationGroup="r"></asp:RegularExpressionValidator>
    </td><td colspan="2">* All fields are mandatory</td></tr>
        <tr><td align="center" colspan="4" class="auto-style1"><asp:Label ID="lblAmt" runat="server"></asp:Label>
            </td></tr>
        <tr><td align="center" colspan="4" class="auto-style1"></td></tr>
<tr><td align="center" colspan="4">
    <asp:Button ID="btnPayment" runat="server" Text="Make Payment" BackColor="#00458A" ForeColor="White" Height="30px" OnClick="btnPayment_Click" ValidationGroup="r" />
    </td></tr>
<tr><td align="center" colspan="4">
    &nbsp;</td></tr>
<tr><td align="center" colspan="4">
    &nbsp;</td></tr>
</table>
</asp:Content>
