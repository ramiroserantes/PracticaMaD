using Es.Udc.DotNet.PracticaMad.Model.CategoryDao;
using Es.Udc.DotNet.PracticaMad.Model.CommentDao;
using Es.Udc.DotNet.PracticaMad.Model.CommentService;
using Es.Udc.DotNet.PracticaMad.Model.PhotoDao;
using Es.Udc.DotNet.PracticaMad.Model.PhotoService;
using Es.Udc.DotNet.PracticaMad.Model.Services.UserService;
using Es.Udc.DotNet.PracticaMad.Model.TagDao;
using Es.Udc.DotNet.PracticaMad.Model.UserProfileDao;
using Ninject;
using System.Configuration;
using System.Data.Entity;

namespace Es.Udc.DotNet.PracticaMad.Test
{
    public class TestManager
    {
        /// <summary>
        /// Configures the n inject kernel.
        /// </summary>
        /// <returns></returns>
        public static IKernel ConfigureNInjectKernel()
        {
            NinjectSettings settings = new NinjectSettings() { LoadExtensions = true };

            IKernel kernel = new StandardKernel(settings);

            kernel.Bind<IUserProfileDao>().
                To<UserProfileDaoEntityFramework>();

            kernel.Bind<ICategoryDao>().
                 To<CategoryDaoEntityFramework>();

            kernel.Bind<ICommentDao>().
                 To<CommentDaoEntityFramework>();

            kernel.Bind<IPhotoDao>().
                 To<PhotoDaoEntityFramework>();

            kernel.Bind<ITagDao>().
                 To<TagDaoEntityFramework>();

            kernel.Bind<IUserService>().
                To<UserService>();

            kernel.Bind<IPhotoService>().
                To<PhotoService>();

            kernel.Bind<ICommentService>().
                To<CommentService>();

            string connectionString =
                ConfigurationManager.ConnectionStrings["practicamadEntitiesTest"].ConnectionString;

            kernel.Bind<DbContext>().
                ToSelf().
                InSingletonScope().
                WithConstructorArgument("nameOrConnectionString", connectionString);

            return kernel;
        }

        /// <summary>
        /// Configures the Ninject kernel from an external module file.
        /// </summary>
        /// <param name="moduleFilename">The module filename.</param>
        /// <returns>
        /// The NInject kernel
        /// </returns>
        public static IKernel ConfigureNInjectKernel(string moduleFilename)
        {
            NinjectSettings settings = new NinjectSettings() { LoadExtensions = true };
            IKernel kernel = new StandardKernel(settings);

            kernel.Load(moduleFilename);

            return kernel;
        }

        /// <summary>
        /// Clears the n inject kernel.
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        public static void ClearNInjectKernel(IKernel kernel)
        {
            kernel.Dispose();
        }
    }
}
