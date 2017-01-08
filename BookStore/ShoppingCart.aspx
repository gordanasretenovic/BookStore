<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site1.Master" CodeFile="ShoppingCart.aspx.cs" Inherits="BookStore.ShoppingCart" %>
<%@ MasterType virtualpath="~/Site1.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
     <h3 style="color:Red">Shopping Cart</h3>

    <asp:Repeater id="cartitemcatalog"  runat="server">
         <HeaderTemplate>
        <table style="border-bottom: #000000 1px solid; width:500px" cellpadding="0">
 

<tr style="text-align: left; background-color:#7795BD; color:#000000; font-size: large; font-weight: bold;">
<th>Title</th>
<th>Quantity</th>
    <th></th>
</tr>
</HeaderTemplate>
       
<ItemTemplate>
<tr>
     <td ><asp:Label ID="lblTitle" Width="170px" runat="server" style="text-align:left;" Text='<%# Eval("Book.Title") %>' /></td>
     <td style="text-align:left"><asp:Label ID="lblQuantity" Width="40px" runat="server" style="text-align: right;" Text='<%# Eval("Quantity") %>' /></td>
     <td ><asp:ImageButton ImageUrl="delete.png" width="15px" Height="15px" runat="server" CommandName="Delete" CommandArgument='<%# Eval("Book.ProductId") %>' OnCommand="OnCommand"/></td>
</tr>
</ItemTemplate>

<SeparatorTemplate>
    <tr>
    <td colspan="6"><hr /></td>
    </tr>
</SeparatorTemplate>
<FooterTemplate>
</table>
</FooterTemplate>
        
        </asp:Repeater>
        <asp:Label runat="server" ID="Label1"  Visible="False">TotalAmount:</asp:Label>
                    <asp:Label ID="lblamount" runat="server" Visible="False"></asp:Label>
             
</asp:Content>
