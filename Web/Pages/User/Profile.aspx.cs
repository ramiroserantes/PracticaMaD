using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMad.Model.Services.UserService;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.User
{
    public partial class Profile : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            IIoCManager iocManager = (IIoCManager)Application["managerIoC"];
            IUserService userService = iocManager.Resolve<IUserService>();

            /* Get LoginName passed as parameter in the request from
            * the previous page. Lanzar de esta forma ->
            *   String url = String.Format("./Profile.aspx?loginName={0}", loginName);
                Response.Redirect(Response.ApplyAppPathModifier(url));
            */
            string loginName = Request.Params.Get("loginName");
            long userId = userService.GetUserProfileId(loginName);

            UserProfileDetails userProfileDetails = userService.FindUserProfileDetails(userId);
            LblUserName.Text = userProfileDetails.FirstName;


            int followeds = userService.GetFolloweds(userId).Count;
            int followers = userService.GetFollowers(userId).Count;

            LblFollowedText.Text = followeds.ToString();
            LblFollowersText.Text = followers.ToString();

            if (SessionManager.IsUserAuthenticated(Context))
            {

                long authenticateUserId = SessionManager.GetUserSession(Context).UserProfileId;

                // If the user is authenticated and goes to his profile, redirects to MyProfile
                if (userId == authenticateUserId)
                {
                    Response.Redirect("./MyProfile.aspx");
                }
                else
                {
                    if (userService.IsAlreadyFollowing(authenticateUserId, userId))
                    {
                        BtnUnfollow.Visible = true;
                    }
                    else
                    {
                        BtnFollow.Visible = true;
                    }
                }

            }
        }

        protected void BtnFollow_Click(object sender, EventArgs e)
        {
            IIoCManager iocManager = (IIoCManager)Application["managerIoC"];
            IUserService userService = iocManager.Resolve<IUserService>();

            string loginName = Request.Params.Get("loginName");
            long userId = userService.GetUserProfileId(loginName);
            long authenticateUserId = SessionManager.GetUserSession(Context).UserProfileId;

            userService.FollowUser(authenticateUserId, userId);
            this.LblFollowersText.Text = userService.GetFollowers(userId).Count.ToString();
            this.BtnFollow.Visible = false;
            this.BtnUnfollow.Visible = true;
        }

        protected void BtnUnfollow_Click(object sender, EventArgs e)
        {
            IIoCManager iocManager = (IIoCManager)Application["managerIoC"];
            IUserService userService = iocManager.Resolve<IUserService>();

            string loginName = Request.Params.Get("loginName");
            long userId = userService.GetUserProfileId(loginName);
            long authenticateUserId = SessionManager.GetUserSession(Context).UserProfileId;

            userService.UnfollowUser(authenticateUserId, userId);
            this.LblFollowersText.Text = userService.GetFollowers(userId).Count.ToString();
            this.BtnUnfollow.Visible = false;
            this.BtnFollow.Visible = true;
        }
    }
}