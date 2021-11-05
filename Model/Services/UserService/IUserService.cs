using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMad.Model.UserProfileDao;
using Es.Udc.DotNet.PracticaMad.Model.UserService;
using Ninject;
using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMad.Model.Services.UserService
{
    public interface IUserService 
    {
        [Inject]
        IUserProfileDao UserProfileDao { set; }

        /*void ChangePassword(long userProfileId, string oldClearPassword,
            string newClearPassword);

        [Transactional]
        UserProfileDetails FindUserProfileDetails(long userProfileId);

        [Transactional]
        LoginResult Login(string loginName, string password,
            Boolean passwordIsEncrypted);*/

        [Transactional]
        long RegisterUser(string loginName, string clearPassword,
            UserProfileDetails userProfileDetails);

        /*[Transactional]
        void UpdateUserProfileDetails(long userProfileId,
            UserProfileDetails userProfileDetails);

        bool UserExists(string loginName);

        [Transactional]
        List<UserProfile> GetFollowers(long userProfileId);

        [Transactional]
        List<UserProfile> GetFolloweds(long userProfileId);*/

    }
}