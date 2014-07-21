<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WorldModel.aspx.cs" Inherits="WorldDemo.WorldModel" ValidateRequest="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function Confirm() {
            var confirmValue = document.createElement("input");
            confirmValue.type = "hidden";
            confirmValue.name = "confirm-value";
            if (confirm("Do you want to continue with the operation?")) {
                confirmValue.value = "Yes";
            } else {
                confirmValue.value = "No";
            }

            document.forms[0].appendChild(confirmValue);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">

        <fieldset>
            <legend>Continents</legend>
            <asp:ListBox ID="ListBoxContinents" runat="server" DataSourceID="EntityDataSourceContinents"
                DataTextField="Name" DataValueField="ContinentID" AutoPostBack="true"
                OnSelectedIndexChanged="ListBoxContinents_SelectedIndexChanged"></asp:ListBox>
            <br />
            <asp:TextBox ID="TextBoxContinentName" MaxLength="20" runat="server"></asp:TextBox>
            <asp:Button ID="ButtonUpdateContinent" runat="server" Text="Update" OnClick="ButtonUpdateContinent_Click" />
            <asp:Button ID="ButtonDeleteContinent" runat="server" OnClick="ButtonDeleteContinent_Click" OnClientClick="Confirm()" Text="Delete" />
            <br />
            <asp:TextBox ID="TextBoxNewContinentName" MaxLength="20" runat="server"></asp:TextBox>
            <asp:Button ID="ButtonAddContinent" runat="server" Text="Add" OnClick="ButtonAddContinent_Click" />
            <br />
            <asp:Label ID="LabelContinentErrors" runat="server" ForeColor="Red"></asp:Label>
            <br />
            <asp:EntityDataSource ID="EntityDataSourceContinents" runat="server" ConnectionString="name=WorldEntities"
                DefaultContainerName="WorldEntities1" EnableDelete="True" EnableFlattening="False" EnableInsert="True"
                EnableUpdate="True" EntitySetName="Continents" EntityTypeFilter="Continent">
            </asp:EntityDataSource>
        </fieldset>
        <br />
        <fieldset>
            <legend>Countries</legend>

            <asp:GridView ID="GridViewCountries" runat="server" AllowPaging="True" AllowSorting="True"
                AutoGenerateColumns="False" DataKeyNames="CountryID" DataSourceID="EntityDataSourceCountries"
                BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                OnRowDataBound="GridViewCountries_RowDataBound" OnRowDeleting="GridViewCountries_RowDeleting"
                 OnRowUpdating="GridViewCountries_RowUpdating" OnRowEditing="GridViewCountries_RowEditing" OnSelectedIndexChanged="GridViewCountries_SelectedIndexChanged"  >
                <Columns>
                    <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" ShowSelectButton="True" />

                    <asp:TemplateField HeaderText="Country Name" SortExpression="Name">
                        <ItemTemplate>
                            <%#: Eval("Name")%>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBoxCountryName" MaxLength="50" runat="server"
                                Text='<%# Bind("Name")%>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="TextBoxNewCountryName" runat="server"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Population" SortExpression="Population">
                        <ItemTemplate>
                            <%#: Eval("Population")%>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBoxPopulation" runat="server"
                                Text='<%# Bind("Population")%>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="TextBoxNewPopulation" runat="server"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Language" SortExpression="Language">
                        <ItemTemplate>
                            <%#: Eval("Language")%>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBoxLanguage" runat="server"
                                Text='<%# Bind("Language")%>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="TextBoxNewLanguage" runat="server"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>


                    <asp:ImageField DataImageUrlField="CountryID" ReadOnly="true"
                        DataImageUrlFormatString="~/ImageHttpHandler.ashx?ID={0}" HeaderText="Flag">
                        <ControlStyle Height="120px" Width="120px" />
                    </asp:ImageField>
                </Columns>
                <EditRowStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
            </asp:GridView>

            <br />
            Country Name:
        <asp:TextBox ID="txtCountryName" runat="server"></asp:TextBox><br />
            Country Language:
        <asp:TextBox ID="txtCountryLanguage" runat="server"></asp:TextBox><br />
            Country Population:
        <asp:TextBox ID="txtCountryPopulation" runat="server"></asp:TextBox><br />
            Country Flag:
        <asp:FileUpload ID="fuPicture" runat="server" />
            <br />



            <asp:Button ID="btnSaveCountry" runat="server" Text="Add" OnClick="btnSaveCountry_Click" />
            <br />
            <asp:Button ID="ButtonChangePhoto" runat="server" Text="ChangePhoto" OnClick="ButtonChangePhoto_Click" />
            <br />
            <asp:Label ID="LabelCountryErrors" runat="server" ForeColor="Red"></asp:Label>
            <br />

            <asp:EntityDataSource ID="EntityDataSourceCountries" runat="server" ConnectionString="name=WorldEntities"
                DefaultContainerName="WorldEntities1" EnableDelete="True" EnableFlattening="False" EnableInsert="True" EnableUpdate="True"
                EntitySetName="Countries" EntityTypeFilter="Country" Where="it.ContinentID=@ContinentID">
                <WhereParameters>
                    <asp:ControlParameter Name="ContinentID" Type="Int32"
                        ControlID="ListBoxContinents" PropertyName="SelectedValue" />
                </WhereParameters>

            </asp:EntityDataSource>

        </fieldset>
        <br />
        <fieldset>
            <legend>Towns</legend>

            <fieldset>
                <legend>Cities</legend>

                <asp:ListView ID="ListViewCities" runat="server" DataKeyNames="TownID" DataSourceID="EntityDataSourceTowns"
                    InsertItemPosition="LastItem" OnItemInserting="ListViewCities_ItemInserting" OnItemUpdating="ListViewCities_ItemUpdating" 
                    OnItemDeleting="ListViewCities_ItemDeleting">
                    <AlternatingItemTemplate>
                        <tr style="background-color: #FFFFFF; color: #284775;">
                            <td>
                                <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="Delete" />
                                <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
                            </td>
                            <td>
                                <asp:Label ID="NameLabel" runat="server" Text='<%# Eval("Name") %>' />
                            </td>
                            <td>
                                <asp:Label ID="PopulationLabel" runat="server" Text='<%# Eval("Population") %>' />
                            </td>
                            <td>
                                <asp:Label ID="CountrtyNameLabel" runat="server" Text='<%# Eval("Country.Name") %>' />
                            </td>
                        </tr>
                    </AlternatingItemTemplate>
                    <EditItemTemplate>
                        <tr style="background-color: #999999;">
                            <td>
                                <asp:Button ID="UpdateButton" runat="server" CommandName="Update" Text="Update" />
                                <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancel" />
                            </td>
                            <td>
                                <asp:TextBox ID="NameTextBox" runat="server" Text='<%# Bind("Name") %>' />
                            </td>
                            <td>
                                <asp:TextBox ID="PopulationTextBox" runat="server" Text='<%# Bind("Population") %>' />
                            </td>
                            <td>
                                <asp:TextBox ID="CountryNameTextBox" runat="server" Text='<%# Eval("Country.Name") %>' />
                            </td>

                        </tr>
                    </EditItemTemplate>
                    <EmptyDataTemplate>
                        <table runat="server" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px;">
                            <tr>
                                <td>No data was returned.</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <InsertItemTemplate>
                        <tr style="">
                            <td>
                                <asp:Button ID="InsertButton" runat="server" CommandName="Insert" Text="Insert" />
                                <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Clear" />
                            </td>
                            <td>
                                <asp:TextBox ID="NameTextBox" runat="server" Text='<%# Bind("Name") %>' />
                            </td>
                            <td>
                                <asp:TextBox ID="PopulationTextBox" runat="server" Text='<%# Bind("Population") %>' />
                            </td>
                            <td>
                                <asp:Label ID="CountryNameLabel" runat="server" Text='<%# Eval("Country.Name") %>' />
                            </td>
                        </tr>
                    </InsertItemTemplate>
                    <ItemTemplate>
                        <tr style="background-color: #E0FFFF; color: #333333;">
                            <td>
                                <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="Delete" />
                                <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
                            </td>
                            <td>
                                <asp:Label ID="NameLabel" runat="server" Text='<%# Eval("Name") %>' />
                            </td>
                            <td>
                                <asp:Label ID="PopulationLabel" runat="server" Text='<%# Eval("Population") %>' />
                            </td>
                            <td>
                                <asp:Label ID="CountryNameLabel" runat="server" Text='<%# Eval("Country.Name") %>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table runat="server">
                            <tr runat="server">
                                <td runat="server">
                                    <table id="itemPlaceholderContainer" runat="server" border="1" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px; font-family: Verdana, Arial, Helvetica, sans-serif;">
                                        <tr runat="server" style="background-color: #E0FFFF; color: #333333;">
                                            <th runat="server">Action</th>
                                            <th runat="server">Name</th>
                                            <th runat="server">Population</th>
                                            <th runat="server">CountryName</th>
                                        </tr>
                                        <tr id="itemPlaceholder" runat="server">
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr runat="server">
                                <td runat="server" style="text-align: center; background-color: #5D7B9D; font-family: Verdana, Arial, Helvetica, sans-serif; color: #FFFFFF">
                                    <asp:DataPager ID="DataPager1" runat="server">
                                        <Fields>
                                            <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowLastPageButton="True" />
                                        </Fields>
                                    </asp:DataPager>
                                </td>
                            </tr>
                        </table>
                    </LayoutTemplate>
                    <SelectedItemTemplate>
                        <tr style="background-color: #E2DED6; font-weight: bold; color: #333333;">
                            <td>
                                <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="Delete" />
                                <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
                            </td>
                            <td>
                                <asp:Label ID="NameLabel" runat="server" Text='<%# Eval("Name") %>' />
                            </td>
                            <td>
                                <asp:Label ID="PopulationLabel" runat="server" Text='<%# Eval("Population") %>' />
                            </td>
                            <td>
                                <asp:Label ID="CountryNameLabel" runat="server" Text='<%# Eval("Country.Name") %>' />
                            </td>
                        </tr>
                    </SelectedItemTemplate>
                </asp:ListView>
                <asp:Label ID="LabelCityErrors" runat="server" ForeColor="Red"></asp:Label>
                <br />
            </fieldset>




            <asp:EntityDataSource ID="EntityDataSourceTowns" runat="server" ConnectionString="name=WorldEntities"
                DefaultContainerName="WorldEntities1" EnableDelete="True" EnableFlattening="False" EnableInsert="True"
                EnableUpdate="True" EntitySetName="Towns" EntityTypeFilter="Town" Where="it.CountryID=@CountryID" Include="Country">
                <WhereParameters>
                    <asp:ControlParameter Name="CountryID" Type="Int32"
                        ControlID="GridViewCountries" PropertyName="SelectedValue" />
                </WhereParameters>

            </asp:EntityDataSource>

        </fieldset>
        <br />
    </form>
</body>
</html>
