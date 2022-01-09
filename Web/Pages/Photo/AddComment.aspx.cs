using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMad.Model.PhotoService;
using Es.Udc.DotNet.PracticaMad.Model.CommentService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;


namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Photo
{
    public partial class AddComment : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                long photoId = long.Parse(Request.Params.Get("photo"));

                commentBody.Visible = false;
                btnAddComment.Visible = false;

                lnkBack.NavigateUrl = "~/Pages/Photo/PhotoDetails.aspx?photo=" + photoId.ToString();

                if (!SessionManager.IsUserAuthenticated(Context))
                {
                    lblUnlogedUser.Visible = true;
                    commentBody.Visible = false;
                    btnAddComment.Visible = false;
                    return;
                }
                /* Get the Service */
                IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
                ICommentService commentService = iocManager.Resolve<ICommentService>();

                commentService.FindCommentByPhotoAndUser(photoId,
                        SessionManager.GetUserSession(Context).UserProfileId);

                lblCommented.Visible = true;
            }
            catch (ArgumentNullException)
            {
                lblNoPhoto.Visible = true;
                commentBody.Visible = false;
                btnAddComment.Visible = false;
                lnkBack.NavigateUrl = "~/Pages/Photo/PhotoSearch.aspx";
            }
            catch (InstanceNotFoundException)
            {
                commentBody.Visible = true;
                btnAddComment.Visible = true;
            }
        }

        protected void BtnAddCommentClick(object sender, EventArgs e)
        {
            if (commentBody.Text == string.Empty)
            {
                return;
            }

            if (SessionManager.IsUserAuthenticated(Context))
            {
                long photoId = long.Parse(Request.Params.Get("photo"));

                /* Get the Service */
                IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
               //IPhotoService photoService = iocManager.Resolve<IPhotoService>();
                ICommentService commentService = iocManager.Resolve<ICommentService>();

                commentService.AddComment(photoId, SessionManager.GetUserSession(Context).UserProfileId,
                      commentBody.Text/*, tags*/);

                Response.Redirect(
                   Response.ApplyAppPathModifier("~/Pages/Photo/PhotoComments.aspx?photo=" + photoId.ToString()));

            }
        }
    }
}