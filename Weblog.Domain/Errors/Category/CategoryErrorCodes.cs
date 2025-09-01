using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblog.Domain.Errors.Category
{
    public class CategoryErrorCodes
    {
        public const string CategoryNotFound = "CATEGORY_NOT_FOUND";
        public const string CategoryNameRequired = "CATEGORY_NAME_REQUIRED";
        public const string CategoryNameMaxLength = "CATEGORY_NAME_MAX_LENGTH";
        public const string CategoryDescriptionRequired = "CATEGORY_DESCRIPTION_REQUIRED";
        public const string CategoryDescriptionMaxLength = "CATEGORY_DESCRIPTION_MAX_LENGTH";
        public const string CategoryInvalidEntityType = "CATEGORY_INVALID_ENTITY_TYPE";
        public const string CategoryEntityTypeRequired = "CATEGORY_ENTITY_TYPE_REQUIRED";
        public const string CategoryEntityTypeMatchFailed = "CATEGORY_ENTITY_TYPE_MATCH_FAILED";



    }
}