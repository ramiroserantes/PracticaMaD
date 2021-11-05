using Es.Udc.DotNet.PracticaMad.Model.Services.UserService;
using Es.Udc.DotNet.PracticaMad.Model.UserProfileDao;
using Es.Udc.DotNet.PracticaMad.Model.Services.UserService.Util;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System;
using System.Transactions;
using Es.Udc.DotNet.PracticaMad.Model.Services.UserService.Exceptions;

namespace Es.Udc.DotNet.PracticaMad.Test
{
    [TestClass]
    public class IUserServiceTest
    {

        private const string loginName = "loginNameTest";

        private const string clearPassword = "password";
        //private const string rolUser = "administrator";
        private const string firstName = "name";
        private const string lastName = "lastName";
        //private const string shipAddress = "xanceda";
        private const string email = "user@udc.es";
        private const string lenguage = "es";
        private const long NON_EXISTENT_USER_ID = -1;
        private static IKernel kernel;
        private static IUserService userService;
        private static IUserProfileDao userProfileDao;

        private TransactionScope transaction;


        public TestContext TestContext { get; set; }


        [TestMethod]
        public void RegisterUserTest()
        {
            using (var scope = new TransactionScope())
            {
                // Register user and find profile
                var userId =
                    userService.RegisterUser(loginName, clearPassword,
                        new UserProfileDetails(loginName, firstName, lastName, email, lenguage));

                var userProfile = userProfileDao.Find(userId);

                // Check data
                Assert.AreEqual(userId, userProfile.userId);
                Assert.AreEqual(loginName, userProfile.loginName);
                Assert.AreEqual(PasswordEncrypter.Crypt(clearPassword), userProfile.userPassword);
                Assert.AreEqual(firstName, userProfile.firstName);
                Assert.AreEqual(lastName, userProfile.lastName);
                Assert.AreEqual(email, userProfile.email);
                Assert.AreEqual(lenguage, userProfile.lenguage);
                

            }
        }

        
        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext)
        {
            //kernel = TestManager.ConfigureNInjectKernel("./Modules/ninjectConfiguration.xml");
            kernel = TestManager.ConfigureNInjectKernel();

            userProfileDao = kernel.Get<IUserProfileDao>();
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
    }
}
