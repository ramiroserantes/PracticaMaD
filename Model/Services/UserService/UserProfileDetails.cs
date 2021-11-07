using System;


namespace Es.Udc.DotNet.PracticaMad.Model.Services.UserService
{
    public class UserProfileDetails 
    {

        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public long UserId { get; private set; }

        /// <summary>
        /// Gets the name of the login.
        /// </summary>
        /// <value>
        /// The name of the login.
        /// </value>
        public String LoginName { get; private set; }

        /// <summary>
        /// Gets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public String FirstName { get; private set; }

        /// <summary>
        /// Gets the lastname.
        /// </summary>
        /// <value>
        /// The lastname.
        /// </value>
        public String Lastname { get; private set; }

        /// <summary>
        /// Gets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public String Email { get; private set; }

        /// <summary>
        /// Gets the lenguage.
        /// </summary>
        /// <value>
        /// The lenguage.
        /// </value>
        public String Lenguage { get; private set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="UserProfileDetails"/> class.
        /// </summary>
        /// <param name="loginName">Name of the login.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="email">The email.</param>
        /// <param name="lenguage">The lenguage.</param>
        public UserProfileDetails(String loginName, String firstName, String lastName,
            String email, String lenguage)
        {
            this.LoginName = loginName;
            this.FirstName = firstName;
            this.Lastname = lastName;
            this.Email = email;
            this.Lenguage = lenguage;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserProfileDetails"/> class.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="loginName">Name of the login.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="email">The email.</param>
        /// <param name="lenguage">The lenguage.</param>
        public UserProfileDetails(long userId, String loginName, String firstName, String lastName,
            String email, String lenguage)
        {
            this.UserId = userId;
            this.LoginName = loginName;
            this.FirstName = firstName;
            this.Lastname = lastName;
            this.Email = email;
            this.Lenguage = lenguage;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserProfileDetails"/> class.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="email">The email.</param>
        /// <param name="lenguage">The lenguage.</param>
        public UserProfileDetails(String firstName, String lastName,
            String email, String lenguage)
        {
            this.FirstName = firstName;
            this.Lastname = lastName;
            this.Email = email;
            this.Lenguage = lenguage;
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {

            UserProfileDetails target = (UserProfileDetails)obj;

            return (this.LoginName == target.LoginName)
                  && (this.FirstName == target.FirstName)
                  && (this.Lastname == target.Lastname)
                  && (this.Email == target.Email)
                  && (this.Lenguage == target.Lenguage);
                
        }

        // The GetHashCode method is used in hashing algorithms and data 
        // structures such as a hash table. In order to ensure that it works 
        // properly, we suppose that the FirstName does not change.        
        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return this.FirstName.GetHashCode();
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"></see> that represents the
        /// current <see cref="T:System.Object"></see>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"></see> that represents the current
        /// <see cref="T:System.Object"></see>.
        /// </returns>
        public override String ToString()
        {
            String strUserProfileDetails;

            strUserProfileDetails =
                "[ loginName = " + LoginName + " | " + 
                "firstName = " + FirstName + " | " +
                "lastName = " + Lastname + " | " +
                "email = " + Email + " | " +
                "lenguage = " + Lenguage + " ]";


            return strUserProfileDetails;
        }
    }
}