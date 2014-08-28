<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditCategories.aspx.cs" Inherits="LibrarySystem.Admin.EditCategories" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:GridView ID="GridViewCategories" runat="server"
        AutoGenerateColumns="False" DataKeyNames="CategoryId"
        PageSize="3" AllowPaging="true" AllowSorting="true"
        ItemType="LibrarySystem.Models.Category"
        SelectMethod="GridViewCategories_GetData"
        DeleteMethod="GridViewCategories_DeleteItem"
        >
        <Columns>            
            <asp:BoundField DataField="Name" HeaderText="CategoryName"
                SortExpression="Name" />            
            <asp:HyperLinkField Text="Edit..." HeaderText="Action" 
                DataNavigateUrlFields="CategoryId" 
                DataNavigateUrlFormatString="EditCategory.aspx?categoryId={0}" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButtonDeleteCategory" runat="server" 
                        CommandName="Delete" 
                        CommandArgument="<%# Item.CategoryId %>"
                        OnClientClick = "return confirm('Do you want to cancel ?');"
                        Text="Delete" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <a href="EditCategory.aspx">Create New Category</a>

</asp:Content>
