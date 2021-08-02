<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="TenderApp.ChangePassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center" width="100%">
         <table>
        <tr><td>Old Password</td><td><b style="color:red;">*</b></td><td>
            <asp:TextBox ID="txtOldPass" runat="server" TextMode="Password" Height="20px" Width="190px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter old password" ControlToValidate="txtOldPass" Display="None" ValidationGroup="c"></asp:RequiredFieldValidator>
            </td></tr>
        <tr><td>New Password</td><td><b style="color:red;">*</b></td><td>
            <asp:TextBox ID="txtNewPass" runat="server" TextMode="Password" Height="20px" Width="190px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please enter new password" ControlToValidate="txtNewPass" Display="None" ValidationGroup="c"></asp:RequiredFieldValidator>
        </td></tr>
        <tr><td>Confirm Password</td><td><b style="color:red;">*</b></td><td>
            <asp:TextBox ID="txtConfirmPass" runat="server" TextMode="Password" Height="20px" Width="190px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please enter new password" ControlToValidate="txtConfirmPass" Display="None" ValidationGroup="c"></asp:RequiredFieldValidator><asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Please enter same password" ControlToCompare="txtNewPass" ControlToValidate="txtConfirmPass" Display="None" ValidationGroup="c"></asp:CompareValidator>
            </td></tr>
        <tr><td colspan="3">
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" ValidationGroup="c" Width="200px" />
        </td></tr>
        <tr><td colspan="3">
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btn_Change" runat="server" Text="Change" BackColor="#22427C" ForeColor="White" OnClick="btn_Change_Click" ValidationGroup="c" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" BackColor="#22427C" ForeColor="White" OnClick="btnCancel_Click" />
            </td></tr>
    </table>
    </div>
   
</asp:Content>
