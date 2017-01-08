<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="CheckOrder.aspx.cs" Inherits="BookStore.CheckOrder" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>Order details</h3>
   
    <div style="width: 631px">
    <asp:Repeater ID="OrderDetails" runat="server">
    <HeaderTemplate>
    <table style="border-bottom: 1px solid black; width:500px" cellpadding="0">
    <tr style="background-color:grey; color:#000000; font-size: large; font-weight: bold; text-align: left">
       <th>Title</th>
       <th>Price</th>
        <th>Quantity</th>
        <th>Amount</th>
    </tr>
    </HeaderTemplate>
   <ItemTemplate>
<tr style="text-align: left">
     <td ><asp:Label ID="lblTitle" Width="170px" runat="server" style="text-align:left;" Text='<%# Eval("Book.Title") %>' /></td>
     <td style="text-align:right"><asp:Label ID="lblQuantity" Width="150px" runat="server" style="text-align: right;"  Text='<%# string.Concat(Eval("Book.Price")," kr") %>' /></td>
     <td  style="text-align:center"><asp:Label   runat="server" Width="150px" Text='<%# Eval("Quantity") %>' /></td>
     <td  style="text-align:right"><asp:Label  Width="170px" runat="server" Text='<%# string.Concat(Eval("Amount")," kr") %>' /></td>
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
    </div>
   <div style="float:left; width: 91%; text-align:right;">
    <asp:Label ID="lblTotal" runat="server" Text="Total Amount:"></asp:Label>
       </div>
     
    <br />
    <asp:Label Width="400px" ID="lblMessage" runat="server"></asp:Label>
  
    
    </asp:Content>
