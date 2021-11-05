using System;
using System.Collections.Generic;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMad.Model.Services.UserService.Exceptions;
using Es.Udc.DotNet.PracticaMad.Model.Services.UserService.Util;
using Es.Udc.DotNet.PracticaMad.Model.UserProfileDao;
using Es.Udc.DotNet.PracticaMad.Model.UserService;
using Ninject;

namespace Es.Udc.DotNet.PracticaMad.Model.Services.UserService
{
    public class UserService : IUserService
    {

        [Inject]
        public IUserProfileDao UserProfileDao { private get; set; }

        /*public void ChangePassword(long userProfileId, string oldClearPassword,
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

        [Transactional]
        public UserProfileDetails FindUserProfileDetails(long userProfileId)
        {
            UserProfile userProfile = UserProfileDao.Find(userProfileId);

            UserProfileDetails userProfileDetails =
                new UserProfileDetails(userProfile.firstName,
                    userProfile.lastName, userProfile.email, userProfile.lenguage);

            return userProfileDetails;

        }

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

        }*/

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

        /*[Transactional]
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

        [Transactional]
        public List<UserProfile> GetFollowers(long userProfileId)
        {
            return UserProfileDao.FindByFollower(userProfileId);

        }

        [Transactional]
        public List<UserProfile> GetFolloweds(long userProfileId)
        {
            return UserProfileDao.FindByFollowed(userProfileId);

        }*/

    }
}