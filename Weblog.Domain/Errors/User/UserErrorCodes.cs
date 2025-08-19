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





    }
}