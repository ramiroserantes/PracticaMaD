using System;
using System.Collections.Generic;
using System.Linq;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMad.Model.Services.UserService.Exceptions;
using Es.Udc.DotNet.PracticaMad.Model.Services.UserService.Util;
using Es.Udc.DotNet.PracticaMad.Model.UserProfileDao;
using Es.Udc.DotNet.PracticaMad.Model.UserService;
using Ninject;

namespace Es.Udc.DotNet.PracticaMad.Model.Services.UserService
{

    /// <seealso cref="Es.Udc.DotNet.PracticaMad.Model.Services.UserService.IUserService" />
    public class UserService : IUserService
    {

        /// <summary>
        /// Sets the user profile DAO.
        /// </summary>
        /// <value>
        /// The user profile DAO.
        /// </value>
        [Inject]
        public IUserProfileDao UserProfileDao { private get; set; }

        /// <summary>
        /// Changes the password.
        /// </summary>
        /// <param name="userProfileId">The user profile identifier.</param>
        /// <param name="oldClearPassword">The old clear password.</param>
        /// <param name="newClearPassword">The new clear password.</param>
        /// <exception cref="Es.Udc.DotNet.PracticaMad.Model.Services.UserService.Exceptions.IncorrectPasswordException"></exception>
        public void ChangePassword(long userProfileId, string oldClearPassword,
            string newClearPassword)
        {
            UserProfile userProfile = UserProfileDao.Find(userProfileId);
            String storedPassword = userProfile.userPassword;

            if (!PasswordEncrypter.IsClearPasswordCorrect(oldClearPassword,
                 storedPassword))
            {
                throw new IncorrectPasswordException(userProfile.loginName);
            }

            userProfile.userPassword =
            PasswordEncrypter.Crypt(newClearPassword);

            UserProfileDao.Update(userProfile);

        }

        /// <summary>
        /// Finds the user profile details.
        /// </summary>
        /// <param name="userProfileId">The user profile identifier.</param>
        /// <returns></returns>
        [Transactional]
        public UserProfileDetails FindUserProfileDetails(long userProfileId)
        {
            UserProfile userProfile = UserProfileDao.Find(userProfileId);

            UserProfileDetails userProfileDetails =
                new UserProfileDetails(userProfile.firstName,
                    userProfile.lastName, userProfile.email, userProfile.lenguage);

            return userProfileDetails;

        }

        /// <summary>
        /// Logins the specified login name.
        /// </summary>
        /// <param name="loginName">Name of the login.</param>
        /// <param name="password">The password.</param>
        /// <param name="passwordIsEncrypted">if set to <c>true</c> [password is encrypted].</param>
        /// <returns></returns>
        /// <exception cref="Es.Udc.DotNet.PracticaMad.Model.Services.UserService.Exceptions.IncorrectPasswordException">
        /// </exception>
        [Transactional]
        public LoginResult Login(String loginName, String password,
            Boolean passwordIsEncrypted)
        {
            UserProfile userProfile =
                      UserProfileDao.FindByLoginName(loginName);

            String storedPassword = userProfile.userPassword;

            if (passwordIsEncrypted)
            {
                if (!password.Equals(storedPassword))
                {
                    throw new IncorrectPasswordException(loginName);
                }
            }
            else
            {
                if (!PasswordEncrypter.IsClearPasswordCorrect(password,
                        storedPassword))
                {
                    throw new IncorrectPasswordException(loginName);
                }
            }

            
            return new LoginResult(userProfile.userId, userProfile.firstName,
                storedPassword, userProfile.lenguage);

        }

        /// <summary>
        /// Registers the user.
        /// </summary>
        /// <param name="loginName">Name of the login.</param>
        /// <param name="clearPassword">The clear password.</param>
        /// <param name="userProfileDetails">The user profile details.</param>
        /// <returns></returns>
        /// <exception cref="Es.Udc.DotNet.ModelUtil.Exceptions.DuplicateInstanceException"></exception>
        [Transactional]
        public long RegisterUser(string loginName, string clearPassword, 
            UserProfileDetails userProfileDetails)
        {
            try
            {
                UserProfileDao.FindByLoginName(loginName);

                throw new DuplicateInstanceException(loginName,
                    typeof(UserProfile).FullName);
            }
            catch (InstanceNotFoundException)
            {
                String encryptedPassword = PasswordEncrypter.Crypt(clearPassword);

                UserProfile userProfile = new UserProfile();

                userProfile.loginName = loginName;
                userProfile.userPassword = encryptedPassword;
                userProfile.firstName = userProfileDetails.FirstName;
                userProfile.lastName = userProfileDetails.Lastname;
                userProfile.email = userProfileDetails.Email;
                userProfile.lenguage = userProfileDetails.Lenguage;

                UserProfileDao.Create(userProfile);

                return userProfile.userId;
            }
        }

        /// <summary>
        /// Updates the user profile details.
        /// </summary>
        /// <param name="userProfileId">The user profile identifier.</param>
        /// <param name="userProfileDetails">The user profile details.</param>
        [Transactional]
        public void UpdateUserProfileDetails(long userProfileId,
            UserProfileDetails userProfileDetails)
        {

            UserProfile userProfile =
                  UserProfileDao.Find(userProfileId);

            userProfile.firstName = userProfileDetails.FirstName;
            userProfile.lastName = userProfileDetails.Lastname;
            userProfile.email = userProfileDetails.Email;
            userProfile.lenguage = userProfileDetails.Lenguage;
            UserProfileDao.Update(userProfile);
        }

        /// <summary>
        /// Users the exists.
        /// </summary>
        /// <param name="loginName">Name of the login.</param>
        /// <returns></returns>
        public bool UserExists(string loginName)
        {
            try
            {
                UserProfile userProfile = UserProfileDao.FindByLoginName(loginName);
            }
            catch (InstanceNotFoundException)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Gets the followers.
        /// </summary>
        /// <param name="userProfileId">The user profile identifier.</param>
        /// <returns></returns>
        [Transactional]
        public List<UserProfile> GetFollowers(long userProfileId)
        {
            return UserProfileDao.FindByFollower(userProfileId);

        }

        /// <summary>
        /// Gets the followeds.
        /// </summary>
        /// <param name="userProfileId">The user profile identifier.</param>
        /// <returns></returns>
        [Transactional]
        public List<UserProfile> GetFolloweds(long userProfileId)
        {
            return UserProfileDao.FindByFollowed(userProfileId);

        }

        /// <summary>
        /// Follows the user.
        /// </summary>
        /// <param name="userProfileId">The user profile identifier.</param>
        /// <param name="userIdToFollow">The user identifier to follow.</param>
        /// <exception cref="InstanceNotFoundException"></exception>
        [Transactional]
        public void FollowUser(long userProfileId, long userIdToFollow)
        {

            List<UserProfile> followeds = UserProfileDao.FindByFollowed(userProfileId);

            bool containsItem = followeds.Any(u => u.userId == userIdToFollow); //comprobacion
            if (!containsItem)
            {
                UserProfileDao.UpdateFollowUser(userProfileId, userIdToFollow);
            }
        }


    }
}