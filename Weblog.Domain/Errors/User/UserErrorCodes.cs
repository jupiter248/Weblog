using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblog.Domain.Errors.User
{
    public class UserErrorCodes
    {
        public const string UserNotFound = "USER_NOT_FOUND";
        public const string InvalidUsername = "INVALID_USERNAME";
        public const string IncorrectPassword = "INCORRECT_PASSWORD";
        public const string RoleAssignFailed = "ROLE_ASSIGN_FAILED";
        public const string UserCreateFailed = "USER_CREATE_FAILED";
        public const string PasswordChangeFailed = "PASSWORD_CHANGE_FAILED";
        public const string UsernameRequired = "USERNAME_REQUIRED";
        public const string UsernameMaxLength = "USERNAME_MAX_LENGTH";
        public const string UserFirstNameMaxLength = "USER_FIRST_NAME_MAX_LENGTH";
        public const string UserFirstNameRequired = "USER_FIRST_NAME_REQUIRED";
        public const string UserLastNameMaxLength = "USER_LAST_NAME_MAX_LENGTH";
        public const string UserLastNameRequired = "USER_LAST_NAME_REQUIRED";
        public const string PhoneRequired = "PHONE_REQUIRED";
        public const string InvalidPhone = "INVALID_PHONE";
        public const string PasswordRequired = "PASSWORD_REQUIRED";
        public const string PasswordLength = "PASSWORD_LENGTH";




        

    }
}