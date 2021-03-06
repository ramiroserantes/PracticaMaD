using System;

namespace Es.Udc.DotNet.PracticaMad.Model.Services.UserService
{

    [Serializable()]
    public class LoginResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoginResult"/> class.
        /// </summary>
        /// <param name="userProfileId">The user profile identifier.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="encryptedPassword">The encrypted password.</param>
        /// <param name="lenguage">The lenguage.</param>
        /// <param name="country">The country.</param>
        public LoginResult(long userProfileId, String firstName, String loginName,
            String encryptedPassword, string lenguage, string country)
        {
            this.UserProfileId = userProfileId;
            this.FirstName = firstName;
            this.LoginName = loginName;
            this.EncryptedPassword = encryptedPassword;
            this.Lenguage = lenguage;
            this.Country = country;
        }

        #region Properties Region

        /// <summary>
        /// Gets the encrypted password.
        /// </summary>
        /// <value>
        /// The encrypted password.
        /// </value>
        public string EncryptedPassword { get; private set; }

        /// <summary>
        /// Gets the country.
        /// </summary>
        /// <value>
        /// The country.
        /// </value>
        public string Country { get; private set; }

        public string LoginName { get; private set; }
        /// <summary>
        /// Gets the lenguage.
        /// </summary>
        /// <value>
        /// The lenguage.
        /// </value>
        public string Lenguage { get; private set; }

        /// <summary>
        /// Gets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string FirstName { get; private set; }

        /// <summary>
        /// Gets the user profile identifier.
        /// </summary>
        /// <value>
        /// The user profile identifier.
        /// </value>
        public long UserProfileId { get; private set; }

        #endregion Properties Region

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            LoginResult target = (LoginResult)obj;

            return (this.UserProfileId == target.UserProfileId)
                   && (this.FirstName == target.FirstName)
                   && (this.LoginName == target.LoginName)
                   && (this.EncryptedPassword == target.EncryptedPassword)
                   && (this.Lenguage == target.Lenguage)
                   && (this.Country == target.Country);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return this.UserProfileId.GetHashCode();
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// Una cadena que representa el objeto actual.
        /// </returns>
        public override String ToString()
        {
            String strLoginResult;

            strLoginResult =
                "[ userProfileId = " + UserProfileId + " | " +
                "firstName = " + FirstName + " | " +
                "loginName = " + LoginName + " | " +
                "encryptedPassword = " + EncryptedPassword + " | " +
                "lenguage = " + Lenguage + " | " +
                "country = " + Country + " ]";

            return strLoginResult;
        }
    }
}