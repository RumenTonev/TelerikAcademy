<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BookDetails.aspx.cs" Inherits="LibrarySystem.BookDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <asp:FormView ID="FormViewBook" runat="server"  
            >
            <ItemTemplate>
                <h3>Book Details</h3>
                <table border="0">
                    <tr>                       
                        <td><%#: Eval("Title")%></td>
                    </tr>
                    <tr>
                        <td>by <%#: Eval("Author")%></td>
                    </tr>
                    <tr>
                        <td>ISBN <%#: Eval("ISBN")%></td>
                    </tr>
                    <tr>
                        <td>Web Site:<%#: Eval("WebSiteURL")%></td>
                    </tr>
                    <tr>
                        <td><%#: Eval("Description")%></td>
                    </tr>  
                </table>
                <hr />
                <asp:LinkButton PostBackUrl="~/Default.aspx" runat="server" Text="Back to books" />
            </ItemTemplate>
            
        </asp:FormView>
</asp:Content>
