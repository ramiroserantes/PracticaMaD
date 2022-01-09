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
        }
    }
}