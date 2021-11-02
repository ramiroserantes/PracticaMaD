using System;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserService
{
    [Serializable()]
    public class LoginResult
    {
        public LoginResult(long userProfileId, String firstName,
            String encryptedPassword)
        {
            this.UserProfileId = userProfileId;
            this.FirstName = firstName;
            this.EncryptedPassword = encryptedPassword;
            //this.Internalization = internalization;
        }

        #region Properties Region

        public string EncryptedPassword { get; private set; }

        public Enum Internalization { get; private set; }

        public string FirstName { get; private set; }

        public long UserProfileId { get; private set; }

        #endregion Properties Region

        public override bool Equals(object obj)
        {
            LoginResult target = (LoginResult)obj;

            return (this.UserProfileId == target.UserProfileId)
                   && (this.FirstName == target.FirstName)
                   && (this.EncryptedPassword == target.EncryptedPassword);
                   //&& (this.Internalization == target.Internalization);
        }

        public override int GetHashCode()
        {
            return this.UserProfileId.GetHashCode();
        }

        public override String ToString()
        {
            String strLoginResult;

            strLoginResult =
                "[ userProfileId = " + UserProfileId + " | " +
                "firstName = " + FirstName + " | " +
                "encryptedPassword = " + EncryptedPassword + " | " +
                "language = " + Internalization + " ]";

            return strLoginResult;
        }
    }
}