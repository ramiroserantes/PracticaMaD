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
using Es.Udc.DotNet.PracticaMad.Model.Services.UserService;
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

                commentBody.Visible = true;
                btnAddComment.Visible = true;

                lnkBack.NavigateUrl = "~/Pages/Photo/PhotoDetails.aspx?photo=" + photoId.ToString();

                if (!SessionManager.IsUserAuthenticated(Context))
                {
                    lblUnlogedUser.Visible = true;
                    commentBody.Visible = false;
                    btnAddComment.Visible = false;
                    return;
                }
               
                IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
                ICommentService commentService = iocManager.Resolve<ICommentService>();

                commentService.FindCommentByPhotoAndUser(photoId,
                        SessionManager.GetUserSession(Context).UserProfileId);

                /*lblCommented.Visible = true;*/
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

                
                IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
                ICommentService commentService = iocManager.Resolve<ICommentService>();
                IUserService userService = iocManager.Resolve<IUserService>();

                commentService.AddComment(SessionManager.GetUserSession(Context).LoginName, photoId, SessionManager.GetUserSession(Context).UserProfileId,
                      commentBody.Text);

                Response.Redirect(
                   Response.ApplyAppPathModifier("~/Pages/Photo/PhotoComments.aspx?photo=" + photoId.ToString()));

            }
        }
    }
}