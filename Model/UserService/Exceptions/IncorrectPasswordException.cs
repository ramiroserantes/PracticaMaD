using System;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserService.Exceptions
{

    [Serializable]
    public class IncorrectPasswordException : Exception
    {
        public IncorrectPasswordException(string loginName)
            : base("Incorrect password exception => loginName = " + loginName)
        {
            LoginName = loginName;
        }

        public string LoginName { get; private set; }

    }
}
