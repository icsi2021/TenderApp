<%@ Page Title="" Language="C#" MasterPageFile="~/SiteUser.Master" AutoEventWireup="true" CodeBehind="BidderEntryForm.aspx.cs" Inherits="TenderApp.BidderEntryForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentUser" runat="server">
     <style type="text/css">
        .auto-style1 {
            height: 21px;
        }
         .auto-style2 {
             height: 33px;
         }
       
    .uppercase
    {
    text-transform: uppercase;
    }
             .valsummary ul
{
display: none;
visibility: hidden;
}
    </style>
    <script type="text/javascript">
        function isNumber(evt) {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                return false;

            return true;
        }
       
        function ValidateCheckBox(sender, args) {
            if (document.getElementById("<%=chkdisclaimer.ClientID %>").checked == true) {
                args.IsValid = true;
            } else {
                args.IsValid = false;
            }
        }
  
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentUser" runat="server">
     <table width="100%">
         <tr><td align="center" colspan="4">
   
             <b>Bidder's Entry Form</b></td></tr>
          <tr><td>
   
    </td><td></td><td></td><td></td></tr>
<tr><td colspan="4">
    <asp:Label ID="Label1" runat="server" ForeColor="#00458A" Font-Bold="True"></asp:Label>
    </td></tr>
    <tr><td>
    </td><td></td><td></td><td></td></tr>
          <tr><td>
   
    </td><td></td><td></td><td></td></tr>
<tr><td>Tender/RFP/EOI/RFQ Name<b style="color:red;">*</b></td><td>
    <asp:DropDownList ID="ddlTenderName" runat="server" Width="181px" Height="20px" OnSelectedIndexChanged="ddlTenderName_SelectedIndexChanged" AutoPostBack="True">
        
    </asp:DropDownList>
    <asp:RequiredFieldValidator ID="r1" runat="server" ControlToValidate="ddlTenderName" ErrorMessage="Please select Tender/RFP/EOI/RFQ Name" ForeColor="#990000" Display="None" InitialValue="----Select----" ValidationGroup="r"></asp:RequiredFieldValidator>
    </td>
    <td>Tender/RFP/EOI/RFQ No.<b style="color:red;">*</b></td><td>
    <asp:DropDownList ID="ddlTenderNo" runat="server" Width="181px" Height="20px">
        
    </asp:DropDownList>
    <asp:RequiredFieldValidator ID="r34" runat="server" ControlToValidate="ddlTenderNo" ErrorMessage="Please select Tender/RFP/EOI/RFQ No" ForeColor="#990000" Display="None" InitialValue="----Select----" ValidationGroup="r"></asp:RequiredFieldValidator>
    </td>
    </tr>
<tr>
    <td>Fees Type<b style="color:red;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; *</b></td><td>
    <asp:DropDownList ID="ddlFeeType" runat="server" Height="20px" Width="181px" AutoPostBack="True" OnSelectedIndexChanged="ddlFeeType_SelectedIndexChanged">
        <asp:ListItem>----Select----</asp:ListItem>
        <asp:ListItem Value="F">Tender fee</asp:ListItem>
        <asp:ListItem Value="A">Earnest Money Deposit (EMD)</asp:ListItem>
    </asp:DropDownList>
    <asp:RequiredFieldValidator ID="r9" runat="server" ControlToValidate="ddlFeeType" ErrorMessage="Please select Fees Type" ForeColor="#990000" Display="None" InitialValue="----Select----" ValidationGroup="r"></asp:RequiredFieldValidator>
    </td>
    <td>Bidder’s Business Name<b style="color:red;">&nbsp;&nbsp;&nbsp;&nbsp;  *</b></td><td>
    <asp:TextBox ID="txtBusinessName" runat="server" Width="180px" Height="20px"  ValidationGroup="r" CssClass="uppercase" AutoCompleteType="Disabled"></asp:TextBox>
    <asp:RequiredFieldValidator ID="r2" runat="server" ControlToValidate="txtBusinessName" ErrorMessage="Please enter Bidder’s Business Name" ForeColor="#990000" ValidationGroup="r" Display="None"></asp:RequiredFieldValidator>
    </td></tr>
