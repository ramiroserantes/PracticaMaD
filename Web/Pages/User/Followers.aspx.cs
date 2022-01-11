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
    public partial class Followers : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            IIoCManager iocManager = (IIoCManager)Application["managerIoC"];
            IUserService userService = iocManager.Resolve<IUserService>();

            long userId = SessionManager.GetUserSession(Context).UserProfileId;

            List<userProfile> followers = userService.GetFollowers(userId);


            TableRow row;

            TableCell images;
            TableCell linkCell;
            Image imagenes;
            HyperLink link;

            foreach (userProfile u in followers)
            {
                row = new TableRow();

                linkCell = new TableCell();
                link = new HyperLink();
                link.ID = "linkId";
                link.Text = followers[0].loginName;

                linkCell.Controls.Add(link);
                row.Controls.Add(linkCell);
            }
        }
    }
}