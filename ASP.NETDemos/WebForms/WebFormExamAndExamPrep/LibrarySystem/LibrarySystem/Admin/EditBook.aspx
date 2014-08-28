<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditBook.aspx.cs" Inherits="LibrarySystem.Admin.EditBook" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <h1>Edit Book</h1>
    <asp:Table runat="server" >
        <asp:TableHeaderRow runat="server" >
            <asp:TableHeaderCell runat="server" Text ="BookProperty"></asp:TableHeaderCell>
            <asp:TableHeaderCell runat="server" Text ="PropertyValue"></asp:TableHeaderCell>
        </asp:TableHeaderRow>
        <asp:TableRow>
            <asp:TableCell Text="Title"></asp:TableCell>
             <asp:TableCell ><asp:TextBox ID="TextBoxTitle"  runat="server"   /></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell Text="Author"></asp:TableCell>
             <asp:TableCell ><asp:TextBox ID="TextBoxAuthor"  runat="server"   /></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell Text="ISBN"></asp:TableCell>
             <asp:TableCell ><asp:TextBox ID="TextBoxISBN"  runat="server"   /></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell Text="WebSite"></asp:TableCell>
             <asp:TableCell ><asp:TextBox ID="TextBoxWebSiteURL"  runat="server"   /></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell Text="Description"></asp:TableCell>
             <asp:TableCell ><asp:TextBox ID="TextBoxDescription"  runat="server"   /></asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    

    <asp:ListBox runat="server" ID="ListBoxCategories" Visible="false"></asp:ListBox>
    
    <asp:LinkButton ID="LinkButtonSaveCategory" runat="server" 
        Text="Save" OnClick="LinkButtonSaveCategory_Click"/>
</asp:Content>

