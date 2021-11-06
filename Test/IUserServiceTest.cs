using Es.Udc.DotNet.PracticaMad.Model.Services.UserService;
using Es.Udc.DotNet.PracticaMad.Model.UserProfileDao;
using Es.Udc.DotNet.PracticaMad.Model.Services.UserService.Util;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System;
using System.Transactions;
using Es.Udc.DotNet.PracticaMad.Model.Services.UserService.Exceptions;
using Es.Udc.DotNet.PracticaMad.Model.UserService;
using Es.Udc.DotNet.PracticaMad.Model;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMad.Test
{
    [TestClass]
    public class IUserServiceTest
    {

        private const string loginName = "loginNameTest";

        private const string clearPassword = "password";
        private const string firstName = "name";
        private const string lastName = "lastName";
        private const string email = "user@udc.es";
        private const string lenguage = "es";

        private const string loginName2 = "loginNameTest2";

        private const string clearPassword2 = "password2";
        private const string firstName2 = "name2";
        private const string lastName2 = "lastName2";
        private const string email2 = "user2@udc.es";
        private const string lenguage2 = "es2";


        private const long NON_EXISTENT_USER_ID = -1;
        private static IKernel kernel;
        private static IUserService userService;
        private static IUserProfileDao userProfileDao;

        //private TransactionScope transaction;


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

        ///// <summary>
        /////A test for Login with clear password
        /////</summary>
        [TestMethod]
        public void LoginClearPasswordTest()
        {
            using (var scope = new TransactionScope())
            {
                // Register user
                var userId = userService.RegisterUser(loginName, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, lenguage));

                var expected = new LoginResult(userId, firstName,
                    PasswordEncrypter.Crypt(clearPassword), lenguage);

                // Login with clear password
                var actual =
                    userService.Login(loginName,
                        clearPassword, false);

                // Check data
                Assert.AreEqual(expected, actual);
            }
        }

        ///// <summary>
        /////A test for Login with encrypted password
        /////</summary>
        [TestMethod]
        public void LoginEncryptedPasswordTest()
        {
            using (var scope = new TransactionScope())
            {
                // Register user
                var userId = userService.RegisterUser(loginName, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, lenguage));

                var expected = new LoginResult(userId, firstName,
                    PasswordEncrypter.Crypt(clearPassword), lenguage);

                // Login with encrypted password
                var obtained =
                    userService.Login(loginName,
                        PasswordEncrypter.Crypt(clearPassword), true);

                // Check data
                Assert.AreEqual(expected, obtained);

            }
        }

        /// <summary>
        /// A test for FindUserProfileDetails
        /// </summary>
        [TestMethod]
        public void FindUserProfileDetailsTest()
        {
            using (var scope = new TransactionScope())
            {
                var expected =
                    new UserProfileDetails(firstName, lastName, email, lenguage);

                var userId =
                    userService.RegisterUser(loginName, clearPassword, expected);

                var obtained =
                    userService.FindUserProfileDetails(userId);

                // Check data
                Assert.AreEqual(expected, obtained);

            }
        }

        /// <summary>
        /// A test for registering a user that already exists in the database
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(DuplicateInstanceException))]
        public void RegisterDuplicatedUserTest()
        {
            using (var scope = new TransactionScope())
            {
                // Register user
                userService.RegisterUser(loginName, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, lenguage));

                // Register the same user
                userService.RegisterUser(loginName, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, lenguage));

            }
        }

        ///// <summary>
        /////A test for Login with incorrect password
        /////</summary>
        [TestMethod]
        [ExpectedException(typeof(IncorrectPasswordException))]
        public void LoginIncorrectPasswordTest()
        {
            using (var scope = new TransactionScope())
            {
                // Register user
                var userId = userService.RegisterUser(loginName, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, lenguage));

                // Login with incorrect (clear) password
                var actual =
                    userService.Login(loginName, clearPassword + "X", false);

            }
        }

        ///// <summary>
        /////A test for Login with a non-existing user
        /////</summary>
        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void LoginNonExistingUserTest()
        {
            // Login for a user that has not been registered
            var actual =
                userService.Login(loginName, clearPassword, false);
        }

        /// <summary>
        /// A test for FindUserProfileDetails when the user does not exist
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void FindUserProfileDetailsForNonExistingUserTest()
        {
            userService.FindUserProfileDetails(NON_EXISTENT_USER_ID);
        }

        /// <summary>
        /// A test for UpdateUserProfileDetails
        /// </summary>
        [TestMethod]
        public void UpdateUserProfileDetailsTest()
        {
            using (var scope = new TransactionScope())
            {
                // Register user and update profile details
                var userId = userService.RegisterUser(loginName, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, lenguage));

                var expected =
                    new UserProfileDetails(firstName + "X", lastName + "X",
                        email + "X", "XX");

                userService.UpdateUserProfileDetails(userId, expected);

                var obtained =
                    userService.FindUserProfileDetails(userId);

                // Check changes
                Assert.AreEqual(expected, obtained);

            }
        }

        /// <summary>
        /// A test for UpdateUserProfileDetails when the user does not exist
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void UpdateUserProfileDetailsForNonExistingUserTest()
        {
            using (var scope = new TransactionScope())
            {
                userService.UpdateUserProfileDetails(NON_EXISTENT_USER_ID,
                    new UserProfileDetails(firstName, lastName, email, lenguage));
            }
        }

        /// <summary>
        /// A test for ChangePassword
        /// </summary>
        [TestMethod]
        public void ChangePasswordTest()
        {
            using (var scope = new TransactionScope())
            {
                // Register user
                var userId = userService.RegisterUser(loginName, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, lenguage));

                // Change password
                var newClearPassword = clearPassword + "X";
                userService.ChangePassword(userId, clearPassword, newClearPassword);

                // Try to login with the new password. If the login is correct, then the password
                // was successfully changed.
                userService.Login(loginName, newClearPassword, false);
            }
        }
        /// <summary>
        /// A test for ChangePassword entering a wrong old password
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(IncorrectPasswordException))]
        public void ChangePasswordWithIncorrectPasswordTest()
        {
            using (var scope = new TransactionScope())
            {
                // Register user
                var userId = userService.RegisterUser(loginName, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, lenguage));

                // Change password
                var newClearPassword = clearPassword + "X";
                userService.ChangePassword(userId, clearPassword + "Y", newClearPassword);

            }
        }

        /// <summary>
        /// A test for ChangePassword when the user does not exist
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void ChangePasswordForNonExistingUserTest()
        {
            userService.ChangePassword(NON_EXISTENT_USER_ID,
                clearPassword, clearPassword + "X");
        }

        /// <summary>
        /// A test to check if a valid user loginName is found
        /// </summary>
        [TestMethod]
        public void UserExistsForValidUser()
        {
            using (var scope = new TransactionScope())
            {
                // Register user
                userService.RegisterUser(loginName, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, lenguage));

                bool userExists = userService.UserExists(loginName);

                Assert.IsTrue(userExists);
            }
        }

        /// <summary>
        /// A test to check if a not valid user loginame is found
        /// </summary>
        [TestMethod]
        public void UserExistsForNotValidUser()
        {
            using (var scope = new TransactionScope())
            {
                String invalidLoginName = loginName + "_someFakeUserSuffix";

                bool userExists = userService.UserExists(invalidLoginName);

                Assert.IsFalse(userExists);
            }
        }

        [TestMethod]
        public void RegisterUserFollower()
        {
            using (var scope = new TransactionScope())
            {
                // Register user1
                long userId1 = userService.RegisterUser(loginName, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, lenguage));

                // Register user2
                long userId2 = userService.RegisterUser(loginName2, clearPassword2,
                    new UserProfileDetails(firstName2, lastName2, email2, lenguage2));

                userService.FollowUser(userId1, userId2);

                List<UserProfile> expectedFollowers = userProfileDao.FindByFollower(userId2);

                Assert.AreEqual(1, expectedFollowers.Count);
                Assert.AreEqual(1, userProfileDao.FindByFollowed(userId1));

                for (int i = 0; i < expectedFollowers.Count; i++)
                {
                    Assert.AreEqual(userId1, expectedFollowers[i].userId);
                }

            }
        }

        [TestMethod]
        public void RegisterUserFollowed()
        {
            using (var scope = new TransactionScope())
            {
                // Register user1
                long userId1 = userService.RegisterUser(loginName, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, lenguage));

                // Register user2
                long userId2 = userService.RegisterUser(loginName2, clearPassword2,
                    new UserProfileDetails(firstName2, lastName2, email2, lenguage2));

                userService.FollowUser(userId1, userId2);

                List<UserProfile> expectedFolloweds = userProfileDao.FindByFollowed(userId1);

                Assert.AreEqual(1, expectedFolloweds.Count);

                for (int i = 0; i < expectedFolloweds.Count; i++)
                {
                    Assert.AreEqual(userId2, expectedFolloweds[i].userId);
                }

            }
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
