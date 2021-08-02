<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TenderList.aspx.cs" Inherits="TenderApp.WebForm3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .auto-style1 {
            height: 21px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table  width="100%">
          <tr><td colspan="5" align="center"><b>Applied Bidder List</b></td></tr>
          <tr><td colspan="5" align="center"></td></tr>  
        <tr><td colspan="5" align="center"></td></tr>
<tr><td><b>Tender No</b><b style="color:red;">*</b></td><td>
    <asp:DropDownList ID="ddlTenderNo" runat="server" Height="30px" Width="200px">
    </asp:DropDownList>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlTenderNo" ErrorMessage="Please select Tender No" ForeColor="Red" InitialValue="----Select----" ValidationGroup="s"></asp:RequiredFieldValidator>
    </td><td><b>Fee Type</b><b style="color:red;">*</b></td><td>
    <asp:DropDownList ID="ddlFeeType" runat="server" Width="200px" Height="30px">
        <asp:ListItem>----Select----</asp:ListItem>
        <asp:ListItem Value="4100">SALE OF TENDER FORMS</asp:ListItem>
        <asp:ListItem Value="553">EARNEST MONEY REFUNDABLE</asp:ListItem>
    </asp:DropDownList>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlFeeType" ErrorMessage="Please select Fee Type" ForeColor="Red" InitialValue="----Select----" ValidationGroup="s"></asp:RequiredFieldValidator>
    </td><td>
        <asp:Button ID="btnSearch" runat="server" BackColor="#00458A" ForeColor="White" Text="Search" OnClick="btnSearch_Click" Height="30px" ValidationGroup="s" />
    </td></tr>
<tr><td colspan="5" class="auto-style1" align="RIGHT">
    <asp:Button ID="Button1" runat="server" BackColor="#00458A" ForeColor="White" Text="Export  To Excel" OnClick="Button1_Click" />
    </td></tr>
    <tr><td colspan="5"></td></tr>
    <tr><td colspan="5"> <asp:GridView ID="gdvParticipationList" runat="server" AutoGenerateColumns="False" Width="100%">
        <Columns>
                  <asp:TemplateField HeaderText="Tender Id" Visible="false" >
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
                      <asp:TemplateField HeaderText="Publishing Date" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblPUBLISHINGDATE" runat="server" Text='<%# Eval("PUBLISHINGDATE","{0:dd/MM/yyyy HH:mm tt}")%>'></asp:Label>
                        </ItemTemplate>                  
                    </asp:TemplateField> 
             <asp:TemplateField HeaderText="Bidder Farm's Name" >
                        <ItemTemplate>
                            <asp:Label ID="lblFarm" runat="server" Text='<%# Eval("BIDERFARM")%>'></asp:Label>
                        </ItemTemplate>                  
                    </asp:TemplateField> 
             <asp:TemplateField HeaderText="Contact Person" >
                        <ItemTemplate>
                            <asp:Label ID="lblPerson" runat="server" Text='<%# Eval("CONTACTPERSON")%>'></asp:Label>
                        </ItemTemplate>                  
                    </asp:TemplateField> 

              <asp:TemplateField HeaderText="Mobile" >
                        <ItemTemplate>
                            <asp:Label ID="lblMobile" runat="server" Text='<%# Eval("MOBILE")%>'></asp:Label>
                        </ItemTemplate>                  
                    </asp:TemplateField> 
                    <asp:TemplateField HeaderText="Creation Date" >
                        <ItemTemplate>
                            <asp:Label ID="lblCLOSINGDATE" runat="server" Text='<%# Eval("CREATEDATE","{0:dd/MM/yyyy HH:mm tt}")%>'></asp:Label>
                        </ItemTemplate>                  
                    </asp:TemplateField> 
                      <asp:TemplateField HeaderText="Type Of Fee" >
                        <ItemTemplate>
                            <asp:Label ID="lblTENDERFeeType" runat="server" Text='<%# Eval("FEETYPE")%>'></asp:Label>
                        </ItemTemplate>                  
                    </asp:TemplateField> 
                     <asp:TemplateField HeaderText="Amount" >
                        <ItemTemplate>
                            <asp:Label ID="lblAmt" runat="server" Text='<%# Eval("FEEAMT")%>'></asp:Label>
                        </ItemTemplate>                  
                    </asp:TemplateField> 
                 
                   
              
            
        </Columns>
          <EmptyDataTemplate>
        <div align="center">No records found.</div>
    </EmptyDataTemplate>
       <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />  
                    <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />  
                    <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />  
                    <RowStyle BackColor="White" ForeColor="#003399" />  
                    <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />  
                    <SortedAscendingCellStyle BackColor="#EDF6F6" />  
                    <SortedAscendingHeaderStyle BackColor="#0D4AC4" />  
                    <SortedDescendingCellStyle BackColor="#D6DFDF" />  
                    <SortedDescendingHeaderStyle BackColor="#002876" /> 
    </asp:GridView></td></tr>
</table>
   
</asp:Content>