<tr>
    <td>Contact Person<b style="color:red;">*</b></td><td>
    <asp:TextBox ID="txtContactPerson" runat="server" Width="180px" Height="20px"  ValidationGroup="r" CssClass="uppercase" AutoCompleteType="Disabled"></asp:TextBox>
    <%--<asp:RegularExpressionValidator ID="r5" runat="server" ControlToValidate="txtContactPerson" Display="None" ErrorMessage="Please enter Contact Person" ValidationExpression="[a-zA-Z ]*$" ValidationGroup="r"></asp:RegularExpressionValidator>--%>
    <asp:RequiredFieldValidator ID="r8" runat="server" ControlToValidate="txtContactPerson" ErrorMessage="Please enter Contact Person" ForeColor="#990000" ValidationGroup="r" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>
    </td>
    <td>Address<b style="color:red;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; *</b></td><td>
    <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" Width="179px" ValidationGroup="r"></asp:TextBox>
    <asp:RequiredFieldValidator ID="r3" runat="server" ControlToValidate="txtAddress" ErrorMessage="Please enter Address" ForeColor="#990000" ValidationGroup="r" Display="None"></asp:RequiredFieldValidator>
    </td></tr>
<tr>
    <td>Email Id<b style="color:red;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; *</b></td><td>
    <asp:TextBox ID="txtEmail" runat="server" Width="180px" Height="20px" TextMode="Email" MaxLength="50" AutoCompleteType="Disabled"></asp:TextBox><br />
        <asp:Label ID="Label3" runat="server" ForeColor="#990000"></asp:Label>
    <asp:RequiredFieldValidator ID="r7" runat="server" ControlToValidate="txtEmail" ErrorMessage="Please enter Email ID" ForeColor="#990000" ValidationGroup="r" Display="None"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="re1" runat="server" ControlToValidate="txtEmail" ErrorMessage="Please enter Valid Email ID" ForeColor="#990000" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="r" Display="None"></asp:RegularExpressionValidator>
    </td>
    <td>GST No.<b style="color:red;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; *</b></td><td>
    <asp:TextBox ID="txtGSTNo" runat="server" Width="180px" Height="20px" MaxLength="15" ValidationGroup="r" CssClass="uppercase" AutoCompleteType="Disabled"></asp:TextBox>
    <asp:RequiredFieldValidator ID="r4" runat="server" ControlToValidate="txtGSTNo" ErrorMessage="Please enter GST No" ForeColor="#990000" ValidationGroup="r" Display="None"></asp:RequiredFieldValidator><br />
    <asp:RegularExpressionValidator ID="re2" runat="server" ControlToValidate="txtGSTNo" ErrorMessage="Please enter valid GST No. ex.(18AABCT3518Q1Z6)"
         ForeColor="#990000" ValidationExpression="^[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[1-9A-Z]{1}Z[0-9A-Z]{1}$" ValidationGroup="r"></asp:RegularExpressionValidator>
    </td></tr>
