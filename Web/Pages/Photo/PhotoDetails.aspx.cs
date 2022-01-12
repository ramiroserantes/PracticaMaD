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
using System.Collections;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Photo
{
    public partial class PhotoDetails : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblPhotoError.Visible = false;
            TablePhotoInfo.Visible = false;
            hlComments.Visible = true;
            hlAddComment.Visible = true;

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

            PracticaMad.Model.Photo photo = photoService.FindPhoto(photoId);
            List<PracticaMad.Model.Tag> tags = photoService.FindByPhotoTags(photoId);

            String tagsString = "";

            foreach (PracticaMad.Model.Tag t in tags)
            {
                tagsString = tagsString + " " + t.tagName.ToString();
            }

            long userId;

            try
            {
                userId = SessionManager.GetUserSession(Context).UserProfileId;
            }
            catch (NullReferenceException)
            {
                userId = -1;
            }

            if (photoService.FindPhoto(photo.photoId).UserProfile.userId != userId)
            {
                btnDelete.Visible = false;
            }
            else
            {
                btnDelete.Visible = true;
            }

            cellPhotoID.Text = photo.photoId.ToString();
            cellPhotoTitle.Text = photo.title;
            cellPhotoDescription.Text = photo.photoDescription;
            cellCategoryType.Text = photo.Category.categoryType;
            cellPhotoDate.Text = photo.photoDate.ToString("d/M/yyyy");
            cellPhotoDia.Text = photo.f.ToString();
            cellPhotoExhi.Text = photo.t.ToString();
            cellPhotoIso.Text = photo.iso;
            cellPhotoUser.Text = photo.UserProfile.loginName;
            cellPhotoBalance.Text = photo.wb.ToString();
            cellPhotoLikes.Text = photo.UserProfile1.Count.ToString();

            cellPhotoTags.Text = tagsString;

            hlComments.NavigateUrl =
                "~/Pages/Photo/PhotoComments.aspx" +
                "?photo=" + photo.photoId;



            if (SessionManager.IsUserAuthenticated(Context))
            {
                try
                {
                    commentService.FindCommentByPhotoAndUser(photoId,
                        SessionManager.GetUserSession(Context).UserProfileId);

                    lblDash1.Visible = true;
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
        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IPhotoService photoService = iocManager.Resolve<IPhotoService>();



            long userId = SessionManager.GetUserSession(Context).UserProfileId;
            long id = Convert.ToInt64(cellPhotoID.Text);

            photoService.DeletePhoto(id, userId);


            Response.Redirect(Response.
                        ApplyAppPathModifier("/Pages/MainPage.aspx"));
        }

        protected void BtnLikeClick(object sender, EventArgs e)
        {
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IPhotoService photoService = iocManager.Resolve<IPhotoService>();
            long userId = -1;
            try
            {
                userId = SessionManager.GetUserSession(Context).UserProfileId;
            }
            catch (NullReferenceException)
            {
                Response.Redirect(Response.
                       ApplyAppPathModifier("~/Pages/User/Authentication.aspx"));
            }
            long photoId = Convert.ToInt64(cellPhotoID.Text);

            photoService.GenerateLike(userId, photoId);


            Response.Redirect(Response.
                        ApplyAppPathModifier("/Pages/Photo/PhotoDetails.aspx" + "?photo=" + photoId));
        }
        protected void BtnRemoveLikeClick(object sender, EventArgs e)
        {
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IPhotoService photoService = iocManager.Resolve<IPhotoService>();
            long userId = -1;
            try
            {
                userId = SessionManager.GetUserSession(Context).UserProfileId;
            }
            catch (NullReferenceException) {
                Response.Redirect(Response.
                       ApplyAppPathModifier("~/Pages/User/Authentication.aspx"));
            }
            
            long photoId = Convert.ToInt64(cellPhotoID.Text);

            photoService.DeleteLike(userId, photoId);

            Response.Redirect(Response.
                        ApplyAppPathModifier("/Pages/Photo/PhotoDetails.aspx" + "?photo=" + photoId));
        }

        protected void BtnTags_Click(object sender, EventArgs e)
        {
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IPhotoService photoService = iocManager.Resolve<IPhotoService>();

            long id = Convert.ToInt64(cellPhotoID.Text);

            Response.Redirect(Response.
                        ApplyAppPathModifier("/Pages/Photo/ModifyTags.aspx" + "?photo=" + id));
        }
    }
}