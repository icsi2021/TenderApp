<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WebForm4.aspx.cs" Inherits="TenderApp.WebForm4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<table>
<tr><td>Reference No.</td><td>
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    </td><td>Email</td><td>
    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
    </td><td>Mobile</td><td>
    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
    </td>
    <td>
        <asp:Button ID="Button1" runat="server" Text="Generate Receipt" /></td>
    </tr>
      <tr><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>
        <tr><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>
    <tr><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>
<tr><td colspan="7">
    <asp:Image ID="Image1" runat="server" ImageUrl="~/Receipt.png" Width="700px" />
    </td></tr>

</table>
</asp:Content>
