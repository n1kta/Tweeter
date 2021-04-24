using System;
using System.Collections.Generic;
using System.Text;

namespace Tweeter.Application.ExceptionMessage
{
    public static class AuthExceptionMessages
    {
        public const string USER_ALREADY_EXIST = "User already exist. Pleas pick another Email or Username.";
        public const string INVALID_USERNAME_OR_PASSWORD =
            "Invalid Username or Password. Please try again with another Username or Password.";
    }
}
