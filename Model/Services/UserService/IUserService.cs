using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMad.Model.UserProfileDao;
using Es.Udc.DotNet.PracticaMad.Model.Services.UserService;
using Ninject;
using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMad.Model.Services.UserService
{

    public interface IUserService 
    {
        /// <summary>
        /// Sets the user profile DAO.
        /// </summary>
        /// <value>
        /// The user profile DAO.
        /// </value>
        [Inject]
        IUserProfileDao UserProfileDao { set; }

        /// <summary>
        /// Changes the password.
        /// </summary>
        /// <param name="userProfileId">The user profile identifier.</param>
        /// <param name="oldClearPassword">The old clear password.</param>
        /// <param name="newClearPassword">The new clear password.</param>
        void ChangePassword(long userProfileId, string oldClearPassword,
            string newClearPassword);

        /// <summary>
        /// Finds the user profile details.
        /// </summary>
        /// <param name="userProfileId">The user profile identifier.</param>
        /// <returns></returns>
        [Transactional]
        UserProfileDetails FindUserProfileDetails(long userProfileId);

        /// <summary>
        /// Logins the specified login name.
        /// </summary>
        /// <param name="loginName">Name of the login.</param>
        /// <param name="password">The password.</param>
        /// <param name="passwordIsEncrypted">if set to <c>true</c> [password is encrypted].</param>
        /// <returns></returns>
        [Transactional]
        LoginResult Login(string loginName, string password,
            Boolean passwordIsEncrypted);

        /// <summary>
        /// Registers the user.
        /// </summary>
        /// <param name="loginName">Name of the login.</param>
        /// <param name="clearPassword">The clear password.</param>
        /// <param name="userProfileDetails">The user profile details.</param>
        /// <returns></returns>
        [Transactional]
        long RegisterUser(string loginName, string clearPassword,
            UserProfileDetails userProfileDetails);

        /// <summary>
        /// Updates the user profile details.
        /// </summary>
        /// <param name="userProfileId">The user profile identifier.</param>
        /// <param name="userProfileDetails">The user profile details.</param>
        [Transactional]
        void UpdateUserProfileDetails(long userProfileId,
            UserProfileDetails userProfileDetails);

        /// <summary>
        /// Users the exists.
        /// </summary>
        /// <param name="loginName">Name of the login.</param>
        /// <returns></returns>
        bool UserExists(string loginName);

        /// <summary>
        /// Gets the followers.
        /// </summary>
        /// <param name="userProfileId">The user profile identifier.</param>
        /// <returns></returns>
        [Transactional]
        List<UserProfile> GetFollowers(long userProfileId);

        /// <summary>
        /// Gets the followeds.
        /// </summary>
        /// <param name="userProfileId">The user profile identifier.</param>
        /// <returns></returns>
        [Transactional]
        List<UserProfile> GetFolloweds(long userProfileId);

        /// <summary>
        /// Follows the user.
        /// </summary>
        /// <param name="userProfileId">The user profile identifier.</param>
        /// <param name="userIdToFollow">The user identifier to follow.</param>
        [Transactional]
        void FollowUser(long userProfileId, long userIdToFollow);

        /// <summary>
        /// Unfollows the user.
        /// </summary>
        /// <param name="userProfileId">The user profile identifier.</param>
        /// <param name="userIdToFollow">The user identifier to unfollow.</param>
        [Transactional]
        void UnfollowUser(long userProfileId, long userIdToFollow);

        /// <summary>
        /// Checks if user is already following another user.
        /// </summary>
        /// <param name="userProfileId">The user profile.</param>
        /// <param name="userIdToFollow">The other user profile.</param>
        /// <returns></returns>
        bool IsAlreadyFollowing(long userProfileId, long userIdToFollow);

        /// <summary>
        /// Gets the userProfileId from the loginName.
        /// </summary>
        /// <param name="loginName">The user profile.</param>
        /// <returns></returns>
        long GetUserProfileId(string loginName);

    }
}