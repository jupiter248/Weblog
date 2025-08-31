using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblog.Domain.Errors.Contributor
{
    public class ContributorErrorCodes
    {
        public const string ContributorNotFound = "CONTRIBUTOR_NOT_FOUND";
        public const string ContributorFirstNameRequired = "CONTRIBUTOR_FIRST_NAME_REQUIRED";
        public const string ContributorLastNameRequired = "CONTRIBUTOR_LAST_NAME_REQUIRED";
        public const string ContributorFirstNameLength = "CONTRIBUTOR_FIRST_NAME_LENGTH";
        public const string ContributorLastNameLength = "CONTRIBUTOR_LAST_NAME_LENGTH";
        public const string ContributorDescriptionRequired = "CONTRIBUTOR_DESCRIPTION_REQUIRED";
        public const string ContributorDescriptionMaxLength = "CONTRIBUTOR_DESCRIPTION_MAX_LENGTH";
    }
}