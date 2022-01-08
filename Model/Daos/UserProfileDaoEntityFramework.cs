using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMad.Model.UserProfileDao
{
    /// <seealso cref="Es.Udc.DotNet.ModelUtil.Dao.GenericDaoEntityFramework&lt;Es.Udc.DotNet.PracticaMad.Model.UserProfile, System.Int64&gt;" />
    /// <seealso cref="Es.Udc.DotNet.PracticaMad.Model.UserProfileDao.IUserProfileDao" />
    public class UserProfileDaoEntityFramework :
        GenericDaoEntityFramework<UserProfile, Int64>, IUserProfileDao
    {
        /// <summary>
        /// Finds a UserProfile by loginName
        /// </summary>
        /// <param name="loginName">loginName</param>
        /// <returns>
        /// The UserProfile
        /// </returns>
        /// <exception cref="Es.Udc.DotNet.ModelUtil.Exceptions.InstanceNotFoundException"></exception>
        public UserProfile FindByLoginName(string loginName)
        {
            UserProfile userProfile = null;
 

            DbSet<UserProfile> userProfiles = Context.Set<UserProfile>();

            var result =
                (from u in userProfiles
                 where u.loginName == loginName
                 select u);

            userProfile = result.FirstOrDefault();

            if (userProfile == null)
                throw new InstanceNotFoundException(loginName,
                    typeof(UserProfile).FullName);

            return userProfile;
        }

        /// <summary>
        /// Finds the by follower.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public List<UserProfile> FindByFollower(long userId)
        {
            DbSet<UserProfile> userProfiles = Context.Set<UserProfile>();

            return (
                from l in userProfiles
                where l.UserProfile2.Any(c => c.userId == userId)
                select l).ToList();

        }

        /// <summary>
        /// Finds the by followed.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public List<UserProfile> FindByFollowed(long userId)
        {
            DbSet<UserProfile> userProfiles = Context.Set<UserProfile>();

            return (
                from l in userProfiles
                where l.UserProfile1.Any(c => c.userId == userId)
                select l).ToList();

        }

        /// <summary>
        /// Follows an user.
        /// </summary>
        /// <param name="userId">The user who follows the other.</param>
        /// <param name="userId2">The user who is going to be followed.</param>
        /// <returns></returns>
        public void UpdateFollowUser(long userId, long userId2)
        {
            DbSet<UserProfile> userProfiles = Context.Set<UserProfile>();

            UserProfile userProfile = null;
            UserProfile userProfile2 = null;

            var result =
                (from u in userProfiles
                 where u.userId == userId
                 select u);

            var result2 =
                (from u in userProfiles
                 where u.userId == userId2
                 select u);

            userProfile = result.FirstOrDefault();
            userProfile2 = result2.FirstOrDefault();

            userProfile.UserProfile2.Add(userProfile2);
            this.Update(userProfile);

        }

        /// <summary>
        /// Unfollows an user.
        /// </summary>
        /// <param name="userId">The user who follows the other.</param>
        /// <param name="userId2">The user who is going to be unfollowed.</param>
        /// <returns></returns>
        public void UpdateUnfollowUser(long userId, long userId2)
        {
            DbSet<UserProfile> userProfiles = Context.Set<UserProfile>();

            UserProfile userProfile = null;
            UserProfile userProfile2 = null;

            var result =
                (from u in userProfiles
                 where u.userId == userId
                 select u);

            var result2 =
                (from u in userProfiles
                 where u.userId == userId2
                 select u);

            userProfile = result.FirstOrDefault();
            userProfile2 = result2.FirstOrDefault();

            userProfile.UserProfile2.Remove(userProfile2);
            this.Update(userProfile);
        }

        /// <summary>
        /// Finds list with liked photos.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public List<Photo> FindByLikedPhotos(long userId)
        {
            DbSet<Photo> photos = Context.Set<Photo>();

            return (
                from l in photos
                where l.UserProfile1.Any(c => c.userId == userId)
                select l).ToList();

        }

        /// <summary>
        /// Generates a like from a user.
        /// </summary>
        /// <param name="userId">The user who likes the photo.</param>
        /// <param name="photoId">The photo that is liked.</param>
        /// <returns></returns>
        public void UpdateLikePhoto(long userId, long photoId)
        {
            DbSet<UserProfile> userProfiles = Context.Set<UserProfile>();
            DbSet<Photo> photos = Context.Set<Photo>();

            UserProfile userProfile = null;
            Photo photo = null;

            var result =
                (from u in userProfiles
                 where u.userId == userId
                 select u);

            var result2 =
                (from u in photos
                 where u.photoId == photoId
                 select u);

            userProfile = result.FirstOrDefault();
            photo = result2.FirstOrDefault();

            userProfile.Photo1.Add(photo);
            this.Update(userProfile);

        }

        /// <summary>
        /// Deletes a like from a user.
        /// </summary>
        /// <param name="userId">The user who likes the photo.</param>
        /// <param name="photoId">The photo that is liked.</param>
        /// <returns></returns>
        public void DeleteLikePhoto(long userId, long photoId)
        {
            DbSet<UserProfile> userProfiles = Context.Set<UserProfile>();
            DbSet<Photo> photos = Context.Set<Photo>();

            UserProfile userProfile = null;
            Photo photo = null;

            var result =
                (from u in userProfiles
                 where u.userId == userId
                 select u);

            var result2 =
                (from u in photos
                 where u.photoId == photoId
                 select u);

            userProfile = result.FirstOrDefault();
            photo = result2.FirstOrDefault();

            userProfile.Photo1.Remove(photo);
            this.Update(userProfile);

        }

        public long FindByPhotoId(long photoId)
        {
          
            Photo photo = null;
            DbSet<Photo> photos = Context.Set<Photo>();

            var result =
                (from p in photos
                 where p.photoId == photoId
                 select p);

            photo = result.FirstOrDefault();

            if (photo == null)
                throw new InstanceNotFoundException(photoId,
                    typeof(Photo).FullName);

            return photo.userId;
        }
    }

}

