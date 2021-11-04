/*using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.PhotoDao;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using Es.Udc.DotNet.PracticaMaD.Model.CommentDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System;
using System.Collections.Generic;
using System.Transactions;
namespace Es.Udc.DotNet.PracticaMaD.Test
{
    [TestClass]
    public class ICommentDaoTest
    {

        #region Variables

        private static IKernel kernel;
        private static IPhotoDao photoDao;
        private static ICategoryDao categoryDao;
        private static ICommentDao commentDao;
        private static IUserProfileDao userProfileDao;

        // Variables used in several tests are initialized here
        private Category category;
        private Photo photo;
        private UserProfile user;

        private const string categoryType = "categoryTest";

        private const string title = "title";

        private System.DateTime date = System.DateTime.Now;

        private const string commentBody = "CommentTest";

        private TransactionScope transaction;

        private TestContext testContextInstance;

        #endregion Variables

        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        
        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            kernel = TestManager.ConfigureNInjectKernel();
            photoDao = kernel.Get<IPhotoDao>();
            categoryDao = kernel.Get<ICategoryDao>();
            commentDao = kernel.Get<ICommentDao>();
            userProfileDao = kernel.Get<IUserProfileDao>();
        }

        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            TestManager.ClearNInjectKernel(kernel);
        }

        //Use TestInitialize to run code before running each test
        [TestInitialize()]
        public void MyTestInitialize()
        {
            transaction = new TransactionScope();

            category = new Category();
            category.categoryType = categoryType;

            categoryDao.Create(category);

            photo = new Photo();
            photo.categoryId = category.categoryId;
            photo.title = title;
            photo.photoDate = date;


            photoDao.Create(photo);

            user = new UserProfile
            {
                loginName = "loginNameTest",
                userPassword = "password",
                firstName = "name",
                lastName = "lastName",
                email = "user@udc.es",
                internalization = "es"
            };

            userProfileDao.Create(user);
        }

        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            transaction.Dispose();
        }

        #endregion Additional test attributes

        [TestMethod]
        public void DAO_FindByPhotoIdOrderByCommentDate()
        {
            category = new Category();
            category.categoryType = categoryType;

            categoryDao.Create(category);

            photo = new Photo();
            photo.categoryId = category.categoryId;
            photo.title = title;
            photo.photoDate = date;


            photoDao.Create(photo);

            user = new UserProfile
            {
                loginName = "loginNameTest",
                userPassword = "password",
                firstName = "name",
                lastName = "lastName",
                email = "user@udc.es",
                internalization = "es"
            };

            userProfileDao.Create(user);

            int numberComments = 3;

            List<Comment> createdComments = new List<Comment>(numberComments);

            for (int i = 0; i < numberComments; i++)
            {
                var comment = new Comment();
                comment.commentDescription = commentBody;
                comment.commentDate = date;
                comment.photoId = photo.photoId;
                comment.userId = user.userId;
                commentDao.Create(comment);
                createdComments.Add(comment);
            }

            List<Comment> totalRetrievedComments = commentDao.FindByPhotoIdOrderByCommentDate(photo.photoId);
            
            Assert.AreEqual(numberComments, totalRetrievedComments.Count);

            for (int i = 0; i < numberComments; i++)
            {
                Assert.AreEqual(totalRetrievedComments[i], createdComments[i]);
            }
        }
    }
}*/

