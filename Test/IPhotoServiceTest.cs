using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMad.Model;
using Es.Udc.DotNet.PracticaMad.Model.CategoryDao;
using Es.Udc.DotNet.PracticaMad.Model.CommentDao;
using Es.Udc.DotNet.PracticaMad.Model.PhotoDao;
using Es.Udc.DotNet.PracticaMad.Model.PhotoService;
using Es.Udc.DotNet.PracticaMad.Model.Services.UserService;
using Es.Udc.DotNet.PracticaMad.Model.TagDao;
using Es.Udc.DotNet.PracticaMad.Model.UserProfileDao;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

/// <summary>
/// This is a test class for IProductServiceTest and is intended to contain all IProductServiceTest
/// Unit Tests
/// </summary>
namespace Es.Udc.DotNet.PracticaMad.Test
{
    [TestClass]
    public class IPhotoServiceTest
    {

        #region Variables


        private const string title = "title";
        private const string photoDescription = "description";
        private const string categoryName = "category1";
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
        private long CreateUser()
        {
            
            return userService.RegisterUser(loginName, clearPassword,
                        new UserProfileDetails(loginName, firstName, lastName, email, lenguage));

        }

        /// <summary>
        /// A test for FindAllProducts
        /// </summary>
        [TestMethod]
        public void FindAllPhotosTest()
        {
            using (var scope = new TransactionScope())
            {
                long userId = CreateUser();
                var category = CreateCategory(categoryName);
                List<Photo> photoList = new List<Photo>
                {
                    CreatePhoto(title, photoDescription, photoDate,
                    f,  t,  iso, wb, category.categoryId,  userId),

                    CreatePhoto("title2", "photoDescription2", System.DateTime.Now,
                    3,  33,  "iso2", 333, category.categoryId,  userId),

                };

                var expectedPhotoList = photoService.FindAllPhotos();

                // Check data
                Assert.AreEqual(photoList.Count, expectedPhotoList.Photos.Count);

                for (int i = 0; i < photoList.Count; i++)
                {
                    Assert.AreEqual(photoList[i].photoId, expectedPhotoList.Photos[i].photoId);
                    Assert.AreEqual(photoList[i].title, expectedPhotoList.Photos[i].title);
                    Assert.AreEqual(photoList[i].photoDescription, expectedPhotoList.Photos[i].photoDescription);
                    Assert.AreEqual(photoList[i].photoDate, expectedPhotoList.Photos[i].photoDate);
                    Assert.AreEqual(photoList[i].f, expectedPhotoList.Photos[i].f);
                    Assert.AreEqual(photoList[i].t, expectedPhotoList.Photos[i].t);
                    Assert.AreEqual(photoList[i].iso, expectedPhotoList.Photos[i].iso);
                    Assert.AreEqual(photoList[i].wb, expectedPhotoList.Photos[i].wb);
                    Assert.AreEqual(photoList[i].Category.categoryId, expectedPhotoList.Photos[i].Category.categoryId);
                    Assert.AreEqual(photoList[i].UserProfile.userId, expectedPhotoList.Photos[i].UserProfile.userId);

                }
            }
        }

        // <summary>
        /// A test for FindAllProductsByKeyword. With Category.
        /// </summary>
        [TestMethod]
        public void FindAllPhotosByKeywordAndCategoryTest()
        {
            using (var scope = new TransactionScope())
            {
                long userId = CreateUser();
                var category1 = CreateCategory(categoryName);
                var category2 = CreateCategory("category2");

                List<Photo> photoList = new List<Photo>
                {
                    CreatePhoto(title, photoDescription, photoDate,
                    f,  t,  iso, wb, category1.categoryId,  userId),

                    CreatePhoto("title2", "photoDescription2", System.DateTime.Now,
                    3,  33,  "iso2", 333, category1.categoryId,  userId),

                };

                CreatePhoto("ooo", "oooo", System.DateTime.Now,
                   3, 33, "iso2", 333, category2.categoryId, userId);

                CreatePhoto("ooo", "oooo", System.DateTime.Now,
                   3, 33, "iso2", 333, category2.categoryId, userId);

                var expectedPhotoList = photoService.FindAllPhotosByCategoryAndKeyword("title", category1.categoryId);

                expectedPhotoList.Photos.Reverse();
                // Check data
                Assert.AreEqual(photoList.Count, expectedPhotoList.Photos.Count);
                for (int i = 0; i < photoList.Count; i++)
                {
                    Assert.AreEqual(photoList[i].photoId, expectedPhotoList.Photos[i].photoId);
                    Assert.AreEqual(photoList[i].title, expectedPhotoList.Photos[i].title);
                    Assert.AreEqual(photoList[i].photoDescription, expectedPhotoList.Photos[i].photoDescription);
                    Assert.AreEqual(photoList[i].photoDate, expectedPhotoList.Photos[i].photoDate);
                    Assert.AreEqual(photoList[i].f, expectedPhotoList.Photos[i].f);
                    Assert.AreEqual(photoList[i].t, expectedPhotoList.Photos[i].t);
                    Assert.AreEqual(photoList[i].iso, expectedPhotoList.Photos[i].iso);
                    Assert.AreEqual(photoList[i].wb, expectedPhotoList.Photos[i].wb);
                    Assert.AreEqual(photoList[i].Category.categoryId, expectedPhotoList.Photos[i].Category.categoryId);
                    Assert.AreEqual(photoList[i].UserProfile.userId, expectedPhotoList.Photos[i].UserProfile.userId);

                }
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
