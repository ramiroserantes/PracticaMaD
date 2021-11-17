using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMad.Model;
using Es.Udc.DotNet.PracticaMad.Model.CategoryDao;
using Es.Udc.DotNet.PracticaMad.Model.CommentDao;
using Es.Udc.DotNet.PracticaMad.Model.PhotoDao;
using Es.Udc.DotNet.PracticaMad.Model.PhotoService;
using Es.Udc.DotNet.PracticaMad.Model.CommentService;
using Es.Udc.DotNet.PracticaMad.Model.Services.UserService;
using Es.Udc.DotNet.PracticaMad.Model.TagDao;
using Es.Udc.DotNet.PracticaMad.Model.UserProfileDao;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Transactions;
using System.Windows.Forms;

/// <summary>
/// This is a test class for IPhotoServiceTest and is intended to contain all IPhotoServiceTest
/// Unit Tests
/// </summary>
namespace Es.Udc.DotNet.PracticaMad.Test
{
    [TestClass]
    public class ICommentServiceTest
    {

        #region Variables


        private const string title = "title";
        private const string photoDescription = "description";
        private const string categoryType = "category1";
        private readonly System.DateTime photoDate = System.DateTime.Now;
        private const long f = 2;
        private const long t = 22;
        private const long wb = 222;
        private const string iso = "iso";


        private const string loginName = "loginNameTest";

        private const string clearPassword = "password";
        private const string firstName = "name";
        private const string lastName = "lastName";
        private const string email = "user@udc.es";
        private const string lenguage = "es";

        private const string commentBody = "comment1";

        private const string tagName1 = "tag1";
        private const string tagName2 = "tag2";

        private static IKernel kernel;
        private static IPhotoService photoService;
        private static ICategoryDao categoryDao;
        private static IPhotoDao photoDao;
        private static ICommentDao commentDao;
        private static ITagDao tagDao;
        private static IUserProfileDao userDao;
        private static IUserService userService;
        private static ICommentService commentService;


        /// <summary>
        /// Gets or sets the test context which provides information about and functionality for the
        /// current test run.
        /// </summary>
        public TestContext TestContext { get; set; }

        #endregion Variables

        private Category CreateCategory(string categoryType)
        {
            Category category = new Category
            {
                categoryType = categoryType
            };

            categoryDao.Create(category);

            return category;
        }
        private Photo CreatePhoto(string title, string photoDescription, System.DateTime photoDate,
                long f, long t, string iso, long wb, long categoryId, long userId)
        {

            Photo photo = new Photo
            {
                title = title,
                photoDescription = photoDescription,
                photoDate = photoDate,
                f = f,
                t = t,
                iso = iso,
                wb = wb,
                categoryId = categoryId,
                userId = userId
            };

            photoDao.Create(photo);

            return photo;
        }
        private UserProfile CreateUser(string loginName)
        {
            UserProfile user = new UserProfile
            {
                loginName = loginName,
                userPassword = "password",
                firstName = "name",
                lastName = "lastName",
                email = "user@udc.es",
                lenguage = "es",
            };

            userDao.Create(user);

            return user;
        }
        private Tag CreateTag(string tagName, long userId)
        {
            Tag tag = new Tag
            {
                tagName = tagName,
                userId = userId
            };

            tagDao.Create(tag);

            return tag;
        }     

        /// <summary>
        /// A test for AddComment.
        /// </summary>
        [TestMethod]
        public void AddCommentTest()
        {
            using (var scope = new TransactionScope())
            {
                var user = CreateUser("loginNameTest1");
                var category = CreateCategory(categoryType);
                var photo = CreatePhoto(title, photoDescription, photoDate,
                    f, t, iso, wb, category.categoryId, user.userId);


                var commentId = commentService.AddComment(photo.photoId, user.userId, commentBody);

                var findComment = commentDao.Find(commentId);

                // Check data
                Assert.AreEqual(photo.photoId, findComment.photoId);
                Assert.AreEqual(user.userId, findComment.userId);
                Assert.AreEqual(commentBody, findComment.commentDescription);
                Assert.AreEqual(System.DateTime.Now.Date, findComment.commentDate.Date);
            }
        }

