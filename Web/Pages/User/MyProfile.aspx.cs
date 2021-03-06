using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMad.Model.Services.UserService;
using Es.Udc.DotNet.PracticaMad.Model.PhotoService;
using modelPhoto = Es.Udc.DotNet.PracticaMad.Model.Photo;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.User
{
    public partial class MyProfile : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            IIoCManager iocManager = (IIoCManager)Application["managerIoC"];
            IUserService userService = iocManager.Resolve<IUserService>();
            IPhotoService photoService = iocManager.Resolve<IPhotoService>();

            LblUserName.Text = SessionManager.GetUserSession(Context).FirstName;
            

            long userId = SessionManager.GetUserSession(Context).UserProfileId;

            int followeds = userService.GetFolloweds(userId).Count;
            int followers = userService.GetFollowers(userId).Count;

            LblFollowedText.Text = followeds.ToString();
            LblFollowersText.Text = followers.ToString();

            loadPhotos();
        }

        protected void loadPhotos()
        {
            IIoCManager iocManager = (IIoCManager)Application["managerIoC"];

            IPhotoService photoService = iocManager.Resolve<IPhotoService>();

            PhotoBlock photos = photoService.FindAllPhotosByUser(SessionManager.GetUserSession(Context).UserProfileId, 0, Global.numberImages);

            TableRow row;

            TableCell images, linkCell;
            Image imagenes;
            HyperLink link;

            foreach (modelPhoto photo in photos.Photos)
            {
                row = new TableRow();

                images = new TableCell();
                images.ID = "photoId";
                imagenes = new Image();
                imagenes.ImageUrl = "../../Images/" + photo.photoId.ToString() + ".jpg";
                imagenes.Width = 100;
                imagenes.Height = 100;
                images.Controls.Add(imagenes);
                row.Controls.Add(images);

                linkCell = new TableCell();
                link = new HyperLink();
                link.ID = "linkId";
                link.Text = "Ver imagen";
                link.NavigateUrl = "~/Pages/Photo/PhotoDetails.aspx?photo=" + photo.photoId.ToString();
                linkCell.Controls.Add(link);
                row.Controls.Add(linkCell);

                lclTableImages.Rows.Add(row);
            }


        }
    }

    }
