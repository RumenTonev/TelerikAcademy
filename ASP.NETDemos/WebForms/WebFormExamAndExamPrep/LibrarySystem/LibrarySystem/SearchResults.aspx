<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SearchResults.aspx.cs" Inherits="LibrarySystem.SearchResults" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <div class="poll-question">
                
                <ul>
                    <asp:Repeater runat="server" ID="BooksRepeater"
                        ItemType="LibrarySystem.Models.Book"
                        >
                        <ItemTemplate>
                            <li>
                                
                               <a href="BookDetails.aspx?id=<%#: Item.BookId %>"><%#: Item.Title + " by " + Item.Author %></a>(Category:<%#:Item.Category.Name %>)
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
</asp:Content>
