<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="LibrarySystem._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

   
   <asp:ListView ID="ListViewCategories"   runat="server" 
            ItemType="LibrarySystem.Models.Category">
       
        <LayoutTemplate>
             <div class="poll-question-outer">
            <div id="itemPlaceholder" runat="server"></div>
                 </div>
        </LayoutTemplate>
        <ItemTemplate>
            <div class="poll-question">
                <%#: Item.Name %>
                <ul>
                    <asp:Repeater runat="server" ID="RepeaterBooks"
                        ItemType="LibrarySystem.Models.Book"
                        DataSource="<%# Item.Books %>" OnItemDataBound="RepeaterBooks_ItemDataBound">
                        <ItemTemplate>
                            <li>
                                
                                <a href="BookDetails.aspx?id=<%#: Item.BookId %>"><%#: Item.Title + " by " + Item.Author %></a>
                            </li>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblEmptyData"
        Text="No Data To Display" runat="server" Visible="false">
 </asp:Label>
                        </FooterTemplate>
                    </asp:Repeater>
                </ul>
            </div>
        </ItemTemplate>
       

    </asp:ListView>
    <div id="search-container">
    <asp:TextBox runat="server" ID="TextBoxSearch" AutoPostBack="false" ></asp:TextBox>
    <asp:Button runat="server" ID="ButtonSearch" Text="Search" OnClick="ButtonSearch_Click" />
        </div>
</asp:Content>
