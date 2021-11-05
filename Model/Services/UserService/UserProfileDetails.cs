using System;


namespace Es.Udc.DotNet.PracticaMad.Model.Services.UserService
{
    public class UserProfileDetails 
    {

        public long UserId { get; private set; }

        public String LoginName { get; private set; }

        public String FirstName { get; private set; }

        public String Lastname { get; private set; }

        public String Email { get; private set; }

        public String Lenguage { get; private set; }


        public UserProfileDetails(String loginName, String firstName, String lastName,
            String email, String lenguage)
        {
            this.LoginName = loginName;
            this.FirstName = firstName;
            this.Lastname = lastName;
            this.Email = email;
            this.Lenguage = lenguage;
        }

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

        public UserProfileDetails(String firstName, String lastName,
            String email, String lenguage)
        {
            this.FirstName = firstName;
            this.Lastname = lastName;
            this.Email = email;
            this.Lenguage = lenguage;
        }

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