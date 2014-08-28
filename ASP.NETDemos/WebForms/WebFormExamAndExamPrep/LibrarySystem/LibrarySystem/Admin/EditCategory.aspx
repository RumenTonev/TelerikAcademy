<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditCategory.aspx.cs" Inherits="LibrarySystem.Admin.EditCategory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <h1>Edit Category</h1>

    Category Name:
    <asp:TextBox ID="TextBoxCategoryName" runat="server" AutoPostBack="false"  /> 
    <asp:LinkButton ID="LinkButtonSaveCategory" runat="server" 
        Text="Save" OnClick="LinkButtonSaveCategory_Click"/>

   

</asp:Content>
