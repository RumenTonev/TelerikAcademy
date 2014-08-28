<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditBooks.aspx.cs" Inherits="LibrarySystem.Admin.EditBooks" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

     <h1>Edit Books</h1>
    <asp:GridView runat="server" ID="GridViewBooks" AutoGenerateColumns="false"
        SelectMethod="GridViewBooks_GetData"  AllowPaging="true" AllowSorting="true"
        DataKeyNames="Id" PageSize="5" ItemType="LibrarySystem.Models.BookModel" CellPadding="10">
        <Columns>
            <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title"/>
            <asp:BoundField DataField="Author" HeaderText="Author" SortExpression="Author" />
            <asp:BoundField DataField="ISBN" HeaderText="ISBN" SortExpression="ISBN" />
            <asp:BoundField DataField="WebSite" HeaderText="Web Site" SortExpression="WebSite" />
            <asp:BoundField DataField="Category" HeaderText="Category" SortExpression="Category" />
            <asp:HyperLinkField Text="Edit..." HeaderText="Action" 
                DataNavigateUrlFields="Id" 
                DataNavigateUrlFormatString="EditBook.aspx?bookId={0}" />
            <asp:TemplateField>
                <ItemTemplate>
                     <asp:LinkButton runat="server" Text="Delete" CommandArgument="<%#: Item.Id %>" OnClientClick = "return confirm('Do you want to cancel ?');" OnCommand="Delete_Command"/>
                    
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>




    <a href="EditBook.aspx">Create New Book</a>
</asp:Content>
