using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblog.Domain.Errors.Tag
{
    public class TagErrorCodes
    {
        public const string TagNotFound = "TAG_NOT_FOUND";
        public const string TagTitleRequired = "TAG_TITLE_REQUIRED";
        public const string TagTitleMaxLength = "TAG_TITLE_MAX_LENGTH";


    }
}