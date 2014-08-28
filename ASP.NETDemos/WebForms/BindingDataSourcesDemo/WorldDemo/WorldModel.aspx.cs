using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WorldDemo
{
    public partial class WorldModel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           // 
        }
        #region Continents
        protected void ButtonUpdateContinent_Click(object sender, EventArgs e)
        {
            this.LabelContinentErrors.Text = string.Empty;

            if (string.IsNullOrWhiteSpace(this.ListBoxContinents.SelectedValue))
            {
                this.LabelContinentErrors.Text = "No continent selected.";
                return;
            }

            string newContinentName = this.TextBoxContinentName.Text.Trim();

            if (string.IsNullOrWhiteSpace(newContinentName))
            {
                this.LabelContinentErrors.Text = "No continent name specified.";
                return;
            }


            using (var context = new WorldEntities())
            {
                int continentId = int.Parse(this.ListBoxContinents.SelectedValue);
                var continent = context.Continents.FirstOrDefault(c => c.ContinentID == continentId);
                if (continent != null)
                {
                    continent.Name = newContinentName;
                    context.Entry<Continent>(continent).State = EntityState.Modified;
                    context.SaveChanges();
                    Response.Redirect(Request.RawUrl);
                }
                else
                {
                    this.LabelContinentErrors.Text = "No continent found with id=" + continentId;
                }
            }
           
        }

        protected void ButtonDeleteContinent_Click(object sender, EventArgs e)
        {
            this.LabelContinentErrors.Text = string.Empty;

            if (string.IsNullOrWhiteSpace(this.ListBoxContinents.SelectedValue))
            {
                this.LabelContinentErrors.Text = "No continent selected.";
                return;
            }

            string confirmValue = Request.Form["confirm-value"];
            if (confirmValue == "No")
            {
                return;
            }

            using (var context = new WorldEntities())
            {
                int continentId = int.Parse(this.ListBoxContinents.SelectedValue);
                var continent = context.Continents.FirstOrDefault(c => c.ContinentID == continentId);
                if (continent != null)
                {
                    context.Continents.Remove(continent);
                    context.SaveChanges();
                    Response.Redirect(Request.RawUrl);
                }
                else
                {
                    this.LabelContinentErrors.Text = "No continent found with id=" + continentId;
                }
            }
        }

        protected void ButtonAddContinent_Click(object sender, EventArgs e)
        {
            this.LabelContinentErrors.Text = string.Empty;

            string newContinentName = this.TextBoxNewContinentName.Text.Trim();

            if (string.IsNullOrWhiteSpace(newContinentName))
            {
                this.LabelContinentErrors.Text = "No continent name specified.";
                return;
            }

            using (var context = new WorldEntities())
            {
                context.Continents.Add(new Continent
                {
                  Name = newContinentName
                });

                context.SaveChanges();
                Response.Redirect(Request.RawUrl);
            }
        }

        protected void ListBoxContinents_SelectedIndexChanged(object sender, EventArgs e)
        {
            string continentName = (sender as ListBox).SelectedItem.Text;
            this.TextBoxContinentName.Text = continentName;
        }

        #endregion
        #region Countries
        private void ValidatePopulation(string s, out int result)
        {
            if (!int.TryParse(s, out result) || result <= 0)
            {
                throw new ArgumentException("The population should be a positive integer.");
            }
        }
        private void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Missing name.");
            }
        }
        private void ValidateCountryName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Missing name.");
            }
            if (!this.CheckNameExisting(name))
            {
                throw new ArgumentException("No country with such name");
            }

        }
        private bool CheckNameExisting(string name)
        {
          Country res=null;
            using (var context = new WorldEntities())
            {
                 res = context.Countries.FirstOrDefault(x => x.Name == name);
            }
            if (res == null)
            {
                return false;
            }
            return true;
        }
        protected void ListViewCities_ItemInserting(object sender, ListViewInsertEventArgs e)
        {
            this.LabelCityErrors.Text = string.Empty;

            try
            {
                string cityName = (e.Item.FindControl("NameTextBox") as TextBox).Text;
                this.ValidateName(cityName);

                


                int population;
                string populationAsString = (e.Item.FindControl("PopulationTextBox") as TextBox).Text;
                this.ValidatePopulation(populationAsString, out population);
                var countryId = this.GridViewCountries.SelectedDataKey.Value.ToString();

                this.EntityDataSourceTowns.InsertParameters.Add("CountryID", TypeCode.String, countryId);
              // string countryName;
               // var reeee = this.GridViewCountries.SelectedDataKey.Value;
               // TextBox txt = new TextBox();
               // Binding b = new Binding("Name");

                //txt.SetBinding(TextBox.TextProperty, b);
               // (e.Item.FindControl("CountryNameTextBox") as TextBox).Text= this.GridViewCountries.SelectedDataKey.Value.ToString();
               // (e.Item.FindControl("CountryNameTextBox") as TextBox).DataBind();
               // var meeee = (e.Item.FindControl("CountryNameTextBox") as TextBox).Text;
                //checkID
                
               

                this.ListViewCities.DataBind();
            }
            catch (Exception ex)
            {
                this.LabelCityErrors.Text = ex.Message;
                e.Cancel = true;
            }
        }

        protected void ListViewCities_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            this.LabelCityErrors.Text = string.Empty;

            try
            {
                string cityName = (this.ListViewCities.Items[e.ItemIndex].FindControl("NameTextBox") as TextBox).Text;
                this.ValidateName(cityName);

                string countryName = (this.ListViewCities.Items[e.ItemIndex].FindControl("CountryNameTextBox") as TextBox).Text;
                this.ValidateCountryName(countryName);

                int population;
                string populationAsString = (this.ListViewCities.Items[e.ItemIndex].FindControl("PopulationTextBox") as TextBox).Text;
                this.ValidatePopulation(populationAsString, out population);
            }
            catch (DbEntityValidationException ex)
            {
                var errors = ex.EntityValidationErrors
                    .SelectMany(eve => eve.ValidationErrors)
                    .Select(ve => ve.ErrorMessage);
                this.LabelCityErrors.Text = string.Join(", ", errors);
                e.Cancel = true;
            }
            catch (Exception ex)
            {
                this.LabelCityErrors.Text = ex.Message;
                e.Cancel = true;
            }
        }

        protected void ListViewCities_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {

        }
        #endregion

        
             protected void GridViewCountries_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowState != DataControlRowState.Edit)
            {
                //In this sample, there are  3 buttons and the second one is Delete button, that's why we use the index 2
                //indexing goes as 0 is button #1, 1 Literal (Space between buttons), 2 button #2, 3 Literal (Space) etc.
                ((LinkButton)e.Row.Cells[0].Controls[2]).OnClientClick = "Confirm()";
            }
        }

             protected void GridViewCountries_RowDeleting(object sender, GridViewDeleteEventArgs e)
             {
                 string confirmValue = Request.Form["confirm-value"];
                 if (confirmValue == "No")
                 {
                     e.Cancel = true;
                 }
             }

             protected void GridViewCountries_RowUpdating(object sender, GridViewUpdateEventArgs e)
             {
                 WorldEntities context = null;
                 this.LabelCountryErrors.Text = string.Empty;

                 try
                 {
                    // string countryName = (this.GridViewCountries.Rows[e.RowIndex].FindControl("TextBoxCountryName") as TextBox).Text;
                    // this.ValidateName(countryName);

                     
                    // int population;
                    // string populationAsString = (this.GridViewCountries.Rows[e.RowIndex].FindControl("TextBoxPopulation") as TextBox).Text;
                    // this.ValidatePopulation(populationAsString, out population);

                   

                        
                 }
                 catch (Exception ex)
                 {
                     this.LabelCountryErrors.Text = ex.Message;
                     e.Cancel = true;
                 }
                 finally
                 {
                     if (context != null)
                     {
                         context.Dispose();
                     }
                 }
             }

             protected void btnSaveCountry_Click(object sender, EventArgs e)
             {
                 if (!string.IsNullOrEmpty(txtCountryName.Text) &&
                !string.IsNullOrEmpty(txtCountryLanguage.Text) &&!string.IsNullOrEmpty(txtCountryPopulation.Text) &&
                fuPicture.HasFile &&
                ListBoxContinents.SelectedIndex >= 0)
                 {
                     Byte[] imgByte = null;
                     HttpPostedFile File = fuPicture.PostedFile;
                     imgByte = new Byte[File.ContentLength];
                     File.InputStream.Read(imgByte, 0, File.ContentLength);

                     var db = new WorldEntities();

                     var c = new Country();
                     var fff = btnSaveCountry.CommandArgument;
                    // if (btnSaveCountry.CommandArgument != null)
                     //{
                       //  c = db.Countries.Find(int.Parse(btnSaveCountry.CommandArgument));
                       //  btnSaveCountry.CommandArgument = null;
                    // }
                     //if (c == null)
                    // {
                        // c = new Country();
                     //}

                     c.Name = Server.HtmlEncode(txtCountryName.Text);
                     c.Language = Server.HtmlEncode(txtCountryLanguage.Text);
                     c.Population =Int32.Parse(Server.HtmlEncode(txtCountryPopulation.Text));
                     c.photo = imgByte;
                     c.ContinentID = int.Parse(ListBoxContinents.SelectedValue);

                     db.Countries.Add(c);
                     db.SaveChanges();
                     
                     txtCountryName.Text = "";
                     txtCountryLanguage.Text = "";
                     txtCountryPopulation.Text = "";
                     GridViewCountries.DataBind();
                 }
             }

             protected void ButtonChangePhoto_Click(object sender, EventArgs e)
             {
                 int curIndex = Int32.Parse(GridViewCountries.SelectedDataKey.Value.ToString());
                 Byte[] imgByte = null;
                 if(fuPicture.HasFile)
                 {
                     
                     HttpPostedFile File = fuPicture.PostedFile;
                     imgByte = new Byte[File.ContentLength];
                     File.InputStream.Read(imgByte, 0, File.ContentLength);
                 }
                 using (var context = new WorldEntities())
                 {
                     
                     var country = context.Countries.FirstOrDefault(c => c.CountryID == curIndex);
                     if (country != null)
                     {
                         country.photo = imgByte;
                         
                         context.Entry<Country>(country).State = EntityState.Modified;
                         context.SaveChanges();
                         this.GridViewCountries.DataBind();
                         //Response.Redirect(Request.RawUrl);
                     }
                     else
                     {
                         this.LabelContinentErrors.Text = "No continent found with id=" + curIndex;
                     }
                 }
             }

             protected void GridViewCountries_SelectedIndexChanged(object sender, EventArgs e)
             {
                 GridViewRow row =GridViewCountries.SelectedRow;
                 if (row.RowState != (DataControlRowState.Edit | DataControlRowState.Selected))
                 {
                     GridViewCountries.EditIndex = -1;
                 } 
             }

             protected void GridViewCountries_RowEditing(object sender, GridViewEditEventArgs e)
             {

                 GridViewRow row = GridViewCountries.Rows[e.NewEditIndex];

                 // Check the row state. If the row is not in edit mode and is selected,
                 // select the current row. This ensures that the GridView control selects
                 // the current row when the user clicks the Edit button.
                 if (row.RowState != (DataControlRowState.Edit | DataControlRowState.Selected))
                 {
                     GridViewCountries.SelectedIndex = e.NewEditIndex;
                 }
             }
        

        
    }
}