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
    public class IPhotoServiceTest
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
        private Photo CreatePhoto(string userLogin, string title, string photoDescription, System.DateTime photoDate,
                long f, long t, string iso, long wb, long categoryId, long userId)
        {

            Photo photo = new Photo
            {
                userName = userLogin,
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
                country = "ES",
            };

            userDao.Create(user);

            return user;
        }


        private Tag CreateTag(string tagName)
        {
            Tag tag = new Tag
            {
                tagName = tagName,
            };

            tagDao.Create(tag);

            return tag;
        }

        /// <summary>
        /// A test for UpdatePhoto
        /// </summary>
        [TestMethod]
        public void UpdatePhotoTest()
        {
            using (var scope = new TransactionScope())
            {
                var user = CreateUser("loginNameTest1");
                var category = CreateCategory(categoryType);
                var photo = CreatePhoto(user.loginName, title, photoDescription, photoDate,
                    f, t, iso, wb, category.categoryId, user.userId);
                var photoFind = photoDao.Find(photo.photoId);

                // Check data
                Assert.AreEqual(title, photoFind.title);
                Assert.AreEqual(photoDescription, photoFind.photoDescription);
                Assert.AreEqual(photoDate, photoFind.photoDate);
                Assert.AreEqual(f, photoFind.f);
                Assert.AreEqual(t, photoFind.t);
                Assert.AreEqual(iso, photoFind.iso);
                Assert.AreEqual(wb, photoFind.wb);
                Assert.AreEqual(category.categoryId, photoFind.categoryId);
                Assert.AreEqual(user.userId, photoFind.userId);

                string titulo = "update1";
                string descripcion = "update2";

                photoService.UpdatePhotoDetails(photo.photoId, titulo, descripcion);
                photoFind = photoDao.Find(photo.photoId);

                // Check data
                Assert.AreEqual(titulo, photoFind.title);
                Assert.AreEqual(descripcion, photoFind.photoDescription);
                Assert.AreEqual(photoDate, photoFind.photoDate);
                Assert.AreEqual(f, photoFind.f);
                Assert.AreEqual(t, photoFind.t);
                Assert.AreEqual(iso, photoFind.iso);
                Assert.AreEqual(wb, photoFind.wb);
                Assert.AreEqual(category.categoryId, photoFind.categoryId);
                Assert.AreEqual(user.userId, photoFind.userId);
            }
        }

        /// <summary>
        /// A test for FindAllFhotos
        /// </summary>
        [TestMethod]
        public void FindAllPhotosTest()
        {
            using (var scope = new TransactionScope())
            {
                var user = CreateUser("loginNameTest1");
                var category = CreateCategory(categoryType);
                List<Photo> photoList = new List<Photo>
                {
                    CreatePhoto(user.loginName, title, photoDescription, photoDate,
                    f,  t,  iso, wb, category.categoryId,  user.userId),

                    CreatePhoto(user.loginName, "title2", "photoDescription2", System.DateTime.Now,
                    3,  33,  "iso2", 333, category.categoryId,  user.userId),

                };

                var expectedPhotoList = photoService.FindAllPhotos();

                // Check data
                Assert.AreEqual(photoList.Count, expectedPhotoList.Photos.Count);

                for (int i = 0; i < photoList.Count; i++)
                {
                    Assert.AreEqual(photoList[i].userName, expectedPhotoList.Photos[i].userName);
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
        /// A test for FindAllPhotosByKeywordAndCategory.
        /// </summary>
        [TestMethod]
        public void FindAllPhotosByKeywordAndCategoryTest()
        {
            using (var scope = new TransactionScope())
            {
                var user = CreateUser("loginNameTest1");
                var category1 = CreateCategory(categoryType);
                var category2 = CreateCategory("category2");

                List<Photo> photoList = new List<Photo>
                {
                    CreatePhoto(user.loginName, title, photoDescription, photoDate,
                    f,  t,  iso, wb, category1.categoryId,  user.userId),

                    CreatePhoto(user.loginName, "title2", "photoDescription2", System.DateTime.Now,
                    3,  33,  "iso2", 333, category1.categoryId,  user.userId),

                };

                CreatePhoto(user.loginName, "ooo", "oooo", System.DateTime.Now,
                   3, 33, "iso2", 333, category2.categoryId, user.userId);

                CreatePhoto(user.loginName, "ooo", "oooo", System.DateTime.Now,
                   3, 33, "iso2", 333, category2.categoryId, user.userId);

                var expectedPhotoList = photoService.FindAllPhotosByCategoryAndKeyword("title", category1.categoryId);

                expectedPhotoList.Photos.Reverse();
                // Check data
                Assert.AreEqual(photoList.Count, expectedPhotoList.Photos.Count);
                for (int i = 0; i < photoList.Count; i++)
                {
                    Assert.AreEqual(photoList[i].userName, expectedPhotoList.Photos[i].userName);
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

        

        /// <summary>
        /// A test for AddTag.
        /// </summary>
        [TestMethod]
        public void AddTagTest()
        {
            using (var scope = new TransactionScope())
            {
                var user = CreateUser("loginNameTest1");

                var tagId = photoService.AddTag(tagName1);

                var tag = tagDao.Find(tagId);

                // Check data
                Assert.AreEqual(tagName1, tag.tagName);
            }
        }

        /// <summary>
        /// A test for FindAllTags.
        /// </summary>
        [TestMethod]
        public void FindAllTagsTest()
        {
            using (var scope = new TransactionScope())
            {
                var user = CreateUser("loginNameTest1");

                List<Tag> tagList = new List<Tag>
                {
                    CreateTag(tagName1),
                    CreateTag(tagName2)
                };

                TagBlock tagFoundList = photoService.FindAllTags();

                // Check data
                Assert.AreEqual(tagList.Count, tagFoundList.Tags.Count);

                for (int i = 0; i < tagList.Count; i++)
                {
                    Assert.AreEqual(tagList[i].tagId, tagFoundList.Tags[i].tagId);
                    Assert.AreEqual(tagList[i].tagName, tagFoundList.Tags[i].tagName);
                }

                Assert.IsFalse(tagFoundList.ExistMoreTags);
            }
        }

       
        /// <summary>
        /// A test for FindAllPhotoLikes.
        /// </summary>
        [TestMethod]
        public void FindAllPhotoLikesTest()
        {
            using (var scope = new TransactionScope())
            {
                var user = CreateUser("loginNameTest1");
                var user2 = CreateUser("loginNameTest2");
                var user3 = CreateUser("loginNameTest3");

                var category = CreateCategory(categoryType);
                var photo = CreatePhoto(user.loginName, title, photoDescription, photoDate,
                    f, t, iso, wb, category.categoryId, user.userId);

                photoService.GenerateLike(user.userId, photo.photoId);
                photoService.GenerateLike(user2.userId, photo.photoId);
                photoService.GenerateLike(user3.userId, photo.photoId);

                Assert.AreEqual(3, photoService.GetPhotoLikes(photo.photoId));
               
            }
        }

        /// <summary>
        /// A test for DeletePhotoLikesTest.
        /// </summary>
        [TestMethod]

        public void DeletePhotoLikesTest()
        {
            using (var scope = new TransactionScope())
            {
                var user = CreateUser("loginNameTest1");
                var user2 = CreateUser("loginNameTest2");
                var user3 = CreateUser("loginNameTest3");

                var category = CreateCategory(categoryType);
                var photo = CreatePhoto(user.loginName, title, photoDescription, photoDate,
                    f, t, iso, wb, category.categoryId, user.userId);

                photoService.GenerateLike(user.userId, photo.photoId);
                photoService.GenerateLike(user2.userId, photo.photoId);
                photoService.GenerateLike(user3.userId, photo.photoId);

                photoService.DeleteLike(user.userId, photo.photoId);
                photoService.DeleteLike(user2.userId, photo.photoId);
               
                Assert.AreEqual(1, photoService.GetPhotoLikes(photo.photoId));

            }
        }

        /// <summary>
        /// A test for UploadPhoto.
        /// </summary>
        [TestMethod]
        public void UploadPhotoTest()
        {
            using (var scope = new TransactionScope())
            {
             
           
                string path = "C:/resourceTest/copo.jpg";
                var user = CreateUser("loginNameTest1");
                var category = CreateCategory(categoryType);
                var image = Image.FromFile(path);
                var photoId = photoService.UploadPhoto(user.loginName, title, photoDescription, f, t, iso, wb,
                    category.categoryId, user.userId, image);

                var findPhoto = photoDao.Find(photoId);

                Assert.AreEqual(findPhoto.userName, user.loginName);
                Assert.AreEqual(findPhoto.photoDate.Date, System.DateTime.Now.Date);
                Assert.AreEqual(findPhoto.title, title);
                Assert.AreEqual(findPhoto.photoDescription, photoDescription);
                Assert.AreEqual(findPhoto.photoDate.Date, System.DateTime.Now.Date);
                Assert.AreEqual(findPhoto.f, f);
                Assert.AreEqual(findPhoto.t, t);
                Assert.AreEqual(findPhoto.iso, iso);
                Assert.AreEqual(findPhoto.wb, wb);
                Assert.AreEqual(findPhoto.link, "C:/EntregaMaD/PracticaMaD/Web/Images/" + findPhoto.photoId.ToString() + ".jpg");
                Assert.AreEqual(findPhoto.categoryId, category.categoryId);
                Assert.AreEqual(findPhoto.userId, user.userId);

                File.Delete(findPhoto.link);
            }
        }

        /// <summary>
        /// A test for DeletePhoto.
        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void DeletePhotoTest()
        {
            using (var scope = new TransactionScope())
            {
                string path = "C:/resourceTest/copo.jpg";
                var user1 = CreateUser("loginNameTest1");
                var category = CreateCategory(categoryType);
                var image = Image.FromFile(path);
                var photoId = photoService.UploadPhoto(user1.loginName, title, photoDescription, f, t, iso, wb,
                    category.categoryId, user1.userId, image);

                var findPhoto = photoDao.Find(photoId);

                Assert.AreEqual(findPhoto.photoDescription, photoDescription);

                photoService.DeletePhoto(photoId, user1.userId);
                photoDao.Find(photoId);

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
