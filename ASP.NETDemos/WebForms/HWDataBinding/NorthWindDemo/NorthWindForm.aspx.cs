using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using NorthWindDemo.Models;
using Newtonsoft.Json;
using System.Web.Script.Services;

namespace NorthWindDemo
{
    public partial class NorthWindForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                using (var context = new NorthwindEntities())
                {
                    this.GridViewEmployees.DataSource = context.Employees.ToList();
                    this.GridViewEmployees.DataBind();
                }
            }
        }
        public DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;

        }
        protected void GridViewEmployees_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortingDirection = string.Empty;
            if (direction == SortDirection.Ascending)
            {
                direction = SortDirection.Descending;
                sortingDirection = "Desc";

            }
            else
            {
                direction = SortDirection.Ascending;
                sortingDirection = "Asc";

            }

            using (var context = new NorthwindEntities())
            {
                var res = ConvertToDataTable<Employee>(context.Employees
                        .ToList());
                DataView sortedView = new DataView(res);
                if (e.SortExpression == "FirstName,LastName" && sortingDirection == "Desc")
                { sortedView.Sort = "FirstName DESC,LastName DESC"; }
                else if (e.SortExpression == "FirstName,LastName " && sortingDirection == "Asc")
                { sortedView.Sort = "FirstName ASC, LastName ASC"; }
                else { sortedView.Sort = e.SortExpression + " " + sortingDirection; }
                Session["SortedView"] = sortedView;
                this.GridViewEmployees.DataSource = sortedView;
                this.GridViewEmployees.DataBind();
            }
        }
        protected void GridViewEmployees_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.GridViewEmployees.PageIndex = e.NewPageIndex;
            if (Session["SortedView"] != null)
            {
                this.GridViewEmployees.DataSource = Session["SortedView"];
                this.GridViewEmployees.DataBind();
            }
            else
            {
                using (var context = new NorthwindEntities())
                {
                    this.GridViewEmployees.DataSource = context.Employees.ToList();
                    this.GridViewEmployees.DataBind();
                }
            }
        }
        public SortDirection direction
        {
            get
            {
                if (ViewState["directionState"] == null)
                {
                    ViewState["directionState"] = SortDirection.Ascending;
                }
                return (SortDirection)ViewState["directionState"];
            }
            set
            {
                ViewState["directionState"] = value;
            }
        }
        [WebMethod()]
        [ScriptMethod(UseHttpGet = true)]
        public static string GetData(string idValue)
        {
            SingleEmployeeDetails createdModel;
            int id = Int32.Parse(idValue);

            using (var context = new NorthwindEntities())
            {
                var employee = context.Employees.FirstOrDefault(usr => usr.EmployeeID == id);
                createdModel = new SingleEmployeeDetails()
                {
                    Address = employee.Address,
                    City = employee.City,
                    FullName = employee.FirstName + ' ' + employee.LastName,
                    PostalCode = employee.PostalCode,
                    Region = employee.Region,
                    Title = employee.Title
                };

            }

            var result = JsonConvert.SerializeObject(createdModel);
            return result;

        }


        protected void GridViewEmployees_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItem is Employee)
            {
                e.Row.Attributes.Add("onmouseover", "ViewEmployeDetails(" + (e.Row.DataItem as Employee).EmployeeID + ")");
                e.Row.Attributes.Add("onmouseout", "Closer()");
            }
            else if (e.Row.DataItem is DataRowView)
            {

                e.Row.Attributes.Add("onmouseover", "ViewEmployeDetails(" + (e.Row.DataItem as DataRowView).Row.ItemArray[0] + ")");
                e.Row.Attributes.Add("onmouseout", "Closer()");
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
                {
                    Label lb = new Label();
                    lb = (Label)e.Row.FindControl("LabelFullName");
                    lb.Text = DataBinder.Eval(e.Row.DataItem, "FirstName").ToString() + " " + DataBinder.Eval(e.Row.DataItem, "LastName").ToString();
                }
            }
        }
    }
}