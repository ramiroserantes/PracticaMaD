using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.PracticaMad.Model.Services.UserService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.PracticaMad.Model.PhotoService;

using Es.Udc.DotNet.PracticaMad.Model.CommentService;

using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.ModelUtil.Exceptions;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Photo
{
    public partial class PhotoDetails: SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblPhotoError.Visible = false;
            TablePhotoInfo.Visible = false;
            hlComments.Visible = true;
            hlAddComment.Visible = true;
            hlUser.Visible = true;

            long photoId;

            /* Get photoId */
            try
            {
                photoId = long.Parse(Request.Params.Get("photo"));
            }
            catch (ArgumentNullException)
            {
                lblPhotoError.Visible = true;
                return;
            }

            TablePhotoInfo.Visible = true;

            /* Get the Service */
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IPhotoService photoService = iocManager.Resolve<IPhotoService>();
            ICommentService commentService = iocManager.Resolve<ICommentService>();

            /* Get Products Info */
            PracticaMad.Model.Photo photo = photoService.FindPhoto(photoId);

            cellPhotoID.Text = photo.photoId.ToString();
            cellPhotoTitle.Text = photo.title;
            cellCategoryType.Text = photo.Category.categoryType;
            cellPhotoDate.Text = photo.photoDate.ToString("d/M/yyyy");

            hlComments.NavigateUrl =
                "/Pages/Photo/PhotoComments.aspx" +
                "?photo=" + photo.photoId;


            if (SessionManager.IsUserAuthenticated(Context))
            {
                try
                {
                    commentService.FindCommentByPhotoAndUser(photoId,
                        SessionManager.GetUserSession(Context).UserProfileId);

                    lblDash1.Visible = false;
                }
                catch (InstanceNotFoundException)
                {
                    hlAddComment.Visible = true;
                }
            }
            else
            {
                hlAddComment.Visible = true;
            }

            hlAddComment.NavigateUrl = "~/Pages/Photo/AddComment.aspx" +
                    "?photo=" + photoId;

            if (photo.Comment.Count == 0)
            {
                hlComments.Visible = false;
                lblDash1.Visible = false;
            }
        }
    }
}