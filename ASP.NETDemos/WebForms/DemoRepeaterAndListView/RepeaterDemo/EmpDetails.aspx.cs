using RepeaterDemo.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RepeaterDemo
{
    public partial class EmpDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpRequest request = this.Context.Request;
            string id = request.QueryString["id"];
            int idActual = Int32.Parse(id);
            using (var context = new NorthwindEntities())
            {
                var employee = context.Employees.FirstOrDefault(x => x.EmployeeID == idActual);
                this.FormViewCustomer.DataSource = ObjectExtensions.WrapInEnumerable<Employee>(employee);
                this.FormViewCustomer.DataBind();
            }
        }
    }
}
