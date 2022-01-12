using System;
using Es.Udc.DotNet.PracticaMad.Model.PhotoService;
using Es.Udc.DotNet.PracticaMad.Model;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web
{

    public partial class PracticaMaD : System.Web.UI.MasterPage
    {

        public static readonly String USER_SESSION_ATTRIBUTE = "userSession";

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!SessionManager.IsUserAuthenticated(Context))
            {

                
                if (lnkMyProfile != null)
                    lnkMyProfile.Visible = false;
                if (lblDash2 != null)
                    lblDash2.Visible = false;              
                if (lblDash3 != null)
                    lblDash3.Visible = false;
                if (lnkUpload != null)
                    lnkUpload.Visible = false;
                if (lblDash5 != null)
                    lblDash5.Visible = false;
                if (lnkLogout != null)
                    lnkLogout.Visible = false;

            }
            else
            {
                if (lblWelcome != null)                   
                    lblWelcome.Text =
                        GetLocalResourceObject("lblWelcome.Hello.Text").ToString()
                        + " " + SessionManager.GetUserSession(Context).FirstName;
                if (lblDash1 != null)
                    lblDash1.Visible = false;
                if (lnkAuthenticate != null)
                    lnkAuthenticate.Visible = false;
            }
            cloudTag();
        }

        protected void cloudTag()
        {

            HyperLink item;
            TableRow row;
            TableCell cell;

            int count = 10;
            int start = 0;

            TagBlock block;
            do
            {
                block = SessionManager.FindAllTags(start, count);
                row = new TableRow();

                foreach (Tag tag in block.Tags)
                {
                    cell = new TableCell();
                    item = new HyperLink();

                    item.Text = tag.tagName;
                    item.NavigateUrl = "~/Pages/Photo/Explore.aspx?tagId=" + tag.tagId;
                    cell.Controls.Add(item);
                    row.Cells.Add(cell);
                };

                tags.Rows.Add(row);
                start++;

            } while (block.ExistMoreTags);

        }

    }
}