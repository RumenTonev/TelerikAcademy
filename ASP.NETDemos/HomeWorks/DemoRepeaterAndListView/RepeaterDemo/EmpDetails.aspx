<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmpDetails.aspx.cs" Inherits="RepeaterDemo.EmpDetails" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:FormView ID="FormViewCustomer" runat="server"  
            >
            <ItemTemplate>
                <h3><%#: Eval("FirstName") + " " + Eval("LastName") %></h3>
                <table border="0">
                    <tr>
                        <td>Address:</td>
                        <td><%#: Eval("Address")%></td>
                    </tr>
                    <tr>
                        <td>City:</td>
                        <td><%#: Eval("City")%></td>
                    </tr>
                    <tr>
                        <td>Region:</td>
                        <td><%#: Eval("Region")%></td>
                    </tr>
                    <tr>
                        <td>Country:</td>
                        <td><%#: Eval("Country")%></td>
                    </tr>
                    <tr>
                        <td>HomePhone:</td>
                        <td><%#: Eval("HomePhone")%></td>
                    </tr>
                    <tr>
                        <td>Notes:</td>
                        <td><%#: Eval("Notes")%></td>
                    </tr>
                </table>
                <hr />
                <asp:LinkButton PostBackUrl="~/NorthwindRepeater.aspx" runat="server" Text="Back to repeater" />
            </ItemTemplate>
            
        </asp:FormView>

        
    </form>
</body>
</html>