<tr>
    <td>Mobile No.<b style="color:red;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; *</b></td><td>
    <asp:TextBox ID="txtMobile" runat="server" Width="180px" Height="20px" MaxLength="10" ValidationGroup="r" onkeypress="javascript:return isNumber(event)" AutoCompleteType="Disabled"></asp:TextBox>
    <asp:Button ID="btn_otp" runat="server" BackColor="#00458A" ForeColor="White" OnClick="btn_otp_Click" Text="Send OTP" Width="73px" />
    <asp:RequiredFieldValidator ID="r6" runat="server" ControlToValidate="txtMobile" ErrorMessage="Please enter Mobile No" ValidationGroup="r" Display="None"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="re4" runat="server" ControlToValidate="txtMobile" Display="None" ErrorMessage="Please enter valid Mobile No" ValidationExpression="[0-9]{10}" ValidationGroup="r"></asp:RegularExpressionValidator>
   <br /> <asp:Label ID="lblMobile" runat="server" ForeColor="#990000"></asp:Label>
    </td>
    <td class="auto-style2">PAN No.<b style="color:red;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; *</b></td><td class="auto-style2">
    <asp:TextBox ID="txtPAN" runat="server" Width="180px" Height="20px" MaxLength="10" ValidationGroup="r" CssClass="uppercase" AutoCompleteType="Disabled" ></asp:TextBox>
    <asp:RequiredFieldValidator ID="re5" runat="server" ControlToValidate="txtPAN" ErrorMessage="Please enter PAN No." ForeColor="#990000" ValidationGroup="r" Display="None"></asp:RequiredFieldValidator>
    <br/><asp:RegularExpressionValidator ID="re3" runat="server" ControlToValidate="txtPAN" ErrorMessage="Please enter valid PAN No. ex.(ABCDE1234Z)" 
        ForeColor="#990000" ValidationExpression="[A-Za-z]{5}\d{4}[A-Za-z]{1}" ValidationGroup="r"></asp:RegularExpressionValidator>
    </td></tr>
         <tr><td>Enter OTP <b style="color:red;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; *</b></td><td> <asp:TextBox ID="txtotp" runat="server" Height="20px" OnTextChanged="txtotp_TextChanged" onkeypress="javascript:return isNumber(event)" ValidationGroup="r" Width="180px" MaxLength="4" AutoPostBack="True" AutoCompleteType="Disabled"></asp:TextBox>
    <asp:RequiredFieldValidator ID="r11" runat="server" ControlToValidate="txtotp" Display="None" ErrorMessage="Please enter OTP" ValidationGroup="r"></asp:RequiredFieldValidator>
   <br /> <asp:Label ID="lblOtp" runat="server" ForeColor="#990000"></asp:Label>
    </td><td></td></tr>
         <tr><td class="auto-style1"></td><td class="auto-style1"></td><td class="auto-style1"></td><td class="auto-style1"><b style="color:red;">* All fields are mandatory</b></td></tr>
         <tr><td>
   
    </td><td></td><td></td><td></td></tr>
          <tr><td class="auto-style1">
   
    </td><td class="auto-style1"></td><td class="auto-style1"></td><td class="auto-style1"></td></tr>
         <tr><td align="center" colspan="4" class="auto-style1">
            <asp:Label ID="lblPayAble" BackColor="#00458A" ForeColor="White" runat="server"></asp:Label><asp:Label ID="lblAmt"  BackColor="#00458A" ForeColor="White" runat="server"></asp:Label>
            </td></tr>
          <tr><td>
   
    </td><td></td><td></td><td></td></tr>
          <tr><td>
   
    </td><td></td><td></td><td></td></tr>
      
          <tr><td colspan="4"> <p style="text-align:justify"><b>Disclaimer:</b> If the bidder cannot complete their financial transaction successfully due to any reason or ICSI does not receive payment of tender fees and EMD in its accounts, ICSI cannot be made liable for such unsuccessful transaction. Further, just by mere payment of tender fees and EMD by the bidder does not guarantee a bidder that its bid has been accepted / qualified as per tender terms. The Institute also reserves its right to cancel this tender/RFP/EOI/RFQ or extend the due date of receipt of bid(s) without assigning any reason thereof which will be notified suitably by the Institute. Cancellation of tender and rules of refund of tender fees and EMD is tender specific and the details thereof to be referred from respective tender documents. The notice of changes in the bid document will be notified in the Institute’s website which will be binding on all the Bidders.</p></td></tr>
          <tr><td colspan="4"></td></tr>
           <tr><td colspan="4">
            <b style="color:red;">*</b>
            <asp:CheckBox ID="chkdisclaimer" runat="server" Text="I agree and accept" ValidationGroup="r" />
                
           <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Please check Disclaimer" ClientValidationFunction = "ValidateCheckBox" ValidationGroup="r" ForeColor="Red"></asp:CustomValidator>
            </td></tr>
<tr><td align="center" colspan="4">
    <asp:Button ID="btnPayment" runat="server" Text="Make Payment" BackColor="#00458A" ForeColor="White" Height="30px" OnClick="btnPayment_Click" ValidationGroup="r" />
    </td></tr>
<tr><td  colspan="4">
    <asp:ValidationSummary ID="ValidationSummary1"  runat="server" Font-Bold="True" ForeColor="Red" ValidationGroup="r" CssClass="valsummary" HeaderText="Please fill the all  mandatory fields" />
    </td></tr>
<tr><td align="center" colspan="4">
    &nbsp;</td></tr>
</table>
</asp:Content>
