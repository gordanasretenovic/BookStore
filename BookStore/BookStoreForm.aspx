<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeFile="BookStoreForm.aspx.cs" Inherits="BookStore.BookStoreForm" %>
<%@ MasterType virtualpath="~/Site1.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
     .search {
         background:url(icons_search.png) right no-repeat;
         background-size: 20px;    
         padding-left: 35px;
         border:1px solid #ccc;
         height: 25px;
}
      .buttonClass
        {
          
          text-align: center;
            color: white; 
  
    text-decoration: none;
    
     border: 2px solid #a1a1a1;
    padding: 3px 10px; 
    background: #a1a1a1;
   
    border-radius: 5px;
         
          font-size:medium;
    
        }
        .buttonClass:hover
        {
           color: black; 
    background-color: white;
    text-decoration: none;
    border: 2px solid black; 
        }
    .auto-style6 {
        width: 100%;
         margin-top: 40px;
    }
    .auto-style7 {
        width: 193px;
       
    }
 </style>   
    <table class="auto-style6">
        <tr>
            <td class="auto-style7">   
    <asp:RadioButtonList ID="rbl" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rbl_SelectedIndexChanged" ForeColor="#FF3300" RepeatDirection="Horizontal">
        <asp:ListItem  Value="Title">Title</asp:ListItem>
        <asp:ListItem Value="Author">Author</asp:ListItem>
        <asp:ListItem>Alla</asp:ListItem>
    </asp:RadioButtonList>
            </td>
            <td>Sök
    <asp:TextBox ID="TextBox1" runat="server" CssClass="search" OnTextChanged="TextBox1_TextChanged" ></asp:TextBox>
            </td>
        </tr>
</table>
    <asp:Repeater id="rootobjectcatalog" runat="server">
       <HeaderTemplate>
            <table style="width:500px">
                <tr><td><h3 style="color:red">Product Information</h3> </td></tr>
              
          </HeaderTemplate>
<ItemTemplate>
    <tr><td>
        <h4>Title: <asp:Label Width="200px" ID="Productnamelabel" runat="server"  
            Text= '<%# Eval("Title") %>' /></h4>
    </td>
  
    <td class="ProductPropertyLabel">Author:
      <asp:Label Width="150px" runat="server" ID="Label" 
                 Text='<%# Eval("Author") %>' />
    </td>
  
  
            <td class="ProductPropertyLabel">Price:
          <asp:Label Width="150px" runat="server" ID="Label3" 
                     Text='<%# string.Concat(Eval("Price")," kr") %>' />
    </td>
  
           <%-- <tr>
            <td class="ProductPropertyLabel">In stock:</td>
     <%-- <td>
      <asp:Label runat="server" ID="Label4" 
                 Text='<%# Eval("InStock") %>' />
    </td>--%>
               <%--</tr>--%>
                <tr>
                 <td>
                     <asp:LinkButton ID="LnkButtion" CssClass="buttonClass" Text="Add to cart"  CommandName="Add" CommandArgument='<%# Eval("ProductId") %>' OnCommand="CommandBtn_Click" runat="server"  />
      
    </td>
                     </tr>  
 
</ItemTemplate>
         
      <%--  <SeparatorTemplate>
    <hr />
             
</SeparatorTemplate>
     --%>

        <footertemplate>
            
    </table>        
          </footertemplate>
        <SeparatorTemplate>
        <tr>
            <td colspan="4" style="background-color: #E1E1E1">
            </td>
        </tr>
    </SeparatorTemplate>
</asp:Repeater> 

<script type="text/javascript" >
    function clearTextBox(textBoxID) {
        document.getElementById(textBoxID).value = "";
    }

</script>   
</asp:Content>




