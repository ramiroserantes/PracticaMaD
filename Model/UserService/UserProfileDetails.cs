using System;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserService
{
    public class UserProfileDetails
    {
        #region Properties Region

        public string firstName { get; private set; }

        public string lastName { get; private set; }

        public string email { get; private set; }

        #endregion
        public UserProfileDetails(string firstName, string lastName, string email)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
        }

        public override bool Equals(object obj)
        {
            UserProfileDetails target = (UserProfileDetails)obj;

            return (this.firstName == target.firstName)
                  && (this.lastName == target.lastName)
                  && (this.email == target.email);
        }
      
        public override int GetHashCode()
        {
            return this.firstName.GetHashCode();
        }

        public override String ToString()
        {
            String strUserProfileDetails;

            strUserProfileDetails =
                "[ firstName = " + firstName + " | " +
                "lastName = " + lastName + " | " +
                "email = " + email + " ]";

            return strUserProfileDetails;
        }
    }
}
