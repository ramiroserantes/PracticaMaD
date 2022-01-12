using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMad.Model;
using Es.Udc.DotNet.PracticaMad.Model.CommentService;
using Es.Udc.DotNet.PracticaMad.Model.PhotoService;
using Es.Udc.DotNet.PracticaMad.Model.Services.UserService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Photo
{
    public partial class ModifyTags : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            string selectedValue = "-1";
            try
            {
                selectedValue = Request.Params.Get("Tag");
            }
            catch (InstanceNotFoundException)
            {
                
            }
            long photoId;

            /* Get photoId */
            try
            {
                photoId = long.Parse(Request.Params.Get("photo"));
            }
            catch (ArgumentNullException)
            {
                return;
            }

            /* Get the Service */
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IPhotoService photoService = iocManager.Resolve<IPhotoService>();

            if (!IsPostBack)
            {
                LoadTags(selectedValue);
            }
        }

        protected void LoadTags(string s)
        {

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

            TagDropDownList.DataSource = dv;
            TagDropDownList.DataTextField = "TagTypeField";
            TagDropDownList.DataValueField = "TagIdField";

            // Bind the data to the control.
            TagDropDownList.DataBind();

            // Set the default selected item.
            TagDropDownList.SelectedValue = s;
        }
        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            if (SessionManager.IsUserAuthenticated(Context))
            {
                IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
                IPhotoService photoService = iocManager.Resolve<IPhotoService>();

                long photoId = long.Parse(Request.Params.Get("photo"));

                long tagId = long.Parse(TagDropDownList.SelectedItem.Value);

                photoService.DeleteTag(tagId, photoId);


                Response.Redirect(Response.
                            ApplyAppPathModifier("/Pages/Photo/PhotoDetails.aspx" + "?photo=" + photoId));
            }
        }

        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            if (SessionManager.IsUserAuthenticated(Context))
            {
                long photoId = long.Parse(Request.Params.Get("photo"));


                IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
                IPhotoService photoService = iocManager.Resolve<IPhotoService>();


                photoService.AddPhotoTag(photoService.AddTag(tagBody.Text), photoId);

                Response.Redirect(
                   Response.ApplyAppPathModifier("~/Pages/Photo/PhotoDetails.aspx?photo=" + photoId.ToString()));

            }
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