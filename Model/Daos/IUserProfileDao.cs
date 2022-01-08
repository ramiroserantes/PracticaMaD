using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System.Collections.Generic;
using System;


namespace Es.Udc.DotNet.PracticaMad.Model.UserProfileDao
{
    /// <seealso cref="Es.Udc.DotNet.ModelUtil.Dao.IGenericDao&lt;Es.Udc.DotNet.PracticaMad.Model.UserProfile, System.Int64&gt;" />
    public interface IUserProfileDao : IGenericDao<UserProfile, Int64>
    {

        /// <summary>
        /// Finds a UserProfile by loginName
        /// </summary>
        /// <param name="loginName">loginName</param>
        /// <returns>
        /// The UserProfile
        /// </returns>
        /// <exception cref="InstanceNotFoundException"></exception>
        UserProfile FindByLoginName(String loginName);

        /// <summary>
        /// Finds the by follower.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        List<UserProfile> FindByFollower(long userId);

        /// <summary>
        /// Finds the by followed.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        List<UserProfile> FindByFollowed(long userId);

        void UpdateFollowUser(long userId, long userId2);

        void UpdateUnfollowUser(long userId, long userId2);

        List<Photo> FindByLikedPhotos(long userId);

        void UpdateLikePhoto(long userId, long photoId);

        void DeleteLikePhoto(long userId, long photoId);

        long FindByPhotoId(long photoId);
    }
}
