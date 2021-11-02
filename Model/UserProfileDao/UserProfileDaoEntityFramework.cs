using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System.Data.Entity;
using System.Linq;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao
{
    public class UserProfileDaoEntityFramework :
        GenericDaoEntityFramework<UserProfile, long>, IUserProfileDao
    {
        #region Public Constructors
        public UserProfileDaoEntityFramework()
        {
        }
        #endregion Public Constructors

        #region IUserProfileDao Members. Specific Operations
        public UserProfile FindByLoginName(string loginName)
        {
            UserProfile userProfile = null;

            #region Option 1: Using Linq.
            DbSet<UserProfile> userProfiles = Context.Set<UserProfile>();

            var result =
                (from u in userProfiles
                 where u.loginName == loginName
                 select u);

            userProfile = result.FirstOrDefault();
            #endregion Option 1: Using Linq.

            if (userProfile == null)
                throw new InstanceNotFoundException(loginName,
                    typeof(UserProfile).FullName);

            return userProfile;
        }

        #endregion IUserProfileDao Members
    }
}
