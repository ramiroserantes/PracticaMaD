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
    public partial class MyProfile : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {            
            IIoCManager iocManager = (IIoCManager)Application["managerIoC"];
            IUserService userService = iocManager.Resolve<IUserService>();

            LblUserName.Text = SessionManager.GetUserSession(Context).FirstName;

            long userId = SessionManager.GetUserSession(Context).UserProfileId;

            int followeds = userService.GetFolloweds(userId).Count;
            int followers = userService.GetFollowers(userId).Count;

            LblFollowedText.Text = followeds.ToString();
            LblFollowersText.Text = followers.ToString();
        }
    }
}