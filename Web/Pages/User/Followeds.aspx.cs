using Es.Udc.DotNet.PracticaMad.Model.Services.UserService.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Web.Security;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMad.Model.Services.UserService;
using Es.Udc.DotNet.PracticaMad.Model.PhotoService;
using userProfile = Es.Udc.DotNet.PracticaMad.Model.UserProfile;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.User
{
    public partial class Followeds : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            long userId = -1;
            try
            {
                userId = SessionManager.GetUserSession(Context).UserProfileId;
            }
            catch (NullReferenceException) {
                Response.Redirect("~/Pages/MainPage.aspx");
            }
            lnkBack.NavigateUrl = "~/Pages/MainPage.aspx";

            /* Get the Service */
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IUserService userService = iocManager.Resolve<IUserService>();

            List<userProfile> followeds = userService.GetFolloweds(userId);

                gvFollowers.DataSource = followeds;
                gvFollowers.DataBind();

        }


        protected void gvFollowers_RowCreated(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Visible = false;
        }

   
    }
}