<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdministratorDefault.aspx.cs" Inherits="ChatApp.Administrator.AdmninstratorDefault" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <fieldset>
        <legend>Messages<br />
 
            <asp:GridView ID="GridViewMessages" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                AllowSorting="True" AllowPaging="true" AutoGenerateColumns="False" DataKeyNames="Id" ItemType="ChatApp.Models.Message" ShowFooter="true"
                OnRowUpdating="GridViewMessages_RowUpdating" OnRowDeleting="GridViewMessages_RowDeleting"
                OnRowEditing="GridViewMessages_RowEditing" OnPageIndexChanging="GridViewMessages_PageIndexChanging"
                AutoGenerateEditButton="false" OnRowCancelingEdit="GridViewMessages_RowCancelingEdit">

                <Columns>
                    <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" ShowSelectButton="True" />

                    <asp:BoundField DataField="Id" Visible="false" />

                    <asp:TemplateField HeaderText="Author">
                        <ItemTemplate>
                            <%#: Eval("Author.FirstName") %> <%#: Eval("Author.LastName") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Contents" SortExpression="Contents">
                        <ItemTemplate>
                            <%#: Eval("Contents")%>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBoxContents" MaxLength="200" runat="server"
                                Text='<%# Bind("Contents")%>'></asp:TextBox>
                        </EditItemTemplate>
                        
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Timestamp" SortExpression="Timestamp">
                        <ItemTemplate>
                            <%#: Eval("Timestamp")%>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <FooterTemplate>
                            <asp:LinkButton runat="server" ID="GoChat"  OnClick="GoChat_Click"
                                 Text="GoToChat"></asp:LinkButton>
                        </FooterTemplate>
                    </asp:TemplateField>

                </Columns>

                <FooterStyle BackColor="White" ForeColor="#000066" />
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <RowStyle ForeColor="#000066" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#00547E" />
            </asp:GridView>
   
            <br />
            <asp:Label ID="LabelErrorMessage" runat="server" ForeColor="Red"></asp:Label>
        </legend>
    </fieldset>
</asp:Content>
