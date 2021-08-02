<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TenderEntryForm.aspx.cs" Inherits="TenderApp.WebForm2" EnableEventValidation="false" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
   
    <style type="text/css">
        .style1
        {
            height: 26px;
        }
        .auto-style1 {
            height: 21px;
        }
        .auto-style2 {
            height: 55px;
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
        .auto-style3 {
            height: 38px;
        }
        .auto-style4 {
            height: 50px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   
    <%-- <asp:DropDownList ID="ddlExtendDate" runat="server">
        <asp:ListItem>---Date---</asp:ListItem>
    </asp:DropDownList>
    <asp:DropDownList ID="ddlExtendTime" runat="server">
        <asp:ListItem>--Time--</asp:ListItem>
    </asp:DropDownList>--%>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
     <link href="Scripts/CSS.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/Extension.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.0.0/jquery.min.js"></script>
   <script type="text/javascript">
       function fun_AllowDotEMD(txt) {

           var amount = document.getElementById('MainContent_txtEMD').value;
           // var res = amount.replace(/\D/g, "").replace(/\B(?=(\d{2})+(?!\d))/g, ",");
           // alert(res);
           document.getElementById('MainContent_txtEMD').value = amount + ".00";
       }
       function fun_AllowOnlyAmount(txt) {
           if (event.keyCode > 47 && event.keyCode < 58 || event.keyCode == 46 || event.keyCode == 44) {
               var txtbx = document.getElementById('MainContent_txtEMD');
               var amount = document.getElementById('MainContent_txtEMD').value;
               var x = amount;
               x = x.toString();
               //var lastThree = x.substring(x.length - 3);
               //var otherNumbers = x.substring(0, x.length - 3);
               //if (otherNumbers != '')
               //   lastThree = ',' + lastThree;
               amount = amount.split(".")[0]
               var res = amount.replace(/\D/g, "").replace(/\B(?=(\d{2})+(?!\d))/g, ",");
               // alert(res);
               document.getElementById('MainContent_txtEMD').value = res;
               var present = 0;
               var count = 0;

               if (amount.indexOf(".", present) || amount.indexOf(".", present + 1));
               {
                   // alert('0');
               }

               /*if(amount.length==2)
               {
                 if(event.keyCode != 46)
                 return false;
               }*/
               do {
                   present = amount.indexOf(".", present);
                   if (present != -1) {
                       count++;
                       present++;
                   }
               }
               while (present != -1);
               if (present == -1 && amount.length == 0 && event.keyCode == 46) {
                   event.keyCode = 0;
                   //alert("Wrong position of decimal point not  allowed !!");
                   return false;
               }
               if (present == -1 && amount.length == 0 && event.keyCode == 44) {
                   event.keyCode = 0;
                   //alert("Wrong position of decimal point not  allowed !!");
                   return false;
               }
               if (count >= 1 && event.keyCode == 46) {

                   event.keyCode = 0;
                   //alert("Only one decimal point is allowed !!");
                   return false;
               }
               if (count >= 1 && event.keyCode == 44) {

                   event.keyCode = 0;
                   //alert("Only one decimal point is allowed !!");
                   return false;
               }
               if (count == 1) {
                   var lastdigits = amount.substring(amount.indexOf(".") + 1, amount.length);
                   if (lastdigits.length >= 2) {
                       //alert("Two decimal places only allowed");
                       event.keyCode = 0;
                       return false;
                   }
               }

               return true;
           }
           else {
               event.keyCode = 0;
               //alert("Only Numbers with dot allowed !!");
               return false;
           }

       }
       function fun_AllowDot(txt) {

           var amount = document.getElementById('MainContent_txtCost').value;
          // var res = amount.replace(/\D/g, "").replace(/\B(?=(\d{2})+(?!\d))/g, ",");
           // alert(res);
           document.getElementById('MainContent_txtCost').value = amount+".00";
       }

       function fun_AllowOnlyAmountAndDot(txt) {
           if (event.keyCode > 47 && event.keyCode < 58 || event.keyCode == 46 || event.keyCode == 44) {
               var txtbx = document.getElementById('MainContent_txtCost');
               var amount = document.getElementById('MainContent_txtCost').value;
               amount = amount.split(".")[0]
              // document.getElementById('MainContent_txtCost').value = amount;
               //amount = document.getElementById('MainContent_txtCost').value;
              // var x = amount;
              // x = x.toString();
              // //var lastThree = x.substring(x.length - 3);
              // //var otherNumbers = x.substring(0, x.length - 3);
              // //if (otherNumbers != '')
              //  //   lastThree = ',' + lastThree;
              
               var res =amount.replace(/\D/g, "").replace(/\B(?=(\d{2})+(?!\d))/g, ",");
              // alert(res);
               document.getElementById('MainContent_txtCost').value = res;
               var present = 0;
               var count = 0;

               if (amount.indexOf(".", present) || amount.indexOf(".", present + 1));
               {
                   // alert('0');
               }

               /*if(amount.length==2)
               {
                 if(event.keyCode != 46)
                 return false;
               }*/
               do {
                   present = amount.indexOf(".", present);
                   if (present != -1) {
                       count++;
                       present++;
                   }
               }
               while (present != -1);
               if (present == -1 && amount.length == 0 && event.keyCode == 46) {
                   event.keyCode = 0;
                   //alert("Wrong position of decimal point not  allowed !!");
                   return false;
               }
               if (present == -1 && amount.length == 0 && event.keyCode == 44) {
                   event.keyCode = 0;
                   //alert("Wrong position of decimal point not  allowed !!");
                   return false;
               }
               if (count >= 1 && event.keyCode == 46) {

                   event.keyCode = 0;
                   //alert("Only one decimal point is allowed !!");
                   return false;
               }
               if (count >= 1 && event.keyCode == 44) {

                   event.keyCode = 0;
                   //alert("Only one decimal point is allowed !!");
                   return false;
               }
               if (count == 1) {
                   var lastdigits = amount.substring(amount.indexOf(".") + 1, amount.length);
                   if (lastdigits.length >= 2) {
                       //alert("Two decimal places only allowed");
                       event.keyCode = 0;
                       return false;
                   }
               }
         
               return true;
           }
           else {
               event.keyCode = 0;
               //alert("Only Numbers with dot allowed !!");
               return false;
           }

       }
       isNumberKey = function (evt) {
           var charCode = (evt.which) ? evt.which : evt.keyCode;
           if (charCode != 46 && charCode > 31
               && (charCode < 48 || charCode > 57)) {
               return false;
           }
           else {
               return true;

              
           }
          
       }

       //function isNumber(evt) {
       //    var iKeyCode = (evt.which) ? evt.which : evt.keyCode
       //    if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57)&&iKeyCode!=188)
       //        return false;

       //    return true;
       //}
       //var textInput = document.getElementById("txtEMD");

       //textInput.addEventListener("keyup", function (evt) {
       //    var n = parseInt(this.value.replace(/\D/g, ''), 10);
       //    textInput.value = n.toLocaleString();
       //}, false);
        function dateselect(ev) {
            var calendarBehavior1 = $find("Calendar1");
            var d = calendarBehavior1._selectedDate;
            var now = new Date();
            calendarBehavior1.get_element().value = d.format("dd/MM/yyyy") + " " + now.format("HH:mm:ss")
        }
        function dateselect1(ev) {

            var ObjEventStartDate1 = document.getElementById('MainContent_txtPublishDateTime');
            var ObjEventEndDate1 = document.getElementById('MainContent_txtCloseDateTime');
            
            var stDt=ObjEventStartDate1.value.split(" ")[0];
            var cldt = ObjEventEndDate1.value.split(" ")[0];
            var calendarBehavior1 = $find("txtCloseDateTime_CalendarExtender");
            var d = calendarBehavior1._selectedDate;
            var now = new Date();
            calendarBehavior1.get_element().value = d.format("dd/MM/yyyy") + " " + now.format("HH:mm:ss")
            stDt = new Date(stDt.split('/')[2], stDt.split('/')[1], stDt.split('/')[0]);
            cldt = new Date(cldt.split('/')[2], cldt.split('/')[1], cldt.split('/')[0]);
            if (stDt > cldt || cldt < stDt) {
             
                document.getElementById('MainContent_txtCloseDateTime').value = "";
            }
           
            
            
        }
        function dateselect2(ev) {
            var ObjEventEndDate1 = document.getElementById('MainContent_txtCloseDateTime');
            var ObjEventExtDate = document.getElementById('MainContent_txtExtendDateTime');
            var calendarBehavior1 = $find("txtExtendDateTime_CalendarExtender");
            var cldat=ObjEventEndDate1.value.split(" ")[0] ;
            var extdt = ObjEventExtDate.value.split(" ")[0];
            var d = calendarBehavior1._selectedDate;
            var now = new Date();
            calendarBehavior1.get_element().value = d.format("dd/MM/yyyy") + " " + now.format("HH:mm:ss")
            cldat = new Date(cldat.split('/')[2], cldat.split('/')[1], cldat.split('/')[0]);
            extdt = new Date(extdt.split('/')[2], extdt.split('/')[1], extdt.split('/')[0]);
            //cldt = new Date(cldt.split('/')[2], cldt.split('/')[1], cldt.split('/')[0]);
            if (cldat > extdt || extdt < cldat) {
                document.getElementById('MainContent_txtExtendDateTime').value = "";
            }
            

        }
    </script>
    <script type="text/jscript" src="Scripts/EventMaster.js"></script>
    <script src="Scripts/geretedDate.js"></script>
<table width="100%">
     <tr><td align="center" colspan="5">
   
        <b> Tender Entry Form</b></td></tr>
<tr><td>Tender/RFP/EOI/RFQ No<b style="color:red">&nbsp; * </b></td><td>
    <asp:TextBox ID="txtTenderNo" runat="server" Width="163px" Height="20px" ValidationGroup="p" CssClass="uppercase" AutoPostBack="True" OnTextChanged="txtTenderNo_TextChanged"></asp:TextBox>
    <asp:RequiredFieldValidator ID="rfvTenderNo" runat="server" ErrorMessage="Please enter Tender/RFP/EOI/RFQ No"  ControlToValidate="txtTenderNo" ForeColor="#990000" ValidationGroup="p" Display="None"></asp:RequiredFieldValidator>
    </td>
    <td></td>
    <td>Tender/RFP/EOI/RFQ Name<b style="color:red">*</b></td><td>
    <asp:TextBox ID="txtName" runat="server" Width="163px" Height="20px" ValidationGroup="p" CssClass="uppercase"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvTenderName" runat="server" ErrorMessage="Please enter Tender/RFP/EOI/RFQ Name" ControlToValidate="txtName" ForeColor="#990000" ValidationGroup="p" Display="None"></asp:RequiredFieldValidator>
    </td></tr>
<tr><td class="auto-style4">Publishing Date Time<b style="color:red">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; *</b></td><td class="auto-style4">
    <%-- <asp:DropDownList ID="ddlExtendDate" runat="server">
        <asp:ListItem>---Date---</asp:ListItem>
    </asp:DropDownList>
    <asp:DropDownList ID="ddlExtendTime" runat="server">
        <asp:ListItem>--Time--</asp:ListItem>
    </asp:DropDownList>
        onchange="Validation(this);"
        --%>
    <asp:TextBox ID="txtPublishDateTime" runat="server" Width="163px"  Height="20px" TextMode="DateTime" CssClass="disable_past_dates" AutoCompleteType="Disabled"></asp:TextBox>
    <asp:ImageButton ID="imgPopup" ImageUrl="~/1651699-calendar-icon-calendar-png-260_260.png" ImageAlign="Bottom"  Height="20px" Width="20px"
    runat="server" />
      <cc1:CalendarExtender ID="Calendar1" runat="server" PopupButtonID="imgPopup"  BehaviorID="Calendar1"  Enabled="True" TargetControlID="txtPublishDateTime"
         OnClientDateSelectionChanged="dateselect" Format="dd/MM/yyyy HH':'mm':'ss">
    </cc1:CalendarExtender>
   
    <asp:RequiredFieldValidator ID="rfvPublishDate" runat="server" ErrorMessage="Please enter Publishing Date" ControlToValidate="txtPublishDateTime" ForeColor="#990000" ValidationGroup="p" Display="None"></asp:RequiredFieldValidator>
    <asp:Label ID="lblPubdate" runat="server" ForeColor="#990000"></asp:Label>
    </td>
    <td class="auto-style4"></td>
    <td class="auto-style4">Closing Date Time<b style="color:red">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; *</b></td><td class="auto-style4">
    <%-- <asp:DropDownList ID="ddlExtendDate" runat="server">
        <asp:ListItem>---Date---</asp:ListItem>
    </asp:DropDownList>
    <asp:DropDownList ID="ddlExtendTime" runat="server">
        <asp:ListItem>--Time--</asp:ListItem>
    </asp:DropDownList>
        onchange="Validation(this);"
        --%>
    <asp:TextBox ID="txtCloseDateTime" runat="server" Width="163px"  Height="20px" CssClass="disable_past_dates" AutoCompleteType="Disabled"></asp:TextBox>
        <asp:ImageButton ID="ImageButton1" ImageUrl="~/1651699-calendar-icon-calendar-png-260_260.png" ImageAlign="Bottom"  Height="20px" Width="20px"
    runat="server" />
    <cc1:CalendarExtender ID="txtCloseDateTime_CalendarExtender" runat="server" PopupButtonID="ImageButton1" BehaviorID="txtCloseDateTime_CalendarExtender"
         Enabled="True" TargetControlID="txtCloseDateTime" OnClientDateSelectionChanged="dateselect1" Format="dd/MM/yyyy HH':'mm':'ss">
    </cc1:CalendarExtender>
        <asp:RequiredFieldValidator ID="rfvClosingDate" runat="server" ErrorMessage="Please enter Closing Date Time" ControlToValidate="txtCloseDateTime" ForeColor="#990000" ValidationGroup="p" Display="None"></asp:RequiredFieldValidator>
    <asp:Label ID="lblclosedate" runat="server" ForeColor="#990000"></asp:Label>
    </td></tr>
<tr><td class="auto-style2">Tender/RFP/EOI/RFQ Cost<b style="color:red">*</b><br />
    (including GST @18%)
    </td><td class="auto-style2">
    <asp:TextBox ID="txtCost" runat="server" onkeypress="javascript:return fun_AllowOnlyAmountAndDot(event)" onchange="javascript:return fun_AllowDot(event)" Width="163px" Height="20px"  MaxLength="12" AutoCompleteType="Disabled"  ></asp:TextBox>
        &nbsp;
        <asp:Label ID="Label1" runat="server" Text="₹" Font-Bold="True" Font-Size="X-Large"></asp:Label><asp:RequiredFieldValidator ID="rfvCost" runat="server" ErrorMessage="Please enter Tender/RFP/EOI/RFQ Cost" ControlToValidate="txtCost" ForeColor="#990000" ValidationGroup="p" Display="None"></asp:RequiredFieldValidator>
    </td>
    <td class="auto-style2"></td>
    <td class="auto-style2">Extended Date Time</td><td class="auto-style2">
   <%-- <asp:DropDownList ID="ddlExtendDate" runat="server">
        <asp:ListItem>---Date---</asp:ListItem>
    </asp:DropDownList>
    <asp:DropDownList ID="ddlExtendTime" runat="server">
        <asp:ListItem>--Time--</asp:ListItem>
    </asp:DropDownList>
       onchange="ValidationExtend(this);"
       --%>
    <asp:TextBox ID="txtExtendDateTime" runat="server" Width="163px"  TextMode="DateTime" Height="20px" CssClass="disable_past_dates" Enabled="False" AutoCompleteType="Disabled"></asp:TextBox>
        <asp:ImageButton ID="ImageButton2" ImageUrl="~/1651699-calendar-icon-calendar-png-260_260.png" ImageAlign="Bottom"
    runat="server" Height="20px" Width="20px" />
    <cc1:CalendarExtender ID="txtExtendDateTime_CalendarExtender" runat="server"  PopupButtonID="ImageButton2" BehaviorID="txtExtendDateTime_CalendarExtender" Enabled="True" 
        TargetControlID="txtExtendDateTime" OnClientDateSelectionChanged="dateselect2" Format="dd/MM/yyyy HH':'mm':'ss">
    </cc1:CalendarExtender>

    <asp:Label ID="lblextendate" runat="server" ForeColor="#990000"></asp:Label>

    </td></tr>
<tr><td class="style1">Publishing Department<b style="color:red">&nbsp;&nbsp;&nbsp; *</b></td><td class="style1">
    <asp:DropDownList ID="ddlDept" runat="server" Height="25px" Width="164px">
        
    </asp:DropDownList>
   
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlDept" ErrorMessage="Please select Publishing Department" ForeColor="#993300" ValidationGroup="p" Display="None" InitialValue="----Select----"></asp:RequiredFieldValidator>
   
    </td>
    <td></td>
    <td class="style1">EMD Amount<b style="color:red">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; *</b></td><td class="style1">
    <asp:TextBox ID="txtEMD" runat="server" Width="163px" Height="20px" onkeypress="javascript:return fun_AllowOnlyAmount(event)" onchange="javascript:return fun_AllowDotEMD(event)" MaxLength="20" AutoCompleteType="Disabled"></asp:TextBox>
    &nbsp;
    <asp:Label ID="Label2" runat="server" Text="₹" Font-Bold="True" Font-Size="X-Large"></asp:Label>
    
        <asp:RequiredFieldValidator ID="rfvEMD" runat="server" ErrorMessage="Please enter EMD Amount" ControlToValidate="txtEMD" ForeColor="#990000" ValidationGroup="p" Display="None"></asp:RequiredFieldValidator>
    </td></tr>
    <tr><td align="center" colspan="5" class="auto-style3">
        <asp:Label ID="lblMessage" runat="server" ForeColor="Green"></asp:Label><br /><b style="color:red;">* All fields are mandatory</b>
        </td></tr>
       <tr><td colspan="5" class="auto-style1">
        &nbsp;
           <asp:ValidationSummary ID="ValidationSummary1"  runat="server" Font-Bold="True" ForeColor="Red" ValidationGroup="p" CssClass="valsummary" HeaderText="Please fill all the mandatory fields" />
           </td></tr>
<tr><td align="center" colspan="5">
    <asp:Button ID="btnSave_Update" runat="server" Text="Save" ValidationGroup="p" OnClick="btnSave_Update_Click" BackColor="#00458A" ForeColor="White" Height="30px" Width="100px" />
    <asp:Button ID="btnCancel" runat="server" Text="Cancel" BackColor="#00458A" ForeColor="White" Height="30px" Width="100px" OnClick="btnCancel_Click" />
    <asp:Label ID="lblTenderid" runat="server" Text="" Visible="False"></asp:Label>
    </td></tr>
    <tr><td colspan="5">
        &nbsp;</td></tr>
    <tr><td align="right" colspan="5"> <asp:Button ID="Button1" runat="server"  BackColor="#00458A" ForeColor="White" Text="Export  To Excel" OnClick="Button1_Click" /></td></tr>
    <tr align="center"><td colspan="5">
        <asp:Panel ID="Panel1" runat="server" Width="100%" HorizontalAlign="Center">

            <asp:GridView ID="grdv_tenderlistview" runat="server" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="grdv_tenderlistview_PageIndexChanging" OnSelectedIndexChanged="grdv_tenderlistview_SelectedIndexChanged" Width="100%">
                 <Columns>
                  <asp:TemplateField HeaderText="Tender Id" >
                    <ItemTemplate>
                            <asp:Label ID="lblTENDERID" runat="server" Text='<%# Eval("TENDERID")%>'></asp:Label>
                        </ItemTemplate>                  
                    </asp:TemplateField>

                   <asp:TemplateField HeaderText="Tender Name" >
                    <ItemTemplate>
                            <asp:Label ID="lblTENDERNAME" runat="server" Text='<%# Eval("TENDERNAME")%>'></asp:Label>
                        </ItemTemplate>                  
                    </asp:TemplateField>
                  <asp:TemplateField HeaderText="Tender No" >
                        <ItemTemplate>
                            <asp:Label ID="lblTENDERNO" runat="server" Text='<%# Eval("TENDERNO")%>'></asp:Label>
                        </ItemTemplate>                  
                    </asp:TemplateField>                
                      <asp:TemplateField HeaderText="Publishing Date"  >
                        <ItemTemplate>
                            <asp:Label ID="lblPUBLISHINGDATE" runat="server" Text='<%# Eval("PUBLISHINGDATE","{0:dd/MM/yyyy HH:mm tt}")%>' ></asp:Label>
                        </ItemTemplate>                  
                    </asp:TemplateField> 
                      <asp:TemplateField HeaderText="Closing Date" >
                        <ItemTemplate>
                            <asp:Label ID="lblCLOSINGDATE" runat="server" Text='<%# Eval("CLOSINGDATE","{0:dd/MM/yyyy HH:mm tt}")%>'></asp:Label>
                        </ItemTemplate>                  
                    </asp:TemplateField> 
                          <asp:TemplateField HeaderText="Extended Date" >
                        <ItemTemplate>
                            <asp:Label ID="lblExtDATE" runat="server" Text='<%# Eval("EXTENDEDDATE" ,"{0:dd/MM/yyyy HH:mm tt}")%>'></asp:Label>
                        </ItemTemplate>                  
                    </asp:TemplateField> 
                      <asp:TemplateField HeaderText="Tender Fees" >
                        <ItemTemplate>
                            <asp:Label ID="lblTENDERFEES" runat="server" Text='<%# Eval("TENDERFEES")%>'></asp:Label>
                        </ItemTemplate>                  
                    </asp:TemplateField> 
                     <asp:TemplateField HeaderText="Emd Amount" >
                        <ItemTemplate>
                            <asp:Label ID="lblEMDAMT" runat="server" Text='<%# Eval("EMDAMT")%>'></asp:Label>
                        </ItemTemplate>                  
                    </asp:TemplateField> 
                 
                   <asp:CommandField ShowSelectButton="True" SelectText="EDIT" HeaderText="Action"  />

            
        </Columns>      
               <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />  
                    <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />  
                    <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />  
                    <RowStyle BackColor="White" ForeColor="#003399" />  
                    <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />  
                    <SortedAscendingCellStyle BackColor="#EDF6F6" />  
                    <SortedAscendingHeaderStyle BackColor="#0D4AC4" />  
                    <SortedDescendingCellStyle BackColor="#D6DFDF" />  
                    <SortedDescendingHeaderStyle BackColor="#002876" /> 
            </asp:GridView>
        </asp:Panel>
    </td></tr>
</table>

</asp:Content>
