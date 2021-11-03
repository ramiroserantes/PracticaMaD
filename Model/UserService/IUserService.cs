using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Ninject;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserService
{
    public interface IUserService
    {
        [Inject]
        IUserProfileDao UserProfileDao { set; }

        void ChangePassword(long userProfileId, string oldClearPassword,
            string newClearPassword);

        [Transactional]
        UserProfileDetails FindUserProfileDetails(long userProfileId);
 
        [Transactional]
        LoginResult Login(string loginName, string password,
            Boolean passwordIsEncrypted);

        [Transactional]
        long RegisterUser(string loginName, string clearPassword,
            UserProfileDetails userProfileDetails);

        [Transactional]
        void UpdateUserProfileDetails(long userProfileId,
            UserProfileDetails userProfileDetails);

        bool UserExists(string loginName);

        [Transactional]
        UserProfile GetFollowers(long userProfileId);

        [Transactional]
        UserProfile GetFollowed(long userProfileId);

    }
}