        /// <summary>
        /// A test for DeleteComment.
        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void DeleteCommentTest()
        {
            using (var scope = new TransactionScope())
            {
                var user = CreateUser("loginNameTest1");
                var category = CreateCategory(categoryType);
                var photo = CreatePhoto(title, photoDescription, photoDate,
                    f, t, iso, wb, category.categoryId, user.userId);

                var commentId = commentService.AddComment(photo.photoId, user.userId, commentBody);

                var findComment = commentDao.Find(commentId);
                Assert.AreEqual(commentBody, findComment.commentDescription);

                commentService.DeleteComment(commentId);
                commentDao.Find(commentId);
            }
        }

        /// <summary>
        /// A test for UpdateComment.
        /// </summary>
        [TestMethod]
        public void UpdateCommentTest()
        {
            using (var scope = new TransactionScope())
            {
                var user = CreateUser("loginNameTest1");
                var category = CreateCategory(categoryType);
                var photo = CreatePhoto(title, photoDescription, photoDate,
                    f, t, iso, wb, category.categoryId, user.userId);

                var commentId = commentService.AddComment(photo.photoId, user.userId, commentBody);

                var findComment = commentDao.Find(commentId);

                // Check data
                Assert.AreEqual(photo.photoId, findComment.photoId);
                Assert.AreEqual(user.userId, findComment.userId);
                Assert.AreEqual(commentBody, findComment.commentDescription);
                Assert.AreEqual(System.DateTime.Now.Date, findComment.commentDate.Date);

                commentService.UpdateComment(commentId, "commentTest2");
                findComment = commentDao.Find(commentId);

                // Check data
                Assert.AreEqual("commentTest2", findComment.commentDescription);
            }
        }

        /// <summary>
        /// A test for FindAllPhotoComments.
        /// </summary>
        [TestMethod]
        public void FindAllPhotoCommentsTest()
        {
            using (var scope = new TransactionScope())
            {
                var user = CreateUser("loginNameTest1");
                var category = CreateCategory(categoryType);
                var photo = CreatePhoto(title, photoDescription, photoDate,
                    f, t, iso, wb, category.categoryId, user.userId);

                List<string> commentsBody = new List<string>
                {
                    commentBody,
                    "comment2"
                };

                List<long> commentsIds = new List<long>
                {
                    commentService.AddComment(photo.photoId, user.userId, commentsBody[0]),
                    commentService.AddComment(photo.photoId, user.userId, commentsBody[1])
                };

                CommentBlock listComments = commentService.FindAllPhotoComments(photo.photoId);

                // Check data
                Assert.AreEqual(commentsIds.Count, listComments.Comments.Count);


            }
        }

      

        #region Additional test attributes

        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext)
        {
            kernel = TestManager.ConfigureNInjectKernel();


            photoDao = kernel.Get<IPhotoDao>();
            categoryDao = kernel.Get<ICategoryDao>();
            commentDao = kernel.Get<ICommentDao>();
            tagDao = kernel.Get<ITagDao>();
            userDao = kernel.Get<IUserProfileDao>();

            photoService = kernel.Get<IPhotoService>();
            userService = kernel.Get<IUserService>();
            commentService = kernel.Get<ICommentService>();
        }

        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup]
        public static void MyClassCleanup()
        {
            TestManager.ClearNInjectKernel(kernel);
        }

        //Use TestInitialize to run code before running each test
        [TestInitialize]
        public void MyTestInitialize()
        {
        }

        //Use TestCleanup to run code after each test has run
        [TestCleanup]
        public void MyTestCleanup()
        {
        }

        #endregion Additional test attributes
    }
}
