using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.PracticaMad.Model.Services.UserService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.PracticaMad.Model.PhotoService;
using Es.Udc.DotNet.PracticaMad.Model;

using Es.Udc.DotNet.ModelUtil.IoC;
using modelPhoto = Es.Udc.DotNet.PracticaMad.Model.Photo;
using System.Data.SqlClient;
using System.Configuration;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Photo
{
    public partial class Explore : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            int startIndex, count;
            string selectedValue = "-1";

            lnkPrevious.Visible = false;
            lnkNext.Visible = false;
            lblNoPhotos.Visible = false;
          


            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IPhotoService photoService = iocManager.Resolve<IPhotoService>();

           
            try
            {
                startIndex = int.Parse(Request.Params.Get("startIndex"));
            }
            catch (ArgumentNullException)
            {
                startIndex = 0;
            }

            try
            {
                count = int.Parse(Request.Params.Get("count"));
            }
            catch (ArgumentNullException)
            {
                count = 2;
            }

            /* Get keyword */
            string keyword = Request.Params.Get("keyword");

            if (keyword != null)
            {
                txtSearch.Text = keyword;

                /* Get category id */
                try
                {
                    long catId = long.Parse(Request.Params.Get("category"));

                    selectedValue = Request.Params.Get("category");

                    PhotoBlock photoBlock = photoService.FindAllPhotosByCategoryAndKeyword(keyword, catId, startIndex, count);

                    if (photoBlock.Photos.Count == 0)
                    {
                        lblNoPhotos.Visible = true;
                    }
                    else
                    {
                        gvPhotos.DataSource = photoBlock.Photos;
                        gvPhotos.DataBind();

                        /* "Previous" link */
                        if ((startIndex - count) >= 0)
                        {
                            string url =
                                "/Pages/Photo/Explore.aspx" + "?keyword=" + keyword + "&category=" + catId +
                                "&startIndex=" + (startIndex - count) + "&count=" + count;

                            lnkPrevious.NavigateUrl = Response.ApplyAppPathModifier(url);
                            lnkPrevious.Visible = true;
                        }

                        /* "Next" link */
                        if (photoBlock.ExistMorePhotos)
                        {
                            string url =
                                "/Pages/Photo/Explore.aspx" + "?keyword=" + keyword + "&category=" + catId +
                                "&startIndex=" + (startIndex + count) + "&count=" + count;

                            lnkNext.NavigateUrl = Response.ApplyAppPathModifier(url);
                            lnkNext.Visible = true;
                        }
                    }
                }
                catch (ArgumentNullException)
                {

                    PhotoBlock photoBlock = photoService.FindAllPhotosByKeyword(keyword, startIndex, count);

                    if (photoBlock.Photos.Count == 0)
                    {
                        lblNoPhotos.Visible = true;
                    }
                    else
                    {
                       
                        gvPhotos.DataSource = photoBlock.Photos;
                        gvPhotos.DataBind();

                        /* "Previous" link */
                        if ((startIndex - count) >= 0)
                        {
                            string url =
                                "/Pages/Photo/Explore.aspx" + "?keyword=" + keyword +
                                "&startIndex=" + (startIndex - count) + "&count=" + count;

                            lnkPrevious.NavigateUrl = Response.ApplyAppPathModifier(url);
                            lnkPrevious.Visible = true;
                        }

                        /* "Next" link */
                        if (photoBlock.ExistMorePhotos)
                        {
                            string url =
                                "/Pages/Photo/Explore.aspx" + "?keyword=" + keyword +
                                "&startIndex=" + (startIndex + count) + "&count=" + count;

                            lnkNext.NavigateUrl = Response.ApplyAppPathModifier(url);
                            lnkNext.Visible = true;
                        }
                    }
                }
            }
            else
            {
                try
                {
                    long tagId = long.Parse(Request.Params.Get("tagId"));

                  
                    PhotoBlock photoBlock = photoService.FindAllPhotosByTag(tagId, startIndex, count);

                    if (photoBlock.Photos.Count == 0)
                    {
                        lblNoPhotos.Visible = true;
                    }
                    else
                    {
                        gvPhotos.DataSource = photoBlock.Photos;
                        gvPhotos.DataBind();

                        /* "Previous" link */
                        if ((startIndex - count) >= 0)
                        {
                            string url =
                                "/Pages/Photo/Explore.aspx" + "?tagId=" + tagId + "startIndex=" + (startIndex - count) +
                                "&count=" + count;

                            lnkPrevious.NavigateUrl = Response.ApplyAppPathModifier(url);
                            lnkPrevious.Visible = true;
                        }

                        /* "Next" link */
                        if (photoBlock.ExistMorePhotos)
                        {
                            string url =
                                "/Pages/Photo/Explore.aspx" + "?tagId=" + tagId + "startIndex=" + (startIndex + count) +
                                "&count=" + count;

                            lnkNext.NavigateUrl = Response.ApplyAppPathModifier(url);
                            lnkNext.Visible = true;
                        }
                    }
                }
                catch (ArgumentNullException) { 

                    PhotoBlock photoBlock = photoService.FindAllPhotos(startIndex, count);

                    if (photoBlock.Photos.Count == 0)
                    {
                        lblNoPhotos.Visible = true;
                    }
                    else
                    {
                        gvPhotos.DataSource = photoBlock.Photos;
                        gvPhotos.DataBind();


                        if ((startIndex - count) >= 0)
                        {
                            string url =
                                "/Pages/Photo/Explore.aspx" + "?startIndex=" + (startIndex - count) +
                                "&count=" + count;

                            lnkPrevious.NavigateUrl = Response.ApplyAppPathModifier(url);
                            lnkPrevious.Visible = true;
                        }

                        /* "Next" link */
                        if (photoBlock.ExistMorePhotos)
                        {
                            string url =
                                "/Pages/Photo/Explore.aspx" + "?startIndex=" + (startIndex + count) +
                                "&count=" + count;

                            lnkNext.NavigateUrl = Response.ApplyAppPathModifier(url);
                            lnkNext.Visible = true;
                        }
                    }
                }
            }

            if (!IsPostBack)
            {
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

     
    

        protected void BtnSearchClick(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string keyword = txtSearch.Text;
                long catId = long.Parse(CategoryDropDownList.SelectedValue);



                if (keyword == "")
                {
                    return;
                }

                if (catId > 0)
                {
                    Response.Redirect("/Pages/Photo/Explore.aspx" + "?keyword=" + keyword + "&category=" + catId);
                }
                else
                {
                    Response.Redirect("/Pages/Photo/Explore.aspx" + "?keyword=" + keyword);
                }
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
        protected void gvPhotos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
            
            }
        }

        protected void gvPhotos_RowCreated(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Visible = false;
        }

   
    }
}