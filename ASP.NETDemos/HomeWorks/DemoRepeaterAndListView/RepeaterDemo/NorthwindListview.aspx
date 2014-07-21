<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NorthwindListview.aspx.cs" Inherits="RepeaterDemo.NorthwindListview" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ListView ID="ListViewEmployees" runat="server"
            ItemType="RepeaterDemo.Employee">
            <LayoutTemplate>
                <h3>Employees</h3>
                <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
            </LayoutTemplate>

            <ItemSeparatorTemplate>
                <hr />
            </ItemSeparatorTemplate>
        
          
            <ItemTemplate>
                <div class="product-description">
                
                    <a href="EmpDetails.aspx?id=<%#: Eval("EmployeeID") %>"><%#: Item.FirstName+" "+Item.LastName %></a>
                 </div>
            </ItemTemplate>
         
            
        </asp:ListView>

        <asp:DataPager ID="DataPagerEmployees" runat="server"
            PagedControlID="ListViewEmployees" PageSize="3"
            QueryStringField="page">
            <Fields>
                <asp:NextPreviousPagerField ShowFirstPageButton="true"
                    ShowNextPageButton="false" ShowPreviousPageButton="false" />
                <asp:NumericPagerField />
                <asp:NextPreviousPagerField ShowLastPageButton="true"
                    ShowNextPageButton="false" ShowPreviousPageButton="false" />
            </Fields>
        </asp:DataPager>
    </form>
</body>
</html>
