using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMad.Model;
using Es.Udc.DotNet.PracticaMad.Model.CommentService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;


namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Photo
{
    public partial class ModifyComment : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    long commentId = long.Parse(Request.Params.Get("comment"));

                    /* Get the Service */
                    IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
                    ICommentService commentService = iocManager.Resolve<ICommentService>();

                    Comment comment = null;


                    try
                    {
                        comment = commentService.FindCommentById(commentId);
                        string url =
                                "/Pages/Photo/PhotoComments.aspx?photo=" + comment.photoId.ToString();

                        hlReturnToDetails.NavigateUrl = Response.ApplyAppPathModifier(url);
                        hlReturnToDetails.Visible = true;
                    }
                    catch (InstanceNotFoundException)
                    {
                        lblNoComment.Visible = true;
                        commentBody.Visible = false;
                        btnEditComment.Visible = false;

                        return;
                    }

                    if (!SessionManager.IsUserAuthenticated(Context) ||
                        SessionManager.GetUserSession(Context).UserProfileId != comment.userId)
                    {
                        lblUnlogedUser.Visible = true;
                        commentBody.Visible = false;
                        btnEditComment.Visible = false;

                        return;
                    }

                    commentBody.Text = comment.commentDescription;

                }
                catch (ArgumentNullException)
                {
                    lblNoComment.Visible = true;
                    commentBody.Visible = false;
                    btnEditComment.Visible = false;
                }
            }
        }

        protected void BtnEditCommentClick(object sender, EventArgs e)
        {
            if (commentBody.Text == string.Empty)
            {
                return;
            }

            if (SessionManager.IsUserAuthenticated(Context))
            {
                long commentId = long.Parse(Request.Params.Get("comment"));
                /* Get the Service */
                IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
                ICommentService commentService = iocManager.Resolve<ICommentService>();

                
                commentService.UpdateComment(commentId, commentBody.Text);
                

                long photoId = commentService.FindCommentById(commentId).photoId;

                Response.Redirect(
                   Response.ApplyAppPathModifier("~/Pages/Photo/PhotoComments.aspx?photo=" + photoId.ToString()));
            }
        }
    }
}