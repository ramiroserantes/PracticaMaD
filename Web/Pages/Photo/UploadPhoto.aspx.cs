using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMad.Model.PhotoService;
using Es.Udc.DotNet.PracticaMad.Model;
using Es.Udc.DotNet.PracticaMad.Model.CommentService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using System.Data;
using System.Collections.Generic;
using System.Drawing;
using System.IO;



namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Photo
{
    public partial class UploadPhoto : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string selectedValue = "-1";
            try
            {
                selectedValue = Request.Params.Get("category");

                btnUploadPhoto.Visible = true;

                lnkBack.NavigateUrl = "~/Pages/MainPage.aspx?";

                if (!SessionManager.IsUserAuthenticated(Context))
                {
                    lblUnlogedUser.Visible = true;
                    btnUploadPhoto.Visible = false;
                    return;
                }

            }
            catch (ArgumentNullException)
            {
                lblNoPhoto.Visible = true;
                btnUploadPhoto.Visible = false;
                lnkBack.NavigateUrl = "~/Pages/MainPage.aspx?";
            }
            catch (InstanceNotFoundException)
            {
                btnUploadPhoto.Visible = true;
            }

            if (!IsPostBack)
            {
                IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
                IPhotoService photoService = iocManager.Resolve<IPhotoService>();
                List<Category> categories = photoService.FindAllCategories();

                // Create a table to store data for the DropDownList control.
                DataTable dt = new DataTable();

                // Define the columns of the table.
                dt.Columns.Add(new DataColumn("CategoryTypeField", typeof(string)));
                dt.Columns.Add(new DataColumn("CategoryIdField", typeof(long)));

                // Populate the table.
                dt.Rows.Add(CreateRow("-", -1, dt));

                foreach (Category category in categories)
                {
                    dt.Rows.Add(CreateRow(category.categoryType, category.categoryId, dt));
                }

                // Create a DataView from the DataTable to act as the data source
                // for the DropDownList control.
                DataView dv = new DataView(dt);

                CategoryDropDownList.DataSource = dv;
                CategoryDropDownList.DataTextField = "CategoryTypeField";
                CategoryDropDownList.DataValueField = "CategoryIdField";

                // Bind the data to the control.
                CategoryDropDownList.DataBind();

                // Set the default selected item.
                CategoryDropDownList.SelectedValue = selectedValue;
            }



        }

        protected void BtnUploadPhotoClick(object sender, EventArgs e)
        {
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IPhotoService photoService = iocManager.Resolve<IPhotoService>();

            String title, description, link, iso;
            System.Drawing.Image image;
            long whiteBalance ,exhibitionTime, diaphragm;

            title = this.txtTitle.Text;
            description =this.txtDesc.Text;
            diaphragm = long.Parse(this.txtDia.Text);
            link = @"D:\MaD\MaD-ParteWeb\PracticaMaD\Web\Images\";
            exhibitionTime = long.Parse(this.txtExhi.Text);
            iso = this.txtIso.Text;
            whiteBalance = long.Parse(this.txtBalance.Text);

            //Check whether Directory (Folder) exists.
            if (!Directory.Exists(link))
            {
                //If Directory (Folder) does not exists Create it.
                Directory.CreateDirectory(link);
            }

            //Save the File to the Directory (Folder).
            ImageLoader.SaveAs(link + Path.GetFileName(ImageLoader.FileName));

            image = System.Drawing.Image.FromFile(link + Path.GetFileName(ImageLoader.FileName));

            photoService.UploadPhoto(title, description, diaphragm, exhibitionTime, iso, whiteBalance,
                (long.Parse(CategoryDropDownList.SelectedItem.Value)), SessionManager.GetUserSession(Context).UserProfileId, image);

            Response.Redirect(
             Response.ApplyAppPathModifier("~/Pages/MainPage.aspx?"));

        }

        protected DataRow CreateRow(string text, long value, DataTable dt)
        {
            // Create a DataRow using the DataTable.
            DataRow dr = dt.NewRow();

            dr[0] = text;
            dr[1] = value;

            return dr;
        }
    }
}