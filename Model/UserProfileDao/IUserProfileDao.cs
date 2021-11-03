using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao
{
    public interface IUserProfileDao : IGenericDao<UserProfile, long>
    {
        UserProfile FindByLoginName(string loginName);

        List<UserProfile> FindByFollower(long userId);

        List<UserProfile> FindByFollowed(long userId);
    }
}
