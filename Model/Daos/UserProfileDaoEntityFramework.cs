using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMad.Model.UserProfileDao
{
    public class UserProfileDaoEntityFramework :
        GenericDaoEntityFramework<UserProfile, Int64>, IUserProfileDao
    {
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

        public List<UserProfile> FindByFollower(long userId)
        {
            DbSet<UserProfile> userProfiles = Context.Set<UserProfile>();

            return (
                from l in userProfiles
                where l.UserProfile2.Any(c => c.userId == userId)
                select l).ToList();

        }

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
