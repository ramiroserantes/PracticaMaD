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

    }

}

