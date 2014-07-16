<%@ Page Title="" Language="C#" MasterPageFile="~/NorthWindDemo.Master" AutoEventWireup="true" CodeBehind="NorthWindForm.aspx.cs" Inherits="NorthWindDemo.NorthWindForm" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="scm" runat="server" EnablePageMethods="true" EnablePartialRendering="true" />
    <asp:GridView ID="GridViewEmployees" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="EmployeeID"
        OnRowDataBound="GridViewEmployees_RowDataBound" OnSorting="GridViewEmployees_Sorting" OnPageIndexChanging="GridViewEmployees_PageIndexChanging">
        <Columns>
            <asp:TemplateField HeaderText="FullName" SortExpression="FirstName,LastName">
                <ItemTemplate>
                    <asp:Label ID="LabelFullName" runat="server"></asp:Label>
                    <br />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="City" SortExpression="City">
                <ItemTemplate>
                    <asp:Label ID="LabelCity" runat="server" Text='<%# Bind("City") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Country" SortExpression="Country">
                <ItemTemplate>
                    <asp:Label ID="LabelCountry" runat="server" Text='<%# Bind("Country") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <div id="holderSingle">
        <img id="holderImage" src="http://localhost:46962/NorthWindForm.img2?id=3" />
        <div id="textContainer"></div>
    </div>
    <br />

    <script>
        $('#holderSingle').hide();
        function ViewEmployeDetails(idOfButton) {




            $.ajax({
                type: "GET",
                url: "NorthWindForm.aspx/GetData",
                data: { 'idValue': idOfButton },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var cur = jQuery.parseJSON(data.d);
                    $('#textContainer').empty();
                    var raw_fragment = document.createDocumentFragment();
                    for (var property in cur) {
                        if (cur.hasOwnProperty(property)) {
                            var p = document.createElement("div");
                            p.appendChild(document.createTextNode(property + ' : ' + cur[property]));
                            raw_fragment.appendChild(p);
                        }
                    }
                    document.getElementById('textContainer').appendChild(raw_fragment);
                    $('#holderImage').attr('src', "http://localhost:46962/NorthWindForm.img2?id=" + idOfButton);
                }
            });
            $('#holderSingle').show();

        }

        function Closer() {

            $('#holderSingle').hide();

        }
    </script>
</asp:Content>
