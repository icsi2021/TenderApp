<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaymentHDFC.aspx.cs" Inherits="PwC.ICSI.UI.StudentMemberPages.Payment.PaymentHDFC" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "https://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="https://www.w3.org/1999/xhtml" >
<head>
    <title>ICSINewPaymentGetway</title>
</head>
<body>
   <form action="https://secure.ccavenue.com/transaction/transaction.do?command=initiateTransaction" method="post" target="_top" style="text-align:center;">
    <%--<form id="nonseamless" method="post" target="_top" name="redirect" action="https://test.ccavenue.com/transaction/transaction.do?command=initiateTransaction" >--%>

        <%--<input type="hidden" name="msg" value="<%=str %>" />--%>
        
        <input type="hidden" id="encRequest" name="encRequest" value="<%=strEncRequest%>"/>
        <input type="hidden" name="access_code" id="Hidden1" value="<%=strAccessCode%>"/>

    <div align="center">
        <table border="0" cellpadding="0" cellspacing="0"  style="width: 100%; text-align:left; height: 27px;vertical-align:top;" class="requestheader">
            <tr>
                <td align="center" style="font-weight: bold; color: white; background-color: #003399">
                    <asp:Label ID="Label2" runat="server" CssClass="requestlabel"
                Text="Confirmation / Declaration" Width="400px"></asp:Label>
                </td>
            </tr>
        </table>
        <br />
        <h4 style="color:Red"><%=PayIdAndTranId %></h4>
        <table style="width: 50%; text-align:center; height: 25px; border-left-color: #000000; border-bottom-color: #000000; border-top-style: solid; border-top-color: #000000; border-right-style: solid; border-left-style: solid; border-right-color: #000000; border-bottom-style: solid;">
            <tr>
                <td style="text-align:center; font-size:small; border-bottom-color:Black; border-bottom-style:solid; border-bottom-width:thin;">
                Confirmation / Declaration 
                </td>
            </tr>
            <tr>
                <td  class="formlabe2" style="text-align: left; font-size:small">
                    <ol style="margin-bottom:10px;">
                    <li style="margin-bottom:10px;">Some times the user can not get the print out of the acknowledgement /payment receipt.
                        The reason might be the “Failed Transaction” OR &nbsp;“Acknowledge not generated
                        (exclusively)” due to heavy traffic or network connectivity issues.</li>        
                    <li style="margin-bottom:10px;">
                        <span lang="EN-IN" style="font-size:10.0pt;font-family:&quot;Times New Roman&quot;,&quot;serif&quot;;mso-fareast-font-family:&quot;Times New Roman&quot;;mso-bidi-font-family:Arial;mso-ansi-language:EN-IN;mso-fareast-language:EN-IN;mso-bidi-language:AR-SA">
                            In case the receipt is not available within two working day, the stakeholder should send an email to the Institute at info@icsi.edu with details (Acknowledgment No. , Tender No., Tender Name & Date) 
                        </span> 
                    </li>        
                    <li style="margin-bottom:10px;">
                        <span lang="EN-IN" style="font-size:10.0pt;font-family:&quot;Times New Roman&quot;,&quot;serif&quot;;mso-fareast-font-family:&quot;Times New Roman&quot;;mso-bidi-font-family:Arial;mso-ansi-language:EN-IN;mso-fareast-language:EN-IN;mso-bidi-language:AR-SA">
                            The institute takes approximately 48 hours to reconcile the data with the bank.
                        </span>
                    </li>
                </ol>
                </td>
            </tr>            
            <tr><td></td></tr>
        </table>
        <br />
        <input type="submit" id="btnsumit" value="Proceed To Payment" name="Submit" style="background-color:blue;border:solid;color:white" />
    </div>      
    </form>
</body>
</html>

