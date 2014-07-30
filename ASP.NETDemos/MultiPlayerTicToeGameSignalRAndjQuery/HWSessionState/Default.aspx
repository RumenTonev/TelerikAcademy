<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HWSessionState._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

   <asp:UpdatePanel runat="server" id="UpdatePanel1">

<ContentTemplate>
    <asp:LinkButton ID="LinkButtonNewGame" runat="server" OnClick="LinkButtonNewGame_Click">NewGame</asp:LinkButton>
<asp:Timer runat="server" id="Timer1" Interval="5000" OnTick="Timer1_Tick"></asp:Timer>
<asp:Label runat="server" Text="Page not refreshed yet." id="Label1">
</asp:Label>
     <h2>ArchivedGames.</h2>
        <asp:Repeater ID="RepeaterGamesArchived" runat="server">
           
            <HeaderTemplate>

                <ul>
            </HeaderTemplate>
            <ItemTemplate>
                <li>
                    <div><strong><%#: Eval("Id") %> <%#: Eval("Result") %></strong></div>                  
                </li>
            </ItemTemplate>
            <FooterTemplate>
                </ul>
            </FooterTemplate>
        </asp:Repeater>
    <h2>ClosedGames.</h2>
    <asp:Repeater ID="RepeaterGamesClosed" runat="server">
        
            <HeaderTemplate>
                <ul>
            </HeaderTemplate>
            <ItemTemplate>
                <li>
                   
                    <div><strong><%#: Eval("Id") %> Closed</strong></div>                  
                </li>
                </li>
            </ItemTemplate>
            <FooterTemplate>
                </ul>
            </FooterTemplate>
        </asp:Repeater>
    <h2>OpenGames.</h2>





   <asp:Repeater ID="RepeaterGamesOpen" runat="server" OnItemDataBound="RepeaterGamesOpen_ItemDataBound" OnItemCommand="RepeaterGamesOpen_ItemCommand" >
       
            <HeaderTemplate>
                <ul>
            </HeaderTemplate>
            <ItemTemplate>
               <asp:LinkButton runat="server" ID="HyperLink" CommandName="cmd" ></asp:LinkButton>
                
            </ItemTemplate>
            <FooterTemplate>
                </ul>
            </FooterTemplate>
        </asp:Repeater>
    </ContentTemplate>
</asp:UpdatePanel>

</asp:Content>
