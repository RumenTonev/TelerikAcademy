<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NorthwindRepeater.aspx.cs" Inherits="RepeaterDemo.NorthwindRepeater" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Repeater ID="RepeaterEmployee" runat="server">
            <HeaderTemplate>
                <ul>
            </HeaderTemplate>
            <ItemTemplate>
                <li>
                    <a href="EmpDetails.aspx?id=<%#: Eval("EmployeeID") %>"><%#: Eval("FirstName") + " " + Eval("LastName") %></a>
                </li>
            </ItemTemplate>
            <FooterTemplate>
                </ul>
            </FooterTemplate>
        </asp:Repeater>
    </form>
</body>
</html>
