using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMad.Model;
using Es.Udc.DotNet.PracticaMad.Model.CommentService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.ModelUtil.IoC;
using modelPhoto = Es.Udc.DotNet.PracticaMad.Model.Photo;
using System.Data.SqlClient;
using System.Configuration;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Photo
{
    public partial class PhotoComments : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int startIndex, count;
            long photoId;

            lnkPrevious.Visible = false;
            lnkNext.Visible = false;
            lblNoComments.Visible = false;
            btnDelete.Visible = false;
            btnModify.Visible = false;

            try
            {
                photoId = long.Parse(Request.Params.Get("photoId"));
                btnDelete.Attributes["name"] = photoId.ToString();
            }
            catch (ArgumentNullException)
            {
                lblNoComments.Visible = true;
                lnkBack.NavigateUrl = "~/Pages/Photo/Explore.aspx";
                return;
            }

            lnkBack.NavigateUrl = "~/Pages/Photo/PhotoDetails.aspx?photoId=" + photoId.ToString();

            /* Get Start Index */
            try
            {
                startIndex = int.Parse(Request.Params.Get("startIndex"));
            }
            catch (ArgumentNullException)
            {
                startIndex = 0;
            }

            /* Get Count */
            try
            {
                count = int.Parse(Request.Params.Get("count"));
            }
            catch (ArgumentNullException)
            {
                count = 2;
            }

            /* Get the Service */
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            ICommentService commentService = iocManager.Resolve<ICommentService>();

            CommentBlock commentBlock =
                commentService.FindAllPhotoComments(photoId, startIndex, count);

            if (commentBlock.Comments.Count == 0)
            {
                lblNoComments.Visible = true;
                return;
            }

            gvPhotos.DataSource = commentBlock.Comments;
            gvPhotos.DataBind();

            /* "Previous" link */
            if ((startIndex - count) >= 0)
            {
                string url =
                    "/Pages/Photo/PhotoComments.aspx" + "?photo=" + photoId +
                    "&startIndex=" + (startIndex - count) + "&count=" + count;

                lnkPrevious.NavigateUrl = Response.ApplyAppPathModifier(url);
                lnkPrevious.Visible = true;
            }

            /* "Next" link */
            if (commentBlock.ExistMoreComments)
            {
                string url =
                    "/Pages/Photo/PhotoComments.aspx" + "?photo=" + photoId +
                    "&startIndex=" + (startIndex + count) + "&count=" + count;

                lnkNext.NavigateUrl = Response.ApplyAppPathModifier(url);
                lnkNext.Visible = true;
            }

            Comment comment = null;

            if (SessionManager.IsUserAuthenticated(Context))
            {
                try
                {
                    comment = commentService.FindCommentByPhotoAndUser(photoId,
                        SessionManager.GetUserSession(Context).UserProfileId);

                    cellOwnCommentId.Text = comment.commentId.ToString();
                    cellOwnCommentProfileName.Text = comment.UserProfile.loginName;
                    cellOwnCommentBody.Text = comment.commentDescription;
                    cellOwnCommentDate.Text = comment.commentDate.ToString("d/M/yyyy");

                    ownComment.Visible = true;
                    btnDelete.Visible = true;
                    btnModify.Visible = true;
                }
                catch (InstanceNotFoundException)
                {
                    ownComment.Visible = false;
                }
            }
            else
            {
                ownComment.Visible = false;
            }
        }

        protected void gvPhotos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                /* Get the Service */
                IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
                ICommentService commentService = iocManager.Resolve<ICommentService>();

                Comment comment = commentService.FindCommentById(long.Parse(e.Row.Cells[0].Text));

            }
        }

        protected void gvPhotos_RowCreated(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Visible = false;
        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            ICommentService commentService = iocManager.Resolve<ICommentService>();

            long photoId = long.Parse(btnDelete.Attributes["name"]);

            long id = Convert.ToInt64(cellOwnCommentId.Text);
            commentService.DeleteComment(id);


            Response.Redirect(Response.
                        ApplyAppPathModifier("/Pages/Photo/PhotoComments.aspx" + "?photo=" + photoId));
        }

        protected void BtnModify_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Pages/Photo/ModifyComment.aspx?comment=" + cellOwnCommentId.Text);
        }
    }
}
