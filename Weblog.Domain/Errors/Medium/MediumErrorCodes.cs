using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblog.Domain.Errors.Medium
{
    public class MediumErrorCodes
    {
        public const string MediumNotFound = "MEDIUM_NOT_FOUND";
        public const string MediumParentIdInvalid = "MEDIUM_PARENT_ID_INVALID";
        public const string MediumFileInvalid = "MEDIUM_FILE_INVALID";
        public const string MediumDeleteForbidden = "MEDIUM_DELETE_FORBIDDEN";
        public const string MediumNameMaxLength = "MEDIUM_NAME_MAX_LENGTH";
        public const string MediumNameRequired = "MEDIUM_NAME_REQUIRED";
        public const string MediumPathMaxLength = "MEDIUM_PATH_LENGTH";
        public const string MediumPathRequired = "MEDIUM_PATH_REQUIRED";
        public const string MediumAltTextMaxLength = "MEDIUM_ALT_TEXT_LENGTH";
        public const string MediumAltTextRequired = "MEDIUM_ALT_TEXT_REQUIRED";
        public const string MediumFileRequired = "MEDIUM_FILE_REQUIRED";
        public const string MediumFileSizes = "MEDIUM_FILE_SIZE";
        public const string MediumEntityTypeInvalid = "MEDIUM_ENTITY_TYPE_INVALID";
        public const string MediumTypeInvalid = "MEDIUM_TYPE_INVALID";

    }
}