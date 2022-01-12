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
using System.Drawing;
using System.IO;



namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Photo
{
    public partial class UploadPhoto : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string selectedValue = "-1";
            string selectedValue2 = "-2";
            try
            {
                selectedValue = Request.Params.Get("category");
                selectedValue2 = Request.Params.Get("Tag");

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
                LoadTags(selectedValue2);
            }



        }

        protected void LoadTags(string s) {

            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IPhotoService photoService = iocManager.Resolve<IPhotoService>();
            TagBlock tag = photoService.FindAllTags();

            // Create a table to store data for the DropDownList control.
            DataTable dt = new DataTable();

            // Define the columns of the table.
            dt.Columns.Add(new DataColumn("TagTypeField", typeof(string)));
            dt.Columns.Add(new DataColumn("TagIdField", typeof(long)));

            // Populate the table.
            dt.Rows.Add(CreateRow("-", -1, dt));

            foreach (Tag t in tag.Tags)
            {
                dt.Rows.Add(CreateRow(t.tagName, t.tagId, dt));
            }

            // Create a DataView from the DataTable to act as the data source
            // for the DropDownList control.
            DataView dv = new DataView(dt);

            DropDownList1.DataSource = dv;
            DropDownList1.DataTextField = "TagTypeField";
            DropDownList1.DataValueField = "TagIdField";

            // Bind the data to the control.
            DropDownList1.DataBind();

            // Set the default selected item.
            DropDownList1.SelectedValue = s;
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
            //link = @"C:\EntregaMaD\PracticaMaD\Web\Images\";
            link = @"C:\EntregaMaD\PracticaMaD\Web\Images\";
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

            long p = photoService.UploadPhoto(SessionManager.GetUserSession(Context).FirstName, title, description, diaphragm, exhibitionTime, iso, whiteBalance,
                (long.Parse(CategoryDropDownList.SelectedItem.Value)), SessionManager.GetUserSession(Context).UserProfileId, image);


            photoService.AddPhotoTag(long.Parse(DropDownList1.SelectedItem.Value), p);

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